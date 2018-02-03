namespace TaskAssignment.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttendanceType")]
    public partial class AttendanceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendanceType()
        {
            Attendances = new HashSet<Attendance>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(10)]
        public string TypeName { get; set; }

        public bool IsAbcense { get; set; }

        [StringLength(15)]
        public string Alias { get; set; }

        [StringLength(4)]
        public string Symbol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
