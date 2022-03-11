
using Microsoft.AspNetCore.Http;

namespace Dignite.Wechat.Mp.WebApp
{
    /// <summary>
    /// 授权验证的上下文信息
    /// </summary>
    public class WebAppGrantValidationContext
    {
        public HttpContext HttpContext { get; }

        public AuthencationAccessTokenResult AccessTokenResult { get; }

        public AuthencationUserInfo WechatUser { get; }

        public WebAppGrantValidationContext(HttpContext httpContext,  AuthencationAccessTokenResult accessTokenResult,AuthencationUserInfo wechatUser=null)
        {
            HttpContext = httpContext;
            WechatUser = wechatUser;
            AccessTokenResult = accessTokenResult;
        }
    }
}
