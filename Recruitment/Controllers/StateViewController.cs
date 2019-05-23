using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.Controllers
{
    [RoutePrefix("state")]
    public class StateViewController : Controller
    {
        #region get views

        [Route("")]
        public ActionResult ListAllStates()
        {
            List<StateDTO> stateDTOs = PopulateStateDTOs();
            return View("ListStates", stateDTOs);
        }

        [Route("add")]
        public ActionResult FormAddState()
        {
            StateFormViewModel formViewModel = new StateFormViewModel
            {
                StateDTOs = PopulateStateDTOs()
            };

            return View("FormAddState", formViewModel);
        }
        #endregion

        #region actions
        [ActionName("AddState")]
        public ActionResult AddState(StateFormViewModel formViewModel)
        {
            if (ModelState.IsValid)
            {
                using (RecruitmentEntities recruitment = new RecruitmentEntities())
                {
                    try
                    {
                        STATE state = new STATE()
                        {
                            STATE_ID = formViewModel.FormState.StateId,
                            STATE_NAME = formViewModel.FormState.StateName,
                            STATE_NEXT = formViewModel.FormState.StateNext
                        };
                        recruitment.STATEs.Add(state);
                        recruitment.SaveChanges();

                        return Redirect("~/state/add");
                    }
                    catch (Exception)
                    {
                        TempData["validasi"] = "Data Has been used";
                        return View("FormAddState", formViewModel);
                    }
                    
                }
            }

            formViewModel.StateDTOs = PopulateStateDTOs();
            return View("FormAddState", formViewModel);
        }

        [ActionName("DeleteState")]
        public ActionResult DeleteState(string stateId)
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                recruitment.STATEs.Remove(recruitment.STATEs.Find(stateId));
                recruitment.SaveChanges();
                return Redirect("~/state");
            }
        }

        [ActionName("EditState")]
        public ActionResult EditState(string stateId)
        {
            List<StateDTO> stateDTOs = PopulateStateDTOs();

            StateFormViewModel formViewModel = new StateFormViewModel
            {
                StateDTOs = stateDTOs,
                FormState = stateDTOs.Find(s => s.StateId == stateId)
            };

            return View("FormEditState", formViewModel);
        }

        [ActionName("PutState")]
        public ActionResult PutState(StateFormViewModel formViewModel)
        {
            if (ModelState.IsValid)
            {
                using (RecruitmentEntities recruitment = new RecruitmentEntities())
                {
                    STATE state = new STATE()
                    {
                        STATE_ID = formViewModel.FormState.StateId,
                        STATE_NAME = formViewModel.FormState.StateName,
                        STATE_NEXT = formViewModel.FormState.StateNext
                    };

                    recruitment.Entry(state).State = System.Data.Entity.EntityState.Modified;
                    recruitment.SaveChanges();
                    return Redirect("~/state");
                }
            }

            formViewModel.StateDTOs = PopulateStateDTOs();
            return View("FormEditState", formViewModel);
        }

        #endregion

        #region Support

        List<StateDTO> PopulateStateDTOs()
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                List<StateDTO> stateDTOs = recruitment.STATEs.Select(s => new StateDTO()
                {
                    StateId = s.STATE_ID,
                    StateName = s.STATE_NAME,
                    StateNext = s.STATE_NEXT
                }).ToList();

                return stateDTOs;
            }
        }

        #endregion
    }
}