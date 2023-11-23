using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using QuanLyKhoBiaNGK.Models;

namespace QuanLyKhoBiaNGK.ViewModels;
public class UserWithRoles : User
{
    public string RoleNames { get; set; }
    public List<string>? Roles { get; set; }
}
