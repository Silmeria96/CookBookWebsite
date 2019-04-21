using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Article
    {
        [Key]
        public int ID { get; set; }        

        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Display(Name = "副标题")]
        public string SubTitle { get; set; }

        [Display(Name = "缩略图URL")]
        public string Image { get; set; }

        [Display(Name = "文章内容")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [ReadOnly(true)]
        [Display(Name = "创建日期")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "创建人")]
        public int CreateUserID { get; set; }

        [Display(Name = "点赞量")]
        public int LikeNum { get; set; }



        public virtual User CreateUser { get; set; }
    }
}