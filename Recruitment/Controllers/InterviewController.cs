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
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                List<CandidateInterviewDTO> CandidateInterview = db.CANDIDATEs.Join(db.POSITIONs,
                        candidate => candidate.JUDUL_POSISI,
                        position => position.POSITION_ID,
                        (candidate, position) => 
                            new CandidateInterviewDTO {
                                AppliedPosition = db.POSITIONs.FirstOrDefault(p => p.POSITION_ID == candidate.JUDUL_POSISI).POSITION_NAME,
                                SuitablePosition = db.POSITIONs.FirstOrDefault(p => p.POSITION_ID == candidate.SUITABLE_POSITION).POSITION_NAME,
                                
                            }
                ).ToList();
            } 
            return View("Index", candidates);
        }
    }
}