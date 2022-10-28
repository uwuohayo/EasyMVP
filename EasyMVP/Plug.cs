using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using MEC;
using Qurre;
using Qurre.API;
using Qurre.API.Events;
using Qurre.Events.Modules;
using Player = Qurre.API.Player;
using Round = Qurre.Events.Round;
using Server = Qurre.Events.Server;

namespace EasyMVP
{
    public class Plug : Plugin
    {
        public override string Developer => "ohayo!#5601";
        public override string Name => "EasyMVP";
        public override Version Version => new Version(1, 0, 0);
        public override int Priority => int.MinValue;

        public Player mvpPlayer;
        public int mvpKills;

        private Plug instance;
        public Plug getInstanse()
        {
            return instance;
        }

        public override void Enable()
        {
            instance = this;

            ConfigManager.registerCfg();

            if (ConfigManager.EasyMVP_enable == false)
            {
                Log.Error(" > the " + Name + " is disabled because you disabled it in the config");
                return;
            }

            Round.Waiting += onWaiting;
            Round.End += onRoundEnd;

            Log.Info(" " + Name + " enabled :)");
            Log.Info(" version: " + Version);
            Log.Info(" dev: " + Developer);
            Log.Info(" site: www.rootkovskiy.ovh");
        }

        public override void Disable()
        {
            instance = null;

            Round.Waiting -= onWaiting;
            Round.End -= onRoundEnd;

            Log.Info(" " + Name + " disabled :(");
            Log.Info(" version: " + Version);
            Log.Info(" dev: " + Developer);
            Log.Info(" site: www.rootkovskiy.ovh");
        }

        public void onWaiting()
        {
            mvpPlayer = null;
            mvpKills = 0;
        }

        public void onRoundEnd(RoundEndEvent ev)
        {
            List<int> playersKillCounts = (from value in Player.List select value.KillsCount).ToList<int>();
            mvpKills = playersKillCounts.Max();
            foreach (Player p in Player.List)
            {
                if (p.KillsCount == mvpKills)
                {
                    mvpPlayer = p;
                }
            }
            foreach (Player p in Player.List)
            {
                p.Broadcast(ConfigManager.EasyMVP_messageUp.Replace("%mvpNick%", mvpPlayer.Nickname) + "\n" + ConfigManager.EasyMVP_messageDown.Replace("%mvpKills%", mvpKills.ToString()), ConfigManager.EasyMVP_messageTime);
            }
        }
    }
}

