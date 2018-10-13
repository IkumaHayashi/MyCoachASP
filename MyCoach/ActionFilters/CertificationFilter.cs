﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace MyCoach.ActionFilters
{
    public class LoginAuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return;
            }
            //TODO:登録していないとできないようにする
            return;
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            return;
        }
    }
}