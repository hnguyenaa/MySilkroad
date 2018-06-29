using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public static class Config
    {
        public static IncreaseStatPointType IncreaseStatPointType = IncreaseStatPointType.FullStrength;

        public static bool IsAutoZerk = true;

        public static string SroPath = "";


        static Config()
        {

            string path = Environment.CurrentDirectory + @"\configs\config.ini";

            if (File.Exists(path))
            {
                using (TextReader reader = File.OpenText(path))
                {
                    string input = "";
                    while ((input = reader.ReadLine()) != null)
                    {
                        switch (input)
                        {
                            case "[Sro_Path]":
                                SroPath = reader.ReadLine();
                                break;
                            case "[AutoLogin]":
                                //Globals.MainWindow.SetText(Globals.MainWindow.username, config_reader.ReadLine().Split('=')[1]);
                                //Globals.MainWindow.SetCheck(Globals.MainWindow.autologin, config_reader.ReadLine().Split('=')[1]);
                                //Globals.MainWindow.SetCheck(Globals.MainWindow.characterselect, config_reader.ReadLine().Split('=')[1]);
                                //Globals.MainWindow.SetText(Globals.MainWindow.charnameselect, config_reader.ReadLine().Split('=')[1]);
                                break;
                        }
                    }
                }
            }

            
        }

        public static void Save()
        {
            string folder = Environment.CurrentDirectory + @"\configs";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string pathFile = folder + "config.ini";
            using (var writer = new StreamWriter(pathFile))
            {
                if (SroPath != "")
                {
                    writer.WriteLine("[Sro_Path]");
                    writer.WriteLine(SroPath);
                }
                //config_writer.WriteLine("[AutoLogin]");
                //config_writer.WriteLine("username=" + Globals.MainWindow.ReadText(Globals.MainWindow.username));
                //config_writer.WriteLine("autologincheck=" + Globals.MainWindow.Checked(Globals.MainWindow.autologin));
                //config_writer.WriteLine("characterselect=" + Globals.MainWindow.Checked(Globals.MainWindow.characterselect));
                //config_writer.WriteLine("charactername=" + Globals.MainWindow.ReadText(Globals.MainWindow.charnameselect));
            }
            
        }
        
    }


}
