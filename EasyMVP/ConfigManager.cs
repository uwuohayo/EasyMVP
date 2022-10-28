using System;
namespace EasyMVP
{
    public class ConfigManager
    {
        public static bool EasyMVP_enable;
        public static String EasyMVP_messageUp;
        public static String EasyMVP_messageDown;
        public static ushort EasyMVP_messageTime;

        public static void registerCfg()
        {
            EasyMVP_enable = Plug.Config.GetBool("EasyMVP_enable", true, "on or off EasyMVP?");
            EasyMVP_messageUp = Plug.Config.GetString("EasyMVP_messageUp", "<color=#FF60A9>MVP</color> on this round - <color=#FF60A9>%mvpNick%</color>", "first bc line");
            EasyMVP_messageDown = Plug.Config.GetString("EasyMVP_messageDown", "This player has <color=#FF60A9>%mvpKills%</color> kills", "second bc line");
            EasyMVP_messageTime = Plug.Config.GetUShort("EasyMVP_messageTime", 15, "time to display message");
        }
    }
}

