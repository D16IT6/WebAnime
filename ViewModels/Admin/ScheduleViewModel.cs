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
        //[ForeignKey(nameof(AnimeID))]
        public int Id { get; set; }
        [Range(1, 7, ErrorMessage = "{0} phải nằm trong khoảng từ {1} đến {2}")]
        [Display(Name = "Ngày trong tuần")]
        [Required(ErrorMessage = "Ngày được để chống", AllowEmptyStrings = false)]
        public int DayOfWeek { get; set; }
        
        [Display(Name = "Thời gian")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public DateTime Time { get; set; }
        
        //[DataType(DataType.DateTime)] 
        //public DateTime? CreatedDate { get; set; }
        //public int? CreatedBy { get; set; }

        //[DataType(DataType.DateTime)] 
        //public DateTime? ModifiedDate { get; set; }
        //public int? ModifiedBy { get; set; }

        //[DataType(DataType.DateTime)] 
        //public DateTime? DeletedDate { get; set; }
        //public int? DeletedBy { get; set; }

    }
}
