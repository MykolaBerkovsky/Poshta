namespace Poshta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("REGIONCollection")]
    public partial class REGIONCollection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REGIONCollection()
        {
            SECTION = new HashSet<SECTION>();
        }

        [Key]
        public int id_region { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Область")]
        public string region { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SECTION> SECTION { get; set; }
    }
}
