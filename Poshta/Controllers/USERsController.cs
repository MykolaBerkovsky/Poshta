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
    [Authorize(Roles = "Admin, Manager")]
    public class USERsController : Controller
    {
        private ModelDB db = new ModelDB();
        // GET: USERs
        public async Task<ActionResult> Index()
        {
            var uz = db.USER.Find(Convert.ToInt32(User.Identity.Name));
            ViewBag.Role = uz.ROLES.role;
            var uSER = db.USER.Include(u => u.ROLES).Include(u => u.SECTION).Include(u => u.StanU);
            if (uz.ROLES.role == "Manager")
            {
                uSER = uSER.Where(x => x.id_section == uz.id_section);
            }
            return View(await uSER.ToListAsync());
        }

        // GET: USERs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = await db.USER.FindAsync(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: USERs/Create
        public ActionResult Create()
        {
            var uz = db.USER.Find(Convert.ToInt32(User.Identity.Name));
            ViewBag.Role = uz.ROLES.role;
            ViewBag.region = db.REGIONCollection;
            ViewBag.town = db.SECTION.Select(x => new RegionTown { id_region = x.id_region, town = x.town}).Distinct();
            ViewBag.nomer = db.SECTION.Select(x => new townNom { town=x.town, n_section = x.n_section });

            if(db.USER.Find(Convert.ToInt32(User.Identity.Name)).ROLES.role == "Admin")
            {
                ViewBag.id_role = new SelectList(db.ROLES, "id_role", "role");
            }
            else
            {
                ViewBag.id_role = new SelectList(db.ROLES.Where(x => x.role != "Admin"), "id_role", "role");
            }
            ViewBag.stan_u = new SelectList(db.StanU, "id_stan_u", "stan_u");
            return View();
        }
        // POST: USERs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "contact_number,last_name,first_name,surname,phone,mail,id_role,login,password,stan_u,id_region,town,nom")] dopuser dopuser1)
        {
            var uz = db.USER.Find(Convert.ToInt32(User.Identity.Name));
            ViewBag.Role = uz.ROLES.role;
            if (db.USER.Select(x => x.password).Contains(dopuser1.password))
            {
                ModelState.AddModelError("password", "Такий пароль уже існує");
            }
            if (uz.ROLES.role == "Admin")
            {
                if (db.SECTION.Where(x => x.id_region == dopuser1.id_region && x.town == dopuser1.town && x.n_section == dopuser1.nom).FirstOrDefault() == null)
                {
                    ModelState.AddModelError("id_region", "Таке відділення не існує, введіть інші дані");
                }
                if (ModelState.IsValid)
                {
                    USER uSER = new USER();
                    uSER.last_name = dopuser1.last_name;
                    uSER.first_name = dopuser1.first_name;
                    uSER.surname = dopuser1.surname;
                    uSER.phone = dopuser1.phone;
                    uSER.mail = dopuser1.mail;
                    uSER.id_role = dopuser1.id_role;
                    uSER.login = dopuser1.login;
                    uSER.password = dopuser1.password;
                    uSER.stan_u = dopuser1.stan_u;
                    uSER.id_section = db.SECTION.Where(x => x.id_region == dopuser1.id_region && x.town == dopuser1.town && x.n_section == dopuser1.nom).Select(x => x.id_section).First();
                    db.USER.Add(uSER);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    USER uSER = new USER();
                    uSER.last_name = dopuser1.last_name;
                    uSER.first_name = dopuser1.first_name;
                    uSER.surname = dopuser1.surname;
                    uSER.phone = dopuser1.phone;
                    uSER.mail = dopuser1.mail;
                    uSER.id_role = dopuser1.id_role;
                    uSER.login = dopuser1.login;
                    uSER.password = dopuser1.password;
                    uSER.stan_u = dopuser1.stan_u;
                    uSER.id_section = uz.id_section;
                    db.USER.Add(uSER);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            if (db.USER.Find(Convert.ToInt32(User.Identity.Name)).ROLES.role == "Admin")
            {
                ViewBag.id_role = new SelectList(db.ROLES, "id_role", "role");
            }
            else
            {
                ViewBag.id_role = new SelectList(db.ROLES.Where(x => x.role != "Admin"), "id_role", "role");
            }
            ViewBag.stan_u = new SelectList(db.StanU, "id_stan_u", "stan_u", dopuser1.stan_u);
            ViewBag.region = db.REGIONCollection;
            ViewBag.town = db.SECTION.Select(x => new RegionTown { id_region = x.id_region, town = x.town }).Distinct();
            ViewBag.nomer = db.SECTION.Select(x => new townNom { town = x.town, n_section = x.n_section });
            return View(dopuser1);
        }

        // GET: USERs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = await db.USER.FindAsync(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            var uz = db.USER.Find(Convert.ToInt32(User.Identity.Name));
            ViewBag.Role = uz.ROLES.role;
            if (db.USER.Find(Convert.ToInt32(User.Identity.Name)).ROLES.role == "Admin")
            {
                ViewBag.region = db.REGIONCollection;
                ViewBag.id_role = new SelectList(db.ROLES, "id_role", "role");
                ViewBag.town = db.SECTION.Select(x => new RegionTown { id_region = x.id_region, town = x.town }).Distinct();
                ViewBag.nomer = db.SECTION.Select(x => new townNom { town = x.town, n_section = x.n_section });
            }
            else
            {
                ViewBag.id_role = new SelectList(db.ROLES.Where(x => x.role != "Admin"), "id_role", "role");
            }
            ViewBag.stan_u = new SelectList(db.StanU, "id_stan_u", "stan_u", uSER.stan_u);
            return View(uSER);
        }

        // POST: USERs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "contact_number,last_name,first_name,surname,phone,mail,id_role,login,password,stan_u,id_region,town,nom")] dopuser dopuser1)
        {
            var uz = db.USER.Find(Convert.ToInt32(User.Identity.Name));
            ViewBag.Role = uz.ROLES.role;
            if (db.SECTION.Where(x => x.id_region == dopuser1.id_region && x.town == dopuser1.town && x.n_section == dopuser1.nom).FirstOrDefault() == null && uz.ROLES.role == "Admin")
            {
                ModelState.AddModelError("id_section", "Введене відділення не існує, тому встановлене попереднє значення");
            }
            USER uSER = db.USER.Find(dopuser1.contact_number);
            uSER.last_name = dopuser1.last_name;
            uSER.first_name = dopuser1.first_name;
            uSER.surname = dopuser1.surname;
            uSER.phone = dopuser1.phone;
            uSER.mail = dopuser1.mail;
            uSER.id_role = dopuser1.id_role;
            uSER.login = dopuser1.login;
            if (db.USER.Where(x => x.password != uSER.password).Select(x => x.password).Contains(dopuser1.password) && dopuser1.password != uSER.password )
            {
                ModelState.AddModelError("password", "Такий пароль уже існує");
            }
            uSER.password = dopuser1.password;
            uSER.stan_u = dopuser1.stan_u;
            uSER.SECTION = db.USER.Where(x => x.contact_number == dopuser1.contact_number).First().SECTION;
            if (ModelState.IsValid)
            {
                if (uz.ROLES.role == "Admin")
                {
                    uSER.id_section = db.SECTION.Where(x => x.id_region == dopuser1.id_region && x.town == dopuser1.town && x.n_section == dopuser1.nom).Select(x => x.id_section).First();
                }
                else
                {
                    uSER.id_section = uz.id_section;
                }
                db.Entry(uSER).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (db.USER.Find(Convert.ToInt32(User.Identity.Name)).ROLES.role == "Admin")
            {
                ViewBag.region = db.REGIONCollection;
                ViewBag.id_role = new SelectList(db.ROLES, "id_role", "role");
                ViewBag.town = db.SECTION.Select(x => new RegionTown { id_region = x.id_region, town = x.town }).Distinct();
                ViewBag.nomer = db.SECTION.Select(x => new townNom { town = x.town, n_section = x.n_section });
            }
            else
            {
                ViewBag.id_role = new SelectList(db.ROLES.Where(x => x.role != "Admin"), "id_role", "role");
            }
            ViewBag.stan_u = new SelectList(db.StanU, "id_stan_u", "stan_u", dopuser1.stan_u);
            return View(uSER);
        }

        // GET: USERs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = await db.USER.FindAsync(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            USER uSER = await db.USER.FindAsync(id);
            //db.NAKLADNA.RemoveRange(db.NAKLADNA.Where(x => x.contact_number_v == id));
            db.USER.Remove(uSER);
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
