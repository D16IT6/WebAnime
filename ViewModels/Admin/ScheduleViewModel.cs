using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Admin
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ngày phát hành")]
        [Required(ErrorMessage = "Ngày không được để trống")]
        [CustomValidation(typeof(ScheduleViewModel), "ValidateReleaseDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AiringDate { get; set; }

        [Display(Name = "Giờ phát hành")]
        [Required(ErrorMessage = "Giờ không được để trống")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan AiringTime { get; set; }

        public DateTime CurrentDate
        {
            get { return DateTime.Now; }
        }

        public static ValidationResult ValidateReleaseDate(DateTime releaseDate, ValidationContext context)
        {
            var currentDate = (DateTime)context.ObjectInstance.GetType().GetProperty("CurrentDate").GetValue(context.ObjectInstance, null);

            if (releaseDate <= currentDate)
            {
                return new ValidationResult("Ngày phát hành phải lớn hơn ngày hiện tại");
            }

            return ValidationResult.Success;
        }
    }
}
