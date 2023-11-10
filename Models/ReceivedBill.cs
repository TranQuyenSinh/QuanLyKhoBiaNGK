namespace QuanLyKhoBiaNGK.Models
{
    public class ReceivedBill
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        //public int UserId { get; set; }
        public string Note { get; set; }
        public int SupplierId { get; set; }
        public int Total { get; set; }

        
        public virtual ICollection<DetailReceived> DetailReceiveds { get; set; }
        public virtual Supplier Supplier { get; set; }
        //public virtual tb_USER tb_USER { get; set; }
    }
}
