using HomeMyDay.Database;
using HomeMyDay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository.Implementation
{
    public class EFFaqRepository : IFaqRepository
    {
        private readonly HomeMyDayDbContext _context;

        public EFFaqRepository(HomeMyDayDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FaqCategory> GetCategoriesAndQuestions()
        {
            return _context.FaqCategory.Include(nameof(FaqCategory.FaqQuestions));
        }
    }
}