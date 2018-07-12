using Include;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseMediaData.Controllers
{
    public static class ParseSkillData
    {
        //private void LoadFile()
        //{
        //    string path = Environment.CurrentDirectory + @"\Media\server_dep\silkroad\textdata\skilldataenc.txt";
        //    if (!File.Exists(path))
        //    {
        //        path = Environment.CurrentDirectory + @"\Media\server_dep\silkroad\textdata\skilldata.txt";
        //    }

        //    if (!File.Exists(path))
        //    {
        //        throw new Exception("File \"skilldata.txt\" is not exist.");
        //    }

        //    using (TextReader reader = File.OpenText(path))
        //    {
        //        string input = "";
        //        while ((input = reader.ReadLine()) != null)
        //        {
        //            string temp = Environment.CurrentDirectory + @"\Media\server_dep\silkroad\textdata\" + input;
        //            if (File.Exists(@"Media\server_dep\silkroad\textdata\" + input.Replace(".txt", "enc.txt")))
        //            {
        //                input = input.Replace(".txt", "enc.txt");
        //                temp = temp.Replace(".txt", "enc.txt");
        //            }
        //            Decrypter.Decrypt(input);
        //            listPathOfFileSaveSkillData.Add(temp);
        //        }
        //    }
        //}

        public static List<string> ReadFile_skilldataenc()
        {
            List<string> listFileData = new List<string>();
            string path = Environment.CurrentDirectory + @"\Media\server_dep\silkroad\textdata\skilldataenc.txt";
            if (!File.Exists(path))
            {
                throw new Exception("File \"skilldataenc.txt\" is not exist.");
            }

            using (TextReader reader = File.OpenText(path))
            {
                string input = "";
                while ((input = reader.ReadLine()) != null)
                {
                    string temp = Environment.CurrentDirectory + @"\Media\server_dep\silkroad\textdata\" + input;
                    if (File.Exists(temp))
                    {
                        Decrypter.Decrypt(input);
                        listFileData.Add(temp);
                    }
                }
            }

            return listFileData;
        }

        public static List<string> ReadFile_skilldata()
        {
            List<string> listFileData = new List<string>();
            string path = Environment.CurrentDirectory + @"\Media\server_dep\silkroad\textdata\skilldata.txt";
            if (!File.Exists(path))
            {
                throw new Exception("File \"skilldata.txt\" is not exist.");
            }

            using (TextReader reader = File.OpenText(path))
            {
                string input = "";
                while ((input = reader.ReadLine()) != null)
                {
                    string temp = Environment.CurrentDirectory + @"\Media\server_dep\silkroad\textdata\" + input;
                    if (File.Exists(temp))
                    {
                        Decrypter.Decrypt(input);
                        listFileData.Add(temp);
                    }
                }
            }

            return listFileData;
        }

        public static List<string> ReadAllData(List<string> listPath)
        {
            List<string> rawData = new List<string>();
            foreach (var path in listPath)
            {
                if (!File.Exists(path)) continue;
                using (TextReader reader = File.OpenText(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("1"))
                            rawData.Add(line);
                    }
                }
            }

            return rawData;
        }

        public static void SaveToInsertSql(List<string> rawData)
        {          
            string folderOutput = Environment.CurrentDirectory + @"\output";
            if (!Directory.Exists(folderOutput))
            {
                Directory.CreateDirectory(folderOutput);
            }

            try
            {
                int chunkLimit = 1000;
                int colCount = 0;
                for (int i = 0; i < rawData.Count; i += 1000)
                {
                    var chunkWrite = rawData.Skip(i).Take(chunkLimit).ToList();
                    int number = Math.Min(rawData.Count, (i + 1000));
                    string pathInsert = folderOutput + @"\skill_insert_raw_" + number + ".sql";

                    using (TextWriter write = File.CreateText(pathInsert))
                    {
                        foreach (var item in chunkWrite)
                        {
                            //write.WriteLine(item);
                            var skill = RawRefSkill.Parse(item);
                            string insert = RawRefSkill.GenerateInsertSqlCode(skill);
                            if(!string.IsNullOrEmpty(insert))
                                write.WriteLine(insert);
                            //write.WriteLine(GenerateSql(item));

                        }

                    }
                }
            }
            catch { throw; }
        }

        private static string GenerateSql(string str)
        {
            string[] split = str.Split('\t');

            string insert = @"INSERT INTO dbo.RawSkill VALUES (@prmValues);";
            string values = "";

            for (int i = 0; i < split.Length; i++)
            {
                values += "'" + split[i] + "', ";
            }

            if (values.Length > 0) values = values.TrimEnd(',', ' ');

            insert = insert.Replace("@prmValues", values);

            return insert;
//INSERT INTO dbo.RawSkill VALUES ('1   36796   0   MSKILL_ARABIA_MAD_GENERAL_ATTACK08  ???? ??? 3??(??, ??? ??, HP 40%)    xxx 0   140 2   0   0   0   490 3010    4000    4000    0   0   0   1   0   60  1   1   0   0   0   0   0   1   1   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   3   0   0   255 255 0   0   0   0   0   255 255 255 255 xxx xxx xxx xxx xxx 100 89  0   6386804 5   133 33009   38125   100 28003   2   3   0   15000   100 14  20  1634165091  0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0   0');
            //INSERT INTO dbo.RawSkill VALUES ('1', '36885', '370', 'SKILL_SPEEDGM_01', '??? ???', 'SKILL_SPEEDGM', '0', '1', '1', '0', '0', '0', '0', '0', '1000', '0', '0', '0', '6', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '3', '0', '0', '255', '255', '0', '0', '0', '0', '0', '255', '255', '255', '255', 'item\etc\speedgm.ddj', 'SN_ITEM_SPEEDGM', 'xxx', 'SN_ITEM_SPEEDGM_TT_DESC', 'xxx', '0', '0', '3', '1667396966', '1685418593', '360000000', '1752396901', '600', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
        }
    }
}
