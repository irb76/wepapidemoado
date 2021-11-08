using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CoreApiAdoDemo.Model
{
    [DataContract]
    public class CuentaAhorroModel
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "MontoInicial")]
        public decimal MontoInicial { get; set; }

        [DataMember(Name = "Saldo")]
        public decimal Saldo { get; set; }

        [DataMember(Name = "ClienteCuenta")]
        public int ClienteCuenta { get; set; }
    }
}
