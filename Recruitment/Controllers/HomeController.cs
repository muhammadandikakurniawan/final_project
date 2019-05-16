using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recruitment.Models;
using System.Data.Entity;
using Newtonsoft.Json;

namespace Recruitment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home1
        public ActionResult Index()
        {
            return View();
        }

        [Route("Home")]
        public ActionResult PieChart()
        {
            RecruitmentEntities db = new RecruitmentEntities();
            List<MostPosition> mostpost = new List<MostPosition>();//alltime
            List<MostPosition> mostpostmonth = new List<MostPosition>();//month
            List<MostPosition> mostpostyear = new List<MostPosition>();//year
            List<MostPosition> topposition = new List<MostPosition>();//alltime result
            List<MostPosition> toppositionmonth = new List<MostPosition>();//month result
            List<MostPosition> toppositionyear = new List<MostPosition>();//year result

            List<DataPoint> dataPoints = new List<DataPoint>();//pie
            List<DataPoint> dataPointsBar = new List<DataPoint>();//bar
            List<DataPoint> dataPointsBar2 = new List<DataPoint>();//barsecond

            List<Dictionary<string, string>> namajobs = new List<Dictionary<string, string>>();
            Dictionary<string, string> dictnamajob = new Dictionary<string, string>();//dict namajob
            Dictionary<string, int> dictnamajumlah = new Dictionary<string, int>();//dict jumlahjob

            Dictionary<string, string> dictnamaposisi = new Dictionary<string, string>();//dict posisi
            Dictionary<string, int> dictjumlahposisi = new Dictionary<string, int>();//dict jumlah posisi keseluruhan

            Dictionary<string, int> dictjumlahposisistock = new Dictionary<string, int>();//dict stock
            Dictionary<string, int> dictjumlahposisicall = new Dictionary<string, int>();//dict call

            foreach (CANDIDATE e in db.CANDIDATEs)
            {

                int jumlah = db.CANDIDATEs.Where(l => l.SOURCE_ID == e.SOURCE_ID).Count();//ambiljumlah
                SOURCE jobsource = db.SOURCEs.Where(l => l.SOURCE_ID == e.SOURCE_ID).FirstOrDefault();//ambilnama


                try
                {
                    dictnamajumlah.Add(jobsource.SOURCE_ID, jumlah);
                    dictnamajob.Add(jobsource.SOURCE_ID, jobsource.SOURCE_NAME);
                    namajobs.Add(dictnamajob);
                    dataPoints.Add(new DataPoint(dictnamajob[e.SOURCE_ID], dictnamajumlah[e.SOURCE_ID]));
                }
                catch (System.ArgumentException)
                {

                }



                POSITION posisi = db.POSITIONs.Where(l => l.POSITION_ID == e.JUDUL_POSISI).FirstOrDefault();
                int jumlahstatestock = db.CANDIDATEs.Where(l => l.STATE_ID == "ST000"|| l.STATE_ID == "ST001").Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Count();//jumlah state dicandidate stock
                int jumlahstatecall = db.CANDIDATEs.Where(l => l.STATE_ID == "ST002").Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Count();//jumlah state dicandidate call
                CANDIDATE candidatecall = db.CANDIDATEs.Where(l => l.STATE_ID == "ST002").FirstOrDefault();
                CANDIDATE candidatepra = db.CANDIDATEs.Where(l => l.STATE_ID == "ST000" || l.STATE_ID == "ST001").FirstOrDefault();


                try
                {
                    dictjumlahposisistock.Add(posisi.POSITION_ID, jumlahstatestock);
                    dictjumlahposisicall.Add(posisi.POSITION_ID, jumlahstatecall);
                    dictnamaposisi.Add(posisi.POSITION_ID, posisi.POSITION_NAME);
                    dataPointsBar.Add(new DataPoint(dictnamaposisi[e.JUDUL_POSISI], dictjumlahposisistock[e.JUDUL_POSISI]));
                    dataPointsBar2.Add(new DataPoint(dictnamaposisi[e.JUDUL_POSISI], dictjumlahposisicall[e.JUDUL_POSISI]));
                }
                catch (System.ArgumentException)
                {

                }
                catch (System.NullReferenceException)
                {

                }


            }


            //var join = from c in candidates
            //           join st in states on c.stateid equals st.stateid into table2
            //           from st in table2.defaultifempty()
            //           join p in positions on c.judulposisi equals p.idposisi into table3
            //           from p in table3.defaultifempty()
            //           select new mostposition { mostcandidate=c,moststate=st,mostposition=p };


            //int jumlahposisi = db.CANDIDATEs.Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Count();
            //int jumlahposisiakhir= 0;
            IQueryable<CANDIDATE> candidatepras = db.CANDIDATEs.Where(l => l.STATE_ID == "ST000");
            foreach (CANDIDATE e in candidatepras)
            {

                //alltime


                int jumlahposisipra = candidatepras.Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Where(l => l.STATE_ID == "ST000").Count();

                POSITION namapra = db.POSITIONs.Where(l => l.POSITION_ID == e.JUDUL_POSISI).FirstOrDefault();

                STATE namastatepra = db.STATEs.Where(l => l.STATE_ID == e.STATE_ID).FirstOrDefault();


                MostPosition mostpra = new MostPosition { mostqty = jumlahposisipra, mostposition = namapra.POSITION_NAME, moststate = namastatepra.STATE_NAME };

                mostpost.Add(mostpra);



                //if (candidatecall.JUDUL_POSISI.Count() > e.JUDUL_POSISI.Count())
                //{

                //   jumlahposisiakhir = jumlahposisi;
                //}

                //int jumlahposisicall = join.Where(l => l.mostCandidate.JudulPosisi == e.JUDUL_POSISI).Count();
            }
            IQueryable<CANDIDATE> candidatecalls = db.CANDIDATEs.Where(l => l.STATE_ID == "ST002");
            foreach (CANDIDATE e in candidatecalls)
            {

                //alltime




                int jumlahposisicall = candidatecalls.Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Where(l => l.STATE_ID == "ST002").Count();

                POSITION namacall = db.POSITIONs.Where(l => l.POSITION_ID == e.JUDUL_POSISI).FirstOrDefault();

                STATE namastatecall = db.STATEs.Where(l => l.STATE_ID == e.STATE_ID).FirstOrDefault();

                MostPosition mostcall = new MostPosition { mostqty = jumlahposisicall, mostposition = namacall.POSITION_NAME, moststate = namastatecall.STATE_NAME };
                mostpost.Add(mostcall);


                //mostpost.Add(mostpra);




                //if (candidatecall.JUDUL_POSISI.Count() > e.JUDUL_POSISI.Count())
                //{

                //   jumlahposisiakhir = jumlahposisi;
                //}

                //int jumlahposisicall = join.Where(l => l.mostCandidate.JudulPosisi == e.JUDUL_POSISI).Count();
            }

            MostPosition topcall = mostpost.Where(l => l.moststate == "Call").OrderByDescending(l => l.mostqty).FirstOrDefault();
            MostPosition topstock = mostpost.Where(l => l.moststate == "Pra-Selection").OrderByDescending(l => l.mostqty).FirstOrDefault();
            topposition.Add(topcall);
            topposition.Add(topstock);
            TempData["topposition"] = topposition;//alltime


            List<CANDIDATE> candidatesmonthpra = db.CANDIDATEs.Where(l => l.DATETIME_CREATE.Year == DateTime.Now.Year && l.DATETIME_CREATE.Month == DateTime.Now.Month).Where(l => l.STATE_ID == "ST000").ToList();//filter month
            foreach (CANDIDATE e in candidatesmonthpra)
            {
                try
                {
                    //month


                    int jumlahposisipramonth = candidatesmonthpra.Where(l => l.STATE_ID == "ST000").Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Where(l => l.DATETIME_CREATE.Month == DateTime.Now.Month).Count();

                    POSITION namapramonth = db.POSITIONs.Where(l => l.POSITION_ID == e.JUDUL_POSISI).FirstOrDefault();

                    STATE namastatepramonth = db.STATEs.Where(l => l.STATE_ID == e.STATE_ID).FirstOrDefault();

                    MostPosition mostpramonth = new MostPosition { mostqty = jumlahposisipramonth, mostposition = namapramonth.POSITION_NAME, moststate = namastatepramonth.STATE_NAME };

                    mostpostmonth.Add(mostpramonth);

                }
                catch (ArgumentNullException)
                {

                }

            }
            List<CANDIDATE> candidatesmonthcall = db.CANDIDATEs.Where(l => l.DATETIME_CREATE.Year == DateTime.Now.Year && l.DATETIME_CREATE.Month == DateTime.Now.Month).Where(l => l.STATE_ID == "ST002").ToList();//filter month
            foreach (CANDIDATE e in candidatesmonthcall)
            {
                try
                {
                    //month


                    int jumlahposisicallmonth = candidatesmonthcall.Where(l => l.STATE_ID == "ST002").Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Where(l => l.DATETIME_CREATE.Month == DateTime.Now.Month).Count();

                    POSITION namacallmonth = db.POSITIONs.Where(l => l.POSITION_ID == e.JUDUL_POSISI).FirstOrDefault();

                    STATE namastatecallmonth = db.STATEs.Where(l => l.STATE_ID == e.STATE_ID).FirstOrDefault();

                    MostPosition mostcallmonth = new MostPosition { mostqty = jumlahposisicallmonth, mostposition = namacallmonth.POSITION_NAME, moststate = namastatecallmonth.STATE_NAME };

                    mostpostmonth.Add(mostcallmonth);
                }
                catch (ArgumentNullException)
                {

                }

            }
            var topcallmonth = mostpostmonth.Where(l => l.moststate == "Call").OrderByDescending(l => l.mostqty).FirstOrDefault();
            var topstockmonth = mostpostmonth.Where(l => l.moststate == "Pra-Selection").OrderByDescending(l => l.mostqty).FirstOrDefault();
            toppositionmonth.Add(topcallmonth);
            toppositionmonth.Add(topstockmonth);
            TempData["toppositionmonth"] = toppositionmonth;//month

            List<CANDIDATE> candidatesyearspra = db.CANDIDATEs.Where(l => l.DATETIME_CREATE.Year == DateTime.Now.Year).Where(l => l.STATE_ID == "ST000").ToList();//filter year
            foreach (CANDIDATE e in candidatesyearspra)
            {

                //year



                int jumlahposisiprayear = candidatesyearspra.Where(l => l.STATE_ID == "ST000").Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Where(l => l.DATETIME_CREATE.Year == DateTime.Now.Year).Count();

                POSITION namaprayear = db.POSITIONs.Where(l => l.POSITION_ID == e.JUDUL_POSISI).FirstOrDefault();

                STATE namastateprayear = db.STATEs.Where(l => l.STATE_ID == e.STATE_ID).FirstOrDefault();

                MostPosition mostprayear = new MostPosition { mostqty = jumlahposisiprayear, mostposition = namaprayear.POSITION_NAME, moststate = namastateprayear.STATE_NAME };


                mostpostyear.Add(mostprayear);


            }
            List<CANDIDATE> candidatesyearscall = db.CANDIDATEs.Where(l => l.DATETIME_CREATE.Year == DateTime.Now.Year).Where(l => l.STATE_ID == "ST002").ToList();//filter year
            foreach (CANDIDATE e in candidatesyearscall)
            {

                //year

                int jumlahposisicallyear = candidatesyearscall.Where(l => l.STATE_ID == "ST002").Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Where(l => l.DATETIME_CREATE.Year == DateTime.Now.Year).Count();

                POSITION namacallyear = db.POSITIONs.Where(l => l.POSITION_ID == e.JUDUL_POSISI).FirstOrDefault();

                STATE namastatecallyear = db.STATEs.Where(l => l.STATE_ID == e.STATE_ID).FirstOrDefault();

                MostPosition mostcallyear = new MostPosition { mostqty = jumlahposisicallyear, mostposition = namacallyear.POSITION_NAME, moststate = namastatecallyear.STATE_NAME };



                mostpostyear.Add(mostcallyear);


            }
            var topcallyear = mostpostyear.Where(l => l.moststate == "Call").OrderByDescending(l => l.mostqty).FirstOrDefault();
            var topstockyear = mostpostyear.Where(l => l.moststate == "Pra-Selection").OrderByDescending(l => l.mostqty).FirstOrDefault();
            toppositionyear.Add(topcallyear);
            toppositionyear.Add(topstockyear);
            TempData["toppositionyear"] = toppositionyear;//year

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.DataPointsBar = JsonConvert.SerializeObject(dataPointsBar);
            ViewBag.DataPointsBar2 = JsonConvert.SerializeObject(dataPointsBar2);
            return View("Index");
        }


        public ActionResult MenuLayout()
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {

                List<MenuModels> menu = recruitment.MENUs.Select(m =>
                new MenuModels
                {
                    MenuId = m.MENU_ID,
                    MenuName = m.MENU_NAME,
                    RoleId = m.ROLE_ID,
                    Action = m.ACTION,
                    Controller = m.CONTROLLER
                }).ToList();
                return PartialView("_MenuLayout", menu);
            }
        }

        [ActionName("reset")]
        [HttpGet]
        public ActionResult Reset(string id)
        {
            
             return View("ResetForm", new ResetViewModel {Username = id } );  
        }

        [ActionName("ResetPass")]
        [HttpPost]
        public ActionResult ResetPass(ResetViewModel reset)
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                USER uSER = db.USERs.Find(reset.Username);
                string pass = MD5Encryption.encryption(reset.Password);
                if (uSER.PASSWORD.Equals(pass))
                {
                    db.Entry(uSER).State = System.Data.Entity.EntityState.Modified;
                    string password = MD5Encryption.encryption(reset.NewPassword);
                    uSER.PASSWORD = password;
                    db.SaveChanges();
                    
                }
            }
            return Redirect("~/Login");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}