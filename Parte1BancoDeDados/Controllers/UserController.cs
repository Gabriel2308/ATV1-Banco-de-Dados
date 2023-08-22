using Microsoft.AspNetCore.Mvc;
using Parte1BancoDeDados.Data;
using Parte1BancoDeDados.Data.DTO;
using Parte1BancoDeDados.infra.connection;

namespace Parte1BancoDeDados.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllUsers")]
        public IEnumerable<UserEntity> GetUserByCpf()
        {
            List<UserEntity> result = new List<UserEntity>();

            User res = new User();
            result = res.getAllUsers();
            return result;
        }
        [HttpPost("CreateUser")]
        public StatusReturn PostUser(UserEntity user)
        {
            User res = new User();

            return res.CreateUser(user);

        }

        [HttpGet("GetUserByCPF")]
        public ActionResult<UserDTO> GetUserByCPF(int cpf)
        {
            User res = new User();
            var result = res.GetUser(cpf);
            if (result.nome != null)
                return Ok(new ApiResponse(result, 0, ""));
            else 
            {
                result.cpf  = "";
                result.nome = "";
                return NotFound(new ApiResponse(result, 1, "Usuario nao encontrado"));
            } 

        }
    }
}