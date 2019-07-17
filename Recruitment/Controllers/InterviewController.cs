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
                                Name = candidate.NAMA_LENGKAP,
                                Source = db.SOURCEs.FirstOrDefault(s => s.SOURCE_ID == candidate.SOURCE_ID).SOURCE_NAME,
                                PhoneNumber = candidate.NOHP,
                                Email = candidate.EMAIL,
                                PraSelectionPIC  = db.USERs.FirstOrDefault(u => u.ROLE_ID == db.SELECTION_HISTORY.FirstOrDefault(h => h.CANDIDATE_ID == candidate.CANDIDATE_ID && h.STATE_ID == "").PIC_ID)
                                
                            }
                ).ToList();
            } 
            return View("Index", candidates);
        }
    }
}