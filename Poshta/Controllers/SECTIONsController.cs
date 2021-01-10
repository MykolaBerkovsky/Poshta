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
    [Authorize(Roles = "Admin")]
    public class SECTIONsController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: SECTIONs
        public async Task<ActionResult> Index()
        {
            var sECTION = db.SECTION.Include(s => s.REGIONCollection).Include(s => s.StanS);
            return View(await sECTION.ToListAsync());
        }

        // GET: SECTIONs/Create
        public ActionResult Create()
        {
            ViewBag.id_region = new SelectList(db.REGIONCollection, "id_region", "region");
            ViewBag.id_stan_s = new SelectList(db.StanS, "id_stan_s", "stan_s");
            return View();
        }

        // POST: SECTIONs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_section,id_region,town,n_section,id_stan_s")] SECTION sECTION)
        {
            if (db.SECTION.Where(x => x.id_region == sECTION.id_region && x.town == sECTION.town).Select(x => x.n_section).Contains(sECTION.n_section))
            {
                ModelState.AddModelError("n_section", "Такий номер відділення існує");
            }
            if (ModelState.IsValid)
            {
                db.SECTION.Add(sECTION);
                await db.SaveChangesAsync();
                Fill_P();
                return RedirectToAction("Index");
            }

            ViewBag.id_region = new SelectList(db.REGIONCollection, "id_region", "region", sECTION.id_region);
            ViewBag.id_stan_s = new SelectList(db.StanS, "id_stan_s", "stan_s", sECTION.id_stan_s);
            return View(sECTION);
        }
        public void Fill_P()
        {
            var p = db.SECTION.ToList().Last();
            foreach (var y in db.PACKAGE.ToList())
            {
                db.SectionPackage.Add(new SectionPackage() { id_package = y.id_package, id_section = p.id_section, count = 0 });
                db.SaveChanges();
            }
        }
        // GET: SECTIONs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SECTION sECTION = await db.SECTION.FindAsync(id);
            if (sECTION == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_region = new SelectList(db.REGIONCollection, "id_region", "region", sECTION.id_region);
            ViewBag.id_stan_s = new SelectList(db.StanS, "id_stan_s", "stan_s", sECTION.id_stan_s);
            return View(sECTION);
        }

        // POST: SECTIONs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_section,id_region,town,n_section,id_stan_s")] SECTION sECTION)
        {
            if (db.SECTION.Where(x => x.id_region == sECTION.id_region && x.town == sECTION.town).Select(x => x.n_section).Contains(sECTION.n_section))
            {
                ModelState.AddModelError("n_section", "Помилка: спроба повторного створення відділення");
            }
            if (ModelState.IsValid)
            {
                db.Entry(sECTION).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_region = new SelectList(db.REGIONCollection, "id_region", "region", sECTION.id_region);
            ViewBag.id_stan_s = new SelectList(db.StanS, "id_stan_s", "stan_s", sECTION.id_stan_s);
            return View(sECTION);
        }

        //// GET: SECTIONs/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SECTION sECTION = await db.SECTION.FindAsync(id);
        //    if (sECTION == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sECTION);
        //}

        //// POST: SECTIONs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    SECTION sECTION = await db.SECTION.FindAsync(id);
        //    db.SECTION.Remove(sECTION);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
