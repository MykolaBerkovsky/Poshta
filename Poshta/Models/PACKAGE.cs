namespace Poshta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PACKAGE")]
    public partial class PACKAGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PACKAGE()
        {
            NAKLADNA = new HashSet<NAKLADNA>();
            SectionPackage = new HashSet<SectionPackage>();
        }

        [Key]
        public int id_package { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Назва")]
        public string package_info { get; set; }
        [Display(Name = "Вартість(UAH)")]
        [Range(0, 2000, ErrorMessage = "Від'ємне значення або перевищує допустиме значення")]
        public int cost { get; set; }
        [Display(Name = "Ширина")]
        [Range(0, 2000, ErrorMessage = "Від'ємне значення або перевищує допустиме значення")]
        public int p_width { get; set; }
        [Display(Name = "Довжина")]
        [Range(0, 2000, ErrorMessage = "Від'ємне значення або перевищує допустиме значення")]
        public int p_length { get; set; }
        [Display(Name = "Висота")]
        [Range(0, 2000, ErrorMessage = "Від'ємне значення або перевищує допустиме значення")]
        public int p_height { get; set; }
        public int id_stan_p { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NAKLADNA> NAKLADNA { get; set; }

        public virtual StanP StanP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SectionPackage> SectionPackage { get; set; }
    }
}
