
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [DisplayName("Tên sản phẩm")]
        public string Name{ get; set; }
        
        public virtual ICollection<Product>? Products { get; set; }
    }
}
