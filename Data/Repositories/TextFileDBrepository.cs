using Data.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class TextFileDBrepository
    {
        private FileSharingContext Context;

        public TextFileDBrepository(FileSharingContext _Context)
        {
            _Context = Context;
        }

        public void CreateFile(TextFileModel textFile)
        {
            Context.TextFileModels.Add(textFile);
            Context.SaveChanges();
        }



        public void EditFile(Guid filename, string changes, TextFileModel updated)
        {
            var ogFile = GetFile(updated.FileName);

            ogFile.FileName = filename;
            ogFile.UploadedOn = DateTime.Now;
            ogFile.Data = changes;
            ogFile.LastUpdated = updated.LastUpdated;
            ogFile.LastEditedBy = updated.LastEditedBy;
            Context.SaveChanges();
        }


        public void ShareFile(int Id, string Recipient)
        {
            var recipient = Context.AclModels.SingleOrDefault(x => x.UserName.Equals(Recipient));
            recipient.Id = Id;
            Context.SaveChanges();
        }

        public IQueryable<TextFileModel> GetFiles()
        {
            return Context.TextFileModels;
        }

        public TextFileModel GetFile(Guid filename)
        {
            return GetFiles().SingleOrDefault(x => x.FileName == filename);
        }

        public void CreatePermissions(AclModel aclModel)
        {
            Context.AclModels.Add(aclModel);
            Context.SaveChanges();
        }

        public IQueryable<AclModel> GetPermissions()
        {
            return Context.AclModels;
        }

    }
}
