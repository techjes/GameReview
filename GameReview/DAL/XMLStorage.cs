using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReview.Models;
using System.Xml.Serialization;
using System.IO;

namespace GameReview.DAL
{
    public static class XMLStorage
    {
        public static void Write(List<Game> games)
        {
            XmlSerializer xS = new XmlSerializer(typeof(List<Game>));
            TextWriter tW = new StreamWriter("C:\\Users\\techj\\Desktop\\store.xml");

            using (tW)
            {
                xS.Serialize(tW, games);
            }
        }
        public static List<Game> Read()
        {
            List<Game> games = null;
            XmlSerializer xS = new XmlSerializer(typeof(List<Game>));
            
            try
            {
                TextReader tR = new StreamReader("C:\\Users\\techj\\Desktop\\store.xml");
                using (tR)
                {
                    games = xS.Deserialize(tR) as List<Game>;
                }
            }
            catch (FileNotFoundException)
            {
            }

            return games;
        }
    }
}
