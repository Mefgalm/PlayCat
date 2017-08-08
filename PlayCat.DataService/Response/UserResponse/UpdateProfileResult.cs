using PlayCat.ApiModel;

namespace PlayCat.DataService.Response
{
    public class UpdateProfileResult : BaseResult
    {
        public UpdateProfileResult() : base(new BaseResult())
        {
        }

        public UpdateProfileResult(BaseResult baseResult) : base(baseResult)
        {
        }

        public User User { get; set; }
    }
}
