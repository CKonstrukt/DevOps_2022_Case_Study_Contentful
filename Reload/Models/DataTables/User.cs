using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reload.Models.DataTables
{
    public class User
    {
        public string Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
