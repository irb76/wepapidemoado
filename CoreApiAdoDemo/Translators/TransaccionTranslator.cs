using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiAdoDemo.Model;
using System.Data.SqlClient;
using CoreApiAdoDemo.Utility;

namespace CoreApiAdoDemo.Translators
{
    public static class TransaccionTranslator
    {
        public static TransaccionModel TranslateAsTransaccion(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new TransaccionModel();

            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("IdCuenta"))
                item.IdCuenta = SqlHelper.GetNullableInt32(reader, "IdCuenta");

            if (reader.IsColumnExists("TipoTransaccion"))
                item.TipoTransaccion = SqlHelper.GetNullableString(reader, "TipoTransaccion");

            if (reader.IsColumnExists("Monto"))
                item.Monto = SqlHelper.GetNullableDecimal(reader, "Monto");

            return item;
        }

        public static List<TransaccionModel> TranslateAsTransaccionesList(this SqlDataReader reader)
        {
            var list = new List<TransaccionModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsTransaccion(reader, true));
            }
            return list;
        }
    }
}
