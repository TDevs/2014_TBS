using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System;
using System.Linq;
using System.Web.Mvc;
using Oas.Service.Xml;


namespace Oas.Service.Files
{
    public class FileUltity
    {
        public static string UploadFile(HttpPostedFileBase file)//NewSDSModel sds
        {

            if (file.ContentLength > 0)
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads");
                string filePath = string.Empty;

                var stream = System.Web.HttpContext.Current.Request.InputStream;
                HttpPostedFileBase postedFile = file;
                stream = postedFile.InputStream;
                filePath = Path.Combine(path, System.IO.Path.GetFileName(file.FileName.Trim().Replace('_', '_').ToLower()));

                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                System.IO.File.WriteAllBytes(filePath, buffer);
                return filePath;

            }

            return string.Empty;
        }
    }
}
