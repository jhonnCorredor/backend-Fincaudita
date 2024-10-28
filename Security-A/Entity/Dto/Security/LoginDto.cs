﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto.Security
{
    public class LoginDto
    {
        public int userID { get; set; }
        public string password { get; set; }
        public int? roleID { get; set; }
        public string? role { get; set; }
        public string? ListView {  get; set; }

    }
}
