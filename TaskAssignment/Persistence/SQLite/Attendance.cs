namespace TaskAssignment.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendance")]
    public partial class Attendance
    {
        public long Id { get; set; }

        public long MemberId { get; set; }

        public long? TaskId { get; set; }

        public long TypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual Task Task { get; set; }

        public virtual AttendanceType AttendanceType { get; set; }

        public virtual Member Member { get; set; }
    }
}
