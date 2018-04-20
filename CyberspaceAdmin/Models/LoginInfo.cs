using System.Web.Mvc;

namespace CyberspaceAdmin.Controllers
{
    public class LoginInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}