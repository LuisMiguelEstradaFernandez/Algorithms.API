using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Services;
using Algorithms.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Algorithms.API.Extensions;

namespace Algorithms.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("/api/")]
    public class AlgorithmsController : ControllerBase
    {
        private readonly IAlgorithmService _algorithmService;
        private readonly IMapper _mapper;

        public AlgorithmsController(IAlgorithmService algorithmService, IMapper mapper)
        {
            _algorithmService = algorithmService;
            _mapper = mapper;
        }

        // General HTTP Methods

        [HttpPost("algorithms")]
        [SwaggerOperation(
            Summary = "Add new Algorithm",
            Description = "Add new Algorithm with initial data",
            OperationId = "AddAlgorithm"
        )]
        [SwaggerResponse(200, "Algorithm Added", typeof(AlgorithmResource))]
        [ProducesResponseType(typeof(AlgorithmResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveAlgorithmResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var algorithm = _mapper.Map<SaveAlgorithmResource, Algorithm>(resource);
            var result = await _algorithmService.SaveAsync(algorithm);

            if (!result.Success)
                return BadRequest(result.Message);

            var AlgorithmResource = _mapper.Map<Algorithm, AlgorithmResource>(result.Resource);
            return Ok(AlgorithmResource);
        }

        [HttpGet("algorithms/{algorithmId}")]
        [SwaggerOperation(
            Summary = "Get Algorithm",
            Description = "Get Algorithm In the Data Base by id",
            OperationId = "GetAlgorithm"
        )]
        [SwaggerResponse(200, "Returned Algorithm", typeof(AlgorithmResource))]
        [ProducesResponseType(typeof(AlgorithmResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> GetActionAsync(Guid algorithmId)
        {
            var result = await _algorithmService.GetById(algorithmId);
            if (!result.Success)
                return BadRequest(result.Message);
            var AlgorithmResource = _mapper.Map<Algorithm, AlgorithmResource>(result.Resource);
            return Ok(AlgorithmResource);
        }

        [HttpDelete("algorithms/{algorithmId}")]
        [SwaggerOperation(
            Summary = "Delete Algorithm",
            Description = "Delete Algorithm In the Data Base by id",
            OperationId = "DeleteAlgorithm"
        )]
        [SwaggerResponse(200, "Deleted Algorithm", typeof(AlgorithmResource))]
        [ProducesResponseType(typeof(AlgorithmResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid algorithmId)
        {
            var result = await _algorithmService.Delete(algorithmId);
            if (!result.Success)
                return BadRequest(result.Message);
            var AlgorithmResource = _mapper.Map<Algorithm, AlgorithmResource>(result.Resource);
            return Ok(AlgorithmResource);
        }

        [HttpGet("algorithms")]
        [SwaggerOperation(
           Summary = "Get All Algorithms",
           Description = "Get All Algorithms In the Data Base",
           OperationId = "GetAllAlgorithms"
        )]
        [SwaggerResponse(200, "Returned All Algorithms", typeof(IEnumerable<AlgorithmResource>))]
        [ProducesResponseType(typeof(IEnumerable<AlgorithmResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<AlgorithmResource>> GetAllAsync()
        {
            var Algorithms = await _algorithmService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Algorithm>, IEnumerable<AlgorithmResource>>(Algorithms);
            return resources;
        }

        // HTTP Methods for User Entity

        [HttpGet("users/{userId}/algorithms")]
        [SwaggerOperation(
           Summary = "Get All Algorithms by UserId",
           Description = "Get All Algorithms In the DataBase by UserId",
           OperationId = "GetAllAlgorithmsByUserId"
        )]
        [SwaggerResponse(200, "Returned All Algorithms", typeof(IEnumerable<Algorithm>))]
        [ProducesResponseType(typeof(IEnumerable<AlgorithmResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<AlgorithmResource>> GetAllByUserId(Guid userId)
        {
            var Algorithms = await _algorithmService.ListByUserId(userId);
            var resources = _mapper.Map<IEnumerable<Algorithm>, IEnumerable<AlgorithmResource>>(Algorithms);
            return resources;
        }

        [HttpPost]
        [Route("users/{userId}/algorithms/{algorithmId}")]
        [SwaggerOperation(
            Summary = "Assign Algorithm to user",
            Description = "Assign Algorithm to user by AlgorithmId and userId",
            OperationId = "AssignAlgorithm To User"
        )]
        [SwaggerResponse(200, "userConcet to user Assigned", typeof(AlgorithmResource))]
        [ProducesResponseType(typeof(AlgorithmResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> AssignAlgorithmToUser(Guid userId, Guid algorithmId)
        {
            var result = await _algorithmService.AssignAlgorithmToUser(userId, algorithmId);
            if (!result.Success)
                return BadRequest(result.Message);
            var Algorithm = await _algorithmService.GetById(result.Resource.Id);
            var AlgorithmResource = _mapper.Map<Algorithm, AlgorithmResource>(Algorithm.Resource);
            return Ok(AlgorithmResource);
        }

        [HttpDelete("users/{userId}/algorithms/{algorithmId}")]
        [SwaggerOperation(
            Summary = "Unassign userConcet to user",
            Description = "Unassign Algorithm to user by AlgorithmId and userId",
            OperationId = "UnassignAlgorithm To User"
        )]
        [SwaggerResponse(200, "userConcet to user Unassigned", typeof(AlgorithmResource))]
        [ProducesResponseType(typeof(AlgorithmResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> UnassignAlgorithmToUser(Guid userId, Guid algorithmId)
        {
            var result = await _algorithmService.UnassignAlgorithmToUser(userId, algorithmId);
            if (!result.Success)
                return BadRequest(result.Message);
            var Algorithm = await _algorithmService.GetById(result.Resource.Id);
            var AlgorithmResource = _mapper.Map<Algorithm, AlgorithmResource>(Algorithm.Resource);
            return Ok(AlgorithmResource);
        }

    }
}
