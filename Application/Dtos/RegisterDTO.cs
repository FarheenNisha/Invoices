using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("User Name")]
        [StringLength(128, ErrorMessage = "{0} can be {1} characters max")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "{0} can be {1} characters max", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[!#\$%&'\(\)\*\+,-\.\/:;<=>\?@[\]\^_`\{\|}~])[a-zA-Z0-9!#\$%&'\(\)\*\+,-\.\/:;<=>\?@[\]\^_`\{\|}~]{0,}$", ErrorMessage = "The password must contain atleast one uppercase, one lowercase, one digit and one special character")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "{0} can be {1} characters max", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(128, ErrorMessage = "{0} can be {1} characters max")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Enter a valid email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Phone Number")]
        [StringLength(10, ErrorMessage = "{0} can be {1} numerics max")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid 10 digit number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(128, ErrorMessage = "{0} can be {1} characters max")]
        public string Name { get; set; }

        
        [StringLength(64, ErrorMessage = "{0} can be {1} characters max")]
        public string ProfileImage { get; set; }
        public string Role { get; set; }
    }
}
