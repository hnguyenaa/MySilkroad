using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0xB074] Object Action
    /// </summary>
    public static class _0xB074
    {
        enum ActionType
        {
            None = 0,
            Successful = 0x01,
            Unsuccessful = 0x02
        }
        struct DataPacket0xB074
        {
            public ActionType ActionType { get; set; }
            public byte WaitForAction { get; set; }

            public void SetActionType(byte value)
            {
                switch (value)
                {
                    case 0x01:
                        ActionType = ActionType.Successful;
                        break;
                    case 0x02:
                        ActionType = ActionType.Unsuccessful;
                        break;
                    default:
                        ActionType = ActionType.None;
                        break;
                }
            }
        }

        enum ResultType
        {
            None = 0x00,
            Attack = 0x01,
            Obstactle = 0x02,
            NotEnoughMP = 0x03
        }
        public static void Parse(Packet packet)
        {
            byte flag = packet.ReadUInt8();
            var resultType = (ResultType)flag;

            switch (resultType)
            {
                case ResultType.Attack:
                    //byte flag2 = packet.ReadUInt8();// 0x01 : Waffe ; 0x02 : hand
                    break;
                case ResultType.Obstactle:
                    byte value = packet.ReadUInt8(); //immer 00 ?
                    if (value == 0x00)
                    {
                        Views.BindingFrom.WriteLine("[0xB074][Object Action][Obstactle] value = " + value + " | action = " + flag + " | " + resultType);
                        Bot.BotInput.DoWord_MobBehindObstacle();
                    }
                    //else
                    //{
                    //    Views.BindingFrom.WriteLine("[0xB074][Object Action][Obstactle] different value = " + value + " | action = " + flag + " | " + resultType);
                    //}
                    break;
                case ResultType.NotEnoughMP:
                    //byte nr2 = packet.ReadUInt8();//Normal attack
                    //if (nr2 == 4)
                    //{
                    //    byte nr3 = packet.ReadUInt8();
                    //    if (nr3 == 64)
                    //    {
                    //    }
                    //}
                    break;
                default:
                    Views.BindingFrom.WriteLine("[0xB074][Object Action][Unkown Type] value = " + resultType);
                    break;
            }
        }
        private static void Share(object data)
        {

        }
        public static void DoWork(Packet packet)
        {
            Parse(packet);
            //byte actionResult = packet.ReadUInt8();
            //byte waitAction = packet.ReadUInt8();

            //if (actionResult == 1)//accept action
            //{
            //    if (Bot.BotMedia.UsingSkillStatus == Bot.TypeOfUsingSkill.RequestUsingSkill)
            //    {
            //        if (waitAction == 0)
            //        {
            //            Bot.BotMedia.UsingSkillStatus = Bot.TypeOfUsingSkill.UsingSkillSuccess;
            //            Bot.BotInput.ContinueAfterCastSkill();
            //            Views.BindingView.WriteLine("[_0xB074_ObjectAction:accept action] UsingSkillSuccess -> ContinueAfterCastSkill");
            //        }
            //        else if (waitAction == 1)
            //        {
            //            Bot.BotMedia.UsingSkillStatus = Bot.TypeOfUsingSkill.WaitingCastSkill;
            //            Views.BindingView.WriteLine("[_0xB074_ObjectAction::accept action] WaitingCastSkill");
            //        }
            //    }
            //}
            //else if (actionResult == 2)//next action
            //{
            //    if (Bot.BotMedia.UsingSkillStatus == Bot.TypeOfUsingSkill.WaitingCastSkill)
            //    {
            //        if (waitAction == 0)
            //        {
            //            Bot.BotMedia.UsingSkillStatus = Bot.TypeOfUsingSkill.UsingSkillSuccess;
            //            Bot.BotInput.ContinueAfterCastSkill();
            //            Views.BindingView.WriteLine("[_0xB074_ObjectAction::DoWork(next action)] UsingSkillSuccess -> ContinueAfterCastSkill");
            //        }
            //    }
            //}

            //if ((actionResult != 1 && actionResult != 2) || (waitAction != 0 && waitAction != 1))
            //{
            //    Views.BindingView.Write("[Exception][_0xB074_ObjectAction::DoWork]");
            //    byte[] packet_bytes = packet.GetBytes();
            //    Views.BindingView.WriteLine(String.Format("[S->P][AG_RM][{0:X4}][{1} bytes]{2}{3}{4}{5}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes)));
            //}
        }
    }
}
