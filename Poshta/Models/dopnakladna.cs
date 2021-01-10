using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poshta.Models
{
    public class dopnakladna
    {
        [Display(Name = "Номер")]
        public int n_nakladna { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Телефон")]
        public string phone_v { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Прізвище")]
        public string v_l { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Ім'я")]
        public string v_f { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "По батькові")]
        public string v_s { get; set; }
        [StringLength(35)]
        [Display(Name = "Почта")]
        public string v_mail { get; set; }
        public int contact_number_v { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Телефон")]
        public string phone_g { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Прізвище")]
        public string g_l { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Ім'я")]
        public string g_f { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "По батькові")]
        public string g_s { get; set; }
        [StringLength(35)]
        [Display(Name = "Почта")]
        public string g_mail { get; set; }
        public int id_region { get; set; }
        public string town { get; set; }
        public int nom { get; set; }
        [Display(Name = "Пакування")]
        public int id_package { get; set; }
        [Display(Name = "Дата")]
        public DateTime date { get; set; }

        public int id_transort { get; set; }
        [Display(Name = "Вага")]
        public double weight { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Опис")]
        public string descriotion { get; set; }

        [StringLength(30)]
        [Display(Name = "Додаткова інформація")]
        public string dop_info { get; set; }
        [Display(Name = "Вартість")]
        public int cost { get; set; }
        [Display(Name = "Оплата")]
        public Boolean pay { get; set; }
    }
}