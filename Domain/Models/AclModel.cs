using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class AclModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [ForeignKey("TextFileModel")]
        public Guid FileName { get; set; }

        public virtual TextFileModel TextFileModel { get; set; }

        public bool Permissions { get; set; }
    }
}