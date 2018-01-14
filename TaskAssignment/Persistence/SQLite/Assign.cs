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

    [Table("Assign")]
    public partial class Assign
    {
        public long Id { get; set; }

        public long TaskId { get; set; }

        public long MemberId { get; set; }

        public virtual Member Member { get; set; }

        public virtual Task Task { get; set; }
    }
}
#endif