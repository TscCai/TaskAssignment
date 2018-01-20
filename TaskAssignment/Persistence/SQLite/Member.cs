// Code may be different between EF6-SQLite and EF6-MySQL
// Use compiler directive to control different dialect
#if SQLITE
namespace TaskAssignment.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Assigns = new HashSet<Assign>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public bool Enable { get; set; }

        public bool IsInternal { get; set; }

        public bool Countable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assign> Assigns { get; set; }
    }
}
#endif