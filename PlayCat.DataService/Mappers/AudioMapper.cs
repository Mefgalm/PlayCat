using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService.Mappers
{
    public static class AudioMapper
    {
        public static class ToApi
        {
            public static ApiModel.Audio Get(DataModel.Audio audio)
            {
                return audio == null ? null : new ApiModel.Audio
                {

                };
            }
        }
    }
}
