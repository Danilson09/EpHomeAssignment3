using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class LogService
    {
        private IlogRepository IlogRepository { get; set; }

        public LogService(IlogRepository _IlogRepository)
        {
            this.IlogRepository = _IlogRepository;
        }

        public void Log(string Message, string IP, string UserName)
        {
            IlogRepository.Log(Message, IP, UserName);
        }

        public void Log(Exception exeption, string IP, string UserName)
        {
            IlogRepository.Log(exeption, IP, UserName);
        }
    
    }
}
