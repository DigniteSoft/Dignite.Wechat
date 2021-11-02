using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dignite.Wechat.Mp.WebApp
{
    /// <summary>
    /// 基于IdentityServer的登陆中间件
    /// </summary>
    public class SignInMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISignInValidator _grantValidationSender;

        public SignInMiddleware(RequestDelegate next, ISignInValidator grantValidationSender)
        {
            _next = next;
            _grantValidationSender = grantValidationSender;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            if (HttpMethods.IsPost(request.Method))
            {
                //若当前请求不是获取微信网页授权的地址，则跳过处理，直接执行后续中间件
                if (!WebAppConsts.SignInPath.Equals(request.Path, StringComparison.OrdinalIgnoreCase))
                {
                    await this._next(context);
                    return;
                }

                var state = request.Form["state"];
                var code = request.Form["code"];

                var result = await _grantValidationSender.ValidateAsync(code, state);

                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
            else
            {
                await this._next(context);
                return;
            }
        }
    }
}
