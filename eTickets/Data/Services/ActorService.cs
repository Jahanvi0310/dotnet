using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using eTickets.Data.Base;
namespace eTickets.Data.Services
{
    public class ActorsService :EntityBaseRepository<Actor>,IActorsService
    {
       

        public ActorsService(AppDbContext context):base(context)
        
        {

        }
    }

        
}
