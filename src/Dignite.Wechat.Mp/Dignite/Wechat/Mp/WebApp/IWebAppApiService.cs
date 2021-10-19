using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Wechat.Mp.WebApp
{
    public interface IWebAppApiService: ITransientDependency
    {
        /// <summary>
        /// 获取微信授权CODE的URL
        /// </summary>
        /// <returns></returns>
        Task<string> GetAuthorizeUrlAsync(AuthencationScope scope, string callbackUrl);

        /// <summary>
        /// 获取微信用户的AccessToken信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        Task<AuthencationAccessTokenResult> GetAccessTokenAsync(string openId);

        /// <summary>
        /// 用 code 换取AccessToken信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task<AuthencationAccessTokenResult> ExchangeAccessTokenAsync(string code, string state);

        /// <summary>
        /// 获取微信用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<AuthencationUserInfo> GetUserInfoAsync(AuthencationAccessTokenResult accessToken);

        /// <summary>
        /// 刷新AccessToken信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task RefreshTokenAsync(AuthencationAccessTokenResult accessToken);



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<JsapiTicketResult> GetJsapiTicketAsync();
    }
}
