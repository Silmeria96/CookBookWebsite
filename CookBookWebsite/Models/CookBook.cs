using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    /// <summary>
    /// 菜谱
    /// </summary>
    public class CookBook
    {
        [Key]
        public int ID { get; set; }

        [ReadOnly(true)]
        [Display(Name = "创建日期")]
        public DateTime? CreateDate { get; set; }

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

        [Display(Name = "是否付费")]
        public bool NeedPayment { get; set; }

        [ReadOnly(true)]
        [Display(Name = "点赞量")]
        public int? LikeNum { get; set; }




        [Display(Name = "菜谱食材")]
        public virtual ICollection<CookBookMaterial> Materials { get; set; }

        [Display(Name = "菜谱步骤")]
        public virtual ICollection<CookBookStep> Steps { get; set; }

        [Display(Name = "菜谱评论")]
        public virtual ICollection<CookBookComment> Comments { get; set; }

        [Display(Name = "关联订单")]
        public virtual ICollection<CookBookOrder> Orders { get; set; }

        [Display(Name = "收藏用户列表")]
        public virtual ICollection<User> UserCollected { get; set; }

        [Display(Name = "加入购物车用户列表")]
        public virtual ICollection<User> UserShopCarted { get; set; }
    }
}