using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using eTickets.Data.Base;
namespace eTickets.Data.Services
{
    public class ProducersService :EntityBaseRepository<Producer>,IProducersService
    {
       

        public ProducersService(AppDbContext context):base(context)
        
        {

        }
    }

        
}
