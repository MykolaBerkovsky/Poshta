namespace Poshta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StanTransp")]
    public partial class StanTransp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StanTransp()
        {
            NAKLADNA = new HashSet<NAKLADNA>();
        }

        [Key]
        public int id_transort { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Транспортний стан")]
        public string transport_info { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NAKLADNA> NAKLADNA { get; set; }
    }
}
