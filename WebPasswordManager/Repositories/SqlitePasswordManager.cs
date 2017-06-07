using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;
using WebPasswordManager.Models;
using Dapper;

namespace WebPasswordManager.Repositories
{
    public class SqlitePasswordManager : IPManger
    {
        public void DeleteAccount(int id)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute("DELETE FROM SecurityQuestion WHERE AccountId = ?", id);
                    connection.Execute("DELETE FROM PasswordAccount WHERE Id = ?", id);
                    transaction.Commit();
                }
            }
        }

        public PasswordAccount GetAccountByID(int id)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var passwordAccount = connection.QueryFirst<PasswordAccount>("SELECT * FROM PasswordAccount WHERE Id = ?", id);
                if (passwordAccount != null)
                {
                    passwordAccount.SecurityQuestions = connection.Query<SecurityQuestion>("SELECT * FROM SecurityQuestion WHERE AccountId = ?", id).ToList();
                }
                return passwordAccount;
            }
        }

        public List<PasswordAccount> GetAllAccounts()
        {
            var result = new List<PasswordAccount>();
            using (var connection = CreateConnection())
            {
                connection.Open();
                var passwordAccounts = connection.Query<PasswordAccount>("SELECT * FROM PasswordAccount").ToList();
                var securityQuestions = connection.Query<SecurityQuestion>("SELECT * FROM SecurityQuestion").ToList().ToLookup(x => x.AccountId);
                foreach (var account in passwordAccounts)
                {
                    account.SecurityQuestions = securityQuestions[account.Id].ToList();
                    result.Add(account);
                }
            }
            return result;
        }

        public void SaveAccount(PasswordAccount account)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    if (account.Id == 0)
                    {
                        connection.Execute("INSERT INTO PasswordAccount(AccountName, Url, UserName, Password, AccountOwner) VALUES (?,?,?,?,?)",
                            new { account.AccountName, account.Url, account.UserName, account.Password });
                        account.Id = (int)connection.LastInsertRowId;
                    }
                    else
                    {
                        connection.Execute("UPDATE PasswordAccount SET AccountName = ?, Url = ?, UserName = ?, Password = ?, AccountOwner = ? WHERE Id = ?",
                            new { account.AccountName, account.Url, account.UserName, account.Password, account.Id });
                    }

                    foreach (var securityQuestion in account.SecurityQuestions)
                    {
                        securityQuestion.AccountId = account.Id;
                        if (securityQuestion.Id == 0)
                        {
                            connection.Execute("INSERT INTO SecurityQuestion(AccountId, Question, Answer) VALUES (?,?,?)",
                                new { securityQuestion.AccountId, securityQuestion.Question, securityQuestion.Answer });
                            securityQuestion.Id = (int)connection.LastInsertRowId;
                        }
                        else
                        {
                            connection.Execute("UPDATE SecurityQuestion SET AccountId = ?, Question = ?, Answer = ? WHERE Id = ?",
                                new { securityQuestion.AccountId, securityQuestion.Question, securityQuestion.Answer, securityQuestion.Id });
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        public static void Init()
        {
            string path = GetDatabasePath();
            if (!File.Exists(path))
            {
                string initScriptPath = HttpContext.Current.Server.MapPath("~/App_Data/Init.sql");
                string initScript = File.ReadAllText(initScriptPath);
                using (var connection = CreateConnection(includePassword: false))
                {
                    connection.Open();
                    connection.ChangePassword(MvcApplication.Password);
                    connection.Execute(initScript);
                }
            }
        }

        private static SQLiteConnection CreateConnection(bool includePassword = true)
        {
            var builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = GetDatabasePath();
            if (includePassword)
            {
                builder.Password = MvcApplication.Password;
            }
            string connectionString = builder.ToString();
            return new SQLiteConnection(connectionString);
        }

        private static string GetDatabasePath()
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/Database.db3");
        }
    }
}