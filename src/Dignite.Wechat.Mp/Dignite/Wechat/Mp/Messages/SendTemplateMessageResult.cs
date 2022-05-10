
namespace Dignite.Wechat.Mp.Messages
{
    /// <summary>
    /// 发送模板消息后的结果
    /// </summary>
    public class SendTemplateMessageResult:WechatResult
    {
        /// <summary>
        /// msgid
        /// </summary>
        public long msgid { get; set; }
    }
}
