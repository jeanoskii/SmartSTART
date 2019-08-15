using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class PositionTypeDAO
    {
        private DB db = new DB();

        public DataTable SelectAllPositionType()
        {
            return db.GetData("SELECT position_type_code, position_type FROM Position_Types");
        }

        public DataTable SelectPositionType(string positionTypeCode)
        {
            return db.GetData("SELECT position_type_code, position_type FROM Position_Types "
                + "WHERE position_type_code = '" + positionTypeCode + "'");
        }

        public void InsertPositionType(string positionTypeCode, string positionType)
        {
            db.Execute("InsertPositionType", "INSERT INTO Position_Types (position_type_code, " +
                "position_type) VALUES ('" + positionTypeCode + "','" + positionType + "')");
        }

        public void UpdatePositionType(string positionTypeCode, string positionType)
        {
            db.Execute("UpdatePositionType", "UPDATE Position_Types SET position_type = '"
                + positionType + "' WHERE position_type_code = '" + positionTypeCode + "'");
        }

        public void DeletePositionType(string positionTypeCode)
        {
            db.Execute("DeletePositionType", "DELETE FROM Position_Types WHERE position_type_code = '"
                + positionTypeCode + "'");
        }
    }
}