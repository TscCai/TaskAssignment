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

    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            Assigns = new HashSet<Assign>();
        }

        public long Id { get; set; }

        [StringLength(10)]
        public string Location { get; set; }

        [Required]
        [StringLength(255)]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public long ConditionId { get; set; }

        public long TypeId { get; set; }

        public bool Visible { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assign> Assigns { get; set; }

        public virtual TaskCondition TaskCondition { get; set; }

        public virtual TaskType TaskType { get; set; }
    }
}
#endif