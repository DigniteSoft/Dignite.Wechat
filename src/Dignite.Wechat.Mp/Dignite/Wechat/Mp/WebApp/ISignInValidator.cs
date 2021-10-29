
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Wechat.Mp.WebApp
{
    public interface ISignInValidator: ITransientDependency
    {
        Task<SignInValidationResult> ValidateAsync(string code,string state);
    }
}
