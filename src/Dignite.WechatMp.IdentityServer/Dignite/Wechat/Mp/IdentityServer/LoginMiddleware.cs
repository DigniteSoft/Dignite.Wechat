using Dignite.Wechat.Mp.IdentityServer.Settings;
using Dignite.Wechat.Mp.WebApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Volo.Abp.Settings;

namespace Dignite.Wechat.Mp.IdentityServer
{
    /// <summary>
    /// 基于IdentityServer的登陆中间件
    /// </summary>
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISettingProvider _settingProvider;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _accessor;

        public LoginMiddleware(
            RequestDelegate next, 
            ISettingProvider settingProvider,
            IHttpClientFactory clientFactory,
            IHttpContextAccessor accessor)
        {
            _next = next;
            _settingProvider = settingProvider;
            _clientFactory = clientFactory;
            _accessor = accessor;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            if (HttpMethods.IsPost(request.Method))
            {
                //若当前请求不是获取微信网页授权的地址，则跳过处理，直接执行后续中间件
                if (!IdentityServerConsts.LoginPath.Equals(request.Path, StringComparison.OrdinalIgnoreCase))
                {
                    await this._next(context);
                    return;
                }

                var state = request.Form["state"];
                var code = request.Form["code"];
                var grant_type = IdentityServerConsts.GrantType;
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

                var response = await client.PostAsync("https://localhost:44393/connect/token", content, _accessor.HttpContext.RequestAborted);
                if (response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();

                    await context.Response.WriteAsync(msg);
                }
                else
                {
                    await context.Response.WriteAsync(response.StatusCode.ToString());                    
                }
            }
        }
    }
}
