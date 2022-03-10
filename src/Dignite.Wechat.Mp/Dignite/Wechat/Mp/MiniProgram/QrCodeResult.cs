namespace Dignite.Wechat.Mp.MiniProgram
{
    public class QrCodeResult : WechatResult
    {
        public string ContentType { get; set; }

        public byte[] Buffer { get; set; }
    }
}
