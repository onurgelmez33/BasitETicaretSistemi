using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.System
{
    public interface IUrlService
    {
        string GenerateSlug(string name);
    }
}
