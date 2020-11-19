using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercoplano.Maisbarato.Server.RESTful.Security
{
    public class RandomProvider
    {
        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

    }
}
