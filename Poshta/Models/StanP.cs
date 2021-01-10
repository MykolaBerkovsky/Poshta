namespace Poshta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StanP")]
    public partial class StanP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StanP()
        {
            PACKAGE = new HashSet<PACKAGE>();
        }

        [Key]
        public int id_stan_p { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Стан")]
        public string stan_p { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACKAGE> PACKAGE { get; set; }
    }
}
