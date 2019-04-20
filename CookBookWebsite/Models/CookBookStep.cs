using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    /// <summary>
    /// 菜谱步骤
    /// </summary>
    public class CookBookStep
    {
        [Key]
        public int StepID { get; set; }

        [Display(Name = "序号")]
        public int? OrderID { get; set; }

        [Display(Name = "图片URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "内容")]
        public string Content { get; set; }

        [Display(Name = "关联菜谱")]
        public int CookBookID { get; set; }

        [Display(Name = "关联菜谱")]
        public virtual CookBook CookBook { get; set; }
    }
}