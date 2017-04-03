using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReview.Models;

namespace GameReview.DAL
{
    public interface IGRepo
    {
        List<Game> SelectAll();
        Game Select(int id);
        void Edit(Game game);
        void Remove(int id);
    }
}
