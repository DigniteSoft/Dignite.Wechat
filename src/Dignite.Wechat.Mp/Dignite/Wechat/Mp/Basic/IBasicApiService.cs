using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Wechat.Mp.Basic
{
    public interface IBasicApiService: ITransientDependency
    {
        /// <summary>
        /// 获取微信公众号全局AccessToken
        /// </summary>
        /// <returns></returns>
        Task<AccessTokenResult> GetAccessTokenAsync();

        /// <summary>
        /// 刷新公众号AccessToken
        /// </summary>
        /// <returns></returns>
        Task<AccessTokenResult> RefreshAccessTokenAsync();
    }
}
