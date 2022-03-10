using Dignite.Wechat.Mp.Basic;
using Dignite.Wechat.Mp.IdentityServer;
using Dignite.Wechat.Mp.IdentityServer.Settings;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Wechat.Mp.WebApp
{
    /// <summary>
    /// 微信公众号程序在拿到code和state后，向IdentityServer发起登记请求
    /// </summary>
    public class WebAppGrantValidationSender : IWebAppGrantValidationSender, ITransientDependency
    {
        private readonly ISettingProvider _settingProvider;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _accessor;

        public WebAppGrantValidationSender(ISettingProvider settingProvider, IHttpClientFactory clientFactory, IHttpContextAccessor accessor)
        {
            _settingProvider = settingProvider;
            _clientFactory = clientFactory;
            _accessor = accessor;
        }

        public async Task<GrantValidationResult> ValidateAsync(string code, string state)
        {
            var grant_type = IdentityServerConsts.WechatWebAppGrantType;
            var client_id = await _settingProvider.GetOrNullAsync(IdentityServerSettings.ClientId);
            var client_secret = await _settingProvider.GetOrNullAsync(IdentityServerSettings.ClientSecret);

            var client = _clientFactory.CreateClient(MpConsts.HttpClientName);
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("grant_type", grant_type),
                    new KeyValuePair<string, string>("client_id", client_id),
                    new KeyValuePair<string, string>("client_secret", client_secret),
                    new KeyValuePair<string, string>("state", state),
                    new KeyValuePair<string, string>("code", code)
                });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var url = GetAbsoluteUri(_accessor.HttpContext.Request);
            var response = await client.PostAsync(
                url + "/connect/token",
                content,
                _accessor.HttpContext.RequestAborted
                );
            if (response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                var validationResult = JsonConvert.DeserializeObject<GrantValidationResult>(msg);
                return validationResult;
            }
            else
            {
                throw new AbpException(response.StatusCode.ToString());
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