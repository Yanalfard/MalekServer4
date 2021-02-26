using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MalekServer3.Models.ViewModel
{
    public class ContactViewModel
    {
        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [StringLength(150)]
        public string Title { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(600, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [DataType(DataType.MultilineText)]
        [StringLength(600)]
        public string Body { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(15, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [MinLength(8, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        public string TellNo { get; set; }
    }
}