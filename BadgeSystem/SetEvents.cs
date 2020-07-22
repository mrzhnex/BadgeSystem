using EXILED;
using EXILED.Extensions;
using System.IO;
using System.Linq;
using System.Text;

namespace BadgeSystem
{
    public class SetEvents
    {
        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            if (Global.Active)
            {
                foreach (string idAndName in Global.fixedIdAndName)
                {
                    if (idAndName.Contains(ev.Player.GetUserId().Replace("@steam", string.Empty)))
                    {
                        if (idAndName.Split(' ').Length != 2)
                            break;
                        ev.Player.nicknameSync.MyNick = ev.Player.nicknameSync.MyNick + idAndName.Split(' ')[1];
                        ev.Player.gameObject.AddComponent<BadgeSystemComponent>();
                        return;
                    }
                }
                ev.Player.nicknameSync.MyNick = ev.Player.nicknameSync.MyNick + Global.SetSurName();
                ev.Player.gameObject.AddComponent<BadgeSystemComponent>();
            }
            else
            {
                ev.Player.gameObject.AddComponent<BadgeSystemComponent>();
            }
        }

        internal void OnRemoteAdminCommand(ref RACommandEvent ev)
        {
            if (Player.GetPlayer(ev.Sender.Nickname) == null || Player.GetPlayer(ev.Sender.Nickname).gameObject == null || Player.GetPlayer(ev.Sender.Nickname).gameObject.GetComponent<BadgeSystemComponent>() == null)
                return;
            if (ev.Command == "hidetag")
            {
                Player.GetPlayer(ev.Sender.Nickname).gameObject.GetComponent<BadgeSystemComponent>().IsBadgeCover = true;
                Player.GetPlayer(ev.Sender.Nickname).gameObject.GetComponent<BadgeSystemComponent>().IsRefreshBadgeCover = true;
            }
            else if (ev.Command == "showtag")
            {
                Player.GetPlayer(ev.Sender.Nickname).gameObject.GetComponent<BadgeSystemComponent>().IsBadgeCover = false;
                Player.GetPlayer(ev.Sender.Nickname).gameObject.GetComponent<BadgeSystemComponent>().IsRefreshBadgeCover = true;
            }
        }

        public void OnRoundEnd()
        {
            Global.surnameInGame = new System.Collections.Generic.List<string>();
            try
            {
                Global.fixedIdAndName = File.ReadAllLines(Path.Combine(Global.GetDataFolder(), Global.fileNameFixed), Encoding.UTF8).ToList();
                Global.randomName = File.ReadAllLines(Path.Combine(Global.GetDataFolder(), Global.fileNameRandom), Encoding.UTF8).ToList();
                Global.Active = true;
                Log.Info("BadgeSystem's data has been successfully downloaded");
            }
            catch (System.Exception)
            {
                Global.Active = false;
                Log.Info("Error loading names. BadgeSystem was disabled. See you next round, exile...");
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
    }
}