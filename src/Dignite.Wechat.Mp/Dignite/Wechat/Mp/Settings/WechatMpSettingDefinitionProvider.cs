
using Dignite.Wechat.Mp.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Wechat.Mp.Settings
{
    public class WechatMpSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            var definitions = new SettingDefinition[] { 
                new SettingDefinition(
                    name:WechatMpSettings.AppId,
                    displayName:L("DigniteWechatMpSettings.MpAppId"),
                    isVisibleToClients:false,
                    isEncrypted:false),

                new SettingDefinition(
                    name:WechatMpSettings.Secret,
                    displayName:L("DigniteWechatMpSettings.MpSecret"),
                    isVisibleToClients:false,
                    isEncrypted:false),
                new SettingDefinition(
                    name:WechatMpSettings.MiniProgramAppId,
                    displayName:L("WechatMpSettings.MiniProgramAppId"),
                    isVisibleToClients:false,
                    isEncrypted:false),

                new SettingDefinition(
                    name:WechatMpSettings.MiniProgramSecret,
                    displayName:L("WechatMpSettings.MiniProgramSecret"),
                    isVisibleToClients:false,
                    isEncrypted:false),

            };

            context.Add(definitions);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DigniteWechatMpResource>(name);
        }
    }
}