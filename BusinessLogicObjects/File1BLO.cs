using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using CAPRES.ValueObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class File1BLO
    {
        private File1DAO file1DAO = new File1DAO();
        private File1VO file1VO = new File1VO();
        private DataTable dt = new DataTable();

        public DataTable SelectFile1ForCSV(string startDate, string endDate)
        {
            return file1DAO.SelectFile1ForCSV(startDate, endDate);
        }

        public File1VO SelectFile1OfCandidate(int candidateId)
        {
            dt = file1DAO.SelectFile1OfCandidate(candidateId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    file1VO.File1Id = int.Parse(dr["file1_id"].ToString());
                    file1VO.CandidateId = int.Parse(dr["candidate_id"].ToString());
                    file1VO.FirstName = dr["first_name"].ToString();
                    file1VO.LastName = dr["last_name"].ToString();
                    file1VO.NickName = dr["nick_name"].ToString();
                    file1VO.MiddleName = dr["middle_name"].ToString();
                    file1VO.DateOfBirth = dr["date_of_birth"].ToString();
                    file1VO.PlaceOfBirth = dr["place_of_birth"].ToString();
                    file1VO.Nationality = dr["nationality_code"].ToString();
                    file1VO.Gender = dr["gender_code"].ToString();
                    file1VO.CivilStatus = dr["civil_status_code"].ToString();
                    file1VO.CivilStatusDate = dr["civil_status_date"].ToString();
                    file1VO.Affix = dr["affix_code"].ToString();
                    file1VO.Religion = dr["religion_code"].ToString();
                    file1VO.Height = dr["height"].ToString();
                    file1VO.Weight = dr["weight"].ToString();
                    file1VO.BloodType = dr["blood_type"].ToString();
                    file1VO.SSS = dr["sss_number"].ToString();
                    file1VO.TIN = dr["tin_number"].ToString();
                    file1VO.PresentAddress = dr["present_address"].ToString();
                    file1VO.PresentCity = dr["present_city"].ToString();
                    file1VO.PresentDistrict = dr["present_district"].ToString();
                    file1VO.PresentCountry = dr["present_country_code"].ToString();
                    file1VO.PresentTelephoneHome = dr["present_telephone_home"].ToString();
                    file1VO.PresentTelephoneAdditional = dr["present_telephone_additional"].ToString();
                    file1VO.PresentTelephoneMobile = dr["present_telephone_mobile"].ToString();
                    file1VO.PresentFax = dr["present_fax"].ToString();
                    file1VO.PermanentAddress = dr["permanent_address"].ToString();
                    file1VO.PermanentCity = dr["permanent_city"].ToString();
                    file1VO.PermanentDistrict = dr["permanent_district"].ToString();
                    file1VO.PermanentCountry = dr["permanent_country_code"].ToString();
                    file1VO.ProvincialAddress = dr["provincial_address"].ToString();
                    file1VO.ProvincialCity = dr["provincial_city"].ToString();
                    file1VO.ProvincialDistrict = dr["provincial_district"].ToString();
                    file1VO.ProvincialCountry = dr["provincial_country_code"].ToString();
                    file1VO.ProvincialTelephone = dr["provincial_telephone"].ToString();
                }
                return file1VO;
            }
            else
            {
                return null;
            }
        }

        public File1VO SelectFile1OfCandidateForPrint(int candidateId)
        {
            dt = file1DAO.SelectFile1OfCandidateForPrint(candidateId);
            foreach (DataRow dr in dt.Rows)
            {
                file1VO.File1Id = int.Parse(dr["file1_id"].ToString());
                file1VO.CandidateId = int.Parse(dr["candidate_id"].ToString());
                file1VO.FirstName = dr["first_name"].ToString();
                file1VO.LastName = dr["last_name"].ToString();
                file1VO.NickName = dr["nick_name"].ToString();
                file1VO.MiddleName = dr["middle_name"].ToString();
                file1VO.DateOfBirth = dr["date_of_birth"].ToString();
                file1VO.PlaceOfBirth = dr["place_of_birth"].ToString();
                file1VO.Nationality = dr["nationality"].ToString();
                file1VO.Gender = dr["gender"].ToString();
                file1VO.CivilStatus = dr["civil_status"].ToString();
                file1VO.CivilStatusDate = dr["civil_status_date"].ToString();
                file1VO.Affix = dr["affix"].ToString();
                file1VO.Religion = dr["religion"].ToString();
                file1VO.Height = dr["height"].ToString();
                file1VO.Weight = dr["weight"].ToString();
                file1VO.BloodType = dr["blood_type"].ToString();
                file1VO.SSS = dr["sss_number"].ToString();
                file1VO.TIN = dr["tin_number"].ToString();
                file1VO.PresentAddress = dr["present_address"].ToString();
                file1VO.PresentCity = dr["present_city"].ToString();
                file1VO.PresentDistrict = dr["present_district"].ToString();
                file1VO.PresentCountry = dr["present_country"].ToString();
                file1VO.PresentTelephoneHome = dr["present_telephone_home"].ToString();
                file1VO.PresentTelephoneAdditional = dr["present_telephone_additional"].ToString();
                file1VO.PresentTelephoneMobile = dr["present_telephone_mobile"].ToString();
                file1VO.PresentFax = dr["present_fax"].ToString();
                file1VO.PermanentAddress = dr["permanent_address"].ToString();
                file1VO.PermanentCity = dr["permanent_city"].ToString();
                file1VO.PermanentDistrict = dr["permanent_district"].ToString();
                file1VO.PermanentCountry = dr["permanent_country"].ToString();
                file1VO.ProvincialAddress = dr["provincial_address"].ToString();
                file1VO.ProvincialCity = dr["provincial_city"].ToString();
                file1VO.ProvincialDistrict = dr["provincial_district"].ToString();
                file1VO.ProvincialCountry = dr["provincial_country"].ToString();
                file1VO.ProvincialTelephone = dr["provincial_telephone"].ToString();
            }
            return file1VO;
        }

        public void InsertFile1(int candidateId, string status, string firstName, string lastName,
            string nickName, string middleName, string dateOfBirth, string placeOfBirth,
            string nationality, string gender, string civilStatus, string civilStatusDate, string affix,
            string religion, string height, string weight, string bloodType, string sss, string tin,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax, string permanentAddress, string permanentCity,
            string permanentDistrict, string permanentCountry,
            string provincialAddress, string provincialCity, string provincialDistrict,
            string provincialCountry, string provincialTelephone)
        {
            /* if (status == "pending" || status == "applicant")
            {
                if (civilStatusDate.Length == 0 && provincialCountry == "0")
                {
                    file1DAO.InsertFile1AFEWithoutCivDateProvAdd(candidateId, firstName, lastName, nickName,
                        middleName, presentTelephoneMobile, dateOfBirth, placeOfBirth, civilStatus,
                        gender, bloodType, weight, height, presentAddress, presentCity,
                        presentDistrict, presentCountry);
                }
                else if (civilStatusDate.Length > 0 && provincialCountry == "0")
                {
                    file1DAO.InsertFile1AFEWithoutProvAdd(candidateId, firstName, lastName, nickName,
                        middleName, presentTelephoneMobile, dateOfBirth, placeOfBirth, civilStatus,
                        civilStatusDate, gender, bloodType, weight, height, presentAddress, presentCity,
                        presentDistrict, presentCountry);
                }
                else if (civilStatusDate.Length == 0 && provincialCountry != "0")
                {
                    file1DAO.InsertFile1AFEWithoutCivDate(candidateId, firstName, lastName, nickName,
                        middleName, presentTelephoneMobile, dateOfBirth, placeOfBirth, civilStatus,
                        gender, bloodType, weight, height, presentAddress, presentCity,
                        presentDistrict, presentCountry, provincialAddress, provincialCity, provincialDistrict,
                        provincialCountry);
                }
                else
                {
                    file1DAO.InsertFile1AFE(candidateId, firstName, lastName, nickName, middleName,
                        presentTelephoneMobile, dateOfBirth, placeOfBirth, civilStatus, civilStatusDate,
                        gender, bloodType, weight, height, presentAddress, presentCity, presentCity,
                        presentCountry, provincialAddress, provincialCity, provincialDistrict, provincialCountry);
                }
            }
            else
            {
                if (civilStatusDate.Length == 0 && permanentCountry == "0" && provincialCountry == "0")
                {
                    file1DAO.InsertFile1WithoutCivDatePermProvAdds(candidateId, firstName, lastName, nickName,
                        middleName, dateOfBirth, placeOfBirth, nationality, gender, civilStatus, affix,
                        religion, height, weight, bloodType, sss, tin, presentAddress, presentCity,
                        presentDistrict, presentCountry, presentTelephoneHome,
                        presentTelephoneAdditional, presentTelephoneMobile, presentFax);
                }
                else if (civilStatusDate.Length > 0 && permanentCountry == "0" && provincialCountry == "0")
                {
                    file1DAO.InsertFile1WithoutPermProvAdds(candidateId, firstName, lastName, nickName, middleName,
                        dateOfBirth, placeOfBirth, nationality, gender, civilStatus, civilStatusDate, affix,
                        religion, height, weight, bloodType, sss, tin, presentAddress, presentCity,
                        presentDistrict, presentCountry, presentTelephoneHome,
                        presentTelephoneAdditional, presentTelephoneMobile, presentFax);
                }
                else if (civilStatusDate.Length > 0 && permanentCountry != "0" && provincialCountry == "0")
                {
                    file1DAO.InsertFile1WithoutProvAdd(candidateId, firstName, lastName, nickName, middleName,
                        dateOfBirth, placeOfBirth, nationality, gender, civilStatus, civilStatusDate, affix,
                        religion, height, weight, bloodType, sss, tin, presentAddress, presentCity,
                        presentDistrict, presentCountry, presentTelephoneHome,
                        presentTelephoneAdditional, presentTelephoneMobile, presentFax,
                        permanentAddress, permanentCity, permanentDistrict, permanentCountry);
                }
                else if (civilStatusDate.Length > 0 && permanentCountry == "0" && provincialCountry != "0")
                {
                    file1DAO.InsertFile1WithoutPermAdd(candidateId, firstName, lastName, nickName, middleName,
                        dateOfBirth, placeOfBirth, nationality, gender, civilStatus, civilStatusDate, affix,
                        religion, height, weight, bloodType, sss, tin, presentAddress, presentCity,
                        presentDistrict, presentCountry, presentTelephoneHome,
                        presentTelephoneAdditional, presentTelephoneMobile, presentFax,
                        provincialAddress, provincialCity, provincialDistrict, provincialCountry,
                        provincialTelephone);
                }
                else if (civilStatusDate.Length == 0 && permanentCountry != "0" && provincialCountry == "0")
                {
                    file1DAO.InsertFile1WithoutCivDateProvAdd(candidateId, firstName, lastName, nickName,
                        middleName, dateOfBirth, placeOfBirth, nationality, gender, civilStatus, affix,
                        religion, height, weight, bloodType, sss, tin, presentAddress, presentCity,
                        presentDistrict, presentCountry, presentTelephoneHome,
                        presentTelephoneAdditional, presentTelephoneMobile, presentFax,
                        permanentAddress, permanentCity, permanentDistrict, permanentCountry);
                }
                else if (civilStatusDate.Length == 0 && permanentCountry != "0" && provincialCountry != "0")
                {
                    file1DAO.InsertFile1WithoutCivDate(candidateId, firstName, lastName, nickName, middleName,
                        dateOfBirth, placeOfBirth, nationality, gender, civilStatus, affix,
                        religion, height, weight, bloodType, sss, tin, presentAddress, presentCity,
                        presentDistrict, presentCountry, presentTelephoneHome,
                        presentTelephoneAdditional, presentTelephoneMobile, presentFax,
                        permanentAddress, permanentCity, permanentDistrict, permanentCountry,
                        provincialAddress, provincialCity, provincialDistrict, provincialCountry,
                        provincialTelephone);
                }
                else if (civilStatusDate.Length == 0 && permanentCountry == "0" && provincialCountry != "0")
                {
                    file1DAO.InsertFile1WithoutCivDatePermAdd(candidateId, firstName, lastName, nickName,
                        middleName, dateOfBirth, placeOfBirth, nationality, gender, civilStatus, affix,
                        religion, height, weight, bloodType, sss, tin, presentAddress, presentCity,
                        presentDistrict, presentCountry, presentTelephoneHome,
                        presentTelephoneAdditional, presentTelephoneMobile, presentFax,
                        provincialAddress, provincialCity, provincialDistrict, provincialCountry,
                        provincialTelephone);
                }
                else
                { */
                    file1DAO.InsertFile1(candidateId, firstName, lastName, nickName, middleName,
                        dateOfBirth, placeOfBirth, nationality, gender, civilStatus, civilStatusDate, affix,
                        religion, height, weight, bloodType, sss, tin, presentAddress, presentCity,
                        presentDistrict, presentCountry, presentTelephoneHome,
                        presentTelephoneAdditional, presentTelephoneMobile, presentFax,
                        permanentAddress, permanentCity, permanentDistrict, permanentCountry,
                        provincialAddress, provincialCity, provincialDistrict, provincialCountry,
                        provincialTelephone);
                //}
            //}
        }

        public void UpdateFile1(int file1Id, string firstName,
            string lastName, string nickName, string middleName, string dateOfBirth,
            string placeOfBirth, string nationality, string gender, string civilStatus, string civilStatusDate,
            string affix, string religion, string height, string weight, string bloodType, string sss, string tin,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax, string permanentAddress, string permanentCity,
            string permanentDistrict, string permanentCountry,
            string provincialAddress, string provincialCity, string provincialDistrict,
            string provincialCountry, string provincialTelephone)
        {
            file1DAO.UpdateFile1(file1Id, firstName, lastName, nickName,
                middleName, dateOfBirth, placeOfBirth, nationality, gender, civilStatus, civilStatusDate,
                affix, religion, height, weight, bloodType, sss, tin, presentAddress, presentCity,
                presentDistrict, presentCountry, presentTelephoneHome,
                presentTelephoneAdditional, presentTelephoneMobile, presentFax,
                permanentAddress, permanentCity, permanentDistrict, permanentCountry,
                provincialAddress, provincialCity, provincialDistrict, provincialCountry,
                provincialTelephone);
        }
    }
}