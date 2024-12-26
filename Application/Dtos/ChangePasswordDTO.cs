using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Cuurent Password")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "{0} can be {1} characters max", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[!#\$%&'\(\)\*\+,-\.\/:;<=>\?@[\]\^_`\{\|}~])[a-zA-Z0-9!#\$%&'\(\)\*\+,-\.\/:;<=>\?@[\]\^_`\{\|}~]{0,}$", ErrorMessage = "The password must contain atleast one uppercase, one lowercase, one digit and one special character")]
        public string CuurentPassword { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("New Password")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "{0} can be {1} characters max", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[!#\$%&'\(\)\*\+,-\.\/:;<=>\?@[\]\^_`\{\|}~])[a-zA-Z0-9!#\$%&'\(\)\*\+,-\.\/:;<=>\?@[\]\^_`\{\|}~]{0,}$", ErrorMessage = "The password must contain atleast one uppercase, one lowercase, one digit and one special character")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "{0} can be {1} characters max", MinimumLength = 8)]
        [Compare("NewPassword", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

    }
}
