using Newtonsoft.Json;
using System;

namespace Dignite.Wechat.Mp.WebApp
{

    /// <summary>
    /// OAuth AccessToken的结果
    /// </summary>
    [Serializable]
    public class AuthencationAccessTokenResult : WechatResult
    {
        /// <summary>
        /// 接口调用凭证
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        /// <summary>
        /// 授权用户唯一标识
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：获取用户个人信息（UnionID机制）
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }

        /// <summary>
        /// <see cref="AccessToken"/> 过期时间
        /// </summary>
        internal DateTime AccessTokenExpireTime { get; set; }


        /// <summary>
        /// <see cref="RefreshToken"/> 过期时间
        /// </summary>
        internal DateTime RefreshTokenExpireTime { get; set; }
    }
}
