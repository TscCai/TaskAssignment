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
            Attendances = new HashSet<Attendance>();
        }

        public long Id { get; set; }

        public long SubstationId { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public long TypeId { get; set; }

        public long ConditionId { get; set; }

        public bool Visible { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assign> Assigns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual Substation Substation { get; set; }

        public virtual TaskCondition TaskCondition { get; set; }

        public virtual TaskType TaskType { get; set; }
    }
}
