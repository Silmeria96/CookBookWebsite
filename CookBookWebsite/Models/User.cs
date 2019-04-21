using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name="帐号")]
        public string Account { get; set; }

        [Required]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "昵称")]
        public string Name { get; set; }

        [Display(Name = "性别")]
        public int Sex { get; set; }

        [Display(Name = "生日")]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "个人简介")]
        [DataType(DataType.MultilineText)]
        public string Introduction { get; set; }

        [Display(Name = "账户余额")]
        [ScaffoldColumn(false)]
        public double Money { get; set; }




        [Display(Name = "文章列表")]
        public virtual ICollection<Article> Articles { get; set; }

        [Display(Name = "收藏的菜谱")]
        public virtual ICollection<CookBook> CookBookCollected { get; set; }

        [Display(Name = "加入购物车的菜谱")]
        public virtual ICollection<CookBook> CookBookShopCarted { get; set; }

        [Display(Name = "用户订单")]
        public virtual ICollection<CookBookOrder> Orders { get; set; }

    }
}