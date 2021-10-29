using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Wechat.Mp.IdentityServer
{
    public static class IdentityServerExtensions
    {

        /// <summary>
        /// 微信授权登陆后，进入配置的<see cref="IWebAppGrantValidationHandler"/>
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebAppGrantValidationHandler<THandler>(this IServiceCollection services)
               where THandler : class, IWebAppGrantValidationHandler
        {
            return services.AddScoped<IWebAppGrantValidationHandler, THandler>();
        }
    }
}
