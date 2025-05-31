using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string PartnerNo { get; set; } = string.Empty;
        public string PartnerPassword { get; set; } = string.Empty;
    }
}