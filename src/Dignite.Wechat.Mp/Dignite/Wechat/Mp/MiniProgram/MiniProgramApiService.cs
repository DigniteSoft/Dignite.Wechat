
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using Dignite.Wechat.Services;
using Dignite.Wechat.Mp.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Dignite.Wechat.Mp.Basic;
using Serilog;

namespace Dignite.Wechat.Mp.MiniProgram
{
    public class MiniProgramApiService : ApiService, IMiniProgramApiService
    {
        private int ErrCode40001Count = 0;
        private const string accessTokenCacheKey = "dignite-wechat-miniprogram-accesstoken-cache";

        private readonly IDistributedCache<AccessTokenResult> _accessTokenCache;
        private readonly IHttpContextAccessor _accessor;

        public MiniProgramApiService(
            IHttpContextAccessor accessor,
            IDistributedCache<AccessTokenResult> accessTokenCache
            )
        {
            _accessor = accessor;
            _accessTokenCache = accessTokenCache;
        }


        /// <summary>
        /// 获取微信小程序的全局AccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<AccessTokenResult> GetAccessTokenAsync()
        {
            var token = await _accessTokenCache.GetAsync(accessTokenCacheKey);
            if (token == null)
            {
                token = await GetAccessTokenFromApiAsync();
                await _accessTokenCache.SetAsync(accessTokenCacheKey, token,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(token.ExpiresIn - 10)
                    });
            }

            return token;
        }

        /// <summary>
        /// 刷新微信小程序的全局AccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<AccessTokenResult> RefreshAccessTokenAsync()
        {
            await _accessTokenCache.RemoveAsync(accessTokenCacheKey);
            return await GetAccessTokenAsync();
        }



        private async Task<AccessTokenResult> GetAccessTokenFromApiAsync()
        {
            var requestUrl = QueryHelpers.AddQueryString("https://api.weixin.qq.com/cgi-bin/token",
                 new Dictionary<string, string>
                {
                    { "grant_type", "client_credential" },
                    { "appid", await SettingProvider.GetOrNullAsync(WechatMpSettings.MiniProgramAppId) },
                    { "secret", await SettingProvider.GetOrNullAsync(WechatMpSettings.MiniProgramSecret) }
                });

            var client = ClientFactory.CreateClient(MpConsts.HttpClientName);
            var response = await client.GetAsync(requestUrl);
            var msg = await response.Content.ReadAsStringAsync();
            //Log.Information($"GetAccessTokenFromApiAsync-Result:{msg}");
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessTokenResult>(msg);
            //Log.Information($"GetAccessTokenFromApiAsync-Obj-{Newtonsoft.Json.JsonConvert.SerializeObject(result)}");
            return result;
        }


        public async Task<MiniProgramSessionResult> GetSessionTokenAsync(string code)
        {
            Log.Information($"Start-GetSessionTokenAsync:{code}");
            //var token = await GetAccessTokenAsync();
            //Log.Information($"accessToken:{ token?.AccessToken}");
            var appId = await SettingProvider.GetOrNullAsync(WechatMpSettings.MiniProgramAppId);
            var secret = await SettingProvider.GetOrNullAsync(WechatMpSettings.MiniProgramSecret);
            var requestUrl = QueryHelpers.AddQueryString("https://api.weixin.qq.com/sns/jscode2session", new Dictionary<string, string>
            {
                { "appid",appId},
                { "secret",secret},
                { "js_code", code },
                { "grant_type", "authorization_code" }
            });
            //Log.Information($"jscode2session:{requestUrl}");
            var client = ClientFactory.CreateClient(MpConsts.HttpClientName);
            var response = await client.GetAsync(requestUrl, _accessor.HttpContext.RequestAborted);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"An error occurred when retrieving wechat mini program user information ({response.StatusCode}). Please check if the authentication information is correct and the corresponding Microsoft Account API is enabled.");

            var content = await response.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MiniProgramSessionResult>(content);
            //Log.Information($"jscode2session-Result:{content}");
            return result;
        }

        public async Task<WechatResult> SendTemplateMessageAsync(string touser, string template_id, IDictionary<string, MiniProgramTemplateMessageData> data, string page = null)
        {
            var access_token = (await GetAccessTokenAsync()).AccessToken;
            //var miniprogram_state = "developer";
            var json = JsonSerializer.Serialize(new
            {
                touser,
                template_id,
                // miniprogram_state,
                page,
                data
            });
            var content = new StringContent(json.Replace('[', '}').Replace(']', '}'), Encoding.UTF8, "application/json");


            var client = ClientFactory.CreateClient(MpConsts.HttpClientName);
            var response = await client.PostAsync("https://api.weixin.qq.com/cgi-bin/message/subscribe/send?access_token=" + access_token, content);

            var result = JsonSerializer.Deserialize<WechatResult>(await response.Content.ReadAsStringAsync());

            if (result.errcode != 0)
            {
                if (result.errcode == 40001 && ErrCode40001Count <= 5)
                {
                    ErrCode40001Count++;
                    await RefreshAccessTokenAsync();
                    await this.SendTemplateMessageAsync(touser, template_id, data, page);
                }
            }
            return result;
        }


        public async Task<WechatResult> MessageSecurityCheckAsync(string content)
        {
            var access_token = (await GetAccessTokenAsync()).AccessToken;

            var contents = new StringContent(JsonSerializer.Serialize(new { content }), Encoding.UTF8, "application/json");

            var client = ClientFactory.CreateClient(MpConsts.HttpClientName);
            var response = await client.PostAsync("https://api.weixin.qq.com/wxa/msg_sec_check?access_token=" + access_token, contents);

            var result = JsonSerializer.Deserialize<WechatResult>(await response.Content.ReadAsStringAsync());
            if (result.errcode != 0)
            {
                if (result.errcode == 40001 && ErrCode40001Count <= 5)
                {
                    ErrCode40001Count++;
                    await RefreshAccessTokenAsync();
                    await this.MessageSecurityCheckAsync(content);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取小程序页面的二维码
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<QrCodeResult> GetQrCode(string path)
        {
            var access_token = (await GetAccessTokenAsync()).AccessToken;

            var parameters = new StringContent(JsonSerializer.Serialize(new { path }), Encoding.UTF8, "application/json");

            var client = ClientFactory.CreateClient(MpConsts.HttpClientName);
            var response = await client.PostAsync("https://api.weixin.qq.com/wxa/getwxacode?access_token=" + access_token, parameters);

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<QrCodeResult>(content);
                return result;
            }
            catch
            {
                QrCodeResult qrCodeResult = new QrCodeResult();
                qrCodeResult.Buffer = await response.Content.ReadAsByteArrayAsync();
                return qrCodeResult;
            }
        }
    }
}
