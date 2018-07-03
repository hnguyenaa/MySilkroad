using SroBasic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Metadata
{
    public static class MediaData
    {

        public static Dictionary<uint, Skill> Skills = new Dictionary<uint, Skill>();
        public static Dictionary<uint, Item> Items = new Dictionary<uint, Item>();
        public static Dictionary<uint, Mob> Mobs = new Dictionary<uint, Mob>();
        public static ClientInfo ClientInfo = new ClientInfo();

        static MediaData()
        {

            Skills = new Dictionary<uint, Skill>(LoadMediaSkills());
            Items = new Dictionary<uint, Item>(LoadMediaItems());
            Mobs = new Dictionary<uint, Mob>(LoadMediaMobs());
            ClientInfo = LoadClientInfo();
            

        //    LocalGateway = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
        //    LocalAgent = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20001);

            #region Pet
            grabPets = new ReadOnlyCollection<string>(new string[] { 
                "COS_P_SPOT_RABBIT",
                "COS_P_RABBIT",
                "COS_P_GGLIDER",
                "COS_P_MYOWON",
                "COS_P_SEOWON",
                "COS_P_RACCOONDOG",
                "COS_P_CAT",
                "COS_P_BROWNIE",
                "COS_P_PINKPIG",
                "COS_P_GOLDPIG",
                "COS_P_FOX"
            });

            attackPets = new ReadOnlyCollection<string>(new string[] { 
                "COS_P_BEAR",
                "COS_P_FOX",
                "COS_P_PENGUIN",
                "COS_P_WOLF_WHITE_SMALL",
                "COS_P_WOLF_WHITE",
                "COS_P_WOLF"
            });

            #endregion
        }

        #region Pet
        private static ReadOnlyCollection<string> grabPets;
        public static string[] GrabPets { get { return grabPets.ToArray(); } }


        private static ReadOnlyCollection<string> attackPets;
        public static string[] AttackPets { get { return attackPets.ToArray(); } }

        public static ReadOnlyCollection<string> AttackPetItems = new ReadOnlyCollection<string>(new string[] { 
                "ITEM_COS_P_FOX_SCROLL",
                "ITEM_COS_P_BEAR_SCROLL",
                "ITEM_COS_P_FLUTE",
                "ITEM_COS_P_FLUTE_SILK",
                "ITEM_COS_P_FLUTE_WHITE",
                "ITEM_COS_P_FLUTE_WHITE_SMALL",
                "ITEM_COS_P_PENGUIN_SCROLL"
            });

        public static ReadOnlyCollection<string> GrabPetItems = new ReadOnlyCollection<string>(new string[] { 
                "ITEM_COS_P_SPOT_RABBIT_SCROLL",
                "ITEM_COS_P_RABBIT_SCROLL",
                "ITEM_COS_P_RABBIT_SCROLL_SILK",
                "ITEM_COS_P_GGLIDER_SCROLL",
                "ITEM_COS_P_MYOWON_SCROLL",
                "ITEM_COS_P_SEOWON_SCROLL",
                "ITEM_COS_P_RACCOONDOG_SCROLL",
                "ITEM_COS_P_BROWNIE_SCROLL",
                "ITEM_COS_P_CAT_SCROLL",
                "ITEM_COS_P_PINKPIG_SCROLL",
                "ITEM_COS_P_GOLDPIG_SCROLL",
                "ITEM_COS_P_GOLDPIG_SCROLL_SILK"
            });

        #endregion
        #region Helps
        private static ClientInfo LoadClientInfo()
        {
            ClientInfo clientInfo = new ClientInfo();

            string path = Environment.CurrentDirectory + @"\data\config.txt";

            if (!File.Exists(path))
            {
                Views.BindingFrom.WriteLine("File \"config.txt\" not found");
                return clientInfo;
            }

            using (TextReader reader = File.OpenText(path))
            {
                string input = "";
                while ((input = reader.ReadLine()) != null)
                {
                    if (input != "" && !input.StartsWith("//") && input.StartsWith("1"))
                    {
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(input))
                {
                    string[] split = input.Split(',');
                    string type = split[1];
                    string locale = split[2];
                    string vesion = split[3];
                    string ip = split[4];
                    string port = split[5];
                    
                    
                        
                    clientInfo.SroType = type;
                    clientInfo.Locale = Convert.ToByte(locale);
                    clientInfo.Version = Convert.ToUInt32(vesion);
                    clientInfo.IP = System.Net.IPAddress.Parse(ip);
                    clientInfo.Port = Convert.ToInt32(port);
                    clientInfo.RedirectGatewayServer = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 20001);
                    clientInfo.RedirectAgentSetver = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 20002);
                }
            }

            return clientInfo;
        }
        private static Dictionary<uint, Skill> LoadMediaSkills()
        {
            Dictionary<uint, Skill> result = new Dictionary<uint, Skill>();

            string path = Environment.CurrentDirectory + @"\data\skills.txt";

            if (!File.Exists(path))
            {
                //throw new Exception("File skills.txt not found");
                Views.BindingFrom.WriteLine("File skills.txt not found");
                return result;
            }

            using (TextReader reader = File.OpenText(path))
            {
                string input = "";
                string[] split;

                uint id = 0;
                string name = "";
                string type = "";
                string groupType = "";
                byte level = 0;
                ushort castTime = 0;
                uint cooldown = 0;
                byte objReq = 0;
                byte usingType = 0;
                ushort force = 0;
                ushort mp = 0;

                while ((input = reader.ReadLine()) != null)
                {
                    if (input != "" && !input.StartsWith("//"))
                    {
                        split = input.Split(',');

                        id = 0; uint.TryParse(split[0], out id);
                        name = split[1];
                        type = split[2];
                        groupType = split[3];
                        level = 0; byte.TryParse(split[4], out level);
                        castTime = 0; ushort.TryParse(split[5], out castTime);
                        cooldown = 0; uint.TryParse(split[6], out cooldown);
                        objReq = 0; byte.TryParse(split[7], out objReq);
                        usingType = 0; byte.TryParse(split[8], out usingType);
                        force = 0; ushort.TryParse(split[9], out force);
                        mp = 0; ushort.TryParse(split[10], out mp);

                        Skill skill = new Skill
                        (
                            id: id,
                            name: name,
                            type: type,
                            groupType: groupType,
                            level: level,
                            castTime: castTime,
                            cooldown: cooldown,
                            objReq: objReq,
                            usingType: usingType,
                            force: force,
                            mp: mp
                        );

                        if (!result.ContainsKey(skill.ID))
                            result.Add(skill.ID, skill);
                    }
                }
            }

            return result;
        }

        private static Dictionary<uint, Item> LoadMediaItems()
        {
            Dictionary<uint, Item> result = new Dictionary<uint, Item>();
            string path = Environment.CurrentDirectory + @"\data\items.txt";

            if (!File.Exists(path))
            {
                //throw new Exception("File items.txt not found");
                Views.BindingFrom.WriteLine("File items.txt not found");
                return result;
            }

            using (TextReader reader = File.OpenText(path))
            {
                string input = "";
                string[] split;

                uint id = 0;
                string name = "";
                string type = "";
                byte level = 0;
                ushort stack = 0;
                ushort durability = 0;

                while ((input = reader.ReadLine()) != null)
                {
                    if (input != "" && !input.StartsWith("//"))
                    {
                        split = input.Split(',');
                        id = StringToUInt32(split[0]);
                        name = split[2];
                        type = split[1];
                        level = 0; byte.TryParse(split[3], out level);
                        stack = 0; ushort.TryParse(split[4], out stack);
                        durability = 0; ushort.TryParse(split[5], out durability);

                        Item item = new Item(
                            id: id,
                            type: type,
                            name: name,
                            level: level,
                            stack: stack,
                            durability: durability
                            );

                        if (!result.ContainsKey(item.ID))
                            result.Add(item.ID, item);
                    }
                }
            }

            return result;
        }

        private static Dictionary<uint, Mob> LoadMediaMobs()
        {
            Dictionary<uint, Mob> result = new Dictionary<uint, Mob>();
            string path = Environment.CurrentDirectory + @"\data\mobs.txt";

            if (!File.Exists(path))
            {
                //throw new Exception("File mobs.txt not found");
                Views.BindingFrom.WriteLine("File mobs.txt not found");
                return result;
            }

            using (TextReader reader = File.OpenText(path))
            {
                string input = "";
                string[] split;

                uint id = 0;
                string name = "";
                string type = "";
                byte level = 0;
                uint hp = 0;

                while ((input = reader.ReadLine()) != null)
                {
                    if (input != "" && !input.StartsWith("//"))
                    {
                        split = input.Split(',');
                        id = StringToUInt32(split[0]);
                        name = split[1];
                        type = split[2];
                        level = 0; byte.TryParse(split[3], out level);
                        hp = 0; uint.TryParse(split[4], out hp);

                       Mob mob = new Mob(
                            id: id,
                            name: name,
                            type: type,
                            level: level,
                            hp: hp
                        );

                        if (!result.ContainsKey(mob.ID))
                            result.Add(mob.ID, mob);
                    }
                }
            }

            return result;
        }

        private static UInt32 StringToUInt32(string hexString)
        {
            char[] arr = hexString.ToCharArray();
            string hexConvert = "0000" + arr[2] + arr[3] + arr[0] + arr[1];
            UInt32 result = UInt32.Parse(hexConvert, System.Globalization.NumberStyles.HexNumber);
            return result;
        }
        #endregion
    }
}
