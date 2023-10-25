﻿using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoBiaNGK.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }

        //public virtual ICollection<tb_PHIEUNHAPHANG> tb_PHIEUNHAPHANG { get; set; }
    }
}
