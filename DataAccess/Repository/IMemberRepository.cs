using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        bool AuthenticateUser(string email, string password);
        void addMember(Member member);
        void editMember(Member member);
        void deleteMember(Member member);
    }
}
