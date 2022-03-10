
using Dignite.Wechat.Mp.Basic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Wechat.Mp.WebApp
{
    /// <summary>
    /// 微信公众号登陆的发起者
    /// </summary>
    public interface IWebAppGrantValidationSender: ITransientDependency
    {
        Task<GrantValidationResult> ValidateAsync(string code,string state);
    }
}
