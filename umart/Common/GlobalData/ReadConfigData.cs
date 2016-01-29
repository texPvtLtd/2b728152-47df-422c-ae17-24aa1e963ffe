﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Common.GlobalData
{
    public static class ReadConfigData
    {
        public static int GetMaxRequestLength()
        {
            //check size of image to be uplaoded.
            int maxRequestLength = 0;
            HttpRuntimeSection section = ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
            if (section != null)
                maxRequestLength = section.MaxRequestLength;
            return maxRequestLength;
        }

        public static string GetDBLoginUser()
        {
            string username = "";
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["UmartContext"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conString);
            username = builder.UserID; 
            return username;      
        }

        public static string GetConnectionString()
        {           
            return System.Configuration.ConfigurationManager.ConnectionStrings["UmartContext"].ConnectionString;           
        }
    }
}
