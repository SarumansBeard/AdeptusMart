using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;

namespace AdeptusMart03.BusinessAccessLayer.Services
{
    public class ServiceService
    {
        private readonly IRepository<Account> _accountRepo;

        public ServiceService(IRepository<Account> accountRepo)
        {
            _accountRepo = accountRepo;
        }


        public async Task GetAccountDetails()
        {

        }




    }
}
