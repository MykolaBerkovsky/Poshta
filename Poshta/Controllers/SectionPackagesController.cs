using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Poshta.Models;

namespace Poshta.Controllers
{
    [Authorize(Roles = "Manager")]
    public class SectionPackagesController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: SectionPackages
        public ActionResult Index()
        {
            var id = db.USER.Find(Convert.ToInt32(User.Identity.Name)).id_section;
            ViewBag.id_s = id;
            var resylt = (from SectionPackage in db.SectionPackage.Where(x => x.id_section == id).ToList()
                          select new sklad { id = SectionPackage.id_package, info = db.PACKAGE.Find(SectionPackage.id_package).package_info, count = SectionPackage.count });
            return View(resylt);
        }

        // GET: SectionPackages/Edit/5
        public ActionResult Edit(int? id_s, int? id_p)
        {
            if (id_s == null && id_p == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sectionPackage = db.SectionPackage.ToList().Where(x => x.id_package == id_p && x.id_section == id_s).Select(z => new sklad { id = z.id_package, info = db.PACKAGE.Find(z.id_package).package_info, count = z.count }).FirstOrDefault();
            if (sectionPackage == null)
            {
                return HttpNotFound();
            }
            return View(sectionPackage);
        }

        // POST: SectionPackages/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,count")] sklad sklad)
        {
            if (ModelState.IsValid)
            {
                var ids = db.USER.Find(Convert.ToInt32(User.Identity.Name)).id_section;
                SectionPackage sectionPackage = db.SectionPackage.Where(x => x.id_section == ids && x.id_package == sklad.id).FirstOrDefault();
                sectionPackage.count = sklad.count;
                db.Entry(sectionPackage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sklad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
