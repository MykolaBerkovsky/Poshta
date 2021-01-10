using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poshta.Models
{
    public class sklad
    {
        public int id { get; set; }
        public string info { get; set; }
        [Display(Name = "Кількість")]
        [Range(0, 2000, ErrorMessage = "Від'ємне значення або перевищує допустиме значення")]
        public int count { get; set; }
    }
}