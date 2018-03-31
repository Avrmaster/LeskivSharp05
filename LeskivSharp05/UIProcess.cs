using System;
using System.Diagnostics;
using LeskivSharp05.Annotations;

namespace LeskivSharp05
{
    public class UiProcess
    {
        private readonly Process _process;

        public string Name => _process.ProcessName;
        public int Id => _process.Id;
        public string User => _process.MachineName;
        public string Path => _process.MainModule.FileName;
        public DateTime LaunchDateTime => _process.StartTime;

        public bool IsActive => _process.Responding;
        public float CPU => 0;
        public long MEM => _process.PrivateMemorySize64/1024;
        public int ThreadsCnt => _process.Threads.Count;

        internal UiProcess([NotNull] Process process)
        {
            this._process = process;
        }

        public override bool Equals(object obj)
        {
            return obj is UiProcess another && this.Id == another.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
