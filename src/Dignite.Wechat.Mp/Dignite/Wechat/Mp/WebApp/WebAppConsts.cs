
namespace Dignite.Wechat.Mp.WebApp
{
    public class WebAppConsts
    {
        /// <summary>
        /// 用于IdentityServe4的认证授权类型名称
        /// </summary>
        public const string GrantType = "wechat_officaccount";

        /// <summary>
        /// 用于外部登陆时的LoginProvider名称
        /// </summary>
        public const string AuthenticationScheme = "wechat_officaccount";

        /// <summary>
        /// 获取jsapi加密签名的地址
        /// </summary>
        public const string JsapiSignaturePath = "/wechat/get-jsapi-signature";
    }
}
