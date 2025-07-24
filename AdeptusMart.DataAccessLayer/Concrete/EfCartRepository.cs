using AdeptusMart.DataAccess.Concrete;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;
using AdeptusMart02.DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdeptusMart02.DataAccessLayer.Concrete
{
    public class EfCartRepository : EfRepository<Cart> , ICartRepository 
    {
        private readonly AdeptusMartDbContext _context;

        public EfCartRepository(AdeptusMartDbContext context) : base(context)
        {
            _context = context;
        }

        
        public async Task<Guid> GetCartIdWithSessionId(string sessionId)
        {
            var userId = await _context.Accounts
                .Where(x => x.SessionId == sessionId)
                .Select(x=>x.Id)
                .FirstOrDefaultAsync();

            bool IsCartExists = await _context.Carts
                .AnyAsync(x => x.UserId == userId);

            if (!IsCartExists)
            {
                var newCart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow                    
                };
                await _context.Carts.AddAsync(newCart);
                await _context.SaveChangesAsync();
                return newCart.Id                    ;
            }
            else
            {
                return await _context.Carts
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();        
            }
        }

       
    }
}
