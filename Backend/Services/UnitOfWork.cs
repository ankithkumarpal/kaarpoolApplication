using DataLayer;
using Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Runtime.InteropServices;

namespace CarPool.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private CarPoolContext _context;
        private ILog _logger;
        public IAuthRepository authRepository { get; set; }
        public IBookRideRepository bookRideRepository { get; set; }
        public IOfferRideRepository offerRideRepository { get; set; }
        public UnitOfWork (CarPoolContext context, ILog logger)
        {
            _context = context;
            _logger = logger;
            authRepository = new AuthRepository(context , logger);
            bookRideRepository = new BookRideRepository(context , logger);
            offerRideRepository = new OfferRideRepository(context ,logger);
        } 

        public void  SaveChanges()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.SaveChanges();
                transaction.Commit();
            }catch(Exception ex)
            {
                _logger.Log(ex.Message);
                transaction.Rollback();
            }
        }
        
    }
}
