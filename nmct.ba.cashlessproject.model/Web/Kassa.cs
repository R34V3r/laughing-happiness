using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.model.Web
{
    public class Kassa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [Display(Name = "Naam kassa")]
        [DataType(DataType.Text)]
        public string RegisterName { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [Display(Name = "Toestel type")]
        [DataType(DataType.Text)]
        public string Device { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [Display(Name = "Aankoopdatum")]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht in te vullen.")]
        [Display(Name = "Verloopdatum")]
        [DataType(DataType.Date)]
        public DateTime ExpiresDate { get; set; }

    }
}
