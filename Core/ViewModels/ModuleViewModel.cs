using LexiconLMS.Core.Models;
using LexiconLMS.Core.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class ModuleViewModel
    {
        public List<DocumentsModules> Documents { get; set; }
        public Module Module { get; set; }
        public List<Activity> Activities { get; set; }
        public Activity Activity { get; set; }

    }
}
