
using Dignite.Wechat.Mp.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Wechat.Mp.IdentityServer.Settings
{
    public class WechatMpSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            var definitions = new SettingDefinition[] { 
                new SettingDefinition(
                    name:IdentityServerSettings.ClientId,
                    displayName:L("DigniteWechatMpIdentityServerSettings.ClientId"),
                    isVisibleToClients:false,
                    isEncrypted:false).WithProviders("C"),

                new SettingDefinition(
                    name:IdentityServerSettings.ClientSecret,
                    displayName:L("DigniteWechatMpIdentityServerSettings.ClientSecret"),
                    isVisibleToClients:false,
                    isEncrypted:false).WithProviders("C")
            };

            context.Add(definitions);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DigniteWechatMpIdentityServerResource>(name);
        }
    }
}