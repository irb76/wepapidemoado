using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CoreApiAdoDemo.Model
{
    [DataContract]
    public class TransaccionModel
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "IdCuenta")]
        public int IdCuenta { get; set; }

        [DataMember(Name = "TipoTransaccion")]
        public string TipoTransaccion { get; set; }

        [DataMember(Name = "Monto")]
        public decimal Monto { get; set; }
    }
}
