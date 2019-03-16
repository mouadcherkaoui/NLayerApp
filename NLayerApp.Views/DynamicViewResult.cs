using Microsoft.AspNetCore.Mvc;

namespace NLayerAppp.Views
{
    public class DynamicViewResult : ViewResult
    {
        public new dynamic Model { get; set; }

    }
}