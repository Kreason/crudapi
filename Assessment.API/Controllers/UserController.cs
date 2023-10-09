using Assessment.BLL;
using Assessment.BLL.Interface;
using Assessment.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.API.Controllers
{
    // The user controller is where endpoints are made to perform crud operations on users
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        //THis is where the Business logic layer is called and used for dependency injection
        private readonly IUserBLL _userBLL;

        public UserController(IUserBLL userBLL)
        {
            _userBLL = userBLL;
        }

        [Authorize] // controllers also make use of authorization attributes so that the api is kept secure, in this case i use a jwt token to auth
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> Create()
        {
            //the controllers are made to return action results which have a status of the call once completed eg 200 is an OK
            try
            {
                return Ok(await _userBLL.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromQuery] string username, string password)
        {
            try
            {
                if (username == null || password == null)
                {
                    return NoContent();
                }

                return Ok(await _userBLL.Authenticate(username, password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest createUser)
        {
            try
            {
                return Ok(await _userBLL.Create(createUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUser)
        {
            try
            {
                return Ok(await _userBLL.Update(updateUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int userID)
        {
            try
            {
                return Ok(await _userBLL.Delete(userID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
