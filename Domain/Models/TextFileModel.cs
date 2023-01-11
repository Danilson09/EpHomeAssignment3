using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class TextFileModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FileName { get; set; }

        [Required]
        public DateTime UploadedOn { get; set; }

        [Required]
        public string AuthorName { get; set; }
        

        public string LastEditedBy { get; set; }
        public Nullable<DateTime> LastUpdated { get; set; }

        public string Data { get; set; }

        public string Path { get; set; }

    }
}