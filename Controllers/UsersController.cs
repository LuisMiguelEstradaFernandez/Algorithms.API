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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // General HTTP Methods
        [HttpGet]
        [SwaggerOperation(
            Summary = "List all users",
            Description = "List of users",
            OperationId = "ListAllUsers"
            )]
        [SwaggerResponse(200, "List of Users", typeof(IEnumerable<UserResource>))]
        [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
        public async Task<IEnumerable<UserResource>> GetAllAsync()
        {
            var users = await _userService.ListAsync();

            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);

            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get Users",
            Description = "Get User By User Id",
            OperationId = "GetUserById"
        )]
        [SwaggerResponse(200, "Users Returned", typeof(UserResource))]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);

            return Ok(userResource);
        }

        [HttpPost]
        [SwaggerOperation(
           Summary = "Add new user",
           Description = "Add new user with initial data",
           OperationId = "AddUser"
        )]
        [SwaggerResponse(200, "User Added", typeof(UserResource))]
        [ProducesResponseType(typeof(UserResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.SaveAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);

            return Ok(userResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
             Summary = "Update User",
             Description = "Update User By User Id",
             OperationId = "UpdateUserById"
        )]
        [SwaggerResponse(200, "User Updated", typeof(UserResource))]
        [ProducesResponseType(typeof(UserResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.UpdateAsync(id, user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);

            return Ok(userResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete User",
            Description = "Delete User By User Id",
            OperationId = "DeleteUserById"
        )]
        [SwaggerResponse(200, "User Deleted", typeof(UserResource))]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _userService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);

            return Ok(userResource);
        }
    }

}
