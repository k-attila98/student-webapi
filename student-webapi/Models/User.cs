﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_webapi.Models
{
    public class User: IdentityUser
    {
        public string Password { get; set; }
    }
}
