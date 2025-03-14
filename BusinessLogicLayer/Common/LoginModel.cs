﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Common
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required !")]
        [StringLength(50, MinimumLength = 2)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required !")]
        [StringLength(30, MinimumLength = 7)]
        public string Password { get; set; }
    }
}
