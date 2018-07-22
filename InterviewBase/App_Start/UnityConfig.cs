using InterviewBase.Models;
using InterviewBase.Services.Abstractions.DbSevice;
using InterviewBase.Services.Infastructure.DbService;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace InterviewBase.App_Start
{
    public class UnityConfig
    {
        public static IUnityContainer Container { get; internal set; }

        public static void RegisterComponents()
        {
            Container = new UnityContainer();
            Container.RegisterType<MarketplaceContext>();
            Container.RegisterType<IProductTypeService, ProductTypeService>();
            Container.RegisterType<ICustomerService, CustomerService>();
            Container.RegisterType<IEmployeeService, EmployeeService>();
            Container.RegisterType<IOrderService, OrderService>();
            Container.RegisterType<IProductService, ProductService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }
    }
}