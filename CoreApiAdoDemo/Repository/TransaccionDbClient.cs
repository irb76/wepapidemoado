using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiAdoDemo.Model;
using CoreApiAdoDemo.Utility;
using CoreApiAdoDemo.Translators;
using System.Data.SqlClient;
using System.Data;

namespace CoreApiAdoDemo.Repository
{
    public class TransaccionDbClient
    {
        public List<TransaccionModel> GetAllTransacciones(string connString)
        {
            return SqlHelper.ExtecuteProcedureReturnData<List<TransaccionModel>>(connString,
                "GetTransacciones", r => r.TranslateAsTransaccionesList());
        }

        public string AddTransaccion(TransaccionModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@IdCuenta",model.IdCuenta),
                new SqlParameter("@TipoTransaccion",model.TipoTransaccion),
                new SqlParameter("@Monto",model.Monto),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "AddTransaccion", param);
            return (string)outParam.Value;
        }
    }
}
