using Poshta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ClosedXML.Excel;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace Poshta.Controllers
{
    [Authorize(Roles = "Manager")]
    public class MENEGERController : Controller
    {
        private ModelDB db = new ModelDB();
        public ActionResult Zapit1()
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            DateTime date = DateTime.Today;
            var result = from NAKLADNA in db.NAKLADNA
                         join USER in db.USER on NAKLADNA.contact_number_v equals USER.contact_number
                         join PACKAGE in db.PACKAGE on NAKLADNA.id_package equals PACKAGE.id_package
                         where NAKLADNA.USER.id_section == a && NAKLADNA.date >= date
                         group NAKLADNA by new { NAKLADNA.PACKAGE.package_info, NAKLADNA.PACKAGE.cost } into g
                         select new Zaput1 { pakage_info = g.Key.package_info, cost = g.Key.cost, count = g.Count() };
            return View(result.ToList());
        }
        public ActionResult Zapit1Exel()
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            DateTime date = DateTime.Today;
            var result = from NAKLADNA in db.NAKLADNA
                         join USER in db.USER on NAKLADNA.contact_number_v equals USER.contact_number
                         join PACKAGE in db.PACKAGE on NAKLADNA.id_package equals PACKAGE.id_package
                         where NAKLADNA.USER.id_section == a && NAKLADNA.date >= date
                         group NAKLADNA by new { NAKLADNA.PACKAGE.package_info, NAKLADNA.PACKAGE.cost } into g
                         select new Zaput1 { pakage_info = g.Key.package_info, cost = g.Key.cost, count = g.Count() };
            using(var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Запит1");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Назва пакування";
                worksheet.Cell(currentRow, 2).Value = "Вартість";
                worksheet.Cell(currentRow, 3).Value = "Кількість";
                foreach(var el in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = el.pakage_info;
                    worksheet.Cell(currentRow, 2).Value = el.cost;
                    worksheet.Cell(currentRow, 3).Value = el.count;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Zvit1"+ date + ".xlsx"
                        );
                }                                                                                                                                           
            }
        }
        public ActionResult Zapit2()
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            DateTime date = DateTime.Today;
            var result = from NAKLADNA in db.NAKLADNA
                         join USER in db.USER on NAKLADNA.contact_number_v equals USER.contact_number
                         where NAKLADNA.USER.id_section == a && NAKLADNA.date >= date
                         select new Zaput2 { nomer = NAKLADNA.n_nakladna, cost = NAKLADNA.cost, pay = NAKLADNA.pay };
            ViewBag.resultSum = result.Select(x => x.cost).DefaultIfEmpty(0).Sum();
            return View(result.ToList());
        }
        public ActionResult Zapit2Exel()
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            DateTime date = DateTime.Today;
            var result = from NAKLADNA in db.NAKLADNA
                         join USER in db.USER on NAKLADNA.contact_number_v equals USER.contact_number
                         where NAKLADNA.USER.id_section == a && NAKLADNA.date >= date
                         select new Zaput2 { nomer = NAKLADNA.n_nakladna, cost = NAKLADNA.cost, pay = NAKLADNA.pay };
            var ressum = result.Select(x => x.cost).DefaultIfEmpty(0).Sum();
            ViewBag.resultSum = ressum;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Запит2");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Номер накладної";
                worksheet.Cell(currentRow, 2).Value = "Вартість";
                worksheet.Cell(currentRow, 3).Value = "Оплата";
                foreach (var el in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = el.nomer;
                    worksheet.Cell(currentRow, 2).Value = el.cost;
                    worksheet.Cell(currentRow, 3).Value = el.pay==true ? "Оплачено" : "Не оплачено";
                }
                worksheet.Cell(currentRow + 1, 1).Value = "Загальна сума: " + ressum;
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Zvit2" + date + ".xlsx"
                        );
                }
            }
        }
        public ActionResult Zapit3()
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            DateTime date = DateTime.Today;
            var result = from NAKLADNA in db.NAKLADNA
                         join USER in db.USER on NAKLADNA.contact_number_v equals USER.contact_number
                         where NAKLADNA.USER.id_section == a && NAKLADNA.date >= date
                         group NAKLADNA by new { NAKLADNA.CASTOMER.last_name, NAKLADNA.CASTOMER.first_name, NAKLADNA.CASTOMER.surname, NAKLADNA.CASTOMER.phone } into g
                         select new Zaput3 { last_name = g.Key.last_name, first_name = g.Key.first_name, surname = g.Key.surname, phone = g.Key.phone, count = g.Count() };
            return View(result.ToList());
        }
        public ActionResult Zapit3Exel()
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            DateTime date = DateTime.Today;
            var result = from NAKLADNA in db.NAKLADNA
                         join USER in db.USER on NAKLADNA.contact_number_v equals USER.contact_number
                         where NAKLADNA.USER.id_section == a && NAKLADNA.date >= date
                         group NAKLADNA by new { NAKLADNA.CASTOMER.last_name, NAKLADNA.CASTOMER.first_name, NAKLADNA.CASTOMER.surname, NAKLADNA.CASTOMER.phone } into g
                         select new Zaput3 { last_name = g.Key.last_name, first_name = g.Key.first_name, surname = g.Key.surname, phone = g.Key.phone, count = g.Count() };
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Запит3");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Прізвище";
                worksheet.Cell(currentRow, 2).Value = "Ім'я";
                worksheet.Cell(currentRow, 3).Value = "По батькові";
                worksheet.Cell(currentRow, 4).Value = "Телефон";
                worksheet.Cell(currentRow, 5).Value = "Кількість";
                foreach (var el in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = el.last_name;
                    worksheet.Cell(currentRow, 2).Value = el.first_name;
                    worksheet.Cell(currentRow, 3).Value = el.surname;
                    worksheet.Cell(currentRow, 4).Value = el.phone;
                    worksheet.Cell(currentRow, 5).Value = el.count;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Zvit3" + date + ".xlsx"
                        );
                }
            }
        }
        public ActionResult Zapit4(string phone)
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            //var dop = db.NAKLADNA.ToList().ForEach(x => x.date);
            var result = from NAKLADNA in db.NAKLADNA.ToList()
                         join USER in db.USER on NAKLADNA.contact_number_v equals USER.contact_number
                         where NAKLADNA.USER.id_section == a && NAKLADNA.phone_v == phone
                         group NAKLADNA by new { DateTime = Convert.ToDateTime(NAKLADNA.date.ToShortDateString()) } into g
                         select new Zaput4 { date = g.Key.DateTime, count = g.Count() };
            ViewBag.clientP = phone;
            var c = db.CASTOMER.Where(x => x.phone == phone);
            if(c.FirstOrDefault() == null)
            {
                return HttpNotFound();
            }
            ViewBag.client = c.Select(x => string.Concat(x.last_name, " ", x.first_name, " ", x.surname, " ", x.phone)).ToList().First();
            return View(result.ToList());
        }
        public ActionResult Zapit4Exel(string phone)
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            var result = from NAKLADNA in db.NAKLADNA.ToList()
                         join USER in db.USER on NAKLADNA.contact_number_v equals USER.contact_number
                         where NAKLADNA.USER.id_section == a && NAKLADNA.phone_v == phone
                         group NAKLADNA by new { DateTime = Convert.ToDateTime(NAKLADNA.date.ToShortDateString()) } into g
                         select new Zaput4 { date = g.Key.DateTime, count = g.Count() };
            var client = db.CASTOMER.Where(x => x.phone == phone).Select(x => string.Concat(x.last_name, " ", x.first_name, " ", x.surname, " ", x.phone)).ToList().First();
            ViewBag.client = client;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Запит3");
                var currentRow = 2;
                worksheet.Cell(1, 1).Value = client;
                worksheet.Cell(currentRow, 1).Value = "Дата";
                worksheet.Cell(currentRow, 2).Value = "Кількість";
                foreach (var el in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = el.date.ToString("dd/MM/yyyy");
                    worksheet.Cell(currentRow, 2).Value = el.count;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Zvit4.xlsx"
                        );
                }
            }
        }
        public ActionResult G()
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            DateTime now = DateTime.Today;
            bool z = (double)now.Day / DateTime.DaysInMonth(now.Year, now.Month) >= 0.9;
            var use_2_last_month = from PACKAGE in db.PACKAGE.ToList()
                                   join NAKLADNA in db.NAKLADNA.ToList().Where(x => x.USER.id_section == a && x.date < now.AddDays(-DateTime.Now.Day + 1) && x.date > now.AddDays(-DateTime.Now.Day + 1).AddMonths(-2)) on PACKAGE.id_package equals NAKLADNA.id_package into gj
                                   from subpet in gj.DefaultIfEmpty()
                                   select new { id = PACKAGE.id_package, p_i = PACKAGE.package_info, n = (subpet == null ? null : subpet.USER.first_name), date = (subpet == null ? null : subpet.date.AddDays(-subpet.date.Day + 1).ToShortDateString()) } into stat
                                   group stat by new { stat.id, stat.p_i } into g
                                   select new { id = g.Key.id, info = g.Key.p_i, count_use = g.Count(x => x.n != null), count_month = g.Select(x => new { d = x.date }).Distinct().Count(x => x.d != null) } into ok
                                   where ok.count_month != 0
                                   select new { id = ok.id, info = ok.info, count = (int)Math.Floor(ok.count_use / (double)ok.count_month) };
            var sklad = (from SectionPackage in db.SectionPackage.Where(x => x.id_section == a).ToList()
                          select new { id = SectionPackage.id_package, info = db.PACKAGE.Find(SectionPackage.id_package).package_info, count = SectionPackage.count }).ToList();
            List<sklad> zakup = new List<sklad>(); //Результат
            foreach(int i in sklad.Select(x => x.id))
            {
                var _2m = use_2_last_month.Where(x => x.id == i).FirstOrDefault();
                var s = sklad.Where(x => x.id == i).FirstOrDefault();
                if (_2m == null)
                {
                    continue;
                }
                else if (db.PACKAGE.Find(i).id_stan_p == 2)
                {
                    continue;
                }
                else if ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month) < 0.2)
                {
                    zakup.Add(new sklad() { id = i, info = _2m.info, count = ((int)(Math.Round(_2m.count * (double)now.Day / DateTime.DaysInMonth(now.Year, now.Month), 0))) });
                }
                else if (s.count == 0)
                {
                    if ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month) < 0.25)
                    {
                        zakup.Add(new sklad() { id = i, info = _2m.info, count = ((int)Math.Round(_2m.count * (1 - (double)now.Day / DateTime.DaysInMonth(now.Year, now.Month) + 0.3), 0) + (z ? _2m.count : 0)) });
                    }
                    else if((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month) < 0.5)
                    {
                        zakup.Add(new sklad() { id = i, info = _2m.info, count = ((int)(Math.Round(_2m.count * (1 - ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month)) + 0.15), 0)) + (z ? _2m.count : 0)) });
                    }
                    else
                    {

                        zakup.Add(new sklad() { id = i, info = _2m.info, count = ((int)(Math.Round(_2m.count * (1 - ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month))), 0)) + (z ? _2m.count : 0)) });
                    }
                }
                else if (s.count >= (int)(Math.Round(_2m.count * (1 - (double)now.Day / DateTime.DaysInMonth(now.Year, now.Month)))))
                {
                    zakup.Add(new sklad() { id = i, info = _2m.info, count = 0 + (z ? _2m.count : 0) });
                }
                else
                {
                    zakup.Add(new sklad() { id = i, info = _2m.info, count = ((int)(Math.Round(_2m.count * (1 - ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month))), 0))) });
                }
            }
            return View(zakup);
        }

        public ActionResult SendZ()
        {
            var id = Convert.ToInt32(User.Identity.Name.ToString());
            var a = db.USER.Where(x => x.contact_number == id).Select(x => x.id_section).First();
            DateTime now = DateTime.Today;
            bool z = (double)now.Day / DateTime.DaysInMonth(now.Year, now.Month) >= 0.9;
            var use_2_last_month = from PACKAGE in db.PACKAGE.ToList()
                                   join NAKLADNA in db.NAKLADNA.ToList().Where(x => x.USER.id_section == a && x.date < now.AddDays(-DateTime.Now.Day + 1) && x.date > now.AddDays(-DateTime.Now.Day + 1).AddMonths(-2)) on PACKAGE.id_package equals NAKLADNA.id_package into gj
                                   from subpet in gj.DefaultIfEmpty()
                                   select new { id = PACKAGE.id_package, p_i = PACKAGE.package_info, n = (subpet == null ? null : subpet.USER.first_name), date = (subpet == null ? null : subpet.date.AddDays(-subpet.date.Day + 1).ToShortDateString()) } into stat
                                   group stat by new { stat.id, stat.p_i } into g
                                   select new { id = g.Key.id, info = g.Key.p_i, count_use = g.Count(x => x.n != null), count_month = g.Select(x => new { d = x.date }).Distinct().Count(x => x.d != null) } into ok
                                   where ok.count_month != 0
                                   select new { id = ok.id, info = ok.info, count = (int)Math.Floor(ok.count_use / (double)ok.count_month) };
            var sklad = (from SectionPackage in db.SectionPackage.Where(x => x.id_section == a).ToList()
                         select new { id = SectionPackage.id_package, info = db.PACKAGE.Find(SectionPackage.id_package).package_info, count = SectionPackage.count }).ToList();
            List<sklad> zakup = new List<sklad>(); //Результат
            foreach (int i in sklad.Select(x => x.id))
            {
                var _2m = use_2_last_month.Where(x => x.id == i).FirstOrDefault();
                var s = sklad.Where(x => x.id == i).FirstOrDefault();
                if (_2m == null)
                {
                    continue;
                }
                if (db.PACKAGE.Find(i).id_stan_p == 2)
                {
                    continue;
                }
                if ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month) < 0.2)
                {
                    zakup.Add(new sklad() { id = i, info = _2m.info, count = ((int)(Math.Round(_2m.count * (double)now.Day / DateTime.DaysInMonth(now.Year, now.Month), 0))) });
                }
                if (s.count == 0)
                {
                    if ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month) < 0.25)
                    {
                        zakup.Add(new sklad() { id = i, info = _2m.info, count = ((int)Math.Round(_2m.count * (1 - (double)now.Day / DateTime.DaysInMonth(now.Year, now.Month) + 0.3), 0) + (z ? _2m.count : 0)) });
                    }
                    else if ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month) < 0.5)
                    {
                        zakup.Add(new sklad() { id = i, info = _2m.info, count = ((int)(Math.Round(_2m.count * (1 - ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month)) + 0.15), 0)) + (z ? _2m.count : 0)) });
                    }
                    else
                    {
                        zakup.Add(new sklad() { id = i, info = _2m.info, count = ((int)(Math.Round(_2m.count * (1 - ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month))), 0)) + (z ? _2m.count : 0)) });
                    }
                }
                else if (s.count >= (int)(Math.Round(_2m.count * (1 - (double)now.Day / DateTime.DaysInMonth(now.Year, now.Month)))))
                {
                    zakup.Add(new sklad() { id = i, info = _2m.info, count = 0 + (z ? _2m.count : 0) });
                }
                else
                {
                    zakup.Add(new sklad() { id = i, info = _2m.info, count = ((int)(Math.Round(_2m.count * (1 - ((double)now.Day / DateTime.DaysInMonth(now.Year, now.Month))), 0))) });
                }
            }
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("nikola04060206@gmail.com", "Viddil");
            // кому отправляем
            MailAddress to = new MailAddress("nomvodafon@gmail.com");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Закупка пакуваання";
            // текст письма
            var vid = db.USER.Where(x => x.contact_number == id).First();
            m.Body = "<h2>Відділення № "+ vid.SECTION.n_section +" Місто "+ vid.SECTION.town + " Область " + vid.SECTION.REGIONCollection.region + "</h2>";
            foreach(var i in zakup)
            {
                m.Body += "<p>"+ i.info + " Кількість " + i.count +"</p>";
            }
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("nikola04060206@gmail.com", "Linkoln103ainz");
            smtp.EnableSsl = true;
            smtp.Send(m);
            return RedirectToAction("G", "MENEGER");
        }
    }
}