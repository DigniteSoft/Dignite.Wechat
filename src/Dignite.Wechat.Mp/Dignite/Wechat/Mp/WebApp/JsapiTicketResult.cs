

namespace Dignite.Wechat.Mp.WebApp
{
    public class JsapiTicketResult : WechatResult
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒。目前是7200秒之内的值。
        /// </summary>
        public int expires_in { get; set; }
    }
}
