using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Ext.Direct.Mvc.Attributes;

namespace Ext.Direct.MvcTest.Controllers {
    public class FilesController : DirectController {
        [FormHandler]
        public ActionResult Upload() {
            var files = Request.Files;

            foreach (string file in files) {
                HttpPostedFileBase hpf = files[file];
                if (hpf.ContentLength == 0)
                    continue;
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploaded Files");
                string savedFileName = Path.Combine(folderPath, Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);
            }

            return Json(new { success = true });
        }
    }
}
