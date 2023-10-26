using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [DisplayName("Tên sản phẩm")]
        public string Name{ get; set; }
        [StringLength(100)]
        [DisplayName("Mô tả")]
        public string Description{ get; set; }
        [DisplayName("Định mức tồn")]
        public int InventoryLevel { get; set; }
        [DisplayName("Tồn kho")]
        public int Inventory { get; set; }
        [DisplayName("Loại sản phẩm")]
        public int CategoryId{ get; set; }

        [DisplayName("Loại sản phẩm")]
        public Category? Category { get; set;}

        public virtual ICollection<Price>? Prices { get; set; }
    }
}
