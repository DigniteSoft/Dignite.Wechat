using Dignite.Wechat.Mp.IdentityServer.Localization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Wechat.Mp.IdentityServer
{
    [DependsOn(
        typeof(DigniteWechatMpModule)
    )]
    public class DigniteWechatMpIdentityServerModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            //配置微信webapp授权登陆
            PreConfigure<IIdentityServerBuilder>(builder =>
            {
                builder.AddExtensionGrantValidator<WebAppGrantValidator>();
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteWechatMpIdentityServerModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DigniteWechatMpIdentityServerResource>("en")
                    .AddVirtualJson("/Dignite/Wechat/Mp/IdentityServer/Localization/Resources");
            });


            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Wechat.Mp.IdentityServer", typeof(DigniteWechatMpIdentityServerResource));
            });

        }
    }
}