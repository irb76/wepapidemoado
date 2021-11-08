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
    public class ClienteDbClient
    {
        public List<ClientesModel> GetAllClientes(string connString)
        {
            return SqlHelper.ExtecuteProcedureReturnData<List<ClientesModel>>(connString,
                "GetClientes", r => r.TranslateAsClientesList());
        }

        public string AddCliente(ClientesModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                //new SqlParameter("@Id",model.Id),
                new SqlParameter("@NombreCliente",model.NombreCliente),
                new SqlParameter("@EmailCliente",model.EmailCliente),
                new SqlParameter("@MontoInicial",model.MontoInicial),
                new SqlParameter("@Saldo",model.Saldo),
                new SqlParameter("@ClienteCuenta",model.ClienteCuenta),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "AddCliente", param);
            return (string)outParam.Value;
        }

        public string SaveCliente(ClientesModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@NombreCliente",model.NombreCliente),
                new SqlParameter("@EmailCliente",model.EmailCliente),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "SaveCliente", param);
            return (string)outParam.Value;
        }

        public string DeleteCliente(int id, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",id),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "DeleteCliente", param);
            return (string)outParam.Value;
        }
    }
}
