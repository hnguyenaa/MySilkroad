using SilkroadSecurityApi;
using SroBasic.Metadata;
using SroBasic.Models;
using SroBasic.Models.PacketData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0x3015] Single Spawn
    /// </summary>
    public static class _0x3015
    {
        private static SingleSpawn Parse(Packet packet)
        {
            SingleSpawn data = new SingleSpawn();
            try
            {
                uint objectID = packet.ReadUInt32();

                if (MediaData.Mobs.ContainsKey(objectID)) // object is mob
                {
                    Mob mob = MediaData.Mobs[objectID];

                    #region Mob Parsing
                    if (mob.Type.StartsWith("MOB"))
                    {
                        MobSpawn mobSpawned = ParseMobSpawned(packet);

                        data.Type = SpawnedType.Mob;
                        data.Mob = mobSpawned;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Views.BindingFrom.WriteLine("[ERROR][0x3015][Single Spawn] " + ex.Message);
            }
            return data;
        }
        private static void Share(SingleSpawn data)
        {
            if (data.Type == SpawnedType.Mob)
            {
                //Metadata.Globals.MobSpawns.Add(data.Mob);
                Globals.AddMob(data.Mob);
            }
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }

        #region Helps parse packet
        //public static ItemSpawned ParseItemSpawned(Packet packet, Item item)
        //{
        //    ItemSpawned result = new ItemSpawned();
        //    try
        //    {
        //        string type = item.Type;

        //        if (type.StartsWith("ITEM_ETC_GOLD"))
        //        {
        //            packet.ReadUInt32(); // Amount:so tien
        //        }
        //        else if (type.StartsWith("ITEM_QSP"))
        //        {
        //            packet.ReadAscii(); // Name
        //        }
        //        else if (type.StartsWith("ITEM_CH") || type.StartsWith("ITEM_EU"))
        //        {
        //            packet.ReadUInt8(); // Plus
        //        }

        //        uint uniqueID = packet.ReadUInt32(); //  UniqueID

        //        packet.ReadUInt8(); //XSEC
        //        packet.ReadUInt8(); //YSEC
        //        packet.ReadSingle(); //X
        //        packet.ReadSingle(); //Z
        //        packet.ReadSingle(); //Y

        //        packet.ReadUInt16(); //POS

        //        byte owner = packet.ReadUInt8();//is owner
        //        if (owner == 1) // Owner exist
        //        {
        //            uint ownerID = packet.ReadUInt32(); //characte id
        //        }

        //        byte blue = packet.ReadUInt8(); //Item Blued
        //        packet.ReadUInt8();//unk = 5
        //        packet.ReadUInt32();//mosterID

        //        result = new ItemSpawned(uniqueID, item, owner, blue);
        //    }
        //    catch (Exception ex)
        //    {
        //        Views.BindingView.Write("[ERROR] [ParseSpawnSpecificObject::ParseItemSpawned]");
        //        Views.BindingView.WriteLine(ex.Message);
        //    }
        //    return result;
        //}

        public static MobSpawn ParseMobSpawned(Packet packet)
        {
            MobSpawn result = new MobSpawn();
            //try
            //{
                uint uniqueID = packet.ReadUInt32(); // MOB UniqueID

                //current coor
                byte xsec = packet.ReadUInt8();
                byte ysec = packet.ReadUInt8();
                float xcoord = packet.ReadSingle();
                float zcoord = packet.ReadSingle();
                float ycoord = packet.ReadSingle();

                ushort Position = packet.ReadUInt16(); // Position
                byte move = packet.ReadUInt8(); // Moving
                byte Running = packet.ReadUInt8(); // Running

                //next coor
                if (move == 1)
                {
                    xsec = packet.ReadUInt8();
                    ysec = packet.ReadUInt8();
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
                }
                else
                {
                    packet.ReadUInt8(); // Unknown
                    packet.ReadUInt16(); // Unknwon
                }

                byte alive = packet.ReadUInt8(); // Alive
                packet.ReadUInt8(); // Unknown
                packet.ReadUInt8(); // Unknown
                packet.ReadUInt8(); // Zerk Active
                packet.ReadSingle(); // Walk Speed
                packet.ReadSingle(); // Run Speed
                packet.ReadSingle(); // Zerk Speed
                packet.ReadUInt32(); // Unknown
                byte type = packet.ReadUInt8();

                if (alive == 1)
                {
                    Coordinate coordinate = new Coordinate(xsec, ysec, xcoord, ycoord, zcoord);
                    double distance = Coordinate.Distance(coordinate, Globals.Character.Coordinate);
                    result = new MobSpawn(uniqueID, coordinate, distance);
                }
            //}
            //catch (Exception ex)
            //{
            //    Views.BindingView.Write("[ERROR] [ParseSpawnSpecificObject::ParseMobSpawned]");
            //    Views.BindingView.WriteLine(ex.Message);
            //}
            return result;
        }
        #endregion
    }
}
