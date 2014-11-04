using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Ext.Direct.MvcTest.Models;

namespace Ext.Direct.MvcTest.Controllers {
    public class ContactsController : DirectController {

        private readonly ContactsContext db = new ContactsContext();

        public ActionResult GetList(int start, int limit) {
            var total = db.Contacts.Count();
            var contacts = db.Contacts.OrderBy(c => c.FirstName).ThenBy(c => c.LastName).Skip(start).Take(limit).ToList();
            return Json(new {
                total = total,
                data = contacts
            });
        }

        public ActionResult Get(int id) {
            var contact = db.Contacts.Single(c => c.ID == id);
            return Json(new {
                success = true,
                data = contact
            });
        }

        [FormHandler]
        public ActionResult Update(Contact contact) {
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new {
                success = true,
                data = contact
            });
        }
    }
}
