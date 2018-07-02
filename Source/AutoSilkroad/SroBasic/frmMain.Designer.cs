namespace SroBasic
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rtfLog = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grpCharacterList = new System.Windows.Forms.GroupBox();
            this.chkAutoSelectCharacter = new System.Windows.Forms.CheckBox();
            this.btnSelectCharacter = new System.Windows.Forms.Button();
            this.lstCharacters = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAutoSubmitCaptcha = new System.Windows.Forms.CheckBox();
            this.btnSubmitCaptcha = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCaptcha = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.chkAutoLoginServer = new System.Windows.Forms.CheckBox();
            this.btnLoginServer = new System.Windows.Forms.Button();
            this.cboLoginServer = new System.Windows.Forms.ComboBox();
            this.txtLoginPass = new System.Windows.Forms.TextBox();
            this.txtLoginUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnLoadSkillTrain = new System.Windows.Forms.Button();
            this.btnSaveSkillTrain = new System.Windows.Forms.Button();
            this.btnDeselectSkillTrain = new System.Windows.Forms.Button();
            this.btnSelectSkillTrain = new System.Windows.Forms.Button();
            this.trvCharacterSkillTrains = new System.Windows.Forms.TreeView();
            this.trvCharacterSkills = new System.Windows.Forms.TreeView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvMob = new System.Windows.Forms.DataGridView();
            this.dgvSkillTrain = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkIsAutoZerk = new System.Windows.Forms.CheckBox();
            this.btnStartZerk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoIncreaseNone = new System.Windows.Forms.RadioButton();
            this.rdoIncreaseIntellect = new System.Windows.Forms.RadioButton();
            this.rdoIncreaseStrength = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.prgCharacterMP = new System.Windows.Forms.ProgressBar();
            this.prgCharacterHP = new System.Windows.Forms.ProgressBar();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblCharacterLevel = new System.Windows.Forms.Label();
            this.lblCharacterName = new System.Windows.Forms.Label();
            this.lblCharacterZerk = new System.Windows.Forms.Label();
            this.lblCoordinateY = new System.Windows.Forms.Label();
            this.lblCoordinateX = new System.Windows.Forms.Label();
            this.btnStartTrain = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDebugCharacterData = new System.Windows.Forms.Button();
            this.chkClientless = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.timerClientPing = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grpCharacterList.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpLogin.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkillTrain)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 134);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(736, 346);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtfLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(728, 320);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rtfLog
            // 
            this.rtfLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtfLog.Location = new System.Drawing.Point(3, 3);
            this.rtfLog.Name = "rtfLog";
            this.rtfLog.Size = new System.Drawing.Size(722, 314);
            this.rtfLog.TabIndex = 0;
            this.rtfLog.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grpCharacterList);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.grpLogin);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(742, 320);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Login";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grpCharacterList
            // 
            this.grpCharacterList.Controls.Add(this.chkAutoSelectCharacter);
            this.grpCharacterList.Controls.Add(this.btnSelectCharacter);
            this.grpCharacterList.Controls.Add(this.lstCharacters);
            this.grpCharacterList.Location = new System.Drawing.Point(453, 6);
            this.grpCharacterList.Name = "grpCharacterList";
            this.grpCharacterList.Size = new System.Drawing.Size(170, 147);
            this.grpCharacterList.TabIndex = 2;
            this.grpCharacterList.TabStop = false;
            this.grpCharacterList.Text = "3. Character";
            // 
            // chkAutoSelectCharacter
            // 
            this.chkAutoSelectCharacter.AutoSize = true;
            this.chkAutoSelectCharacter.Checked = true;
            this.chkAutoSelectCharacter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoSelectCharacter.Location = new System.Drawing.Point(6, 124);
            this.chkAutoSelectCharacter.Name = "chkAutoSelectCharacter";
            this.chkAutoSelectCharacter.Size = new System.Drawing.Size(81, 17);
            this.chkAutoSelectCharacter.TabIndex = 9;
            this.chkAutoSelectCharacter.Text = "Auto Select";
            this.chkAutoSelectCharacter.UseVisualStyleBackColor = true;
            // 
            // btnSelectCharacter
            // 
            this.btnSelectCharacter.Location = new System.Drawing.Point(89, 118);
            this.btnSelectCharacter.Name = "btnSelectCharacter";
            this.btnSelectCharacter.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCharacter.TabIndex = 8;
            this.btnSelectCharacter.Text = "Select";
            this.btnSelectCharacter.UseVisualStyleBackColor = true;
            this.btnSelectCharacter.Click += new System.EventHandler(this.btnSelectCharacter_Click);
            // 
            // lstCharacters
            // 
            this.lstCharacters.FormattingEnabled = true;
            this.lstCharacters.Location = new System.Drawing.Point(6, 25);
            this.lstCharacters.Name = "lstCharacters";
            this.lstCharacters.Size = new System.Drawing.Size(158, 82);
            this.lstCharacters.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkAutoSubmitCaptcha);
            this.groupBox2.Controls.Add(this.btnSubmitCaptcha);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCaptcha);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(232, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 158);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2. Captcha";
            // 
            // chkAutoSubmitCaptcha
            // 
            this.chkAutoSubmitCaptcha.AutoSize = true;
            this.chkAutoSubmitCaptcha.Checked = true;
            this.chkAutoSubmitCaptcha.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoSubmitCaptcha.Location = new System.Drawing.Point(12, 128);
            this.chkAutoSubmitCaptcha.Name = "chkAutoSubmitCaptcha";
            this.chkAutoSubmitCaptcha.Size = new System.Drawing.Size(83, 17);
            this.chkAutoSubmitCaptcha.TabIndex = 8;
            this.chkAutoSubmitCaptcha.Text = "Auto Submit";
            this.chkAutoSubmitCaptcha.UseVisualStyleBackColor = true;
            // 
            // btnSubmitCaptcha
            // 
            this.btnSubmitCaptcha.Location = new System.Drawing.Point(122, 124);
            this.btnSubmitCaptcha.Name = "btnSubmitCaptcha";
            this.btnSubmitCaptcha.Size = new System.Drawing.Size(75, 23);
            this.btnSubmitCaptcha.TabIndex = 7;
            this.btnSubmitCaptcha.Text = "Submit";
            this.btnSubmitCaptcha.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Captcha:";
            // 
            // txtCaptcha
            // 
            this.txtCaptcha.Location = new System.Drawing.Point(65, 91);
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.Size = new System.Drawing.Size(141, 20);
            this.txtCaptcha.TabIndex = 1;
            this.txtCaptcha.Text = "1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 64);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.chkAutoLoginServer);
            this.grpLogin.Controls.Add(this.btnLoginServer);
            this.grpLogin.Controls.Add(this.cboLoginServer);
            this.grpLogin.Controls.Add(this.txtLoginPass);
            this.grpLogin.Controls.Add(this.txtLoginUser);
            this.grpLogin.Controls.Add(this.label3);
            this.grpLogin.Controls.Add(this.label2);
            this.grpLogin.Controls.Add(this.label1);
            this.grpLogin.Location = new System.Drawing.Point(8, 6);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(208, 147);
            this.grpLogin.TabIndex = 0;
            this.grpLogin.TabStop = false;
            this.grpLogin.Text = "1. Login";
            // 
            // chkAutoLoginServer
            // 
            this.chkAutoLoginServer.AutoSize = true;
            this.chkAutoLoginServer.Checked = true;
            this.chkAutoLoginServer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoLoginServer.Location = new System.Drawing.Point(6, 117);
            this.chkAutoLoginServer.Name = "chkAutoLoginServer";
            this.chkAutoLoginServer.Size = new System.Drawing.Size(77, 17);
            this.chkAutoLoginServer.TabIndex = 7;
            this.chkAutoLoginServer.Text = "Auto Login";
            this.chkAutoLoginServer.UseVisualStyleBackColor = true;
            // 
            // btnLoginServer
            // 
            this.btnLoginServer.Location = new System.Drawing.Point(91, 117);
            this.btnLoginServer.Name = "btnLoginServer";
            this.btnLoginServer.Size = new System.Drawing.Size(75, 23);
            this.btnLoginServer.TabIndex = 6;
            this.btnLoginServer.Text = "Login";
            this.btnLoginServer.UseVisualStyleBackColor = true;
            this.btnLoginServer.Click += new System.EventHandler(this.btnLoginServer_Click);
            // 
            // cboLoginServer
            // 
            this.cboLoginServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoginServer.FormattingEnabled = true;
            this.cboLoginServer.Location = new System.Drawing.Point(54, 73);
            this.cboLoginServer.Name = "cboLoginServer";
            this.cboLoginServer.Size = new System.Drawing.Size(112, 21);
            this.cboLoginServer.TabIndex = 5;
            // 
            // txtLoginPass
            // 
            this.txtLoginPass.Location = new System.Drawing.Point(54, 47);
            this.txtLoginPass.Name = "txtLoginPass";
            this.txtLoginPass.PasswordChar = '*';
            this.txtLoginPass.Size = new System.Drawing.Size(112, 20);
            this.txtLoginPass.TabIndex = 4;
            this.txtLoginPass.Text = "12021989";
            // 
            // txtLoginUser
            // 
            this.txtLoginUser.Location = new System.Drawing.Point(54, 21);
            this.txtLoginUser.Name = "txtLoginUser";
            this.txtLoginUser.Size = new System.Drawing.Size(112, 20);
            this.txtLoginUser.TabIndex = 3;
            this.txtLoginUser.Text = "hnguyenaa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pass:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnLoadSkillTrain);
            this.tabPage3.Controls.Add(this.btnSaveSkillTrain);
            this.tabPage3.Controls.Add(this.btnDeselectSkillTrain);
            this.tabPage3.Controls.Add(this.btnSelectSkillTrain);
            this.tabPage3.Controls.Add(this.trvCharacterSkillTrains);
            this.tabPage3.Controls.Add(this.trvCharacterSkills);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(742, 320);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Skill";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnLoadSkillTrain
            // 
            this.btnLoadSkillTrain.Location = new System.Drawing.Point(300, 164);
            this.btnLoadSkillTrain.Name = "btnLoadSkillTrain";
            this.btnLoadSkillTrain.Size = new System.Drawing.Size(33, 23);
            this.btnLoadSkillTrain.TabIndex = 5;
            this.btnLoadSkillTrain.Text = "Load Skill Train";
            this.btnLoadSkillTrain.UseVisualStyleBackColor = true;
            this.btnLoadSkillTrain.Click += new System.EventHandler(this.btnLoadSkillTrain_Click);
            // 
            // btnSaveSkillTrain
            // 
            this.btnSaveSkillTrain.Location = new System.Drawing.Point(299, 135);
            this.btnSaveSkillTrain.Name = "btnSaveSkillTrain";
            this.btnSaveSkillTrain.Size = new System.Drawing.Size(33, 23);
            this.btnSaveSkillTrain.TabIndex = 4;
            this.btnSaveSkillTrain.Text = "Save Skill Train";
            this.btnSaveSkillTrain.UseVisualStyleBackColor = true;
            this.btnSaveSkillTrain.Click += new System.EventHandler(this.btnSaveSkillTrain_Click);
            // 
            // btnDeselectSkillTrain
            // 
            this.btnDeselectSkillTrain.Location = new System.Drawing.Point(300, 99);
            this.btnDeselectSkillTrain.Name = "btnDeselectSkillTrain";
            this.btnDeselectSkillTrain.Size = new System.Drawing.Size(33, 23);
            this.btnDeselectSkillTrain.TabIndex = 3;
            this.btnDeselectSkillTrain.Text = "<<";
            this.btnDeselectSkillTrain.UseVisualStyleBackColor = true;
            this.btnDeselectSkillTrain.Click += new System.EventHandler(this.btnDeselectSkillTrain_Click);
            // 
            // btnSelectSkillTrain
            // 
            this.btnSelectSkillTrain.Location = new System.Drawing.Point(300, 70);
            this.btnSelectSkillTrain.Name = "btnSelectSkillTrain";
            this.btnSelectSkillTrain.Size = new System.Drawing.Size(33, 23);
            this.btnSelectSkillTrain.TabIndex = 2;
            this.btnSelectSkillTrain.Text = ">>";
            this.btnSelectSkillTrain.UseVisualStyleBackColor = true;
            this.btnSelectSkillTrain.Click += new System.EventHandler(this.btnSelectSkillTrain_Click);
            // 
            // trvCharacterSkillTrains
            // 
            this.trvCharacterSkillTrains.Location = new System.Drawing.Point(339, 6);
            this.trvCharacterSkillTrains.Name = "trvCharacterSkillTrains";
            this.trvCharacterSkillTrains.Size = new System.Drawing.Size(286, 278);
            this.trvCharacterSkillTrains.TabIndex = 1;
            // 
            // trvCharacterSkills
            // 
            this.trvCharacterSkills.Location = new System.Drawing.Point(8, 6);
            this.trvCharacterSkills.Name = "trvCharacterSkills";
            this.trvCharacterSkills.Size = new System.Drawing.Size(286, 278);
            this.trvCharacterSkills.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvMob);
            this.tabPage4.Controls.Add(this.dgvSkillTrain);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(728, 320);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "bot Debug";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvMob
            // 
            this.dgvMob.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMob.Location = new System.Drawing.Point(284, 24);
            this.dgvMob.Name = "dgvMob";
            this.dgvMob.Size = new System.Drawing.Size(260, 262);
            this.dgvMob.TabIndex = 1;
            // 
            // dgvSkillTrain
            // 
            this.dgvSkillTrain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSkillTrain.Location = new System.Drawing.Point(6, 24);
            this.dgvSkillTrain.Name = "dgvSkillTrain";
            this.dgvSkillTrain.Size = new System.Drawing.Size(260, 262);
            this.dgvSkillTrain.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox4);
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(728, 320);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Bot Config";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkIsAutoZerk);
            this.groupBox4.Controls.Add(this.btnStartZerk);
            this.groupBox4.Location = new System.Drawing.Point(8, 57);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(303, 46);
            this.groupBox4.TabIndex = 61;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Zerk Action";
            // 
            // chkIsAutoZerk
            // 
            this.chkIsAutoZerk.AutoSize = true;
            this.chkIsAutoZerk.Location = new System.Drawing.Point(6, 23);
            this.chkIsAutoZerk.Name = "chkIsAutoZerk";
            this.chkIsAutoZerk.Size = new System.Drawing.Size(73, 17);
            this.chkIsAutoZerk.TabIndex = 62;
            this.chkIsAutoZerk.Text = "Auto Zerk";
            this.chkIsAutoZerk.UseVisualStyleBackColor = true;
            this.chkIsAutoZerk.CheckedChanged += new System.EventHandler(this.chkIsAutoZerk_CheckedChanged);
            // 
            // btnStartZerk
            // 
            this.btnStartZerk.Location = new System.Drawing.Point(100, 17);
            this.btnStartZerk.Name = "btnStartZerk";
            this.btnStartZerk.Size = new System.Drawing.Size(86, 23);
            this.btnStartZerk.TabIndex = 59;
            this.btnStartZerk.Text = "Start Zerk";
            this.btnStartZerk.UseVisualStyleBackColor = true;
            this.btnStartZerk.Click += new System.EventHandler(this.btnStartZerk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoIncreaseNone);
            this.groupBox1.Controls.Add(this.rdoIncreaseIntellect);
            this.groupBox1.Controls.Add(this.rdoIncreaseStrength);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 45);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto Increase Stat Point";
            // 
            // rdoIncreaseNone
            // 
            this.rdoIncreaseNone.AutoSize = true;
            this.rdoIncreaseNone.Location = new System.Drawing.Point(183, 19);
            this.rdoIncreaseNone.Name = "rdoIncreaseNone";
            this.rdoIncreaseNone.Size = new System.Drawing.Size(51, 17);
            this.rdoIncreaseNone.TabIndex = 2;
            this.rdoIncreaseNone.TabStop = true;
            this.rdoIncreaseNone.Text = "None";
            this.rdoIncreaseNone.UseVisualStyleBackColor = true;
            this.rdoIncreaseNone.CheckedChanged += new System.EventHandler(this.rdoIncreaseNone_CheckedChanged);
            // 
            // rdoIncreaseIntellect
            // 
            this.rdoIncreaseIntellect.AutoSize = true;
            this.rdoIncreaseIntellect.Location = new System.Drawing.Point(96, 19);
            this.rdoIncreaseIntellect.Name = "rdoIncreaseIntellect";
            this.rdoIncreaseIntellect.Size = new System.Drawing.Size(81, 17);
            this.rdoIncreaseIntellect.TabIndex = 1;
            this.rdoIncreaseIntellect.TabStop = true;
            this.rdoIncreaseIntellect.Text = "Full Intellect";
            this.rdoIncreaseIntellect.UseVisualStyleBackColor = true;
            this.rdoIncreaseIntellect.CheckedChanged += new System.EventHandler(this.rdoIncreaseIntellect_CheckedChanged);
            // 
            // rdoIncreaseStrength
            // 
            this.rdoIncreaseStrength.AutoSize = true;
            this.rdoIncreaseStrength.Location = new System.Drawing.Point(6, 19);
            this.rdoIncreaseStrength.Name = "rdoIncreaseStrength";
            this.rdoIncreaseStrength.Size = new System.Drawing.Size(84, 17);
            this.rdoIncreaseStrength.TabIndex = 0;
            this.rdoIncreaseStrength.TabStop = true;
            this.rdoIncreaseStrength.Text = "Full Strength";
            this.rdoIncreaseStrength.UseVisualStyleBackColor = true;
            this.rdoIncreaseStrength.CheckedChanged += new System.EventHandler(this.rdoIncreaseStrength_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.prgCharacterMP);
            this.panel1.Controls.Add(this.prgCharacterHP);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.lblCharacterLevel);
            this.panel1.Controls.Add(this.lblCharacterName);
            this.panel1.Controls.Add(this.lblCharacterZerk);
            this.panel1.Controls.Add(this.lblCoordinateY);
            this.panel1.Controls.Add(this.lblCoordinateX);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 480);
            this.panel1.TabIndex = 3;
            // 
            // prgCharacterMP
            // 
            this.prgCharacterMP.BackColor = System.Drawing.SystemColors.Control;
            this.prgCharacterMP.ForeColor = System.Drawing.Color.DodgerBlue;
            this.prgCharacterMP.Location = new System.Drawing.Point(36, 32);
            this.prgCharacterMP.Name = "prgCharacterMP";
            this.prgCharacterMP.Size = new System.Drawing.Size(108, 12);
            this.prgCharacterMP.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgCharacterMP.TabIndex = 54;
            this.prgCharacterMP.Value = 100;
            // 
            // prgCharacterHP
            // 
            this.prgCharacterHP.BackColor = System.Drawing.SystemColors.Control;
            this.prgCharacterHP.ForeColor = System.Drawing.Color.Red;
            this.prgCharacterHP.Location = new System.Drawing.Point(36, 11);
            this.prgCharacterHP.Name = "prgCharacterHP";
            this.prgCharacterHP.Size = new System.Drawing.Size(108, 12);
            this.prgCharacterHP.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgCharacterHP.TabIndex = 51;
            this.prgCharacterHP.Value = 100;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.Location = new System.Drawing.Point(4, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(26, 13);
            this.label20.TabIndex = 53;
            this.label20.Text = "MP:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label19.Location = new System.Drawing.Point(4, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(25, 13);
            this.label19.TabIndex = 52;
            this.label19.Text = "HP:";
            // 
            // lblCharacterLevel
            // 
            this.lblCharacterLevel.AutoSize = true;
            this.lblCharacterLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharacterLevel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCharacterLevel.Location = new System.Drawing.Point(4, 73);
            this.lblCharacterLevel.Name = "lblCharacterLevel";
            this.lblCharacterLevel.Size = new System.Drawing.Size(60, 13);
            this.lblCharacterLevel.TabIndex = 50;
            this.lblCharacterLevel.Text = "Level:  125";
            // 
            // lblCharacterName
            // 
            this.lblCharacterName.AutoSize = true;
            this.lblCharacterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharacterName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCharacterName.Location = new System.Drawing.Point(4, 52);
            this.lblCharacterName.Name = "lblCharacterName";
            this.lblCharacterName.Size = new System.Drawing.Size(140, 13);
            this.lblCharacterName.TabIndex = 49;
            this.lblCharacterName.Text = "Name:  <mot cai ten rat dai>";
            // 
            // lblCharacterZerk
            // 
            this.lblCharacterZerk.AutoSize = true;
            this.lblCharacterZerk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharacterZerk.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCharacterZerk.Location = new System.Drawing.Point(82, 73);
            this.lblCharacterZerk.Name = "lblCharacterZerk";
            this.lblCharacterZerk.Size = new System.Drawing.Size(44, 13);
            this.lblCharacterZerk.TabIndex = 46;
            this.lblCharacterZerk.Text = "Zerk:  5";
            // 
            // lblCoordinateY
            // 
            this.lblCoordinateY.AutoSize = true;
            this.lblCoordinateY.BackColor = System.Drawing.SystemColors.Control;
            this.lblCoordinateY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoordinateY.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCoordinateY.Location = new System.Drawing.Point(82, 94);
            this.lblCoordinateY.Name = "lblCoordinateY";
            this.lblCoordinateY.Size = new System.Drawing.Size(50, 13);
            this.lblCoordinateY.TabIndex = 48;
            this.lblCoordinateY.Text = "Y: 47896";
            // 
            // lblCoordinateX
            // 
            this.lblCoordinateX.AutoSize = true;
            this.lblCoordinateX.BackColor = System.Drawing.SystemColors.Control;
            this.lblCoordinateX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoordinateX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCoordinateX.Location = new System.Drawing.Point(4, 94);
            this.lblCoordinateX.Name = "lblCoordinateX";
            this.lblCoordinateX.Size = new System.Drawing.Size(50, 13);
            this.lblCoordinateX.TabIndex = 47;
            this.lblCoordinateX.Text = "X: 36753";
            // 
            // btnStartTrain
            // 
            this.btnStartTrain.Location = new System.Drawing.Point(126, 18);
            this.btnStartTrain.Name = "btnStartTrain";
            this.btnStartTrain.Size = new System.Drawing.Size(86, 23);
            this.btnStartTrain.TabIndex = 58;
            this.btnStartTrain.Text = "Start Train";
            this.btnStartTrain.UseVisualStyleBackColor = true;
            this.btnStartTrain.Click += new System.EventHandler(this.btnStartTrain_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(234, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 23);
            this.button1.TabIndex = 57;
            this.button1.Text = "Debug Character Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDebugCharacterData
            // 
            this.btnDebugCharacterData.Location = new System.Drawing.Point(401, 5);
            this.btnDebugCharacterData.Name = "btnDebugCharacterData";
            this.btnDebugCharacterData.Size = new System.Drawing.Size(131, 23);
            this.btnDebugCharacterData.TabIndex = 2;
            this.btnDebugCharacterData.Text = "Debug Character Data";
            this.btnDebugCharacterData.UseVisualStyleBackColor = true;
            this.btnDebugCharacterData.Click += new System.EventHandler(this.btnDebugCharacterData_Click);
            // 
            // chkClientless
            // 
            this.chkClientless.AutoSize = true;
            this.chkClientless.Location = new System.Drawing.Point(17, 52);
            this.chkClientless.Name = "chkClientless";
            this.chkClientless.Size = new System.Drawing.Size(70, 17);
            this.chkClientless.TabIndex = 1;
            this.chkClientless.Text = "Clientless";
            this.chkClientless.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(19, 18);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timerClientPing
            // 
            this.timerClientPing.Interval = 5000;
            this.timerClientPing.Tick += new System.EventHandler(this.timerClientPing_Tick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(170, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(736, 480);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnStart);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.chkClientless);
            this.panel3.Controls.Add(this.btnDebugCharacterData);
            this.panel3.Controls.Add(this.btnStartTrain);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(736, 134);
            this.panel3.TabIndex = 59;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 480);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "Auto Sro v1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.grpCharacterList.ResumeLayout(false);
            this.grpCharacterList.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkillTrain)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox rtfLog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkClientless;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox grpCharacterList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.ComboBox cboLoginServer;
        private System.Windows.Forms.TextBox txtLoginPass;
        private System.Windows.Forms.TextBox txtLoginUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCaptcha;
        private System.Windows.Forms.Button btnSubmitCaptcha;
        private System.Windows.Forms.Button btnLoginServer;
        private System.Windows.Forms.Button btnSelectCharacter;
        private System.Windows.Forms.ListBox lstCharacters;
        private System.Windows.Forms.CheckBox chkAutoLoginServer;
        private System.Windows.Forms.CheckBox chkAutoSubmitCaptcha;
        private System.Windows.Forms.CheckBox chkAutoSelectCharacter;
        private System.Windows.Forms.Button btnDebugCharacterData;
        private System.Windows.Forms.ProgressBar prgCharacterMP;
        private System.Windows.Forms.ProgressBar prgCharacterHP;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblCharacterLevel;
        private System.Windows.Forms.Label lblCharacterName;
        private System.Windows.Forms.Label lblCharacterZerk;
        private System.Windows.Forms.Label lblCoordinateY;
        private System.Windows.Forms.Label lblCoordinateX;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView trvCharacterSkills;
        private System.Windows.Forms.Button btnDeselectSkillTrain;
        private System.Windows.Forms.Button btnSelectSkillTrain;
        private System.Windows.Forms.TreeView trvCharacterSkillTrains;
        private System.Windows.Forms.Timer timerClientPing;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvSkillTrain;
        private System.Windows.Forms.DataGridView dgvMob;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnStartTrain;
        private System.Windows.Forms.Button btnSaveSkillTrain;
        private System.Windows.Forms.Button btnLoadSkillTrain;
        private System.Windows.Forms.Button btnStartZerk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoIncreaseIntellect;
        private System.Windows.Forms.RadioButton rdoIncreaseStrength;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkIsAutoZerk;
        private System.Windows.Forms.RadioButton rdoIncreaseNone;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}

