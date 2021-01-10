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
    public class PACKAGEs1Controller : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: PACKAGEs1
        public async Task<ActionResult> Index()
        {
            var pACKAGE = db.PACKAGE.Include(p => p.StanP);
            return View(await pACKAGE.ToListAsync());
        }

        // GET: PACKAGEs1/Create
        public ActionResult Create()
        {
            ViewBag.id_stan_p = new SelectList(db.StanP, "id_stan_p", "stan_p");
            return View();
        }

        // POST: PACKAGEs1/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_package,package_info,cost,p_width,p_length,p_height,id_stan_p")] PACKAGE pACKAGE)
        {
            if (ModelState.IsValid)
            {
                db.PACKAGE.Add(pACKAGE);
                await db.SaveChangesAsync();
                Fill_P();
                return RedirectToAction("Index");
            }

            ViewBag.id_stan_p = new SelectList(db.StanP, "id_stan_p", "stan_p", pACKAGE.id_stan_p);
            return View(pACKAGE);
        }
        public void Fill_P()
        {
            var p = db.PACKAGE.ToList().Last();
            foreach (var y in db.SECTION.ToList())
            {
                db.SectionPackage.Add(new SectionPackage() { id_package = p.id_package, id_section = y.id_section, count = 0 });
                db.SaveChanges();
            }
        }
        // GET: PACKAGEs1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGE pACKAGE = await db.PACKAGE.FindAsync(id);
            if (pACKAGE == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_stan_p = new SelectList(db.StanP, "id_stan_p", "stan_p", pACKAGE.id_stan_p);
            return View(pACKAGE);
        }

        // POST: PACKAGEs1/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_package,package_info,cost,p_width,p_length,p_height,id_stan_p")] PACKAGE pACKAGE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pACKAGE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_stan_p = new SelectList(db.StanP, "id_stan_p", "stan_p", pACKAGE.id_stan_p);
            return View(pACKAGE);
        }

        // GET: PACKAGEs1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGE pACKAGE = await db.PACKAGE.FindAsync(id);
            if (pACKAGE == null)
            {
                return HttpNotFound();
            }
            return View(pACKAGE);
        }

        // POST: PACKAGEs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PACKAGE pACKAGE = await db.PACKAGE.FindAsync(id);
            //db.NAKLADNA.RemoveRange(db.NAKLADNA.Where(x => x.id_package == id));
            db.PACKAGE.Remove(pACKAGE);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
