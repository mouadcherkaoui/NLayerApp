﻿using Microsoft.AspNetCore.Mvc;
using NLayerApp.Infrastructure.Controllers;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using NLayerApp.Infrastructure.Repositories;
using NLayerApp.Controllers.Attributes;
using NLayerApp.Views.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerApp.Controllers
{
    [GenericControllerNameConvention(typeof(RepositoryController<,,>))]
    [Route("[controller]")]
    public class RepositoryController<TContext, TEntity, TKey>
        : Controller, IRepositoryController<TEntity, TKey, IActionResult>
        where TContext : class, IContext
        where TEntity : class, IEntity, IEntity<TKey>, new()
    {
        IRepository<TEntity, TKey> _repository;

        public RepositoryController(IRepository<TEntity, TKey> repository)
        {
            _repository = repository;
        }

        [Route("")]
        [Route("[action]")]
        [HttpGet]
        public IActionResult Index()
        {            
            var result = _repository.GetEntities();  
            
            var viewResult = View("Dynamic/Index", 
                new DynamicEntityComponentViewModel(result){
                    ModelType = typeof(TEntity)
                });

            return viewResult; 
        }

        [Route("Index/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(TKey id)
        {
            var entity = await _repository.GetEntity(id);

            if ( entity == null) return NotFound();

            var viewResult = View("Dynamic/Details", 
                new EntityComponentViewModel()
                    { 
                        ModelType = typeof(TEntity), 
                        ModelInstance = entity
                    }
                );   

            return viewResult;
        }
        
        [HttpGet("Add")]
        public IActionResult Add()
        {
            var viewResult = View("Dynamic/Add", 
                new DynamicEntityComponentViewModel(new TEntity())
                    { 
                        ModelType = typeof(TEntity),
                        ViewType = "Edit"
                    }
                );

            return viewResult;
        }

        [HttpPost("Add")]
        [ValidateAntiForgeryToken()]
        public IActionResult Add(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                _repository.AddEntity(entity);
                return RedirectToAction("Index");
            }     

            var viewResult = View("Dynamic/Add", 
                new EntityComponentViewModel()
                    { 
                        ModelType = typeof(TEntity), 
                        ModelInstance = entity,
                        ViewType = "Edit"
                    }
                );

            return viewResult;
        }

        [HttpGet("Update/{id}")]
        public IActionResult Update(TKey id)
        {
            var result = _repository.GetEntity(id);
            
            if(result == null) return NotFound();

            var viewResult = View("Dynamic/Update", 
                new DynamicEntityComponentViewModel(result)
                    { 
                        ViewType = "Edit",
                        ModelType = typeof(TEntity)
                    }
                );

            return viewResult;
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken()]
        public IActionResult Update(TKey id, [FromForm]TEntity entity)
        {
            if(ModelState.IsValid)
            {
                entity.Id = id;
                _repository.UpdateEntity(entity);
                return RedirectToAction("Index", new { id = entity.Id });
            }

            var viewResult = View("Dynamic/Update", 
                new EntityComponentViewModel()
                    { 
                        ModelType = typeof(TEntity), 
                        ModelInstance = entity,
                        ViewType = "Edit"
                    }
                );

            return viewResult;
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(TKey id)
        {
            var result = await _repository.DeleteEntityAsync(id);
            if(result)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }   

        private IActionResult EntityView(EntityComponentViewModel model)
        {
            return View(model);
        }
    }
}
