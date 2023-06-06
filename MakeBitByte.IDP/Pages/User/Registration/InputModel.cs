using System.ComponentModel.DataAnnotations;

namespace MakeBitByte.IDP.Pages.User.Registration
{
    public class InputModel
    {
        public string ReturnUrl { get; set; }

        [MaxLength(500)]
        [Display(Name= "Username")]
        public string UserName { get; set; }

        [MaxLength(500)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [MaxLength(500)]
        [Display(Name = "Given Name")]
        public string GivenName { get; set; }

        [MaxLength(500)]
        [Display(Name = "Family Name")]
        public string FamilyName { get; set; }

        [MaxLength(500)]
        [Display(Name ="Email")]
        [EmailAddress]
        public string Email { get; set; }

        // default role claim to none and subscriberSince claim to current date time
    }
}
