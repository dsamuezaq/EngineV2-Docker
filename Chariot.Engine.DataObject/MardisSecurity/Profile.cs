
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisSecurity
{
    [Table("Profile", Schema = "MardisSecurity")]
    public class Profile
    {

        public Profile()
        {
            Users = new HashSet<User>();
            AuthorizationProfiles = new HashSet<AuthorizationProfile>();
        }

        [Key]
        public System.Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string StatusRegister { get; set; }

        public System.Guid IdTypeUser { get; set; }

        public string Avatar { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<AuthorizationProfile> AuthorizationProfiles { get; set; }

    }
}
