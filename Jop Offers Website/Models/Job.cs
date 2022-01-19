using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Jop_Offers_Website.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Display(Name ="عنوان الوظيفه")]
        [Required(ErrorMessage = "لابد ان تدخل عنوان للوظيفه")]
        public string JobTitle { get; set; }

        [Display(Name = "وصف الوظيفه")]
        [Required(ErrorMessage = "لابد ان تدخل وصف لوظيفه")]
        public string JobContent { get; set; }

        [Display(Name = "صوره الوظيفه")]
        public string JobImage { get; set; }

        [Display(Name ="تاريخ النشر")]
        public DateTime jobDate { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل الحد الادنى من الراتب")]
        [Range(500, 30000, ErrorMessage = "ادخل رقم من 500 الى 30000")]
        [Display(Name = "الحد الادنى للراتب")]
        public int MinSalary { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل الحد الاعلى من الراتب")]
        [Range(500, 30000, ErrorMessage = "ادخل رقم من 500 الى 30000")]
        [Display(Name = "الحد الاعلى للراتب")]
        public int MaxSalary { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل الحد الادنى من السن المطلوب")]
        [Range(18, 60, ErrorMessage = "ادخل رقم من 18 الى 60")]
        [Display(Name = "الحد الادنى للعمر")]
        public int MinAge { get; set; }

        [Required(ErrorMessage = "يجب ان تدخل الخد الاعلى من السن المطلوب")]
        [Range(18, 60, ErrorMessage = "ادخل رقم من 18 الى 60")]
        [Display(Name = "الحد الاعلى")]
        public int MaxAge { get; set; }

        [Display(Name = "نوع الوظيفه")]
        [Required(ErrorMessage = "يجب ان تختار نوع الوظيفه")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public int GovernoratesId { get; set; }
        public int ExperienceId { get; set; }
        public int GenderId { get; set; }
        public int QualificationId { get; set; }
        public virtual Category category { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Governorates Governorates { get; set; }
        public virtual ExperienceLevel Experience { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Qualification Qualification { get; set; }

    }
}