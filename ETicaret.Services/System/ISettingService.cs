﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.System
{
    public interface ISettingService
    {
        T GetSetting<T>(string key) where T : IConvertible;
    }
}
