namespace Fileholder.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("all_files")]
    public partial class AllFiles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AllFiles()
        {
            AllFilesAndGroupFilesLink = new HashSet<AllFilesAndGroupFilesLink>();
        }

        [Key]
        [Column("file_id")]
        public int FileId { get; set; }

        [Required]
        [StringLength(500)]
        [Column("file_name")]
        public string FileName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("file_extension")]
        public string FileExtension { get; set; }
        
        [Column("file_size")]
        public int FileSize { get; set; }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Column("all_files_and_group_files_link")]
        public virtual ICollection<AllFilesAndGroupFilesLink> AllFilesAndGroupFilesLink { get; set; }
    }
}
