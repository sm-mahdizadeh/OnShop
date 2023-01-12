namespace OnShop.Framework.Common
{
    public class FarazSmsSendModel
    {
        public string Uname { get; set; }
        public string Pass { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public string[] To { get; set; }
        public string Op { get; set; } = "send";
        public string Time { get; set; }
    }
}