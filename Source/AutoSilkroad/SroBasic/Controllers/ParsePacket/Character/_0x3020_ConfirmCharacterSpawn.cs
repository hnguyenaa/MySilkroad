using SilkroadSecurityApi;
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
    /// Confirm Character Spawn
    /// </summary>
    public static class _0x3020
    {
        public static ConfirmSpawnCharacter Parse(Packet packet)
        {
            uint uniqueID = packet.ReadUInt32();

            packet = new Packet(packet);
            byte[] skipCharacterId = new byte[4];
            skipCharacterId[0] = packet.ReadUInt8();
            skipCharacterId[1] = packet.ReadUInt8();
            skipCharacterId[2] = packet.ReadUInt8();
            skipCharacterId[3] = packet.ReadUInt8();

            ConfirmSpawnCharacter data = new ConfirmSpawnCharacter { SkipCharacterID = skipCharacterId.ToList(), UniqueID = uniqueID };
            return data;
        }

        private static CharacterData Parse_0x3013(Packet packet, List<byte> skipCharacterId)
        {
            CharacterData data = new CharacterData();

            #region Character
            packet.ReadUInt32();//	4	uint	*unk00
            packet.ReadUInt32(); //Model//	4	uint	RefObjID
            packet.ReadUInt8(); //Volume and Height//	1	byte	Scale
            byte level = packet.ReadUInt8();//	1	byte	Level
            packet.ReadUInt8();//	1	byte	MaxLevel
            packet.ReadUInt64();//	8	ulong	ExpOffset
            packet.ReadUInt32(); //SP bar//	4	uint	SExpOffset
            packet.ReadUInt64();//	8	ulong	RemainGold
            packet.ReadUInt32();//	4	uint	RemainSkillPoint
            ushort statPoint = packet.ReadUInt16();//	2	ushort	RemainStatPoint
            byte zerk = packet.ReadUInt8();//Zerk?!!////	1	byte	
            packet.ReadUInt32();//	4	uint	GatheredExpPoint
            uint hp = packet.ReadUInt32();//	4	uint	HP
            uint mp = packet.ReadUInt32();//	4	uint	MP
            packet.ReadUInt8(); //	1	byte	noob flag (1 = Beginner Icon, 2 = Helpful, 3 = Beginner&Helpful)
            packet.ReadUInt8(); //	1	byte	DailyPK
            packet.ReadUInt16();//	2	ushort	TotalPK
            packet.ReadUInt32();//	4	uint	PKPenaltyPoint
            packet.ReadUInt8();//	1	byte PK rank?
            packet.ReadUInt8();//	1	byte	*unk01 -> Check for != 0 max inv slots

            //set character 
            data.Level = level;
            data.StatPoint = statPoint;
            data.Zerk = zerk;
            data.HP = hp;
            data.MP = mp;
            #endregion

            #region Items
            byte inventorySlot = packet.ReadUInt8();//	1   byte    Total inventory slot 
            byte itemCount = packet.ReadUInt8();//	1	byte	Total items in inventory

            //set character's item info
            data.TotalInventorySlot = inventorySlot;
            data.TotalItem = itemCount;

            #region for loop
            var inventory = new Inventory();
            for (int y = 0; y < itemCount; y++)
            {
                byte slot = packet.ReadUInt8();//   1   byte    slot's item
                packet.ReadUInt32(); // 4 uint - Unknown 01
                uint itemId = packet.ReadUInt32(); //  4   uint    item_id

                var item_ = new Item();
                if (Metadata.MediaData.Items.ContainsKey(itemId))
                {
                    item_ = Metadata.MediaData.Items[itemId];

                    inventory = new Inventory
                    {
                        Slot = slot,
                        ID = item_.ID,
                        Name = item_.Name,
                        Type = item_.Type
                    };
                }

                if (item_.ID > 0) //found  Item
                {
                    string type = item_.Type;

                    if (type.StartsWith("ITEM_CH") || type.StartsWith("ITEM_EU")
                        || type.StartsWith("ITEM_MALL_AVATAR") || type.StartsWith("ITEM_ETC_E060529_GOLDDRAGONFLAG")
                        || type.StartsWith("ITEM_EVENT_CH") || type.StartsWith("ITEM_EVENT_EU")
                        || type.StartsWith("ITEM_EVENT_AVATAR_W_NASRUN") || type.StartsWith("ITEM_EVENT_AVATAR_M_NASRUN"))
                    {
                        #region Item for character
                        packet.ReadUInt8();//  1   byte item_plus
                        packet.ReadUInt64();//    8   ulong   item_modifier
                        uint durability = packet.ReadUInt32();//  4   uint Durability

                        byte blueamm = packet.ReadUInt8();//    1   byte    blueamm
                        for (int i = 0; i < blueamm; i++)
                        {
                            packet.ReadUInt32(); //  4   uint    stat_id
                            packet.ReadUInt32();//    4   uint    stat_value
                        }

                        packet.ReadUInt8(); //Unknwon
                        uint numSocket = packet.ReadUInt8(); //=>number socket
                        for (int i = 0; i < numSocket; i++)
                        {
                            packet.ReadUInt8(); //=>number socket
                            packet.ReadUInt32(); //=>number socket
                            packet.ReadUInt32(); //=>number socket
                        }

                        uint unk4 = packet.ReadUInt8(); //Unknwon
                        byte flag1 = packet.ReadUInt8(); // Flag ?
                        if (flag1 == 1)
                        {
                            packet.ReadUInt8(); //Unknown
                            packet.ReadUInt32(); // Unknown ID ? ADV Elexir ID ?
                            packet.ReadUInt32(); // Unknwon Count
                        }
                        #endregion
                        inventory.Count = 1;
                        inventory.Durability = durability;
                    }
                    else if (Metadata.MediaData.GrabPetItems.Contains(type) || Metadata.MediaData.AttackPetItems.Contains(type))
                    {

                        #region pet
                        byte flag = packet.ReadUInt8();//flag_uk_notIn
                        if (flag == 2 || flag == 3 || flag == 4)
                        {
                            packet.ReadUInt32(); //Model-unk12

                            packet.ReadAscii();//unk13(name pet)
                            if (!Metadata.MediaData.AttackPetItems.Contains(type))
                            {
                                packet.ReadUInt32();//unk14
                            }
                            packet.ReadUInt8();//unk15
                        }
                        #endregion
                        inventory.Count = 1;
                        inventory.Durability = 0;
                    }
                    else if ((type.StartsWith("ITEM_COS") && type.Contains("SILK"))
                        || (type.StartsWith("ITEM_EVENT_COS") && !type.Contains("_C_"))
                        || type.StartsWith("ITEM_COS_P"))
                    {
                        #region COS
                        byte flag = packet.ReadUInt8();
                        if (flag == 2 || flag == 3 || flag == 4)
                        {
                            packet.ReadUInt32(); //Model -unk8
                            packet.ReadAscii();//unk9
                            packet.ReadUInt8();//unk10
                            //if (!Metadata.AttackPetItems.Contains(type))
                            //{
                            //    packet.ReadUInt32();//unk11_if_notIN
                            //}
                        }
                        else
                        {
                            if (type.EndsWith("_1D"))
                            {
                                packet.ReadUInt8();//unk11_if_1D_flag1
                            }
                        }
                        #endregion
                        inventory.Count = 1;
                        inventory.Durability = 0;
                    }
                    
                    else if (type == "ITEM_ETC_TRANS_MONSTER" || type.StartsWith("ITEM_MALL_MAGIC_CUBE"))
                    {
                        packet.ReadUInt32();//unk16

                        inventory.Count = 1;
                        inventory.Durability = 0;
                    }
                    else
                    {
                        #region orther

                        ushort count = packet.ReadUInt16();//count(else):count
                        if (type.Contains("ITEM_ETC_ARCHEMY_ATTRSTONE") || type.Contains("ITEM_ETC_ARCHEMY_MAGICSTONE"))
                        {
                            if (type.Contains("ITEM_ETC_ARCHEMY_MAGICSTONE_SOLID") || type.Contains("ITEM_ETC_ARCHEMY_MAGICSTONE_LUCK"))
                            {
                            }
                            else
                            {
                                packet.ReadUInt8();//unk
                            }
                        }
                        #endregion
                        inventory.Count = count;
                        inventory.Durability = 0;

                    }
                }

                //set inventory for character
                data.Inventories.Add(inventory);
            }

            #endregion

            #endregion

            #region Avatars
            packet.ReadUInt8(); // Avatars Max // 05
            int avatarcount = packet.ReadUInt8();
            for (int i = 0; i < avatarcount; i++)
            {
                packet.ReadUInt8(); //Slot
                packet.ReadUInt32();//unk
                uint avatar_id = packet.ReadUInt32();//avatar_id (item.id)

                packet.ReadUInt8();//item.plus
                packet.ReadUInt64();//unk (item.modifier)
                packet.ReadUInt32();//unk (Durability)
                byte blueamm = packet.ReadUInt8();
                for (int a = 0; a < blueamm; a++)
                {
                    packet.ReadUInt32();//stat_id
                    packet.ReadUInt32();//stat_value
                }
                packet.ReadUInt32();//unk
            }
            packet.ReadUInt8(); //Avatars End
            #endregion

            #region Mastery
            byte mastery = packet.ReadUInt8();//	1	byte	MasteryFlag [0 = done, 1 = Mastery]
            while (mastery == 1)
            {
                packet.ReadUInt32(); // Mastery ID//		4	uint	Mastery.ID
                packet.ReadUInt8();  // Mastery LV//		1	byte	Mastery.Level
                mastery = packet.ReadUInt8(); // New Mastery Start / List End//		1	byte	 MasterFlag (0 = done, 1 = Mastery)
            }
            packet.ReadUInt8(); // Mastery END
            #endregion

            #region skill
            byte startRead = packet.ReadUInt8(); // Skill List Start
            while (startRead == 1)
            {
                uint skillID = packet.ReadUInt32(); // Skill ID
                packet.ReadUInt8();

                startRead = packet.ReadUInt8(); // New Skill Start / List End

                if (Metadata.MediaData.Skills.ContainsKey(skillID))
                {
                    var skill = Metadata.MediaData.Skills[skillID];
                    data.Skills.Add(skill);
                }
                else
                {
                    data.Skills.Add(new Skill(skillID));
                }
            }

            #endregion

            #region Skipping Quest Part
            //var skipCharacterId = GlobalData._0x3020_Data.SkipCharacterID;
            while (true)
            {
                if (packet.ReadUInt8() == skipCharacterId[0])
                {
                    if (packet.ReadUInt8() == skipCharacterId[1])
                    {
                        if (packet.ReadUInt8() == skipCharacterId[2])
                        {
                            if (packet.ReadUInt8() == skipCharacterId[3])
                            {
                                break;
                            }
                        }
                    }
                }
            }
            #endregion

            #region Walking

            byte xsec = packet.ReadUInt8();
            byte ysec = packet.ReadUInt8();
            float xcoord = packet.ReadSingle();
            float zcoord = packet.ReadSingle();
            float ycoord = packet.ReadSingle();

            packet.ReadUInt16(); // Position
            packet.ReadUInt8(); // Move ?? Maybie Useless
            packet.ReadUInt8(); // Run
            packet.ReadUInt8();
            packet.ReadUInt16();
            packet.ReadUInt8();
            packet.ReadUInt8(); //DeathFlag
            packet.ReadUInt8(); //Movement Flag
            packet.ReadUInt8(); //Berserker Flag
            float walkSpeed = packet.ReadSingle();// *1.1f; //Walking Speed
            float runSpeed = packet.ReadSingle();// *1.1f; //Running Speed
            float zerkSpeed = packet.ReadSingle();// *1.1f; //Berserker Speed
            packet.ReadUInt8();
            string characterName = packet.ReadAscii();
            packet.ReadAscii(); // ALIAS

            //set character info
            data.Coordinate = new Coordinate(xsec, ysec, xcoord, ycoord, zcoord);
            data.WalkSpeed = walkSpeed;
            data.RunSpeed = runSpeed;
            data.ZerkSpeed = zerkSpeed;
            data.Name = characterName;
            #endregion

            #region Job

            packet.ReadUInt8(); // Job Level
            packet.ReadUInt8(); // Job Type
            packet.ReadUInt32(); // Trader Exp
            packet.ReadUInt32(); // Thief Exp
            packet.ReadUInt32(); // Hunter Exp
            packet.ReadUInt8(); // Trader LV
            packet.ReadUInt8(); // Thief LV
            packet.ReadUInt8(); // Hunter LV
            packet.ReadUInt8(); // PK Flag
            packet.ReadUInt16(); // Unknown
            packet.ReadUInt32(); // Unknown
            packet.ReadUInt16(); // Unknown
            uint characterID = packet.ReadUInt32(); // Account ID

            data.ID = characterID;
            #endregion

            return data;
        }

        private static void Share(ConfirmSpawnCharacter data, CharacterData characterData, bool isClientless)
        {
            Character character = new Character(characterData);
            Metadata.Globals.Character = character;
            Metadata.Globals.Character.UniqueID = data.UniqueID;

            Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.All);

            if (isClientless)
            {
                Packet p = new Packet(0x750E);
                SroBasic.Controllers.ThreadProxy.ProxyClientless.SendPacketToAgentRemote(p);

                Packet p2 = new Packet(0x3012);
                SroBasic.Controllers.ThreadProxy.ProxyClientless.SendPacketToAgentRemote(p2);

            }
        }
        public static void DoWork(Packet packet, bool isClientless = false)
        {
            var _0x3020Data = Parse(packet);
            //var _0x3013Data = Parse_0x3013(_0x3013._0x3013_Packet, _0x3020Data.SkipCharacterID);
            var _0x3013Data = Parse_0x3013_Debug(_0x3013._0x3013_Packet, _0x3020Data.SkipCharacterID);
            Share(_0x3020Data, _0x3013Data, isClientless);
        }

        private static CharacterData Parse_0x3013_Debug(Packet packet, List<byte> skipCharacterId)
        {
            CharacterData data = new CharacterData();
            try
            {
                #region Character
                packet.ReadUInt32();//	4	uint	*unk00
                packet.ReadUInt32(); //Model//	4	uint	RefObjID
                packet.ReadUInt8(); //Volume and Height//	1	byte	Scale
                byte level = packet.ReadUInt8();//	1	byte	Level
                packet.ReadUInt8();//	1	byte	MaxLevel
                packet.ReadUInt64();//	8	ulong	ExpOffset
                packet.ReadUInt32(); //SP bar//	4	uint	SExpOffset
                packet.ReadUInt64();//	8	ulong	RemainGold
                packet.ReadUInt32();//	4	uint	RemainSkillPoint
                ushort statPoint = packet.ReadUInt16();//	2	ushort	RemainStatPoint
                byte zerk = packet.ReadUInt8();//Zerk?!!////	1	byte	
                packet.ReadUInt32();//	4	uint	GatheredExpPoint
                uint hp = packet.ReadUInt32();//	4	uint	HP
                uint mp = packet.ReadUInt32();//	4	uint	MP
                packet.ReadUInt8(); //	1	byte	noob flag (1 = Beginner Icon, 2 = Helpful, 3 = Beginner&Helpful)
                packet.ReadUInt8(); //	1	byte	DailyPK
                packet.ReadUInt16();//	2	ushort	TotalPK
                packet.ReadUInt32();//	4	uint	PKPenaltyPoint
                packet.ReadUInt8();//	1	byte PK rank?
                packet.ReadUInt8();//	1	byte	*unk01 -> Check for != 0 max inv slots

                //set character 
                data.Level = level;
                data.StatPoint = statPoint;
                data.Zerk = zerk;
                data.HP = hp;
                data.MP = mp;
                #endregion

                #region Items
                byte totalInventorySlot = packet.ReadUInt8();//	1   byte    Total inventory slot 
                byte itemCount = packet.ReadUInt8();//	1	byte	Total items in inventory

                //set character's item info
                data.TotalInventorySlot = totalInventorySlot; Views.BindingFrom.WriteLine("TotalInventorySlot: " + totalInventorySlot);
                data.TotalItem = itemCount; Views.BindingFrom.WriteLine("TotalItem: " + itemCount);

                #region for loop
                try
                {
                    var inventory = new Inventory();
                    for (int y = 0; y < itemCount; y++)
                    {
                        byte slot = packet.ReadUInt8();//   1   byte    slot's item
                        packet.ReadUInt32(); // 4 uint - Unknown 01
                        uint itemId = packet.ReadUInt32(); //  4   uint    item_id

                        if (Metadata.MediaData.Items.ContainsKey(itemId))
                        {
                            var item_ = Metadata.MediaData.Items[itemId];
                            //Views.BindingFrom.WriteLine("Item: " + item_.ToString());
                            inventory = new Inventory
                            {
                                Slot = slot,
                                ID = item_.ID,
                                Name = item_.Name,
                                Type = item_.Type
                            };

                            string type = item_.Type;

                            Views.BindingFrom.WriteLine("TotalItem: sadasdad " + type);
                            if (Metadata.MediaData.GrabPetItems.Contains(type))
                            {
                                Views.BindingFrom.WriteLine("TotalItem: sadasdad asdasda " + type);
                            }

                            if (type.StartsWith("ITEM_CH") || type.StartsWith("ITEM_EU")
                                || type.StartsWith("ITEM_MALL_AVATAR") || type.StartsWith("ITEM_ETC_E060529_GOLDDRAGONFLAG")
                                || type.StartsWith("ITEM_EVENT_CH") || type.StartsWith("ITEM_EVENT_EU")
                                || type.StartsWith("ITEM_EVENT_AVATAR_W_NASRUN") || type.StartsWith("ITEM_EVENT_AVATAR_M_NASRUN"))
                            {
                                #region Item for character
                                packet.ReadUInt8();//  1   byte item_plus
                                packet.ReadUInt64();//    8   ulong   item_modifier
                                uint durability = packet.ReadUInt32();//  4   uint Durability

                                byte blueamm = packet.ReadUInt8();//    1   byte    blueamm
                                for (int i = 0; i < blueamm; i++)
                                {
                                    packet.ReadUInt32(); //  4   uint    stat_id
                                    packet.ReadUInt32();//    4   uint    stat_value
                                }

                                packet.ReadUInt8(); //Unknwon
                                uint numSocket = packet.ReadUInt8(); //=>number socket
                                for (int i = 0; i < numSocket; i++)
                                {
                                    packet.ReadUInt8(); //=>number socket
                                    packet.ReadUInt32(); //=>number socket
                                    packet.ReadUInt32(); //=>number socket
                                }

                                uint unk4 = packet.ReadUInt8(); //Unknwon
                                byte flag1 = packet.ReadUInt8(); // Flag ?
                                if (flag1 == 1)
                                {
                                    packet.ReadUInt8(); //Unknown
                                    packet.ReadUInt32(); // Unknown ID ? ADV Elexir ID ?
                                    packet.ReadUInt32(); // Unknwon Count
                                }
                                #endregion
                                inventory.Count = 1;
                                inventory.Durability = durability;
                            }
                            else if (Metadata.MediaData.GrabPetItems.Contains(type) || Metadata.MediaData.AttackPetItems.Contains(type))
                            {

                                #region pet
                                byte flag = packet.ReadUInt8();//flag_uk_notIn
                                Views.BindingFrom.WriteLine("flag: " + flag);
                                if (flag == 2 || flag == 3 || flag == 4)
                                {
                                    packet.ReadUInt32(); //Model-unk12

                                    packet.ReadAscii();//unk13(name pet)
                                    if (!Metadata.MediaData.AttackPetItems.Contains(type))
                                    {
                                        packet.ReadUInt32();//unk14
                                    }
                                    packet.ReadUInt8();//unk15
                                }
                                #endregion
                                inventory.Count = 1;
                                inventory.Durability = 0;
                            }
                            else if ((type.StartsWith("ITEM_COS") && type.Contains("SILK"))
                                || (type.StartsWith("ITEM_EVENT_COS") && !type.Contains("_C_"))
                                || type.StartsWith("ITEM_COS_P"))
                            {
                                #region COS
                                byte flag = packet.ReadUInt8();
                                if (flag == 2 || flag == 3 || flag == 4)
                                {
                                    packet.ReadUInt32(); //Model -unk8
                                    packet.ReadAscii();//unk9
                                    packet.ReadUInt8();//unk10
                                    //if (!Metadata.AttackPetItems.Contains(type))
                                    //{
                                    //    packet.ReadUInt32();//unk11_if_notIN
                                    //}
                                }
                                else
                                {
                                    if (type.EndsWith("_1D"))
                                    {
                                        packet.ReadUInt8();//unk11_if_1D_flag1
                                    }
                                }
                                #endregion
                                inventory.Count = 1;
                                inventory.Durability = 0;
                            }

                            else if (type == "ITEM_ETC_TRANS_MONSTER" || type.StartsWith("ITEM_MALL_MAGIC_CUBE"))
                            {
                                packet.ReadUInt32();//unk16

                                inventory.Count = 1;
                                inventory.Durability = 0;
                            }
                            else
                            {
                                #region orther

                                ushort count = packet.ReadUInt16();//count(else):count
                                if (type.Contains("ITEM_ETC_ARCHEMY_ATTRSTONE") || type.Contains("ITEM_ETC_ARCHEMY_MAGICSTONE"))
                                {
                                    if (type.Contains("ITEM_ETC_ARCHEMY_MAGICSTONE_SOLID") || type.Contains("ITEM_ETC_ARCHEMY_MAGICSTONE_LUCK"))
                                    {
                                    }
                                    else
                                    {
                                        packet.ReadUInt8();//unk
                                    }
                                }
                                #endregion
                                inventory.Count = count;
                                inventory.Durability = 0;
                            }

                        }
                        else
                        {
                            Views.BindingFrom.WriteLine("[error][Item Loop]: unkonw item slot|id :" + slot + "|" + itemId + ":" + itemId.ToString("X8"));
                            ushort count = packet.ReadUInt16();
                        }

                        Views.BindingFrom.WriteLine("inventory: " + inventory.ToString());

                        //set inventory for character
                        data.Inventories.Add(inventory);
                    }
                }
                catch (Exception ex)
                {
                    Views.BindingFrom.WriteLine("[Error][ItemsLoop]: " + ex.Message);
                }
                #endregion

                #endregion

                #region Avatars
                packet.ReadUInt8(); // Avatars Max // 05
                int avatarcount = packet.ReadUInt8();
                for (int i = 0; i < avatarcount; i++)
                {
                    packet.ReadUInt8(); //Slot
                    packet.ReadUInt32();//unk
                    uint avatar_id = packet.ReadUInt32();//avatar_id (item.id)

                    packet.ReadUInt8();//item.plus
                    packet.ReadUInt64();//unk (item.modifier)
                    packet.ReadUInt32();//unk (Durability)
                    byte blueamm = packet.ReadUInt8();
                    for (int a = 0; a < blueamm; a++)
                    {
                        packet.ReadUInt32();//stat_id
                        packet.ReadUInt32();//stat_value
                    }
                    packet.ReadUInt32();//unk
                }
                packet.ReadUInt8(); //Avatars End
                #endregion

                #region Mastery
                byte mastery = packet.ReadUInt8();//	1	byte	MasteryFlag [0 = done, 1 = Mastery]
                while (mastery == 1)
                {
                    packet.ReadUInt32(); // Mastery ID//		4	uint	Mastery.ID
                    packet.ReadUInt8();  // Mastery LV//		1	byte	Mastery.Level
                    mastery = packet.ReadUInt8(); // New Mastery Start / List End//		1	byte	 MasterFlag (0 = done, 1 = Mastery)
                }
                packet.ReadUInt8(); // Mastery END
                #endregion

                #region skill
                byte startRead = packet.ReadUInt8(); // Skill List Start
                while (startRead == 1)
                {
                    uint skillID = packet.ReadUInt32(); // Skill ID
                    packet.ReadUInt8();

                    startRead = packet.ReadUInt8(); // New Skill Start / List End

                    if (Metadata.MediaData.Skills.ContainsKey(skillID))
                    {
                        var skill = Metadata.MediaData.Skills[skillID];
                        data.Skills.Add(skill);
                    }
                }

                #endregion

                #region Skipping Quest Part
                //var skipCharacterId = GlobalData._0x3020_Data.SkipCharacterID;
                try
                {
                    while (true)
                    {
                        if (packet.ReadUInt8() == skipCharacterId[0])
                        {
                            if (packet.ReadUInt8() == skipCharacterId[1])
                            {
                                if (packet.ReadUInt8() == skipCharacterId[2])
                                {
                                    if (packet.ReadUInt8() == skipCharacterId[3])
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Views.BindingFrom.WriteLine("[Error][0x3013][Char info][Skipping Quest Part] " + ex.Message);
                }
                #endregion

                #region Walking

                byte xsec = packet.ReadUInt8();
                byte ysec = packet.ReadUInt8();
                float xcoord = packet.ReadSingle();
                float zcoord = packet.ReadSingle();
                float ycoord = packet.ReadSingle();

                packet.ReadUInt16(); // Position
                packet.ReadUInt8(); // Move ?? Maybie Useless
                packet.ReadUInt8(); // Run
                packet.ReadUInt8();
                packet.ReadUInt16();
                packet.ReadUInt8();
                packet.ReadUInt8(); //DeathFlag
                packet.ReadUInt8(); //Movement Flag
                packet.ReadUInt8(); //Berserker Flag
                float walkSpeed = packet.ReadSingle();// *1.1f; //Walking Speed
                float runSpeed = packet.ReadSingle();// *1.1f; //Running Speed
                float zerkSpeed = packet.ReadSingle();// *1.1f; //Berserker Speed
                packet.ReadUInt8();
                string characterName = packet.ReadAscii();
                packet.ReadAscii(); // ALIAS

                //set character info
                data.Coordinate = new Coordinate(xsec, ysec, xcoord, ycoord, zcoord);
                data.WalkSpeed = walkSpeed;
                data.RunSpeed = runSpeed;
                data.ZerkSpeed = zerkSpeed;
                data.Name = characterName;
                #endregion

                #region Job

                packet.ReadUInt8(); // Job Level
                packet.ReadUInt8(); // Job Type
                packet.ReadUInt32(); // Trader Exp
                packet.ReadUInt32(); // Thief Exp
                packet.ReadUInt32(); // Hunter Exp
                packet.ReadUInt8(); // Trader LV
                packet.ReadUInt8(); // Thief LV
                packet.ReadUInt8(); // Hunter LV
                packet.ReadUInt8(); // PK Flag
                packet.ReadUInt16(); // Unknown
                packet.ReadUInt32(); // Unknown
                packet.ReadUInt16(); // Unknown
                uint characterID = packet.ReadUInt32(); // Account ID

                data.ID = characterID;
                #endregion
            }
            catch (Exception ex)
            {
                Views.BindingFrom.WriteLine("[Error] " + ex.Message);
            }
            return data;
        }
    }
}
