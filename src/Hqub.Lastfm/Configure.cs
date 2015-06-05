using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hqub.Lastfm
{
   public static class Configure
    {
       static Configure()
       {
           Delay = 1000;
       }

       /// <summary>
       /// Set value delay between requests.
       /// </summary>
       public static int Delay { get; set; }
    }
}
