using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poshta.Models
{
    public class dopuser
    {
        public int contact_number { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Прізвище")]
        public string last_name { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Ім'я")]
        public string first_name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "По батькові")]
        public string surname { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Телефон")]
        public string phone { get; set; }

        [StringLength(35)]
        [Display(Name = "Почта")]
        public string mail { get; set; }
        public int id_region { get; set; }
        public string town { get; set; }
        public int nom { get; set; }
        public int id_role { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Логін")]
        public string login { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Пароль")]
        public string password { get; set; }
        public int stan_u { get; set; }
    }
}