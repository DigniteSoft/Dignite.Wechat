using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Wechat.Mp.IdentityServer
{
    public static class IdentityServerExtensions
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
        /// 向中间件管道注册微信登陆中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseIdentityServerLogin(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
