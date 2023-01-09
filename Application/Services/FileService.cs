using Data.Context;
using Data.Repositories;
using Domain.Models;
using System;
using System.Linq;


namespace Application.Services
{
    public class FileService
    {
        private TextFileDBrepository TextFileDBRepository { get; set; }
        public FileService(TextFileDBrepository _textFileDBRepository)
        {
            TextFileDBRepository = _textFileDBRepository;
        }




        public FileSharingContext context;


        public IQueryable<TextFileModel> GetFilesEntry()
        {
            var result = from file in TextFileDBRepository.GetFiles()
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

        public FileService(FileSharingContext _context)
        {
            context = _context;
        }


        public IQueryable<AclModel> GetPermissions()
        {
            var permissions = from permission in TextFileDBRepository.GetPermissions()
                              select new AclModel()
                              {
                                  Permissions = permission.Permissions

                              };
            return permissions;

        }

        public void CreateNewFile(Guid fileName, DateTime uploadedOn, string data, string authorName, string path)
        {
            if (TextFileDBRepository.GetFiles().Where(x => x.FileName == fileName).Count() > 0)
            {
                throw new Exception("File exists. change file name");
            }

            TextFileDBRepository.CreateFile(new TextFileModel()
            {

                AuthorName = authorName,
                UploadedOn = uploadedOn,
                FileName = fileName,
                Data = data,
                FilePath = path
            });
        }

        public void EditFile(Guid name, string UpdatedData)
        {



            TextFileDBRepository.EditFile(name, UpdatedData, new TextFileModel()
            {
                FileName = name,
                LastUpdated = DateTime.Now,
                Data = UpdatedData

            });
        }

        public void ShareFile(int id, string Recipient, AclModel file)
        {
            TextFileDBRepository.ShareFile(id, Recipient);
            {
                id = file.Id;
                Recipient = file.UserName;
            }

        }




    }
}
