using Autofac;
using Autofac.Core;
using Nop.Core.Caching;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Web.DallasArt.Context;
//using Nop.Web.DallasArt.Controllers.Customer.Classes;
using Nop.Web.DallasArt.Events.Responders;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Services;


namespace Nop.Web.DallasArt.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string ContextName = "nop_object_context_dallasart_contact_request";

        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            // services

            builder.RegisterType<ConstantContactService>().As<IConstantContactService>().InstancePerLifetimeScope();
            builder.RegisterType<ContactRequestService>().As<IContactRequestService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageService>().As<IMessageService>().InstancePerLifetimeScope();
        //    builder.RegisterType<EnrollmentService>().As<IEnrollmentRequestService>().InstancePerLifetimeScope();
       //     builder.RegisterType<ProcessRegistation>().As<IProcessRegistration>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRoleService>().As<ICustomerRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderTrackingService>().As<IOrderTrackingService>().InstancePerLifetimeScope();

            builder.RegisterType<CustomerServices>().As<ICustomerServices>().InstancePerLifetimeScope();
            builder.RegisterType<MembershipService>().As<IMembershipService>().InstancePerLifetimeScope();
            builder.RegisterType<EntityTokensAddedEventConsumerCustomer>().As<IEntityTokensAddedEventConsumerCustomer>().InstancePerLifetimeScope();


          //  builder.RegisterType<Import>()
          //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));


            //work context
            builder.RegisterType<DallasArtContext>().As<IDallasArtContext>().InstancePerLifetimeScope();

   
            ////override required repository with our custom context
            //builder.RegisterType<EfRepository<ContactRequest>>()
            //    .As<IRepository<ContactRequest>>()
            //    .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ContextName))
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<EfRepository<EnrollmentLocation>>()
            //    .As<IRepository<EnrollmentLocation>>()
            //    .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ContextName))
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<EfRepository<EnrollmentPayments>>()
            //           .As<IRepository<EnrollmentPayments>>()
            //           .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ContextName))
            //           .InstancePerLifetimeScope();
        
        }


        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order => 2;
    }
}
