using SilkroadSecurityApi;
using SroBasic.Component.Logic;
using SroBasic.Controllers;
using SroBasic.Controllers.ThreadProxy;
using SroBasic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SroBasic
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            Views.BindingFrom.InitBinding(this);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //try
            //{
                PrintLog("Load skills data. Total skills: " + Metadata.MediaData.Skills.Count + Environment.NewLine);
                PrintLog("Load items data. Total items: " + Metadata.MediaData.Items.Count + Environment.NewLine);
                PrintLog("Load mobs data. Total mobs: " + Metadata.MediaData.Items.Count + Environment.NewLine);
            //}
            //catch (Exception ex)
            //{
            //    PrintLog(ex.Message);
            //}

                if (Metadata.Config.IncreaseStatPointType == Metadata.IncreaseStatPointType.FullIntellect)
                    rdoIncreaseIntellect.Checked = true;
                else
                    rdoIncreaseStrength.Checked = true;

                chkIsAutoZerk.Checked = Metadata.Config.IsAutoZerk;

                //if (chkClientless.Checked)
                //{
                //    var gatewayRemoteEP = new System.Net.IPEndPoint(Metadata.MediaData.ClientInfo.IP, Metadata.MediaData.ClientInfo.Port);
                //    ProxyClientless.SetGatewayRemoteEndPoint(gatewayRemoteEP);
                //    ProxyClientless.StartGateway();
                //    timerClientPing.Enabled = true;
                //}
                //else
                //{
                //    var agentLocalEP = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 20002);
                //    Proxy.SetAgentLocalEndPoint(agentLocalEP);

                //    var gatewayLocalEP = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 20001);
                //    var gatewayRemoteEP = new System.Net.IPEndPoint(Metadata.MediaData.ClientInfo.IP, Metadata.MediaData.ClientInfo.Port);
                //    Proxy.SetGatewayLocalEndPoint(gatewayLocalEP);
                //    Proxy.SetGatewayRemoteEndPoint(gatewayRemoteEP);
                //    Proxy.StartGateway();
                //}
        }

        public void PrintLog(string log)
        {
            rtfLog.Invoke((MethodInvoker)(() => { rtfLog.AppendText(log); }));
        }


        //[DllImport("kernel32.dll")]
        //static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, int dwProcessId);
        //[DllImport("kernel32.dll")]
        //static extern uint WriteProcessMemory(IntPtr hProcess, uint lpBaseAddress, byte[] lpBuffer, int nSize, uint lpNumberOfBytesWritten);
        //[DllImport("kernel32.dll")]
        //static extern uint VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint flAllocationType, uint flProtect);
        //[DllImport("kernel32.dll")]
        //static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);
        //public static IntPtr Handle;
        //public static IntPtr SROHandle;
        //private Process Started;
        private void btnStart_Click(object sender, EventArgs e)
        {

            if (chkClientless.Checked)
            {
                var gatewayRemoteEP = new System.Net.IPEndPoint(Metadata.MediaData.ClientInfo.IP, Metadata.MediaData.ClientInfo.Port);
                ProxyClientless.SetGatewayRemoteEndPoint(gatewayRemoteEP);
                ProxyClientless.StartGateway();
                timerClientPing.Enabled = true;
            }
            else
            {
                var agentLocalEP = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 20002);
                Proxy.SetAgentLocalEndPoint(agentLocalEP);

                var gatewayLocalEP = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 20001);
                var gatewayRemoteEP = new System.Net.IPEndPoint(Metadata.MediaData.ClientInfo.IP, Metadata.MediaData.ClientInfo.Port);
                Proxy.SetGatewayLocalEndPoint(gatewayLocalEP);
                Proxy.SetGatewayRemoteEndPoint(gatewayRemoteEP);
                Proxy.StartGateway();
            }
            //if (Metadata.Config.SroPath != "" && File.Exists(Metadata.Config.SroPath))
            //{
            //    string path = Metadata.Config.SroPath;
            //    int gatewayLocalPort = 20001;

            //    CreateMutex(IntPtr.Zero, false, "Silkroad Online Launcher");
            //    CreateMutex(IntPtr.Zero, false, "Ready");
            //    uint count = 0;
            //    Process SilkProcess = new Process();
            //    SilkProcess.StartInfo.FileName = path;
            //    SilkProcess.StartInfo.Arguments = "0/22 0 0";
            //    Started = Process.Start(SilkProcess.StartInfo);
            //    Handle = OpenProcess((uint)(0x000F0000L | 0x00100000L | 0xFFF), 0, Started.Id);
            //    uint ConnectionStack = VirtualAllocEx(Handle, IntPtr.Zero, 8, 0x1000, 0x4);
            //    byte[] ConnectionStackArray = BitConverter.GetBytes(ConnectionStack);

            //    if (gatewayLocalPort == 20001)
            //    {
            //        byte[] Connection = {
            //                                0x02,0x00,
            //                                0x4E, 0x21, // PORT (20001)
            //                                0x7F,0x00,0x00,0x01 // IP (127.0.0.1)
            //                            };
            //        uint Codecave = VirtualAllocEx(Handle, IntPtr.Zero, 16, 0x1000, 0x4);
            //        byte[] CodecaveArray = BitConverter.GetBytes(Codecave - 0x004B08A1 - 5);
            //        byte[] CodeCaveFunc = {
            //                                    0xBF,ConnectionStackArray[0],ConnectionStackArray[1],ConnectionStackArray[2],ConnectionStackArray[3],
            //                                    0x8B,0x4E,0x04,
            //                                    0x6A,0x10,
            //                                    0x68,0xA6,0x08,0x4B,0x00,
            //                                    0xC3
            //                              };
            //        byte[] JMPCodeCave = { 0xE9, CodecaveArray[0], CodecaveArray[1], CodecaveArray[2], CodecaveArray[3] };
            //        WriteProcessMemory(Handle, ConnectionStack, Connection, Connection.Length, count);
            //        WriteProcessMemory(Handle, Codecave, CodeCaveFunc, CodeCaveFunc.Length, count);
            //        WriteProcessMemory(Handle, 0x004B08A1, JMPCodeCave, JMPCodeCave.Length, count);
            //    }

            //    Metadata.Config.Save();

            //    btnStart.Enabled = false;
            //}
            //else
            //{
            //    OpenFileDialog dialog = new OpenFileDialog();
            //    dialog.Filter = "sro_client.exe | *.exe";
            //    dialog.ShowDialog();
            //    if (File.Exists(dialog.FileName) && dialog.FileName.ToUpper().Contains("SRO_CLIENT.EXE"))
            //    {
            //        Metadata.Config.SroPath = dialog.FileName;
            //    }
            //}
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chkClientless.Checked)
            {
                timerClientPing.Enabled = false;
                ProxyClientless.StopGateway();
                ProxyClientless.StopAgent();
                //Started.Kill();
            }

            
            Environment.Exit(0);
        }

        #region Binding From
        public void BindingServerCombobox(List<Models.PacketData.Server> servers)
        {
            if(servers != null && servers.Count > 0)
            {
                cboLoginServer.Invoke((MethodInvoker)(() =>
                {
                    cboLoginServer.DataSource = servers;
                    cboLoginServer.ValueMember = "ID";
                    cboLoginServer.DisplayMember = "Name";
                }));

                //if (chkAutoLoginServer.Checked)
                //{
                //    btnLoginServer_Click(null, null);
                //}
            }
        }

        public void BindingCharacterList(List<string> characters)
        {
            if (characters != null && characters.Count > 0)
            {
                
                lstCharacters.Invoke((MethodInvoker)(() =>
                {
                    lstCharacters.Items.Clear();
                    foreach (var item in characters)
                    {
                        lstCharacters.Items.Add(item);
                    }
                }));
            }
        }

        public void BindingCharacterInfo(Views.BindingCharacterType type, Character character)
        {
            #region character info
            if (type == Views.BindingCharacterType.All)
            {
                lblCharacterName.Invoke((MethodInvoker)(() => { lblCharacterName.Text = "Name: " + character.Name; }));
            }
            if ((type & Views.BindingCharacterType.Level) == Views.BindingCharacterType.Level)
            {
                lblCharacterLevel.Invoke((MethodInvoker)(() => { lblCharacterLevel.Text = "Level: " + character.Level; }));
            }
            if ((type & Views.BindingCharacterType.Coordinate) == Views.BindingCharacterType.Coordinate)
            {
                lblCoordinateX.Invoke((MethodInvoker)(() => { lblCoordinateX.Text = "X: " + character.Coordinate.X; }));
                lblCoordinateY.Invoke((MethodInvoker)(() => { lblCoordinateY.Text = "Y: " + character.Coordinate.Y; }));
            }
            //if ((type & Views.BindingCharacterType.StatPoint) == Views.BindingCharacterType.StatPoint)
            //{
            //    lblCharacterStatPoint.Invoke((MethodInvoker)(() => { lblCharacterStatPoint.Text = "Start Points: " + character.StatPoint; }));
            //}
            if ((type & Views.BindingCharacterType.Zerk) == Views.BindingCharacterType.Zerk)
            {
                lblCharacterZerk.Invoke((MethodInvoker)(() => { lblCharacterZerk.Text = "Zerk: " + character.Zerk; }));
            }


            if ((type & Views.BindingCharacterType.HP_MP) == Views.BindingCharacterType.HP_MP)
            {
                prgCharacterHP.Invoke((MethodInvoker)(() =>
                {
                    if (character.MaxHP < character.HP)
                        prgCharacterHP.Maximum = (int)character.HP;
                    else
                        prgCharacterHP.Maximum = (int)character.MaxHP;
                    prgCharacterHP.Value = (int)character.HP;
                }));
                prgCharacterMP.Invoke((MethodInvoker)(() =>
                {
                    if (character.MaxMP < character.MP)
                        prgCharacterMP.Maximum = (int)character.MP;
                    else
                        prgCharacterMP.Maximum = (int)character.MaxMP;
                    prgCharacterMP.Value = (int)character.MP;
                }));
            }

            #endregion

            //character Skill
            if ((type & Views.BindingCharacterType.Skill) == Views.BindingCharacterType.Skill)
            {
                BindingTreeViewCharacterSkill(character.Skills, character.SkillTains);
                BindingTreeViewCharacterSkillTrain(character.SkillTains);
            }
        }
        
        private void InitTreViewCharacterSkill()
        {
            #region TreeView Character Skill
            TreeNode buffNode = new TreeNode();
            buffNode.Name = "buffNode";
            buffNode.Text = "Buff skill group";
            buffNode.Tag = "buffSkill";

            TreeNode attackNode = new TreeNode();
            attackNode.Name = "attackNode";
            attackNode.Text = "Attack skill group";
            attackNode.Tag = "attackSkill";

            TreeNode imbueNode = new TreeNode();
            imbueNode.Name = "imbueNode";
            imbueNode.Text = "Imbue skill";
            imbueNode.Tag = "imbueSkill";

            TreeNode passiveNode = new TreeNode();
            passiveNode.Name = "passiveNode";
            passiveNode.Text = "Passive skill group";
            passiveNode.Tag = "passiveSkill";

            trvCharacterSkills.Invoke((MethodInvoker)(() => {
                trvCharacterSkills.Nodes.Clear();
                trvCharacterSkills.Nodes.Add(imbueNode);
                trvCharacterSkills.Nodes.Add(buffNode);
                trvCharacterSkills.Nodes.Add(attackNode);
                trvCharacterSkills.Nodes.Add(passiveNode);
            }));
            #endregion
        }

        private TreeNode CreateSkillNode(SroBasic.Models.Skill skill)
        {
            TreeNode node = new TreeNode();
            node.Name = skill.ID.ToString();
            node.Text = skill.Name + " - lv. " + skill.Level + " - group: " + skill.UsingType;
            node.Tag = skill.GroupType;

            node.Nodes.Add("id: " + skill.ID.ToString());
            node.Nodes.Add("type: " + skill.GroupType);
            //node.Nodes.Add("mp requerst: " + skill.MPRequest.ToString());
            node.Nodes.Add("cast time: " + skill.CastTime.ToString());
            node.Nodes.Add("cooldow: " +skill.Cooldown.ToString());
            node.Nodes.Add("objReq: " + skill.ObjReq);

            return node;
        }
        private void BindingTreeViewCharacterSkill(List<Skill> listSkill, List<Skilltrain> listSkillTran)
        {
            if (trvCharacterSkills.InvokeRequired)
            {
                this.trvCharacterSkills.Invoke((MethodInvoker)delegate() { BindingTreeViewCharacterSkill(listSkill, listSkillTran); });
            }
            else
            {
                #region Imbue
                TreeNode imbueNode = new TreeNode();
                imbueNode.Name = "imbueNode";
                imbueNode.Text = "Imbue skill";
                imbueNode.Tag = "imbueSkill";

                var imbueList = listSkill.Where(a => a.UsingType == 1 && a.Type.Contains("_GIGONGTA_"))
                            .OrderBy(a => a.GroupType).ToList();
                foreach (var item in imbueList)
                {
                    if (!listSkillTran.Exists(x => x.ID == item.ID))
                    {
                        var node = CreateSkillNode(item);
                        imbueNode.Nodes.Add(node);
                    }
                }
                #endregion
                #region Buff
                TreeNode buffNode = new TreeNode();
                buffNode.Name = "buffNode";
                buffNode.Text = "Buff skill group";
                buffNode.Tag = "buffSkill";

                var buffList = listSkill.Where(a => a.UsingType == 1 && !a.Type.Contains("_GIGONGTA_"))
                            .OrderBy(a => a.GroupType).ToList();
                foreach (var item in buffList)
                {
                    if (!listSkillTran.Exists(x => x.ID == item.ID))
                    {
                        var node = CreateSkillNode(item);
                        buffNode.Nodes.Add(node);
                    }
                }
                #endregion

                #region Attack
                TreeNode attackNode = new TreeNode();
                attackNode.Name = "attackNode";
                attackNode.Text = "Attack skill group";
                attackNode.Tag = "attackSkill";

                var attackList = listSkill.Where(a => a.UsingType == 2)
                            .OrderBy(a => a.GroupType).ToList();
                foreach (var item in attackList)
                {
                    if (!listSkillTran.Exists(x => x.ID == item.ID))
                    {
                        var node = CreateSkillNode(item);
                        attackNode.Nodes.Add(node);
                    }
                }
                #endregion

                #region Passive
                TreeNode passiveNode = new TreeNode();
                passiveNode.Name = "passiveNode";
                passiveNode.Text = "Passive skill group";
                passiveNode.Tag = "passiveSkill";

                var passiveList = listSkill.Where(a => a.UsingType == 0)
                            .OrderBy(a => a.GroupType).ToList();
                foreach (var item in passiveList)
                {
                    if (!listSkillTran.Exists(x => x.ID == item.ID))
                    {
                        var node = CreateSkillNode(item);
                        passiveNode.Nodes.Add(node);
                    }
                }
                #endregion

                for (int i = 0; i < trvCharacterSkills.Nodes.Count; i++)
                {
                    if (trvCharacterSkills.Nodes[i].IsExpanded)
                    {
                        if (trvCharacterSkills.Nodes[i].Name == "imbueNode")
                        {
                            imbueNode.Expand();
                        }
                        if (trvCharacterSkills.Nodes[i].Name == "buffNode")
                        {
                            buffNode.Expand();
                        }
                        if (trvCharacterSkills.Nodes[i].Name == "attackNode")
                        {
                            attackNode.Expand();
                        }
                        if (trvCharacterSkills.Nodes[i].Name == "passiveNode")
                        {
                            passiveNode.Expand();
                        }
                    }
                }

                trvCharacterSkills.Nodes.Clear();
                trvCharacterSkills.Nodes.Add(imbueNode);
                trvCharacterSkills.Nodes.Add(buffNode);
                trvCharacterSkills.Nodes.Add(attackNode);
                trvCharacterSkills.Nodes.Add(passiveNode);
            }

        }
        private void BindingTreeViewCharacterSkillTrain(List<Skilltrain> listSkill)
        {
            if (trvCharacterSkillTrains.InvokeRequired)
            {
                this.trvCharacterSkillTrains.Invoke((MethodInvoker)delegate() { BindingTreeViewCharacterSkillTrain(listSkill); });
            }
            else
            {
                #region Imbue
                TreeNode imbueNode = new TreeNode();
                imbueNode.Name = "imbueNode";
                imbueNode.Text = "Imbue skill";
                imbueNode.Tag = "imbueSkill";

                var imbueList = listSkill.Where(a => a.UsingType == 1 && a.GroupType.Contains("_GIGONGTA_")).ToList();
                foreach (var item in imbueList)
                {
                    TreeNode node = new TreeNode();
                    node.Name = item.ID.ToString();
                    node.Text = item.Name;
                    node.Tag = item.GroupType;

                    imbueNode.Nodes.Add(node);
                }
                #endregion
                #region Buff
                TreeNode buffNode = new TreeNode();
                buffNode.Name = "buffNode";
                buffNode.Text = "Buff skill group";
                buffNode.Tag = "buffSkill";

                var buffList = listSkill.Where(a => a.UsingType == 1 && !a.GroupType.Contains("_GIGONGTA_"))
                            .OrderBy(a => a.GroupType).ToList();
                foreach (var item in buffList)
                {
                    TreeNode node = new TreeNode();
                    node.Name = item.ID.ToString();
                    node.Text = item.Name;
                    node.Tag = item.GroupType;

                    buffNode.Nodes.Add(node);
                }
                #endregion

                #region Attack
                TreeNode attackNode = new TreeNode();
                attackNode.Name = "attackNode";
                attackNode.Text = "Attack skill group";
                attackNode.Tag = "attackSkill";

                var attackList = listSkill.Where(a => a.UsingType == 2)
                            .OrderBy(a => a.GroupType).ToList();
                foreach (var item in attackList)
                {
                    TreeNode node = new TreeNode();
                    node.Name = item.ID.ToString();
                    node.Text = item.Name;
                    node.Tag = item.GroupType;

                    attackNode.Nodes.Add(node);
                }
                #endregion

                //for (int i = 0; i < trvCharacterSkillTrains.Nodes.Count; i++)
                //{
                //    if (trvCharacterSkillTrains.Nodes[i].IsExpanded)
                //    {
                //        if (trvCharacterSkillTrains.Nodes[i].Name == "imbueNode")
                //        {
                //            imbueNode.Expand();
                //        }
                //        if (trvCharacterSkillTrains.Nodes[i].Name == "buffNode")
                //        {
                //            buffNode.Expand();
                //        }
                //        if (trvCharacterSkillTrains.Nodes[i].Name == "attackNode")
                //        {
                //            attackNode.Expand();
                //        }
                //    }
                //}
                imbueNode.Expand();
                buffNode.Expand();
                attackNode.Expand();

                trvCharacterSkillTrains.Nodes.Clear();
                trvCharacterSkillTrains.Nodes.Add(imbueNode);
                trvCharacterSkillTrains.Nodes.Add(buffNode);
                trvCharacterSkillTrains.Nodes.Add(attackNode);
            }

        }

        public void BindingBotDebug(Views.BindingBotDebug type)
        {
            if ((type & Views.BindingBotDebug.Skill) == Views.BindingBotDebug.Skill)
            {
                dgvSkillTrain.DataSource = null;
                dgvSkillTrain.DataSource = Metadata.Globals.Character.SkillTains;
            }
        }


        #endregion

        private void btnLoginServer_Click(object sender, EventArgs e)
        {
            uint serverId = Convert.ToUInt32(cboLoginServer.SelectedValue);
            var p = GeneratePacket.LoginServer(Globals.clientInfo.Locale, Globals.loginUser, Globals.loginPass, serverId);

            if(chkClientless.Checked)
                ProxyClientless.SendPacketToGatewayRemote(p);
            else
                Proxy.SendPacketToGatewayRemote(p);


            grpLogin.Enabled = false;
        }

        private void btnDebugCharacterData_Click(object sender, EventArgs e)
        {
            SilkroadSecurityApi.Packet _0x3013Packet = DebugPacket.LoadPacketFormFile(0x3013);
            SilkroadSecurityApi.Packet _0x3020Packet = DebugPacket.LoadPacketFormFile(0x3020);

            SroBasic.Controllers.ParsePacket._0x3013.DoWork(_0x3013Packet);
            SroBasic.Controllers.ParsePacket._0x3020.DoWork(_0x3020Packet);

            Views.BindingFrom.WriteDebugPacket(_0x3013Packet);
        }

        private void btnSelectSkillTrain_Click(object sender, EventArgs e)
        {
            TreeNode node = trvCharacterSkills.SelectedNode;

            uint skillID = 0;
            if (node != null && uint.TryParse(node.Name, out skillID))
            {
                Metadata.Globals.Character.SelectSkillTrain(skillID);
                Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.Skill);
                Views.BindingFrom.BindingBotDebug(Views.BindingBotDebug.Skill);
            }
        }

        private void btnDeselectSkillTrain_Click(object sender, EventArgs e)
        {
            TreeNode node = trvCharacterSkillTrains.SelectedNode;

            uint skillID = 0;
            if (node != null && uint.TryParse(node.Name, out skillID))
            {
                Metadata.Globals.Character.DeselectSkillTrain(skillID);
                Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.Skill);
                Views.BindingFrom.BindingBotDebug(Views.BindingBotDebug.Skill);
            }
        }

        private void timerClientPing_Tick(object sender, EventArgs e)
        {
            Packet p = new Packet(0x750E);
            SroBasic.Controllers.ThreadProxy.ProxyClientless.SendPacketToGatewayRemote(p);            
        }

        private void btnStartTrain_Click(object sender, EventArgs e)
        {
            if (btnStartTrain.Text == "Start Train")
            {
                btnStartTrain.Text = "Stop Train";
                Controllers.Bot.BotInput.Start();
            }
            else
            {
                btnStartTrain.Text = "Start Train";
                Controllers.Bot.BotInput.Stop();
            }
        }

        private void btnSelectCharacter_Click(object sender, EventArgs e)
        {
            string characterName = lstCharacters.SelectedItem.ToString();
            Packet p = new Packet(0x7001);//CLIENT_SELECT_CHARACTER = 0x7001
            p.WriteAscii(characterName);

            if(chkClientless.Checked)
                ProxyClientless.SendPacketToAgentRemote(p);
            else
                Proxy.SendPacketToAgentRemote(p);


            grpCharacterList.Enabled = false;
        }

        private void btnSaveSkillTrain_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@"data"))
            {
                Directory.CreateDirectory("data");
            }

            string path = @"\data\skill_train.txt";
            using (TextWriter write = File.CreateText(path))
            {
                if (Metadata.Globals.Character.SkillTains.Count > 0)
                {
                    foreach (var item in Metadata.Globals.Character.SkillTains)
                    {
                        write.WriteLine(item.ID);
                    }
                }
            }
        }

        private void btnLoadSkillTrain_Click(object sender, EventArgs e)
        {
            string path = @"\data\skill_train.txt";
            if (!File.Exists(path))
            {
                return;
            }

            using (TextReader reader = File.OpenText(path))
            { 
                string input = "";
                while ((input = reader.ReadLine()) != null)
                {
                    uint skillId = 0;
                    uint.TryParse(input, out skillId);
                    Metadata.Globals.Character.SelectSkillTrain(skillId);
                }
            }
           
            Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.Skill);
            Views.BindingFrom.BindingBotDebug(Views.BindingBotDebug.Skill);
        }

        private void btnStartZerk_Click(object sender, EventArgs e)
        {
            Packet packet = GeneratePacket.Berserk();
            SroBasic.Controllers.ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
        }

        private void rdoIncreaseStrength_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoIncreaseStrength.Checked)
                Metadata.Config.IncreaseStatPointType = Metadata.IncreaseStatPointType.FullStrength;
        }

        private void rdoIncreaseIntellect_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoIncreaseIntellect.Checked)
                Metadata.Config.IncreaseStatPointType = Metadata.IncreaseStatPointType.FullIntellect;
        }

        private void chkIsAutoZerk_CheckedChanged(object sender, EventArgs e)
        {
            Metadata.Config.IsAutoZerk = chkIsAutoZerk.Checked;
        }

        private void rdoIncreaseNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoIncreaseIntellect.Checked)
                Metadata.Config.IncreaseStatPointType = Metadata.IncreaseStatPointType.None;
        }

        private void btnTestAttack_Click(object sender, EventArgs e)
        {   Packet packet = new Packet(0x00);
            if(!string.IsNullOrEmpty(txtMobId.Text) &&!string.IsNullOrEmpty(txtSkillId.Text))
            {
                packet = GeneratePacket.AttackSkill(Convert.ToUInt32(txtSkillId.Text), Convert.ToUInt32(txtMobId.Text));
            }
            else if (string.IsNullOrEmpty(txtMobId.Text) && !string.IsNullOrEmpty(txtSkillId.Text))
            {
                packet = GeneratePacket.BuffSkill(Convert.ToUInt32(txtSkillId.Text));
            }
            else if (!string.IsNullOrEmpty(txtMobId.Text) && string.IsNullOrEmpty(txtSkillId.Text))
            {
                packet = GeneratePacket.AttackNormal(Convert.ToUInt32(txtMobId.Text));
            }

            SroBasic.Controllers.ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
        }


    }
}
