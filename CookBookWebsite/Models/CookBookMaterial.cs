using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    /// <summary>
    /// 菜谱食材
    /// </summary>
    public class CookBookMaterial
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "食材等级（主料-3、辅料-2、其它-1）")]
        public int? Level { get; set; }

        [Display(Name = "食材名称")]
        public string Name { get; set; }

        [Display(Name = "食材数量")]
        public string Amount { get; set; }

        [Display(Name = "关联菜谱")]
        public int CookBookID { get; set; }



        public virtual CookBook CookBook { get; set; }
    }
}