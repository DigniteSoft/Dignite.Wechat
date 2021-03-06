

using Newtonsoft.Json;

namespace Dignite.Wechat.Mp.Basic
{
    public class GrantValidationResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
