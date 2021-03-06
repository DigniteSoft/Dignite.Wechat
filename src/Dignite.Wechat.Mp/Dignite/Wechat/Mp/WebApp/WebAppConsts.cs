
namespace Dignite.Wechat.Mp.WebApp
{
    public class WebAppConsts
    {
        /// <summary>
        /// 用于外部登陆时的LoginProvider名称
        /// </summary>
        public const string AuthenticationScheme = "wechat_webapp";


        /// <summary>
        /// 获取微信网页授权地址
        /// </summary>
        public const string AuthorizationPath = "/wechat/mp/webapp/authorization";

        /// <summary>
        /// 登陆地址
        /// </summary>
        public const string SignInPath = "/wechat/mp/webapp/signin";

        /// <summary>
        /// 获取jsapi加密签名的地址
        /// </summary>
        public const string JsapiSignaturePath = "/api/wechat/mp/webapp/get-jsapi-signature";
    }
}
