using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0xB045] Object Select
    /// </summary>
    public static class _0xB045
    {
        class Data
        {
            public uint ObjectSelectID { get; set; }
            public bool IsSelectSuccess { get; set; }
            public bool IsStillAlive { get; set; }
        }

        private static Data Parse(Packet packet)
        {
            Data data = new Data();
            try
            {
                byte flag = packet.ReadUInt8();
                if (flag == 1) // success
                {
                    uint selectedid = packet.ReadUInt32();
                    packet.ReadUInt8();//alway =0x01

                    //if (Metadata.Globals.MobSpawns.Exists(a=>a.UniqueID == selectedid))
                    //{
                    //    uint hp = packet.ReadUInt32();

                    //    data.ObjectSelectID = selectedid;
                    //    data.IsSelectSuccess = true;
                    //    if (hp > 0)
                    //        data.IsStillAlive = true;
                    //    else
                    //        data.IsStillAlive = false;
                    //}
                    if (Metadata.Globals.MobSpawns.ContainsKey(selectedid))
                    {
                        uint hp = packet.ReadUInt32();

                        data.ObjectSelectID = selectedid;
                        data.IsSelectSuccess = true;
                        if (hp > 0)
                            data.IsStillAlive = true;
                        else
                            data.IsStillAlive = false;
                    }
                }
                else if (flag == 0x02)//mob not exist
                {
                    data.IsSelectSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Views.BindingFrom.WriteLine("[ERROR][0xB045][Object Select] " + ex.Message);
            }
            return data;
        }
        //private static Data Parse(Packet packet)
        //{
        //    Data data = new Data();

        //    byte flag = packet.ReadUInt8();
        //    if (flag == 1) // success
        //    {
        //        uint selectedid = packet.ReadUInt32();
        //        packet.ReadUInt8();//alway =0x01

        //        if (Globals.Spawn.IsExistMob(selectedid))
        //        {
        //            uint hp = packet.ReadUInt32();

        //            data.ObjectSelectID = selectedid;
        //            data.IsSelectSuccess = true;
        //            if (hp > 0)
        //                data.IsStillAlive = true;
        //            else
        //                data.IsStillAlive = false;
        //        }
        //    }
        //    else if (flag == 0x02)//mob not exist
        //    {
        //        data.IsSelectSuccess = false;
        //    }

        //    return data;
        //}
        private static void Share(Data data)
        {
            if (data.IsSelectSuccess)
            {
                if (data.IsStillAlive)
                {
                    Views.BindingFrom.WriteLine("[0xB045][Object Select] select object id =" + data.ObjectSelectID);
                    //Metadata.Globals.SetMobSelected(data.ObjectSelectID);

                    Bot.BotInput.DoWork_SelectMobSuccess(data.ObjectSelectID);
                }
                else
                {
                    Bot.BotInput.DoWork_SelectMobFall();
                }
            }
            else
            {
                Bot.BotInput.DoWork_SelectMobFall();
            }
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
