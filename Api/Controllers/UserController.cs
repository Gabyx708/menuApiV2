//using Application.Interfaces.IAuthentication;
//using Application.Interfaces.IAutomation;
//using Application.Interfaces.IPersonal;
//using Application.Request.AutomationRequest;
//using Application.Request.PersonalRequests;
//using Application.Request.UsuarioLoginRequests;
//using Application.Response.GenericResponses;
//using Application.Response.PersonalResponses;
//using Application.Response.UsuarioLoginResponse;
//using Application.Tools.Log;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Api.Controllers
//{
//    [Route("api/v1.3/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IPersonalService _services;
//        private readonly IAuthenticationService _authService;
//        private readonly IAutomation _automationServices;

//        public UserController(IPersonalService services, IAuthenticationService authService, IAutomation automationServices)
//        {
//            _services = services;
//            _authService = authService;
//            _automationServices = automationServices;
//        }

//        [HttpPost("login")]
//        [ProducesResponseType(typeof(UsuarioLoginResponse), 200)]
//        public IActionResult loginUser(UsuarioLoginRequest request)
//        {
//            var usuarioLog = _authService.autenticarUsuario(request);

//            if (usuarioLog == null)
//            {

//                Logger.LogInformation("login attempt for user: {@user}", request.username);
//                return Unauthorized();
//            }

//            Logger.LogInformation("login success for user: {@user}", usuarioLog.id);
//            return Ok(usuarioLog);
//        }

//        //[Authorize]
//        //[HttpPost("password/{id}/jsj")]
//        //[ProducesResponseType(typeof(NoContentResult), 204)]
//        //[ProducesResponseType(typeof(SystemResponse), 409)]
//        //public IActionResult changePassword(Guid id, PersonalPasswordRequest request)
//        //{
//        //    var result = _authService.changeUserPassword(id, request);

//        //    Logger.LogInformation("change password attemp for user: {@userId}", id);

//        //    if (result == null)
//        //    {
//        //        return new JsonResult(new SystemResponse { Message = "invalidad password", StatusCode = 409 }) { StatusCode = 409 };
//        //    }

//        //    return NoContent();
//        //}

//        [Authorize]
//        [HttpPost("password/reset/{id}")]
//        [ProducesResponseType(typeof(NoContentResult), 204)]
//        [ProducesResponseType(typeof(SystemResponse), 409)]
//        public IActionResult resetPassword(Guid id)
//        {
//            var result = _authService.resetPassword(id);

//            if (result == null)
//            {
//                return new JsonResult(new SystemResponse { Message = "invalidad password", StatusCode = 409 }) { StatusCode = 409 };
//            }

//            return NoContent();
//        }

//        [Authorize]
//        [HttpGet]
//        [ProducesResponseType(typeof(List<PersonalResponse>), 200)]
//        public IActionResult GetTodoElpersonal()
//        {
//            var empleados = _services.GetAllPersonal();
//            return new JsonResult(empleados) { StatusCode = 200 };
//        }

//        [Authorize]
//        [HttpPost]
//        [ProducesResponseType(typeof(PersonalResponse), 201)]
//        public IActionResult CreatePersonal(PersonalRequest request)
//        {
//            PersonalResponse personalNuevo;

//            try
//            {

//                personalNuevo = _services.createPersonal(request);
//            }
//            catch (InvalidOperationException e)
//            {

//                return new JsonResult(new SystemResponse { Message = "intenta con otro DNI", StatusCode = 409 }) { StatusCode = 409 };
//            }

//            return new JsonResult(personalNuevo) { StatusCode = 201 };
//        }

//        [Authorize]
//        [HttpGet("{id}")]
//        [ProducesResponseType(typeof(PersonalResponse), 200)]
//        [ProducesResponseType(typeof(NotFoundResult), 404)]
//        public IActionResult GetPersonal(Guid id)
//        {
//            var personal = _services.GetPersonalById(id);
//            string message = $"no se encontro un usuario con el id: {id}";

//            if (personal == null) { return NotFound(message); };

//            return new JsonResult(personal) { StatusCode = 200 };
//        }

//        [Authorize]
//        [HttpPatch("{id}")]
//        public IActionResult AlterarPersonal(Guid id, PersonalRequest request)
//        {
//            var personalAlterado = _services.UpdatePersonal(id, request);

//            return Ok(personalAlterado);
//        }

//        [Authorize]
//        [HttpPatch("automation")]
//        public IActionResult AutomatizarPedido(AutomationRequest request)
//        {
//            try
//            {
//                var result = _automationServices.SetPedidoAutomatico(request);

//                if (result == null) { return NotFound(); };

//                return new JsonResult(result) { StatusCode = 204 };
//            }
//            catch (Exception e)
//            {
//                return new JsonResult(new SystemResponse { Message = "ocurrio un problema", StatusCode = 500 }) { StatusCode = 500 };
//            }

//        }
//    }
//}
