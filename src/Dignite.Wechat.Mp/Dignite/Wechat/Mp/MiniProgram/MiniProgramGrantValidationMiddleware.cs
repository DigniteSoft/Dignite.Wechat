using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Dignite.Wechat.Mp.MiniProgram
{
    /// <summary>
    /// 基于IdentityServer的登陆中间件
    /// </summary>
    public class MiniProgramGrantValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMiniProgramGrantValidationSender _grantValidationSender;

        public MiniProgramGrantValidationMiddleware(RequestDelegate next, IMiniProgramGrantValidationSender grantValidationSender)
        {
            _next = next;
            _grantValidationSender = grantValidationSender;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            if (HttpMethods.IsGet(request.Method))
            {
                //若当前请求不是获取微信网页授权的地址，则跳过处理，直接执行后续中间件
                if (!MiniProgramConsts.SignInPath.Equals(request.Path, StringComparison.OrdinalIgnoreCase))
                {
                    await this._next(context);
                    return;
                }
                var userInfo = request.Query["userInfo"];
                userInfo = HttpUtility.UrlDecode(userInfo);
                var code = request.Query["code"];
                var result = await _grantValidationSender.ValidateAsync(code, userInfo);

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
