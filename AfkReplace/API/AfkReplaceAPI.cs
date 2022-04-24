using AfkReplace.Exceptions;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfkReplace.API
{
    public static class AfkReplaceAPI
    {
        public static IEnumerable<Player> Spectators = new List<Player>();

        public static Player RandomSpectator()
        {
            foreach (Player pl in Player.List)
            {
                if (!pl.IsAlive)
                    Spectators.Append(pl);
            }

            if (Spectators.Count() == 0)
                throw new NotEnoughSpectatorsException("There are not enough spectators to replace the AFK player.");

            return Spectators.ToArray().RandomItem();
        }
    }
}
