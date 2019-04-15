using Autofac;
using Autofac.Integration.Mvc;
using ETicaret.Data;
using ETicaret.Services.Catalog;
using ETicaret.Services.CMS;
using ETicaret.Services.Kullanici;
using ETicaret.Services.Mesajlasma;
using ETicaret.Services.Order;
using ETicaret.Services.ShoppingCart;
using ETicaret.Services.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ETicaret.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Autofac.IContainer _appContainer;
        protected void Application_Start()
        {
            AutofacCalistir();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            if (HttpContext.Current.User == null)
            {
                CreateGuestMember();
            }
        }
        private void CreateGuestMember()
        {
            var _customerService = _appContainer.Resolve<IKullaniciService>();
            var ipAddress = HttpContext.Current.Request.UserHostAddress;
            if (!_customerService.EmailKontrolEt(ipAddress))
            {
                _customerService.CreateGuestMember(ipAddress);
                FormsAuthentication.SetAuthCookie(ipAddress, true);
            }
        }

        private void AutofacCalistir()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<AppDbContext>().As<AppDbContext>();
            builder.RegisterType<KullaniciService>().As<IKullaniciService>();
            builder.RegisterType<EmailSenderService>().As<IEmailSenderService>();
            builder.RegisterType<SettingService>().As<ISettingService>();
            builder.RegisterType<KategoriService>().As<IKategoriService>();
            builder.RegisterType<UrunService>().As<IUrunService>();
            builder.RegisterType<PictureService>().As<IPictureService>();
            builder.RegisterType<SlideshowService>().As<ISlideshowService>();
            builder.RegisterType<UrlService>().As<IUrlService>();
            builder.RegisterType<BlogService>().As<IBlogService>();
            builder.RegisterType<RouteService>().As<IRouteService>();
            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>();
            builder.RegisterType<AddressService>().As<IAddressService>();
            builder.RegisterType<OrderService>().As<IOrderService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            _appContainer = container;
        }

        protected void Application_PostAuthenticateRequest()
        {
            if (FormsAuthentication.CookiesSupported)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        var _customerService = _appContainer.Resolve<IKullaniciService>();
                        string userName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        var customer = _customerService.GetUserByEmail(userName);
                        string role = customer.Role;
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(userName, "Forms"), role.Split(';'));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
