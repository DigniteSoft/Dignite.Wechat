

using Newtonsoft.Json;

namespace Dignite.Wechat.Mp.MiniProgram
{
    /// <summary>
    /// 
    /// </summary>
    public class MiniProgramSessionResult : WechatResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("OpenId")]
        public string OpenId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
