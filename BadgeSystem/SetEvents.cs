using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System.IO;
using System.Linq;
using System.Text;

namespace BadgeSystem
{
    public class SetEvents
    {
        internal void OnWaitingForPlayers()
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

        internal void OnJoined(JoinedEventArgs ev)
        {
            if (Global.Active)
            {
                foreach (string idAndName in Global.fixedIdAndName)
                {
                    if (idAndName.Contains(ev.Player.UserId.Replace("@steam", string.Empty)))
                    {
                        if (idAndName.Split(' ').Length != 2)
                            break;
                        ev.Player.ReferenceHub.nicknameSync.MyNick = ev.Player.ReferenceHub.nicknameSync.MyNick + idAndName.Split(' ')[1];
                        ev.Player.GameObject.AddComponent<BadgeSystemComponent>();
                        return;
                    }
                }
                ev.Player.ReferenceHub.nicknameSync.MyNick = ev.Player.ReferenceHub.nicknameSync.MyNick + Global.SetSurName();
            }
            ev.Player.GameObject.AddComponent<BadgeSystemComponent>();
        }

        internal void OnSendingRemoteAdminCommand(SendingRemoteAdminCommandEventArgs ev)
        {
            if (ev.Sender.GameObject.GetComponent<BadgeSystemComponent>() == null)
                return;
            if (ev.Name == "hidetag")
            {
                ev.Sender.GameObject.GetComponent<BadgeSystemComponent>().IsBadgeCover = true;
                ev.Sender.GameObject.GetComponent<BadgeSystemComponent>().IsRefreshBadgeCover = true;
            }
            else if (ev.Name == "showtag")
            {
                ev.Sender.GameObject.GetComponent<BadgeSystemComponent>().IsBadgeCover = false;
                ev.Sender.GameObject.GetComponent<BadgeSystemComponent>().IsRefreshBadgeCover = true;
            }
        }
    }
}