using System.ComponentModel.DataAnnotations;
using QuanLyKhoBiaNGK.Models;

namespace QuanLyKhoBiaNGK.ViewModels
{
    public class CreateReceivedBillVM
    {
        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

        [Display(Name = "Nhà cung cấp")]
        public int SupplierId { get; set; }
        [Display(Name = "Tổng tiền")]
        public int Total { get; set; }
        public virtual ICollection<DetailReceived>? DetailReceiveds { get; set; }
        public virtual Supplier Supplier { get; set; }


    }
}
