namespace Poshta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CASTOMER")]
    public partial class CASTOMER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CASTOMER()
        {
            NAKLADNA = new HashSet<NAKLADNA>();
            NAKLADNA1 = new HashSet<NAKLADNA>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "Телефон")]
        public string phone { get; set; }

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

        [StringLength(35)]
        [Display(Name = "Почта")]
        public string mail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NAKLADNA> NAKLADNA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NAKLADNA> NAKLADNA1 { get; set; }
        public override bool Equals(object obj)
        {
            return this.phone == (obj as CASTOMER).phone && this.last_name == (obj as CASTOMER).last_name && this.first_name == (obj as CASTOMER).first_name && this.surname == (obj as CASTOMER).surname && this.mail == (obj as CASTOMER).mail;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
