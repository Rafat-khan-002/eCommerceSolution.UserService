using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")] //api/auth
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public AuthController(IUsersService usersService)
        {
            _usersService = usersService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest(
                    new ServiceResponse<AuthenticationResponse>
                    {
                        IsSuccess = false,
                        Message = "Invalid registration data",
                        Error = new ErrorResponse(
                            400,
                            "Invalid registration data"
                        )
                    });
            }

            var response = await _usersService.Register(registerRequest);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        //Endpoint for user login use case
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            //Check for invalid LoginRequest
            if (loginRequest == null)
            {
                return BadRequest("Invalid login data");
            }

            AuthenticationResponse? authenticationResponse = await _usersService.Login(loginRequest);

            if (authenticationResponse == null || authenticationResponse.Success == false)
            {
                return Unauthorized(authenticationResponse);
            }

            return Ok(authenticationResponse);
        }
    }
}
