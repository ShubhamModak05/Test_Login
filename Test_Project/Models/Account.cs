using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Test_deqode.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Test_Project.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Pass { get; set; }
        public string ConfirmePass { get; set; }
    }

    public class AccountOperation
    {
        public List<Account> GetUserDetail()
        {
            List<Account> UserDetail = new List<Account>();
            DataTable dt = SqlFunction.GetDataTable("Select * from User_Details");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    UserDetail.Add(
                        new Account
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            UserName = row["UserName"].ToString(),
                            Email = row["Email"].ToString(),
                            Pass = row["PassWord"].ToString()
                        });
                }
            }
            return UserDetail;
        }

        public bool InsertUser(Account model)
        {
            StringBuilder qury = new StringBuilder();
            qury.Append("Insert into User_Details values('");
            qury.Append(model.UserName + "','");
            qury.Append(model.Pass + "','");
            qury.Append(model.Email + "')");

            return Convert.ToBoolean(SqlFunction.ExecuteNonQuery(qury.ToString()));
        }

        public Account GetDetails(int Id)
        {
            Account UserDetail = new Account();
            DataTable dt = SqlFunction.GetDataTable("Select * from User_Details where Id =" + Id);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    UserDetail.Id = Convert.ToInt32(row["Id"]);
                    UserDetail.UserName = row["UserName"].ToString();
                    UserDetail.Pass = row["PassWord"].ToString();
                    UserDetail.Email = row["Email"].ToString();
                }
            }
            return UserDetail;
        }
    }
}