using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdeptusMart.DataAccess.Concrete;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;
using AdeptusMart02.DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart02.DataAccessLayer.Concrete
{
    public class EfAccountRepository : EfRepository<Account>, IAccountRepository
    {
        private readonly AdeptusMartDbContext _context;

        public EfAccountRepository(AdeptusMartDbContext context) : base (context)        {
            _context = context;
        }

        public async Task<Account> GetByCredentialsAsync(string username, string password)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x=>x.Username == username && x.Password == password);
        }
    }
}
