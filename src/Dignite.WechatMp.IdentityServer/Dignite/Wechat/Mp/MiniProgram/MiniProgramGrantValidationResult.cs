using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Dignite.Wechat.Mp.MiniProgram
{
    public class MiniProgramGrantValidationResult: WechatResult
    {
        public string UserId { get; set; }

        public List<Claim> AddedClaims { get; set; }
    }
}
