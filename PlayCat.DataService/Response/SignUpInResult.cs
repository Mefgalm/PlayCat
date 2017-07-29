using PlayCat.ApiModel;

namespace PlayCat.DataService.Response
{
    public class SignUpInResult : BaseResult
    {
        public User User { get; set; }

        public AuthToken AuthToken { get; set; }
    }
}
