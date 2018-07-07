using SroBasic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Metadata
{
    public enum IncreaseStatPointType
    {
        None,
        FullStrength,
        FullIntellect,
        Hybrid
    }

    public static class Configs
    {
        public static ClientConfig ClientConfig { get; set; }
        public static PatchConfig PatchConfig { get; set; }
        public static LoginConfig LoginConfig { get; set; }

        public static IncreaseStatPointType IncreaseStatPointType = IncreaseStatPointType.FullStrength;

        public static bool IsAutoZerk = true;

        private static string directory = "";

        static Configs()
        {
            directory = GetDirectoryConfig();
            ClientConfig = LoadingClientConfig(directory);
            PatchConfig = LoadingPatchConfig(directory);
        }

        private static string GetDirectoryConfig()
        {
            string result = "";
            string path = Environment.CurrentDirectory + @"\data\loading.ini";
            if (File.Exists(path))
            {
                using (TextReader reader = File.OpenText(path))
                {
                    string input;
                    while ((input = reader.ReadLine()) != null)
                    {
                        if (input.StartsWith("1"))
                        {
                            result = Environment.CurrentDirectory + @"\configs\" + input.Replace("1|", "").Trim();
                            break;
                        }
                    }
                }
            }

            return result;
        }
        private static ClientConfig LoadingClientConfig(string directory)
        {
            var clientInfo = new ClientConfig();
            string path = directory + @"\client.ini";
            if (File.Exists(path))
            {
                using (TextReader reader = File.OpenText(path))
                {
                    string input;
                    while ((input = reader.ReadLine()) != null)
                    {
                        #region
                        switch (input)
                        {
                            case "[file_path]":
                                while ((input = reader.ReadLine()) != null)
                                {
                                    if (input.StartsWith("1"))
                                    {
                                        clientInfo.ClientPath = input.Replace("1|", "").Trim();
                                        break;
                                    }
                                }
                                break;
                            case "[sro_type]":
                                while ((input = reader.ReadLine()) != null)
                                {
                                    if (input.StartsWith("1"))
                                    {
                                        clientInfo.SroType = input.Replace("1|", "").Trim();
                                        break;
                                    }
                                }
                                break;
                            case "[locale]":
                                while ((input = reader.ReadLine()) != null)
                                {
                                    if (input.StartsWith("1"))
                                    {
                                        byte locale = 0; byte.TryParse(input.Replace("1|", "").Trim(), out locale);
                                        clientInfo.Locale = locale;
                                        break;
                                    }
                                }
                                break;
                            case "[version]":
                                while ((input = reader.ReadLine()) != null)
                                {
                                    if (input.StartsWith("1"))
                                    {
                                        uint version = 0; uint.TryParse(input.Replace("1|", "").Trim(), out version);
                                        clientInfo.Version = version;
                                        break;
                                    }
                                }
                                break;
                            case "[gateway_server]":
                                while ((input = reader.ReadLine()) != null)
                                {
                                    if (input.StartsWith("1"))
                                    {
                                        string[] split = input.Replace("1|", "").Trim().Split(':');
                                        if (split.Length > 1)
                                        {
                                            IPAddress ip = IPAddress.Parse("127.0.0.1");
                                            IPAddress.TryParse(split[0], out ip);

                                            int port = 20001; int.TryParse(split[1], out port);

                                            clientInfo.GatewayServer = new IPEndPoint(ip, port);
                                        }
                                        break;
                                    }
                                }
                                break;
                        }
                        #endregion
                    }
                }
            }

            return clientInfo;
        }
        private static PatchConfig LoadingPatchConfig(string directory)
        {
            var patchConfig = new PatchConfig();
            string path = directory + @"\patch.ini";
            if (File.Exists(path))
            {
                using (TextReader reader = File.OpenText(path))
                {
                    string input;
                    while ((input = reader.ReadLine()) != null)
                    {
                        #region
                        switch (input)
                        {
                            case "[redirect_gateway_server]":
                                input = reader.ReadLine();
                                string[] split = input.Trim().Split(':');
                                if (split.Length > 1)
                                {
                                    IPAddress ip = IPAddress.Parse("127.0.0.1");
                                    IPAddress.TryParse(split[0], out ip);

                                    int port = 20001; int.TryParse(split[1], out port);

                                    patchConfig.RedirectGatewayServer = new IPEndPoint(ip, port);
                                }
                                break;
                            case "[redirect_agent_server]":
                                input = reader.ReadLine();
                                split = input.Trim().Split(':');
                                if (split.Length > 1)
                                {
                                    IPAddress ip = IPAddress.Parse("127.0.0.1");
                                    IPAddress.TryParse(split[0], out ip);

                                    int port = 20001; int.TryParse(split[1], out port);

                                    patchConfig.RedirectAgentServer = new IPEndPoint(ip, port);
                                }
                                break;
                            case "[multi_client]":
                                input = reader.ReadLine().Trim();
                                patchConfig.MultiClient = input.Equals("1");
                                break;
                            case "[redirect_ip]":
                                input = reader.ReadLine().Trim();
                                patchConfig.RedirectIP = input.Equals("1");
                                break;
                            case "[nude_patch]":
                                input = reader.ReadLine().Trim();
                                patchConfig.NudePatch = input.Equals("1");
                                break;
                            case "[zoom_hack]":
                                input = reader.ReadLine().Trim();
                                patchConfig.ZoomHack = input.Equals("1");
                                break;
                            case "[swear_filter]":
                                input = reader.ReadLine().Trim();
                                patchConfig.SwearFilter = input.Equals("1");
                                break;
                            case "[server_status]":
                                input = reader.ReadLine().Trim();
                                patchConfig.ServerStatus = input.Equals("1");
                                break;
                            case "[no_game_guard]":
                                input = reader.ReadLine().Trim();
                                patchConfig.NoGameGuard = input.Equals("1");
                                break;
                            case "[english_patch]":
                                input = reader.ReadLine().Trim();
                                patchConfig.EnglishPatch = input.Equals("1");
                                break;
                            case "[patch_seed]":
                                input = reader.ReadLine().Trim();
                                patchConfig.PatchSeed = input.Equals("1");
                                break;
                        }
                        #endregion
                    }
                }
            }
            else
            {                       
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                patchConfig.RedirectGatewayServer = new IPEndPoint(ip, 20001);
                patchConfig.RedirectAgentServer = new IPEndPoint(ip, 20002);
                patchConfig.MultiClient = true;
                patchConfig.RedirectIP = true;
            }

            return patchConfig;
        }
        private static LoginConfig LoadingLoginConfig(string directory)
        {
            var loginConfig = new LoginConfig();
            string path = directory + @"\login.ini";
            if (File.Exists(path))
            {
                using (TextReader reader = File.OpenText(path))
                {
                    string input;
                    while ((input = reader.ReadLine()) != null)
                    {
                        #region
                        switch (input)
                        {
                            case "[username]":
                                input = reader.ReadLine();
                                loginConfig.Username = input.Trim();
                                break;
                            case "[password]":
                                input = reader.ReadLine();
                                loginConfig.Username = input.Trim();
                                break;
                        }
                        #endregion
                    }
                }
            }
            else
            {
                loginConfig.Username = "hnguyenaa";
                loginConfig.Password = "12021989";
            }

            return loginConfig;
        }

        public static void Save()
        {
            //string folder = Environment.CurrentDirectory + @"\configs";
            //if (!Directory.Exists(folder))
            //{
            //    Directory.CreateDirectory(folder);
            //}

            //string pathFile = folder + "config.ini";
            //using (var writer = new StreamWriter(pathFile))
            //{
            //    if (SroPath != "")
            //    {
            //        writer.WriteLine("[Sro_Path]");
            //        writer.WriteLine(SroPath);
            //    }
            //}
            
        }
        
    }


}
