using Gruvo.Models;
using System.Collections.Generic;

namespace Gruvo.BLL
{
    public class TokenUserPairs: ITokenUserPairs
    {
        public Dictionary<string, UserInfo> Pairs { get; } = new Dictionary<string, UserInfo>();
    }
}
