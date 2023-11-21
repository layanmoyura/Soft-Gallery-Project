using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class AdminLoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }
    }
}
