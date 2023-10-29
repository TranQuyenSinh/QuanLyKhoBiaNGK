using QuanLyKhoBiaNGK.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuanLyKhoBiaNGK.ViewModels
{
    public class CreateProductModel
    {
        [DisplayName("Tên sản phẩm")]
        [StringLength(100)]
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

        public List<Price> Prices { get; set; }
    }
}
