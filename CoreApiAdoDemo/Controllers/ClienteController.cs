using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiAdoDemo.Model;
using CoreApiAdoDemo.Repository;
using CoreApiAdoDemo.Utility;
using Microsoft.Extensions.Options;

namespace CoreApiAdoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IOptions<MySettingsModel> appSettings;

        public ClienteController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllClientes")]
        public IActionResult GetAllClientes()
        {
            var data = DbClientFactory<ClienteDbClient>.Instance.GetAllClientes(appSettings.Value.DbConn);
            return Ok(data);
        }

        

        [HttpPost]
        [Route("SaveCliente")]
        public IActionResult SaveCliente([FromBody] ClientesModel model)
        {
            var msg = new Message<ClientesModel>();
            var data = DbClientFactory<ClienteDbClient>.Instance.SaveCliente(model, appSettings.Value.DbConn);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                if (model.Id == 0)
                    msg.ReturnMessage = "Cliente guardado con exito";
                else
                    msg.ReturnMessage = "cliente actualizado con exito";
            }
            else if (data == "C201")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Email ya existe";
            }
            return Ok(msg);
        }

        [HttpPost]
        [Route("DeleteCliente")]
        public IActionResult DeleteCliente([FromBody] ClientesModel model)
        {
            var msg = new Message<ClientesModel>();
            var data = DbClientFactory<ClienteDbClient>.Instance.DeleteCliente(model.Id, appSettings.Value.DbConn);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                msg.ReturnMessage = "Cliente Borrado";
            }
            else if (data == "C203")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Id no valido";
            }
            return Ok(msg);
        }
    }
}
