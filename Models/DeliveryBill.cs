using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace QuanLyKhoBiaNGK.Models
{
    public class DeliveryBill
    {

        public int Id { get; set; }

        [Display(Name = "Ngày")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Ghi chú")]
        public string Note { get; set; }
        [Display(Name = "Khách Hàng")]

        public int CustomerId { get; set; }

       
        [Display(Name = "Tổng Tiền Sau Thuế")]
        public int Total { get; set; } = 0;


        //true:si~ : false:le?
        [Display(Name = "Loại Gía")]
        public bool PriceType { get; set; }

        [Display(Name = "Tiền Sản Phẩm")]
        public int SubTotal { get; set; } = 0;
        [Display(Name = "Khách Hàng")]
        public Customer? Customer { get; set; }
        public List<DeliveryBillItem>? DeliveryBillItem { get; set; }

    }
}
