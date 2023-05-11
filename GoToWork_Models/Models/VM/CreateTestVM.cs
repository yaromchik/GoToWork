using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWork_Models.Models.VM
{
    public class CreateTestVM
    {
        public IEnumerable<SelectListItem> LanguageSelectList { get; set; }
        public MyTest Testik { get; set; }
    }
}
