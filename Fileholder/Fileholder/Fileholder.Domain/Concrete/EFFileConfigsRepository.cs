using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fileholder.Domain.Abstract;
using System.Data.SqlClient;
using Fileholder;

namespace Fileholder.Domain.Concrete
{
    public class EFFileConfigsRepository : Test, IFileConfigsRepository
    {
        public void AddConfig(FileConfigs fileConfigs)
        {
            context.FileConfigs.Add(fileConfigs);
            context.SaveChanges();
        }

        public List<FileConfigs> GetAllFilesConfigs()
        {
            List<FileConfigs> fileConfigs = new List<FileConfigs>();

            var dates = context.Database.SqlQuery<FileConfigs>("" +
                "SELECT date_of_deletion, group_file_id, " +
                "password, description, date_of_deletion, download_counter " +
                "FROM file_configs");

            foreach (var item in dates)
            {
                fileConfigs.Add(item);
            }

            return fileConfigs;
        }

        public bool CheckDeleteDate(string fileGuid)
        {
            var dates = context.Database.SqlQuery<DateTime>("SELECT date_of_deletion " +
                "FROM file_configs fc " +
                "JOIN group_file_of_one_upload gfou " +
                "ON fc.group_file_id = gfou.group_file_id " +
                "WHERE gfou.group_file_guid = {0}", fileGuid);

            foreach (var item in dates)
            {
                if (DateTime.Now > item)
                {
                    return true;
                }
            }

            return false;
        }

        public void DeleteGroupFiles(string fileGuid)
        {
            context.Database.ExecuteSqlCommand("dbo.delete_group_files {0}", fileGuid);
        }

        public void IncreaseDownloadCount(string fileGuid)
        {
            context.Database.ExecuteSqlCommand("increase_download_count {0}", fileGuid);
        }

        public FileConfigs GetConfigsByFileGuid(string fileGuid)
        {
            var dates = context.Database.SqlQuery<FileConfigs>("SELECT fc.file_configs_id as FileConfigsId, " +
                "fc.group_file_id as GroupFileId, " +
                "fc.password as Password, fc.description as Description, " +
                "fc.date_of_deletion as DateOfDeletion, " +
                "fc.download_counter as DownloadCounter " +
                "FROM file_configs fc " +
                "JOIN group_file_of_one_upload gfou " +
                "ON fc.group_file_id = gfou.group_file_id " +
                "WHERE gfou.group_file_guid = {0}", fileGuid);

            FileConfigs fileConfigs = new FileConfigs();

            foreach (var item in dates)
            {
                fileConfigs = item;
            }

            return fileConfigs;
        }
    }
}
