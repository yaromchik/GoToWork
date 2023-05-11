using GoToWork_Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoToWork_Models.Models.VM
{
    public class PostIndexVM
    {
        public IEnumerable<Post> Postic { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }    
        public int Page { get; set; }
        public int CountPage { get; set; }
    }
}
