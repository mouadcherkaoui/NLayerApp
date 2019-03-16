using Microsoft.AspNetCore.Mvc;

using NLayerdApp.Infrastructure.DataAccessLayer;
using NLayerdApp.Infrastructure.Repositories;
using NLayerdApp.Infrastructure.Models;
using NLayerdApp.Infrastructure.Controllers;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace NLayerdApp.Controllers
{
    public class ApiRepositoryController<TContext, TEntity, TKey>
        : Controller, IApiRepositoryController<TEntity, TKey, IActionResult>
        where TContext : class, IContext
        where TEntity : class, IEntity, IEntity<TKey>
    {
        IRepository<TEntity, TKey> _repository;
        public ApiRepositoryController(IRepository<TEntity, TKey> repository)
        {
            _repository = repository;
        }

        [HttpGet( "" )]
        [Produces( "application/json" )]
        public virtual IActionResult Get()
        {
            return Ok(_repository.GetEntities());
        }
        
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        public virtual IActionResult Get(TKey id)
        {
            var isHatoas = Request.Headers.ContainsKey("Accept") 
                && Request.Headers["Accept"] .ToString().ToLower().EndsWith("hateoas");
            
            var result = _repository.GetEntity(id);
            if(result == null) return NoContent();
            
            return isHatoas 
                    ? HATEOASObjectResult(result) 
                    : Ok(result);
        }
        
        [HttpPost()]
        public virtual IActionResult Post([FromBody]TEntity entity)
        {
            if (ModelState.IsValid)
            {
                _repository.AddEntity(entity);
                return CreatedAtAction("Get", new { entity });
            }
            return NoContent();
        }
        
        [HttpPut()]
        public virtual IActionResult Put([FromBody]TEntity entity)
        {
            if(ModelState.IsValid)
            {
                _repository.UpdateEntity(entity);
                return AcceptedAtAction("Get", new { entity });
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(TKey id)
        {
            var result = _repository.DeleteEntity(id);
            if(result)
            {
                return Accepted();
            }
            return NoContent();
        }    

        private ObjectResult HATEOASObjectResult(TEntity item)
        {
            var typeName = typeof(TEntity).Name;
                var link = new LinkHelper<TEntity>(item);
                link.Links = new System.Collections.Generic.List<Link>();
                link.Links.Add(new Link {
                    Href = $"/api/{typeName}/{item.Id}",
                    Rel = "self",
                    method = "GET"
                });
                link.Links.Add(new Link {
                    Href = $"/api/{typeName}/{item.Id}",
                    Rel = $"put-{typeName}",
                    method = "PUT"
                });
                link.Links.Add(new Link {
                    Href = $"/api/{typeName}/{item.Id}",
                    Rel = $"delete-{typeName}",
                    method = "DELETE"
                });

                return new ObjectResult(link);
        }  
    }
}