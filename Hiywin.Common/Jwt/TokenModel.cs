﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Common.Jwt
{
    public class TokenModel
    {
        public string auth_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
}
