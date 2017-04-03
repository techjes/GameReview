using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReview.Models;

namespace GameReview.DAL
{
    public class GRepo : IGRepo, IDisposable
    {
        private List<Game> _games;

        public List<Game> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Game Select(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Game game)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
