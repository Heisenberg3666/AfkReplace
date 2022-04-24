using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using System;


namespace AfkReplace
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        private EventHandler events;

        public override string Name => "AfkReplace";
        public override string Author => "Heisenberg3666";
        public override Version Version => new Version(1, 0, 0, 0);
        public override Version RequiredExiledVersion => new Version(5, 1, 3);

        public override void OnEnabled()
        {
            base.OnEnabled();
            Instance = this;
            events = new EventHandler();
            RegisterEvents();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
            events = null;
            Instance = null;
            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            Player.Kicking += events.OnKicking;
        }

        public void UnregisterEvents()
        {
            Player.Kicking -= events.OnKicking;
        }
    }
}
