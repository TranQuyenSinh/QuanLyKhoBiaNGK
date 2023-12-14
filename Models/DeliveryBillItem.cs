namespace QuanLyKhoBiaNGK.Models
{
    public class DeliveryBillItem
    {
        public int Id { get; set; }

        public int DeliveryBillId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }   
        public DeliveryBill DeliveryBill { get; set; }
        public Product Product { get; set; }
    }
}