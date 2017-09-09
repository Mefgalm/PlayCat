using PlayCat.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService.Response.UploadResponse
{
    public class UploadResult : BaseResult
    {
        public UploadResult() : base(new BaseResult())
        {
        }

        public UploadResult(BaseResult baseResult) : base(baseResult)
        {
        }

        public Audio Audio { get; set; }
    }
}
