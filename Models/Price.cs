using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        [DisplayName("Đơn vị tính")]
        public string Unit { get; set; }
        [DisplayName("Quy đổi ")]
        public int ConversionRate { get; set; }
        [DisplayName("Giá nhập")]
        public int PurchasePrice { get; set; }
        [DisplayName("Giá bán lẻ")]
        public int RetailPrice { get; set; }
        [DisplayName("Giá bán sỉ")]
        public int WholesalePrice { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
