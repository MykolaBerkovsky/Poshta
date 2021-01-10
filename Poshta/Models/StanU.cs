namespace Poshta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StanU")]
    public partial class StanU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StanU()
        {
            USER = new HashSet<USER>();
        }

        [Key]
        public int id_stan_u { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Стан")]
        public string stan_u { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER> USER { get; set; }
    }
}
