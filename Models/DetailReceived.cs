using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoBiaNGK.Models
{
    public class DetailReceived
    {
        [Key]
        public int Id { get; set; }
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public string Unit { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        //thanh tien
        public int Amount { get; set; }

        public virtual Product Product { get; set; }
        public virtual ReceivedBill ReceivedBill { get; set; }
    }
}
