

namespace Dignite.Wechat.Mp.Messages
{
    public class TemplateMessageMiniProgram
    {
        /// <summary>
        /// 小程序AppId（必填）
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 路径，如：index?foo=bar
        /// </summary>
        public string pagepath { get; set; }
    }
}
