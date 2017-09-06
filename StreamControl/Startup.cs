using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using StreamControl.Data;

[assembly: OwinStartup(typeof(StreamControl.Startup))]

namespace StreamControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
