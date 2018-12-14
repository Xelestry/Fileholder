using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fileholder.Domain.Abstract;
using System.Data.SqlClient;

namespace Fileholder.Domain.Concrete
{
    public class EFGroupFilesOfOneUploadRepository : Test, IGroupFilesOfOneUploadRepository
    {     
        public void AddGroup(GroupFileOfOneUpload groupFiles)
        {
            context.GroupFilesOfOneUpload.Add(groupFiles);
            context.SaveChanges();
        }

        public DateTime GetUploadDate(string fileGuid)
        {
            var date = context.Database.SqlQuery<DateTime>("SELECT file_upload_date " +
                "FROM dbo.group_file_of_one_upload " +
                "WHERE group_file_guid = {0}", fileGuid);

            return date.First();
        }

        public string GetUserName(string fileGuid)
        {
            var userName = context.Database.SqlQuery<string>("SELECT[user_name] " +
                "FROM dbo.group_file_of_one_upload " +
                "WHERE group_file_guid = {0}", fileGuid);

            return userName.First();
        }

        public string GetFileGuidById(int fileId)
        {
            return context.Database.SqlQuery<string>("" +
                "SELECT group_file_guid " +
                "FROM dbo.group_file_of_one_upload gfou" +
                " JOIN all_files_and_group_files_link afgfl ON " +
                "gfou.group_file_id = afgfl.group_file_id " +
                "JOIN all_files af ON " +
                "afgfl.[file_id] = af.[file_id] " +
                "WHERE af.[file_id] = {0}", fileId).First();
        }

        public List<string> GetAllUserFile(string username)
        {
            List<string> fileGuids = new List<string>();

            var userName = context.Database.SqlQuery<string>("SELECT group_file_guid " +
                "FROM group_file_of_one_upload gfou " +
                "JOIN AspNetUsers anu " +
                "ON gfou.user_name = anu.UserName " +
                "WHERE anu.UserName = {0}", username);

            foreach (var item in userName)
            {
                fileGuids.Add(item);
            }

            return fileGuids;
        }
        //public GroupFilesOfOneUpload GetGroupByGuid(string groupGuid)
        //{
        //    var test = context.GroupFilesOfOneUpload.Where(guid => guid.GroupFilesGuid == groupGuid).ToList();
        //    GroupFilesOfOneUpload groupFiles = new GroupFilesOfOneUpload();
        //    groupFiles.AllFilesAndGroupFilesLink.

        //    return 
        //}

        public List<string> GetAllFileGuids()
        {
            List<string> allGuids = new List<string>();

            var userName = context.Database.SqlQuery<string>("SELECT group_file_guid " +
                "FROM group_file_of_one_upload gfou " +
                "JOIN all_files_and_group_files_link afgfl " +
                "ON gfou.group_file_id = afgfl.group_file_id " +
                "JOIN all_files af " +
                "ON afgfl.file_id = af.file_id " +
                "GROUP BY group_file_guid");

            foreach (var item in userName)
            {
                allGuids.Add(item);
            }

            return allGuids;
        }
    }
}
