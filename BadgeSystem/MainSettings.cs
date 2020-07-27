using Exiled.API.Features;
using System.IO;
using System.Linq;
using System.Text;

namespace BadgeSystem
{
    public class MainSettings : Plugin<Config>
    {
        public override string Name => nameof(BadgeSystem);
        public SetEvents SetEvents { get; set; }

        public override void OnEnabled()
        {
            SetEvents = new SetEvents();
            try
            {
                Global.color = File.ReadAllText(Path.Combine(Global.GetDataFolder(), Global.fileNameColor), Encoding.UTF8);
                Log.Info("Successfully download custom action color: " + Global.color);
            }
            catch (System.Exception)
            {
                Global.color = "army_green";
                Log.Info("Failed download custom action color. Set default action color: " + Global.color);
            }
            try
            {
                Global.fixedIdAndName = File.ReadAllLines(Path.Combine(Global.GetDataFolder(), Global.fileNameFixed), Encoding.UTF8).ToList();
                Global.randomName = File.ReadAllLines(Path.Combine(Global.GetDataFolder(), Global.fileNameRandom), Encoding.UTF8).ToList();
                Exiled.Events.Handlers.Player.Joined += SetEvents.OnJoined;
                Exiled.Events.Handlers.Server.WaitingForPlayers += SetEvents.OnWaitingForPlayers;
                Exiled.Events.Handlers.Server.SendingRemoteAdminCommand += SetEvents.OnSendingRemoteAdminCommand;
                Log.Info(Name + " on");
            }
            catch (System.Exception)
            {
                Log.Info("Error loading names. Plugin was disabled");
            }
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Joined -= SetEvents.OnJoined;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= SetEvents.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand -= SetEvents.OnSendingRemoteAdminCommand;
            Log.Info(Name + " off");
        }
    }
}