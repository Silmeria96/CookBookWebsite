using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    /// <summary>
    /// 菜谱评论
    /// </summary>
    public class CookBookComment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "评论内容")]
        public string Content { get; set; }

        [Display(Name = "评论时间")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "评论者")]
        public int CreateUserID { get; set; }

        [Display(Name = "关联菜谱")]
        public int CookBookID { get; set; }


        [JsonIgnore]
        public virtual User CreateUser { get; set; }

        [JsonIgnore]
        public virtual CookBook CookBook { get; set; }
    }
}