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
        public string Name { get; set; }
        [StringLength(100)]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Định mức tồn")]
        public int InventoryLevel { get; set; }
        [DisplayName("Tồn kho")]
        public int Inventory { get; set; }
        [DisplayName("Loại sản phẩm")]
        public int CategoryId { get; set; }
        [DisplayName("Loại sản phẩm")]
        public Category? Category { get; set; }
        [StringLength(20)]
        [DisplayName("Đơn vị tính")]
        public string Unit { get; set; }
        [DisplayName("Giá nhập")]
        public int PurchasePrice { get; set; }
        [DisplayName("Giá bán lẻ")]
        public int RetailPrice { get; set; }
        [DisplayName("Giá bán sỉ")]
        public int WholesalePrice { get; set; }
    }
}
