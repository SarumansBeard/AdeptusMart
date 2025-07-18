using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart03.BusinessAccessLayer.Services
{
    public class RegisterService
    {
        private readonly IRepository<Account> _accountRepo;

        public RegisterService(IRepository<Account> accountRepo)
        {
            _accountRepo = accountRepo;
        }



        public async Task<bool> Register(Account account)
        {           
            var allUsers = await _accountRepo.GetAllAsync();

            if (allUsers.Any(u => u.Username == account.Username))
            {
                bool success = false;
                return success; 
            }


            try
            {
                Account newAccount = new Account()
                {
                    Id = Guid.NewGuid(),
                    RegisterTime = DateTime.UtcNow,
                    IsDeleted = false,
                    IsSignIn = false,
                    Username = account.Username,
                    Password = account.Password,
                    Email = account.Email

                };

                await _accountRepo.AddAsync(newAccount);

            }        
                  
            catch (Exception ex)
            {
                Console.WriteLine($"Cart eklenirken hata: {ex.Message}");
                throw;
            }
            return true;
        }
    }
}
