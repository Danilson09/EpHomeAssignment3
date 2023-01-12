using Data.Context;
using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class LogDBrepository : IlogRepository
    {
        private FileSharingContext Context;

        public LogDBrepository(FileSharingContext _Context)
        {
            this.Context = _Context;
        }

        public void Log(string Message, string IP, string UserName)
        {
            var LogModel = new LogModel()
            {
                FileName = Guid.NewGuid(),
                Message = Message,
                IP = IP,
                UserName = UserName,
                TimeStamp = DateTime.Now
            };

            Context.LogModels.Add(LogModel);
            Context.SaveChanges();
        }

        public void Log(Exception exception, string IP, string UserName)
        {
            var log = new LogModel()
            {
                FileName = Guid.NewGuid(),
                Message = exception.Message,
                IP = IP,
                UserName = UserName,
                TimeStamp = DateTime.Now
            };

            Context.LogModels.Add(log);
            Context.SaveChanges();
        }

        
    }
}
