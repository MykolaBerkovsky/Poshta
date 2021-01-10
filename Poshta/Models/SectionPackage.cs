namespace Poshta.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SectionPackage")]
    public partial class SectionPackage
    {
        [Key]
        public int id_sp { get; set; }
        public int id_section { get; set; }

        public int id_package { get; set; }

        public int count { get; set; }

        public virtual PACKAGE PACKAGE { get; set; }

        public virtual SECTION SECTION { get; set; }
    }
}
