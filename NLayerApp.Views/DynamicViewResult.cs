using Microsoft.AspNetCore.Mvc;

namespace NLayerApp.Views
{
    public class DynamicViewResult : ViewResult
    {
        public new dynamic Model { get; set; }

    }
}