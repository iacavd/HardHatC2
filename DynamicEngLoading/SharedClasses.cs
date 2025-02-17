﻿using System.Collections.Generic;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace DynamicEngLoading
{
    public interface IEngineerCommand
    {
        string Name { get; }
        Task Execute(EngineerTask task);
    }
    [Serializable]
    public abstract class EngineerCommand : IEngineerCommand
    {
        public abstract string Name { get; }

        public abstract Task Execute(EngineerTask task);
    }

    [Serializable]
    public class EngineerTask
    {
        public string Id { get; set; }

        public string Command { get; set; }

        public Dictionary<string, string> Arguments { get; set; }

        public byte[] File { get; set; }

        public bool IsBlocking { get; set; }


        [NonSerialized]
        public CancellationToken cancelToken = new CancellationToken();

        public EngineerTask() { }

        public EngineerTask(string Id, string Command, Dictionary<string, string> Arguments, byte[] File, bool IsBlocking)
        {
            this.Id = Id;
            this.Command = Command;
            this.Arguments = Arguments;
            this.File = File;
            this.IsBlocking = IsBlocking;
        }
    }


    [Serializable]
    public class EngineerTaskResult
    {
        public string Id { get; set; }

        public string Command { get; set; }

        public byte[] Result { get; set; }

        public bool IsHidden { get; set; }

        public string EngineerId { get; set; }

        public EngTaskStatus Status { get; set; }

        public TaskResponseType ResponseType { get; set; }
    }
    public enum EngTaskStatus
    {
        Running = 2,
        Complete = 3,
        FailedWithWarnings = 4,
        CompleteWithErrors = 5,
        Failed = 6,
        Cancelled = 7
    }
    public enum TaskResponseType
    {
        None,
        String,
        FileSystemItem,
        ProcessItem,
        TokenStoreItem,
        FilePart,
    }

    public class FilePart
    {
        public int Type { get; set; } // 1 = file part, 2 = end of file
        public int Length { get; set; } // Size of data in bytes
        public byte[] Data { get; set; } // Actual data
    }

    public class TokenStoreItem
    {
        public int Index { get; set; }
        public string Username { get; set; }
        public int PID { get; set; }
        public string SID { get; set; }
        public bool IsCurrent { get; set; }
    }

    public class ProcessItem
    {
        public string ProcessName { get; set; }
        public string ProcessPath { get; set; }
        public string Owner { get; set; }
        public int ProcessId { get; set; }
        public int ProcessParentId { get; set; }
        public int SessionId { get; set; }
        public string Arch { get; set; }
    }
    
    [Serializable]
    public class FileSystemItem
    {
        public string Name { get; set; } = "";
        public long Length { get; set; } = 0;
        public string Owner { get; set; } = "";
        public long ChildItemCount { get; set; } = 0;
        public DateTime CreationTimeUtc { get; set; } = new DateTime();
        public DateTime LastAccessTimeUtc { get; set; } = new DateTime();
        public DateTime LastWriteTimeUtc { get; set; } = new DateTime();
        public List<ACL> ACLs { get; set; } = new List<ACL>();
    }

    [Serializable]
    public class ACL
    {
        public string IdentityRef { get; set; } = "";
        public string AccessControlType { get; set; } = "";
        public string FileSystemRights { get; set; } = "";
        public bool IsInherited { get; set; }
    }
}
