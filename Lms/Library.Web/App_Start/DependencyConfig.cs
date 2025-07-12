using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Library.Core.Data;
using Library.Core.Repositories;
using Library.Core.Repositories.Interfaces;
using Library.Core.Services.Books;
using Library.Core.Services.Members;
using Library.Core.Services.Report;
using Library.Core.Services.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.App_Start
{
    public class DependencyConfig
    {
        public static IContainer Container;
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // Register MVC controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register your repository and interface
            builder.RegisterType<BookRepository>().As<IBookRepository>().InstancePerRequest();
            builder.RegisterType<BookService>().As<IBookService>().InstancePerRequest();
            builder.RegisterType<MemberRepository>().As<IMemberRepository>().InstancePerRequest();
            builder.RegisterType<MemberService>().As<IMemberService>().InstancePerRequest();
            builder.RegisterType<AuthRepository>().As<IAuthRepository>().InstancePerRequest();
            builder.RegisterType<LoginService>().As<ILoginService>().InstancePerRequest();
            builder.RegisterType<ReportService>().As<IReportService>().InstancePerRequest();

            Container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}