using LexiconLMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class ModuleViewModel
    {
        public Module Module { get; set; }
        public List<Activity> Activities { get; set; }
        public Activity Activity { get; set; }

    }
}
