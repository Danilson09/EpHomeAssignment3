
using Application.ViewModels;
using Data.Repositories;
using Domain.Models;
using System;
using System.Linq;


namespace Application.Services
{
    public class FileService
    {
        private TextFileDBrepository TextFileDBrepository { get; set; }
        public FileService(TextFileDBrepository _textFileDBRepository)
        {
            TextFileDBrepository = _textFileDBRepository;
        }
        public IQueryable<TextFileModel> GetFilesEntry()
        {
            var File = from f in TextFileDBrepository.GetFiles()
                         select new TextFileModel()
                         {
                             FileName = f.FileName,
                             AuthorName = f.AuthorName,
                             UploadedOn = f.UploadedOn,
                             Data = f.Data,
                             LastEditedBy = f.LastEditedBy,
                             LastUpdated = f.LastUpdated,

                         };
            return File;
        }


        public TextFileModel GetFileEntry(Guid FileID)
        {
            return GetFilesEntry().SingleOrDefault(file => file.FileName == FileID);
        }

        public Guid CreateNewFile(Guid name,string content, string authorName, string path = "")
        {
            if (TextFileDBrepository.GetFiles().Where(x => x.FileName == name).Count() > 0)
            {
                throw new Exception("File exists. change file name");
            }

            TextFileDBrepository.CreateFile(new TextFileModel()
            {
                FileName = name,
                UploadedOn = DateTime.Now,
                Data = content,
                AuthorName = authorName,
                Path = path,
                
            });
            return name;
        }

        public void CreatePermissions(Guid Name, string UserName, bool Permission)
        {
            var file = GetFilesEntry().SingleOrDefault(x => x.FileName == Name);
            TextFileDBrepository.CreatePermissions(new Domain.Models.AclModel()
            {
                FileName = Name,
                Permissions = Permission,
                UserName = UserName
            });
        }

        public IQueryable<AclModel> GetPermissions()
        {
            var GetPermissions = from perm in TextFileDBrepository.GetPermissions()
                                 select new AclModel()
                                 {
                                     Id = perm.Id
                                 };
            return GetPermissions;
        }

        public void EditFile(Guid name, string UpdatedData,CreateViewModel textfile)
        {
            TextFileDBrepository.EditFile(name, UpdatedData, new TextFileModel()
            {
                FileName = name,
                
                Data = UpdatedData,
                LastUpdated = DateTime.Now,
                LastEditedBy = textfile.LastEditedBy

            });
        }






    }
}
