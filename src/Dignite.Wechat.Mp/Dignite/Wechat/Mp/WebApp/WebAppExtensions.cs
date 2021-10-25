using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Dignite.Wechat.Mp.WebApp
{
    public static class WebAppExtensions
    {
        /// <summary>
        /// 微信授权登陆后，进入配置的<see cref="IWebAppGrantValidateHandler"/>
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebAppGrantValidationHandler<THandler>(this IServiceCollection services)
               where THandler : class, IWebAppGrantValidateHandler
        {
            return services.AddScoped<IWebAppGrantValidateHandler, THandler>();
        }

        /// <summary>
        /// 向中间件管道注册用户网页授权中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebAppAuthorizationUrl(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationUrlMiddleware>();
        }

        /// <summary>
        /// 向中间件管道注册获取微信JSAPI签名中间件
        /// 此中间件用于生成wx.config中的几个参数值
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebAppJsapiSignature(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JsapiSignatureMiddleware>();
        }
    }
}
