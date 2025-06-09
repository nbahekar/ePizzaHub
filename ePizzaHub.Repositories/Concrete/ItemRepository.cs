using ePizzaHub.Infra.Models;
using ePizzaHub.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Concrete
{
    public class ItemRepository:GenericRepository<Item>,IItemRepository
    {
        public ItemRepository(ePizzaHubContext ePizzaHubContext):base(ePizzaHubContext) { }
    }
}
