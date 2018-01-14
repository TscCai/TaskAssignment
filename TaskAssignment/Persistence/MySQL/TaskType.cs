// Code may be different between EF6-SQLite and EF6-MySQL
// Use compiler directive to control different dialect
#if MYSQL
namespace TaskAssignment.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("taskassignment.tasktype")]
    public partial class TaskType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskType()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string TypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
#endif