using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiAdoDemo.Model;
using System.Data.SqlClient;
using CoreApiAdoDemo.Utility;

namespace CoreApiAdoDemo.Translators
{
    public static class CuentaTranslator
    {
        public static CuentaAhorroModel TranslateAsCuenta(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new CuentaAhorroModel();

            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("MontoInicial"))
                item.MontoInicial = SqlHelper.GetNullableDecimal(reader, "MontoInicial");

            if (reader.IsColumnExists("Saldo"))
                item.Saldo = SqlHelper.GetNullableDecimal(reader, "Saldo");

            if (reader.IsColumnExists("ClienteCuenta"))
                item.ClienteCuenta = SqlHelper.GetNullableInt32(reader, "ClienteCuenta");

            return item;
        }

        public static List<CuentaAhorroModel> TranslateAsCuentasList(this SqlDataReader reader)
        {
            var list = new List<CuentaAhorroModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsCuenta(reader, true));
            }
            return list;
        }
    }
}
