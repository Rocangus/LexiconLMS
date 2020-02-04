using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Core.ViewModels
{
    public class SystemUserViewModel
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}