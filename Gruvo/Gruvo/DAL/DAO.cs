using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Gruvo.Models;

namespace Gruvo.DAL
{
    public abstract class DAO
    {
        protected IUserDAO userDAO;
        protected IPostDAO postDAO;
        protected ISubs subsDAO;

        public IUserDAO UserDAO { get { return userDAO; } }
        public IPostDAO PostDAO { get { return postDAO; } }
       // public ISubs SubstDAO { get { return subsDAO; } }
    }
}
