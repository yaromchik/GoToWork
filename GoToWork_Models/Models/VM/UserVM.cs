using GoToWork_Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoToWork_Models.Models.VM
{
    public class UserVM
    {
        public IdentityUser Users { get; set; }
        public string RoleName { get; set; }
    }
}
