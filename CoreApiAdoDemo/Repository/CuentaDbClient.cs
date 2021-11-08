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
    public class CuentaDbClient
    {
        public List<CuentaAhorroModel> GetAllCuentas(string connString)
        {
            return SqlHelper.ExtecuteProcedureReturnData<List<CuentaAhorroModel>>(connString,
                "GetCuentas", r => r.TranslateAsCuentasList());
        }

        public string AddCuenta(CuentaAhorroModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@MontoInicial",model.MontoInicial),
                new SqlParameter("@Saldo",model.Saldo),
                new SqlParameter("@ClienteCuenta",model.ClienteCuenta),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "AddCuenta", param);
            return (string)outParam.Value;
        }
    }
}
