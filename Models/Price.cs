using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Unit{ get; set; }
        public int ConversionRate { get; set; }
        public int PurchasePrice { get; set; }
        public int RetailPrice { get; set; }
        public int WholesalePrice { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
