
namespace Dignite.Wechat.Mp.Messages
{
    /// <summary>
    /// 模板消息的数据
    /// </summary>
    public class TemplateMessageData
    {
        /// <summary>
        /// 项目值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 16进制颜色代码，如：#FF0000
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// TemplateDataItem 构造函数
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="color">color</param>
        public TemplateMessageData(string value, string color = "#173177")
        {
            this.value = value;
            this.color = color;
        }
    }
}
