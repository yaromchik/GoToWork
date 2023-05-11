using GoToWork_Models.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GoToWork_Models.Models
{
    public class Post
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Короткий вопрос")]
        public string ShortQuestion { get; set; }
        [DisplayName("Вопрос")]
        public string Question { get; set; }
        [DisplayName("Изображение")]
        public string Image { get; set; }
        [Required]
        [DisplayName("Ответ")]
        public string Answer { get; set; }
        public DateTime Date { get; set; }
        public bool IsVerified { get; set; }
        public string AuthorName { get; set; }
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
    }

}










