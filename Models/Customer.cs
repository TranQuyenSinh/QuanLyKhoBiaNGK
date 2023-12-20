using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [DisplayName("Họ và Tên")]
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Ngày Sinh")]
        public Nullable<System.DateTime> Birth { get; set; }
        [DisplayName("Giới Tính")]
        public Nullable<bool> Gender { get; set; }
        [StringLength(100)]
        [DisplayName("Địa Chỉ")]
        public string? Address { get; set; }
        [StringLength(20)]
        [DisplayName("Số Điện Thoại")]
        public string? Phone { get; set; }
        [StringLength(50)]
        [DisplayName("Email")]

        public string? Email { get; set; }
        //public virtual ICollection<tb_PHIEUBANHANG> tb_PHIEUBANHANG { get; set; }
        public virtual ICollection<DeliveryBill>? Bills { get; set; }
    }
}
