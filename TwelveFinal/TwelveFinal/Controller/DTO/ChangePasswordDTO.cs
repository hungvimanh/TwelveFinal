﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class ChangePasswordDTO
    {
        public string Identify { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
