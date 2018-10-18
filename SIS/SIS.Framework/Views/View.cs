using SIS.Framework.ActionResults.Contents;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SIS.Framework.Views
{
    public class View : IRenderable
    {
        private readonly string fullyQualifiedTemplateName;

        private readonly IDictionary<string, object> viewData;

        public View(string fullyQualifiedTemplateName, IDictionary<string,object> viewData)
        {
            //Path??
            this.fullyQualifiedTemplateName = fullyQualifiedTemplateName;
            this.viewData = viewData;
        }

        private string ReadFile()
        {
            if (!IsExisting(this.fullyQualifiedTemplateName))
                throw new FileNotFoundException($"View does not exist {fullyQualifiedTemplateName}");

            return File.ReadAllText(this.fullyQualifiedTemplateName);
        }

        public string Render()
        {
            string fullHtml = this.ReadFile();
            string renderedHtml = this.RenderHtml(fullHtml);

            return renderedHtml;
        }

        private string RenderHtml(string fullHtml)
        {
            if(this.viewData.Any())
            {
                foreach(var parameter in this.viewData)
                {
                    fullHtml = fullHtml
                        .Replace($"{{{{{parameter.Key}}}}}",
                        parameter.Value.ToString());
                }
            }

            return fullHtml;
        }

        private bool IsExisting(string filePath)
        {
            if (File.Exists(filePath))
                return true;

            return false;
        }
    }
}
