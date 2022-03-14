

using Newtonsoft.Json;
using System;

namespace Dignite.Wechat.Mp.MiniProgram
{
    /// <summary>
    /// 通过code获取到的微信用户信息，这是前端提交过来的原始数据
    /// </summary>
    [Serializable]
    public class MiniProgramUserInfo
    {
        /// <summary>
        /// 用戶UnionID
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId
        {
            get; set;
        }

        /// <summary>
        /// 用戶OpenID
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像图片的 URL。
        /// URL 最后一个数值代表正方形头像大小（有 0、46、64、96、132 数值可选，0 代表 640x640 的正方形头像，46 表示 46x46 的正方形头像，剩余数值以此类推。默认132），用户没有头像时该项为空。
        /// 若用户更换头像，原有头像 URL 将失效。
        /// </summary>
        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        [JsonProperty("gender")]
        public int Gender { get; set; }

        /// <summary>
        /// 用户所在国家
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 显示 country，province，city 所用的语言；
        /// 值为：en\zh_CN\zh_TW
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
