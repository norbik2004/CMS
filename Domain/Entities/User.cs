using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public string Id { get; private set; }
        public string Email { get; private set; }
        public string DisplayName { get; private set; }
        public string UserName { get; private set; }
        public string PhoneNumber { get; private set; }

        public User(string id, string email, string displayName, string userName, string phoneNumber)
        {
            Id = id;
            Email = email;
            DisplayName = displayName;
            UserName = userName;
            PhoneNumber = phoneNumber;
        }
    }
}
