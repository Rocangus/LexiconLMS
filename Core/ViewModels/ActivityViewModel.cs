using LexiconLMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class ActivityViewModel
    {
        public Activity Activity { get; set; }
        public bool IsAssignment { get; set; }
    }
}
