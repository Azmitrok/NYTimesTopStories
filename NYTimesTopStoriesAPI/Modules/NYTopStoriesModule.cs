using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYTimesTopStoriesAPI.Modules
{
    public class NYTopStoriesModule : NancyModule
    {
        public NYTopStoriesModule()
        {
            Get("/", _ => "Ento moe!!");
            Get("/about", _ => "About - Ento moe!!");
        }
    }
}
