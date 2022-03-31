using System;
using System.Linq;

namespace Mission13.Models
{
    public interface IBowlersRepository
    {
        IQueryable<Bowler> Bowlers { get; }
        IQueryable<Team> Teams { get; }

        public void AddBowler(Bowler b);
        public void EditBowler(Bowler b);
        public void DeleteBowler(Bowler b);
    }
}
