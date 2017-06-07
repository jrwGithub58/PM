using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPasswordManager.Models;
using Newtonsoft.Json;

namespace WebPasswordManager.Repositories
{
    public class PManager : IPManger
    {
        public void DeleteAccount(int id)
        {
            GetAccountsData().RemoveAll(x => x.Id == id);
        }

        public PasswordAccount GetAccountByID(int id)
        {            
            return GetAccountsData().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<PasswordAccount> GetAllAccounts()
        {
            List<PasswordAccount> accounts = GetAccountsData();

            return accounts;
        }

        private List<PasswordAccount> GetAccountsData()
        {
            List<PasswordAccount> accounts = new List<PasswordAccount>();

            accounts.Add(new PasswordAccount { Id = 1, AccountName = "Turbo Tax", Url = "", UserName = "jrwathen@cox.net", Password = "bobo36cs", AccountOwner = "Jim" });
            accounts.Add(new PasswordAccount { Id = 2, AccountName = "Verizon Wireless" , Url = "", UserName = "jrw85021", Password = "castle19", AccountOwner = "Jim & Carolyn"});
            accounts.Add(new PasswordAccount { Id = 3, AccountName = "Health Equity", Url = "", UserName = "jrwathen", Password = "Jcjrej58!", AccountOwner = "Jim" });
            return accounts;
        }

        public void SaveAccount(PasswordAccount account)
        {
            if(account.Id == 0)
            {
                account.Id = GetNewAccountId();
            }
        }

        private int GetNewAccountId()
        {
            return GetAccountsData().Select(x => x.Id).Max() + 1;
        }
    }
}