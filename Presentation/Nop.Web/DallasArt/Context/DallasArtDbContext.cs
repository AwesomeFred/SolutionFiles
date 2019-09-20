using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Nop.Core;
using Nop.Data;
using Nop.Data.Mapping.DallasArt;


namespace Nop.Web.DallasArt.Context
{


    public class DallasArtObjectContext : DbContext , IDbContext  
    {

        #region Ctor

        public DallasArtObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //((IObjectContextAdapter) this).ObjectContext.ContextOptions.LazyLoadingEnabled = true;
        }
        #endregion

        #region Implementation of IDbContext

        #endregion

        #region Utilities
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContactRequestMap());
            modelBuilder.Configurations.Add(new MemberEnrollmentMap());
            modelBuilder.Configurations.Add(new MemberEnrollmentPaymentsMap());


            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Methods


        private string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity 
        {
            return base.Set<TEntity>(); 
        }


        public void Install()
        {
            // It's required to set initializer to null (for SQL Server Compact).
            // otherwise, you'll get something like "The model backing the 'your context name' 
            // context has changed since the database was created. Consider using Code First 
            // Migrations to update the database"

            Database.SetInitializer<DallasArtObjectContext>(null);

            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());
            SaveChanges();
        }


        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public bool ProxyCreationEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Properties
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public bool AutoDetectChangesEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion



  
    }


}