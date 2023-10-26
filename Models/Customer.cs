using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class Customer
    {

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName{ get; set; }
        [StringLength(50)]
        public string LastName{ get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Birth { get; set; }
        public Nullable<bool> Gender { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        //public virtual ICollection<tb_PHIEUBANHANG> tb_PHIEUBANHANG { get; set; }
    }
}
