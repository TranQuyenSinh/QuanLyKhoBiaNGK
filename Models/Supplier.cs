using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [DisplayName("Họ tên")]
        public string FullName { get; set; }
        [StringLength(100)]
        [DisplayName("Địa Chỉ")]
        public string Address { get; set; }
        [StringLength(20)]
        [DisplayName("Số Điện Thoại")]
        public string Phone { get; set; }
        [StringLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }

        //public virtual ICollection<tb_PHIEUNHAPHANG> tb_PHIEUNHAPHANG { get; set; }
    }
}
