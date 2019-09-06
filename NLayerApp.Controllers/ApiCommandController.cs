using System;
using System.Threading.Tasks;
using NLayerApp.Infrastructure.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace NLayerApp.Controllers
{
    public class ApiCommandController: Controller
    {
        Lazy<IAsyncCommand<Task<IActionResult>>> _indexCommand;
        public ApiCommandController(params Lazy<IAsyncCommand<Task<IActionResult>>>[] commands)
        {
            _indexCommand = commands[0];   

        }

        public Task<IActionResult> Index => _indexCommand.Value.ExecuteAsync();
    }
}