using SilkroadSecurityApi;
using SroBasic.Metadata;
using SroBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0xB021] Character Move
    /// </summary>
    public static class _0xB021
    {
        class Data
        {
            public uint ObjectID { get; set; }
            public Coordinate Coordinate { get; set; }
        }
        private static Data Parse(Packet packet)
        {
            Data data = new Data();
            //try
            //{
            uint objectID = packet.ReadUInt32();
            byte flag = packet.ReadUInt8();
            if (flag == 0x01)
            {
                byte xsec = packet.ReadUInt8();
                byte ysec = packet.ReadUInt8();
                float xcoord = 0;
                float zcoord = 0;
                float ycoord = 0;
                if (ysec == 0x80)
                {
                    xcoord = packet.ReadUInt16() - packet.ReadUInt16();
                    zcoord = packet.ReadUInt16() - packet.ReadUInt16();
                    ycoord = packet.ReadUInt16() - packet.ReadUInt16();
                }
                else
                {
                    xcoord = packet.ReadUInt16();
                    zcoord = packet.ReadUInt16();
                    ycoord = packet.ReadUInt16();
                }
                int real_xcoord = 0;
                int real_ycoord = 0;
                if (xcoord > 32768)
                {
                    real_xcoord = (int)(65536 - xcoord);
                }
                else
                {
                    real_xcoord = (int)xcoord;
                }
                if (ycoord > 32768)
                {
                    real_ycoord = (int)(65536 - ycoord);
                }
                else
                {
                    real_ycoord = (int)ycoord;
                }

                Coordinate coordinate = new Coordinate(xsec, ysec, real_xcoord, real_ycoord, zcoord);

                data.ObjectID = objectID;
                data.Coordinate = coordinate;

            }
            //}
            //catch (Exception ex)
            //{
            //    Views.BindingView.Write("[ERROR] [ParsePacketDebug::MovementMove]");
            //    Views.BindingView.WriteLine(ex.Message);
            //}
            return data;
        }
        private static void Share(Data data)
        {
            if (Globals.Character.UniqueID == data.ObjectID)
            {
                Globals.Character.Coordinate = data.Coordinate;

                Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.Coordinate);
                //Globals.Spawn.UpdateAllDistance(data.Coordinate);
            }
            else
            {
                //update Coordinate if obj = mob
                Metadata.Globals.SetMobCoordinate(data.ObjectID, data.Coordinate);
                //Logic.Globals.Spawn.SetCoordinateMob(data.ObjectID, data.Coordinate); 
                //Globals.Spawn.SetCoordinateMobAndUpdateDistance(data.ObjectID, data.Coordinate,
                //    Globals.Character.Coordinate);
            }
        }
        public static void DoWork(Packet packet)
        {
            if (Metadata.Globals.IsDebug)
            {
                var data = Parse(packet);
                Share(data);
            }
            else
            {
                //var data = Parse(packet);
                //Share(data);
                ParseCompact(packet);
            }
        }

        private static void ParseCompact(Packet packet)
        {
            uint uniqueID = packet.ReadUInt32();
            byte resultCode = packet.ReadUInt8();
            if (resultCode == 0x01)
            {
                byte xsec = packet.ReadUInt8();
                byte ysec = packet.ReadUInt8();
                float xcoord = 0;
                float zcoord = 0;
                float ycoord = 0;
                if (ysec == 0x80)
                {
                    xcoord = packet.ReadUInt16() - packet.ReadUInt16();
                    zcoord = packet.ReadUInt16() - packet.ReadUInt16();
                    ycoord = packet.ReadUInt16() - packet.ReadUInt16();
                }
                else
                {
                    xcoord = packet.ReadUInt16();
                    zcoord = packet.ReadUInt16();
                    ycoord = packet.ReadUInt16();
                }
                int real_xcoord = 0;
                int real_ycoord = 0;
                if (xcoord > 32768)
                {
                    real_xcoord = (int)(65536 - xcoord);
                }
                else
                {
                    real_xcoord = (int)xcoord;
                }
                if (ycoord > 32768)
                {
                    real_ycoord = (int)(65536 - ycoord);
                }
                else
                {
                    real_ycoord = (int)ycoord;
                }

                Coordinate coordinate = new Coordinate(xsec, ysec, real_xcoord, real_ycoord, zcoord);
                if (Globals.Character.UniqueID == uniqueID)
                {
                    Globals.Character.Coordinate = coordinate;
                    Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.Coordinate);
                }
                else
                {
                    Metadata.Globals.SetMobCoordinate(uniqueID, coordinate);
                }
            }
        }
    }
}
