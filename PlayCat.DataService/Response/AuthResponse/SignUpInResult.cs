using PlayCat.ApiModel;

namespace PlayCat.DataService.Response
{
    public class SignUpInResult : BaseResult
    {
        public SignUpInResult() : base(new BaseResult())
        {
        }

        public SignUpInResult(BaseResult baseResult) : base(baseResult)
        {
        }

        public User User { get; set; }

        public AuthToken AuthToken { get; set; }
    }
}
