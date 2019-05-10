using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.Models
{
    public class DropListSource
    {
        
        public List<SelectListItem> DropListName()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            RecruitmentEntities db = new RecruitmentEntities();
            List<Sumber> sumbers = db.SOURCEs.Select(m => new Sumber
            {
                SumberId = m.SOURCE_ID,
                SumberNama = m.SOURCE_NAME
            }).ToList();
            result.Add(new SelectListItem { Text = "None", Value = null });
            try
            {
                foreach (Sumber sumber in sumbers)
                {
                    result.Add(new SelectListItem { Text = sumber.SumberNama , Value = sumber.SumberId });
                }
            }
            catch (NullReferenceException e)
            {

            }

            return result;
        }

        public List<SelectListItem> DropListPosition()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            RecruitmentEntities db = new RecruitmentEntities();
            List<PositionPoco> positions = db.POSITIONs.Select(m => new PositionPoco
            {
                IdPosisi = m.POSITION_ID,
                Nama =m.POSITION_NAME
            }).ToList();
            result.Add(new SelectListItem { Text = "None", Value = null });
            try
            {
                foreach (PositionPoco posisi in positions)
                {
                    result.Add(new SelectListItem { Text = posisi.Nama, Value = posisi.IdPosisi });
                }
            }
            catch (NullReferenceException e)
            {

            }

            return result;
        }

        public List<SelectListItem> DropListState()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            RecruitmentEntities db = new RecruitmentEntities();
            List<StateDTO> states = db.STATEs.Select(m => new StateDTO
            {
                StateId = m.STATE_ID,
                StateName = m.STATE_NAME,
                StateNext = m.STATE_NEXT
            }).ToList();
            result.Add(new SelectListItem { Text = "None", Value = null });
            try
            {
                foreach (StateDTO state in states)
                {
                    result.Add(new SelectListItem { Text = state.StateName, Value = state.StateId });
                }
            }
            catch (NullReferenceException e)
            {

            }

            return result;
        }
    }
}