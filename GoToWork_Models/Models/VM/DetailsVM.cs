using GoToWork_Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoToWork_Models.Models.VM
{
    public class DetailsVM
    {
        public Post Postic { get; set; }
        public int Page { get; set; }
        public IEnumerable <Comment> CommentList { get; set; }
    }
}
