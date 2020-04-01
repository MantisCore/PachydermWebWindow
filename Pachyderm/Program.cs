using WebWindows.Blazor;
using System;

namespace Pachyderm
{
    public class Program
    {
        static void Main(string[] args)
        {
            ComponentsDesktop.Run<Startup>("Pachyderm", "wwwroot/index.html");
        }
    }
}
