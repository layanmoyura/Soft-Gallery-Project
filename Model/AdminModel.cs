using System.ComponentModel.DataAnnotations;
using Xamarin.Essentials;

namespace PresentationLayer.Models
{
    public class AdminModel
    {
        public int UserID { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string FirstName { get; set; }


        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
