using Gruvo.Models;
using System.Collections.Generic;

namespace Gruvo.Data
{
    public class TokenUserPairs
    {
        private static TokenUserPairs instance;

        private static Dictionary<string, UserInfo> pairs;

        private TokenUserPairs()
        {
            pairs = new Dictionary<string, UserInfo>();
        }

        public static TokenUserPairs GetInstance()
        {
            if (instance == null)
                instance = new TokenUserPairs();
            return instance;
        }

        public Dictionary<string, UserInfo> GetPairs()
        {
            return pairs;
        }
    }
}
