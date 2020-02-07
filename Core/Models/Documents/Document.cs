using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.Models.Documents
{
    public class Document
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Path { get; set; }
        [Required]
        [MaxLength(150)]
        public string Description{get;set;}
        [Required]
        public string SystemUserId { get; set; }
        [Required]
        public DateTime UploadTime { get; set; }

    }
}
