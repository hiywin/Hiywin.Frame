using Hiywin.Common.IoC;
using Hiywin.Common.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Utility
{
    public class IoCJwtOpraters
    {
        public static void Init()
        {
            IoCContainer.Register<IJwtFactory, JwtFactory>();
        }
    }
}
