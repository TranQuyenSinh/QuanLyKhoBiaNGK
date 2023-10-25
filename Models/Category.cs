
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name{ get; set; }
        public virtual ICollection<Product> tb_HANGHOA { get; set; }
    }
}
