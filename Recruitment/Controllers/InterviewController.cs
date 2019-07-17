using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recruitment.Models;

namespace Recruitment.Controllers
{
    public class InterviewController : Controller
    {
        [Route("interview")]
        public ActionResult Index()
        {
            RecruitmentEntities db = new RecruitmentEntities();
            List<CandidateCallDTO> candidates = (from c in db.CANDIDATEs
                          join s in db.SOURCEs on c.SOURCE_ID equals s.SOURCE_ID into table1
                          from s in table1.DefaultIfEmpty()
                          join u in db.USERs on c.USER_CREATE equals u.USERNAME into table2
                          from u in table2.DefaultIfEmpty()
                          join st in db.STATEs on c.STATE_ID equals st.STATE_ID into table3
                          from st in table3.DefaultIfEmpty()
                          join p in db.POSITIONs on c.JUDUL_POSISI equals p.POSITION_ID into table4
                          from p in table4.DefaultIfEmpty()
                          where c.STATE_ID == "ST003"
                          select new CandidateCallDTO
                          {
                              CandidateId = c.CANDIDATE_ID,
                              Name = c.NAMA_LENGKAP,
                              Position = p.POSITION_NAME,
                              Source = s.SOURCE_NAME,
                              Phone = c.NOHP,
                              Email = c.EMAIL,
                              PreSelectPIC = u.FULLNAME,
                              State = st.STATE_NAME,
                              Notes = c.CATATAN,
                              ExpectedSalary = c.EXPECTED_SALARY,
                              AvailableJoin = (DateTime)c.AVAIABLE_JOIN,
                              SuitablePosition = c.SUITABLE_POSITION
                          }).ToList();

            return View("Index", candidates);
        }
    }
}