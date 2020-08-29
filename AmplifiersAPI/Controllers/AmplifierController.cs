using System.Collections.Generic;
using AmplifiersAPI.Services;
using AmplifiersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using AmplifiersAPI.Tools;

namespace AmplifiersAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AmplifierController : ControllerBase
    {
        private readonly AmplifierService _amplifierService;

        public AmplifierController(AmplifierService amplifierService)
        {
            _amplifierService = amplifierService;
        }

        [HttpGet]
        public ActionResult<List<Amplifiers>> GetAll(Users iduser)
        {
            Response response = new Response();

            try
            {
                response.Data = _amplifierService.Get(iduser.Id);
                response.Status = true;
                response.Message = "Todo Cool al traer todos los amplificadores de un usuario";
            }
            catch (Exception ex)
            {
                response = Tools.Tools.FillEx(ex);
            }

            return Ok(response);
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<List<Amplifiers>> GetById(string id)
        {
            Response response = new Response();

            try
            {
                response.Data = _amplifierService.GetById(id);
                response.Status = true;
                response.Message = "Todo Cool al traer un amplificador";
            }
            catch (Exception ex)
            {
                response = Tools.Tools.FillEx(ex);
            }

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<Amplifiers> Create(Amplifiers amp)
        {
            Response response = new Response();

            try
            {
                _amplifierService.Create(amp);
                response.Data = true;
                response.Status = true;
                response.Message = "Todo Cool al crear un nuevo ampli";
            }
            catch (Exception ex)
            {
                response = Tools.Tools.FillEx(ex);
            }

            return Ok(response);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Amplifiers ampIn)
        {
            Response response = new Response();

            try
            {
                var user = _amplifierService.GetById(id);

                if (user != null)
                {
                    _amplifierService.UpdateById(id, ampIn);
                    response.Data = true;
                    response.Message = "Se actualizo la info del ampli";
                }
                else
                {
                    response.Data = false;
                    response.Message = "El ampli no existe";
                }

                response.Status = true;
            }
            catch (Exception ex)
            {
                response = Tools.Tools.FillEx(ex);
            }

            return Ok(response);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            Response response = new Response();

            try
            {
                var user = _amplifierService.GetById(id);

                if (user != null)
                {
                    _amplifierService.DeleteById(user.Id);
                    response.Message = "Se elimino el ampli";
                    response.Data = true;
                }
                else
                {
                    response.Data = false;
                    response.Message = "El ampli no existe";
                }

                response.Status = true;
            }
            catch (Exception ex)
            {
                response = Tools.Tools.FillEx(ex);
            }

            return Ok(response);
        }


    }
}
