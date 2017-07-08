using System;

namespace PlayCat.ApiModel
{
    public class AuthToken
    {
        public Guid Id { get; set; }

        public DateTime DateExpired { get; set; }
    }
}
