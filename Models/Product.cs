using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name{ get; set; }
        [StringLength(100)]
        public string Description{ get; set; }
        public int InventoryLevel{ get; set; }
        public int Inventory { get; set; }
        public int CategoryId{ get; set; }
        public Category Category { get; set;}
    }
}
