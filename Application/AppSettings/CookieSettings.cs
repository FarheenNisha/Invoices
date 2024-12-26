using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppSettings
{
    public class CookieSettings
    {
        public string AccessDeniedPath { get; set; }
        public string LoginPath { get; set; }
        public string LogoutPath { get; set; }
        public string DomainName { get; set; }
        public string CookiePath { get; set; }
        public string AntiforgeryCookieName { get; set; }
        public string TokenCookieName { get; set; }
        public int TokenCookieExpiryMinutes { get; set; }
        public string RefreshTokenCookieName { get; set; }
        public int RefreshTokenCookieExpiryMinutes { get; set; }
        public string AuthCookieName { get; set; }
        public int AuthCookieExpiryMinutes { get; set; }
        public string SessionCookieName { get; set; }
        public int SessionCookieExpiryMinutes { get; set; }
        public string DefaultProfileImage { get; set; }
    }

}
