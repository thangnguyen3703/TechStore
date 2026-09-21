using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Shared_ViewModels;
    public class BranchFormViewModel
    {
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã chi nhánh.")]
        [StringLength(50)]
        [Display(Name = "Mã chi nhánh")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên chi nhánh.")]
        [StringLength(150)]
        [Display(Name = "Tên chi nhánh")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [StringLength(20)]
        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [Display(Name = "Hoạt động")]
        public bool IsActive { get; set; } = true;
    }