using Microsoft.AspNetCore.Mvc;

namespace NLayerdApp.Views
{
    public class DynamicViewResult : ViewResult
    {
        public new dynamic Model { get; set; }

    }
}