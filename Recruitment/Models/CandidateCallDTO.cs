using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class CandidateCallDTO
    {
        string candidateId;
        string name;
        string position;
        string source;
        string phone;
        string email;
        string preSelectPIC;        
        string state;
        string notes;

        //Popup
        int expectedSalary;
        DateTime availableJoin;
        List<EXPERIENCE> experiences;

        public string CandidateId { get => candidateId; set => candidateId = value; }
        public string Name { get => name; set => name = value; }
        public string Position { get => position; set => position = value; }
        public string Source { get => source; set => source = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public string PreSelectPIC { get => preSelectPIC; set => preSelectPIC = value; }        
        public string State { get => state; set => state = value; }
        public string Notes { get => notes; set => notes = value; }
        public int ExpectedSalary { get => expectedSalary; set => expectedSalary = value; }
        public DateTime AvailableJoin { get => availableJoin; set => availableJoin = value; }
        public string AvaliableJoinDate { get => availableJoin.ToLongDateString(); }
        public List<EXPERIENCE> Experiences { get => experiences; set => experiences = value; }

        public List<String> AllBenefits { get => GetAllBenefits(); }
        List<String> GetAllBenefits() {
            List<string> result = new List<string>();
            foreach (EXPERIENCE exp in experiences) {
                try {
                    string[] benefits = exp.BENEFIT.Split(',');
                    foreach (string benefit in benefits) {
                        result.Add(benefit);
                    }
                }
                catch (NullReferenceException) {
                    result.Add("");
                }
            }

            return result;
        }

        public String AllBenefits1Line { get => GetAllBenefits1Line(); }
        String GetAllBenefits1Line() {
            List<string> result = new List<string>();
            foreach (EXPERIENCE exp in experiences) {
                try {
                    string[] benefits = exp.BENEFIT.Split(',');
                    foreach (string benefit in benefits) {
                        result.Add(benefit);
                    }
                }
                catch(NullReferenceException) {
                    result.Add("");
                }
            }

            return string.Join(", ", result);
        }


        public List<String> AllSkills { get => GetAllSkills(); }
        List<String> GetAllSkills() {
            List<string> result = new List<string>();
            foreach (EXPERIENCE exp in experiences) {
                try {
                    string[] skills = exp.SKILL.Split(',');
                    foreach (string skill in skills) {
                        result.Add(skill);
                    }
                }
                catch (NullReferenceException) {
                    result.Add("");
                }
            }

            return result;
        }

        public String AllSkills1Line { get => GetAllSkills1Line(); }
        String GetAllSkills1Line() {
            List<string> result = new List<string>();
            foreach (EXPERIENCE exp in experiences) {
                try {
                    string[] skills = exp.SKILL.Split(',');
                    foreach (string skill in skills) {
                        result.Add(skill);
                    }
                }
                catch (NullReferenceException) {
                    result.Add("");
                }
            }

            return string.Join(", ", result);
        }
    }
}