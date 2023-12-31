﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Infrastructure.Data.Identity
{
    public class ApplicationUser: IdentityUser
    {
        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }

        public byte[]? ProfilePicture { get; set; }
    }
}
