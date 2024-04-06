using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCloakAuthenticate
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Claims { get; set; }
    }
}
