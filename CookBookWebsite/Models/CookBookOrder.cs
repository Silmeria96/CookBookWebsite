using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    /// <summary>
    /// 菜谱订单
    /// </summary>
    public class CookBookOrder
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "创建人")]
        public virtual User CreateUser { get; set; }

        [Display(Name = "菜谱列表")]
        public virtual ICollection<CookBook> CookBookList { get; set; }
    }
}