using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Gruvo.Models;

namespace Gruvo.DAL
{
    public abstract class BaseDAO
    {
        protected IUserDAO userDAO;
        protected IPostDAO postDAO;

        public IUserDAO UserDAO { get { return userDAO; } }
        public IPostDAO PostDAO { get { return postDAO; } }
    }
}
