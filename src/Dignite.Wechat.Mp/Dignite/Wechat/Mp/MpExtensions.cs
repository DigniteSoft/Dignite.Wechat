
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
    }
}
