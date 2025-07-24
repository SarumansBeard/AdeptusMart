using AdeptusMart01.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdeptusMart02.DataAccessLayer.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Guid> GetCartIdWithSessionId(string sessionId);
    }
    
}
