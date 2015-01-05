using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.model.Web
{
    public class Vereniging
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [Display(Name = "Gebruikersnaam")]
        [DataType(DataType.Text)]
        public string Login { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [Display(Name = "DB Naam")]
        public string DbName { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [DataType(DataType.Text)]
        [Display(Name = "DB Gebruikersnaam")]
        public string DbLogin { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [DataType(DataType.Password)]
        [Display(Name = "DB Wachtwoord")]
        public string DbPassword { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [DataType(DataType.Text)]
        [Display(Name = "Organisatie")]
        public string OrganisationName { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Emailadres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefoon")]
        public string Phone { get; set; }

        public override string ToString()
        {
            return OrganisationName;
        }


    }
}
