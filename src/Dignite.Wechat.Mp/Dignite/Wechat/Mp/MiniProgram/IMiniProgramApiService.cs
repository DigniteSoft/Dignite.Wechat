using Dignite.Wechat.Mp.Basic;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Wechat.Mp.MiniProgram
{
    public interface IMiniProgramApiService : ITransientDependency
    {
        /// <summary>
        /// 获取微信小程序的全局AccessToken
        /// </summary>
        /// <returns></returns>
        Task<AccessTokenResult> GetAccessTokenAsync();

        /// <summary>
        /// 刷新微信小程序的全局AccessToken
        /// </summary>
        /// <returns></returns>
        Task<AccessTokenResult> RefreshAccessTokenAsync();

        /// <summary>
        /// 获取小程序的Session
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<MiniProgramSessionResult> GetSessionTokenAsync(string code);

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="template_id"></param>
        /// <param name="data"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<WechatResult> SendTemplateMessageAsync(string touser, string template_id, IDictionary<string, MiniProgramTemplateMessageData> data, string page = null);

        /// <summary>
        /// 内容安全检测
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<WechatResult> MessageSecurityCheckAsync(string content);

        /// <summary>
        /// 获取小程序页面的二维码
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<QrCodeResult> GetQrCode(string path);
    }
}
