using Apilot.Application.DTOs.Envirenoment;
using Apilot.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apilot.Application.DTOs.Environment;

namespace Apilot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnvironmentController : ControllerBase
{
    private readonly IEnvironmentService _environmentService;
    private readonly ILogger<EnvironmentController> _logger;

    public EnvironmentController(
        IEnvironmentService environmentService,
        ILogger<EnvironmentController> logger)
    {
        _environmentService = environmentService ?? throw new ArgumentNullException(nameof(environmentService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [ProducesResponseType(typeof(EnvironmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EnvironmentDto>> CreateEnvironment([FromBody] CreateEnvironmentRequest createEnvironmentRequest)
    {
        try
        {
            _logger.LogInformation("Received request to create environment");
            var result = await _environmentService.CreateEnvironmentAsync(createEnvironmentRequest);
            return CreatedAtAction(nameof(GetEnvironmentById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating environment");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the environment");
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EnvironmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EnvironmentDto>>> GetAllEnvironments()
    {
        try
        {
            _logger.LogInformation("Received request to get all environments");
            var environments = await _environmentService.GetAllEnvironmentsAsync();
            return Ok(environments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all environments");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving environments");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EnvironmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EnvironmentDto>> GetEnvironmentById(int id)
    {
        try
        {
            _logger.LogInformation("Received request to get environment with ID: {Id}", id);
            var environment = await _environmentService.GetEnvironmentByIdAsync(id);
            return Ok(environment);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Environment with ID {Id} not found", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving environment with ID: {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the environment");
        }
    }

    [HttpGet("workspace/{workspaceId}")]
    [ProducesResponseType(typeof(IEnumerable<EnvironmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EnvironmentDto>>> GetEnvironmentsByWorkspaceId(int workspaceId)
    {
        try
        {
            _logger.LogInformation("Received request to get environments for workspace ID: {WorkspaceId}", workspaceId);
            var environments = await _environmentService.GetEnvironmentsByWorkspaceIdAsync(workspaceId);
            return Ok(environments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving environments for workspace ID: {WorkspaceId}", workspaceId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving environments");
        }
    }

    [HttpPut("{id}/name")]
    [ProducesResponseType(typeof(EnvironmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EnvironmentDto>> UpdateEnvironmentName(int id, [FromBody] string name)
    {
        try
        {
            _logger.LogInformation("Received request to update name for environment with ID: {Id}", id);
            var environment = await _environmentService.UpdateEnvironmentAsync(id, name);
            return Ok(environment);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Environment with ID {Id} not found for update", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating name for environment with ID: {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the environment");
        }
    }

    [HttpPut("{id}/variables")]
    [ProducesResponseType(typeof(EnvironmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EnvironmentDto>> AddVariablesToEnvironment(int id, [FromBody] Dictionary<string, string> variables)
    {
        try
        {
            _logger.LogInformation("Received request to add variables to environment with ID: {Id}", id);
            var environment = await _environmentService.AddVariablesToEnvironment(id, variables);
            return Ok(environment);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Environment with ID {Id} not found for adding variables", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding variables to environment with ID: {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the environment");
        }
    }

    [HttpPut("/variables")]
    [ProducesResponseType(typeof(EnvironmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EnvironmentDto>> UpdateVariableInEnvironment([FromBody] UpdateVariableRequest request)
    {
        try
        {
            _logger.LogInformation("Received request to update variable '{Key}' in environment with ID: {Id}", request.Key, request.EnvironmentId);
            var environment = await _environmentService.UpdateVariableInEnvironmentAsync(request.EnvironmentId, request.Key, request.Value);
            return Ok(environment);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Environment or variable not found for update. ID: {Id}, Key: {Key}", request.EnvironmentId, request.Key);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating variable '{Key}' in environment with ID: {Id}", request.Key, request.EnvironmentId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the variable");
        }
    }

    [HttpPost("/variables")]
    [ProducesResponseType(typeof(EnvironmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EnvironmentDto>> AddVariableToEnvironment([FromBody] AddVariableRequest request)
    {
        try
        {
            _logger.LogInformation("Received request to add variable '{Key}' to environment with ID: {Id}", request.Key, request.EnvironmentId);
            var environment = await _environmentService.AddVariableToEnvironmentAsync(request.EnvironmentId, request.Key, request.Value);
            return Ok(environment);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Environment with ID {Id} not found for adding variable", request.EnvironmentId);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding variable to environment with ID: {Id}", request.EnvironmentId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the variable");
        }
    }

    [HttpDelete("/variables")]
    [ProducesResponseType(typeof(EnvironmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EnvironmentDto>> RemoveVariableFromEnvironment(int id, string key)
    {
        try
        {
            _logger.LogInformation("Received request to remove variable '{Key}' from environment with ID: {Id}", key, id);
            var environment = await _environmentService.RemoveVariableFromEnvironmentAsync(id, key);
            return Ok(environment);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Environment or variable not found for deletion. ID: {Id}, Key: {Key}", id, key);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing variable '{Key}' from environment with ID: {Id}", key, id);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while removing the variable");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteEnvironment(int id)
    {
        try
        {
            _logger.LogInformation("Received request to delete environment with ID: {Id}", id);
            await _environmentService.DeleteEnvironmentAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Environment with ID {Id} not found for deletion", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting environment with ID: {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the environment");
        }
    }
}





