using AutoMapper;
using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Services;
using Algorithms.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Algorithms.API.Extensions;

namespace Algorithms.API.Controllers
{
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserLoginsController : ControllerBase
    {
        private readonly IUserLoginService _userLoginService;
        private readonly IMapper _mapper;

        public UserLoginsController(IUserLoginService userLoginService, IMapper mapper)
        {
            _userLoginService = userLoginService;
            _mapper = mapper;
        }

        // General HTTP Methods
        [HttpGet]
        [SwaggerOperation(
            Summary = "List all UserLogins",
            Description = "List of UserLogins",
            OperationId = "ListAllUserLogins"
            )]
        [SwaggerResponse(200, "List of UserLogins", typeof(IEnumerable<UserLoginResource>))]
        [ProducesResponseType(typeof(IEnumerable<UserLoginResource>), 200)]
        public async Task<IEnumerable<UserLoginResource>> GetAllAsync()
        {
            var userLogins = await _userLoginService.ListAsync();

            var resources = _mapper.Map<IEnumerable<UserLogin>, IEnumerable<UserLoginResource>>(userLogins);

            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get UserLogins",
            Description = "Get UserLogin By UserLogin Id",
            OperationId = "GetUserLoginById"
        )]
        [SwaggerResponse(200, "UserLogins Returned", typeof(UserLoginResource))]
        [ProducesResponseType(typeof(UserLoginResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _userLoginService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userLoginResource = _mapper.Map<UserLogin, UserLoginResource>(result.Resource);

            return Ok(userLoginResource);
        }

        [HttpPost]
        [SwaggerOperation(
           Summary = "Add new UserLogin",
           Description = "Add new UserLogin with initial data",
           OperationId = "AddUserLogin"
        )]
        [SwaggerResponse(200, "UserLogin Added", typeof(UserLoginResource))]
        [ProducesResponseType(typeof(UserLoginResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserLoginResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userLogin = _mapper.Map<SaveUserLoginResource, UserLogin>(resource);
            var result = await _userLoginService.SaveAsync(userLogin);

            if (!result.Success)
                return BadRequest(result.Message);

            var userLoginResource = _mapper.Map<UserLogin, UserLoginResource>(result.Resource);

            return Ok(userLoginResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
             Summary = "Update UserLogin",
             Description = "Update UserLogin By UserLogin Id",
             OperationId = "UpdateUserLoginById"
        )]
        [SwaggerResponse(200, "UserLogin Updated", typeof(UserLoginResource))]
        [ProducesResponseType(typeof(UserLoginResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] SaveUserLoginResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userLogin = _mapper.Map<SaveUserLoginResource, UserLogin>(resource);
            var result = await _userLoginService.UpdateAsync(id, userLogin);

            if (!result.Success)
                return BadRequest(result.Message);

            var userLoginResource = _mapper.Map<UserLogin, UserLoginResource>(result.Resource);

            return Ok(userLoginResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete UserLogin",
            Description = "Delete UserLogin By UserLogin Id",
            OperationId = "DeleteUserLoginById"
        )]
        [SwaggerResponse(200, "UserLogin Deleted", typeof(UserLoginResource))]
        [ProducesResponseType(typeof(UserLoginResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _userLoginService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userLoginResource = _mapper.Map<UserLogin, UserLoginResource>(result.Resource);

            return Ok(userLoginResource);
        }

        // HTTP Methods for UserLoginType Entity
        [HttpPost]
        [Route("users/{userId}/userLogins/{userLoginId}")]
        [SwaggerOperation(
            Summary = "Assign User to UserLogin",
            Description = "Assign User to UserLogin by UserId and UserLoginId",
            OperationId = "Assign User To UserLogin"
        )]
        [SwaggerResponse(200, "UserLogin to UserLoginType Assigned", typeof(UserLoginResource))]
        [ProducesResponseType(typeof(UserLoginResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> AssignUserLoginToUserLoginType(Guid userId, Guid userLoginId)
        {
            var result = await _userLoginService.AssingUserToUserLogin(userId, userLoginId);
            if (!result.Success)
                return BadRequest(result.Message);

            var userLogin = await _userLoginService.GetByIdAsync(result.Resource.Id);
            var userLoginResource = _mapper.Map<UserLogin, UserLoginResource>(userLogin.Resource);

            return Ok(userLoginResource);
        }

        [HttpDelete("users/{userId}/userLogins/{userLoginId}")]
        [SwaggerOperation(
            Summary = "Unassign User to UserLogin",
            Description = "Unassign User to UserLogin by UserId and UserLoginId",
            OperationId = "UnassignUser To UserLogin"
        )]
        [SwaggerResponse(200, "UserLogin to UserLoginType Unassigned", typeof(UserLoginResource))]
        [ProducesResponseType(typeof(UserLoginResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> UnassignUserLoginToUserLoginType(Guid userId, Guid userLoginId)
        {
            var result = await _userLoginService.UnassingUserToUserLogin(userId, userLoginId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userLogin = await _userLoginService.GetByIdAsync(result.Resource.Id);
            var userLoginResource = _mapper.Map<UserLogin, UserLoginResource>(userLogin.Resource);

            return Ok(userLoginResource);
        }
    }
}
