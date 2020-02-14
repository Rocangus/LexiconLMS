using LexiconLMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class SystemUserDetailsViewModel
    {
        public SystemUser SystemUser { get; set; }
        public string SignedInUserId { get; set; }
    }
}
