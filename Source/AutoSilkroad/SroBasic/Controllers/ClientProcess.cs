using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SroBasic.Controllers
{
    public static class Kernel32
    {
        /// <summary>
        /// This function creates a named or unnamed mutex object.
        /// <para>Full document in : https://msdn.microsoft.com/en-us/library/ms919034.aspx </para>
        /// </summary>
        /// <remarks>
        /// <code>
        /// HANDLE CreateMutex( 
        /// LPSECURITY_ATTRIBUTES lpMutexAttributes, 
        /// BOOL bInitialOwner, 
        /// LPCTSTR lpName 
        /// );
        /// </code>
        /// </remarks>
        /// <param name="lpMutexAttributes">Ignored. Must be NULL.</param>
        /// <param name="bInitialOwner">Boolean that specifies the initial owner of the mutex object. <para>If this value is TRUE and the caller created the mutex, the calling thread obtains ownership of the mutex object.</para> <para>Otherwise, the calling thread does not obtain ownership of the mutex. To determine if the caller created the mutex, see the Return Values section.</para> </param>
        /// <param name="lpName">Long pointer to a null-terminated string specifying the name of the mutex object. <para>The name is limited to MAX_PATH characters and can contain any character except the backslash path-separator character (\). Name comparison is case sensitive.</para> </param>
        /// <returns>A handle to the mutex object indicates success. <para>If the named mutex object existed before the function call, the function returns a handle to the existing object and GetLastError returns ERROR_ALREADY_EXISTS.</para> Otherwise, the caller created the mutex. NULL indicates failure. To get extended error information, call GetLastError.</returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

        /// <summary>
        /// Reads data from an area of memory in a specified process. The entire area to be read must be accessible or the operation fails.
        /// <para>Full document in : https://msdn.microsoft.com/en-us/library/windows/desktop/ms680553(v=vs.85).aspx </para>
        /// </summary>
        /// <remarks>
        /// <code>
        /// BOOL WINAPI ReadProcessMemory(
        /// _In_  HANDLE  hProcess,
        /// _In_  LPCVOID lpBaseAddress,
        /// _Out_ LPVOID  lpBuffer,
        /// _In_  SIZE_T  nSize,
        /// _Out_ SIZE_T  *lpNumberOfBytesRead
        /// );
        /// </code>
        /// </remarks>
        /// <param name="hProcess">A handle to the process with memory that is being read. The handle must have PROCESS_VM_READ access to the process.</param>
        /// <param name="lpBaseAddress">A pointer to the base address in the specified process from which to read. Before any data transfer occurs, the system verifies that all data in the base address and memory of the specified size is accessible for read access, and if it is not accessible the function fails.</param>
        /// <param name="lpBuffer">A pointer to a buffer that receives the contents from the address space of the specified process.</param>
        /// <param name="nSize">The number of bytes to be read from the specified process.</param>
        /// <param name="lpNumberOfBytesWritten">A pointer to a variable that receives the number of bytes transferred into the specified buffer. If lpNumberOfBytesRead is NULL, the parameter is ignored.</param>
        /// <returns><para>If the function succeeds, the return value is nonzero.</para><para>If the function fails, the return value is 0 (zero). To get extended error information, call GetLastError.</para><para>The function fails if the requested read operation crosses into an area of the process that is inaccessible.</para></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, uint lpBaseAddress, byte[] lpBuffer, int nSize, uint lpNumberOfBytesWritten);
        //public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, int dwProcessId);
        
        [DllImport("kernel32.dll")]
        public static extern uint VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint flAllocationType, uint flProtect);
        
        [DllImport("kernel32")]
        public static extern uint GetProcAddress(IntPtr hModule, string procName);
        
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
    public static class ClientProcess
    {
        public static Process _Process { get; set; }
        private static PatchConfig _PatchConfig { get; set; }
        private static ClientConfig _ClientConfig { get; set; }

        #region Addresses
        static uint BaseAddress = 0x400000;

        static uint AlreadyProgramExe = 0;
        static uint RedirectIPAddress = 0;
        static uint MultiClientAddress = 0;
        static uint CallForwardAddress = 0;
        static uint MultiClientError = 0;
        static uint NoGameGuard = 0;
        static uint ServerStatusFULL = 0;
        static uint NudePatch = 0;
        static uint SwearFilter1 = 0;
        static uint SwearFilter2 = 0;
        static uint SwearFilter3 = 0;
        static uint SwearFilter4 = 0;
        static uint EnglishPatch = 0;
        static uint RussianHack = 0;
        static uint Zoomhack = 0;
        static uint SeedPatchAdress = 0;
        static uint TextDataName = 0;
        static uint StartingMSG = 0;
        static uint ChangeVersion = 0;

        #endregion

        #region Patches
        static string StartingMessageText = "This Client is managed by [NH]\nHave a nice botting time!";
        static byte[] HexColorArray = { 0x00, 0xDD, 0x00, 0x00 };

        static byte[] JMP = { 0xEB };
        static byte[] RETN = { 0xC3 };
        static byte[] NOPNOP = { 0x90, 0x90 };
        static byte[] LanguageTab = { 0xBF, 0x08, 0x00, 0x00, 0x00, 0x90 };
        static byte[] SeedPatch = { 0xB9, 0x33, 0x00, 0x00, 0x00, 0x90, 0x90, 0x90, 0x90, 0x90 };

        static byte PUSH = 0x68;
        static byte MOVESI = 0xBE;
        #endregion

        #region BytePattern

        static byte[] RedirectIPAddressPattern = { 0x89, 0x86, 0x2C, 0x01, 0x00, 0x00, 0x8B, 0x17, 0x89, 0x56, 0x50, 0x8B, 0x47, 0x04, 0x89, 0x46, 0x54, 0x8B, 0x4F, 0x08, 0x89, 0x4E, 0x58, 0x8B, 0x57, 0x0C, 0x89, 0x56, 0x5C, 0x5E, 0xB8, 0x01, 0x00, 0x00, 0x00, 0x5D, 0xC3 };
        static byte[] SeedPatchPattern = { 0x8B, 0x4C, 0x24, 0x04, 0x81, 0xE1, 0xFF, 0xFF, 0xFF, 0x7F };
        static byte[] NudePatchPattern = { 0x8B, 0x84, 0xEE, 0x1C, 0x01, 0x00, 0x00, 0x3B, 0x44, 0x24, 0x14 };
        static byte[] ZoomhackPattern = { 0xDF, 0xE0, 0xF6, 0xC4, 0x41, 0x7A, 0x08, 0xD9, 0x9E };
        static byte[] MulticlientPattern = { 0x6A, 0x06, 0x8D, 0x44, 0x24, 0x48, 0x50, 0x8B, 0xCF };
        static byte[] CallForwardPattern = { 0x56, 0x8B, 0xF1, 0x0F, 0xB7, 0x86, 0x3E, 0x10, 0x00, 0x00, 0x57, 0x66, 0x8B, 0x7C, 0x24, 0x10, 0x0F, 0xB7, 0xCF, 0x8D, 0x14, 0x01, 0x3B, 0x96, 0x4C, 0x10, 0x00, 0x00 };
        static byte[] MultiClientErrorStringPattern = Encoding.Default.GetBytes("½ÇÅ©·Îµå°¡ ÀÌ¹Ì ½ÇÇà Áß ÀÔ´Ï´Ù.");
        static byte[] SwearFilterStringPattern = Encoding.Unicode.GetBytes("UIIT_MSG_CHATWND_MESSAGE_FILTER");
        static byte[] ServerStatusFULLStringPattern = Encoding.Unicode.GetBytes("UIO_STT_SERVER_MAX_FULL");
        static byte[] ChangeVersionStringPattern = Encoding.Unicode.GetBytes("Ver %d.%03d");
        static byte[] StartingMSGStringPattern = Encoding.Unicode.GetBytes("UIIT_STT_STARTING_MSG");
        static byte[] AlreadyProgramExeStringPattern = Encoding.ASCII.GetBytes("//////////////////////////////////////////////////////////////////");
        static byte[] NoGameGuardStringPattern = Encoding.ASCII.GetBytes(@"config\n_protect.dat");
        static byte[] TextDataNameStringPattern = Encoding.ASCII.GetBytes(@"%stextdata\textdataname.txt");
        static byte[] EnglishStringPattern = Encoding.ASCII.GetBytes("English");
        static byte[] RussiaStringPattern = Encoding.ASCII.GetBytes("Russia");

        #endregion

        class PatchConfig
        {
            public bool MultiClient { get; set; }
            public bool NudePatch { get; set; }
            public bool ZoomHack { get; set; }
            public bool SwearFilter { get; set; }
            public bool ServerStatus { get; set; }
            public bool RedirectIP { get; set; }
            public bool NoGameGuard { get; set; }
            public bool EnglishPatch { get; set; }
            public bool PatchSeed { get; set; }
            
            public IPEndPoint RedirectGatewayServer { get; set; }
            public IPEndPoint RedirectAgentServer { get; set; }
        }

        class ClientConfig
        {
            public string FilePath { get; set; }
            public string SroType { get; set; }
            public uint Locale { get; set; }
            public uint Version { get; set; }
            public string IP { get; set; }
            public string Port { get; set; }
            public IPEndPoint GatewayServer { get; set; }
        }

        static ClientProcess()
        {
            _PatchConfig = new PatchConfig
            {
                MultiClient = true,
                NudePatch = false,
                ZoomHack = false,
                SwearFilter = false,
                ServerStatus = false,
                RedirectIP = true,
                NoGameGuard = false,
                EnglishPatch = false,
                PatchSeed = false,
                RedirectGatewayServer = SroBasic.Metadata.MediaData.ClientInfo.RedirectGatewayServer,
                RedirectAgentServer = SroBasic.Metadata.MediaData.ClientInfo.RedirectAgentSetver
            };

            _ClientConfig = new ClientConfig
            {
                FilePath = SroBasic.Metadata.Config.SroPath,
                SroType = SroBasic.Metadata.MediaData.ClientInfo.SroType,
                Version = SroBasic.Metadata.MediaData.ClientInfo.Version,
                GatewayServer = new IPEndPoint(SroBasic.Metadata.MediaData.ClientInfo.IP,SroBasic.Metadata.MediaData.ClientInfo.Port)
            };
        }

        public static void StartProcess()
        {
            //StartProcess(process_Info.filePath, process_Info.multiClient, process_Info.nudePatch, process_Info.zoomHack,
            //    process_Info.swearFilter, process_Info.serverStatus, process_Info.redirectIP, process_Info.noGameGuard, process_Info.englishPatch);

            StartProcess(_ClientConfig.FilePath,
                _PatchConfig.MultiClient,
                _PatchConfig.NudePatch,
                _PatchConfig.ZoomHack,
                _PatchConfig.SwearFilter,
                _PatchConfig.ServerStatus,
                _PatchConfig.RedirectIP,
                _PatchConfig.NoGameGuard,
                _PatchConfig.EnglishPatch);
        }
        private static void StartProcess(string filePath, bool multiClient, bool nudePatch, bool zoomHack,
            bool swearFilter, bool serverStatus, bool redirectIP, bool noGameGuard, bool englishPatch)
        {
            byte[] FileArray = File.ReadAllBytes(filePath);

            #region FIND ADDRESSES
            //AlreadyProgramExeSearch
            AlreadyProgramExe = FindStringPattern(AlreadyProgramExeStringPattern, FileArray, BaseAddress, PUSH, 1) - 2;
            //SeedPatchSearch
            SeedPatchAdress = BaseAddress + FindPattern(SeedPatchPattern, FileArray, 1);
            //ReplaceText
            StartingMSG = FindStringPattern(StartingMSGStringPattern, FileArray, BaseAddress, PUSH, 1) + 24;
            ChangeVersion = FindStringPattern(ChangeVersionStringPattern, FileArray, BaseAddress, PUSH, 1);
            if (multiClient)
            {
                //MulticlientSearch
                MultiClientAddress = BaseAddress + FindPattern(MulticlientPattern, FileArray, 1) + 9;
                //CallForwardSearch
                CallForwardAddress = BaseAddress + FindPattern(CallForwardPattern, FileArray, 1);
                //MultiClientErrorSearch
                MultiClientError = FindStringPattern(MultiClientErrorStringPattern, FileArray, BaseAddress, PUSH, 1) - 8;
            }
            if (nudePatch)
            {
                //NudePatchSearch
                NudePatch = BaseAddress + FindPattern(NudePatchPattern, FileArray, 1) + 11;
            }
            if (zoomHack)
            {
                //ZoomhackSearch
                Zoomhack = BaseAddress + FindPattern(ZoomhackPattern, FileArray, 2) + 5;
            }
            if (swearFilter)
            {
                //SwearFilterSearch
                SwearFilter1 = FindStringPattern(SwearFilterStringPattern, FileArray, BaseAddress, PUSH, 1) - 2;
                SwearFilter2 = FindStringPattern(SwearFilterStringPattern, FileArray, BaseAddress, PUSH, 2) - 2;
                SwearFilter3 = FindStringPattern(SwearFilterStringPattern, FileArray, BaseAddress, PUSH, 3) - 2;
                SwearFilter4 = FindStringPattern(SwearFilterStringPattern, FileArray, BaseAddress, PUSH, 4) - 2;
            }
            if (serverStatus)
            {
                //ServerStatusSearch
                ServerStatusFULL = FindStringPattern(ServerStatusFULLStringPattern, FileArray, BaseAddress, PUSH, 1) - 2;
            }
            if (redirectIP)
            {
                //RedirectIPAddressSearch
                RedirectIPAddress = BaseAddress + FindPattern(RedirectIPAddressPattern, FileArray, 1) - 50;
            }
            if (noGameGuard)
            {
                //NoGameGuardSearch
                NoGameGuard = FindStringPattern(NoGameGuardStringPattern, FileArray, BaseAddress, PUSH, 1);
            }
            if (englishPatch)
            {
                //EnglishPatchSearch
                TextDataName = FindStringPattern(TextDataNameStringPattern, FileArray, BaseAddress, PUSH, 1) - 23;
                EnglishPatch = FindStringPattern(EnglishStringPattern, FileArray, BaseAddress, MOVESI, 1) + 14;
                RussianHack = FindStringPattern(RussiaStringPattern, FileArray, BaseAddress, MOVESI, 1) + 14;
            }

            #endregion
            StartLoader();

        }


        #region startloader
        static uint ByteArray = 0;

        private static void StartLoader()
        {
            //StartLoader(process_Info.filePath, Globals.SilkProcess, clientFile_Info.sro_type, clientFile_Info.locale, process_Info.redirectIP, process_Info.multiClient);
            StartLoader(
                _ClientConfig.FilePath,
                _Process,
                _ClientConfig.SroType,
                (int)_ClientConfig.Locale,
                _PatchConfig.RedirectIP,
                _PatchConfig.MultiClient);
        }
        private static void StartLoader(string clietFilePath, Process clientProcess, string sroType, int locale, bool redirectIP = true, bool multiClient = true)
        {
            if (File.Exists(clietFilePath))
            {
                if (sroType != "iSro")
                {
                    Kernel32.CreateMutex(IntPtr.Zero, false, "Silkroad Online Launcher");
                    Kernel32.CreateMutex(IntPtr.Zero, false, "Ready");


                    clientProcess = new Process();
                    clientProcess.StartInfo.FileName = clietFilePath;
                    clientProcess.StartInfo.Arguments = "0/" + locale + " 0 0";// u can open edxsilkroadloader it will tell u :P //
                    clientProcess.Start();


                    IntPtr SroProcessHandle = Kernel32.OpenProcess((uint)(0x000F0000L | 0x00100000L | 0xFFF), 0, clientProcess.Id);

                    QuickPatches(SroProcessHandle);

                    if (redirectIP)
                    {
                        RedirectIP(SroProcessHandle);
                    }
                    if (multiClient)
                    {
                        MultiClient(SroProcessHandle);
                    }
                    //StartingTextMSG(SroProcessHandle, StartingMessageText, HexColorArray);
                }
                else
                {
                    Kernel32.CreateMutex(IntPtr.Zero, false, "Silkroad Online Launcher");
                    Kernel32.CreateMutex(IntPtr.Zero, false, "Ready");


                    clientProcess = new Process();
                    clientProcess.StartInfo.FileName = clietFilePath;
                    clientProcess.StartInfo.Arguments = "0/" + locale + " 0 0";// u can open edxsilkroadloader it will tell u :P //
                    clientProcess.Start();
                }
            }
            else
            {
                MessageBox.Show("Please set path to sro_client.exe", "Loader Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private static void QuickPatches(IntPtr SroProcessHandle)
        {
            //Quickpatches(SroProcessHandle, process_Info.noGameGuard, process_Info.serverStatus,
            //process_Info.nudePatch, process_Info.swearFilter, process_Info.zoomHack,
            //process_Info.englishPatch, process_Info.patchSeed);
            Quickpatches(SroProcessHandle,
                _PatchConfig.NoGameGuard,
                _PatchConfig.ServerStatus,
                _PatchConfig.NudePatch,
                _PatchConfig.SwearFilter,
                _PatchConfig.ZoomHack,
                _PatchConfig.EnglishPatch,
                _PatchConfig.PatchSeed);
            
        }
        private static void Quickpatches(IntPtr SroProcessHandle, bool noGameGuard, bool serverStatus,
            bool nudePatch, bool swearFilter, bool zoomHack,
            bool englishPatch, bool patchSeed)
        {
            //Already Program Exe
            Kernel32.WriteProcessMemory(SroProcessHandle, AlreadyProgramExe, JMP, JMP.Length, ByteArray);

            //Multiclient Error MessageBox
            Kernel32.WriteProcessMemory(SroProcessHandle, MultiClientError, JMP, JMP.Length, ByteArray);

            if (noGameGuard)//doesnt work
            {
                //No GameGuard
                Kernel32.WriteProcessMemory(SroProcessHandle, NoGameGuard, RETN, RETN.Length, ByteArray);
            }
            if (serverStatus)
            {
                //Serverstatus FULL
                Kernel32.WriteProcessMemory(SroProcessHandle, ServerStatusFULL, NOPNOP, NOPNOP.Length, ByteArray);
            }
            if (nudePatch)
            {
                //Nude Patch
                Kernel32.WriteProcessMemory(SroProcessHandle, NudePatch, NOPNOP, NOPNOP.Length, ByteArray);
            }
            if (swearFilter)
            {
                //Swear Filter
                Kernel32.WriteProcessMemory(SroProcessHandle, SwearFilter1, JMP, JMP.Length, ByteArray);
                Kernel32.WriteProcessMemory(SroProcessHandle, SwearFilter2, JMP, JMP.Length, ByteArray);
                Kernel32.WriteProcessMemory(SroProcessHandle, SwearFilter3, JMP, JMP.Length, ByteArray);
                Kernel32.WriteProcessMemory(SroProcessHandle, SwearFilter4, JMP, JMP.Length, ByteArray);
            }
            if (zoomHack)
            {
                //Zoomhack
                Kernel32.WriteProcessMemory(SroProcessHandle, Zoomhack, JMP, JMP.Length, ByteArray);
            }
            if (englishPatch)//doesnt works
            {
                //English Patch
                Kernel32.WriteProcessMemory(SroProcessHandle, EnglishPatch, NOPNOP, NOPNOP.Length, ByteArray);
                Kernel32.WriteProcessMemory(SroProcessHandle, RussianHack, JMP, JMP.Length, ByteArray);
                Kernel32.WriteProcessMemory(SroProcessHandle, TextDataName, LanguageTab, LanguageTab.Length, ByteArray);
            }
            if (patchSeed)
            {
                //Seed Patch
                Kernel32.WriteProcessMemory(SroProcessHandle, SeedPatchAdress, SeedPatch, SeedPatch.Length, ByteArray);
            }
        }

        private static void RedirectIP(IntPtr SroProcessHandle)
        {
            RedirectIP(SroProcessHandle, _PatchConfig.RedirectGatewayServer.Port.ToString(), _PatchConfig.RedirectGatewayServer.Address.ToString());
        }
        private static void RedirectIP(IntPtr SroProcessHandle, string port, string ip)
        {
            uint RedirectIPCodeCave = Kernel32.VirtualAllocEx(SroProcessHandle, IntPtr.Zero, 27, 0x1000, 0x4);
            uint SockAddrStruct = Kernel32.VirtualAllocEx(SroProcessHandle, IntPtr.Zero, 8, 0x1000, 0x4);
            uint WS2Connect = Kernel32.GetProcAddress(Kernel32.GetModuleHandle("WS2_32.dll"), "connect");

            byte[] WS2Array = BitConverter.GetBytes(WS2Connect - RedirectIPCodeCave - 26);
            byte[] SockAddr = BitConverter.GetBytes(SockAddrStruct);
            byte[] CallRedirectIp = BitConverter.GetBytes(RedirectIPCodeCave - RedirectIPAddress - 5);

            byte[] Port = BitConverter.GetBytes(Convert.ToUInt32(port));
            string[] sIP = ip.Split('.');
            byte[] IP1 = BitConverter.GetBytes(Convert.ToUInt16(sIP[0]));
            byte[] IP2 = BitConverter.GetBytes(Convert.ToUInt16(sIP[1]));
            byte[] IP3 = BitConverter.GetBytes(Convert.ToUInt16(sIP[2]));
            byte[] IP4 = BitConverter.GetBytes(Convert.ToUInt16(sIP[3]));


            byte[] Connection = { 0x02, 0x00, Port[1], Port[0], IP1[0], IP2[0], IP3[0], IP4[0] };
            byte[] CallAddress = { 0xE8, CallRedirectIp[0], CallRedirectIp[1], CallRedirectIp[2], CallRedirectIp[3] };
            byte[] RedirectCode = {       0x50, //PUSH EAX
                                          0x66, 0x8B, 0x47, 0x02, //MOV AX,WORD PTR DS:[EDI+2]
                                          0x66, 0x3D, 0x3D, 0xA3, //CMP AX,0A33D
                                          0x75, 0x05, //JNZ SHORT xxxxxxxx
                                          0xBF, SockAddr[0], SockAddr[1], SockAddr[2], SockAddr[3], //MOV EDI,xxxxxxxx
                                          0x58, //POP EAX
                                          0x6A, 0x10, //PUSH 10
                                          0x57, //PUSH EDI
                                          0x51, //PUSH ECX
                                          0xE8, WS2Array[0], WS2Array[1], WS2Array[2], WS2Array[3], //CALL WS2_32.connect
                                          0xC3 //RETN
                                      };

            Kernel32.WriteProcessMemory(SroProcessHandle, RedirectIPCodeCave, RedirectCode, RedirectCode.Length, ByteArray);
            Kernel32.WriteProcessMemory(SroProcessHandle, SockAddrStruct, Connection, Connection.Length, ByteArray);
            Kernel32.WriteProcessMemory(SroProcessHandle, RedirectIPAddress, CallAddress, CallAddress.Length, ByteArray);
        }
        private static void MultiClient(IntPtr SroProcessHandle)
        {
            uint MultiClientCodeCave = Kernel32.VirtualAllocEx(SroProcessHandle, IntPtr.Zero, 45, 0x1000, 0x4);
            uint MACCodeCave = Kernel32.VirtualAllocEx(SroProcessHandle, IntPtr.Zero, 4, 0x1000, 0x4);
            uint GTC = Kernel32.GetProcAddress(Kernel32.GetModuleHandle("kernel32.dll"), "GetTickCount");

            byte[] CallBack = BitConverter.GetBytes(MultiClientCodeCave + 41);
            byte[] CALLForward = BitConverter.GetBytes(CallForwardAddress - MultiClientCodeCave - 34);
            byte[] MACAddress = BitConverter.GetBytes(MACCodeCave);
            byte[] GTCAddress = BitConverter.GetBytes(GTC - MultiClientCodeCave - 18);

            byte[] MultiClientArray = BitConverter.GetBytes(MultiClientCodeCave - MultiClientAddress - 5);
            byte[] MultiClientCodeArray = { 0xE8, MultiClientArray[0], MultiClientArray[1], MultiClientArray[2], MultiClientArray[3] };

            byte[] MultiClientCode = {   0x8F, 0x05, CallBack[0], CallBack[1], CallBack[2], CallBack[3], //POP DWORD PTR DS:[xxxxxxxx]
                                         0xA3, MACAddress[0], MACAddress[1], MACAddress[2], MACAddress[3], //MOV DWORD PTR DS:[xxxxxxxx],EAX
                                         0x60, //PUSHAD
                                         0x9C, //PUSHFD
                                         0xE8, GTCAddress[0], GTCAddress[1], GTCAddress[2], GTCAddress[3], // Call KERNEL32.gettickcount
                                         0x8B, 0x0D, MACAddress[0], MACAddress[1], MACAddress[2], MACAddress[3], //MOV ECX,DWORD PTR DS:[xxxxxxxx]
                                         0x89, 0x41, 0x02, // MOV DWORD PTR DS:[ECX+2],EAX
                                         0x9D, //POPFD
                                         0x61, //POPAD
                                         0xE8, CALLForward[0], CALLForward[1], CALLForward[2], CALLForward[3], //CALL xxxxxxxx
                                         0xFF, 0x35, CallBack[0], CallBack[1], CallBack[2], CallBack[3], // PUSH DWORD PTR DS:[xxxxxxxx]
                                         0xC3 //RETN
                                       };

            Kernel32.WriteProcessMemory(SroProcessHandle, MultiClientCodeCave, MultiClientCode, MultiClientCode.Length, ByteArray);
            Kernel32.WriteProcessMemory(SroProcessHandle, MultiClientAddress, MultiClientCodeArray, MultiClientCodeArray.Length, ByteArray);
        }

        private static void StartingTextMSG(IntPtr SroProcessHandle, string StartingText, byte[] HexColor)
        {
            string sChangeVersionString = "vieSroBot 1.0\nSro type: vSro";//Globals.botTitle + "\nSro type: " + Globals.sro_type;
            StartingTextMSG(SroProcessHandle, StartingText, sChangeVersionString, HexColor);
        }
        private static void StartingTextMSG(IntPtr SroProcessHandle, string StartingText, string sChangeVersionString, byte[] HexColor)
        {
            string ChangeVersionString = sChangeVersionString;
            uint StartingMSGStringCodeCave = Kernel32.VirtualAllocEx(SroProcessHandle, IntPtr.Zero, StartingText.Length, 0x1000, 0x4);
            uint ChangeVersionStringCodeCave = Kernel32.VirtualAllocEx(SroProcessHandle, IntPtr.Zero, StartingText.Length, 0x1000, 0x4);
            byte[] StartingMSGByteArray = Encoding.Unicode.GetBytes(StartingText);
            byte[] ChangeVersionByteArray = Encoding.Unicode.GetBytes(ChangeVersionString);
            byte[] CallStartingMSG = BitConverter.GetBytes(StartingMSGStringCodeCave);
            byte[] CallChangeVersion = BitConverter.GetBytes(ChangeVersionStringCodeCave);
            byte[] StartingMSGCodeArray = { 0xB8, CallStartingMSG[0], CallStartingMSG[1], CallStartingMSG[2], CallStartingMSG[3] };
            byte[] ChangeVersionCodeArray = { 0x68, CallChangeVersion[0], CallChangeVersion[1], CallChangeVersion[2], CallChangeVersion[3] };
            Kernel32.WriteProcessMemory(SroProcessHandle, ChangeVersionStringCodeCave, ChangeVersionByteArray, ChangeVersionByteArray.Length, ByteArray);
            Kernel32.WriteProcessMemory(SroProcessHandle, ChangeVersion, ChangeVersionCodeArray, ChangeVersionCodeArray.Length, ByteArray);
            Kernel32.WriteProcessMemory(SroProcessHandle, ChangeVersion - 59, HexColor, HexColor.Length, ByteArray);
            Kernel32.WriteProcessMemory(SroProcessHandle, StartingMSGStringCodeCave, StartingMSGByteArray, StartingMSGByteArray.Length, ByteArray);
            Kernel32.WriteProcessMemory(SroProcessHandle, StartingMSG, StartingMSGCodeArray, StartingMSGCodeArray.Length, ByteArray);
            Kernel32.WriteProcessMemory(SroProcessHandle, StartingMSG + 9, HexColor, HexColor.Length, ByteArray);
        }

        private static uint FindPattern(byte[] Pattern, byte[] FileByteArray, uint Result)
        {
            uint MyPosition = 0;
            uint ResultCounter = 0;
            for (uint PositionFileByteArray = 0; PositionFileByteArray < FileByteArray.Length - Pattern.Length; PositionFileByteArray++)
            {
                bool found = true;
                for (uint PositionPattern = 0; PositionPattern < Pattern.Length; PositionPattern++)
                {
                    if (FileByteArray[PositionFileByteArray + PositionPattern] != Pattern[PositionPattern])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    ResultCounter += 1;
                    if (Result == ResultCounter)
                    {
                        MyPosition = PositionFileByteArray;
                        break;
                    }
                }
            }
            return MyPosition;
        }
        private static uint FindStringPattern(byte[] StringByteArray, byte[] FileArray, uint BaseAddress, byte StringWorker, uint Result)
        {
            uint MyPosition = 0;
            byte[] StringWorkerAddress = { StringWorker, 0x00, 0x00, 0x00, 0x00 };
            byte[] StringAddress = new byte[4];
            StringAddress = BitConverter.GetBytes(BaseAddress + FindPattern(StringByteArray, FileArray, 1));
            StringWorkerAddress[1] = StringAddress[0];
            StringWorkerAddress[2] = StringAddress[1];
            StringWorkerAddress[3] = StringAddress[2];
            StringWorkerAddress[4] = StringAddress[3];

            MyPosition = BaseAddress + FindPattern(StringWorkerAddress, FileArray, Result);
            return MyPosition;
        }

        #endregion startloader
    }
}
