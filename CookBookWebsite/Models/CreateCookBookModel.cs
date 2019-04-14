using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    public class CreateCookBookModel
    {
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Display(Name = "副标题")]
        public string SubTitle { get; set; }

        [Display(Name = "缩略图URL")]
        public string Image { get; set; }

        [Display(Name = "文章内容")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "菜谱步骤")]
        public List<CookBookStep> Steps { get; set; }
    }
}