using System.ComponentModel.DataAnnotations;
using QuanLyKhoBiaNGK.Models;
namespace QuanLyKhoBiaNGK.ViewModels;

public class ResetPasswordVM
{
    [Required(ErrorMessage = "{0} không được bỏ trống")]
    [StringLength(50, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu mới")]
    public string NewPassword { get; set; }
}
