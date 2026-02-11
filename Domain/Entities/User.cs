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

        public User(string id, string email, string displayName)
        {
            Id = id;
            Email = email;
            DisplayName = displayName;
        }
    }
}
