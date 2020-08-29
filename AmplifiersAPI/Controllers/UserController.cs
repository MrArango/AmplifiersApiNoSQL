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
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
    
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<Users>> GetAll()
        {
            Response response = new Response();

            try
            {
                response.Data = _userService.Get();
                response.Status = true;
                response.Message = "Todo Cool al traer todos los usuarios";
            }
            catch(Exception ex)
            {
                response = Tools.Tools.FillEx(ex);
            }

            return Ok(response);
        }

        [HttpGet("{id:length(24)}")]
        [Authorize]
        public ActionResult<Users> GetById(string id)
        {
            Response response = new Response();

            try
            {
                var user = _userService.GetById(id);

                if (user != null)
                {
                    response.Data = user;
                    response.Message = "Todo Cool al traer el usuario";
                } 
                else
                {
                    response.Data = null;
                    response.Message = "El usuario no existe";
                }

                response.Status = true;   
            }
            catch (Exception ex)
            {
                response = Tools.Tools.FillEx(ex);
            }
            
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<Users> Create(Users user)
        {
            Response response = new Response();

            try
            {
                _userService.Create(user);
                response.Data = true;
                response.Status = true;
                response.Message = "Todo Cool al crear un nuevo usuario";
            }
            catch (Exception ex)
            {
                response = Tools.Tools.FillEx(ex);
            }

            return Ok(response);
        }

        [HttpPut("{id:length(24)}")]
        [Authorize]
        public IActionResult Update(string id, Users userIn)
        {
            Response response = new Response();

            try
            {
                var user = _userService.GetById(id);

                if (user != null)
                {
                    _userService.UpdateById(id, userIn);
                    response.Data = true;
                    response.Message = "Se actualizo la info del usuario";
                }
                else
                {
                    response.Data = false;
                    response.Message = "El usuario no existe";
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
        [Authorize]
        public IActionResult Delete(string id)
        {
            Response response = new Response();

            try
            {
                var user = _userService.GetById(id);

                if (user != null)
                {
                    _userService.DeleteById(user.Id);
                    response.Message = "Se elimino el usuario";
                    response.Data = true;
                }
                else
                {
                    response.Data = false;
                    response.Message = "El usuario no existe";
                }

                response.Status = true;
            }
            catch (Exception ex)
            {
                response = Tools.Tools.FillEx(ex);
            }

            return Ok(response);
        }
        
        [HttpPost("login")]
        public IActionResult Login(Users user)
        {
            Response response = new Response();

            try
            {
                var Uresponse = _userService.Login(user);

                if (Uresponse != null)
                {
                    response.Data = Uresponse;
                    response.Message = "Eres tu :)";
                }
                else
                {
                    response.Data = null;
                    response.Message = "Tus creedenciales estan mal :(";
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
