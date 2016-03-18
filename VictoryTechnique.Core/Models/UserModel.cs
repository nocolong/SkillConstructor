using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VictoryTechnique.Core.Domain;

namespace VictoryTechnique.Core.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public Belts Belt { get; set; }
    }
}
