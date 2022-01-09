using EduardoPrimavera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EduardoPrimavera.Controllers
{
    public class SessionController
    {
        private EduardoPrimaveraContext db = new EduardoPrimaveraContext();

        public bool SessionAuthentication(HttpRequestMessage Request)
        {
            string cookieName = "session";
            var vCookies = Request.Headers.Where(H => H.Key.ToLower() == "cookie");
            if (vCookies != null)
            {
                var vArrCookie = vCookies.FirstOrDefault().Value;
                if (vArrCookie != null)
                {
                    var vCookiePair =
                        vArrCookie.FirstOrDefault().Split(new char[] { ';' }).Where(C => C.Trim().StartsWith(cookieName)).FirstOrDefault();
                    if (vCookiePair != null)
                    {
                        var vCookie = vCookiePair.Trim().Substring(cookieName.Length + 1);
                        if (vCookie != null)
                        {
                            var teste1 = db.Users.Where(b => b.Session == vCookie).SingleOrDefault();
                            if (teste1 != null)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
