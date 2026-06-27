namespace CustomerAPI.Model
{
    public class CustomerLogin
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string EncryptedPwd { get; set; }
        public string EncryptionKey { get; set; }
        public string EncryptionIV { get; set; }
    }
}
