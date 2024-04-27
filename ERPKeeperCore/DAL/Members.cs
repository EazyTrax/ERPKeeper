
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Security;
using ERPKeeperCore.Enterprise.Models.Security.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Members : ERPNodeDalRepository
    {
        public Members(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Member> GetAll()
        {
            return erpNodeDBContext.Members.ToList();
        }



        public Member? Find(Guid Id) => erpNodeDBContext.Members.Find(Id);

        public Member Authen(LogInModel logInModel)
        {
            var member = erpNodeDBContext.Members
                .FirstOrDefault(x => x.Email == logInModel.Email && x.Password == logInModel.Password);
        
            return member;

        }
    }
}