using AfkReplace.API;
using AfkReplace.Exceptions;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System.Linq;

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
                    // Setting player role, location and health.
                    player.SetRole(ev.Target.Role.Type);
                    Log.Debug($"Spectator ({player.Nickname}) has been set to {player.Role}.", Plugin.Instance.Config.DebugMode);
                    player.Teleport(ev.Target.Position);
                    Log.Debug($"Spectator ({player.Nickname}) has been teleported to {player.Position}.", Plugin.Instance.Config.DebugMode);
                    player.Health = ev.Target.Health;
                    Log.Debug($"Spectator ({player.Nickname}) has been given {ev.Target.Nickname}'s health.", Plugin.Instance.Config.DebugMode);

                    // Giving player Items and Ammo.
                    player.ClearInventory();
                    player.AddItem(ev.Target.Items);
                    player.AddAmmo(AmmoType.Ammo44Cal, ev.Target.Ammo[ItemType.Ammo44cal]);
                    player.AddAmmo(AmmoType.Ammo12Gauge, ev.Target.Ammo[ItemType.Ammo12gauge]);
                    player.AddAmmo(AmmoType.Nato556, ev.Target.Ammo[ItemType.Ammo556x45]);
                    player.AddAmmo(AmmoType.Nato762, ev.Target.Ammo[ItemType.Ammo762x39]);
                    player.AddAmmo(AmmoType.Nato9, ev.Target.Ammo[ItemType.Ammo9x19]);
                    Log.Debug($"Spectator ({player.Nickname}) has been given {ev.Target.Nickname}'s items.", Plugin.Instance.Config.DebugMode);
                    
                    // Giving player effects
                    foreach (PlayerEffect effect  in ev.Target.ActiveEffects)
                        player.EnableEffect(effect, effect.Duration, true);
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
