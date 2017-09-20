using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models.Interfaces
{
    public interface IRepositoryHoliday
    {
        IEnumerable<Holiday> Holidays { get; }
        void AddHoliday();
    }
}
