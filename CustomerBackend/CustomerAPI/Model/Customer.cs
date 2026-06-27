namespace CustomerAPI.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string EncryptedPwd { get; set; }
        public string EncryptionKey { get; set; }
        public string EncryptionIV { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
