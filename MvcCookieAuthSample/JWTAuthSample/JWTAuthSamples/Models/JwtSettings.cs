using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthSamples.Models
{
    public class JwtSettings
    {
        /// <summary>
        /// Token颁发者
        /// </summary>
        public string Issuser { get; set; }
        /// <summary>
        /// token可用客户端
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 加密key
        /// </summary>
        public string SecreKey { get; set; }

    }
}
