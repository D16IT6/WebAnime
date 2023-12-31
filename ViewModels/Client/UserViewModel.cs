﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Client
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [StringLength(32, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string UserName { get; set; }


        [StringLength(50, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Tên đầy đủ")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public DateTime BirthDay { get; set; }

        [StringLength(50, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Địa chỉ email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} phải là email")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "{0} phải là số điện thoại")]
        [Required(ErrorMessage = "{0} là bắt buộc")]

        public string PhoneNumber { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [RegularExpression("^(https?://(localhost|[\\w\\-]+(\\.[\\w\\-]+)+)(:\\d+)?(/[\\w\\-]+)+|/[\\w\\-]+)+\\.[a-zA-Z]{2,4}$", ErrorMessage = "{0} không hợp lệ, chỉ cho phép url hoặc đường dẫn ảnh trực tiếp")]
        [MaxLength(200, ErrorMessage = "{0} dài tối đa {1} ký tự")]
        public string AvatarUrl { get; set; }
    }

}
