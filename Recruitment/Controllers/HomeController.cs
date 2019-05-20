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
        public ActionResult Index()
        {
            return View();
        }

        [Route("Home")]
        public ActionResult PieChart()
        {
            RecruitmentEntities db = new RecruitmentEntities();

            List<MostPosition> Toppositions = new List<MostPosition>();//alltime result
            List<MostPosition> ToppositionMonths = new List<MostPosition>();//month result
            List<MostPosition> ToppositionYears = new List<MostPosition>();//year result

            List<DataPoint> dataPoints = new List<DataPoint>();//pie
            List<DataPoint> dataPointsBar = new List<DataPoint>();//bar
            List<DataPoint> dataPointsBar2 = new List<DataPoint>();//barsecond


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

                    dataPoints.Add(new DataPoint(dictnamajob[e.SOURCE_ID], dictnamajumlah[e.SOURCE_ID]));
                }
                catch (System.ArgumentException)
                {

                }



                POSITION posisi = db.POSITIONs.Where(l => l.POSITION_ID == e.JUDUL_POSISI).FirstOrDefault();
                int jumlahstatestock = db.CANDIDATEs.Where(l => l.STATE_ID == "ST000" || l.STATE_ID == "ST001").Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Count();//jumlah state dicandidate stock
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

            List<CANDIDATE> CandidatePras = db.CANDIDATEs.Where(l => l.STATE_ID == "ST000" || l.STATE_ID == "ST001").ToList();
            SearchTopJob(CandidatePras);//pra alltime
            List<MostPosition> Toppras = (List<MostPosition>)TempData.Peek("Top");
            MostPosition Toppra = Toppras.Where(l => l.moststate == "Pra-Selection" || l.moststate == "Pra-Selected").OrderByDescending(l => l.mostqty).FirstOrDefault();
            Toppositions.Add(Toppra);
            List<CANDIDATE> CandidateCalls = db.CANDIDATEs.Where(l => l.STATE_ID == "ST002").ToList();
            SearchTopJob(CandidateCalls);//call alltime
            List<MostPosition> Topcalls = (List<MostPosition>)TempData.Peek("Top");
            MostPosition Topcall = Topcalls.Where(l => l.moststate == "Call").OrderByDescending(l => l.mostqty).FirstOrDefault();
            Toppositions.Add(Topcall);

            TempData["topposition"] = Toppositions;//alltime

            List<CANDIDATE> CandidatePrasYears = db.CANDIDATEs.Where(l => l.STATE_ID == "ST001" || l.STATE_ID == "ST000").Where(l => l.DATETIME_CREATE.Year == DateTime.Now.Year).ToList();
            SearchTopJob(CandidatePrasYears);//pra years
            List<MostPosition> TopPraYears = (List<MostPosition>)TempData.Peek("Top");
            MostPosition TopPraYear = TopPraYears.Where(l => l.moststate == "Pra-Selection" || l.moststate == "Pra-Selected").OrderByDescending(l => l.mostqty).FirstOrDefault();
            ToppositionYears.Add(TopPraYear);
            List<CANDIDATE> CandidateCallsYears = db.CANDIDATEs.Where(l => l.STATE_ID == "ST002").Where(l => l.DATETIME_CREATE.Year == DateTime.Now.Year).ToList();
            SearchTopJob(CandidateCallsYears);//call years
            List<MostPosition> TopCallYears = (List<MostPosition>)TempData.Peek("Top");
            MostPosition TopCallYear = TopCallYears.Where(l => l.moststate == "Call").OrderByDescending(l => l.mostqty).FirstOrDefault();
            ToppositionYears.Add(TopCallYear);

            TempData["toppositionyear"] = ToppositionYears;//month


            List<CANDIDATE> CandidatePraMonths = db.CANDIDATEs.Where(l => l.DATETIME_CREATE.Year == DateTime.Now.Year).Where(l => l.DATETIME_CREATE.Month == DateTime.Now.Month).Where(l => l.STATE_ID == "ST000" || l.STATE_ID == "ST001").ToList();
            SearchTopJob(CandidatePraMonths);//Pra month
            List<MostPosition> TopPraMonths = (List<MostPosition>)TempData.Peek("Top");
            MostPosition TopPraMonth = TopPraMonths.Where(l => l.moststate == "Pra-Selection" || l.moststate == "Pra-Selected").OrderByDescending(l => l.mostqty).FirstOrDefault();
            ToppositionMonths.Add(TopPraYear);
            List<CANDIDATE> CandidateCallMonths = db.CANDIDATEs.Where(l => l.DATETIME_CREATE.Year == DateTime.Now.Year).Where(l => l.DATETIME_CREATE.Month == DateTime.Now.Month).Where(l => l.STATE_ID == "ST002").ToList();
            SearchTopJob(CandidateCallMonths);//Call month
            List<MostPosition> TopCallMonths = (List<MostPosition>)TempData.Peek("Top");
            MostPosition TopCallMonth = TopCallMonths.Where(l => l.moststate == "Call").OrderByDescending(l => l.mostqty).FirstOrDefault();
            ToppositionMonths.Add(TopCallYear);

            TempData["toppositionmonth"] = ToppositionMonths;//month



            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);//convert json
            ViewBag.DataPointsBar = JsonConvert.SerializeObject(dataPointsBar);//convert json
            ViewBag.DataPointsBar2 = JsonConvert.SerializeObject(dataPointsBar2);//convert json
            return View("Index");
        }


        

        public void SearchTopJob(List<CANDIDATE> candidates)//create list dto job
        {
            RecruitmentEntities db = new RecruitmentEntities();
            TempData["Top"] = 0;
            List<MostPosition> Mostposts = new List<MostPosition>();
            foreach (CANDIDATE e in candidates)
            {

                int jumlahposisiprayear = candidates.Where(l => l.JUDUL_POSISI == e.JUDUL_POSISI).Count();
                POSITION namaprayear = db.POSITIONs.Where(l => l.POSITION_ID == e.JUDUL_POSISI).FirstOrDefault();
                STATE namastateprayear = db.STATEs.Where(l => l.STATE_ID == e.STATE_ID).FirstOrDefault();
                MostPosition Mostpost = new MostPosition { mostqty = jumlahposisiprayear, mostposition = namaprayear.POSITION_NAME, moststate = namastateprayear.STATE_NAME };

                Mostposts.Add(Mostpost);


            }
            TempData["Top"] = Mostposts;

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
            if (ModelState.IsValid)
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
                        TempData["cek"] = "Password Berhasil Di Ubah";
                        return Redirect("~/Login");
                    }
                    else
                    {
                        TempData["validasi"] = "Password Tidak sesuai";
                        string url = "reset/" + reset.Username;
                        return Redirect(url);
                    }
                }
               
            }else
            {
                return View("ResetForm");
            }
            
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}