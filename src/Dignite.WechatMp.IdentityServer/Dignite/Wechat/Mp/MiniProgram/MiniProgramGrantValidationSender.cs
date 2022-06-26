using Dignite.Wechat.Mp.Basic;
using Dignite.Wechat.Mp.IdentityServer;
using Dignite.Wechat.Mp.IdentityServer.Settings;
using Dignite.Wechat.Mp.WebApp;
using IdentityServer4.ResponseHandling;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;
using Volo.Abp.Users;

namespace Dignite.Wechat.Mp.MiniProgram
{
    /// <summary>
    /// 微信小程序在拿到code和user-info后，向IdentityServer发起登记请求
    /// </summary>
    public class MiniProgramGrantValidationSender : IMiniProgramGrantValidationSender, ITransientDependency
    {
        private readonly ISettingProvider _settingProvider;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _accessor;

        public MiniProgramGrantValidationSender( ISettingProvider settingProvider, IHttpClientFactory clientFactory, IHttpContextAccessor accessor)
        {
            _settingProvider = settingProvider;
            _clientFactory = clientFactory;
            _accessor = accessor;
        }

        public async Task<GrantValidationResult> ValidateAsync(string code, string userInfo)
        {
            var grant_type = IdentityServerConsts.WechatMiniProgramGrantType;
            var client_id = await _settingProvider.GetOrNullAsync(IdentityServerSettings.ClientId);
            var client_secret = await _settingProvider.GetOrNullAsync(IdentityServerSettings.ClientSecret);


            var client = _clientFactory.CreateClient(MpConsts.HttpClientName);
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("grant_type", grant_type),
                    new KeyValuePair<string, string>("client_id", client_id),
                    new KeyValuePair<string, string>("client_secret", client_secret),
                    new KeyValuePair<string, string>("userInfo", userInfo),
                    new KeyValuePair<string, string>("code", code)
                });
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            /* 
             * 在绑定已登陆账户时，需要将已登陆用户的TOKEN传递给 connect/token ，用于 IMiniProgramGrantValidateHandler 中获取当前用户
            */
            var requestToken = _accessor.HttpContext.Request.Headers["authorization"];
            if (requestToken.Any())
            {
                var token = requestToken[0].Replace("Bearer ", "");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var url = GetAbsoluteUri(_accessor.HttpContext.Request);
            var response = await client.PostAsync(
                url + "/connect/token",
                content,
                _accessor.HttpContext.RequestAborted
                );
            var msg = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var validationResult = JsonConvert.DeserializeObject<GrantValidationResult>(msg);
                return validationResult;
            }
            else
            {
                throw new AbpException(msg);
            }
        }

        private string GetAbsoluteUri(HttpRequest request)
        {
            return new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host.Value)
                .ToString();
        }
    }
}