using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdeptusMart01.Core.Entities;

namespace AdeptusMart02.DataAccessLayer.Abstract
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByCredentialsAsync(string username, string password);

        Task<Guid> GetUserIdWithSesionId(string sessionId);
    }

}
