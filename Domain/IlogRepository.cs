using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IlogRepository
    {
        void Log(string Message, string IP, string UserName);
        void Log(Exception exception, string IP, string UserName);
    }
}
