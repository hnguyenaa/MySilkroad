using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0x303D] Character Info
    /// </summary>
    public static class _0x303D
    {
        class Data
        {
            public uint MaxHP { get; set; }
            public uint MaxMP { get; set; }
        }
        private static Data Parse(Packet packet)
        {
            packet.ReadUInt32();//	4	uint	PhyAtkMin
            packet.ReadUInt32();//	4	uint	PhyAtkMax
            packet.ReadUInt32();//	4	uint	MagAtkMin
            packet.ReadUInt32();//	4	uint	MagAtkMax
            packet.ReadUInt16();//	2	ushort	PhyDef
            packet.ReadUInt16();//	2	ushort	MagDef
            packet.ReadUInt16();//	2	ushort	HitRate
            packet.ReadUInt16();//	2	ushort	ParryRate
            uint maximumHP = packet.ReadUInt32();//	4	uint	MaxHP
            uint maximumMP = packet.ReadUInt32();//	4	uint	MaxMP
            packet.ReadUInt16();//	2	ushort	STR
            packet.ReadUInt16();//	2	ushort	INT

            var data = new Data { MaxHP = maximumHP, MaxMP = maximumMP };
            return data;
        }
        private static void Share(Data data)
        {
            Metadata.Globals.Character.MaxHP = data.MaxHP;
            Metadata.Globals.Character.MaxMP = data.MaxMP;

            Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.HP_MP);
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
