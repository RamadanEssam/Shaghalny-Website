using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jop_Offers_Website.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل اسمك")]
        [Display(Name = "اسم الراسل")]
        public string Name { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل رقم المحمول الخاص بك")]
        [Display(Name = "رقم المحمول")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^01[0-2][0-9]{8}$", ErrorMessage = "رقم المحمول غير صحيح من فضلك تأكد من عدد الارقام !")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل عنوان للرساله")]
        [Display(Name = "عنوان الرساله")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل نص للرساله")]
        [Display(Name = "نص الرساله")]
        public string Message { get; set; }

        public string userId { get; set; }
        public ApplicationUser user { get; set; }
    }
}