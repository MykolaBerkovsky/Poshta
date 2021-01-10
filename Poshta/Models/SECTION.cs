namespace Poshta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SECTION")]
    public partial class SECTION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SECTION()
        {
            NAKLADNA = new HashSet<NAKLADNA>();
            SectionPackage = new HashSet<SectionPackage>();
            USER = new HashSet<USER>();
        }

        [Key]
        public int id_section { get; set; }

        public int id_region { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Місто")]
        public string town { get; set; }
        [Display(Name = "Номер відділення")]
        [Range(0, 2000, ErrorMessage = "Від'ємне значення або перевищує допустиме значення")]
        public int n_section { get; set; }
        [Display(Name = "Стан")]
        public int id_stan_s { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NAKLADNA> NAKLADNA { get; set; }

        public virtual REGIONCollection REGIONCollection { get; set; }

        public virtual StanS StanS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SectionPackage> SectionPackage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER> USER { get; set; }
    }
}
