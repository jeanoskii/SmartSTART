using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CAPRES.DataAccessObjects
{
    public class File1DAO
    {
        private DB db = new DB();

        public DataTable SelectFile1ForCSV(string startDate, string endDate)
        {
            return db.GetData("SELECT File1.candidate_id, first_name, last_name, nick_name, "
                + "middle_name, CONVERT(char(10), date_of_birth, 101) as date_of_birth, "
                + "place_of_birth, gender_code, civil_status_code, nationality_code, "
                + "religion_code, sss_number, tin_number, present_address, present_city, "
                + "present_country_code, present_district, present_telephone_home, "
                + "present_telephone_additional, present_telephone_mobile, present_fax, "
                + "permanent_address, permanent_city, permanent_country_code, permanent_district, "
                + "provincial_telephone, provincial_address, provincial_city, provincial_country_code, "
                + "provincial_district, "
                + "affix_code, blood_type FROM File1, AFE, Candidates "
                + "WHERE File1.candidate_id = Candidates.candidate_id "
                + "AND Candidates.candidate_id = AFE.candidate_id AND Candidates.status = 'hired' "
                + "AND Candidates.hiring_date BETWEEN '" + startDate + "' AND '" + endDate + "'");
        }

        public DataTable SelectFile1OfCandidate(int candidateId)
        {
            return db.GetData("SELECT file1_id, candidate_id, first_name, last_name, nick_name, "
                + "middle_name, date_of_birth, place_of_birth, gender_code, civil_status_code, civil_status_date, "
                + "nationality_code, religion_code, height, weight, sss_number, tin_number, present_address, "
                + "present_city, present_country_code, present_district, present_telephone_home, "
                + "present_telephone_additional, present_telephone_mobile, present_fax, "
                + "permanent_address, permanent_city, permanent_country_code, permanent_district, "
                + "provincial_address, provincial_city, provincial_country_code, "
                + "provincial_district, provincial_district, provincial_telephone, "
                + "blood_type, affix_code FROM File1 WHERE candidate_id = '"
                + candidateId + "'");
        }

        public DataTable SelectFile1OfCandidateForPrint(int candidateId)
        {
            return db.GetData("SELECT file1_id, candidate_id, first_name, "
                + "last_name, nick_name, middle_name, date_of_birth, place_of_birth, "
                + "n.nationality, g.gender, c.civil_status, civil_status_date, a.affix, r.religion, "
                + "height, weight, b.blood_type, sss_number, tin_number, present_address, present_city, "
                + "present_district, cn1.country AS present_country, present_telephone_home, "
                + "present_telephone_additional, present_telephone_mobile, present_fax, "
                + "permanent_address, permanent_city, permanent_district, cn2.country AS "
                + "permanent_country, provincial_address, provincial_city, provincial_district, "
                + "cn3.country AS provincial_country, provincial_telephone "
                + "FROM File1 LEFT JOIN Nationalities n ON File1.nationality_code = "
                + "n.nationality_code LEFT JOIN Genders g ON File1.gender_code = "
                + "g.gender_code LEFT JOIN Civil_Statuses c ON File1.civil_status_code = "
                + "c.civil_status_code LEFT JOIN Affixes a ON File1.affix_code = "
                + "a.affix_code LEFT JOIN Religions r ON File1.religion_code = "
                + "r.religion_code LEFT JOIN Blood_Types b ON File1.blood_type = "
                + "b.blood_type LEFT JOIN Countries cn1 ON File1.present_country_code = "
                + "cn1.country_code LEFT JOIN Countries cn2 ON File1.permanent_country_code = "
                + "cn2.country_code LEFT JOIN Countries cn3 ON File1.provincial_country_code = "
                + "cn3.country_code WHERE File1.candidate_id = '" + candidateId + "'");
        }

        /// <summary>
        /// Insert complete fields.
        /// </summary>
        public void InsertFile1(int candidateId, string firstName, string lastName,
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
            SqlParameter[] parameters = new SqlParameter[35];
            parameters[0] = new SqlParameter("@candidate_id", candidateId);
            parameters[1] = new SqlParameter("@first_name", firstName);
            parameters[2] = new SqlParameter("@last_name", lastName);
            parameters[3] = new SqlParameter("@nick_name", nickName);
            parameters[4] = new SqlParameter("@middle_name", middleName);
            parameters[5] = new SqlParameter("@date_of_birth", dateOfBirth);
            parameters[6] = new SqlParameter("@place_of_birth", placeOfBirth);
            parameters[7] = new SqlParameter("@nationality_code", nationality);
            parameters[8] = new SqlParameter("@gender_code", gender);
            parameters[9] = new SqlParameter("@civil_status_code", civilStatus);
            parameters[10] = new SqlParameter("@civil_status_date", civilStatusDate);
            parameters[11] = new SqlParameter("@affix_code", affix);
            parameters[12] = new SqlParameter("@religion_code", religion);
            parameters[13] = new SqlParameter("@height", height);
            parameters[14] = new SqlParameter("@weight", weight);
            parameters[15] = new SqlParameter("@blood_type", bloodType);
            parameters[16] = new SqlParameter("@sss_number", sss);
            parameters[17] = new SqlParameter("@tin_number", tin);
            parameters[18] = new SqlParameter("@present_address", presentAddress);
            parameters[19] = new SqlParameter("@present_city", presentCity);
            parameters[20] = new SqlParameter("@present_district", presentDistrict);
            parameters[21] = new SqlParameter("@present_country_code", presentCountry);
            parameters[22] = new SqlParameter("@present_telephone_home", presentTelephoneHome);
            parameters[23] = new SqlParameter("@present_telephone_additional", presentTelephoneAdditional);
            parameters[24] = new SqlParameter("@present_telephone_mobile", presentTelephoneMobile);
            parameters[25] = new SqlParameter("@present_fax", presentFax);
            parameters[26] = new SqlParameter("@permanent_address", permanentAddress);
            parameters[27] = new SqlParameter("@permanent_city", permanentCity);
            parameters[28] = new SqlParameter("@permanent_district", permanentDistrict);
            parameters[29] = new SqlParameter("@permanent_country_code", permanentCountry);
            parameters[30] = new SqlParameter("@provincial_address", provincialAddress);
            parameters[31] = new SqlParameter("@provincial_city", provincialCity);
            parameters[32] = new SqlParameter("@provincial_district", provincialDistrict);
            parameters[33] = new SqlParameter("@provincial_country_code", provincialCountry);
            parameters[34] = new SqlParameter("@provincial_telephone", provincialTelephone);
                
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, date_of_birth, "
                + "place_of_birth, nationality_code, gender_code, civil_status_code, civil_status_date, "
                + "affix_code, religion_code, height, weight, blood_type, sss_number, tin_number, "
                + "present_address, present_city, present_district, present_country_code, "
                + "present_telephone_home, present_telephone_additional, "
                + "present_telephone_mobile, present_fax, permanent_address, "
                + "permanent_city, permanent_district, permanent_country_code, "
                + "provincial_address, provincial_city, provincial_district, "
                + "provincial_country_code, provincial_telephone) "
                + "VALUES (@candidate_id, @first_name, @last_name, @nick_name, @middle_name, @date_of_birth, "
                + "@place_of_birth, @nationality_code, @gender_code, @civil_status_code, @civil_status_date, "
                + "@affix_code, @religion_code, @height, @weight, @blood_type, @sss_number, @tin_number, "
                + "@present_address, @present_city, @present_district, @present_country_code, @present_telephone_home, "
                + "@present_telephone_additional, @present_telephone_mobile, @present_fax, @permanent_address, "
                + "@permanent_city, @permanent_district, @permanent_country_code, @provincial_address, @provincial_city, "
                + "@provincial_district, @provincial_country_code, @provincial_telephone)", parameters);
            
           /* db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, date_of_birth, "
                + "place_of_birth, nationality_code, gender_code, civil_status_code, civil_status_date, "
                + "affix_code, religion_code, height, weight, blood_type, sss_number, tin_number, "
                + "present_address, present_city, present_district, present_country_code, "
                + "present_telephone_home, present_telephone_additional, "
                + "present_telephone_mobile, present_fax, permanent_address, "
                + "permanent_city, permanent_district, permanent_country_code, "
                + "provincial_address, provincial_city, provincial_district, "
                + "provincial_country_code, provincial_telephone) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + dateOfBirth + "','"
                + placeOfBirth + "','" + nationality + "','" + gender + "','"
                + civilStatus + "','" + civilStatusDate + "','" + affix + "','" + religion + "','"
                + height + "','" + weight + "','" + bloodType + "','" + sss + "','" + tin
                + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "','" + presentTelephoneHome + "','"
                + presentTelephoneAdditional + "','" + presentTelephoneMobile + "','"
                + presentFax + "','" + permanentAddress + "','" + permanentCity + "','"
                + permanentDistrict + "','" + permanentCountry + "','"
                + provincialAddress + "','" + provincialCity + "','"
                + provincialDistrict + "','" + provincialCountry + "','"
                + provincialTelephone + "')"); */
        }

        /// <summary>
        /// Insert w/o civil status date.
        /// </summary>
        public void InsertFile1WithoutCivDate(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string dateOfBirth, string placeOfBirth,
            string nationality, string gender, string civilStatus, string affix,
            string religion, string height, string weight, string bloodType, string sss, string tin,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax, string permanentAddress, string permanentCity,
            string permanentDistrict, string permanentCountry,
            string provincialAddress, string provincialCity, string provincialDistrict,
            string provincialCountry, string provincialTelephone)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, date_of_birth, "
                + "place_of_birth, nationality_code, gender_code, civil_status_code, "
                + "affix_code, religion_code, height, weight, blood_type, sss_number, tin_number, "
                + "present_address, present_city, present_district, present_country_code, "
                + "present_telephone_home, present_telephone_additional, "
                + "present_telephone_mobile, present_fax, permanent_address, "
                + "permanent_city, permanent_district, permanent_country_code, "
                + "provincial_address, provincial_city, provincial_district, "
                + "provincial_country_code, provincial_telephone) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + dateOfBirth + "','"
                + placeOfBirth + "','" + nationality + "','" + gender + "','"
                + civilStatus + "','" + affix + "','" + religion + "','"
                + height + "','" + weight + "','" + bloodType + "','" + sss + "','" + tin
                + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "','" + presentTelephoneHome + "','"
                + presentTelephoneAdditional + "','" + presentTelephoneMobile + "','"
                + presentFax + "','" + permanentAddress + "','" + permanentCity + "','"
                + permanentDistrict + "','" + permanentCountry + "','"
                + provincialAddress + "','" + provincialCity + "','"
                + provincialDistrict + "','" + provincialCountry + "','"
                + provincialTelephone + "')");
        }

        /// <summary>
        /// Insert w/o permanent address.
        /// </summary>
        public void InsertFile1WithoutPermAdd(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string dateOfBirth, string placeOfBirth,
            string nationality, string gender, string civilStatus, string civilStatusDate, string affix,
            string religion, string height, string weight, string bloodType, string sss, string tin,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax,
            string provincialAddress, string provincialCity, string provincialDistrict,
            string provincialCountry, string provincialTelephone)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, date_of_birth, "
                + "place_of_birth, nationality_code, gender_code, civil_status_code, civil_status_date, "
                + "affix_code, religion_code, height, weight, blood_type, sss_number, tin_number, "
                + "present_address, present_city, present_district, present_country_code, "
                + "present_telephone_home, present_telephone_additional, "
                + "present_telephone_mobile, present_fax, "
                + "provincial_address, provincial_city, provincial_district, "
                + "provincial_country_code, provincial_telephone) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + dateOfBirth + "','"
                + placeOfBirth + "','" + nationality + "','" + gender + "','"
                + civilStatus + "','" + civilStatusDate + "','" + affix + "','" + religion + "','"
                + height + "','" + weight + "','" + bloodType + "','" + sss + "','" + tin
                + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "','" + presentTelephoneHome + "','"
                + presentTelephoneAdditional + "','" + presentTelephoneMobile + "','"
                + presentFax + "','"
                + provincialAddress + "','" + provincialCity + "','"
                + provincialDistrict + "','" + provincialCountry + "','"
                + provincialTelephone + "')");
        }

        /// <summary>
        /// Insert w/o provincial address.
        /// </summary>
        public void InsertFile1WithoutProvAdd(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string dateOfBirth, string placeOfBirth,
            string nationality, string gender, string civilStatus, string civilStatusDate, string affix,
            string religion, string height, string weight, string bloodType, string sss, string tin,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax, string permanentAddress, string permanentCity,
            string permanentDistrict, string permanentCountry)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, date_of_birth, "
                + "place_of_birth, nationality_code, gender_code, civil_status_code, civil_status_date, "
                + "affix_code, religion_code, height, weight, blood_type, sss_number, tin_number, "
                + "present_address, present_city, present_district, present_country_code, "
                + "present_telephone_home, present_telephone_additional, "
                + "present_telephone_mobile, present_fax, permanent_address, "
                + "permanent_city, permanent_district, permanent_country_code) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + dateOfBirth + "','"
                + placeOfBirth + "','" + nationality + "','" + gender + "','"
                + civilStatus + "','" + civilStatusDate + "','" + affix + "','" + religion + "','"
                + height + "','" + weight + "','" + bloodType + "','" + sss + "','" + tin
                + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "','" + presentTelephoneHome + "','"
                + presentTelephoneAdditional + "','" + presentTelephoneMobile + "','"
                + presentFax + "','" + permanentAddress + "','" + permanentCity + "','"
                + permanentDistrict + "','" + permanentCountry + "')");
        }

        /// <summary>
        /// Insert w/o permanent and provincial addresses.
        /// </summary>
        public void InsertFile1WithoutPermProvAdds(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string dateOfBirth, string placeOfBirth,
            string nationality, string gender, string civilStatus, string civilStatusDate, string affix,
            string religion, string height, string weight, string bloodType, string sss, string tin,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, date_of_birth, "
                + "place_of_birth, nationality_code, gender_code, civil_status_code, civil_status_date, "
                + "affix_code, religion_code, height, weight, blood_type, sss_number, tin_number, "
                + "present_address, present_city, present_district, present_country_code, "
                + "present_telephone_home, present_telephone_additional, "
                + "present_telephone_mobile, present_fax) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + dateOfBirth + "','"
                + placeOfBirth + "','" + nationality + "','" + gender + "','"
                + civilStatus + "','" + civilStatusDate + "','" + affix + "','" + religion + "','"
                + height + "','" + weight + "','" + bloodType + "','" + sss + "','" + tin
                + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "','" + presentTelephoneHome + "','"
                + presentTelephoneAdditional + "','" + presentTelephoneMobile + "','"
                + presentFax + "')");
        }

        /// <summary>
        /// Insert w/o civil status date and permanent address.
        /// </summary>
        public void InsertFile1WithoutCivDatePermAdd(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string dateOfBirth, string placeOfBirth,
            string nationality, string gender, string civilStatus, string affix,
            string religion, string height, string weight, string bloodType, string sss, string tin,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax,
            string provincialAddress, string provincialCity, string provincialDistrict,
            string provincialCountry, string provincialTelephone)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, date_of_birth, "
                + "place_of_birth, nationality_code, gender_code, civil_status_code, "
                + "affix_code, religion_code, height, weight, blood_type, sss_number, tin_number, "
                + "present_address, present_city, present_district, present_country_code, "
                + "present_telephone_home, present_telephone_additional, "
                + "present_telephone_mobile, present_fax, "
                + "provincial_address, provincial_city, provincial_district, "
                + "provincial_country_code, provincial_telephone) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + dateOfBirth + "','"
                + placeOfBirth + "','" + nationality + "','" + gender + "','"
                + civilStatus + "','" + affix + "','" + religion + "','"
                + height + "','" + weight + "','" + bloodType + "','" + sss + "','" + tin
                + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "','" + presentTelephoneHome + "','"
                + presentTelephoneAdditional + "','" + presentTelephoneMobile + "','"
                + presentFax + "','"
                + provincialAddress + "','" + provincialCity + "','"
                + provincialDistrict + "','" + provincialCountry + "','"
                + provincialTelephone + "')");
        }

        /// <summary>
        /// Insert w/o civil status date and provincial address
        /// </summary>
        public void InsertFile1WithoutCivDateProvAdd(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string dateOfBirth, string placeOfBirth,
            string nationality, string gender, string civilStatus, string affix,
            string religion, string height, string weight, string bloodType, string sss, string tin,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax, string permanentAddress, string permanentCity,
            string permanentDistrict, string permanentCountry)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, date_of_birth, "
                + "place_of_birth, nationality_code, gender_code, civil_status_code, "
                + "affix_code, religion_code, height, weight, blood_type, sss_number, tin_number, "
                + "present_address, present_city, present_district, present_country_code, "
                + "present_telephone_home, present_telephone_additional, "
                + "present_telephone_mobile, present_fax, permanent_address, "
                + "permanent_city, permanent_district, permanent_country_code) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + dateOfBirth + "','"
                + placeOfBirth + "','" + nationality + "','" + gender + "','"
                + civilStatus + "','" + affix + "','" + religion + "','"
                + height + "','" + weight + "','" + bloodType + "','" + sss + "','" + tin
                + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "','" + presentTelephoneHome + "','"
                + presentTelephoneAdditional + "','" + presentTelephoneMobile + "','"
                + presentFax + "','" + permanentAddress + "','" + permanentCity + "','"
                + permanentDistrict + "','" + permanentCountry + "')");
        }

        /// <summary>
        /// Insert w/o civil status date, permanent and provincial addresses
        /// </summary>
        public void InsertFile1WithoutCivDatePermProvAdds(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string dateOfBirth, string placeOfBirth,
            string nationality, string gender, string civilStatus, string affix,
            string religion, string height, string weight, string bloodType, string sss, string tin,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, date_of_birth, "
                + "place_of_birth, nationality_code, gender_code, civil_status_code, "
                + "affix_code, religion_code, height, weight, blood_type, sss_number, tin_number, "
                + "present_address, present_city, present_district, present_country_code, "
                + "present_telephone_home, present_telephone_additional, "
                + "present_telephone_mobile, present_fax) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + dateOfBirth + "','"
                + placeOfBirth + "','" + nationality + "','" + gender + "','"
                + civilStatus + "','" + affix + "','" + religion + "','"
                + height + "','" + weight + "','" + bloodType + "','" + sss + "','" + tin
                + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "','" + presentTelephoneHome + "','"
                + presentTelephoneAdditional + "','" + presentTelephoneMobile + "','"
                + presentFax + "')");
        }

        public void InsertFile1AFE(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string presentTelephoneMobile,
            string dateOfBirth, string placeOfBirth, string civilStatus, string civilStatusDate,
            string gender, string bloodType, string weight, string height,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string provincialAddress, string provincialCity, string provincialDistrict,
            string provincialCountry)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, present_telephone_mobile, date_of_birth, "
                + "place_of_birth, civil_status_code, civil_status_date, gender_code, blood_type, "
                + "weight, height, present_address, present_city, present_district, "
                + "present_country_code, provincial_address, provincial_city, provincial_district, "
                + "provincial_country_code) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + presentTelephoneMobile + "','"
                + dateOfBirth + "','" + placeOfBirth + "','" + civilStatus + "','"
                + civilStatusDate + "','" + gender + "','" + bloodType + "','"
                + weight + "','" + height + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "','" + provincialAddress + "','"
                + provincialCity + "','" + provincialDistrict + "','"
                + provincialCountry + "')");
        }

        public void InsertFile1AFEWithoutCivDate(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string presentTelephoneMobile,
            string dateOfBirth, string placeOfBirth, string civilStatus,
            string gender, string bloodType, string weight, string height,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string provincialAddress, string provincialCity, string provincialDistrict,
            string provincialCountry)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, present_telephone_mobile, date_of_birth, "
                + "place_of_birth, civil_status_code, gender_code, blood_type, "
                + "weight, height, present_address, present_city, present_district, "
                + "present_country_code, provincial_address, provincial_city, provincial_district, "
                + "provincial_country_code) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + presentTelephoneMobile + "','"
                + dateOfBirth + "','" + placeOfBirth + "','" + civilStatus + "','"
                + gender + "','" + bloodType + "','"
                + weight + "','" + height + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "','" + provincialAddress + "','"
                + provincialCity + "','" + provincialDistrict + "','"
                + provincialCountry + "')");
        }

        public void InsertFile1AFEWithoutProvAdd(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string presentTelephoneMobile,
            string dateOfBirth, string placeOfBirth, string civilStatus, string civilStatusDate,
            string gender, string bloodType, string weight, string height,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, present_telephone_mobile, date_of_birth, "
                + "place_of_birth, civil_status_code, civil_status_date, gender_code, blood_type, "
                + "weight, height, present_address, present_city, present_district, "
                + "present_country_code) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + presentTelephoneMobile + "','"
                + dateOfBirth + "','" + placeOfBirth + "','" + civilStatus + "','"
                + civilStatusDate + "','" + gender + "','" + bloodType + "','"
                + weight + "','" + height + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "')");
        }

        public void InsertFile1AFEWithoutCivDateProvAdd(int candidateId, string firstName, string lastName,
            string nickName, string middleName, string presentTelephoneMobile,
            string dateOfBirth, string placeOfBirth, string civilStatus,
            string gender, string bloodType, string weight, string height,
            string presentAddress, string presentCity, string presentDistrict,
            string presentCountry)
        {
            db.Execute("InsertFile1", "INSERT INTO File1 (candidate_id, "
                + "first_name, last_name, nick_name, middle_name, present_telephone_mobile, date_of_birth, "
                + "place_of_birth, civil_status_code, gender_code, blood_type, "
                + "weight, height, present_address, present_city, present_district, "
                + "present_country_code) "
                + "VALUES ('" + candidateId + "','" + firstName + "','" + lastName + "','"
                + nickName + "','" + middleName + "','" + presentTelephoneMobile + "','"
                + dateOfBirth + "','" + placeOfBirth + "','" + civilStatus + "','"
                + gender + "','" + bloodType + "','"
                + weight + "','" + height + "','" + presentAddress + "','" + presentCity + "','" + presentDistrict + "','"
                + presentCountry + "')");
        }

        public void UpdateFile1(int file1Id, string firstName,
            string lastName, string nickName, string middleName, string dateOfBirth,
            string placeOfBirth, string nationality, string gender, string civilStatus, string civilStatusDate,
            string affix, string religion, string height, string weight, string bloodType,
            string sss, string tin, string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax, string permanentAddress, string permanentCity,
            string permanentDistrict, string permanentCountry,
            string provincialAddress, string provincialCity, string provincialDistrict,
            string provincialCountry, string provincialTelephone)
        {
            /* db.Execute("UpdateFile1", "UPDATE File1 SET first_name = '"
                + firstName + "', last_name = '" + lastName + "', nick_name = '"
                + nickName + "', middle_name = '" + middleName + "', date_of_birth = '"
                + dateOfBirth + "', place_of_birth = '" + placeOfBirth + "', nationality_code = '"
                + nationality + "', gender_code = '" + gender + "', civil_status_code = '"
                + civilStatus + "', civil_status_date = '" + civilStatusDate + "', affix_code = '"
                + affix + "', religion_code = '" + religion + "', height = '" + height + "', weight = '"
                + weight + "', blood_type = '" + bloodType + "', sss_number = '" + sss + "', tin_number = '"
                + tin + "', present_address = '"
                + presentAddress + "', present_city = '" + presentCity + "', present_district = '"
                + presentDistrict + "', present_country_code = '"
                + presentCountry + "', present_telephone_home = '"
                + presentTelephoneHome + "', present_telephone_additional = '"
                + presentTelephoneAdditional + "', present_telephone_mobile = '"
                + presentTelephoneMobile + "', present_fax = '"
                + presentFax + "', permanent_address = '" + permanentAddress + "', permanent_city = '"
                + permanentCity + "', permanent_district = '"
                + permanentDistrict + "', permanent_country_code = '"
                + permanentCountry + "', provincial_address = '"
                + provincialAddress + "', provincial_city = '"
                + provincialCity + "', provincial_district = '"
                + provincialDistrict + "', provincial_country_code = '"
                + provincialCountry + "', provincial_telephone = '"
                + provincialTelephone + "' WHERE file1_id = '"
                + file1Id + "' AND candidate_id = '" + candidateId + "'"); */

            SqlParameter[] parameters = new SqlParameter[35];
            parameters[0] = new SqlParameter("@first_name", firstName);
            parameters[1] = new SqlParameter("@last_name", lastName);
            parameters[2] = new SqlParameter("@nick_name", nickName);
            parameters[3] = new SqlParameter("@middle_name", middleName);
            parameters[4] = new SqlParameter("@date_of_birth", dateOfBirth);
            parameters[5] = new SqlParameter("@place_of_birth", placeOfBirth);
            parameters[6] = new SqlParameter("@nationality_code", nationality);
            parameters[7] = new SqlParameter("@gender_code", gender);
            parameters[8] = new SqlParameter("@civil_status_code", civilStatus);
            parameters[9] = new SqlParameter("@civil_status_date", civilStatusDate);
            parameters[10] = new SqlParameter("@affix_code", affix);
            parameters[11] = new SqlParameter("@religion_code", religion);
            parameters[12] = new SqlParameter("@height", height);
            parameters[13] = new SqlParameter("@weight", weight);
            parameters[14] = new SqlParameter("@blood_type", bloodType);
            parameters[15] = new SqlParameter("@sss_number", sss);
            parameters[16] = new SqlParameter("@tin_number", tin);
            parameters[17] = new SqlParameter("@present_address", presentAddress);
            parameters[18] = new SqlParameter("@present_city", presentCity);
            parameters[19] = new SqlParameter("@present_district", presentDistrict);
            parameters[20] = new SqlParameter("@present_country_code", presentCountry);
            parameters[21] = new SqlParameter("@present_telephone_home", presentTelephoneHome);
            parameters[22] = new SqlParameter("@present_telephone_additional", presentTelephoneAdditional);
            parameters[23] = new SqlParameter("@present_telephone_mobile", presentTelephoneMobile);
            parameters[24] = new SqlParameter("@present_fax", presentFax);
            parameters[25] = new SqlParameter("@permanent_address", permanentAddress);
            parameters[26] = new SqlParameter("@permanent_city", permanentCity);
            parameters[27] = new SqlParameter("@permanent_district", permanentDistrict);
            parameters[28] = new SqlParameter("@permanent_country_code", permanentCountry);
            parameters[29] = new SqlParameter("@provincial_address", provincialAddress);
            parameters[30] = new SqlParameter("@provincial_city", provincialCity);
            parameters[31] = new SqlParameter("@provincial_district", provincialDistrict);
            parameters[32] = new SqlParameter("@provincial_country_code", provincialCountry);
            parameters[33] = new SqlParameter("@provincial_telephone", provincialTelephone);
            parameters[34] = new SqlParameter("@file1_id", file1Id);
            
            db.Execute("UpdateFile1", "UPDATE File1 SET first_name = @first_name"
                + ", last_name = @last_name, nick_name = @nick_name"
                + ",middle_name = @middle_name, date_of_birth = @date_of_birth"
                + ", place_of_birth = @place_of_birth, nationality_code = @nationality_code"
                + ", gender_code = @gender_code, civil_status_code = @civil_status_code"
                + ", civil_status_date = @civil_status_date, affix_code = @affix_code"
                + ", religion_code = @religion_code, height = @height,  weight = @weight"
                + ", blood_type = @blood_type, sss_number = @sss_number, tin_number = @tin_number"
                + ", present_address = @present_address, present_city = @present_city, present_district = @present_district"
                + ", present_country_code = @present_country_code, present_telephone_home = @present_telephone_home"
                + ", present_telephone_additional = @present_telephone_additional"
                + ", present_telephone_mobile = @present_telephone_mobile"
                + ", present_fax = @present_fax, permanent_address = @permanent_address, permanent_city = @permanent_city"
                + ", permanent_district = @permanent_district, permanent_country_code = @permanent_country_code"
                + ", provincial_address = @provincial_address, provincial_city = @provincial_city"
                + ", provincial_district = @provincial_district, provincial_country_code = @provincial_country_code"
                + ", provincial_telephone = @provincial_telephone WHERE file1_id = @file1_id", parameters);
        }

        //update without civil status date, provincial, permanent address
        public void UpdateFile1WithoutCivDatePermProvAdds(int file1Id, int candidateId, string firstName,
            string lastName, string nickName, string middleName, string dateOfBirth,
            string placeOfBirth, string nationality, string gender, string civilStatus,
            string affix, string religion, string height, string weight, string bloodType,
            string sss, string tin, string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax)
        {
            /* db.Execute("UpdateFile1", "UPDATE File1 SET first_name = '"
                + firstName + "', last_name = '" + lastName + "', nick_name = '"
                + nickName + "', middle_name = '" + middleName + "', date_of_birth = '"
                + dateOfBirth + "', place_of_birth = '" + placeOfBirth + "', nationality_code = '"
                + nationality + "', gender_code = '" + gender + "', civil_status_code = '"
                + civilStatus + "', affix_code = '"
                + affix + "', religion_code = '" + religion + "', height = '" + height + "', weight = '"
                + weight + "', blood_type = '" + bloodType + "', sss_number = '" + sss + "', tin_number = '"
                + tin + "', present_address = '"
                + presentAddress + "', present_city = '" + presentCity + "', present_district = '"
                + presentDistrict + "', present_country_code = '"
                + presentCountry + "', present_telephone_home = '"
                + presentTelephoneHome + "', present_telephone_additional = '"
                + presentTelephoneAdditional + "', present_telephone_mobile = '"
                + presentTelephoneMobile + "', present_fax = '"
                + presentFax + "', WHERE file1_id = '"
                + file1Id + "' AND candidate_id = '" + candidateId + "'"); */
        }

        //update without PermAdd and ProvAdd
        public void UpdateWithoutPermAddProvAdd(int file1Id, int candidateId, string firstName,
            string lastName, string nickName, string middleName, string dateOfBirth,
            string placeOfBirth, string nationality, string gender, string civilStatus, string civilStatusDate,
            string affix, string religion, string height, string weight, string bloodType,
            string sss, string tin, string presentAddress, string presentCity, string presentDistrict,
            string presentCountry, string presentTelephoneHome,
            string presentTelephoneAdditional, string presentTelephoneMobile,
            string presentFax)
        {
            db.Execute("UpdateFile1", "UPDATE File1 SET first_name = '"
                + firstName + "', last_name = '" + lastName + "', nick_name = '"
                + nickName + "', middle_name = '" + middleName + "', date_of_birth = '"
                + dateOfBirth + "', place_of_birth = '" + placeOfBirth + "', nationality_code = '"
                + nationality + "', gender_code = '" + gender + "', civil_status_code = '"
                + civilStatus + "', civil_status_date = '" + civilStatusDate + "', affix_code = '"
                + affix + "', religion_code = '" + religion + "', height = '" + height + "', weight = '"
                + weight + "', blood_type = '" + bloodType + "', sss_number = '" + sss + "', tin_number = '"
                + tin + "', present_address = '"
                + presentAddress + "', present_city = '" + presentCity + "', present_district = '"
                + presentDistrict + "', present_country_code = '"
                + presentCountry + "', present_telephone_home = '"
                + presentTelephoneHome + "', present_telephone_additional = '"
                + presentTelephoneAdditional + "', present_telephone_mobile = '"
                + presentTelephoneMobile + "', present_fax = '"
                + presentFax + "' WHERE file1_id = '"
                + file1Id + "' AND candidate_id = '" + candidateId + "'");
        }
    }
}