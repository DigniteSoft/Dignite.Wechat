using Dignite.Wechat.Mp.MiniProgram;
using Dignite.Wechat.Mp.WebApp;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Wechat.Mp.IdentityServer
{
    public static class IdentityServerExtensions
    {

        /// <summary>
        /// 微信公众号网页授权登陆后，进入配置的<see cref="IWebAppGrantValidationHandler"/>
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebAppGrantValidationHandler<THandler>(this IServiceCollection services)
               where THandler : class, IWebAppGrantValidationHandler
        {
            return services.AddScoped<IWebAppGrantValidationHandler, THandler>();
        }


        /// <summary>
        /// 微信小程序授权登陆后，进入配置的<see cref="IMiniProgramGrantValidateHandler"/>
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMiniProgramGrantValidationHandler<THandler>(this IServiceCollection services)
               where THandler : class, IMiniProgramGrantValidateHandler
        {
            return services.AddScoped<IMiniProgramGrantValidateHandler, THandler>();
        }
    }
}
