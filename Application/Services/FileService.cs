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

        public IQueryable<AclModel> GetPermissions()
        {
            var permissions = from permission in TextFileDBrepository.GetPermissions()
                              select new AclModel()
                              {
                                  Permissions = permission.Permissions

                              };
            return permissions;

        }

        public Guid CreateNewFile(Guid fileName, DateTime uploadedOn, string data, string authorName, string path)
        {
            if (TextFileDBrepository.GetFiles().Where(x => x.FileName == fileName).Count() > 0)
            {
                throw new Exception("File exists. change file name");
            }

            TextFileDBrepository.CreateFile(new TextFileModel()
            {

                AuthorName = authorName,
                UploadedOn = uploadedOn,
                FileName = fileName,
                Data = data,
                FilePath = path
            });
            return fileName;
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
