namespace NLayerAppp.Controllers.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class GeneratedControllerAttribute : System.Attribute
    {
        // See the attribute guidelines at
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string route;
        
        // This is a positional argument
        public GeneratedControllerAttribute(string route)
        {
            this.route = route;
        }
        
        public string Route
        {
            get { return route; }
        }
    }
}
