using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Final.Domain.Entities;
using Final.Service.Services;
using Final.Service.Validators;

namespace Final.Application.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
    private BaseService<Produto> service = new BaseService<Produto>();

    [HttpPost]
    public IActionResult Post([FromBody] Produto item)
    {
        try
        {
            service.Post<ProdutoValidator>(item);
            
            return new ObjectResult(item.Id);
        }
        catch(ArgumentNullException ex)
        {
            return NotFound(ex);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut]
    public IActionResult Put([FromBody] Produto item)
    {
        try
        {
            service.Put<ProdutoValidator>(item);

            return new ObjectResult(item);
        }
        catch(ArgumentNullException ex)
        {
            return NotFound(ex);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
    /// <summary>
    /// Deletes a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>     
    [HttpDelete("{id}")]
     public IActionResult Delete(int id)
    {
        try
        {
            service.Delete(id);

            return new NoContentResult();
        }
        catch(ArgumentException ex)
        {
            return NotFound(ex);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return new ObjectResult(service.Get());
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            return new ObjectResult(service.Get(id));
        }
        catch(ArgumentException ex)
        {
            return NotFound(ex);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
    }
}