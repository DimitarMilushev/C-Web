using SIS.Framework.ActionResults.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Framework.ActionResults.Contents
{
    public interface IViewable : IActionResult
    {
        IRenderable View { get; set; }
    }
}
