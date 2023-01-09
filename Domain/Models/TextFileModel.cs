using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class TextFileModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid FileName { get; set; }

        [Required]
        public DateTime UploadedOn { get; set; }

        [Required]
        public string AuthorName { get; set; }
        [Required]

        public string LastEditedBy { get; set; }
        public Nullable<DateTime> LastUpdated { get; set; }

        [Required]
        public string Data { get; set; }

        public string Path { get; set; }

    }
}