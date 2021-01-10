namespace Poshta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USER")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            NAKLADNA = new HashSet<NAKLADNA>();
        }

        [Key]
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

        public int id_section { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NAKLADNA> NAKLADNA { get; set; }

        public virtual ROLES ROLES { get; set; }

        public virtual SECTION SECTION { get; set; }

        public virtual StanU StanU { get; set; }
    }
}
