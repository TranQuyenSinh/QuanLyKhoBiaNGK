using QuanLyKhoBiaNGK.Models;

namespace QuanLyKhoBiaNGK.ViewModels
{
    public class CreateReceivedBillVM
    {
        public string Note { get; set; }
        public int SupplierId { get; set; }
        public int Total { get; set; }
        public virtual ICollection<DetailReceived>? DetailReceiveds { get; set; }
        public virtual Supplier Supplier { get; set; }

       
    }
}
