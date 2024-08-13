using System.ComponentModel.DataAnnotations;

namespace duma.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Введите число с картинки")]
        public string Captcha { get; set; }
    }
}