using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.Music
{
    public interface IAudioFile
    {
        string VideoId { get; set; }
        string Artist { get; set; }
        string Song { get; set; }
    }
}
