using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiAdoDemo.Model;
using System.Data.SqlClient;
using CoreApiAdoDemo.Utility;

namespace CoreApiAdoDemo.Translators
{
    public static class ClienteTranslator
    {
        public static ClientesModel TranslateAsCliente(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new ClientesModel();
            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("NombreCliente"))
                item.NombreCliente = SqlHelper.GetNullableString(reader, "NombreCliente");

            if (reader.IsColumnExists("EmailCliente"))
                item.EmailCliente = SqlHelper.GetNullableString(reader, "EmailCliente");

            if (reader.IsColumnExists("MontoInicial"))
                item.MontoInicial = SqlHelper.GetNullableDecimal(reader, "MontoInicial");

            if (reader.IsColumnExists("Saldo"))
                item.Saldo = SqlHelper.GetNullableDecimal(reader, "Saldo");

            if (reader.IsColumnExists("ClienteCuenta"))
                item.ClienteCuenta = SqlHelper.GetNullableInt32(reader, "ClienteCuenta");

            return item;
        }

        public static List<ClientesModel> TranslateAsClientesList(this SqlDataReader reader)
        {
            var list = new List<ClientesModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsCliente(reader, true));
            }
            return list;
        }
    }
}
