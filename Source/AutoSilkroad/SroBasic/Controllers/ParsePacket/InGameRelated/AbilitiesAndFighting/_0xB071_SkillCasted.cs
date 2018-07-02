using SilkroadSecurityApi;
using SroBasic.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0xB071] Skill Casted
    /// </summary>
    public static class _0xB071
    {
        class Data
        {
            public uint TempSkillID { get; set; }
            public uint ObjectBeAttacked { get; set; }
            public ObjectWasAttackedType Status { get; set; }

            public Data()
            {
                Status = ObjectWasAttackedType.None;
            }
        }

        enum ObjectWasAttackedType
        {
            Alive = 0x00,
            None = 0x01,
            Die = 0x80,

        }

        private static Data Parse(Packet packet)
        {
            Data data = new Data();

            var isAccess = packet.ReadUInt8();
            if (isAccess == 0x01)
            {
                uint temp_skill_id = packet.ReadUInt32();//unk away = 0xB070.temp
                uint objectWasAttacked = packet.ReadUInt32();
                var isNextRead = packet.ReadUInt8();
                if (isNextRead == 0x01)
                {
                    isAccess = packet.ReadUInt8();
                    var countObject = packet.ReadUInt8(); // = 01/02/03 (Number of mobs was attacked)
                    var objectId = packet.ReadUInt32();// = objectWasAttacked
                    var objectStatus = packet.ReadUInt8();// status
                    //packet.ReadUInt32(); //not need
                    //packet.ReadUInt32(); //not need

                    //else if (mobStatus == 0x04 || mobStatus == 0x08)//nock down
                    //{
                    //}
                    //else //unk
                    //{
                    //}
                    data.Status = (ObjectWasAttackedType)objectStatus;
                }

                data.TempSkillID = temp_skill_id;
                data.ObjectBeAttacked = objectWasAttacked;
            }
            
            return data;
        }

        //private static Data Parse(Packet packet)
        //{
        //    Data data = new Data();
        //    //try
        //    //{
        //    if (packet.ReadUInt8() == 0x01)
        //    {
        //        uint temp_skill_id = packet.ReadUInt32();//unk away = 0xB070.temp
        //        uint objectBeingAttacked = packet.ReadUInt32();
        //        if (objectBeingAttacked > 0 && objectBeingAttacked != Globals.Character.UniqueID)
        //        {
        //            packet.ReadUInt8();//uk alway = 01
        //            packet.ReadUInt8();//uk alway = 01
        //            byte mobCount = packet.ReadUInt8();// = 01/02/03 (Number of mobs was attacked)
        //            objectBeingAttacked = packet.ReadUInt32();//temp skill id
        //            byte mobStatus = packet.ReadUInt8();

        //            data.TempSkillID = temp_skill_id;
        //            if (mobStatus == 0x80) //monster die
        //            {
        //                data.MobIsAlive = false;
        //            }
        //            else
        //            {
        //                data.MobIsAlive = true;
        //            }
        //            //else if (mobStatus == 0x04 || mobStatus == 0x08)//nock down
        //            //{
        //            //}
        //            //else //unk
        //            //{
        //            //}
        //        }
        //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Views.BindingView.Write("[ERROR] [SkillCasted::ParsePacketOpcode0xB070]");
        //    //    Views.BindingView.WriteLine(ex.Message);
        //    //}
        //    return data;
        //}
        private static void Share(Data data)
        {
            //Views.BindingView.WriteLine("[_0xB071_SkillCasted::ShareData][" + data.TempSkillID + "][" + data.MobIsAlive + "]");
            if (data.Status == ObjectWasAttackedType.Die)
            {
                Views.BindingFrom.WriteLine("[0xB071][Skill Casted]: mod die id=" + data.ObjectBeAttacked);
                //Bot.BotInput.SelectNextMobForAttack();
            }
            else if (data.Status == ObjectWasAttackedType.Alive)
            {
                //Views.BindingFrom.WriteLine("[0xB071][Skill Casted]: mod Alive id=" + data.ObjectBeAttacked);
                //BotController.ContinueAttack();
                //Bot.BotInput.MobAttackedStillAlive();
            }

            if (data.ObjectBeAttacked == Globals.Character.UniqueID) { }
            
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
