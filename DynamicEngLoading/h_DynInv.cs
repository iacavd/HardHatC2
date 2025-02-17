﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace DynamicEngLoading
{
    public class h_DynInv
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct UNICODE_STRING
        {
            public UInt16 Length;
            public UInt16 MaximumLength;
            public IntPtr Buffer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ANSI_STRING
        {
            public UInt16 Length;
            public UInt16 MaximumLength;
            public IntPtr Buffer;
        }

        public struct PROCESS_BASIC_INFORMATION
        {
            public IntPtr ExitStatus;
            public IntPtr PebBaseAddress;
            public IntPtr AffinityMask;
            public IntPtr BasePriority;
            public UIntPtr UniqueProcessId;
            public int InheritedFromUniqueProcessId;

            public int Size
            {
                get { return (int)Marshal.SizeOf(typeof(PROCESS_BASIC_INFORMATION)); }
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct OBJECT_ATTRIBUTES
        {
            public Int32 Length;
            public IntPtr RootDirectory;
            public IntPtr ObjectName; // -> UNICODE_STRING
            public uint Attributes;
            public IntPtr SecurityDescriptor;
            public IntPtr SecurityQualityOfService;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IO_STATUS_BLOCK
        {
            public IntPtr Status;
            public IntPtr Information;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct LargeInteger
        {
            [FieldOffset(0)]
            public int Low;
            [FieldOffset(4)]
            public int High;
            [FieldOffset(0)]
            public long QuadPart;
        }

        [StructLayout(LayoutKind.Explicit, Size = 8)]
        public struct LARGE_INTEGER
        {
            [FieldOffset(0)] public long QuadPart;
            [FieldOffset(0)] public uint LowPart;
            [FieldOffset(4)] public int HighPart;
            [FieldOffset(0)] public int LowPartAsInt;
            [FieldOffset(0)] public uint LowPartAsUInt;
            [FieldOffset(4)] public int HighPartAsInt;
            [FieldOffset(4)] public uint HighPartAsUInt;

            public long ToInt64()
            {
                return ((long)this.HighPart << 32) | (uint)this.LowPartAsInt;
            }

            public static LARGE_INTEGER FromInt64(long value)
            {
                return new LARGE_INTEGER
                {
                    LowPartAsInt = (int)(value),
                    HighPartAsInt = (int)((value >> 32))
                };
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CLIENT_ID
        {
            public IntPtr UniqueProcess;
            public IntPtr UniqueThread;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OSVERSIONINFOEX
        {
            public uint OSVersionInfoSize;
            public uint MajorVersion;
            public uint MinorVersion;
            public uint BuildNumber;
            public uint PlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string CSDVersion;
            public ushort ServicePackMajor;
            public ushort ServicePackMinor;
            public ushort SuiteMask;
            public byte ProductType;
            public byte Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LIST_ENTRY
        {
            public IntPtr Flink;
            public IntPtr Blink;
        }

        public enum MEMORYINFOCLASS : int
        {
            MemoryBasicInformation = 0,
            MemoryWorkingSetList,
            MemorySectionName,
            MemoryBasicVlmInformation
        }

        public enum PROCESSINFOCLASS : int
        {
            ProcessBasicInformation = 0, // 0, q: PROCESS_BASIC_INFORMATION, PROCESS_EXTENDED_BASIC_INFORMATION
            ProcessQuotaLimits, // qs: QUOTA_LIMITS, QUOTA_LIMITS_EX
            ProcessIoCounters, // q: IO_COUNTERS
            ProcessVmCounters, // q: VM_COUNTERS, VM_COUNTERS_EX
            ProcessTimes, // q: KERNEL_USER_TIMES
            ProcessBasePriority, // s: KPRIORITY
            ProcessRaisePriority, // s: ULONG
            ProcessDebugPort, // q: HANDLE
            ProcessExceptionPort, // s: HANDLE
            ProcessAccessToken, // s: PROCESS_ACCESS_TOKEN
            ProcessLdtInformation, // 10
            ProcessLdtSize,
            ProcessDefaultHardErrorMode, // qs: ULONG
            ProcessIoPortHandlers, // (kernel-mode only)
            ProcessPooledUsageAndLimits, // q: POOLED_USAGE_AND_LIMITS
            ProcessWorkingSetWatch, // q: PROCESS_WS_WATCH_INFORMATION[]; s: void
            ProcessUserModeIOPL,
            ProcessEnableAlignmentFaultFixup, // s: BOOLEAN
            ProcessPriorityClass, // qs: PROCESS_PRIORITY_CLASS
            ProcessWx86Information,
            ProcessHandleCount, // 20, q: ULONG, PROCESS_HANDLE_INFORMATION
            ProcessAffinityMask, // s: KAFFINITY
            ProcessPriorityBoost, // qs: ULONG
            ProcessDeviceMap, // qs: PROCESS_DEVICEMAP_INFORMATION, PROCESS_DEVICEMAP_INFORMATION_EX
            ProcessSessionInformation, // q: PROCESS_SESSION_INFORMATION
            ProcessForegroundInformation, // s: PROCESS_FOREGROUND_BACKGROUND
            ProcessWow64Information, // q: ULONG_PTR
            ProcessImageFileName, // q: UNICODE_STRING
            ProcessLUIDDeviceMapsEnabled, // q: ULONG
            ProcessBreakOnTermination, // qs: ULONG
            ProcessDebugObjectHandle, // 30, q: HANDLE
            ProcessDebugFlags, // qs: ULONG
            ProcessHandleTracing, // q: PROCESS_HANDLE_TRACING_QUERY; s: size 0 disables, otherwise enables
            ProcessIoPriority, // qs: ULONG
            ProcessExecuteFlags, // qs: ULONG
            ProcessResourceManagement,
            ProcessCookie, // q: ULONG
            ProcessImageInformation, // q: SECTION_IMAGE_INFORMATION
            ProcessCycleTime, // q: PROCESS_CYCLE_TIME_INFORMATION
            ProcessPagePriority, // q: ULONG
            ProcessInstrumentationCallback, // 40
            ProcessThreadStackAllocation, // s: PROCESS_STACK_ALLOCATION_INFORMATION, PROCESS_STACK_ALLOCATION_INFORMATION_EX
            ProcessWorkingSetWatchEx, // q: PROCESS_WS_WATCH_INFORMATION_EX[]
            ProcessImageFileNameWin32, // q: UNICODE_STRING
            ProcessImageFileMapping, // q: HANDLE (input)
            ProcessAffinityUpdateMode, // qs: PROCESS_AFFINITY_UPDATE_MODE
            ProcessMemoryAllocationMode, // qs: PROCESS_MEMORY_ALLOCATION_MODE
            ProcessGroupInformation, // q: USHORT[]
            ProcessTokenVirtualizationEnabled, // s: ULONG
            ProcessConsoleHostProcess, // q: ULONG_PTR
            ProcessWindowInformation, // 50, q: PROCESS_WINDOW_INFORMATION
            ProcessHandleInformation, // q: PROCESS_HANDLE_SNAPSHOT_INFORMATION // since WIN8
            ProcessMitigationPolicy, // s: PROCESS_MITIGATION_POLICY_INFORMATION
            ProcessDynamicFunctionTableInformation,
            ProcessHandleCheckingMode,
            ProcessKeepAliveCount, // q: PROCESS_KEEPALIVE_COUNT_INFORMATION
            ProcessRevokeFileHandles, // s: PROCESS_REVOKE_FILE_HANDLES_INFORMATION
            MaxProcessInfoClass
        };

        /// <summary>
        /// NT_CREATION_FLAGS is an undocumented enum. https://processhacker.sourceforge.io/doc/ntpsapi_8h_source.html
        /// </summary>
        public enum NT_CREATION_FLAGS : ulong
        {
            CREATE_SUSPENDED = 0x00000001,
            SKIP_THREAD_ATTACH = 0x00000002,
            HIDE_FROM_DEBUGGER = 0x00000004,
            HAS_SECURITY_DESCRIPTOR = 0x00000010,
            ACCESS_CHECK_IN_TARGET = 0x00000020,
            INITIAL_THREAD = 0x00000080
        }

        /// <summary>
        /// NTSTATUS is an undocument enum. https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/596a1078-e883-4972-9bbc-49e60bebca55
        /// https://www.pinvoke.net/default.aspx/Enums/NtStatus.html
        /// </summary>
        public enum NTSTATUS : uint
        {
            // Success
            Success = 0x00000000,
            Wait0 = 0x00000000,
            Wait1 = 0x00000001,
            Wait2 = 0x00000002,
            Wait3 = 0x00000003,
            Wait63 = 0x0000003f,
            Abandoned = 0x00000080,
            AbandonedWait0 = 0x00000080,
            AbandonedWait1 = 0x00000081,
            AbandonedWait2 = 0x00000082,
            AbandonedWait3 = 0x00000083,
            AbandonedWait63 = 0x000000bf,
            UserApc = 0x000000c0,
            KernelApc = 0x00000100,
            Alerted = 0x00000101,
            Timeout = 0x00000102,
            Pending = 0x00000103,
            Reparse = 0x00000104,
            MoreEntries = 0x00000105,
            NotAllAssigned = 0x00000106,
            SomeNotMapped = 0x00000107,
            OpLockBreakInProgress = 0x00000108,
            VolumeMounted = 0x00000109,
            RxActCommitted = 0x0000010a,
            NotifyCleanup = 0x0000010b,
            NotifyEnumDir = 0x0000010c,
            NoQuotasForAccount = 0x0000010d,
            PrimaryTransportConnectFailed = 0x0000010e,
            PageFaultTransition = 0x00000110,
            PageFaultDemandZero = 0x00000111,
            PageFaultCopyOnWrite = 0x00000112,
            PageFaultGuardPage = 0x00000113,
            PageFaultPagingFile = 0x00000114,
            CrashDump = 0x00000116,
            ReparseObject = 0x00000118,
            NothingToTerminate = 0x00000122,
            ProcessNotInJob = 0x00000123,
            ProcessInJob = 0x00000124,
            ProcessCloned = 0x00000129,
            FileLockedWithOnlyReaders = 0x0000012a,
            FileLockedWithWriters = 0x0000012b,

            // Informational
            Informational = 0x40000000,
            ObjectNameExists = 0x40000000,
            ThreadWasSuspended = 0x40000001,
            WorkingSetLimitRange = 0x40000002,
            ImageNotAtBase = 0x40000003,
            RegistryRecovered = 0x40000009,

            // Warning
            Warning = 0x80000000,
            GuardPageViolation = 0x80000001,
            DatatypeMisalignment = 0x80000002,
            Breakpoint = 0x80000003,
            SingleStep = 0x80000004,
            BufferOverflow = 0x80000005,
            NoMoreFiles = 0x80000006,
            HandlesClosed = 0x8000000a,
            PartialCopy = 0x8000000d,
            DeviceBusy = 0x80000011,
            InvalidEaName = 0x80000013,
            EaListInconsistent = 0x80000014,
            NoMoreEntries = 0x8000001a,
            LongJump = 0x80000026,
            DllMightBeInsecure = 0x8000002b,

            // Error
            Error = 0xc0000000,
            Unsuccessful = 0xc0000001,
            NotImplemented = 0xc0000002,
            InvalidInfoClass = 0xc0000003,
            InfoLengthMismatch = 0xc0000004,
            AccessViolation = 0xc0000005,
            InPageError = 0xc0000006,
            PagefileQuota = 0xc0000007,
            InvalidHandle = 0xc0000008,
            BadInitialStack = 0xc0000009,
            BadInitialPc = 0xc000000a,
            InvalidCid = 0xc000000b,
            TimerNotCanceled = 0xc000000c,
            InvalidParameter = 0xc000000d,
            NoSuchDevice = 0xc000000e,
            NoSuchFile = 0xc000000f,
            InvalidDeviceRequest = 0xc0000010,
            EndOfFile = 0xc0000011,
            WrongVolume = 0xc0000012,
            NoMediaInDevice = 0xc0000013,
            NoMemory = 0xc0000017,
            ConflictingAddresses = 0xc0000018,
            NotMappedView = 0xc0000019,
            UnableToFreeVm = 0xc000001a,
            UnableToDeleteSection = 0xc000001b,
            IllegalInstruction = 0xc000001d,
            AlreadyCommitted = 0xc0000021,
            AccessDenied = 0xc0000022,
            BufferTooSmall = 0xc0000023,
            ObjectTypeMismatch = 0xc0000024,
            NonContinuableException = 0xc0000025,
            BadStack = 0xc0000028,
            NotLocked = 0xc000002a,
            NotCommitted = 0xc000002d,
            InvalidParameterMix = 0xc0000030,
            ObjectNameInvalid = 0xc0000033,
            ObjectNameNotFound = 0xc0000034,
            ObjectNameCollision = 0xc0000035,
            ObjectPathInvalid = 0xc0000039,
            ObjectPathNotFound = 0xc000003a,
            ObjectPathSyntaxBad = 0xc000003b,
            DataOverrun = 0xc000003c,
            DataLate = 0xc000003d,
            DataError = 0xc000003e,
            CrcError = 0xc000003f,
            SectionTooBig = 0xc0000040,
            PortConnectionRefused = 0xc0000041,
            InvalidPortHandle = 0xc0000042,
            SharingViolation = 0xc0000043,
            QuotaExceeded = 0xc0000044,
            InvalidPageProtection = 0xc0000045,
            MutantNotOwned = 0xc0000046,
            SemaphoreLimitExceeded = 0xc0000047,
            PortAlreadySet = 0xc0000048,
            SectionNotImage = 0xc0000049,
            SuspendCountExceeded = 0xc000004a,
            ThreadIsTerminating = 0xc000004b,
            BadWorkingSetLimit = 0xc000004c,
            IncompatibleFileMap = 0xc000004d,
            SectionProtection = 0xc000004e,
            EasNotSupported = 0xc000004f,
            EaTooLarge = 0xc0000050,
            NonExistentEaEntry = 0xc0000051,
            NoEasOnFile = 0xc0000052,
            EaCorruptError = 0xc0000053,
            FileLockConflict = 0xc0000054,
            LockNotGranted = 0xc0000055,
            DeletePending = 0xc0000056,
            CtlFileNotSupported = 0xc0000057,
            UnknownRevision = 0xc0000058,
            RevisionMismatch = 0xc0000059,
            InvalidOwner = 0xc000005a,
            InvalidPrimaryGroup = 0xc000005b,
            NoImpersonationToken = 0xc000005c,
            CantDisableMandatory = 0xc000005d,
            NoLogonServers = 0xc000005e,
            NoSuchLogonSession = 0xc000005f,
            NoSuchPrivilege = 0xc0000060,
            PrivilegeNotHeld = 0xc0000061,
            InvalidAccountName = 0xc0000062,
            UserExists = 0xc0000063,
            NoSuchUser = 0xc0000064,
            GroupExists = 0xc0000065,
            NoSuchGroup = 0xc0000066,
            MemberInGroup = 0xc0000067,
            MemberNotInGroup = 0xc0000068,
            LastAdmin = 0xc0000069,
            WrongPassword = 0xc000006a,
            IllFormedPassword = 0xc000006b,
            PasswordRestriction = 0xc000006c,
            LogonFailure = 0xc000006d,
            AccountRestriction = 0xc000006e,
            InvalidLogonHours = 0xc000006f,
            InvalidWorkstation = 0xc0000070,
            PasswordExpired = 0xc0000071,
            AccountDisabled = 0xc0000072,
            NoneMapped = 0xc0000073,
            TooManyLuidsRequested = 0xc0000074,
            LuidsExhausted = 0xc0000075,
            InvalidSubAuthority = 0xc0000076,
            InvalidAcl = 0xc0000077,
            InvalidSid = 0xc0000078,
            InvalidSecurityDescr = 0xc0000079,
            ProcedureNotFound = 0xc000007a,
            InvalidImageFormat = 0xc000007b,
            NoToken = 0xc000007c,
            BadInheritanceAcl = 0xc000007d,
            RangeNotLocked = 0xc000007e,
            DiskFull = 0xc000007f,
            ServerDisabled = 0xc0000080,
            ServerNotDisabled = 0xc0000081,
            TooManyGuidsRequested = 0xc0000082,
            GuidsExhausted = 0xc0000083,
            InvalidIdAuthority = 0xc0000084,
            AgentsExhausted = 0xc0000085,
            InvalidVolumeLabel = 0xc0000086,
            SectionNotExtended = 0xc0000087,
            NotMappedData = 0xc0000088,
            ResourceDataNotFound = 0xc0000089,
            ResourceTypeNotFound = 0xc000008a,
            ResourceNameNotFound = 0xc000008b,
            ArrayBoundsExceeded = 0xc000008c,
            FloatDenormalOperand = 0xc000008d,
            FloatDivideByZero = 0xc000008e,
            FloatInexactResult = 0xc000008f,
            FloatInvalidOperation = 0xc0000090,
            FloatOverflow = 0xc0000091,
            FloatStackCheck = 0xc0000092,
            FloatUnderflow = 0xc0000093,
            IntegerDivideByZero = 0xc0000094,
            IntegerOverflow = 0xc0000095,
            PrivilegedInstruction = 0xc0000096,
            TooManyPagingFiles = 0xc0000097,
            FileInvalid = 0xc0000098,
            InsufficientResources = 0xc000009a,
            InstanceNotAvailable = 0xc00000ab,
            PipeNotAvailable = 0xc00000ac,
            InvalidPipeState = 0xc00000ad,
            PipeBusy = 0xc00000ae,
            IllegalFunction = 0xc00000af,
            PipeDisconnected = 0xc00000b0,
            PipeClosing = 0xc00000b1,
            PipeConnected = 0xc00000b2,
            PipeListening = 0xc00000b3,
            InvalidReadMode = 0xc00000b4,
            IoTimeout = 0xc00000b5,
            FileForcedClosed = 0xc00000b6,
            ProfilingNotStarted = 0xc00000b7,
            ProfilingNotStopped = 0xc00000b8,
            NotSameDevice = 0xc00000d4,
            FileRenamed = 0xc00000d5,
            CantWait = 0xc00000d8,
            PipeEmpty = 0xc00000d9,
            CantTerminateSelf = 0xc00000db,
            InternalError = 0xc00000e5,
            InvalidParameter1 = 0xc00000ef,
            InvalidParameter2 = 0xc00000f0,
            InvalidParameter3 = 0xc00000f1,
            InvalidParameter4 = 0xc00000f2,
            InvalidParameter5 = 0xc00000f3,
            InvalidParameter6 = 0xc00000f4,
            InvalidParameter7 = 0xc00000f5,
            InvalidParameter8 = 0xc00000f6,
            InvalidParameter9 = 0xc00000f7,
            InvalidParameter10 = 0xc00000f8,
            InvalidParameter11 = 0xc00000f9,
            InvalidParameter12 = 0xc00000fa,
            ProcessIsTerminating = 0xc000010a,
            MappedFileSizeZero = 0xc000011e,
            TooManyOpenedFiles = 0xc000011f,
            Cancelled = 0xc0000120,
            CannotDelete = 0xc0000121,
            InvalidComputerName = 0xc0000122,
            FileDeleted = 0xc0000123,
            SpecialAccount = 0xc0000124,
            SpecialGroup = 0xc0000125,
            SpecialUser = 0xc0000126,
            MembersPrimaryGroup = 0xc0000127,
            FileClosed = 0xc0000128,
            TooManyThreads = 0xc0000129,
            ThreadNotInProcess = 0xc000012a,
            TokenAlreadyInUse = 0xc000012b,
            PagefileQuotaExceeded = 0xc000012c,
            CommitmentLimit = 0xc000012d,
            InvalidImageLeFormat = 0xc000012e,
            InvalidImageNotMz = 0xc000012f,
            InvalidImageProtect = 0xc0000130,
            InvalidImageWin16 = 0xc0000131,
            LogonServer = 0xc0000132,
            DifferenceAtDc = 0xc0000133,
            SynchronizationRequired = 0xc0000134,
            DllNotFound = 0xc0000135,
            IoPrivilegeFailed = 0xc0000137,
            OrdinalNotFound = 0xc0000138,
            EntryPointNotFound = 0xc0000139,
            ControlCExit = 0xc000013a,
            InvalidAddress = 0xc0000141,
            PortNotSet = 0xc0000353,
            DebuggerInactive = 0xc0000354,
            CallbackBypass = 0xc0000503,
            PortClosed = 0xc0000700,
            MessageLost = 0xc0000701,
            InvalidMessage = 0xc0000702,
            RequestCanceled = 0xc0000703,
            RecursiveDispatch = 0xc0000704,
            LpcReceiveBufferExpected = 0xc0000705,
            LpcInvalidConnectionUsage = 0xc0000706,
            LpcRequestsNotAllowed = 0xc0000707,
            ResourceInUse = 0xc0000708,
            ProcessIsProtected = 0xc0000712,
            VolumeDirty = 0xc0000806,
            FileCheckedOut = 0xc0000901,
            CheckOutRequired = 0xc0000902,
            BadFileType = 0xc0000903,
            FileTooLarge = 0xc0000904,
            FormsAuthRequired = 0xc0000905,
            VirusInfected = 0xc0000906,
            VirusDeleted = 0xc0000907,
            TransactionalConflict = 0xc0190001,
            InvalidTransaction = 0xc0190002,
            TransactionNotActive = 0xc0190003,
            TmInitializationFailed = 0xc0190004,
            RmNotActive = 0xc0190005,
            RmMetadataCorrupt = 0xc0190006,
            TransactionNotJoined = 0xc0190007,
            DirectoryNotRm = 0xc0190008,
            CouldNotResizeLog = 0xc0190009,
            TransactionsUnsupportedRemote = 0xc019000a,
            LogResizeInvalidSize = 0xc019000b,
            RemoteFileVersionMismatch = 0xc019000c,
            CrmProtocolAlreadyExists = 0xc019000f,
            TransactionPropagationFailed = 0xc0190010,
            CrmProtocolNotFound = 0xc0190011,
            TransactionSuperiorExists = 0xc0190012,
            TransactionRequestNotValid = 0xc0190013,
            TransactionNotRequested = 0xc0190014,
            TransactionAlreadyAborted = 0xc0190015,
            TransactionAlreadyCommitted = 0xc0190016,
            TransactionInvalidMarshallBuffer = 0xc0190017,
            CurrentTransactionNotValid = 0xc0190018,
            LogGrowthFailed = 0xc0190019,
            ObjectNoLongerExists = 0xc0190021,
            StreamMiniversionNotFound = 0xc0190022,
            StreamMiniversionNotValid = 0xc0190023,
            MiniversionInaccessibleFromSpecifiedTransaction = 0xc0190024,
            CantOpenMiniversionWithModifyIntent = 0xc0190025,
            CantCreateMoreStreamMiniversions = 0xc0190026,
            HandleNoLongerValid = 0xc0190028,
            NoTxfMetadata = 0xc0190029,
            LogCorruptionDetected = 0xc0190030,
            CantRecoverWithHandleOpen = 0xc0190031,
            RmDisconnected = 0xc0190032,
            EnlistmentNotSuperior = 0xc0190033,
            RecoveryNotNeeded = 0xc0190034,
            RmAlreadyStarted = 0xc0190035,
            FileIdentityNotPersistent = 0xc0190036,
            CantBreakTransactionalDependency = 0xc0190037,
            CantCrossRmBoundary = 0xc0190038,
            TxfDirNotEmpty = 0xc0190039,
            IndoubtTransactionsExist = 0xc019003a,
            TmVolatile = 0xc019003b,
            RollbackTimerExpired = 0xc019003c,
            TxfAttributeCorrupt = 0xc019003d,
            EfsNotAllowedInTransaction = 0xc019003e,
            TransactionalOpenNotAllowed = 0xc019003f,
            TransactedMappingUnsupportedRemote = 0xc0190040,
            TxfMetadataAlreadyPresent = 0xc0190041,
            TransactionScopeCallbacksNotSet = 0xc0190042,
            TransactionRequiredPromotion = 0xc0190043,
            CannotExecuteFileInTransaction = 0xc0190044,
            TransactionsNotFrozen = 0xc0190045,

            MaximumNtStatus = 0xffffffff
        }

        /////////////////win32
        ///
        public unsafe static class Win32
        {
            [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
            public static extern IntPtr memcpy(IntPtr dest, byte[] src, UInt32 count);

            [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
            public static extern void* Memcpy(byte* dest, byte* src, void* count);


            //top level fields for coff relocs 
            public static int IMAGE_REL_AMD64_ADDR64 = 0x0001; // TODO: Move this to a global area
            public static int IMAGE_REL_AMD64_ADDR32NB = 0x0003;
            /* Most common from the looks of it, just 32-bit relative address from the byte following the relocation */
            public static int IMAGE_REL_AMD64_REL32 = 0x0004;
            public static int IMAGE_REL_AMD64_REL32_5 = 0x0009;

            public static class Kernel32
            {
                [StructLayout(LayoutKind.Sequential)]
                public struct IMAGE_BASE_RELOCATION
                {
                    public uint VirtualAdress;
                    public uint SizeOfBlock;
                }


                [StructLayout(LayoutKind.Sequential)]
                public struct IMAGE_IMPORT_DESCRIPTOR
                {
                    public uint OriginalFirstThunk;
                    public uint TimeDateStamp;
                    public uint ForwarderChain;
                    public uint Name;
                    public uint FirstThunk;
                }

                public struct SYSTEM_INFO
                {
                    public ushort wProcessorArchitecture;
                    public ushort wReserved;
                    public uint dwPageSize;
                    public IntPtr lpMinimumApplicationAddress;
                    public IntPtr lpMaximumApplicationAddress;
                    public UIntPtr dwActiveProcessorMask;
                    public uint dwNumberOfProcessors;
                    public uint dwProcessorType;
                    public uint dwAllocationGranularity;
                    public ushort wProcessorLevel;
                    public ushort wProcessorRevision;
                };

                public enum Platform
                {
                    x86,
                    x64,
                    IA64,
                    Unknown
                }

                [Flags]
                public enum ProcessAccessFlags : UInt32
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684880%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
                    PROCESS_ALL_ACCESS = 0x001F0FFF,
                    PROCESS_CREATE_PROCESS = 0x0080,
                    PROCESS_CREATE_THREAD = 0x0002,
                    PROCESS_DUP_HANDLE = 0x0040,
                    PROCESS_QUERY_INFORMATION = 0x0400,
                    PROCESS_QUERY_LIMITED_INFORMATION = 0x1000,
                    PROCESS_SET_INFORMATION = 0x0200,
                    PROCESS_SET_QUOTA = 0x0100,
                    PROCESS_SUSPEND_RESUME = 0x0800,
                    PROCESS_TERMINATE = 0x0001,
                    PROCESS_VM_OPERATION = 0x0008,
                    PROCESS_VM_READ = 0x0010,
                    PROCESS_VM_WRITE = 0x0020,
                    SYNCHRONIZE = 0x00100000
                }

                [Flags]
                public enum FileAccessFlags : UInt32
                {
                    DELETE = 0x10000,
                    FILE_READ_DATA = 0x1,
                    FILE_READ_ATTRIBUTES = 0x80,
                    FILE_READ_EA = 0x8,
                    READ_CONTROL = 0x20000,
                    FILE_WRITE_DATA = 0x2,
                    FILE_WRITE_ATTRIBUTES = 0x100,
                    FILE_WRITE_EA = 0x10,
                    FILE_APPEND_DATA = 0x4,
                    WRITE_DAC = 0x40000,
                    WRITE_OWNER = 0x80000,
                    SYNCHRONIZE = 0x100000,
                    FILE_EXECUTE = 0x20
                }

                [Flags]
                public enum FileShareFlags : UInt32
                {
                    FILE_SHARE_NONE = 0x0,
                    FILE_SHARE_READ = 0x1,
                    FILE_SHARE_WRITE = 0x2,
                    FILE_SHARE_DELETE = 0x4
                }

                [Flags]
                public enum FileOpenFlags : UInt32
                {
                    FILE_DIRECTORY_FILE = 0x1,
                    FILE_WRITE_THROUGH = 0x2,
                    FILE_SEQUENTIAL_ONLY = 0x4,
                    FILE_NO_INTERMEDIATE_BUFFERING = 0x8,
                    FILE_SYNCHRONOUS_IO_ALERT = 0x10,
                    FILE_SYNCHRONOUS_IO_NONALERT = 0x20,
                    FILE_NON_DIRECTORY_FILE = 0x40,
                    FILE_CREATE_TREE_CONNECTION = 0x80,
                    FILE_COMPLETE_IF_OPLOCKED = 0x100,
                    FILE_NO_EA_KNOWLEDGE = 0x200,
                    FILE_OPEN_FOR_RECOVERY = 0x400,
                    FILE_RANDOM_ACCESS = 0x800,
                    FILE_DELETE_ON_CLOSE = 0x1000,
                    FILE_OPEN_BY_FILE_ID = 0x2000,
                    FILE_OPEN_FOR_BACKUP_INTENT = 0x4000,
                    FILE_NO_COMPRESSION = 0x8000
                }

                [Flags]
                public enum StandardRights : uint
                {
                    Delete = 0x00010000,
                    ReadControl = 0x00020000,
                    WriteDac = 0x00040000,
                    WriteOwner = 0x00080000,
                    Synchronize = 0x00100000,
                    Required = 0x000f0000,
                    Read = ReadControl,
                    Write = ReadControl,
                    Execute = ReadControl,
                    All = 0x001f0000,

                    SpecificRightsAll = 0x0000ffff,
                    AccessSystemSecurity = 0x01000000,
                    MaximumAllowed = 0x02000000,
                    GenericRead = 0x80000000,
                    GenericWrite = 0x40000000,
                    GenericExecute = 0x20000000,
                    GenericAll = 0x10000000
                }

                [Flags]
                public enum ThreadAccess : uint
                {
                    Terminate = 0x0001,
                    SuspendResume = 0x0002,
                    Alert = 0x0004,
                    GetContext = 0x0008,
                    SetContext = 0x0010,
                    SetInformation = 0x0020,
                    QueryInformation = 0x0040,
                    SetThreadToken = 0x0080,
                    Impersonate = 0x0100,
                    DirectImpersonation = 0x0200,
                    SetLimitedInformation = 0x0400,
                    QueryLimitedInformation = 0x0800,
                    All = StandardRights.Required | StandardRights.Synchronize | 0x3ff
                }

                [Flags]
                public enum AllocationType : uint
                {
                    NULL = 0x0,
                    Commit = 0x1000,
                    Reserve = 0x2000,
                    Decommit = 0x4000,
                    Release = 0x8000,
                    Reset = 0x80000,
                    Physical = 0x400000,
                    TopDown = 0x100000,
                    WriteWatch = 0x200000,
                    ResetUndo = 0x1000000,
                    LargePages = 0x20000000
                }

                [Flags]
                public enum MemoryProtection : uint
                {
                    Execute = 0x10,
                    ExecuteRead = 0x20,
                    ExecuteReadWrite = 0x40,
                    ExecuteWriteCopy = 0x80,
                    NoAccess = 0x01,
                    ReadOnly = 0x02,
                    ReadWrite = 0x04,
                    WriteCopy = 0x08,
                    GuardModifierflag = 0x100,
                    NoCacheModifierflag = 0x200,
                    WriteCombineModifierflag = 0x400
                }

                [Flags]
                public enum PipeOpenModeFlags : uint
                {
                    PIPE_ACCESS_DUPLEX = 0x00000003,
                    PIPE_ACCESS_INBOUND = 0x00000001,
                    PIPE_ACCESS_OUTBOUND = 0x00000002,
                    FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000,
                    FILE_FLAG_WRITE_THROUGH = 0x80000000,
                    FILE_FLAG_OVERLAPPED = 0x40000000,
                    WRITE_DAC = (uint)0x00040000L,
                    WRITE_OWNER = (uint)0x00080000L,
                    ACCESS_SYSTEM_SECURITY = (uint)0x01000000L
                }

                [Flags]
                public enum PipeModeFlags : uint
                {
                    //One of the following type modes can be specified. The same type mode must be specified for each instance of the pipe.
                    PIPE_TYPE_BYTE = 0x00000000,
                    PIPE_TYPE_MESSAGE = 0x00000004,
                    //One of the following read modes can be specified. Different instances of the same pipe can specify different read modes
                    PIPE_READMODE_BYTE = 0x00000000,
                    PIPE_READMODE_MESSAGE = 0x00000002,
                    //One of the following wait modes can be specified. Different instances of the same pipe can specify different wait modes.
                    PIPE_WAIT = 0x00000000,
                    PIPE_NOWAIT = 0x00000001,
                    //One of the following remote-client modes can be specified. Different instances of the same pipe can specify different remote-client modes.
                    PIPE_ACCEPT_REMOTE_CLIENTS = 0x00000000,
                    PIPE_REJECT_REMOTE_CLIENTS = 0x00000008
                }

                public enum PSS_CAPTURE_FLAGS : uint
                {
                    PSS_CAPTURE_NONE = 0x00000000,
                    PSS_CAPTURE_VA_CLONE = 0x00000001,
                    PSS_CAPTURE_RESERVED_00000002 = 0x00000002,
                    PSS_CAPTURE_HANDLES = 0x00000004,
                    PSS_CAPTURE_HANDLE_NAME_INFORMATION = 0x00000008,
                    PSS_CAPTURE_HANDLE_BASIC_INFORMATION = 0x00000010,
                    PSS_CAPTURE_HANDLE_TYPE_SPECIFIC_INFORMATION = 0x00000020,
                    PSS_CAPTURE_HANDLE_TRACE = 0x00000040,
                    PSS_CAPTURE_THREADS = 0x00000080,
                    PSS_CAPTURE_THREAD_CONTEXT = 0x00000100,
                    PSS_CAPTURE_THREAD_CONTEXT_EXTENDED = 0x00000200,
                    PSS_CAPTURE_RESERVED_00000400 = 0x00000400,
                    PSS_CAPTURE_VA_SPACE = 0x00000800,
                    PSS_CAPTURE_VA_SPACE_SECTION_INFORMATION = 0x00001000,
                    PSS_CREATE_BREAKAWAY_OPTIONAL = 0x04000000,
                    PSS_CREATE_BREAKAWAY = 0x08000000,
                    PSS_CREATE_FORCE_BREAKAWAY = 0x10000000,
                    PSS_CREATE_USE_VM_ALLOCATIONS = 0x20000000,
                    PSS_CREATE_MEASURE_PERFORMANCE = 0x40000000,
                    PSS_CREATE_RELEASE_SECTION = 0x80000000
                }

                public enum PSS_QUERY_INFORMATION_CLASS
                {
                    PSS_QUERY_PROCESS_INFORMATION = 0,
                    PSS_QUERY_VA_CLONE_INFORMATION = 1,
                    PSS_QUERY_AUXILIARY_PAGES_INFORMATION = 2,
                    PSS_QUERY_VA_SPACE_INFORMATION = 3,
                    PSS_QUERY_HANDLE_INFORMATION = 4,
                    PSS_QUERY_THREAD_INFORMATION = 5,
                    PSS_QUERY_HANDLE_TRACE_INFORMATION = 6,
                    PSS_QUERY_PERFORMANCE_COUNTERS = 7
                }

                [Flags]
                public enum EFileAccess : uint
                {
                    //
                    // Standart Section
                    //
                    AccessSystemSecurity = 0x1000000,   // AccessSystemAcl access type
                    MaximumAllowed = 0x2000000,     // MaximumAllowed access type
                    Delete = 0x10000,
                    ReadControl = 0x20000,
                    WriteDAC = 0x40000,
                    WriteOwner = 0x80000,
                    Synchronize = 0x100000,

                    StandardRightsRequired = 0xF0000,
                    StandardRightsRead = ReadControl,
                    StandardRightsWrite = ReadControl,
                    StandardRightsExecute = ReadControl,
                    StandardRightsAll = 0x1F0000,
                    SpecificRightsAll = 0xFFFF,

                    FILE_READ_DATA = 0x0001,        // file & pipe
                    FILE_LIST_DIRECTORY = 0x0001,       // directory
                    FILE_WRITE_DATA = 0x0002,       // file & pipe
                    FILE_ADD_FILE = 0x0002,         // directory
                    FILE_APPEND_DATA = 0x0004,      // file
                    FILE_ADD_SUBDIRECTORY = 0x0004,     // directory
                    FILE_CREATE_PIPE_INSTANCE = 0x0004, // named pipe
                    FILE_READ_EA = 0x0008,          // file & directory
                    FILE_WRITE_EA = 0x0010,         // file & directory
                    FILE_EXECUTE = 0x0020,          // file
                    FILE_TRAVERSE = 0x0020,         // directory
                    FILE_DELETE_CHILD = 0x0040,     // directory
                    FILE_READ_ATTRIBUTES = 0x0080,      // all
                    FILE_WRITE_ATTRIBUTES = 0x0100,     // all

                    //
                    // Generic Section
                    //

                    GenericRead = 0x80000000,
                    GenericWrite = 0x40000000,
                    GenericExecute = 0x20000000,
                    GenericAll = 0x10000000,

                    SPECIFIC_RIGHTS_ALL = 0x00FFFF,
                    FILE_ALL_ACCESS =
                    StandardRightsRequired |
                    Synchronize |
                    0x1FF,

                    FILE_GENERIC_READ =
                    StandardRightsRead |
                    FILE_READ_DATA |
                    FILE_READ_ATTRIBUTES |
                    FILE_READ_EA |
                    Synchronize,

                    FILE_GENERIC_WRITE =
                    StandardRightsWrite |
                    FILE_WRITE_DATA |
                    FILE_WRITE_ATTRIBUTES |
                    FILE_WRITE_EA |
                    FILE_APPEND_DATA |
                    Synchronize,

                    FILE_GENERIC_EXECUTE =
                    StandardRightsExecute |
                      FILE_READ_ATTRIBUTES |
                      FILE_EXECUTE |
                      Synchronize
                }

                [Flags]
                public enum EFileShare : uint
                {
                    /// <summary>
                    ///
                    /// </summary>
                    None = 0x00000000,
                    /// <summary>
                    /// Enables subsequent open operations on an object to request read access.
                    /// Otherwise, other processes cannot open the object if they request read access.
                    /// If this flag is not specified, but the object has been opened for read access, the function fails.
                    /// </summary>
                    Read = 0x00000001,
                    /// <summary>
                    /// Enables subsequent open operations on an object to request write access.
                    /// Otherwise, other processes cannot open the object if they request write access.
                    /// If this flag is not specified, but the object has been opened for write access, the function fails.
                    /// </summary>
                    Write = 0x00000002,
                    /// <summary>
                    /// Enables subsequent open operations on an object to request delete access.
                    /// Otherwise, other processes cannot open the object if they request delete access.
                    /// If this flag is not specified, but the object has been opened for delete access, the function fails.
                    /// </summary>
                    Delete = 0x00000004
                }

                public enum ECreationDisposition : uint
                {
                    /// <summary>
                    /// Creates a new file. The function fails if a specified file exists.
                    /// </summary>
                    New = 1,
                    /// <summary>
                    /// Creates a new file, always.
                    /// If a file exists, the function overwrites the file, clears the existing attributes, combines the specified file attributes,
                    /// and flags with FILE_ATTRIBUTE_ARCHIVE, but does not set the security descriptor that the SECURITY_ATTRIBUTES structure specifies.
                    /// </summary>
                    CreateAlways = 2,
                    /// <summary>
                    /// Opens a file. The function fails if the file does not exist.
                    /// </summary>
                    OpenExisting = 3,
                    /// <summary>
                    /// Opens a file, always.
                    /// If a file does not exist, the function creates a file as if dwCreationDisposition is CREATE_NEW.
                    /// </summary>
                    OpenAlways = 4,
                    /// <summary>
                    /// Opens a file and truncates it so that its size is 0 (zero) bytes. The function fails if the file does not exist.
                    /// The calling process must open the file with the GENERIC_WRITE access right.
                    /// </summary>
                    TruncateExisting = 5
                }

                [Flags]
                public enum EFileAttributes : uint
                {
                    Readonly = 0x00000001,
                    Hidden = 0x00000002,
                    System = 0x00000004,
                    Directory = 0x00000010,
                    Archive = 0x00000020,
                    Device = 0x00000040,
                    Normal = 0x00000080,
                    Temporary = 0x00000100,
                    SparseFile = 0x00000200,
                    ReparsePoint = 0x00000400,
                    Compressed = 0x00000800,
                    Offline = 0x00001000,
                    NotContentIndexed = 0x00002000,
                    Encrypted = 0x00004000,
                    Write_Through = 0x80000000,
                    Overlapped = 0x40000000,
                    NoBuffering = 0x20000000,
                    RandomAccess = 0x10000000,
                    SequentialScan = 0x08000000,
                    DeleteOnClose = 0x04000000,
                    BackupSemantics = 0x02000000,
                    PosixSemantics = 0x01000000,
                    OpenReparsePoint = 0x00200000,
                    OpenNoRecall = 0x00100000,
                    FirstPipeInstance = 0x00080000
                }

                [Flags]
                public enum HANDLE_FLAGS : uint
                {
                    None = 0,
                    INHERIT = 1,
                    PROTECT_FROM_CLOSE = 2
                }
                //set the consts for the handle of stdin, stdout, stderr
                public const int STD_INPUT_HANDLE = -10;
                public const int STD_OUTPUT_HANDLE = -11;
                public const int STD_ERROR_HANDLE = -12;

                public const int PROC_THREAD_ATTRIBUTE_PARENT_PROCESS = 0x00020000;
                public const int PROC_THREAD_ATTRIBUTE_MITIGATION_POLICY = 0x00020007;

                public const int STARTF_USESTDHANDLES = 0x00000100;
                public const int STARTF_USESHOWWINDOW = 0x00000001;
                public const short SW_HIDE = 0x0000;

                public const long BLOCK_NON_MICROSOFT_BINARIES_ALWAYS_ON = 0x100000000000;

            }

            public static class User32
            {
                public static int WH_KEYBOARD_LL { get; } = 13;
                public static int WM_KEYDOWN { get; } = 0x0100;

                public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
            }

            public static class Netapi32
            {
                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public struct LOCALGROUP_USERS_INFO_0
                {
                    [MarshalAs(UnmanagedType.LPWStr)] internal string name;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct LOCALGROUP_USERS_INFO_1
                {
                    [MarshalAs(UnmanagedType.LPWStr)] public string name;
                    [MarshalAs(UnmanagedType.LPWStr)] public string comment;
                }

                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public struct LOCALGROUP_MEMBERS_INFO_2
                {
                    public IntPtr lgrmi2_sid;
                    public int lgrmi2_sidusage;
                    [MarshalAs(UnmanagedType.LPWStr)] public string lgrmi2_domainandname;
                }

                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public struct WKSTA_USER_INFO_1
                {
                    public string wkui1_username;
                    public string wkui1_logon_domain;
                    public string wkui1_oth_domains;
                    public string wkui1_logon_server;
                }

                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public struct SESSION_INFO_10
                {
                    public string sesi10_cname;
                    public string sesi10_username;
                    public int sesi10_time;
                    public int sesi10_idle_time;
                }

                public enum SID_NAME_USE : UInt16
                {
                    SidTypeUser = 1,
                    SidTypeGroup = 2,
                    SidTypeDomain = 3,
                    SidTypeAlias = 4,
                    SidTypeWellKnownGroup = 5,
                    SidTypeDeletedAccount = 6,
                    SidTypeInvalid = 7,
                    SidTypeUnknown = 8,
                    SidTypeComputer = 9
                }

                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public struct SHARE_INFO_1
                {
                    public string shi1_netname;
                    public uint shi1_type;
                    public string shi1_remark;

                    public SHARE_INFO_1(string netname, uint type, string remark)
                    {
                        this.shi1_netname = netname;
                        this.shi1_type = type;
                        this.shi1_remark = remark;
                    }
                }
            }

            public static class Advapi32
            {

                // http://www.pinvoke.net/default.aspx/advapi32.openprocesstoken
                public const UInt32 STANDARD_RIGHTS_REQUIRED = 0x000F0000;
                public const UInt32 STANDARD_RIGHTS_READ = 0x00020000;
                public const UInt32 TOKEN_ASSIGN_PRIMARY = 0x0001;
                public const UInt32 TOKEN_DUPLICATE = 0x0002;
                public const UInt32 TOKEN_IMPERSONATE = 0x0004;
                public const UInt32 TOKEN_QUERY = 0x0008;
                public const UInt32 TOKEN_QUERY_SOURCE = 0x0010;
                public const UInt32 TOKEN_ADJUST_PRIVILEGES = 0x0020;
                public const UInt32 TOKEN_ADJUST_GROUPS = 0x0040;
                public const UInt32 TOKEN_ADJUST_DEFAULT = 0x0080;
                public const UInt32 TOKEN_ADJUST_SESSIONID = 0x0100;
                public const UInt32 TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
                public const UInt32 TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
                    TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
                    TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
                    TOKEN_ADJUST_SESSIONID);
                public const UInt32 TOKEN_ALT = (TOKEN_ASSIGN_PRIMARY | TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms682434(v=vs.85).aspx
                [Flags]
                public enum CREATION_FLAGS : uint
                {
                    NONE = 0x00000000,
                    DEBUG_PROCESS = 0x00000001,
                    DEBUG_ONLY_THIS_PROCESS = 0x00000002,
                    CREATE_SUSPENDED = 0x00000004,
                    DETACHED_PROCESS = 0x00000008,
                    CREATE_NEW_CONSOLE = 0x00000010,
                    NORMAL_PRIORITY_CLASS = 0x00000020,
                    IDLE_PRIORITY_CLASS = 0x00000040,
                    HIGH_PRIORITY_CLASS = 0x00000080,
                    REALTIME_PRIORITY_CLASS = 0x00000100,
                    CREATE_NEW_PROCESS_GROUP = 0x00000200,
                    CREATE_UNICODE_ENVIRONMENT = 0x00000400,
                    CREATE_SEPARATE_WOW_VDM = 0x00000800,
                    CREATE_SHARED_WOW_VDM = 0x00001000,
                    CREATE_FORCEDOS = 0x00002000,
                    BELOW_NORMAL_PRIORITY_CLASS = 0x00004000,
                    ABOVE_NORMAL_PRIORITY_CLASS = 0x00008000,
                    INHERIT_PARENT_AFFINITY = 0x00010000,
                    INHERIT_CALLER_PRIORITY = 0x00020000,
                    CREATE_PROTECTED_PROCESS = 0x00040000,
                    EXTENDED_STARTUPINFO_PRESENT = 0x00080000,
                    PROCESS_MODE_BACKGROUND_BEGIN = 0x00100000,
                    PROCESS_MODE_BACKGROUND_END = 0x00200000,
                    CREATE_BREAKAWAY_FROM_JOB = 0x01000000,
                    CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
                    CREATE_DEFAULT_ERROR_MODE = 0x04000000,
                    CREATE_NO_WINDOW = 0x08000000,
                    PROFILE_USER = 0x10000000,
                    PROFILE_KERNEL = 0x20000000,
                    PROFILE_SERVER = 0x40000000,
                    CREATE_IGNORE_SYSTEM_DEFAULT = 0x80000000
                }

                [Flags]
                public enum LOGON_FLAGS
                {
                    NONE = 0x00000000,
                    LOGON_WITH_PROFILE = 0x00000001,
                    LOGON_NETCREDENTIALS_ONLY = 0x00000002
                }

                public enum LOGON_TYPE
                {
                    LOGON32_LOGON_INTERACTIVE = 2,
                    LOGON32_LOGON_NETWORK,
                    LOGON32_LOGON_BATCH,
                    LOGON32_LOGON_SERVICE,
                    LOGON32_LOGON_UNLOCK = 7,
                    LOGON32_LOGON_NETWORK_CLEARTEXT,
                    LOGON32_LOGON_NEW_CREDENTIALS
                }

                public enum LOGON_PROVIDER
                {
                    LOGON32_PROVIDER_DEFAULT,
                    LOGON32_PROVIDER_WINNT35,
                    LOGON32_PROVIDER_WINNT40,
                    LOGON32_PROVIDER_WINNT50
                }

                [Flags]
                public enum SCM_ACCESS : uint
                {
                    SC_MANAGER_CONNECT = 0x00001,
                    SC_MANAGER_CREATE_SERVICE = 0x00002,
                    SC_MANAGER_ENUMERATE_SERVICE = 0x00004,
                    SC_MANAGER_LOCK = 0x00008,
                    SC_MANAGER_QUERY_LOCK_STATUS = 0x00010,
                    SC_MANAGER_MODIFY_BOOT_CONFIG = 0x00020,

                    SC_MANAGER_ALL_ACCESS = ACCESS_MASK.STANDARD_RIGHTS_REQUIRED |
                        SC_MANAGER_CONNECT |
                        SC_MANAGER_CREATE_SERVICE |
                        SC_MANAGER_ENUMERATE_SERVICE |
                        SC_MANAGER_LOCK |
                        SC_MANAGER_QUERY_LOCK_STATUS |
                        SC_MANAGER_MODIFY_BOOT_CONFIG,

                    GENERIC_READ = ACCESS_MASK.STANDARD_RIGHTS_READ |
                        SC_MANAGER_ENUMERATE_SERVICE |
                        SC_MANAGER_QUERY_LOCK_STATUS,

                    GENERIC_WRITE = ACCESS_MASK.STANDARD_RIGHTS_WRITE |
                        SC_MANAGER_CREATE_SERVICE |
                        SC_MANAGER_MODIFY_BOOT_CONFIG,

                    GENERIC_EXECUTE = ACCESS_MASK.STANDARD_RIGHTS_EXECUTE |
                        SC_MANAGER_CONNECT | SC_MANAGER_LOCK,

                    GENERIC_ALL = SC_MANAGER_ALL_ACCESS,
                }

                [Flags]
                public enum ACCESS_MASK : uint
                {
                    DELETE = 0x00010000,
                    READ_CONTROL = 0x00020000,
                    WRITE_DAC = 0x00040000,
                    WRITE_OWNER = 0x00080000,
                    SYNCHRONIZE = 0x00100000,
                    STANDARD_RIGHTS_REQUIRED = 0x000F0000,
                    STANDARD_RIGHTS_READ = 0x00020000,
                    STANDARD_RIGHTS_WRITE = 0x00020000,
                    STANDARD_RIGHTS_EXECUTE = 0x00020000,
                    STANDARD_RIGHTS_ALL = 0x001F0000,
                    SPECIFIC_RIGHTS_ALL = 0x0000FFFF,
                    ACCESS_SYSTEM_SECURITY = 0x01000000,
                    MAXIMUM_ALLOWED = 0x02000000,
                    GENERIC_READ = 0x80000000,
                    GENERIC_WRITE = 0x40000000,
                    GENERIC_EXECUTE = 0x20000000,
                    GENERIC_ALL = 0x10000000,
                    DESKTOP_READOBJECTS = 0x00000001,
                    DESKTOP_CREATEWINDOW = 0x00000002,
                    DESKTOP_CREATEMENU = 0x00000004,
                    DESKTOP_HOOKCONTROL = 0x00000008,
                    DESKTOP_JOURNALRECORD = 0x00000010,
                    DESKTOP_JOURNALPLAYBACK = 0x00000020,
                    DESKTOP_ENUMERATE = 0x00000040,
                    DESKTOP_WRITEOBJECTS = 0x00000080,
                    DESKTOP_SWITCHDESKTOP = 0x00000100,
                    WINSTA_ENUMDESKTOPS = 0x00000001,
                    WINSTA_READATTRIBUTES = 0x00000002,
                    WINSTA_ACCESSCLIPBOARD = 0x00000004,
                    WINSTA_CREATEDESKTOP = 0x00000008,
                    WINSTA_WRITEATTRIBUTES = 0x00000010,
                    WINSTA_ACCESSGLOBALATOMS = 0x00000020,
                    WINSTA_EXITWINDOWS = 0x00000040,
                    WINSTA_ENUMERATE = 0x00000100,
                    WINSTA_READSCREEN = 0x00000200,
                    WINSTA_ALL_ACCESS = 0x0000037F
                }

                [Flags]
                public enum SERVICE_ACCESS : uint
                {
                    SERVICE_QUERY_CONFIG = 0x00001,
                    SERVICE_CHANGE_CONFIG = 0x00002,
                    SERVICE_QUERY_STATUS = 0x00004,
                    SERVICE_ENUMERATE_DEPENDENTS = 0x00008,
                    SERVICE_START = 0x00010,
                    SERVICE_STOP = 0x00020,
                    SERVICE_PAUSE_CONTINUE = 0x00040,
                    SERVICE_INTERROGATE = 0x00080,
                    SERVICE_USER_DEFINED_CONTROL = 0x00100,

                    SERVICE_ALL_ACCESS = (ACCESS_MASK.STANDARD_RIGHTS_REQUIRED |
                        SERVICE_QUERY_CONFIG |
                        SERVICE_CHANGE_CONFIG |
                        SERVICE_QUERY_STATUS |
                        SERVICE_ENUMERATE_DEPENDENTS |
                        SERVICE_START |
                        SERVICE_STOP |
                        SERVICE_PAUSE_CONTINUE |
                        SERVICE_INTERROGATE |
                        SERVICE_USER_DEFINED_CONTROL),

                    GENERIC_READ = ACCESS_MASK.STANDARD_RIGHTS_READ |
                        SERVICE_QUERY_CONFIG |
                        SERVICE_QUERY_STATUS |
                        SERVICE_INTERROGATE |
                        SERVICE_ENUMERATE_DEPENDENTS,

                    GENERIC_WRITE = ACCESS_MASK.STANDARD_RIGHTS_WRITE |
                        SERVICE_CHANGE_CONFIG,

                    GENERIC_EXECUTE = ACCESS_MASK.STANDARD_RIGHTS_EXECUTE |
                        SERVICE_START |
                        SERVICE_STOP |
                        SERVICE_PAUSE_CONTINUE |
                        SERVICE_USER_DEFINED_CONTROL,

                    ACCESS_SYSTEM_SECURITY = ACCESS_MASK.ACCESS_SYSTEM_SECURITY,
                    DELETE = ACCESS_MASK.DELETE,
                    READ_CONTROL = ACCESS_MASK.READ_CONTROL,
                    WRITE_DAC = ACCESS_MASK.WRITE_DAC,
                    WRITE_OWNER = ACCESS_MASK.WRITE_OWNER,
                }

                [Flags]
                public enum SERVICE_TYPE : uint
                {
                    SERVICE_KERNEL_DRIVER = 0x00000001,
                    SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,
                    SERVICE_WIN32_OWN_PROCESS = 0x00000010,
                    SERVICE_WIN32_SHARE_PROCESS = 0x00000020,
                    SERVICE_INTERACTIVE_PROCESS = 0x00000100,
                }

                public enum SERVICE_START : uint
                {
                    SERVICE_BOOT_START = 0x00000000,
                    SERVICE_SYSTEM_START = 0x00000001,
                    SERVICE_AUTO_START = 0x00000002,
                    SERVICE_DEMAND_START = 0x00000003,
                    SERVICE_DISABLED = 0x00000004,
                }

                public enum SERVICE_ERROR
                {
                    SERVICE_ERROR_IGNORE = 0x00000000,
                    SERVICE_ERROR_NORMAL = 0x00000001,
                    SERVICE_ERROR_SEVERE = 0x00000002,
                    SERVICE_ERROR_CRITICAL = 0x00000003,
                }

                public struct TokenEntry
                {
                    public IntPtr hToken;
                    public WindowsIdentity winId;
                    public int pid;
                    public string username;
                }
            }

            public static class Dbghelp
            {
                public enum MINIDUMP_TYPE
                {
                    MiniDumpNormal = 0x00000000,
                    MiniDumpWithDataSegs = 0x00000001,
                    MiniDumpWithFullMemory = 0x00000002,
                    MiniDumpWithHandleData = 0x00000004,
                    MiniDumpFilterMemory = 0x00000008,
                    MiniDumpScanMemory = 0x00000010,
                    MiniDumpWithUnloadedModules = 0x00000020,
                    MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
                    MiniDumpFilterModulePaths = 0x00000080,
                    MiniDumpWithProcessThreadData = 0x00000100,
                    MiniDumpWithPrivateReadWriteMemory = 0x00000200,
                    MiniDumpWithoutOptionalData = 0x00000400,
                    MiniDumpWithFullMemoryInfo = 0x00000800,
                    MiniDumpWithThreadInfo = 0x00001000,
                    MiniDumpWithCodeSegs = 0x00002000,
                    MiniDumpWithoutAuxiliaryState = 0x00004000,
                    MiniDumpWithFullAuxiliaryState = 0x00008000,
                    MiniDumpWithPrivateWriteCopyMemory = 0x00010000,
                    MiniDumpIgnoreInaccessibleMemory = 0x00020000,
                    MiniDumpWithTokenInformation = 0x00040000,
                    MiniDumpWithModuleHeaders = 0x00080000,
                    MiniDumpFilterTriage = 0x00100000,
                    MiniDumpValidTypeFlags = 0x001fffff
                }

                public enum MINIDUMP_CALLBACK_TYPE : uint
                {
                    ModuleCallback,
                    ThreadCallback,
                    ThreadExCallback,
                    IncludeThreadCallback,
                    IncludeModuleCallback,
                    MemoryCallback,
                    CancelCallback,
                    WriteKernelMinidumpCallback,
                    KernelMinidumpStatusCallback,
                    RemoveMemoryCallback,
                    IncludeVmRegionCallback,
                    IoStartCallback,
                    IoWriteAllCallback,
                    IoFinishCallback,
                    ReadMemoryFailureCallback,
                    SecondaryFlagsCallback,
                    IsProcessSnapshotCallback,
                    VmStartCallback,
                    VmQueryCallback,
                    VmPreReadCallback,
                }

                public struct MINIDUMP_CALLBACK_INFORMATION
                {
                    public MINIDUMP_CALLBACK_ROUTINE CallbackRoutine;
                    public IntPtr CallbackParam;
                }

                [StructLayout(LayoutKind.Explicit, Pack = 4)]
                public struct MINIDUMP_CALLBACK_OUTPUT
                {
                    [FieldOffset(0)]
                    public int Status;
                }

                [StructLayout(LayoutKind.Explicit)]
                public struct MINIDUMP_CALLBACK_INPUT
                {
                    [FieldOffset(0)]
                    public uint ProcessId;
                    [FieldOffset(4)]
                    public IntPtr ProcessHandle;
                    [FieldOffset(12)]
                    public MINIDUMP_CALLBACK_TYPE CallbackType;
                    [FieldOffset(16)]
                    public int Status;
                }

                [UnmanagedFunctionPointer(CallingConvention.StdCall)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public delegate bool MINIDUMP_CALLBACK_ROUTINE(
                    [In] IntPtr CallbackParam,
                    [In] ref MINIDUMP_CALLBACK_INPUT CallbackInput,
                    [In, Out] ref MINIDUMP_CALLBACK_OUTPUT CallbackOutput
                 );
            }

            public class WinBase
            {
                [StructLayout(LayoutKind.Sequential)]
                public struct _SYSTEM_INFO
                {
                    public UInt16 wProcessorArchitecture;
                    public UInt16 wReserved;
                    public UInt32 dwPageSize;
                    public IntPtr lpMinimumApplicationAddress;
                    public IntPtr lpMaximumApplicationAddress;
                    public IntPtr dwActiveProcessorMask;
                    public UInt32 dwNumberOfProcessors;
                    public UInt32 dwProcessorType;
                    public UInt32 dwAllocationGranularity;
                    public UInt16 wProcessorLevel;
                    public UInt16 wProcessorRevision;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _SECURITY_ATTRIBUTES
                {
                    public int nLength;
                    public IntPtr lpSecurityDescriptor;
                    public bool bInheritHandle;
                };
            }

            public class WinNT
            {
                public const UInt32 PAGE_NOACCESS = 0x01;
                public const UInt32 PAGE_READONLY = 0x02;
                public const UInt32 PAGE_READWRITE = 0x04;
                public const UInt32 PAGE_WRITECOPY = 0x08;
                public const UInt32 PAGE_EXECUTE = 0x10;
                public const UInt32 PAGE_EXECUTE_READ = 0x20;
                public const UInt32 PAGE_EXECUTE_READWRITE = 0x40;
                public const UInt32 PAGE_EXECUTE_WRITECOPY = 0x80;
                public const UInt32 PAGE_GUARD = 0x100;
                public const UInt32 PAGE_NOCACHE = 0x200;
                public const UInt32 PAGE_WRITECOMBINE = 0x400;
                public const UInt32 PAGE_TARGETS_INVALID = 0x40000000;
                public const UInt32 PAGE_TARGETS_NO_UPDATE = 0x40000000;

                public const UInt32 SEC_COMMIT = 0x08000000;
                public const UInt32 SEC_IMAGE = 0x1000000;
                public const UInt32 SEC_IMAGE_NO_EXECUTE = 0x11000000;
                public const UInt32 SEC_LARGE_PAGES = 0x80000000;
                public const UInt32 SEC_NOCACHE = 0x10000000;
                public const UInt32 SEC_RESERVE = 0x4000000;
                public const UInt32 SEC_WRITECOMBINE = 0x40000000;

                public const UInt32 SE_PRIVILEGE_ENABLED = 0x2;
                public const UInt32 SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x1;
                public const UInt32 SE_PRIVILEGE_REMOVED = 0x4;
                public const UInt32 SE_PRIVILEGE_USED_FOR_ACCESS = 0x3;

                public const UInt64 SE_GROUP_ENABLED = 0x00000004L;
                public const UInt64 SE_GROUP_ENABLED_BY_DEFAULT = 0x00000002L;
                public const UInt64 SE_GROUP_INTEGRITY = 0x00000020L;
                public const UInt32 SE_GROUP_INTEGRITY_32 = 0x00000020;
                public const UInt64 SE_GROUP_INTEGRITY_ENABLED = 0x00000040L;
                public const UInt64 SE_GROUP_LOGON_ID = 0xC0000000L;
                public const UInt64 SE_GROUP_MANDATORY = 0x00000001L;
                public const UInt64 SE_GROUP_OWNER = 0x00000008L;
                public const UInt64 SE_GROUP_RESOURCE = 0x20000000L;
                public const UInt64 SE_GROUP_USE_FOR_DENY_ONLY = 0x00000010L;

                public enum _SECURITY_IMPERSONATION_LEVEL
                {
                    SecurityAnonymous,
                    SecurityIdentification,
                    SecurityImpersonation,
                    SecurityDelegation
                }

                [Flags]
                public enum SECURITY_INFORMATION : uint
                {
                    OWNER_SECURITY_INFORMATION = 0x00000001,
                    GROUP_SECURITY_INFORMATION = 0x00000002,
                    DACL_SECURITY_INFORMATION = 0x00000004,
                    SACL_SECURITY_INFORMATION = 0x00000008,
                    LABEL_SECURITY_INFORMATION = 0x00000010,
                    ATTRIBUTE_SECURITY_INFORMATION = 0x00000020,
                    SCOPE_SECURITY_INFORMATION = 0x00000040,
                    PROCESS_TRUST_LABEL_SECURITY_INFORMATION = 0x00000080,
                    BACKUP_SECURITY_INFORMATION = 0x00010000,
                    PROTECTED_DACL_SECURITY_INFORMATION = 0x80000000,
                    PROTECTED_SACL_SECURITY_INFORMATION = 0x40000000,
                    UNPROTECTED_DACL_SECURITY_INFORMATION = 0x20000000,
                    UNPROTECTED_SACL_SECURITY_INFORMATION = 0x10000000
                }

                public enum TOKEN_TYPE
                {
                    TokenPrimary = 1,
                    TokenImpersonation
                }

                public enum _TOKEN_ELEVATION_TYPE
                {
                    TokenElevationTypeDefault = 1,
                    TokenElevationTypeFull,
                    TokenElevationTypeLimited
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _MEMORY_BASIC_INFORMATION32
                {
                    public UInt32 BaseAddress;
                    public UInt32 AllocationBase;
                    public UInt32 AllocationProtect;
                    public UInt32 RegionSize;
                    public UInt32 State;
                    public UInt32 Protect;
                    public UInt32 Type;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _MEMORY_BASIC_INFORMATION64
                {
                    public UInt64 BaseAddress;
                    public UInt64 AllocationBase;
                    public UInt32 AllocationProtect;
                    public UInt32 __alignment1;
                    public UInt64 RegionSize;
                    public UInt32 State;
                    public UInt32 Protect;
                    public UInt32 Type;
                    public UInt32 __alignment2;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _LUID_AND_ATTRIBUTES
                {
                    public _LUID Luid;
                    public UInt32 Attributes;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _LUID
                {
                    public UInt32 LowPart;
                    public Int32 HighPart;
                    public _LUID(UInt64 value)
                    {
                        LowPart = (UInt32)(value & 0xffffffffL);
                        HighPart = (Int32)(value >> 32);
                    }
                    public _LUID(_LUID value)
                    {
                        LowPart = value.LowPart;
                        HighPart = value.HighPart;
                    }
                    public _LUID(string value)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(value, @"^0x[0-9A-Fa-f]+$"))
                        {
                            // if the passed _LUID string is of form 0xABC123
                            UInt64 uintVal = Convert.ToUInt64(value, 16);
                            LowPart = (UInt32)(uintVal & 0xffffffffL);
                            HighPart = (Int32)(uintVal >> 32);
                        }
                        else if (System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+$"))
                        {
                            // if the passed _LUID string is a decimal form
                            UInt64 uintVal = UInt64.Parse(value);
                            LowPart = (UInt32)(uintVal & 0xffffffffL);
                            HighPart = (Int32)(uintVal >> 32);
                        }
                        else
                        {
                            System.ArgumentException argEx = new System.ArgumentException("Passed _LUID string value is not in a hex or decimal form", value);
                            throw argEx;
                        }
                    }
                    public static implicit operator ulong(_LUID luid)
                    {
                        // enable casting to a ulong
                        UInt64 Value = ((UInt64)luid.HighPart << 32);
                        return Value + luid.LowPart;
                    }
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _TOKEN_STATISTICS
                {
                    public _LUID TokenId;
                    public _LUID AuthenticationId;
                    public UInt64 ExpirationTime;
                    public TOKEN_TYPE TokenType;
                    public _SECURITY_IMPERSONATION_LEVEL ImpersonationLevel;
                    public UInt32 DynamicCharged;
                    public UInt32 DynamicAvailable;
                    public UInt32 GroupCount;
                    public UInt32 PrivilegeCount;
                    public _LUID ModifiedId;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _TOKEN_PRIVILEGES
                {
                    public UInt32 PrivilegeCount;
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
                    public _LUID_AND_ATTRIBUTES[] Privileges;
                }
                

                [StructLayout(LayoutKind.Sequential)]
                public struct TOKEN_GROUPS_AND_PRIVILEGES
                {
                    public uint SidCount;
                    public uint SidLength;
                    public IntPtr Sids;
                    public uint RestrictedSidCount;
                    public uint RestrictedSidLength;
                    public IntPtr RestrictedSids;
                    public uint PrivilegeCount;
                    public uint PrivilegeLength;
                    public IntPtr Privileges;
                    public _LUID AuthenticationID;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _TOKEN_MANDATORY_LABEL
                {
                    public _SID_AND_ATTRIBUTES Label;
                }

                public struct _SID
                {
                    public byte Revision;
                    public byte SubAuthorityCount;
                    public WinNT._SID_IDENTIFIER_AUTHORITY IdentifierAuthority;
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
                    public ulong[] SubAuthority;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _SID_IDENTIFIER_AUTHORITY
                {
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
                    public byte[] Value;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _SID_AND_ATTRIBUTES
                {
                    public IntPtr Sid;
                    public UInt32 Attributes;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _PRIVILEGE_SET
                {
                    public UInt32 PrivilegeCount;
                    public UInt32 Control;
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
                    public _LUID_AND_ATTRIBUTES[] Privilege;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _TOKEN_USER
                {
                    public _SID_AND_ATTRIBUTES User;
                }

                public enum _SID_NAME_USE
                {
                    SidTypeUser = 1,
                    SidTypeGroup,
                    SidTypeDomain,
                    SidTypeAlias,
                    SidTypeWellKnownGroup,
                    SidTypeDeletedAccount,
                    SidTypeInvalid,
                    SidTypeUnknown,
                    SidTypeComputer,
                    SidTypeLabel
                }

                public enum _TOKEN_INFORMATION_CLASS
                {
                    TokenUser = 1,
                    TokenGroups,
                    TokenPrivileges,
                    TokenOwner,
                    TokenPrimaryGroup,
                    TokenDefaultDacl,
                    TokenSource,
                    TokenType,
                    TokenImpersonationLevel,
                    TokenStatistics,
                    TokenRestrictedSids,
                    TokenSessionId,
                    TokenGroupsAndPrivileges,
                    TokenSessionReference,
                    TokenSandBoxInert,
                    TokenAuditPolicy,
                    TokenOrigin,
                    TokenElevationType,
                    TokenLinkedToken,
                    TokenElevation,
                    TokenHasRestrictions,
                    TokenAccessInformation,
                    TokenVirtualizationAllowed,
                    TokenVirtualizationEnabled,
                    TokenIntegrityLevel,
                    TokenUIAccess,
                    TokenMandatoryPolicy,
                    TokenLogonSid,
                    TokenIsAppContainer,
                    TokenCapabilities,
                    TokenAppContainerSid,
                    TokenAppContainerNumber,
                    TokenUserClaimAttributes,
                    TokenDeviceClaimAttributes,
                    TokenRestrictedUserClaimAttributes,
                    TokenRestrictedDeviceClaimAttributes,
                    TokenDeviceGroups,
                    TokenRestrictedDeviceGroups,
                    TokenSecurityAttributes,
                    TokenIsRestricted,
                    MaxTokenInfoClass
                }

                [Flags]
                public enum LuidAttributes : uint
                {
                    DISABLED = 0x00000000,
                    SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001,
                    SE_PRIVILEGE_ENABLED = 0x00000002,
                    SE_PRIVILEGE_REMOVED = 0x00000004,
                    SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000
                }



                // http://www.pinvoke.net/default.aspx/Enums.ACCESS_MASK
                [Flags]
                public enum ACCESS_MASK : uint
                {
                    DELETE = 0x00010000,
                    READ_CONTROL = 0x00020000,
                    WRITE_DAC = 0x00040000,
                    WRITE_OWNER = 0x00080000,
                    SYNCHRONIZE = 0x00100000,
                    STANDARD_RIGHTS_REQUIRED = 0x000F0000,
                    STANDARD_RIGHTS_READ = 0x00020000,
                    STANDARD_RIGHTS_WRITE = 0x00020000,
                    STANDARD_RIGHTS_EXECUTE = 0x00020000,
                    STANDARD_RIGHTS_ALL = 0x001F0000,
                    SPECIFIC_RIGHTS_ALL = 0x0000FFF,
                    ACCESS_SYSTEM_SECURITY = 0x01000000,
                    MAXIMUM_ALLOWED = 0x02000000,
                    GENERIC_READ = 0x80000000,
                    GENERIC_WRITE = 0x40000000,
                    GENERIC_EXECUTE = 0x20000000,
                    GENERIC_ALL = 0x10000000,
                    DESKTOP_READOBJECTS = 0x00000001,
                    DESKTOP_CREATEWINDOW = 0x00000002,
                    DESKTOP_CREATEMENU = 0x00000004,
                    DESKTOP_HOOKCONTROL = 0x00000008,
                    DESKTOP_JOURNALRECORD = 0x00000010,
                    DESKTOP_JOURNALPLAYBACK = 0x00000020,
                    DESKTOP_ENUMERATE = 0x00000040,
                    DESKTOP_WRITEOBJECTS = 0x00000080,
                    DESKTOP_SWITCHDESKTOP = 0x00000100,
                    WINSTA_ENUMDESKTOPS = 0x00000001,
                    WINSTA_READATTRIBUTES = 0x00000002,
                    WINSTA_ACCESSCLIPBOARD = 0x00000004,
                    WINSTA_CREATEDESKTOP = 0x00000008,
                    WINSTA_WRITEATTRIBUTES = 0x00000010,
                    WINSTA_ACCESSGLOBALATOMS = 0x00000020,
                    WINSTA_EXITWINDOWS = 0x00000040,
                    WINSTA_ENUMERATE = 0x00000100,
                    WINSTA_READSCREEN = 0x00000200,
                    WINSTA_ALL_ACCESS = 0x0000037F,

                    SECTION_ALL_ACCESS = 0x10000000,
                    SECTION_QUERY = 0x0001,
                    SECTION_MAP_WRITE = 0x0002,
                    SECTION_MAP_READ = 0x0004,
                    SECTION_MAP_EXECUTE = 0x0008,
                    SECTION_EXTEND_SIZE = 0x0010
                };
            }

            public class ProcessThreadsAPI
            {
                [Flags]
                internal enum STARTF : uint
                {
                    STARTF_USESHOWWINDOW = 0x00000001,
                    STARTF_USESIZE = 0x00000002,
                    STARTF_USEPOSITION = 0x00000004,
                    STARTF_USECOUNTCHARS = 0x00000008,
                    STARTF_USEFILLATTRIBUTE = 0x00000010,
                    STARTF_RUNFULLSCREEN = 0x00000020,
                    STARTF_FORCEONFEEDBACK = 0x00000040,
                    STARTF_FORCEOFFFEEDBACK = 0x00000080,
                    STARTF_USESTDHANDLES = 0x00000100,
                }

                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms686331(v=vs.85).aspx
                [StructLayout(LayoutKind.Sequential)]
                public struct _STARTUPINFO
                {
                    public Int32 cb;
                    public String lpReserved;
                    public String lpDesktop;
                    public String lpTitle;
                    public UInt32 dwX;
                    public UInt32 dwY;
                    public UInt32 dwXSize;
                    public UInt32 dwYSize;
                    public UInt32 dwXCountChars;
                    public UInt32 dwYCountChars;
                    public UInt32 dwFillAttribute;
                    public UInt32 dwFlags;
                    public UInt16 wShowWindow;
                    public UInt16 cbReserved2;
                    public IntPtr lpReserved2;
                    public IntPtr hStdInput;
                    public IntPtr hStdOutput;
                    public IntPtr hStdError;
                };

                //https://msdn.microsoft.com/en-us/library/windows/desktop/ms686331(v=vs.85).aspx
                [StructLayout(LayoutKind.Sequential)]
                public struct _STARTUPINFOEX
                {
                    public _STARTUPINFO StartupInfo;
                    public IntPtr lpAttributeList;
                };

                //https://msdn.microsoft.com/en-us/library/windows/desktop/ms684873(v=vs.85).aspx
                [StructLayout(LayoutKind.Sequential)]
                public struct _PROCESS_INFORMATION
                {
                    public IntPtr hProcess;
                    public IntPtr hThread;
                    public UInt32 dwProcessId;
                    public UInt32 dwThreadId;
                };
            }

            public class WinCred
            {
#pragma warning disable 0618
                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public struct _CREDENTIAL
                {
                    public CRED_FLAGS Flags;
                    public UInt32 Type;
                    public IntPtr TargetName;
                    public IntPtr Comment;
                    public FILETIME LastWritten;
                    public UInt32 CredentialBlobSize;
                    public UInt32 Persist;
                    public UInt32 AttributeCount;
                    public IntPtr Attributes;
                    public IntPtr TargetAlias;
                    public IntPtr UserName;
                }
#pragma warning restore 0618

                public enum CRED_FLAGS : uint
                {
                    NONE = 0x0,
                    PROMPT_NOW = 0x2,
                    USERNAME_TARGET = 0x4
                }

                public enum CRED_PERSIST : uint
                {
                    Session = 1,
                    LocalMachine,
                    Enterprise
                }

                public enum CRED_TYPE : uint
                {
                    Generic = 1,
                    DomainPassword,
                    DomainCertificate,
                    DomainVisiblePassword,
                    GenericCertificate,
                    DomainExtended,
                    Maximum,
                    MaximumEx = Maximum + 1000,
                }
            }

            public class Secur32
            {
                public struct _SECURITY_LOGON_SESSION_DATA
                {
                    public UInt32 Size;
                    public WinNT._LUID LoginID;
                    public _LSA_UNICODE_STRING Username;
                    public _LSA_UNICODE_STRING LoginDomain;
                    public _LSA_UNICODE_STRING AuthenticationPackage;
                    public UInt32 LogonType;
                    public UInt32 Session;
                    public IntPtr pSid;
                    public UInt64 LoginTime;
                    public _LSA_UNICODE_STRING LogonServer;
                    public _LSA_UNICODE_STRING DnsDomainName;
                    public _LSA_UNICODE_STRING Upn;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct _LSA_UNICODE_STRING
                {
                    public UInt16 Length;
                    public UInt16 MaximumLength;
                    public IntPtr Buffer;
                }
            }

            //this code just copy-paste from gist
            //orig class: rprn
            //some changed for MS-EFSR
            public class EfsrTiny
            {
                [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingFromStringBindingW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = false)]
                private static extern Int32 RpcBindingFromStringBinding(String bindingString, out IntPtr lpBinding);
                [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingSetAuthInfoW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = false)]
                private static extern Int32 RpcBindingSetAuthInfo(IntPtr lpBinding, string ServerPrincName, UInt32 AuthnLevel, UInt32 AuthnSvc, IntPtr AuthIdentity, UInt32 AuthzSvc);

                [DllImport("Rpcrt4.dll", EntryPoint = "NdrClientCall2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = false)]
                private static extern IntPtr NdrClientCall2x86(IntPtr pMIDL_STUB_DESC, IntPtr formatString, IntPtr args);

                [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingFree", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = false)]
                private static extern Int32 RpcBindingFree(ref IntPtr lpString);

                [DllImport("Rpcrt4.dll", EntryPoint = "RpcStringBindingComposeW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = false)]
                private static extern Int32 RpcStringBindingCompose(String ObjUuid, String ProtSeq, String NetworkAddr, String Endpoint, String Options, out IntPtr lpBindingString);

                [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingSetOption", CallingConvention = CallingConvention.StdCall, SetLastError = false)]
                private static extern Int32 RpcBindingSetOption(IntPtr Binding, UInt32 Option, IntPtr OptionValue);

                [DllImport("Rpcrt4.dll", EntryPoint = "NdrClientCall2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = false)]
                internal static extern IntPtr NdrClientCall2x64(IntPtr pMIDL_STUB_DESC, IntPtr formatString, IntPtr binding, string FileName);

                private static byte[] MIDL_ProcFormatStringx86 = new byte[] { 0x00, 0x00, 0x00, 0x48, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x0c, 0x00, 0x32, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x46, 0x02, 0x08, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0b, 0x01, 0x04, 0x00, 0x0c, 0x00, 0x70, 0x00, 0x08, 0x00, 0x08, 0x00 };

                private static byte[] MIDL_ProcFormatStringx64 = new byte[] { 0x00, 0x00, 0x00, 0x48, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x18, 0x00, 0x32, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x46, 0x02, 0x0a, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0b, 0x01, 0x08, 0x00, 0x0c, 0x00, 0x70, 0x00, 0x10, 0x00, 0x08, 0x00 };

                private static byte[] MIDL_TypeFormatStringx86 = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x11, 0x04, 0x02, 0x00, 0x30, 0xa0, 0x00, 0x00, 0x11, 0x08, 0x25, 0x5c, 0x00, 0x00 };

                private static byte[] MIDL_TypeFormatStringx64 = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x11, 0x04, 0x02, 0x00, 0x30, 0xa0, 0x00, 0x00, 0x11, 0x08, 0x25, 0x5c, 0x00, 0x00 };
                Guid interfaceId;
                public EfsrTiny(string pipe)
                {
                    IDictionary<string, string> bindingMapping = new Dictionary<string, string>()
                    {
                        {"lsarpc", "c681d488-d850-11d0-8c52-00c04fd90f7e"},
                        {"efsrpc", "df1941c5-fe89-4e79-bf10-463657acf44d"},
                        {"samr", "c681d488-d850-11d0-8c52-00c04fd90f7e"},
                        {"lsass", "c681d488-d850-11d0-8c52-00c04fd90f7e"},
                        {"netlogon", "c681d488-d850-11d0-8c52-00c04fd90f7e"}
                    };

                    interfaceId = new Guid(bindingMapping[pipe]);

                    pipe = String.Format("\\pipe\\{0}", pipe);
                    Console.WriteLine("[+] Pipe: " + pipe);
                    if (IntPtr.Size == 8)
                    {
                        InitializeStub(interfaceId, MIDL_ProcFormatStringx64, MIDL_TypeFormatStringx64, pipe, 1, 0);
                    }
                    else
                    {
                        InitializeStub(interfaceId, MIDL_ProcFormatStringx86, MIDL_TypeFormatStringx86, pipe, 1, 0);
                    }
                }

                ~EfsrTiny()
                {
                    freeStub();
                }
                public int EfsRpcEncryptFileSrv(string FileName)
                {
                    IntPtr result = IntPtr.Zero;
                    IntPtr pfn = Marshal.StringToHGlobalUni(FileName);

                    try
                    {
                        if (IntPtr.Size == 8)
                        {
                            result = NdrClientCall2x64(GetStubHandle(), GetProcStringHandle(2), Bind(Marshal.StringToHGlobalUni("localhost")), FileName);
                        }
                        else
                        {
                            result = CallNdrClientCall2x86(2, Bind(Marshal.StringToHGlobalUni("localhost")), pfn);
                        }
                    }
                    catch (SEHException)
                    {
                        int err = Marshal.GetExceptionCode();
                        Console.WriteLine("[-] EfsRpcEncryptFileSrv failed: " + err);
                        return err;
                    }
                    finally
                    {
                        if (pfn != IntPtr.Zero)
                            Marshal.FreeHGlobal(pfn);
                    }
                    return (int)result.ToInt64();
                }
                private byte[] MIDL_ProcFormatString;
                private byte[] MIDL_TypeFormatString;
                private GCHandle procString;
                private GCHandle formatString;
                private GCHandle stub;
                private GCHandle faultoffsets;
                private GCHandle clientinterface;
                private string PipeName;

                allocmemory AllocateMemoryDelegate = AllocateMemory;
                freememory FreeMemoryDelegate = FreeMemory;

                public UInt32 RPCTimeOut = 5000;

                protected void InitializeStub(Guid interfaceID, byte[] MIDL_ProcFormatString, byte[] MIDL_TypeFormatString, string pipe, ushort MajorVerson, ushort MinorVersion)
                {
                    this.MIDL_ProcFormatString = MIDL_ProcFormatString;
                    this.MIDL_TypeFormatString = MIDL_TypeFormatString;
                    PipeName = pipe;
                    procString = GCHandle.Alloc(this.MIDL_ProcFormatString, GCHandleType.Pinned);

                    RPC_CLIENT_INTERFACE clientinterfaceObject = new RPC_CLIENT_INTERFACE(interfaceID, MajorVerson, MinorVersion);

                    COMM_FAULT_OFFSETS commFaultOffset = new COMM_FAULT_OFFSETS();
                    commFaultOffset.CommOffset = -1;
                    commFaultOffset.FaultOffset = -1;
                    faultoffsets = GCHandle.Alloc(commFaultOffset, GCHandleType.Pinned);
                    clientinterface = GCHandle.Alloc(clientinterfaceObject, GCHandleType.Pinned);
                    formatString = GCHandle.Alloc(MIDL_TypeFormatString, GCHandleType.Pinned);

                    MIDL_STUB_DESC stubObject = new MIDL_STUB_DESC(formatString.AddrOfPinnedObject(),
                                                                    clientinterface.AddrOfPinnedObject(),
                                                                    Marshal.GetFunctionPointerForDelegate(AllocateMemoryDelegate),
                                                                    Marshal.GetFunctionPointerForDelegate(FreeMemoryDelegate));

                    stub = GCHandle.Alloc(stubObject, GCHandleType.Pinned);
                }


                protected void freeStub()
                {
                    procString.Free();
                    faultoffsets.Free();
                    clientinterface.Free();
                    formatString.Free();
                    stub.Free();
                }

                delegate IntPtr allocmemory(int size);

                protected static IntPtr AllocateMemory(int size)
                {
                    IntPtr memory = Marshal.AllocHGlobal(size);
                    return memory;
                }

                delegate void freememory(IntPtr memory);

                protected static void FreeMemory(IntPtr memory)
                {
                    Marshal.FreeHGlobal(memory);
                }


                protected IntPtr Bind(IntPtr IntPtrserver)
                {
                    string server = Marshal.PtrToStringUni(IntPtrserver);
                    IntPtr bindingstring = IntPtr.Zero;
                    IntPtr binding = IntPtr.Zero;
                    Int32 status;
                    status = RpcStringBindingCompose(interfaceId.ToString(), "ncacn_np", server, PipeName, null, out bindingstring);
                    if (status != 0)
                    {
                        Console.WriteLine("[-] RpcStringBindingCompose failed with status 0x" + status.ToString("x"));
                        return IntPtr.Zero;
                    }
                    status = RpcBindingFromStringBinding(Marshal.PtrToStringUni(bindingstring), out binding);
                    RpcBindingFree(ref bindingstring);
                    if (status != 0)
                    {
                        Console.WriteLine("[-] RpcBindingFromStringBinding failed with status 0x" + status.ToString("x"));
                        return IntPtr.Zero;
                    }

                    status = RpcBindingSetAuthInfo(binding, server, /* RPC_C_AUTHN_LEVEL_PKT_PRIVACY */ 6, /* RPC_C_AUTHN_GSS_NEGOTIATE */ 9, IntPtr.Zero, AuthzSvc: 16);
                    if (status != 0)
                    {
                        Console.WriteLine("[-] RpcBindingSetAuthInfo failed with status 0x" + status.ToString("x"));
                    }

                    status = RpcBindingSetOption(binding, 12, new IntPtr(RPCTimeOut));
                    if (status != 0)
                    {
                        Console.WriteLine("[-] RpcBindingSetOption failed with status 0x" + status.ToString("x"));
                    }
                    Console.WriteLine("[+] binding ok (handle=" + binding.ToString("x") + ")");
                    return binding;
                }

                protected IntPtr GetProcStringHandle(int offset)
                {
                    return Marshal.UnsafeAddrOfPinnedArrayElement(MIDL_ProcFormatString, offset);
                }

                protected IntPtr GetStubHandle()
                {
                    return stub.AddrOfPinnedObject();
                }
                protected IntPtr CallNdrClientCall2x86(int offset, params IntPtr[] args)
                {

                    GCHandle stackhandle = GCHandle.Alloc(args, GCHandleType.Pinned);
                    IntPtr result;
                    try
                    {
                        result = NdrClientCall2x86(GetStubHandle(), GetProcStringHandle(offset), stackhandle.AddrOfPinnedObject());
                    }
                    finally
                    {
                        stackhandle.Free();
                    }
                    return result;
                }


                [StructLayout(LayoutKind.Sequential)]
                struct COMM_FAULT_OFFSETS
                {
                    public short CommOffset;
                    public short FaultOffset;
                }

                [StructLayout(LayoutKind.Sequential)]
                struct RPC_VERSION
                {
                    public ushort MajorVersion;
                    public ushort MinorVersion;
                    public RPC_VERSION(ushort InterfaceVersionMajor, ushort InterfaceVersionMinor)
                    {
                        MajorVersion = InterfaceVersionMajor;
                        MinorVersion = InterfaceVersionMinor;
                    }
                }

                [StructLayout(LayoutKind.Sequential)]
                struct RPC_SYNTAX_IDENTIFIER
                {
                    public Guid SyntaxGUID;
                    public RPC_VERSION SyntaxVersion;
                }

                [StructLayout(LayoutKind.Sequential)]
                struct RPC_CLIENT_INTERFACE
                {
                    public uint Length;
                    public RPC_SYNTAX_IDENTIFIER InterfaceId;
                    public RPC_SYNTAX_IDENTIFIER TransferSyntax;
                    public IntPtr /*PRPC_DISPATCH_TABLE*/ DispatchTable;
                    public uint RpcProtseqEndpointCount;
                    public IntPtr /*PRPC_PROTSEQ_ENDPOINT*/ RpcProtseqEndpoint;
                    public IntPtr Reserved;
                    public IntPtr InterpreterInfo;
                    public uint Flags;

                    public static Guid IID_SYNTAX = new Guid(0x8A885D04u, 0x1CEB, 0x11C9, 0x9F, 0xE8, 0x08, 0x00, 0x2B, 0x10, 0x48, 0x60);

                    public RPC_CLIENT_INTERFACE(Guid iid, ushort InterfaceVersionMajor, ushort InterfaceVersionMinor)
                    {
                        Length = (uint)Marshal.SizeOf(typeof(RPC_CLIENT_INTERFACE));
                        RPC_VERSION rpcVersion = new RPC_VERSION(InterfaceVersionMajor, InterfaceVersionMinor);
                        InterfaceId = new RPC_SYNTAX_IDENTIFIER();
                        InterfaceId.SyntaxGUID = iid;
                        InterfaceId.SyntaxVersion = rpcVersion;
                        rpcVersion = new RPC_VERSION(2, 0);
                        TransferSyntax = new RPC_SYNTAX_IDENTIFIER();
                        TransferSyntax.SyntaxGUID = IID_SYNTAX;
                        TransferSyntax.SyntaxVersion = rpcVersion;
                        DispatchTable = IntPtr.Zero;
                        RpcProtseqEndpointCount = 0u;
                        RpcProtseqEndpoint = IntPtr.Zero;
                        Reserved = IntPtr.Zero;
                        InterpreterInfo = IntPtr.Zero;
                        Flags = 0u;
                    }
                }

                [StructLayout(LayoutKind.Sequential)]
                struct MIDL_STUB_DESC
                {
                    public IntPtr /*RPC_CLIENT_INTERFACE*/ RpcInterfaceInformation;
                    public IntPtr pfnAllocate;
                    public IntPtr pfnFree;
                    public IntPtr pAutoBindHandle;
                    public IntPtr /*NDR_RUNDOWN*/ apfnNdrRundownRoutines;
                    public IntPtr /*GENERIC_BINDING_ROUTINE_PAIR*/ aGenericBindingRoutinePairs;
                    public IntPtr /*EXPR_EVAL*/ apfnExprEval;
                    public IntPtr /*XMIT_ROUTINE_QUINTUPLE*/ aXmitQuintuple;
                    public IntPtr pFormatTypes;
                    public int fCheckBounds;
                    /* Ndr library version. */
                    public uint Version;
                    public IntPtr /*MALLOC_FREE_STRUCT*/ pMallocFreeStruct;
                    public int MIDLVersion;
                    public IntPtr CommFaultOffsets;
                    // New fields for version 3.0+
                    public IntPtr /*USER_MARSHAL_ROUTINE_QUADRUPLE*/ aUserMarshalQuadruple;
                    // Notify routines - added for NT5, MIDL 5.0
                    public IntPtr /*NDR_NOTIFY_ROUTINE*/ NotifyRoutineTable;
                    public IntPtr mFlags;
                    // International support routines - added for 64bit post NT5
                    public IntPtr /*NDR_CS_ROUTINES*/ CsRoutineTables;
                    public IntPtr ProxyServerInfo;
                    public IntPtr /*NDR_EXPR_DESC*/ pExprInfo;
                    // Fields up to now present in win2000 release.

                    public MIDL_STUB_DESC(IntPtr pFormatTypesPtr, IntPtr RpcInterfaceInformationPtr,
                                            IntPtr pfnAllocatePtr, IntPtr pfnFreePtr)
                    {
                        pFormatTypes = pFormatTypesPtr;
                        RpcInterfaceInformation = RpcInterfaceInformationPtr;
                        CommFaultOffsets = IntPtr.Zero;
                        pfnAllocate = pfnAllocatePtr;
                        pfnFree = pfnFreePtr;
                        pAutoBindHandle = IntPtr.Zero;
                        apfnNdrRundownRoutines = IntPtr.Zero;
                        aGenericBindingRoutinePairs = IntPtr.Zero;
                        apfnExprEval = IntPtr.Zero;
                        aXmitQuintuple = IntPtr.Zero;
                        fCheckBounds = 1;
                        Version = 0x50002u;
                        pMallocFreeStruct = IntPtr.Zero;
                        MIDLVersion = 0x801026e;
                        aUserMarshalQuadruple = IntPtr.Zero;
                        NotifyRoutineTable = IntPtr.Zero;
                        mFlags = new IntPtr(0x00000001);
                        CsRoutineTables = IntPtr.Zero;
                        ProxyServerInfo = IntPtr.Zero;
                        pExprInfo = IntPtr.Zero;
                    }
                }
            }
        }

        /////////////////PE
        ///
        public class PE
        {

            public const uint IMAGE_SCN_MEM_EXECUTE = 0x20000000;
            public const uint IMAGE_SCN_MEM_READ = 0x40000000;
            public const uint IMAGE_SCN_MEM_WRITE = 0x80000000;

            public const int IDT_SINGLE_ENTRY_LENGTH = 20;
            public const int IDT_IAT_OFFSET = 16;
            public const int IDT_DLL_NAME_OFFSET = 12;
            public const int ILT_HINT_LENGTH = 2;

            public const int PEB_RTL_USER_PROCESS_PARAMETERS_OFFSET = 0x20;
            public const int RTL_USER_PROCESS_PARAMETERS_COMMANDLINE_OFFSET = 0x70;
            public const int RTL_USER_PROCESS_PARAMETERS_MAX_LENGTH_OFFSET = 2;
            public const int RTL_USER_PROCESS_PARAMETERS_IMAGE_OFFSET = 0x60;
            public const int UNICODE_STRING_STRUCT_STRING_POINTER_OFFSET = 0x8;
            public const int PEB_BASE_ADDRESS_OFFSET = 0x10;

            // DllMain constants
            public const UInt32 DLL_PROCESS_DETACH = 0;
            public const UInt32 DLL_PROCESS_ATTACH = 1;
            public const UInt32 DLL_THREAD_ATTACH = 2;
            public const UInt32 DLL_THREAD_DETACH = 3;

            // Primary class for loading PE
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool DllMain(IntPtr hinstDLL, uint fdwReason, IntPtr lpvReserved);

            //[Flags]
            //public enum DataSectionFlags : uint
            //{
            //    TYPE_NO_PAD = 0x00000008,
            //    CNT_CODE = 0x00000020,
            //    CNT_INITIALIZED_DATA = 0x00000040,
            //    CNT_UNINITIALIZED_DATA = 0x00000080,
            //    LNK_INFO = 0x00000200,
            //    LNK_REMOVE = 0x00000800,
            //    LNK_COMDAT = 0x00001000,
            //    NO_DEFER_SPEC_EXC = 0x00004000,
            //    GPREL = 0x00008000,
            //    MEM_FARDATA = 0x00008000,
            //    MEM_PURGEABLE = 0x00020000,
            //    MEM_16BIT = 0x00020000,
            //    MEM_LOCKED = 0x00040000,
            //    MEM_PRELOAD = 0x00080000,
            //    ALIGN_1BYTES = 0x00100000,
            //    ALIGN_2BYTES = 0x00200000,
            //    ALIGN_4BYTES = 0x00300000,
            //    ALIGN_8BYTES = 0x00400000,
            //    ALIGN_16BYTES = 0x00500000,
            //    ALIGN_32BYTES = 0x00600000,
            //    ALIGN_64BYTES = 0x00700000,
            //    ALIGN_128BYTES = 0x00800000,
            //    ALIGN_256BYTES = 0x00900000,
            //    ALIGN_512BYTES = 0x00A00000,
            //    ALIGN_1024BYTES = 0x00B00000,
            //    ALIGN_2048BYTES = 0x00C00000,
            //    ALIGN_4096BYTES = 0x00D00000,
            //    ALIGN_8192BYTES = 0x00E00000,
            //    ALIGN_MASK = 0x00F00000,
            //    LNK_NRELOC_OVFL = 0x01000000,
            //    MEM_DISCARDABLE = 0x02000000,
            //    MEM_NOT_CACHED = 0x04000000,
            //    MEM_NOT_PAGED = 0x08000000,
            //    MEM_SHARED = 0x10000000,
            //    MEM_EXECUTE = 0x20000000,
            //    MEM_READ = 0x40000000,
            //    MEM_WRITE = 0x80000000
            //}

            [Flags]
            public enum DataSectionFlags : uint
            {
                Stub = 0x00000000,
            }

            public struct IMAGE_DOS_HEADER
            {      // DOS .EXE header
                public UInt16 e_magic;              // Magic number
                public UInt16 e_cblp;               // Bytes on last page of file
                public UInt16 e_cp;                 // Pages in file
                public UInt16 e_crlc;               // Relocations
                public UInt16 e_cparhdr;            // Size of header in paragraphs
                public UInt16 e_minalloc;           // Minimum extra paragraphs needed
                public UInt16 e_maxalloc;           // Maximum extra paragraphs needed
                public UInt16 e_ss;                 // Initial (relative) SS value
                public UInt16 e_sp;                 // Initial SP value
                public UInt16 e_csum;               // Checksum
                public UInt16 e_ip;                 // Initial IP value
                public UInt16 e_cs;                 // Initial (relative) CS value
                public UInt16 e_lfarlc;             // File address of relocation table
                public UInt16 e_ovno;               // Overlay number
                public UInt16 e_res_0;              // Reserved words
                public UInt16 e_res_1;              // Reserved words
                public UInt16 e_res_2;              // Reserved words
                public UInt16 e_res_3;              // Reserved words
                public UInt16 e_oemid;              // OEM identifier (for e_oeminfo)
                public UInt16 e_oeminfo;            // OEM information; e_oemid specific
                public UInt16 e_res2_0;             // Reserved words
                public UInt16 e_res2_1;             // Reserved words
                public UInt16 e_res2_2;             // Reserved words
                public UInt16 e_res2_3;             // Reserved words
                public UInt16 e_res2_4;             // Reserved words
                public UInt16 e_res2_5;             // Reserved words
                public UInt16 e_res2_6;             // Reserved words
                public UInt16 e_res2_7;             // Reserved words
                public UInt16 e_res2_8;             // Reserved words
                public UInt16 e_res2_9;             // Reserved words
                public UInt32 e_lfanew;             // File address of new exe header
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct IMAGE_DATA_DIRECTORY
            {
                public UInt32 VirtualAddress;
                public UInt32 Size;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct IMAGE_OPTIONAL_HEADER32
            {
                public UInt16 Magic;
                public Byte MajorLinkerVersion;
                public Byte MinorLinkerVersion;
                public UInt32 SizeOfCode;
                public UInt32 SizeOfInitializedData;
                public UInt32 SizeOfUninitializedData;
                public UInt32 AddressOfEntryPoint;
                public UInt32 BaseOfCode;
                public UInt32 BaseOfData;
                public UInt32 ImageBase;
                public UInt32 SectionAlignment;
                public UInt32 FileAlignment;
                public UInt16 MajorOperatingSystemVersion;
                public UInt16 MinorOperatingSystemVersion;
                public UInt16 MajorImageVersion;
                public UInt16 MinorImageVersion;
                public UInt16 MajorSubsystemVersion;
                public UInt16 MinorSubsystemVersion;
                public UInt32 Win32VersionValue;
                public UInt32 SizeOfImage;
                public UInt32 SizeOfHeaders;
                public UInt32 CheckSum;
                public UInt16 Subsystem;
                public UInt16 DllCharacteristics;
                public UInt32 SizeOfStackReserve;
                public UInt32 SizeOfStackCommit;
                public UInt32 SizeOfHeapReserve;
                public UInt32 SizeOfHeapCommit;
                public UInt32 LoaderFlags;
                public UInt32 NumberOfRvaAndSizes;

                public IMAGE_DATA_DIRECTORY ExportTable;
                public IMAGE_DATA_DIRECTORY ImportTable;
                public IMAGE_DATA_DIRECTORY ResourceTable;
                public IMAGE_DATA_DIRECTORY ExceptionTable;
                public IMAGE_DATA_DIRECTORY CertificateTable;
                public IMAGE_DATA_DIRECTORY BaseRelocationTable;
                public IMAGE_DATA_DIRECTORY Debug;
                public IMAGE_DATA_DIRECTORY Architecture;
                public IMAGE_DATA_DIRECTORY GlobalPtr;
                public IMAGE_DATA_DIRECTORY TLSTable;
                public IMAGE_DATA_DIRECTORY LoadConfigTable;
                public IMAGE_DATA_DIRECTORY BoundImport;
                public IMAGE_DATA_DIRECTORY IAT;
                public IMAGE_DATA_DIRECTORY DelayImportDescriptor;
                public IMAGE_DATA_DIRECTORY CLRRuntimeHeader;
                public IMAGE_DATA_DIRECTORY Reserved;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct IMAGE_OPTIONAL_HEADER64
            {
                public UInt16 Magic;
                public Byte MajorLinkerVersion;
                public Byte MinorLinkerVersion;
                public UInt32 SizeOfCode;
                public UInt32 SizeOfInitializedData;
                public UInt32 SizeOfUninitializedData;
                public UInt32 AddressOfEntryPoint;
                public UInt32 BaseOfCode;
                public UInt64 ImageBase;
                public UInt32 SectionAlignment;
                public UInt32 FileAlignment;
                public UInt16 MajorOperatingSystemVersion;
                public UInt16 MinorOperatingSystemVersion;
                public UInt16 MajorImageVersion;
                public UInt16 MinorImageVersion;
                public UInt16 MajorSubsystemVersion;
                public UInt16 MinorSubsystemVersion;
                public UInt32 Win32VersionValue;
                public UInt32 SizeOfImage;
                public UInt32 SizeOfHeaders;
                public UInt32 CheckSum;
                public UInt16 Subsystem;
                public UInt16 DllCharacteristics;
                public UInt64 SizeOfStackReserve;
                public UInt64 SizeOfStackCommit;
                public UInt64 SizeOfHeapReserve;
                public UInt64 SizeOfHeapCommit;
                public UInt32 LoaderFlags;
                public UInt32 NumberOfRvaAndSizes;

                public IMAGE_DATA_DIRECTORY ExportTable;
                public IMAGE_DATA_DIRECTORY ImportTable;
                public IMAGE_DATA_DIRECTORY ResourceTable;
                public IMAGE_DATA_DIRECTORY ExceptionTable;
                public IMAGE_DATA_DIRECTORY CertificateTable;
                public IMAGE_DATA_DIRECTORY BaseRelocationTable;
                public IMAGE_DATA_DIRECTORY Debug;
                public IMAGE_DATA_DIRECTORY Architecture;
                public IMAGE_DATA_DIRECTORY GlobalPtr;
                public IMAGE_DATA_DIRECTORY TLSTable;
                public IMAGE_DATA_DIRECTORY LoadConfigTable;
                public IMAGE_DATA_DIRECTORY BoundImport;
                public IMAGE_DATA_DIRECTORY IAT;
                public IMAGE_DATA_DIRECTORY DelayImportDescriptor;
                public IMAGE_DATA_DIRECTORY CLRRuntimeHeader;
                public IMAGE_DATA_DIRECTORY Reserved;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct IMAGE_FILE_HEADER
            {
                public UInt16 Machine;
                public UInt16 NumberOfSections;
                public UInt32 TimeDateStamp;
                public UInt32 PointerToSymbolTable;
                public UInt32 NumberOfSymbols;
                public UInt16 SizeOfOptionalHeader;
                public UInt16 Characteristics;
            }

            [StructLayout(LayoutKind.Explicit)]
            public struct IMAGE_SECTION_HEADER
            {
                [FieldOffset(0)]
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
                public char[] Name;
                [FieldOffset(8)]
                public UInt32 VirtualSize;
                [FieldOffset(12)]
                public UInt32 VirtualAddress;
                [FieldOffset(16)]
                public UInt32 SizeOfRawData;
                [FieldOffset(20)]
                public UInt32 PointerToRawData;
                [FieldOffset(24)]
                public UInt32 PointerToRelocations;
                [FieldOffset(28)]
                public UInt32 PointerToLinenumbers;
                [FieldOffset(32)]
                public UInt16 NumberOfRelocations;
                [FieldOffset(34)]
                public UInt16 NumberOfLinenumbers;
                [FieldOffset(36)]
                public DataSectionFlags Characteristics;

                public string Section
                {
                    get { return new string(Name); }
                }
            }

            [StructLayout(LayoutKind.Explicit)]
            public struct IMAGE_EXPORT_DIRECTORY
            {
                [FieldOffset(0)]
                public UInt32 Characteristics;
                [FieldOffset(4)]
                public UInt32 TimeDateStamp;
                [FieldOffset(8)]
                public UInt16 MajorVersion;
                [FieldOffset(10)]
                public UInt16 MinorVersion;
                [FieldOffset(12)]
                public UInt32 Name;
                [FieldOffset(16)]
                public UInt32 Base;
                [FieldOffset(20)]
                public UInt32 NumberOfFunctions;
                [FieldOffset(24)]
                public UInt32 NumberOfNames;
                [FieldOffset(28)]
                public UInt32 AddressOfFunctions;
                [FieldOffset(32)]
                public UInt32 AddressOfNames;
                [FieldOffset(36)]
                public UInt32 AddressOfOrdinals;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct IMAGE_BASE_RELOCATION
            {
                public uint VirtualAdress;
                public uint SizeOfBlock;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct PE_META_DATA
            {
                public UInt32 Pe;
                public Boolean Is32Bit;
                public IMAGE_FILE_HEADER ImageFileHeader;
                public IMAGE_OPTIONAL_HEADER32 OptHeader32;
                public IMAGE_OPTIONAL_HEADER64 OptHeader64;
                public IMAGE_SECTION_HEADER[] Sections;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct PE_MANUAL_MAP
            {
                public String DecoyModule;
                public IntPtr ModuleBase;
                public PE_META_DATA PEINFO;
            }

            [StructLayout(LayoutKind.Explicit)]
            public struct IMAGE_THUNK_DATA32
            {
                [FieldOffset(0)]
                public UInt32 ForwarderString;
                [FieldOffset(0)]
                public UInt32 Function;
                [FieldOffset(0)]
                public UInt32 Ordinal;
                [FieldOffset(0)]
                public UInt32 AddressOfData;
            }

            [StructLayout(LayoutKind.Explicit)]
            public struct IMAGE_THUNK_DATA64
            {
                [FieldOffset(0)]
                public UInt64 ForwarderString;
                [FieldOffset(0)]
                public UInt64 Function;
                [FieldOffset(0)]
                public UInt64 Ordinal;
                [FieldOffset(0)]
                public UInt64 AddressOfData;
            }

            [StructLayout(LayoutKind.Explicit)]
            public struct ApiSetNamespace
            {
                [FieldOffset(0x0C)]
                public int Count;

                [FieldOffset(0x10)]
                public int EntryOffset;
            }

            [StructLayout(LayoutKind.Explicit, Size = 24)]
            public struct ApiSetNamespaceEntry
            {
                [FieldOffset(0x04)]
                public int NameOffset;

                [FieldOffset(0x08)]
                public int NameLength;

                [FieldOffset(0x10)]
                public int ValueOffset;
            }

            [StructLayout(LayoutKind.Explicit)]
            public struct ApiSetValueEntry
            {
                [FieldOffset(0x0C)]
                public int ValueOffset;

                [FieldOffset(0x10)]
                public int ValueCount;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct LDR_DATA_TABLE_ENTRY
            {
                public LIST_ENTRY InLoadOrderLinks;
                public LIST_ENTRY InMemoryOrderLinks;
                public LIST_ENTRY InInitializationOrderLinks;
                public IntPtr DllBase;
                public IntPtr EntryPoint;
                public UInt32 SizeOfImage;
                public UNICODE_STRING FullDllName;
                public UNICODE_STRING BaseDllName;
            }
        }

        /////////////////Native
        ///


        /// <summary>
        /// Holds delegates for API calls in the NT Layer.
        /// </summary>
        public struct NT_DELEGATES
        {
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NTSTATUS NtCreateThreadEx(out IntPtr threadHandle, h_DynInv.Win32.WinNT.ACCESS_MASK desiredAccess, IntPtr objectAttributes, IntPtr processHandle, IntPtr startAddress, IntPtr parameter, bool createSuspended, int stackZeroBits, int sizeOfStack, int maximumStackSize, IntPtr attributeList);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NTSTATUS RtlCreateUserThread(IntPtr Process, IntPtr ThreadSecurityDescriptor, bool CreateSuspended, IntPtr ZeroBits, IntPtr MaximumStackSize, IntPtr CommittedStackSize, IntPtr StartAddress, IntPtr Parameter, ref IntPtr Thread, IntPtr ClientId);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NTSTATUS NtCreateSection(ref IntPtr SectionHandle, uint DesiredAccess, IntPtr ObjectAttributes, ref ulong MaximumSize, uint SectionPageProtection, uint AllocationAttributes, IntPtr FileHandle);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NTSTATUS NtUnmapViewOfSection(IntPtr hProc, IntPtr baseAddr);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NTSTATUS NtMapViewOfSection(IntPtr SectionHandle, IntPtr ProcessHandle, out IntPtr BaseAddress, IntPtr ZeroBits, IntPtr CommitSize, IntPtr SectionOffset, out ulong ViewSize, uint InheritDisposition, uint AllocationType, uint Win32Protect);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 LdrLoadDll(IntPtr PathToFile, UInt32 dwFlags, ref UNICODE_STRING ModuleFileName, ref IntPtr ModuleHandle);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate void RtlInitUnicodeString(ref UNICODE_STRING DestinationString, [MarshalAs(UnmanagedType.LPWStr)] string SourceString);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate void RtlZeroMemory(IntPtr Destination, int length);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtQueryInformationProcess(IntPtr processHandle, PROCESSINFOCLASS processInformationClass, IntPtr processInformation, int processInformationLength, ref UInt32 returnLength);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtOpenProcess(ref IntPtr ProcessHandle, h_DynInv.Win32.Kernel32.ProcessAccessFlags DesiredAccess, ref OBJECT_ATTRIBUTES ObjectAttributes, ref CLIENT_ID ClientId);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtQueueApcThread(IntPtr ThreadHandle, IntPtr ApcRoutine, IntPtr ApcArgument1, IntPtr ApcArgument2, IntPtr ApcArgument3);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtOpenThread(ref IntPtr ThreadHandle, h_DynInv.Win32.Kernel32.ThreadAccess DesiredAccess, ref OBJECT_ATTRIBUTES ObjectAttributes, ref CLIENT_ID ClientId);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtAllocateVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, IntPtr ZeroBits, ref IntPtr RegionSize, h_DynInv.Win32.Kernel32.AllocationType AllocationType, UInt32 Protect);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtFreeVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, ref IntPtr RegionSize, h_DynInv.Win32.Kernel32.AllocationType FreeType);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtQueryVirtualMemory(IntPtr ProcessHandle, IntPtr BaseAddress, MEMORYINFOCLASS MemoryInformationClass, IntPtr MemoryInformation, UInt32 MemoryInformationLength, ref UInt32 ReturnLength);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtProtectVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, ref IntPtr RegionSize, UInt32 NewProtect, ref UInt32 OldProtect);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtWriteVirtualMemory(IntPtr ProcessHandle, IntPtr BaseAddress, IntPtr Buffer, UInt32 BufferLength, ref UInt32 BytesWritten);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 RtlUnicodeStringToAnsiString(ref ANSI_STRING DestinationString, ref UNICODE_STRING SourceString, bool AllocateDestinationString);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 LdrGetProcedureAddress(IntPtr hModule, IntPtr FunctionName, IntPtr Ordinal, ref IntPtr FunctionAddress);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 RtlGetVersion(ref OSVERSIONINFOEX VersionInformation);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtReadVirtualMemory(IntPtr ProcessHandle, IntPtr BaseAddress, IntPtr Buffer, UInt32 NumberOfBytesToRead, ref UInt32 NumberOfBytesRead);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate UInt32 NtOpenFile(ref IntPtr FileHandle, h_DynInv.Win32.Kernel32.FileAccessFlags DesiredAccess, ref OBJECT_ATTRIBUTES ObjAttr, ref IO_STATUS_BLOCK IoStatusBlock, h_DynInv.Win32.Kernel32.FileShareFlags ShareAccess, h_DynInv.Win32.Kernel32.FileOpenFlags OpenOptions);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint NtResumeThread(IntPtr ThreadHandle, out uint SuspendCount);

            //ntWaitForSingleObject
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint NtWaitForSingleObject(IntPtr Handle, bool Alertable, IntPtr Timeout);
        }

        /// <summary>
        /// Holds delegates for API calls in Kernel32.
        /// </summary>
        public struct KER32_DELEGATES
        {
            //get console window kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr GetConsoleWindow();

            //allocate console kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool AllocConsole();

            //get command line w kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr GetCommandLineW();
            //get standard handle kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr GetStdHandle(int nStdHandle);
            //get current thread id kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint GetCurrentThreadId();
            // create process w kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool CreateProcessW(string lpApplicationName, string lpCommandLine, ref Win32.WinBase._SECURITY_ATTRIBUTES lpProcessAttributes, ref Win32.WinBase._SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref Win32.ProcessThreadsAPI._STARTUPINFOEX lpStartupInfo, out Win32.ProcessThreadsAPI._PROCESS_INFORMATION lpProcessInformation);
            //write process mem kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out IntPtr lpNumberOfBytesWritten);
            //VirtualProtect kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool VirtualProtect(IntPtr lpAddress, uint dwSize, Win32.Kernel32.MemoryProtection flNewProtect, out Win32.Kernel32.MemoryProtection lpflOldProtect);
            //Queue User APC kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint QueueUserAPC(IntPtr pfnAPC, IntPtr hThread, uint dwData);
            //resume thread kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint ResumeThread(IntPtr hThread);
            //create thread kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr param, uint dwCreationFlags, ref uint lpThreadId);
            //createRemoteThread
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, ref uint lpThreadId);
            //virtual alloc kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr VirtualAlloc(IntPtr lpStartAddr, uint size, uint flAllocationType, uint flProtect);
            //virtual alloc Ex kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
            //wait for single object kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
            //Load Library kernel32 
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate IntPtr LoadLibraryW(string library);
            //get proc address kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr GetProcAddress(IntPtr libPtr, string function);
            //free library kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool FreeLibrary(IntPtr library);
            // convert thread to fiber kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool ConvertThreadToFiber(IntPtr lpParameter);
            // createFiber kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr CreateFiber(uint dwStackSize, uint lpStartAddress, IntPtr lpParameter);
            //switch to fiber kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint SwitchToFiber(IntPtr lpFiber);
            //get current thread kernel32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr GetCurrentThread();
            // close handle 
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool CloseHandle(IntPtr hObject);
            //get last error 
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint GetLastError();
            //set std handle 
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool SetStdHandle(int nStdHandle, IntPtr hHandle);
            //openprocess
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);
            //IsWow64Process
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public delegate bool IsWow64Process(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] out bool Wow64Process);
            //createPipe
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool CreatePipe(out IntPtr hReadPipe, out IntPtr hWritePipe, ref Win32.WinBase._SECURITY_ATTRIBUTES lpPipeAttributes, uint nSize);
            //CreateNamedPipe
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
            public delegate IntPtr CreateNamedPipeW(string lpName, Win32.Kernel32.PipeOpenModeFlags dwOpenMode, Win32.Kernel32.PipeModeFlags dwPipeMode, uint nMaxInstances, uint nOutBufferSize, uint nInBufferSize, uint nDefaultTimeOut, IntPtr lpSecurityAttributes);
            //PeekNamedPipe
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool PeekNamedPipe(IntPtr hNamedPipe, byte[] lpBuffer, uint nBufferSize, ref uint lpBytesRead, ref uint lpTotalBytesAvail, ref uint lpBytesLeftThisMessage);
            //createFile
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
            public delegate IntPtr CreateFileW([MarshalAs(UnmanagedType.LPTStr)] string lpFileName, [MarshalAs(UnmanagedType.U4)] Win32.Kernel32.EFileAccess dwDesiredAccess, [MarshalAs(UnmanagedType.U4)] Win32.Kernel32.EFileShare dwShareMode, IntPtr lpSecurityAttributes, Win32.Kernel32.ECreationDisposition dwCreationDisposition, [MarshalAs(UnmanagedType.U4)] Win32.Kernel32.EFileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);
            //readFile
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool ReadFile(IntPtr hFile, [Out] byte[] lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);
            //SetHandleInfo
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool SetHandleInformation(IntPtr hObject, Win32.Kernel32.HANDLE_FLAGS dwMask, Win32.Kernel32.HANDLE_FLAGS dwFlags);
            //connectNamedPipe
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool ConnectNamedPipe(IntPtr hNamedPipe, IntPtr lpOverlapped);

        }

        /// <summary>
        /// Holds delegates for ADVAPI32 API calls
        /// </summary>
        public struct ADVAPI32_DELEGATES
        {
            //logonUser
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, Win32.Advapi32.LOGON_TYPE dwLogonType, Win32.Advapi32.LOGON_PROVIDER dwLogonProvider, out IntPtr phToken);
            //RevetToSelf
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool RevertToSelf();
            //ImpersonateLoggedOnUser
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool ImpersonateLoggedOnUser(IntPtr hToken);
            //openProcessToken
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool OpenProcessToken(IntPtr ProcessHandle, uint dwDesiredAccess, out IntPtr TokenHandle);
            //duplicateTokenEx
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool DuplicateTokenEx(IntPtr hExistingToken, uint dwDesiredAccess, ref Win32.WinBase._SECURITY_ATTRIBUTES lpTokenAttributes, Win32.WinNT._SECURITY_IMPERSONATION_LEVEL ImpersonationLevel, Win32.WinNT.TOKEN_TYPE TokenType, out IntPtr phNewToken);
            //CreateProcessWIthLogon
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool CreateProcessWithLogonW(string lpUsername, string lpDomain, string lpPassword, uint dwLogonFlags, string lpApplicationName, string lpCommandLine, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref Win32.ProcessThreadsAPI._STARTUPINFO lpStartupInfo, out Win32.ProcessThreadsAPI._PROCESS_INFORMATION lpProcessInformation);
            //CreateProcessAsUser
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto,SetLastError = true)]
            public delegate bool CreateProcessAsUser(IntPtr hToken, string lpApplicationName, string lpCommandLine, ref Win32.WinBase._SECURITY_ATTRIBUTES lpProcessAttributes, ref Win32.WinBase._SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref Win32.ProcessThreadsAPI._STARTUPINFO lpStartupInfo, out Win32.ProcessThreadsAPI._PROCESS_INFORMATION lpProcessInformation);
            //getTokenInformation
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool GetTokenInformation(IntPtr TokenHandle, Win32.WinNT._TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, int TokenInformationLength, out int ReturnLength);
            //lookupPrivilegeName 
            [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Auto)]
            public delegate bool LookupPrivilegeName(string lpSystemName, ref Win32.WinNT._LUID lpLuid, StringBuilder lpName, ref int cchName);
            //lookupPrivilegeValue
            [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Auto)]
            public delegate bool LookupPrivilegeValue(string lpSystemName, string lpName, out Win32.WinNT._LUID lpLuid);
            //OpenSCManager
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, Win32.Advapi32.SCM_ACCESS dwDesiredAccess);
            //StartService
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool StartService(IntPtr hService, uint dwNumServiceArgs, string[] lpServiceArgVectors);
            //CloseServiceHandle
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool CloseServiceHandle(IntPtr hSCObject);
            //CreateService
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, Win32.Advapi32.SERVICE_ACCESS dwDesiredAccess, Win32.Advapi32.SERVICE_TYPE dwServiceType, Win32.Advapi32.SERVICE_START dwStartType, Win32.Advapi32.SERVICE_ERROR dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);
            //AdjustTokenPrivileges
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPrivileges, ref Win32.WinNT._TOKEN_PRIVILEGES NewState, int BufferLength, IntPtr PreviousState, IntPtr ReturnLength);
            //impersonateNamedPipeClient
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool ImpersonateNamedPipeClient(IntPtr hNamedPipe);
        }

        /// <summary>
        /// Holds delegates for USER32 API calls
        /// </summary>
        public struct USER32_DELEGATES
        {
            //show window user32
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool ShowWindow(IntPtr hWnd, int nCmdShow);
        }

        ///<summary>
        /// holds delegates for the Userenv API calls
        ///</summary>
       public struct USERENV_DELEGATES
        {
            //delete profile
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate bool DeleteProfile(string lpSidString, string lpProfilePath, string lpComputerName);
        }

        /// <summary>
        /// Holds delegates for Custom API calls. Mainly shellcode
        /// </summary>
        public struct CUSTOM_DELEGATES
        {
            //SleepEncrypt
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate int SleepEncryptDelegate(IntPtr pARAMS);
            //generic delegate
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate string GenericDelegate(string input);
            //generic string out no args
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate string GenericNoArgsDelegate();
            //generic delegate
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public delegate string GenericArrayDelegate(string[] input);
        }
    }
}
