using System;
using System.Collections.Generic;
using System.Text;

namespace CogsLite.Core
{
    public interface IUserStore
    {
        User Get(String emailAddress);
        void Add(User user);
    }
}
