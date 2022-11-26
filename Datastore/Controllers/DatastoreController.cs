using Datastore.Exceptions;
using Datastore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Datastore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DatastoreController : ControllerBase
{
    private readonly ILogger<DatastoreController> _logger;
    private readonly IStorage _partitionLeader;

    public DatastoreController(ILogger<DatastoreController> logger, IStorage partitionLeader)
    {
        _logger = logger;
        _partitionLeader = partitionLeader;
    }

    [HttpGet("getAll")]
    public IActionResult Get()
    {
        try
        {
            var result = _partitionLeader.Get();
            return Ok(result);
        }
        catch (BadRequest e)
        {
            _logger.LogWarning(e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get([FromRoute] Guid id)
    {
        try
        {
            var result = _partitionLeader.Get(id);
            return Ok(result);
        }
        catch (NotFound e)
        {
            _logger.LogWarning(e.Message);
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] Dictionary<string, string> data)
    {
        try
        {
            var result = _partitionLeader.Post(data);
            return Ok(result);
        }
        catch (BadRequest e)
        {
            _logger.LogWarning(e.Message);
            return BadRequest(e.Message);
        }

    }

    [HttpPut("{id}")]
    public IActionResult Put([FromBody] Dictionary<string, string> data, [FromRoute] Guid id)
    {
        try
        {
            var result = _partitionLeader.Put(data, id);
            return Ok(result);
        }
        catch (NotFound e)
        {
            _logger.LogWarning(e.Message);
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        try
        {
            _partitionLeader.Delete(id);
            return Ok();
        }
        catch (BadRequest e)
        {
            _logger.LogWarning(e.Message);
            return BadRequest(e.Message);
        }
    }
}