
using Dignite.Wechat.Mp.Basic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Wechat.Mp.MiniProgram
{
    /// <summary>
    /// 微信小程序在拿到code和user-info后，向授权中心发起登录
    /// </summary>
    public interface IMiniProgramGrantValidationSender: ITransientDependency
    {
        Task<GrantValidationResult> ValidateAsync(string code,string userInfo);
    }
}
