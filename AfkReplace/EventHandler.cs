using AfkReplace.API;
using AfkReplace.Exceptions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace AfkReplace
{
    public class EventHandler
    {
        public void OnKicking(KickingEventArgs ev)
        {
            if (!ev.Target.IsAlive)
                return;

            if (!ev.FullMessage.ToLower().Contains("afk"))
                return;

            try
            {
                Player player = AfkReplaceAPI.RandomSpectator();
                Timing.CallDelayed(5f, () =>
                {
                    player.SetRole(ev.Target.Role.Type);
                    player.Teleport(ev.Target.Position);
                });
            }
            catch (NotEnoughSpectatorsException)
            {
                return;
            }
        }
    }
}
