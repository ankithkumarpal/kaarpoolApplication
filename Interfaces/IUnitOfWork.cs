using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public  interface IUnitOfWork
    {

        public IAuthRepository authRepository { get; set; }
        public IBookRideRepository bookRideRepository { get; set; }
        public IOfferRideRepository offerRideRepository { get; set; }
        public void SaveChanges();
    }
}
