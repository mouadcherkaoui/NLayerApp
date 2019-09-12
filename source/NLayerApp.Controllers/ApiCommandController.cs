using System;
using System.Threading.Tasks;
using NLayerApp.Infrastructure.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace NLayerApp.Controllers
{
    public class ApiCommandController: Controller
    {
        Lazy<IAsyncCommandHandler<IActionResult>> _indexCommand;
        public ApiCommandController(params Lazy<IAsyncCommandHandler<IActionResult>>[] commands)
        {
            _indexCommand = commands[0];   
        }

        public async Task<IActionResult> Index() => await _indexCommand.Value.ExecuteAsync();
    }
}