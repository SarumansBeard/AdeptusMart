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

        public async Task<Guid?> LogIn(string username, string password)
        {
            try 
            {
                var user = await _accountRepository.GetByCredentialsAsync(username, password);

                if (user == null)
                {                    
                    return null;
                }
                else
                {                  
                    return user.Id;                  
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Login işlemi sırasında hata oluştu: {ex.Message}");
            }                       
                       
        }






    }
}
