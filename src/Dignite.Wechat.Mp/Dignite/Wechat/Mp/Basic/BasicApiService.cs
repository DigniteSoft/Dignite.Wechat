using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using Dignite.Wechat.Services;
using Dignite.Wechat.Mp.Settings;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp;

namespace Dignite.Wechat.Mp.Basic
{
    public class BasicApiService: ApiService, IBasicApiService
    {
        private const string accessTokenCacheKey= "dignite-wechat-mp-accesstoken-cache";

        private readonly IDistributedCache<AccessTokenResult> _accessTokenCache;

        public BasicApiService(IDistributedCache<AccessTokenResult> accessTokenCache)
        {
            _accessTokenCache = accessTokenCache;
        }

        /// <summary>
        /// 获取微信公众号全局AccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<AccessTokenResult> GetAccessTokenAsync() 
        {
            var token = await _accessTokenCache.GetAsync(accessTokenCacheKey);
            if(token==null)
            {
                token = await GetAccessTokenFromApiAsync();
                await _accessTokenCache.SetAsync(accessTokenCacheKey, token,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(token.ExpiresIn)
                    });
            }

            return token;
        }

        /// <summary>
        /// 刷新公众号AccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<AccessTokenResult> RefreshAccessTokenAsync()
        {
            await _accessTokenCache.RemoveAsync(accessTokenCacheKey);
            return await GetAccessTokenAsync();
        }

        #region private methods
        private async Task<AccessTokenResult> GetAccessTokenFromApiAsync()
        {
            var requestUrl = QueryHelpers.AddQueryString("https://api.weixin.qq.com/cgi-bin/token",
                 new Dictionary<string, string>
                {
                    { "grant_type", "client_credential" },
                    { "appid", await SettingProvider.GetOrNullAsync(WechatMpSettings.AppId) },
                    { "secret", await SettingProvider.GetOrNullAsync(WechatMpSettings.Secret) }
                });

            var client = ClientFactory.CreateClient(MpConsts.HttpClientName);

            var response = await client.GetAsync(requestUrl);
            var msg = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AccessTokenResult>(msg);

            if (result.errcode != 0)
            {
                throw new BusinessException(WechatErrorCodes.Mp.prefix + result.errcode);
            }

            return result;
        }
        #endregion
    }
}
