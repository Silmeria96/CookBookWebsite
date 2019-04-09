using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CookBookWebsite.Controllers
{
    public class ImageUploadController : Controller
    {
        /// <summary>
        /// 上传菜谱缩略图
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        [HttpPost]
        public JObject UploadCookBookImage(HttpPostedFileBase img)
        {
            JObject result = new JObject();

            if (img == null)
            {
                result["flag"] = false;
                result["msg"] = "请先选择要上传的图片！";
                return result;
            }

            var fileName = img.FileName;
            var filePath = Server.MapPath("~/Images/CookBook/");
            var fileViewPath = Url.Content(string.Format("~/Images/CookBook/{0}", fileName));

            try
            {
                img.SaveAs(Path.Combine(filePath, fileName));
                result["flag"] = true;
                result["msg"] = "上传成功！";
                result["path"] = fileViewPath;
            }
            catch
            {
                result["flag"] = false;
                result["msg"] = "上传失败！";
            }

            return result;
        }

    }
}