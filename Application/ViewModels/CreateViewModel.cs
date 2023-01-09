using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class CreateViewModel
    {


  
        [Required]
        public string AuthorName { get; set; }
       
        public string FilePath { get; set; }
    }
}
