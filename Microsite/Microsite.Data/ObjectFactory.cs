using Microsite.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsite.Data
{
    class ObjectFactory
    {
        public static UserRegisterDTO CreateNewUserObject(UserRegisterDTO user)
        {
            return new UserRegisterDTO
            {
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                Contact = user.Contact,
                Image = user.Image,
                Country = user.Country,
            };
        }
    }
}
