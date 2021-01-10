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
using System.Collections.Specialized;

namespace Poshta.Controllers
{
    [Authorize(Roles = "Operator")]
    public class NAKLADNAsController : Controller
    {
        private ModelDB db = new ModelDB();
        
        // GET: NAKLADNAs
        public async Task<ActionResult> Index()
        {
            var nAKLADNA = db.NAKLADNA.Include(n => n.CASTOMER).Include(n => n.CASTOMER1).Include(n => n.PACKAGE).Include(n => n.SECTION).Include(n => n.StanTransp).Include(n => n.USER);
            return View(await nAKLADNA.ToListAsync());
        }
        public int GETCost(string reg1, string reg2, string tow1, string tow2, double m)
        {
            Dictionary<string, Dictionary<string, int>> tarif_z = new Dictionary<string, Dictionary<string, int>>()
            {
                { "Вінницька", new Dictionary<string, int>() { {"Вінницька", 0 }, { "Волинська", 3 }, { "Дніпропетровська", 3 }, { "Донецька", 4 }, { "Житомирська", 1 }, { "Закарпатська", 3 },
                { "Запорізька", 3 }, { "Івано-Франківська", 3 }, { "Київська", 3 }, { "Кіровоградська", 3 }, { "Луганська", 4 }, { "Львівська", 3 }, { "Миколаївська", 3 }, { "Одеська", 3 },
                { "Полтавська", 3 }, { "Рівненська", 3 }, { "Сумська", 3 }, { "Тернопільська", 2 }, { "Харківська", 3 }, { "Херсонська", 3 }, { "Хмельницька", 2 }, { "Черкаська", 3 }, { "Чернівецька", 2 },
                { "Чернігівська", 3 }
                } },
                { "Волинська", new Dictionary<string, int>() { { "Волинська", 0 }, { "Дніпропетровська", 4 }, { "Донецька", 4 }, { "Житомирська", 2 }, { "Закарпатська", 3 },
                { "Запорізька", 4 }, { "Івано-Франківська", 3 }, { "Київська", 3 }, { "Кіровоградська", 3 }, { "Луганська", 4 }, { "Львівська", 1 }, { "Миколаївська", 4 }, { "Одеська", 4 },
                { "Полтавська", 4 }, { "Рівненська", 1 }, { "Сумська", 4 }, { "Тернопільська", 2 }, { "Харківська", 4 }, { "Херсонська", 4 }, { "Хмельницька", 2 }, { "Черкаська", 3 }, { "Чернівецька", 3 },
                { "Чернігівська", 3 }
                } },
                { "Дніпропетровська", new Dictionary<string, int>() {  { "Дніпропетровська", 0 }, { "Донецька", 2 }, { "Житомирська", 3 }, { "Закарпатська", 4 },
                { "Запорізька", 1 }, { "Івано-Франківська", 4 }, { "Київська", 3 }, { "Кіровоградська", 2 }, { "Луганська", 3 }, { "Львівська", 4 }, { "Миколаївська", 3 }, { "Одеська", 3 },
                { "Полтавська", 2 }, { "Рівненська", 4 }, { "Сумська", 3 }, { "Тернопільська", 4 }, { "Харківська", 2 }, { "Херсонська", 3 }, { "Хмельницька", 3 }, { "Черкаська", 3 }, { "Чернівецька", 4 },
                { "Чернігівська", 3 }
                } },
                { "Донецька", new Dictionary<string, int>() { { "Донецька", 0 }, { "Житомирська", 4 }, { "Закарпатська", 4 },
                { "Запорізька", 1 }, { "Івано-Франківська", 4 }, { "Київська", 4 }, { "Кіровоградська", 3 }, { "Луганська", 1 }, { "Львівська", 4 }, { "Миколаївська", 3 }, { "Одеська", 3 },
                { "Полтавська", 3 }, { "Рівненська", 4 }, { "Сумська", 3 }, { "Тернопільська", 4 }, { "Харківська", 3 }, { "Херсонська", 3 }, { "Хмельницька", 3 }, { "Черкаська", 3 }, { "Чернівецька", 4 },
                { "Чернігівська", 4 }
                } },
                { "Житомирська", new Dictionary<string, int>() { { "Житомирська", 0 }, { "Закарпатська", 3 },
                { "Запорізька", 3 }, { "Івано-Франківська", 3 }, { "Київська", 1 }, { "Кіровоградська", 3 }, { "Луганська", 4 }, { "Львівська", 3 }, { "Миколаївська", 3 }, { "Одеська", 3 },
                { "Полтавська", 3 }, { "Рівненська", 1 }, { "Сумська", 3 }, { "Тернопільська", 3 }, { "Харківська", 3 }, { "Херсонська", 3 }, { "Хмельницька", 2 }, { "Черкаська", 3 }, { "Чернівецька", 3 },
                { "Чернігівська", 2 }
                } },
                { "Закарпатська", new Dictionary<string, int>() { { "Закарпатська", 0 },
                { "Запорізька", 4 }, { "Івано-Франківська", 3 }, { "Київська", 4 }, { "Кіровоградська", 4 }, { "Луганська", 4 }, { "Львівська", 1 }, { "Миколаївська", 4 }, { "Одеська", 4 },
                { "Полтавська", 4 }, { "Рівненська", 3 }, { "Сумська", 4 }, { "Тернопільська", 3 }, { "Харківська", 4 }, { "Херсонська", 4 }, { "Хмельницька", 3 }, { "Черкаська", 4 }, { "Чернівецька", 3 },
                { "Чернігівська", 4 }
                } },
                { "Запорізька", new Dictionary<string, int>() { { "Запорізька", 0 }, { "Івано-Франківська", 4 }, { "Київська", 3 }, { "Кіровоградська", 3 }, { "Луганська", 3 }, { "Львівська", 4 }, { "Миколаївська", 3 }, 
                { "Одеська", 3 }, { "Полтавська", 2 }, { "Рівненська", 4 }, { "Сумська", 3 }, { "Тернопільська",4 }, { "Харківська",3 }, { "Херсонська",3 }, { "Хмельницька", 4 }, { "Черкаська", 3 }, { "Чернівецька", 4 },
                { "Чернігівська", 3 }
                } },
                { "Івано-Франківська", new Dictionary<string, int>() { { "Івано-Франківська", 0 }, { "Київська", 3 }, { "Кіровоградська", 3 }, { "Луганська", 4 }, { "Львівська", 1 }, { "Миколаївська", 4 },
                { "Одеська", 4 }, { "Полтавська", 4 }, { "Рівненська", 3 }, { "Сумська", 4 }, { "Тернопільська",1 }, { "Харківська",4 }, { "Херсонська",4 }, { "Хмельницька", 1 }, { "Черкаська", 4 }, { "Чернівецька", 1 },
                { "Чернігівська", 3 }
                } },
                { "Київська", new Dictionary<string, int>() { { "Київська", 0 }, { "Кіровоградська", 3 }, { "Луганська", 3 }, { "Львівська", 3 }, { "Миколаївська", 3 },
                { "Одеська",3 }, { "Полтавська", 3 }, { "Рівненська", 3 }, { "Сумська", 3 }, { "Тернопільська",3 }, { "Харківська",3 }, { "Херсонська",3 }, { "Хмельницька", 3 }, { "Черкаська", 1 }, { "Чернівецька", 3 },
                { "Чернігівська", 1 }
                } },
                { "Кіровоградська", new Dictionary<string, int>() { { "Кіровоградська", 0 }, { "Луганська", 3 }, { "Львівська", 4 }, { "Миколаївська", 2 },
                { "Одеська",3 }, { "Полтавська", 2 }, { "Рівненська", 3 }, { "Сумська", 3 }, { "Тернопільська",3 }, { "Харківська",3 }, { "Херсонська",2 }, { "Хмельницька", 3 }, { "Черкаська", 1 }, { "Чернівецька", 3 },
                { "Чернігівська", 3 }
                } },
                { "Луганська", new Dictionary<string, int>() { { "Луганська", 0 }, { "Львівська", 4 }, { "Миколаївська", 3 },
                { "Одеська",4 }, { "Полтавська", 3 }, { "Рівненська", 4 }, { "Сумська", 3 }, { "Тернопільська",4 }, { "Харківська",3 }, { "Херсонська",4 }, { "Хмельницька", 4 }, { "Черкаська", 3 }, { "Чернівецька", 4 },
                { "Чернігівська", 4 }
                } },
                { "Львівська", new Dictionary<string, int>() { { "Львівська", 0 }, { "Миколаївська", 4 },
                { "Одеська",4 }, { "Полтавська", 4 }, { "Рівненська", 1 }, { "Сумська", 4 }, { "Тернопільська",1 }, { "Харківська",4 }, { "Херсонська",4 }, { "Хмельницька", 2 }, { "Черкаська", 4 }, { "Чернівецька", 3 },
                { "Чернігівська", 3 }
                } },
                { "Миколаївська", new Dictionary<string, int>() { { "Миколаївська", 0 },
                { "Одеська",1 }, { "Полтавська", 3 }, { "Рівненська", 4 }, { "Сумська", 3 }, { "Тернопільська",3 }, { "Харківська",3 }, { "Херсонська",3 }, { "Хмельницька", 1 }, { "Черкаська", 3 }, { "Чернівецька", 4 },
                { "Чернігівська", 4 }
                } },
                { "Одеська", new Dictionary<string, int>() { { "Одеська",0 }, { "Полтавська", 3 }, { "Рівненська", 4 }, { "Сумська", 4 }, { "Тернопільська",4 }, { "Харківська",3 }, { "Херсонська",2 }, 
                { "Хмельницька", 4 }, { "Черкаська", 3 }, { "Чернівецька", 4 }, { "Чернігівська", 3 }
                } },
                { "Полтавська", new Dictionary<string, int>() {{ "Полтавська", 0 }, { "Рівненська", 2 }, { "Сумська", 1 }, { "Тернопільська",4 }, { "Харківська",1 }, { "Херсонська",3 },
                { "Хмельницька", 3 }, { "Черкаська", 1 }, { "Чернівецька", 4 }, { "Чернігівська", 3 }
                } },
                { "Рівненська", new Dictionary<string, int>() {{ "Рівненська", 0 }, { "Сумська", 3 }, { "Тернопільська",1 }, { "Харківська",4 }, { "Херсонська",4 },
                { "Хмельницька", 1 }, { "Черкаська", 3 }, { "Чернівецька", 3 }, { "Чернігівська", 3 }
                } },
                { "Сумська", new Dictionary<string, int>() {{ "Сумська", 0 }, { "Тернопільська",4 }, { "Харківська",1 }, { "Херсонська",3 }, { "Хмельницька", 3 }, { "Черкаська", 3 }, { "Чернівецька", 4 }, { "Чернігівська", 3 }
                } },
                { "Тернопільська", new Dictionary<string, int>() {{ "Тернопільська",0 }, { "Харківська",4 }, { "Херсонська",4 }, { "Хмельницька", 1 }, { "Черкаська", 3 }, { "Чернівецька", 1 }, { "Чернігівська", 3 }
                } },
                { "Харківська", new Dictionary<string, int>() {{ "Харківська",0 }, { "Херсонська",3 }, { "Хмельницька", 4 }, { "Черкаська", 3 }, { "Чернівецька", 4 }, { "Чернігівська", 3 }
                } },
                { "Херсонська", new Dictionary<string, int>() {{ "Херсонська",0 }, { "Хмельницька", 4 }, { "Черкаська", 3 }, { "Чернівецька", 4 }, { "Чернігівська", 3 }
                } },
                { "Хмельницька", new Dictionary<string, int>() {{ "Хмельницька", 0 }, { "Черкаська", 3 }, { "Чернівецька", 1 }, { "Чернігівська", 3 }
                } },
                { "Черкаська", new Dictionary<string, int>() {{ "Черкаська", 0 }, { "Чернівецька", 4 }, { "Чернігівська", 3 }
                } },
                { "Чернівецька", new Dictionary<string, int>() {{ "Чернівецька", 0 }, { "Чернігівська", 3 }
                } },
                { "Чернігівська", new Dictionary<string, int>() { { "Чернігівська", 0 } } }
            };
            int a = 0;
            int sum = 0;
            if (m > 30.0)
            {
                if (tarif_z.ContainsKey(reg1) && tarif_z[reg1].ContainsKey(reg2))
                {
                    sum = tarif_z[reg1][reg2];
                }
                else
                {
                    sum = tarif_z[reg2][reg1];
                }
            }
            else
            {
                if (m <= 0.5) { if (reg1 != reg2) { sum = 40; } else if (reg1 == reg2 && tow1 != tow2) { sum = 35; } else { sum = 30; } }
                else if (m <= 1) { if (reg1 != reg2) { sum = 45; } else if (reg1 == reg2 && tow1 != tow2) { sum = 40; } else { sum = 35; } }
                else if (m <= 2) { if (reg1 != reg2) { sum = 50; } else if (reg1 == reg2 && tow1 != tow2) { sum = 45; } else { sum = 40; } }
                else if (m <= 5) { if (reg1 != reg2) { sum = 55; } else if (reg1 == reg2 && tow1 != tow2) { sum = 50; } else { sum = 45; } }
                else if (m <= 10) { if (reg1 != reg2) { sum = 65; } else if (reg1 == reg2 && tow1 != tow2) { sum = 60; } else { sum = 55; } }
                else if (m <= 20) { if (reg1 != reg2) { sum = 85; } else if (reg1 == reg2 && tow1 != tow2) { sum = 80; } else { sum = 75; } }
                else if (m <= 30) { if (reg1 != reg2) { sum = 105; } else if (reg1 == reg2 && tow1 != tow2) { sum = 100; } else { sum = 95; } }
                else if (m > 30)
                {
                    if (reg1 != reg2)
                    {
                        if (tarif_z.ContainsKey(reg1) && tarif_z[reg1].ContainsKey(reg2))
                        {
                            a = tarif_z[reg1][reg2];
                        }
                        else
                        {
                            a = tarif_z[reg2][reg1];
                        }
                        sum = 105 + (int)Math.Round((m - 30) * a == 4 ? 6 : a == 3 ? 4.5 : a == 2 ? 3.5 : a == 1 ? 3 : 1, 0) ;
                    }
                    else if (reg1 == reg2 && tow1 != tow2) { sum = 100 + (int)Math.Round((m - 30)*2,0); }
                    else { sum = 95 + (int)Math.Round((m - 30) * 1.5, 0); }
                }
            }
            return sum;
            // GETCost ? reg1 = Дніпропетровська & reg2 = Кіровоградська
        }
        // GET: NAKLADNAs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NAKLADNA nAKLADNA = await db.NAKLADNA.FindAsync(id);
            if (nAKLADNA == null)
            {
                return HttpNotFound();
            }
            return View(nAKLADNA);
        }

        // GET: NAKLADNAs/Create
        public ActionResult Create()
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            var p = db.SectionPackage.ToList().Where(x => x.id_section == a && x.count != 0 && db.PACKAGE.Find(x.id_package).id_stan_p == 1).Select(x => new sklad { id = x.id_package, info = db.PACKAGE.Find(x.id_package).package_info, count = x.count });
            ViewBag.region = db.REGIONCollection;
            ViewBag.town = db.SECTION.Select(x => new RegionTown { id_region = x.id_region, town = x.town }).Distinct();
            ViewBag.nomer = db.SECTION.Select(x => new townNom { town = x.town, n_section = x.n_section });
            ViewBag.id_package = new SelectList(p, "id", "info");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void Edit_P(int? id_s, int? id_p)
        {
            var sectionPackage = db.SectionPackage.ToList().Where(x => x.id_package == id_p && x.id_section == id_s).FirstOrDefault();
            sectionPackage.count = sectionPackage.count-1;
            db.Entry(sectionPackage).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        // POST: NAKLADNAs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "phone_v,v_l,v_f,v_s,v_mail,phone_g,g_l,g_f,g_s,g_mail,id_region,town,nom,id_package,weight,descriotion,dop_info")] dopnakladna dopnakladna)
        {
            if (db.SECTION.Where(x => x.id_region == dopnakladna.id_region && x.town == dopnakladna.town && x.n_section == dopnakladna.nom).FirstOrDefault() != null)
            {
                if (db.SECTION.Where(x => x.id_region == dopnakladna.id_region && x.town == dopnakladna.town && x.n_section == dopnakladna.nom).FirstOrDefault().id_stan_s != 1)
                {
                    ModelState.AddModelError("id_region", "Відділення зачинене або на ремонті");
                }
            }
            else
            {
                ModelState.AddModelError("id_region", "Таке відділення не існує, введіть інші дані");
            }
            if (ModelState.IsValid)
            {
                NAKLADNA nAKLADNA = new NAKLADNA();
                nAKLADNA.phone_v = ADD_USER(new CASTOMER() { phone = dopnakladna.phone_v, first_name = dopnakladna.v_f, last_name = dopnakladna.v_l, surname = dopnakladna.v_s, mail = dopnakladna.v_mail }).phone;
                nAKLADNA.phone_g = ADD_USER(new CASTOMER() { phone = dopnakladna.phone_g, first_name = dopnakladna.g_f, last_name = dopnakladna.g_l, surname = dopnakladna.g_s, mail = dopnakladna.g_mail }).phone;
                nAKLADNA.id_section_g = db.SECTION.Where(x => x.id_region == dopnakladna.id_region && x.town == dopnakladna.town && x.n_section == dopnakladna.nom).Select(x => x.id_section).First();
                nAKLADNA.id_package = dopnakladna.id_package;
                var p = db.PACKAGE.Find(dopnakladna.id_package);
                nAKLADNA.weight = (p.p_width * p.p_length * p.p_height) / 4000.0 > dopnakladna.weight ? (decimal)((p.p_width * p.p_length * p.p_height) / 4000.0) : (decimal)dopnakladna.weight;
                nAKLADNA.descriotion = dopnakladna.descriotion;
                nAKLADNA.dop_info = dopnakladna.dop_info;
                var uz = db.USER.Find(Convert.ToInt32(User.Identity.Name));
                nAKLADNA.contact_number_v = uz.contact_number;
                nAKLADNA.pay = false;
                nAKLADNA.id_transort = 1;
                nAKLADNA.cost = GETCost(uz.SECTION.REGIONCollection.region, db.REGIONCollection.Find(dopnakladna.id_region).region, uz.SECTION.town, dopnakladna.town, (double)nAKLADNA.weight) + db.PACKAGE.Find(dopnakladna.id_package).cost;
                nAKLADNA.date = DateTime.Now;
                Edit_P(uz.id_section, dopnakladna.id_package);
                db.NAKLADNA.Add(nAKLADNA);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = db.NAKLADNA.ToList().Last().n_nakladna });
            }
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            var pa = db.SectionPackage.ToList().Where(x => x.id_section == a && x.count != 0 && db.PACKAGE.Find(x.id_package).id_stan_p == 1).Select(x => new sklad { id = x.id_package, info = db.PACKAGE.Find(x.id_package).package_info, count = x.count });
            ViewBag.region = db.REGIONCollection;
            ViewBag.town = db.SECTION.Select(x => new RegionTown { id_region = x.id_region, town = x.town }).Distinct();
            ViewBag.nomer = db.SECTION.Select(x => new townNom { town = x.town, n_section = x.n_section });
            ViewBag.id_package = new SelectList(pa, "id", "info", dopnakladna.id_package);
            return View(dopnakladna);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public CASTOMER ADD_USER(CASTOMER cASTOMER)
        {
            if(db.CASTOMER.Find(cASTOMER.phone) == null)
            {
                db.CASTOMER.Add(cASTOMER);
            }
            else if (db.CASTOMER.Find(cASTOMER.phone).Equals(cASTOMER))
            {
                return cASTOMER;
            }
            else
            {
                CASTOMER cASTOMER1 = db.CASTOMER.Find(cASTOMER.phone);
                cASTOMER1.last_name = cASTOMER.last_name;
                cASTOMER1.first_name = cASTOMER.first_name;
                cASTOMER1.last_name = cASTOMER.last_name;
                cASTOMER1.mail = cASTOMER.mail;
                db.Entry(cASTOMER1).State = EntityState.Modified;
            }
            db.SaveChanges();
            return cASTOMER;
        }
        // GET: NAKLADNAs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NAKLADNA nAKLADNA = await db.NAKLADNA.FindAsync(id);
            if (nAKLADNA == null)
            {
                return HttpNotFound();
            }   
            ViewBag.id_transort = new SelectList(db.StanTransp, "id_transort", "transport_info", nAKLADNA.id_transort);
            return View(nAKLADNA);
        }

        // POST: NAKLADNAs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "n_nakladna,phone_v,v_l,v_f,v_s,v_mail,phone_g,g_l,g_f,g_s,g_mail,descriotion,dop_info,id_transort,")] dopnakladna dopnakladna)
        {
            if (dopnakladna.phone_v == null || dopnakladna.v_l == null || dopnakladna.v_f == null || dopnakladna.v_l == null || dopnakladna.v_l.Length > 20 || dopnakladna.v_f.Length > 15 || dopnakladna.v_s.Length > 20 || (dopnakladna.v_mail == null ? 0 : dopnakladna.v_mail.Length) > 35 || dopnakladna.phone_v.Length != 10)
            {
                ModelState.AddModelError("CASTOMER.last_name", "Поля пусті або неправильно заповнені тому встановлене попереднє значення. Помилки: " +
                    (dopnakladna.phone_v == null ? "Телефон пустий; " : dopnakladna.phone_v.Length != 10 ? "Строка 'Телефон' має бути довжиною в 10 символів; " : "") +
                    (dopnakladna.v_l == null ? "Прізвище пусте; " : dopnakladna.v_l.Length > 20 ? "Перемищено ліміт символів(20) строка 'Прізвище'; " : "") +
                    (dopnakladna.v_f == null ? "Ім'я пусте; " : dopnakladna.v_f.Length > 15 ? "Перемищено ліміт символів(15) строка 'Ім'я'; " : "") +
                    (dopnakladna.v_s == null ? "По батькові пусте; " : dopnakladna.v_s.Length > 20 ? "Перемищено ліміт символів(20) строка 'По батькові'; " : "") +
                    (dopnakladna.v_mail.Length > 35 ? "Перемищено ліміт символів(35) строка 'Почта'; " : "")

                    );
            }
            if (dopnakladna.phone_g == null || dopnakladna.g_l == null || dopnakladna.g_f == null || dopnakladna.g_l == null || dopnakladna.g_l.Length > 20 || dopnakladna.g_f.Length > 15 || dopnakladna.g_s.Length > 20 || (dopnakladna.g_mail == null ? 0 : dopnakladna.g_mail.Length) > 35 || dopnakladna.phone_g.Length != 10)
            {
                ModelState.AddModelError("CASTOMER1.last_name", "Поля пусті або неправильно заповнені тому встановлене попереднє значення. Помилки: " +
                    (dopnakladna.phone_g == null ? "Телефон пустий; " : dopnakladna.phone_g.Length != 10 ? "Строка має бути довжиною в 10 символів " : "") +
                    (dopnakladna.g_l == null ? "Прізвище пусте; " : dopnakladna.g_l.Length > 20 ? "Перемищено ліміт символів(20) строка 'Прізвище'; " : "") +
                    (dopnakladna.g_f == null ? "Ім'я пусте; " : dopnakladna.g_f.Length > 15 ? "Перемищено ліміт символів(15) строка 'Ім'я'; " : "") +
                    (dopnakladna.g_s == null ? "По батькові пусте; " : dopnakladna.g_s.Length > 20 ? "Перемищено ліміт символів(20) строка 'По батькові'; " : "") +
                    (dopnakladna.g_mail.Length > 35 ? "Перемищено ліміт символів(35) строка 'Почта'; " : "")

                    );
            }
            NAKLADNA nAKLADNA = db.NAKLADNA.Find(dopnakladna.n_nakladna);
            nAKLADNA.descriotion = dopnakladna.descriotion;
            nAKLADNA.dop_info = dopnakladna.dop_info;
            nAKLADNA.id_transort = dopnakladna.id_transort;

            if (ModelState.IsValid)
            {
                nAKLADNA.phone_v = ADD_USER(new CASTOMER() { phone = dopnakladna.phone_v, first_name = dopnakladna.v_f, last_name = dopnakladna.v_l, surname = dopnakladna.v_s, mail = dopnakladna.v_mail }).phone;
                nAKLADNA.phone_g = ADD_USER(new CASTOMER() { phone = dopnakladna.phone_g, first_name = dopnakladna.g_f, last_name = dopnakladna.g_l, surname = dopnakladna.g_s, mail = dopnakladna.g_mail }).phone;
                db.Entry(nAKLADNA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_transort = new SelectList(db.StanTransp, "id_transort", "transport_info", nAKLADNA.id_transort);
            return View(nAKLADNA);
        }

        // GET: NAKLADNAs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            NAKLADNA nAKLADNA = await db.NAKLADNA.FindAsync(id);
            db.NAKLADNA.Remove(nAKLADNA);
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
