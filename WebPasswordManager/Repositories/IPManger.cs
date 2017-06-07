using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPasswordManager.Models;
namespace WebPasswordManager.Repositories
{
    public interface IPManger
    {
        List<PasswordAccount> GetAllAccounts();
        PasswordAccount GetAccountByID(int id);
        void SaveAccount(PasswordAccount account);
        void DeleteAccount(int id);
    }
}
