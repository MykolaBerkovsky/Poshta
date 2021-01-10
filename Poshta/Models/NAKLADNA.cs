namespace Poshta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NAKLADNA")]
    public partial class NAKLADNA
    {
        [Key]
        [Display(Name = "�����")]
        public int n_nakladna { get; set; }

        [Required]
        [StringLength(10)]
        public string phone_v { get; set; }

        public int contact_number_v { get; set; }

        [Required]
        [StringLength(10)]
        public string phone_g { get; set; }

        public int id_section_g { get; set; }

        public int id_package { get; set; }
        [Display(Name = "����")]
        public DateTime date { get; set; }

        public int id_transort { get; set; }
        [Display(Name = "����")]
        public decimal weight { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "����")]
        public string descriotion { get; set; }

        [StringLength(30)]
        [Display(Name = "��������� ����������")]
        public string dop_info { get; set; }
        [Display(Name = "�������")]
        public int cost { get; set; }
        [Display(Name = "������")]
        public Boolean pay { get; set; }

        public virtual CASTOMER CASTOMER { get; set; }

        public virtual CASTOMER CASTOMER1 { get; set; }

        public virtual PACKAGE PACKAGE { get; set; }

        public virtual SECTION SECTION { get; set; }

        public virtual StanTransp StanTransp { get; set; }

        public virtual USER USER { get; set; }
    }
}
