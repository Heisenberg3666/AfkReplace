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
            if (!ev.FullMessage.ToLower().Contains("afk") && !ev.Target.IsAlive)
                return;


            Log.Debug($"Player {ev.Target.Nickname} is being kicked.", Plugin.Instance.Config.DebugMode);
            Log.Debug($"Their position is {ev.Target.Position}.", Plugin.Instance.Config.DebugMode);

            try
            {
                Player player = AfkReplaceAPI.RandomSpectator();
                Log.Debug("Random spectator selected.", Plugin.Instance.Config.DebugMode);
                Timing.CallDelayed(5f, () =>
                {
                    player.SetRole(ev.Target.Role.Type);
                    Log.Debug($"Spectator ({player.Nickname}) has been set to {player.Role}.", Plugin.Instance.Config.DebugMode);
                    player.Teleport(ev.Target.Position);
                    Log.Debug($"Spectator ({player.Nickname}) has been teleported to {player.Position}.", Plugin.Instance.Config.DebugMode);
                    player.ClearInventory();
                    player.AddItem(ev.Target.Items);
                    Log.Debug($"Spectator ({player.Nickname}) has been given {ev.Target.Nickname}'s items.", Plugin.Instance.Config.DebugMode);
                    player.Health = ev.Target.Health;
                    Log.Debug($"Spectator ({player.Nickname}) has been given {ev.Target.Nickname}'s health.", Plugin.Instance.Config.DebugMode);
                });
            }
            catch (NotEnoughSpectatorsException)
            {
                Log.Debug($"There are not enough spectators to replace {ev.Target.Nickname}.", Plugin.Instance.Config.DebugMode);
                return;
            }
        }
    }
}
