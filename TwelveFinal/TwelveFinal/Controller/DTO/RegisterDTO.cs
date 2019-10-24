﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class RegisterDTO : DataDTO
    {
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
