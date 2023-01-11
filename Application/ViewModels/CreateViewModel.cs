using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class CreateViewModel
    {
        public Guid FileName { get; set; }

        public string Data { get; set; }
        public string Author { get; set; }
       
        public DateTime UploadedOn { get; set; }
        public string LastEditedBy { get; set; }
        public Nullable<DateTime> LastUpdated { get; set; }
    }
}
