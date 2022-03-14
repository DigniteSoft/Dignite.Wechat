
using Dignite.Wechat.Mp.MiniProgram;
using Dignite.Wechat.Mp.WebApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Wechat.Mp
{
    /// <summary>
    /// 注册微信小程序需要的公共服务
    /// </summary>
    public static class MpExtensions
    {
        public static IServiceCollection AddWechatMp(this IServiceCollection services)
        {
            services.AddHttpClient(MpConsts.HttpClientName, client =>
            {
                //这里可以统一对client进行配置
            });

            return services;
        }

        /// <summary>
        /// 向中间件管道注册微信小程序登陆中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWechatMp(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiniProgramGrantValidationMiddleware>()
                .UseMiddleware<WebAppGrantValidationMiddleware>()
                .UseMiddleware<AuthorizationUrlMiddleware>()
                .UseMiddleware<JsapiSignatureMiddleware>();
        }
    }
}
