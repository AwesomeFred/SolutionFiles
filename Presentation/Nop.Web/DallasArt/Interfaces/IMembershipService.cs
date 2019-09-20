using System.Collections.Generic;
using Nop.Core.Domain.Customers;
using Nop.Web.DallasArt.Models;

namespace Nop.Web.DallasArt.Interfaces
{
    public interface IMembershipService
    {

      //  IList<Core.Domain.Customers.Customer> Customers(PasswordFormat passwordFormat);

        IList<PaaMember> MembersList(int storeId = 0);

        PaaMember GetMemberById(int memberId, int storeId = 0);

        PaaMember CreateMember(PaaMember member);

        PaaMember UpdateMember(PaaMember member);


        IList<PaaMember> Paa_Current_MemberList(int storeId = 0);

        IList<PaaMember> Paa_ExpiringSoonList(int storeId = 0);

        IList<PaaMember> Paa_Expired_GraceList(int storeId = 0);

        IList<PaaMember> Paa_Expired_RecentlyList(int storeId = 0);

        IList<PaaMember> Paa_ExpiredList(int storeId = 0);
    }
}