using EXILED.Extensions;
using UnityEngine;

namespace BadgeSystem
{
    public class BadgeSystemComponent : MonoBehaviour
    {
        private float Timer = 0.0f;
        private readonly float TimeIsUp = 0.5f;
        private int Stage = 0;
        
        private string Badge = null;
        private string Color = null;
        public bool IsBadgeCover = false;
        public bool IsRefreshBadgeCover = false;

        public void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > TimeIsUp)
            {
                Timer = 0.0f;
                if (Badge == null && Color == null)
                {
                    Badge = gameObject.GetComponent<ServerRoles>().NetworkMyText;
                    Color = gameObject.GetComponent<ServerRoles>().NetworkMyColor;
                }
                if (Player.GetPlayer(gameObject) != null && Player.GetPlayer(gameObject).GetRole() == RoleType.Spectator)
                {
                    if (IsBadgeCover)
                    {
                        if (Stage != 0 || IsRefreshBadgeCover)
                        {
                            SetRank("white", string.Empty);
                            Stage = 0;
                        }
                    }
                    else
                    {
                        if (Stage != 3 || IsRefreshBadgeCover)
                        {
                            SetRank(Color, Badge);
                            Stage = 3;
                        }
                    }
                }
                else if (gameObject.GetComponent<BleedOutPlugin.BleedOutComponent>() != null)
                {
                    switch (gameObject.GetComponent<BleedOutPlugin.BleedOutComponent>().BleedOutType)
                    {
                        case BleedOutPlugin.BleedOutType.Arterial:
                            if (Stage != 4 || IsRefreshBadgeCover)
                            {
                                if (IsBadgeCover)
                                    SetRank(Global.color, Global.bleedout4);
                                else
                                    SetRank(Global.color, Badge + Global.voidSymbol + Global.bleedout4);
                                Stage = 4;
                            }
                            break;
                        case BleedOutPlugin.BleedOutType.Major:
                            if (Stage != 5 || IsRefreshBadgeCover)
                            {
                                if (IsBadgeCover)
                                    SetRank(Global.color, Global.bleedout3);
                                else
                                    SetRank(Global.color, Badge + Global.voidSymbol + Global.bleedout3);
                                Stage = 5;
                            }
                            break;
                        case BleedOutPlugin.BleedOutType.Normal:
                            if (Stage != 6 || IsRefreshBadgeCover)
                            {
                                if (IsBadgeCover)
                                    SetRank(Global.color, Global.bleedout2);
                                else
                                    SetRank(Global.color, Badge + Global.voidSymbol + Global.bleedout2);
                                Stage = 6;
                            }
                            break;
                        case BleedOutPlugin.BleedOutType.Miner:
                            if (Stage != 7 || IsRefreshBadgeCover)
                            {
                                if (IsBadgeCover)
                                    SetRank(Global.color, Global.bleedout1);
                                else
                                    SetRank(Global.color, Badge + Global.voidSymbol + Global.bleedout1);
                                Stage = 7;
                            }
                            break;
                    }
                }
                else if (gameObject.GetComponent<PocketKillsPlugin.PocketKillsComponent>() != null)
                {
                    if (Stage != 1 || IsRefreshBadgeCover)
                    {
                        if (IsBadgeCover)
                            SetRank(Global.color, Global.pocketkills);
                        else
                            SetRank(Global.color, Badge + Global.voidSymbol + Global.pocketkills);
                        Stage = 1;
                    }
                }
                else if (gameObject.GetComponent<BetterTranquilizer.BadgeComponent>() != null)
                {
                    if (Stage != 2 || IsRefreshBadgeCover)
                    {
                        if (IsBadgeCover)
                            SetRank(Global.color, Global.bodyholder);
                        else
                            SetRank(Global.color, Badge + Global.voidSymbol + Global.bodyholder);
                        Stage = 2;
                    }
                }
                else
                {
                    if (IsBadgeCover)
                    {
                        if (Stage != 0 || IsRefreshBadgeCover)
                        {
                            SetRank("white", string.Empty);
                            Stage = 0;
                        }
                    }
                    else
                    {
                        if (Stage != 3 || IsRefreshBadgeCover)
                        {
                            SetRank(Color, Badge);
                            Stage = 3;
                        }
                    }
                }
            }
        }

        private void SetRank(string color, string badge)
        {
            gameObject.GetComponent<ServerRoles>().NetworkMyColor = color;
            gameObject.GetComponent<ServerRoles>().NetworkMyText = badge;
        }
    }
}