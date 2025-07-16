using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart03.BusinessAccessLayer.Services
{
    public class CartService
    {
        private readonly IRepository<CartItem> _cartItemRepo;
        private readonly IRepository<Cart> _cartRepo;
        
       
        public CartService(IRepository<CartItem> cartItemRepo, IRepository<Cart> cartRepo)
        {
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
        }

        
        public async Task AddToCartService(Guid productId, int quantity)
        {
            if (productId == Guid.Empty || quantity <= 0)
            {
               return;
            }

            try
            {
                var existingCart = new Cart
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                    
                };

                await _cartRepo.AddAsync(existingCart);


                var newCartItem = new CartItem
                {
                    CartId = existingCart.Id,
                    Id = productId,
                    Quantity = quantity
                };

                await _cartItemRepo.AddAsync(newCartItem);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Cart eklenirken hata: {ex.Message}");
                throw;
            }
            
            
            


            


           
           


            return;
        }



    }
}
