using EFDemo.Model;
using EFDemo.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EFDemo.UnitOfWork
{
    /// <summary>  
    /// Unit of Work class responsible for DB transactions  
    /// </summary>  
    public class UnitOfWork : IDisposable
    {
        #region Private member variables...  

        //private EFDemoContext _context = null;
        private readonly EFDemoContext _context;
        private GenericRepository<User> _userRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Token> _tokenRepository;
        private GenericRepository<Owner> _ownerRepository;
        private GenericRepository<Account> _accountRepository;
        #endregion

        public IConfiguration Configuration { get; }

        public UnitOfWork()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var section = config.GetSection("ConnectionStrings");

            var connectionstring = section.GetSection("ConStr").Value;
            
            var optionsBuilder = new DbContextOptionsBuilder<EFDemoContext>();
            optionsBuilder.UseSqlServer(connectionstring);

            this._context = new EFDemoContext(optionsBuilder.Options);
        }

        #region Public Repository Creation properties...  

        /// <summary>  
        /// Get/Set Property for product repository.  
        /// </summary>  
        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                    this._productRepository = new GenericRepository<Product>(this._context);
                return this._productRepository;
            }
        }

        /// <summary>  
        /// Get/Set Property for user repository.  
        /// </summary>  
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<User>(this._context);
                return this._userRepository;
            }
        }

        /// <summary>  
        /// Get/Set Property for token repository.  
        /// </summary>  
        public GenericRepository<Token> TokenRepository
        {
            get
            {
                if (this._tokenRepository == null)
                    this._tokenRepository = new GenericRepository<Token>(this._context);
                return this._tokenRepository;
            }
        }

        /// <summary>  
        /// Get/Set Property for Owner repository.  
        /// </summary>  
        public GenericRepository<Owner> OwnerRepository
        {
            get
            {
                if (this._ownerRepository == null)
                    this._ownerRepository = new GenericRepository<Owner>(this._context);
                return this._ownerRepository;
            }
        }

        /// <summary>  
        /// Get/Set Property for Account repository.  
        /// </summary>  
        public GenericRepository<Account> AccountRepository
        {
            get
            {
                if (this._accountRepository == null)
                    this._accountRepository = new GenericRepository<Account>(this._context);
                return this._accountRepository;
            }
        }
        #endregion

        #region Public member methods...  
        /// <summary>  
        /// Save method.  
        /// </summary>  
        public void Save()
        {
            _context.SaveChanges();
            //try
            //{
            //    _context.SaveChanges();
            //}
            //catch (DbEntityValidationException e)
            //{

            //    var outputLines = new List<string>();
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
            //        }
            //    }
            //    System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

            //    throw e;
            //}

        }

        #endregion

        #region Implementing IDiosposable...  

        #region private dispose variable declaration...  
        private bool disposed = false;
        #endregion

        /// <summary>  
        /// Protected Virtual Dispose method  
        /// </summary>  
        /// <param name="disposing"></param>  
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>  
        /// Dispose method  
        /// </summary>  
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
