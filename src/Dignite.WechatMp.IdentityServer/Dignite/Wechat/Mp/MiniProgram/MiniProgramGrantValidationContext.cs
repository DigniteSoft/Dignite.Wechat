using Dignite.Wechat.Mp.MiniProgram;
using Microsoft.AspNetCore.Http;


namespace Dignite.Wechat.Mp.MiniProgram
{
    /// <summary>
    /// 调用方的实现IWeChatMiniProgramLoginHandler时会使用到的上下文对象
    /// 具体包含哪些内容请看定义
    /// </summary>
    public class MiniProgramGrantValidationContext
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpContext HttpContext { get; }

        /// <summary>
        /// 在<see cref="IMiniProgramApiService.GetSessionTokenAsync(string)"/>方法中使用微信小程序code换取的SessionToken
        /// </summary>
        public MiniProgramSessionResult SessionToken { get; }

        /// <summary>
        /// 微信小程序中获取到的用户信息
        /// </summary>
        public MiniProgramUserInfo UserInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="sessionToken"></param>
        /// <param name="userInfo"></param>
        public MiniProgramGrantValidationContext(
            HttpContext httpContext, 
            MiniProgramSessionResult sessionToken,
            MiniProgramUserInfo userInfo)
        {
            HttpContext = httpContext;
            SessionToken = sessionToken;
            UserInfo = userInfo;
        }
    }
}
