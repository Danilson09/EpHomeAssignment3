using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class LogModel
    {   
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid FileName { get; set; }
        public string Message { get; set; }
        public string IP { get; set; }
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
