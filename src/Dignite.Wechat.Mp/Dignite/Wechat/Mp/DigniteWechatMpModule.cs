
using Dignite.Wechat.Mp.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Wechat.Mp
{
    public class DigniteWechatMpModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteWechatMpModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DigniteWechatMpResource>("en")
                    .AddVirtualJson("/Dignite/Wechat/Mp/Localization/Resources");
            });


            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Wechat.Mp", typeof(DigniteWechatMpResource));
            });

            //添加微信Mp配置服务
            context.Services.AddWechatMp();
        }

    }
}