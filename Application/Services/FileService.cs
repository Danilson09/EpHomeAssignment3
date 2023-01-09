using Data.Context;
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
            var result = from file in TextFileDBrepository.GetFiles()
                         select new TextFileModel()
                         {
                             FileName = file.FileName,
                             AuthorName = file.AuthorName,
                             UploadedOn = file.UploadedOn,
                             Data = file.Data,
                             LastEditedBy = file.LastEditedBy,
                             LastUpdated = file.LastUpdated,

                         };
            return result;
        }


        public TextFileModel GetFileEntry(Guid name)
        {
            return GetFilesEntry().SingleOrDefault(file => file.FileName == name);
        }

        public Guid CreateNewFile(Guid Name,string content, string authorName, string path = "")
        {
            if (TextFileDBrepository.GetFiles().Where(x => x.FileName == Name).Count() > 0)
            {
                throw new Exception("File exists. change file name");
            }

            TextFileDBrepository.CreateFile(new TextFileModel()
            {
                FileName = Name,
                UploadedOn = DateTime.Now,
                Data = content,
                AuthorName = authorName,
                Path = path,
                
            });
            return Name;
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



        public void EditFile(Guid name, string UpdatedData)
        {



            TextFileDBrepository.EditFile(name, UpdatedData, new TextFileModel()
            {
                FileName = name,
                LastUpdated = DateTime.Now,
                Data = UpdatedData

            });
        }

  




    }
}
