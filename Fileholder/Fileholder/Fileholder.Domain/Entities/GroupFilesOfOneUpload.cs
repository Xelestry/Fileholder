namespace Fileholder.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("group_file_of_one_upload")]
    public partial class GroupFileOfOneUpload
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupFileOfOneUpload()
        {
            AllFilesAndGroupFilesLink = new HashSet<AllFilesAndGroupFilesLink>();
            FileConfigs = new HashSet<FileConfigs>();
        }

        [Key]
        [Column("group_file_id")]
        public int GroupFileId { get; set; }

        [Required]
        [StringLength(256)]
        [Column("user_name")]
        public string UserName { get; set; }

        [Column("file_upload_date")]
        public DateTime FileUploadDate { get; set; }

        [Required]
        [StringLength(20)]
        [Column("group_file_guid")]      
        public string GroupFileGuid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllFilesAndGroupFilesLink> AllFilesAndGroupFilesLink { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileConfigs> FileConfigs { get; set; }
    }
}
