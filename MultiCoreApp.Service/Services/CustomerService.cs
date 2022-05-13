using MultiCoreApp.Core.IntRepository;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.Core.IntUnitOfWork;
using MultiCoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCoreApp.Service.Services
{
    public class CustomerService : Service<Customer>, ICustomerService
    {

        public CustomerService(IUnitOfWork unit, IRepository<Customer> repo) : base(unit, repo)
        {
        }
    }
}
