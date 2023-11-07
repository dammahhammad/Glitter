using System;
using System.Collections.Generic;
using System.Text;

namespace Microsite.Shared
{
    public class UserRegisterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
    }
}
