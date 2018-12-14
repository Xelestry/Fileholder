namespace Fileholder.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("all_files_and_group_files_link")]
    public partial class AllFilesAndGroupFilesLink
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("file_id")]
        public int FileId { get; set; }

        [Column("group_file_id")]
        public int GroupFileId { get; set; }

        [Column("all_files")]
        public virtual AllFiles AllFiles { get; set; }

        [Column("group_file_of_one_upload")]
        public virtual GroupFileOfOneUpload GroupFileOfOneUpload { get; set; }
    }
}
