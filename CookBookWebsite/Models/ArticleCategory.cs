using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    /// <summary>
    /// 文章分类
    /// </summary>
    public class ArticleCategory
    {
        [Key]
        public int ID { get; set; }

        [Display(Name="分类名称")]
        public string Name { get; set; }

        public virtual ICollection<Article> RelatedArticles { get; set; }
    }
}