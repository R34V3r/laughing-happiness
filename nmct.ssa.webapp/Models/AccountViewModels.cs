using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nmct.ssa.webapp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Emailadres")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Deze browser onthouden?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen")]
        [Display(Name = "Emailadres")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Emailadres is verplicht in te vullen")]
        [Display(Name = "Emailadres")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht in te vullen")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Display(Name = "Herinner mij?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Emailadres is verplicht in te vullen")]
        [EmailAddress(ErrorMessage = "Vul een geldig email adres in.")]
        [Display(Name = "Emailadres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord verplicht in te vullen")]
        [StringLength(100, ErrorMessage = "Het wachtwoord moet een sterke combinatie zijn van letters, cijfers en tekens!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Wachtwoord bevestigen is verplicht in te vullen")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord bevestigen")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en wachtwoord bevestigen veld komt niet overeen!")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Emailadres")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het wachtwoord moet een sterke combinatie zijn van letters, cijfers en tekens!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord bevestigen")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en wachtwoord bevestigen veld komt niet overeen!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen")]
        [EmailAddress]
        [Display(Name = "Emailadres")]
        public string Email { get; set; }
    }
}
