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
        private readonly IRepository<Account> _accountRepo;
        
       
        public CartService(IRepository<CartItem> cartItemRepo, IRepository<Cart> cartRepo, IRepository<Account> _accountRepo)
        {
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _accountRepo = _accountRepo;
        }

        public async Task<List<CartItem>> ShowCartItems(string sessionId)
        {
            var allUserId = await _accountRepo.GetAllAsync();

            var sessionUserId = allUserId
                .Where(a => a.SessionId == sessionId && a.IsSignIn == true)
                .Select(a => a.Id).FirstOrDefault();

            var allCartId = await _cartRepo.GetAllAsync();

            var sessionCartId = allCartId
                .Where(x=>x.UserId == sessionUserId)
                .Select(x => x.Id).FirstOrDefault();

            if (sessionUserId == Guid.Empty)
            {
                return new List<CartItem>();
            }
            try
            {
                var allcartItems = await _cartItemRepo.GetAllAsync();

                var userCartItems = allcartItems
                    .Where(x=>x.CartId == sessionCartId)
                    .ToList();

                return userCartItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sepet öğeleri alınırken hata: {ex.Message}");
                throw;
            }



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
