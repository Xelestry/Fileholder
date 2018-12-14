namespace Fileholder.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=FileholderDB")
        {
        }

        public virtual DbSet<AllFiles> AllFiles { get; set; }
        public virtual DbSet<AllFilesAndGroupFilesLink> AllFilesAndGroupFilesLink { get; set; }
        public virtual DbSet<GroupFileOfOneUpload> GroupFilesOfOneUpload { get; set; }
        public virtual DbSet<FileConfigs> FileConfigs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupFileOfOneUpload>()
                .HasMany(e => e.FileConfigs)
                .WithRequired(e => e.GroupFileOfOneUpload)
                .HasForeignKey(e => e.GroupFileId);
        }
    }
}
