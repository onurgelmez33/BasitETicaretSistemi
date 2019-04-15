using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data
{
    public enum OrderStatus
    {
        Onaylandi = 0,
        Beklemede = 1,
        Kargoda = 2,
        TeslimEdildi = 3,
        Iptal = -1,
        Iade = 4
    }
}
