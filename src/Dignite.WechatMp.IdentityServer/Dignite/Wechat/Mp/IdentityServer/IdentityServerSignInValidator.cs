﻿using Dignite.Wechat.Mp.IdentityServer.Settings;
using Dignite.Wechat.Mp.WebApp;
using IdentityServer4.ResponseHandling;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Wechat.Mp.IdentityServer
{
    /// <summary>
    /// 基于IdentityServer的登陆中间件
    /// </summary>
    public class IdentityServerSignInValidator : ISignInValidator, ITransientDependency
    {
        private readonly ISettingProvider _settingProvider;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _accessor;

        public IdentityServerSignInValidator(ISettingProvider settingProvider, IHttpClientFactory clientFactory, IHttpContextAccessor accessor)
        {
            _settingProvider = settingProvider;
            _clientFactory = clientFactory;
            _accessor = accessor;
        }

        public async Task<SignInValidationResult> ValidateAsync(string code, string state)
        {
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
            var url = GetAbsoluteUri(_accessor.HttpContext.Request);
            var response = await client.PostAsync(
                url + "/connect/token",
                content,
                _accessor.HttpContext.RequestAborted
                );
            if (response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                var validationResult = JsonConvert.DeserializeObject<SignInValidationResult>(msg);
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