using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jop_Offers_Website.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
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

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display (Name = "نوع الحساب ")]
        public string Usertype { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل اسمك")]
        [Display(Name ="أسم المستخدم")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "يجب نبذه مختصره عنك")]
        [Display(Name = "نبذه عنك")]
        public string About { get; set; }

        //[Required(ErrorMessage = "من فضلك قم باختيار صوره")]
        [Display(Name = "الصوره الشخصيه")]
        public string image { get; set; }

        [Required(ErrorMessage = "يجب ان تختار سنه ميلادك")]
        [Display(Name = "سنه الميلاد")]
        public string YearOfBirth { get; set; }

        [Required(ErrorMessage = "يجب ان تحتار محافظتك")]
        [Display(Name = "المحافظه")]
        public string Governorate { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل بريدك الالكترونى")]
        [EmailAddress]
        [Display(Name = "البريد الالكترونى")]
        public string Email { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل رقم المحمول الخاص بك")]
        [Display(Name = "رقم المحمول")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^01[0-2][0-9]{8}$", ErrorMessage = "رقم المحمول غير صحيح من فضلك تأكد من عدد الارقام !")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل كلمه المرور")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمه السر")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمه السر")]
        [Compare("Password", ErrorMessage = "كلمات السر غير متوافقه")]
        public string ConfirmPassword { get; set; }
    }

    //تعديل الحساب 

    public class EditeProfileViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "أسم المستخدم")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "البريد الالكترونى")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمه السر الحاليه")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "يجب نبذه مختصره عنك")]
        [Display(Name = "نبذه عنك")]
        public string About { get; set; }

        
        [Display(Name = "الصوره الشخصيه")]
        public string image { get; set; }

        [Required(ErrorMessage = "يجب ان تختار سنه ميلادك")]
        [Display(Name = "سنه الميلاد")]
        public string YearOfBirth { get; set; }
        [Required(ErrorMessage = "يجب ان تحتار محافظتك")]
        [Display(Name = "المحافظه")]
        public string Governorate { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل رقم المحمول الخاص بك")]
        [Display(Name = "رقم المحمول")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^01[0-2][0-9]{8}$", ErrorMessage = "رقم المحمول غير صحيح من فضلك تأكد من عدد الارقام !")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمه السر الجديده")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمه السر")]
        [Compare("NewPassword", ErrorMessage = "كلمات السر غير متوافقه برجاء التصحيح")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "كلمات السر غير متوافقه")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
