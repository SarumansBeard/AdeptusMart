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
        private readonly ICartRepository _cartRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IProductRepository _productRepo;        


        public CartService(IRepository<CartItem> cartItemRepo, ICartRepository cartRepo, IAccountRepository accountRepo, IProductRepository productRepo)
        {
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _accountRepo = accountRepo;
            _productRepo = productRepo;
        }

        public async Task<List<CartItem>> ShowCartItems(string userId)
        {
            Guid userIdGuid = Guid.Parse(userId);

            var allCartId = await _cartRepo.GetAllAsync();

            var cartId = allCartId
                .Where(x=>x.UserId == userIdGuid)
                .Select(x => x.Id).FirstOrDefault();

            if (userIdGuid == Guid.Empty)
            {
                return new List<CartItem>();
            }
            try
            {
                var allcartItems = await _cartItemRepo.GetAllAsync();

                var userCartItems = allcartItems
                    .Where(x=>x.CartId == cartId)
                    .ToList();

                return userCartItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sepet öğeleri alınırken hata: {ex.Message}");
                throw;
            }



        }



        
        public async Task AddToCartService(Guid productId, int quantity,string userId)
        {
            
            if (productId == Guid.Empty || quantity <= 0 || userId == null)
            {
               return;
            }          

            var cartId = await _cartRepo.GetCartIdWithUserId(userId);
           
            var cartItems = await _cartRepo.GetCartItemsByCartId(cartId);

            var cartItemName = await _productRepo.GetProductNameById(productId);

            try
            {
                if(cartItems.Where(x=>x.ProductId == productId).Any())
                {
                    var existingCartItem = cartItems.FirstOrDefault(x => x.ProductId == productId);
                    if (existingCartItem != null)
                    {
                        existingCartItem.Quantity += quantity;
                        await _cartItemRepo.UpdateAsync(existingCartItem);
                        return;
                    }
                }
                
                CartItem cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cartId,
                    Quantity = quantity,
                    ProductId = productId,
                    ProductName = cartItemName
                };

                await _cartItemRepo.AddAsync(cartItem);
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
