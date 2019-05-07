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
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56339");

                var get = client.GetAsync("api/state");
                get.Wait();
                var result = get.Result.Content.ReadAsAsync<List<STATE>>().Result;
                List<StateDTO> stateDTOs = (List<StateDTO>)result.Select(s => new StateDTO()
                {
                    StateId = s.STATE_ID,
                    StateName = s.STATE_NAME,
                    StateNext = s.STATE_NEXT
                }).ToList();

                return View("ListStates", stateDTOs);
            }
        }

        [Route("add")]
        public ActionResult FormAddState()
        {
            FormViewModel formViewModel = new FormViewModel
            {
                StateDTOs = PopulateStateDTOs()
            };

            return View("FormAddState", formViewModel);
        }
        #endregion

        #region actions
        [ActionName("AddState")]
        public ActionResult AddState(FormViewModel formViewModel)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56339");


                    STATE state = new STATE()
                    {
                        STATE_ID = formViewModel.FormState.StateId,
                        STATE_NAME = formViewModel.FormState.StateName,
                        STATE_NEXT = formViewModel.FormState.StateNext
                    };

                    var post = client.PostAsJsonAsync<STATE>("api/state", state);
                    post.Wait();

                    return Redirect("~/state/add");
                }
            }

            formViewModel.StateDTOs = PopulateStateDTOs();
            return View("FormAddState", formViewModel);
        }

        [ActionName("DeleteState")]
        public ActionResult DeleteState(string stateId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56339");

                var delete = client.DeleteAsync("api/state/" + stateId);
                delete.Wait();

                return Redirect("~/state");
            }
        }

        [ActionName("EditState")]
        public ActionResult EditState(string stateId)
        {

            List<StateDTO> stateDTOs = PopulateStateDTOs();

            FormViewModel formViewModel = new FormViewModel
            {
                StateDTOs = stateDTOs,
                FormState = stateDTOs.Find(s => s.StateId == stateId)
            };

            return View("FormEditState", formViewModel);
        }

        [ActionName("PutState")]
        public ActionResult PutState(FormViewModel formViewModel)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56339");

                    STATE state = new STATE()
                    {
                        STATE_ID = formViewModel.FormState.StateId,
                        STATE_NAME = formViewModel.FormState.StateName,
                        STATE_NEXT = formViewModel.FormState.StateNext
                    };

                    var put = client.PutAsJsonAsync("api/state/", state);
                    put.Wait();

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
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56339");

                var get = client.GetAsync("api/state");
                get.Wait();
                var result = get.Result.Content.ReadAsAsync<List<STATE>>().Result;
                List<StateDTO> stateDTOs = (List<StateDTO>)result.Select(s => new StateDTO()
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