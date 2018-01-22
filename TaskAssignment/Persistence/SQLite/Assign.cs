namespace TaskAssignment.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assign")]
    public partial class Assign
    {
        public long Id { get; set; }

        public long TaskId { get; set; }

        public long MemberId { get; set; }

        public bool IsLeader { get; set; }

        public virtual Member Member { get; set; }

        public virtual Task Task { get; set; }
    }
}
