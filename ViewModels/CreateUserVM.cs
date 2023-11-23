// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKhoBiaNGK.ViewModels
{
    public class CreateUserVM
    {
        [Required(ErrorMessage = "Phải nhập {0}")]
        [DataType(DataType.Text)]
        [Display(Name = "Tên đăng nhập")]
        [StringLength(50, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 4)]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Phải nhập {0}")]
        [DataType(DataType.Text)]
        [Display(Name = "Họ và tên")]
        [StringLength(50, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 4)]
        public string FullName { get; set; }



        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(50, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Phân quyền")]
        public List<string>? Roles { get; set; } = new List<string>();
    }
}
