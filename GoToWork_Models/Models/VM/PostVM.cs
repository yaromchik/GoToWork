using GoToWork_Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoToWork_Models.Models.VM
{
    public class IndexVM
    {
        public Post Postic { get; set; }
        public IEnumerable<SelectListItem> LanguageSelectList { get; set; }
    }
}
