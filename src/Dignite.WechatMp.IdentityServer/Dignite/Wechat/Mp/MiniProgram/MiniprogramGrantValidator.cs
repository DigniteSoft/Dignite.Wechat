using Dignite.Wechat.Mp.IdentityServer;
using Dignite.Wechat.Mp.MiniProgram;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dignite.Wechat.Mp.MiniProgram
{
    public class MiniprogramGrantValidator : IExtensionGrantValidator
    {
        private readonly IMiniProgramApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MiniprogramGrantValidator(
            IMiniProgramApiService apiService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GrantType => IdentityServerConsts.WechatMiniProgramGrantType;

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var handler = httpContext.RequestServices.GetService<IMiniProgramGrantValidateHandler>();


                if (handler == null)
                    throw new Exception($"请实现{nameof(IMiniProgramGrantValidateHandler)}，并注册依赖！");

                //微信小程序登陆的code
                var code = context.Request.Raw["code"];
                var userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<MiniProgramUserInfo>(context.Request.Raw["userInfo"]);
                var sessionResult = await _apiService.GetSessionTokenAsync(code);
                userInfo.OpenId = sessionResult?.OpenId;
                userInfo.UnionId = sessionResult?.UnionId;
                //Log.Information($"userInfo:{Newtonsoft.Json.JsonConvert.SerializeObject(userInfo)}");
                var grantValidationResult = await handler.ExcuteAsync(
                    new MiniProgramGrantValidationContext(httpContext, sessionResult, userInfo)
                    );

                if (grantValidationResult.errcode == 0)
                {
                    //授权通过返回
                    context.Result = new GrantValidationResult
                    (
                        subject: grantValidationResult.UserId,
                        authenticationMethod: GrantType,
                        claims: grantValidationResult.AddedClaims
                    );
                }
                else
                {
                    context.Result = new GrantValidationResult()
                    {
                        IsError = true,
                        Error = "未绑定一个用户，请跳转到账号密码登陆页面！"
                    };
                }
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult()
                {
                    IsError = true,
                    Error = ex.Message
                };
            }
        }


    }
}