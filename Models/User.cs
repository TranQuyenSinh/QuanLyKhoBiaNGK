
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace QuanLyKhoBiaNGK.Models
{
    public class User : IdentityUser
    {
        [StringLength(100)]
        [DisplayName("Họ tên")]
        public string FullName { get; set; }
    }
}
