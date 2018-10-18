namespace SIS.WebServer
{
    public class MvcContext
    {
        private static MvcContext Instance;

        private MvcContext()
        {
        }

        public static MvcContext Get => Instance == null ? (Instance = new MvcContext()) : Instance;

        public string AssemblyName { get; set; }

        public string ControllerFolder = "Controllers";

        public string ControllerSuffix = "Controller";

        public string ViewsFolder = "Views";

        public string ModelsFolder = "Models";
    }
}
