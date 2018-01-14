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

    [Table("taskassignment.assign")]
    public partial class Assign
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int MemberId { get; set; }

        public virtual Member Member { get; set; }

        public virtual Task Task { get; set; }
    }
}
#endif