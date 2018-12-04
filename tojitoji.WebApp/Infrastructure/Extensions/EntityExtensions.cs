using tojitoji.Model.Models;
using tojitoji.WebApp.Models;

namespace tojitoji.WebApp.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateAccount(this Account account, AccountViewModel accountVM)
        {
            account.ID = accountVM.ID;
            account.AccountType = accountVM.AccountType;
            account.Account_Name = accountVM.Account_Name;
            account.Account_1 = accountVM.Account_1;
            account.Account_1_Name = accountVM.Account_1_Name;
            account.Account_2 = accountVM.Account_2;
            account.Account_2_Name = accountVM.Account_2_Name;
            account.Account_3 = accountVM.Account_3;
            account.Account_3_Name = accountVM.Account_3_Name;
            account.Status = accountVM.Status;
            account.TKNhanKC = accountVM.TKNhanKC;
            account.MaKC = accountVM.MaKC;
        }
    }
}