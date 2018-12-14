//TODO: добавить конфиги в проект, если будет время
namespace Fileholder.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("file_configs")]
    public partial class FileConfigs
    {
        [Key]
        [Column("file_configs_id")]
        public int FileConfigsId { get; set; }

        [Column("group_file_id")]
        public int GroupFileId { get; set; }

        [StringLength(20)]
        [Column("password")]
        public string Password { get; set; }

        [StringLength(1000)]
        [Column("description")]
        public string Description { get; set; }

        [Column("date_of_deletion")]
        public DateTime DateOfDeletion { get; set; }

        [Column("download_counter")]
        public int? DownloadCounter { get; set; }

        [Column("group_file_of_one_upload")]
        public virtual GroupFileOfOneUpload GroupFileOfOneUpload { get; set; }
    }
}
