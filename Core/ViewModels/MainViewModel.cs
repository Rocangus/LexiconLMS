using LexiconLMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class MainViewModel
    {
        public SystemUserViewModel SystemUser { get; set; }
        public Course Course { get; set; }
    }
}
