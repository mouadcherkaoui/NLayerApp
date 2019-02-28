using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.Views
{
    public class DynamicViewResult : ViewResult
    {
        public new dynamic Model { get; set; }

    }
}