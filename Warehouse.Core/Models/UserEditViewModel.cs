using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Contracts;

namespace Warehouse.Core.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "ProfilePicture")]
        public byte[]? ProfilePicture { get; set; }


    }
}
