using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class ReceivedBill
    {
        public int Id { get; set; }
        [Display(Name = "Ngày giao")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        //public int UserId { get; set; }
        [Display(Name = "Ghi chú")]
        public string Note { get; set; }
        [Display(Name = "Nhà cung cấp")]
        public int SupplierId { get; set; }
        [Display(Name = "Tổng tiền")]
        public int Total { get; set; }
        public virtual ICollection<DetailReceived> DetailReceiveds { get; set; }
        public virtual Supplier Supplier { get; set; }
        //public virtual tb_USER tb_USER { get; set; }
    }
}
