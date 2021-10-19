using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Dignite.Wechat.Mp.WebApp
{
    public static class WebAppExtensions
    {
        /// <summary>
        /// 添加用户授权的Handler
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddGrantValidationHandler<THandler>(this IServiceCollection services)
               where THandler : class, IWebAppGrantValidateHandler
        {
            return services.AddScoped<IWebAppGrantValidateHandler, THandler>();
        }


        /// <summary>
        /// 向中间件管道注册获取微信JSAPI签名中间件
        /// 此中间件用于生成wx.config中的几个参数值
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJsapiSignature(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JsapiSignatureMiddleware>();
        }
    }
}
