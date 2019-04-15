using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.System
{
    public class SettingService : ISettingService
    {
        private AppDbContext db = new AppDbContext();
        public T GetSetting<T>(string key) where T: IConvertible
        {
            var value = db.Settings.SingleOrDefault(f => f.Key == key).Value;
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
