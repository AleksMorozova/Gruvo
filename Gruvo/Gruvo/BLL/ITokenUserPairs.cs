using System.Collections.Generic;
using Gruvo.Models;

namespace Gruvo.BLL
{
    public interface ITokenUserPairs
    {
        Dictionary<string, UserInfo> Pairs { get; }
    }
}
