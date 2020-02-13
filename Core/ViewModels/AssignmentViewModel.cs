using LexiconLMS.Core.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Core.ViewModels
{
    public class AssignmentViewModel
    {
        public Document Document { get; set; }
        public SystemUserViewModel User { get; set; }
        public int AssignmentId { get; set; }
    }
}
