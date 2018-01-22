namespace TaskAssignment.Persistence
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TaskAssignmentModel : DbContext
    {
        public TaskAssignmentModel()
            : base("name=TaskAssignmentModel") {
        }

        public virtual DbSet<Assign> Assigns { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Substation> Substations { get; set; }
        public virtual DbSet<TaskCondition> TaskConditions { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskType> TaskTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Location>()
                .HasMany(e => e.Substations)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Assigns)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Substation>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Substation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskCondition>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.TaskCondition)
                .HasForeignKey(e => e.ConditionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Assigns)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskType>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.TaskType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
