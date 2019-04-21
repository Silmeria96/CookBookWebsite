using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    /// <summary>
    /// 数据库初始化类
    /// </summary>
    public class CookBookDbInitializer : DropCreateDatabaseAlways<CookBookDbContext>
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(CookBookDbContext context)
        {
            #region 用户

            User user1 = new User();
            user1.Account = "user1";
            user1.Name = "小灰";
            user1.Password = "123123";
            user1.Sex = 0;
            context.Users.Add(user1);

            User user2 = new User();
            user2.Account = "user2";
            user2.Name = "小红";
            user2.Password = "123123";
            user2.Sex = 1;
            context.Users.Add(user2);

            User user3 = new User();
            user3.Account = "user3";
            user3.Name = "小蓝";
            user3.Password = "123123";
            user3.Sex = 1;
            context.Users.Add(user3);

            context.SaveChanges();

            #endregion

            #region 菜谱

            CookBook yuxiangrousi = new CookBook();
            yuxiangrousi.CreateDate = DateTime.Now;
            yuxiangrousi.Title = "鱼香肉丝";
            yuxiangrousi.Content = @"
                鱼香肉丝是一道传统的家常菜，经典而有人气。听说还是三级厨师的考试必考菜。说的这么玄乎，很多人都以为是一道很难得菜。其实不然。准备好的话，就很容易了。
            ";
            yuxiangrousi.Image = "/Images/CookBook/yuxiangrousi.jpg";
            yuxiangrousi.NeedPayment = false;
            yuxiangrousi.Price = 10.00;
            yuxiangrousi.LikeNum = 0;
            context.CookBooks.Add(yuxiangrousi);

            context.SaveChanges();

            #endregion

            #region 菜谱分类

            CookBookCategory jiachangcai = new CookBookCategory();
            jiachangcai.Name = "家常菜";
            jiachangcai.RelatedCookBooks = new List<CookBook>();
            jiachangcai.RelatedCookBooks.Add(yuxiangrousi);
            context.CookBookCategories.Add(jiachangcai);

            CookBookCategory chuancai = new CookBookCategory();
            chuancai.Name = "川菜";
            chuancai.RelatedCookBooks = new List<CookBook>();
            chuancai.RelatedCookBooks.Add(yuxiangrousi);
            context.CookBookCategories.Add(chuancai);

            context.SaveChanges();

            #endregion

            #region 食材

            #region 鱼香肉丝

            CookBookMaterial yuxiangrousi1 = new CookBookMaterial();
            yuxiangrousi1.Level = 3;
            yuxiangrousi1.Name = "里脊肉";
            yuxiangrousi1.Amount = "300g";
            yuxiangrousi1.CookBook = yuxiangrousi;
            yuxiangrousi1.CookBookID = yuxiangrousi.ID;
            context.CookBookMaterials.Add(yuxiangrousi1);

            CookBookMaterial yuxiangrousi2 = new CookBookMaterial();
            yuxiangrousi2.Level = 3;
            yuxiangrousi2.Name = "笋";
            yuxiangrousi2.Amount = "150g";
            yuxiangrousi2.CookBook = yuxiangrousi;
            yuxiangrousi2.CookBookID = yuxiangrousi.ID;
            context.CookBookMaterials.Add(yuxiangrousi2);

            CookBookMaterial yuxiangrousi3 = new CookBookMaterial();
            yuxiangrousi3.Level = 3;
            yuxiangrousi3.Name = "木耳";
            yuxiangrousi3.Amount = "50g";
            yuxiangrousi3.CookBook = yuxiangrousi;
            yuxiangrousi3.CookBookID = yuxiangrousi.ID;
            context.CookBookMaterials.Add(yuxiangrousi3);

            CookBookMaterial yuxiangrousi4 = new CookBookMaterial();
            yuxiangrousi4.Level = 2;
            yuxiangrousi4.Name = "剁椒";
            yuxiangrousi4.Amount = "一汤匙";
            yuxiangrousi4.CookBook = yuxiangrousi;
            yuxiangrousi4.CookBookID = yuxiangrousi.ID;
            context.CookBookMaterials.Add(yuxiangrousi4);

            CookBookMaterial yuxiangrousi5 = new CookBookMaterial();
            yuxiangrousi5.Level = 2;
            yuxiangrousi5.Name = "郫县豆瓣酱";
            yuxiangrousi5.Amount = "三分之一汤匙";
            yuxiangrousi5.CookBook = yuxiangrousi;
            yuxiangrousi5.CookBookID = yuxiangrousi.ID;
            context.CookBookMaterials.Add(yuxiangrousi5);

            CookBookMaterial yuxiangrousi6 = new CookBookMaterial();
            yuxiangrousi6.Level = 2;
            yuxiangrousi6.Name = "蒜";
            yuxiangrousi6.Amount = "五粒";
            yuxiangrousi6.CookBook = yuxiangrousi;
            yuxiangrousi6.CookBookID = yuxiangrousi.ID;
            context.CookBookMaterials.Add(yuxiangrousi6);

            CookBookMaterial yuxiangrousi7 = new CookBookMaterial();
            yuxiangrousi7.Level = 1;
            yuxiangrousi7.Name = "盐";
            yuxiangrousi7.Amount = "半茶匙";
            yuxiangrousi7.CookBook = yuxiangrousi;
            yuxiangrousi7.CookBookID = yuxiangrousi.ID;
            context.CookBookMaterials.Add(yuxiangrousi7);

            context.SaveChanges();

            #endregion

            #endregion

            #region 菜谱步骤

            CookBookStep yuxiangrousiStep1 = new CookBookStep();
            yuxiangrousiStep1.OrderID = 1;
            yuxiangrousiStep1.ImageUrl = "/Images/CookBookStep/yuxiangrousi-step1.jpg";
            yuxiangrousiStep1.Content = "原料图如图。";
            yuxiangrousiStep1.CookBookID = yuxiangrousi.ID;
            yuxiangrousiStep1.CookBook = yuxiangrousi;
            context.CookBookSteps.Add(yuxiangrousiStep1);

            CookBookStep yuxiangrousiStep2 = new CookBookStep();
            yuxiangrousiStep2.OrderID = 2;
            yuxiangrousiStep2.ImageUrl = "/Images/CookBookStep/yuxiangrousi-step2.jpg";
            yuxiangrousiStep2.Content = "笋切丝用水煮6分钟，木耳切丝，里脊切丝用蛋清一个，胡椒粉一茶匙，食用油三茶匙腌渍十分钟。所有的调料除去水淀粉兑成汁。葱姜蒜切末和剁椒，豆瓣酱混合。";
            yuxiangrousiStep2.CookBookID = yuxiangrousi.ID;
            yuxiangrousiStep2.CookBook = yuxiangrousi;
            context.CookBookSteps.Add(yuxiangrousiStep2);

            CookBookStep yuxiangrousiStep3 = new CookBookStep();
            yuxiangrousiStep3.OrderID = 3;
            yuxiangrousiStep3.ImageUrl = "/Images/CookBookStep/yuxiangrousi-step3.jpg";
            yuxiangrousiStep3.Content = "腌渍好的肉丝用干淀粉两茶匙抓匀，锅里放油，五成热放入，大火滑散，变白盛出备用。";
            yuxiangrousiStep3.CookBookID = yuxiangrousi.ID;
            yuxiangrousiStep3.CookBook = yuxiangrousi;
            context.CookBookSteps.Add(yuxiangrousiStep3);

            CookBookStep yuxiangrousiStep4 = new CookBookStep();
            yuxiangrousiStep4.OrderID = 4;
            yuxiangrousiStep4.ImageUrl = "/Images/CookBookStep/yuxiangrousi-step4.jpg";
            yuxiangrousiStep4.Content = "锅里剩下的油，小火炒香剁椒郫县豆瓣，葱姜蒜。";
            yuxiangrousiStep4.CookBookID = yuxiangrousi.ID;
            yuxiangrousiStep4.CookBook = yuxiangrousi;
            context.CookBookSteps.Add(yuxiangrousiStep4);

            CookBookStep yuxiangrousiStep5 = new CookBookStep();
            yuxiangrousiStep5.OrderID = 5;
            yuxiangrousiStep5.ImageUrl = "/Images/CookBookStep/yuxiangrousi-step5.jpg";
            yuxiangrousiStep5.Content = "放入肉丝笋丝木耳丝炒均匀，兑入料汁，大火翻炒到笋丝木耳丝断生。";
            yuxiangrousiStep5.CookBookID = yuxiangrousi.ID;
            yuxiangrousiStep5.CookBook = yuxiangrousi;
            context.CookBookSteps.Add(yuxiangrousiStep5);

            CookBookStep yuxiangrousiStep6 = new CookBookStep();
            yuxiangrousiStep6.OrderID = 6;
            yuxiangrousiStep6.ImageUrl = "/Images/CookBookStep/yuxiangrousi-step6.jpg";
            yuxiangrousiStep6.Content = "勾芡即可。";
            yuxiangrousiStep6.CookBookID = yuxiangrousi.ID;
            yuxiangrousiStep6.CookBook = yuxiangrousi;
            context.CookBookSteps.Add(yuxiangrousiStep6);

            context.SaveChanges();

            #endregion

            base.Seed(context);
        }

    }
}