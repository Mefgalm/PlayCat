using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService
{
    public class AuthService : BaseService
    {
        private readonly InviteService _inviteService;

        public AuthService(PlayCatDbContext dbContext, InviteService inviteService) : base(dbContext)
        {
            _inviteService = inviteService;
        }

        public void SignUp()
        {

        }

        public void SignIn()
        {

        }
    }
}
