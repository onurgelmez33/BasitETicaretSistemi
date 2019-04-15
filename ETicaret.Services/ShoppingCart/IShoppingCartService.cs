using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.ShoppingCart
{
    public interface IShoppingCartService
    {
        void SepeteEkle(int urunId, int quantity, string username);
        void SepettenUrunSil(int urunId, string username);
        void SepetGuncelle(IEnumerable<Sepet> sepet, string username);
        void SepetAzalt(int urunId, string username);
        void SepetiBosalt(string username);
        IQueryable<Sepet> SepetiGetir(string username);
    }
}
