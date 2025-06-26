using ePizzaHub.Infra.Models;
using ePizzaHub.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Concrete
{
    public class PaymentRepository : GenericRepository<PaymentDetail>, IPaymentRepository
    {
        public PaymentRepository(ePizzaHubContext ePizzaHubContext) : base(ePizzaHubContext)
        {
        }
    }
}
