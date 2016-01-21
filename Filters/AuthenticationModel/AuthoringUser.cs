﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Filters.AuthenticationModel
{
    [Serializable]
    public class AuthoringUser:IIdentity
    {
        public Int64 UserId { get; set; }
        public string UserName { get; set; }
       // public string UserType { get; set; }       
       // public string Name { get; private set; }

       // public string RegionName { get; set; }
       // public Int64 RegionID { get; set; }
       //public string FirstName { get; set; }
       //public string LastName { get; set; }
        public string AuthenticationType { get { return "JTIGlobalForms"; } }
        public bool IsAuthenticated { get { return true; } }
        public AuthoringUser() { }
        public AuthoringUser(string name, string userName)
        {
           // this.Name = name;
            this.UserName = userName;
            this.UserId = UserId;
            //this.UserType = userType;
            //this.RegionName = regionName;
            //this.RegionID = regionID;
            //this.FirstName = FirstName;
            //this.LastName = LastName;
          //  this.RegionName = regionName;
        }

        public AuthoringUser(string name, UserInfo userInfo)
            : this(name, userInfo.UserName)
        {
            this.UserId = userInfo.Id;
        }

        public AuthoringUser(FormsAuthenticationTicket ticket)
            :this(ticket.Name,UserInfo.FromString(ticket.UserData))
        {
            if (ticket == null) throw new ArgumentNullException("ticket");
        }


        public string Name
        {
            get { throw new NotImplementedException(); }
        }
    }
}
