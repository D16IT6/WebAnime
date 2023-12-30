using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.API
{
    public class AnimeFavoriteViewModel
    {
        [Range(1,9999)]
        public int AnimeId { get; set; }
        
    }
}
