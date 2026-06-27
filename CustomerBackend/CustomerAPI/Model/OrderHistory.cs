namespace CustomerAPI.Model
{
    public class OrderHistory
    {
        public int OrderId { get; set; }
        public string ItemName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool NotificationSent { get; set; }
    }
}
