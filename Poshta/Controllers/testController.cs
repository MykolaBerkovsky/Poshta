using Poshta.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;
using System.Drawing;
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace Poshta.Controllers
{
    [Authorize(Roles = "Manager, Operator")]
    public class testController : Controller
    {
        private ModelDB db = new ModelDB();

        public ActionResult SendM(int id, bool chek)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("nikola04060206@gmail.com", "Kolya");
            var text = db.NAKLADNA.Where(x => x.n_nakladna == id).ToList().First();
            // кому отправляем
            MailAddress to;
            if (chek)
            {
                to = new MailAddress(text.CASTOMER.mail);
            }
            else
            {
                to = new MailAddress(text.CASTOMER1.mail);
            }
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Посилка";
            // текст письма
            
            m.Body = "<h2>Накладна №" + text.n_nakladna + "</h2>" +
                "<p>ВІДПРАВНИК</p>" +
                "<p>" + text.CASTOMER.first_name + " " + text.CASTOMER.last_name + " " + text.CASTOMER.surname + "</p>" +
                "<p>" + "м." + text.USER.SECTION.town + ", ВІДІЛЕННЯ № " + text.USER.SECTION.n_section + " ," + text.USER.SECTION.REGIONCollection.region + " ОБЛ." + "</p>" +
                "<p>" + text.CASTOMER.phone + "</p>" +
                "<p>Отримувач</p>" +
                "<p>" + text.CASTOMER1.first_name + " " + text.CASTOMER1.last_name + " " + text.CASTOMER1.surname + "</p>" +
                "<p>" + "м." + text.SECTION.town + ", ВІДІЛЕННЯ № " + text.SECTION.n_section + " ," + text.SECTION.REGIONCollection.region + " ОБЛ." + "</p>" +
                "<p>" + text.CASTOMER.phone + "</p>" +
                "<p>" + "Вага: " + text.weight + " кг." + "</p>" +
                "<p>" + "Опис: " + text.descriotion + "</p>" +
                "<p>" + "Додаткова інформація: " + text.dop_info + "</p>" +
                 "<p>" + "Оплата " + text.cost + " Стан оплати " + (text.pay == true ? "Оплачено" : "Не оплачено") + "</p>" +
                 "<p>" + "Стан транспонтування: " + text.StanTransp.transport_info  + "</p>";
                ;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("nikola04060206@gmail.com", "Linkoln103ainz");
            smtp.EnableSsl = true;
            smtp.Send(m);
            return RedirectToAction("Details", "NAKLADNAs", new { id = id });
        }

        [HttpGet]
        public ActionResult GETWORD(int id)
        {
            var text = db.NAKLADNA.Include(n => n.CASTOMER).Include(n => n.CASTOMER1).Include(n => n.PACKAGE).Include(n => n.SECTION).Include(n => n.StanTransp).Include(n => n.USER).Where(x => x.n_nakladna == id).ToList().First();
            MemoryStream stream = new MemoryStream();
            DocX doc = DocX.Create(stream);

            Paragraph par = doc.InsertParagraph();
            par.Append("ПОШТА").FontSize(32).Font(new Xceed.Document.NET.Font("Courier New")).Alignment = Alignment.center;
            par.AppendLine();
            par.Append("ЕКСПЕРС-НАКЛАДНА №").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append(text.n_nakladna.ToString()).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20).Bold();
            par.AppendLine();
            par.Append("Дата оформлення " + text.date.ToShortDateString()).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append("____________________________________").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append("ВІДПРАВНИК").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append(text.CASTOMER.first_name + " " + text.CASTOMER.last_name + " " + text.CASTOMER.surname).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append("м." + text.USER.SECTION.town + ", ВІДІЛЕННЯ № " + text.USER.SECTION.n_section + " ," + text.USER.SECTION.REGIONCollection.region + " ОБЛ.").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append(text.CASTOMER.phone).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append("____________________________________").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append("ОДЕРЖУВАЧ").FontSize(20).Font(new Xceed.Document.NET.Font("Courier New"));
            par.AppendLine();
            par.Append(text.CASTOMER1.first_name + " " + text.CASTOMER1.last_name + " " + text.CASTOMER1.surname).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append("м." + text.SECTION.town + ", ВІДІЛЕННЯ № " + text.SECTION.n_section + " ," + text.SECTION.REGIONCollection.region + " ОБЛ.").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append(text.CASTOMER1.phone).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append("____________________________________").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append("Вага: ").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.Append(text.weight + " кг.").FontSize(20).Font(new Xceed.Document.NET.Font("Courier New"));
            par.AppendLine();
            par.Append("Опис: " + text.descriotion).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append("Додаткова інформація: " + text.dop_info).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            par.Append("____________________________________").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            par.AppendLine();
            if (text.pay == true)
            {
                par.Append("Оплачено:" + text.cost).FontSize(20).Font(new Xceed.Document.NET.Font("Courier New")); ;
                par.AppendLine();
            }
            else
            {
                par.Append("Оплачує отримувач").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
                par.AppendLine();
                par.Append("Вартість: ").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
                par.Append(text.cost.ToString()).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20).Bold();
                par.AppendLine();
            }
            par.Append("____________________________________").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20); ;
            par.AppendLine();
            par.Append("Представник ТОВ 'Пошта'").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20); ;
            par.AppendLine();
            par.Append(text.USER.first_name + " " + text.USER.last_name + " " + text.USER.surname).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20); ;
            par.AppendLine();
            par.Append(text.date.ToString()).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
            
            doc.Save();

            return File(stream.ToArray(), "application/octet-stream", "Накладна.docx");
        }

        [HttpGet]
        public ActionResult GETPAY(int GetMoney, int id)
        {
            var text = db.NAKLADNA.Where(x => x.n_nakladna == id).ToList().First();
            MemoryStream stream = new MemoryStream();
            DocX doc = DocX.Create(stream);
            Paragraph par = doc.InsertParagraph();
            if (text.cost > GetMoney)
            {
                par.Append("Недостатня сума для оплати").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20).Alignment = Alignment.center;
            }
            else
            {
                DateTime date = DateTime.Today;
                par.Append("ТОВ 'ПОСТ ФІНАНС'").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20).Alignment = Alignment.center;
                par.AppendLine();
                par.Append("м." + text.USER.SECTION.town + ", ВІДІЛЕННЯ № " + text.USER.SECTION.n_section + " ," + text.USER.SECTION.REGIONCollection.region + " ОБЛ.").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
                par.AppendLine();
                par.Append("Накладна № " + text.n_nakladna).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
                par.AppendLine();
                par.Append("____________________________________").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
                par.AppendLine();
                par.Append("Сума: " + text.cost).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
                par.AppendLine();
                par.Append("____________________________________").Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
                par.AppendLine();
                par.Append("Готівка      " + GetMoney).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
                par.AppendLine();
                par.Append(("Решта      " + (GetMoney - text.cost))).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
                par.AppendLine();
                par.Append(DateTime.Now.ToString()).Font(new Xceed.Document.NET.Font("Courier New")).FontSize(20);
                text.pay = true;
                db.Entry(text).State = EntityState.Modified;
                db.SaveChanges();
            }
            doc.Save();

            return File(stream.ToArray(), "application/octet-stream", "Оплата.docx");

        }
    }
}