using System.Collections.Generic;
using System.Security.Claims;

namespace Dignite.Wechat.Mp.IdentityServer
{
    public class WebAppGrantValidationResult : WechatResult
    {
        public string UserId { get; set; }

        public List<Claim> AddedClaims { get; set; }
    }
}
