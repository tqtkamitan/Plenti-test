using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserClass
{
    public interface IUserMatch
    {
        bool IsMatch(User newUser, User existingUser);
    }
}
