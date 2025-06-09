using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Interface
{
    public interface ICartService
    {
         Task<int> GetCartItemCountAsync(Guid cartid);
    }
}
