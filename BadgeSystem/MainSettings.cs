using EXILED;
using System.IO;
using System.Linq;
using System.Text;

namespace BadgeSystem
{
    public class MainSettings : Plugin
    {
        public override string getName => nameof(BadgeSystem);
        public SetEvents SetEvents { get; set; }

        public override void OnEnable()
        {
            SetEvents = new SetEvents();
            try
            {
                Global.fixedIdAndName = File.ReadAllLines(Path.Combine(Global.GetDataFolder(), Global.fileNameFixed), Encoding.UTF8).ToList();
                Global.randomName = File.ReadAllLines(Path.Combine(Global.GetDataFolder(), Global.fileNameRandom), Encoding.UTF8).ToList();
                Global.Active = true;
                Events.PlayerJoinEvent += SetEvents.OnPlayerJoin;
                Events.RoundEndEvent += SetEvents.OnRoundEnd;
                Events.RemoteAdminCommandEvent += SetEvents.OnRemoteAdminCommand;
                Log.Info(getName + " on");
            }
            catch (System.Exception)
            {
                Global.Active = false;
                Log.Info(getName + " error loading names. Plugin was disabled");
            }
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
        }

        public override void OnDisable()
        {
            Events.PlayerJoinEvent -= SetEvents.OnPlayerJoin;
            Events.RoundEndEvent -= SetEvents.OnRoundEnd;
            Events.RemoteAdminCommandEvent -= SetEvents.OnRemoteAdminCommand;
            Global.Active = false;
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}