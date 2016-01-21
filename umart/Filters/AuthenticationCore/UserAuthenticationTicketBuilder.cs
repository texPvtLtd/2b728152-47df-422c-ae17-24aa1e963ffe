﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Filters.AuthenticationModel;
using Entities.Models;

namespace Filters.AuthenticationCore
{
    public class UserAuthenticationTicketBuilder
    {
        public static UserInfo CreateUserContextFromUser(User user)
        {
            var userContext = new UserInfo
            {
                Id = user.UserId,
                UserName = user.UserName,
                //FirstName = user.FirstName,
                //LastName = user.LastName,
                //RegionName = user.Region.Name,
                //UserIdentifier = user.Email,
                //UserType = user.UserType,
                //RegionID = user.RegionID,
           //  RegionName = region.Name,
               // RegionName = region.Name.Where(region.RegionID == user.RegionID).ToString().FirstOrDefault(),

            };
            return userContext;
        }

        public static FormsAuthenticationTicket CreateAuthenticationTicket(User user)
        {
            UserInfo userInfo = CreateUserContextFromUser(user);
            var ticket = new FormsAuthenticationTicket(
                1, 
                user.UserName, 
                DateTime.Now, 
                DateTime.Now.Add(FormsAuthentication.Timeout), 
                false, 
                userInfo.ToString());
            return ticket;
        }     

    }
}
