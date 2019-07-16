﻿using System;
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
            //result.Add(new SelectListItem { Text = "None", Value = null });
            try
            {
                foreach (Sumber sumber in sumbers)
                {
                    result.Add(new SelectListItem { Text = sumber.SumberNama, Value = sumber.SumberId });
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
                Nama = m.POSITION_NAME
            }).ToList();
            //result.Add(new SelectListItem { Text = "None", Value = null });
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

        //Populate dropdownlist based on stateNext
        public List<SelectListItem> DropListState(CallModelProses candidate)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            using (RecruitmentEntities RE = new RecruitmentEntities())
            {
                List<string> stateNext = new List<string>();
                try
                {
                    stateNext = RE.STATEs.Find(candidate.StateId).STATE_NEXT.Split(',').ToList();
                    if (stateNext[0] == "ST003")
                    {
                        stateNext.RemoveAt(0);
                        stateNext.Add("ST002");
                    }
                }
                catch (NullReferenceException)
                {
                }

                foreach (string s in stateNext)
                {
                    try
                    {
                        string stateName = RE.STATEs.Find(s).STATE_NAME;
                        result.Add(new SelectListItem { Text = stateName, Value = s });
                    }
                    catch (NullReferenceException) { }
                }
            }

            return result;
        }

        public List<ExperienceDTO> ListExperience(string id)
        {
            RecruitmentEntities db = new RecruitmentEntities();
            //List<string> exps = db.EXPERIENCEs.Where(m => m.CANDIDATE_ID == id)
            //                                  .Select(x => x.EXPERIENCE_NAME + " " + x.INDUSTRI).ToList();
            List<ExperienceDTO> experiences = db.EXPERIENCEs.Where(e => e.CANDIDATE_ID == id).Select(x => new ExperienceDTO
            {
                ExperienceId = x.EXPERIENCE_ID,
                ExperienceName = x.EXPERIENCE_NAME,
                Industri = x.INDUSTRI,
                Posisi = x.POSISI,
                StartDate = (DateTime)x.START_DATE,
                EndDate = (DateTime)x.END_DATE,
                Salary = (int)x.SALARY,
                JobDesc = x.JOB_DESC,
                Skill = x.SKILL,
                Project = x.PROJECT,
                Benefit = x.BENEFIT,
                CandidateId = x.CANDIDATE_ID
            }).ToList();

            return experiences;
        }

        public List<SelectListItem> DropListStatePraSelect()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            try
            {
                result.Add(new SelectListItem { Text = "Pra-Selection", Value = "ST000" });
                result.Add(new SelectListItem { Text = "Pra-Selected", Value = "ST001" });
            }
            catch (NullReferenceException)
            {

            }
            return result;
        }

        public List<SelectListItem> DropListRole()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            RecruitmentEntities db = new RecruitmentEntities();
            List<RoleModels> roles = db.ROLEs.Select(m => new RoleModels
            {
                RoleId = m.ROLE_ID,
                RoleName = m.ROLE_NAME

            }).ToList();
            result.Add(new SelectListItem { Text = "None", Value = null });
            try
            {
                foreach (RoleModels role in roles)
                {
                    result.Add(new SelectListItem { Text = role.RoleId + " (" + role.RoleName + ")", Value = role.RoleId });
                }
            }
            catch (NullReferenceException e)
            {

            }

            return result;
        }

        public List<SelectListItem> DropListSkill()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            RecruitmentEntities db = new RecruitmentEntities();
            List<SkillModels> skills = db.SKILLs.Select(m => new SkillModels
            {
                SkillId = m.ID_SKILL,
                SkillName = m.SKILL_NAME
            }).ToList();
            //result.Add(new SelectListItem { Text = "None", Value = null });
            try
            {
                foreach (SkillModels skill in skills)
                {
                    result.Add(new SelectListItem { Text = skill.SkillName, Value = skill.SkillId.ToString() });
                }
            }
            catch (NullReferenceException e)
            {

            }

            return result;
        }

    }
}