using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Http;

namespace AdeptusMart03.BusinessAccessLayer.Services
{
    public class LoginService
    {
        private readonly IRepository<Account> _accountRepo;
        private readonly IAccountRepository _accountRepository;

        public LoginService(IRepository<Account> accountRepo, IAccountRepository accountRepository)
        {
            _accountRepo = accountRepo;
            _accountRepository = accountRepository;
        }

        public async Task<bool> LogIn(string username, string password,string sessionId)
        {
            bool success = false;

            try 
            {
                var user = await _accountRepository.GetByCredentialsAsync(username, password);

                if (user == null)
                {
                    success = false;
                    return success;
                }
                else
                {                    
                        user.IsSignIn = true;
                        user.SessionId = sessionId; 
                        await _accountRepo.UpdateAsync(user);
                        success = true;                        
                        return success;                   
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Login işlemi sırasında hata oluştu: {ex.Message}");
            }

            
                       
        }






    }
}
