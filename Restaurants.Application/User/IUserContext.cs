using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.User
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}
