﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Gruvo.Models;

namespace Gruvo.DAL
{
    public class MSSQL : DAO
    {
        private readonly SqlConnection connection;
        public MSSQL(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            userDAO = new MSSQLUserDAO(connection);
            postDAO = new MSSQLPostDAO(connection);
           // subsDAO = new MSSQLSubsDAO(connection);
        }
    }
}