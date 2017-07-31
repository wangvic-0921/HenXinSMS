using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace HengXinSMS.Models
{
    public class CheckRepeat
    {
        public int  search( string check)
        {
              using (var context = new HenXinSMSContext())
              {
                  var user_check = from w in context.Users
                                     where w.UserName ==check
                                     select w;
                  if (user_check.FirstOrDefault() == null) 
                  {
                      return 0;
                  }
                  else 
                  {

                      return 1;
                  }
              }
        }
        public int  verify(string username,string password)
        {
            using (var context = new HenXinSMSContext())
            {
                var user_verify = from w in context.Users
                                  where w.UserName == username && w.PassWord == password
                                  select w;
                if (user_verify.FirstOrDefault() == null)
                {
                    return 0;
                }
                else
                {
                  
                    return 1;

                }

            }
        }
        public  int update_pwd(string username,string password,string newpassword)
        {
             using (var context = new HenXinSMSContext())
             {
                 var user_update = from w in context.Users
                                  where w.UserName == username&&w.PassWord==password
                                  select w;
                 if (user_update.FirstOrDefault() == null)
                 {
                     return 0;
                 }
                 else 
                 {
                      var users = context.Users;
                     
                          var userToUpdate = users.First(d=>d.UserName==username);
                          userToUpdate.PassWord= newpassword;
                       
                          context.SaveChanges();

                          return 1;
                      
                 }
              
             }
        }
    }
}