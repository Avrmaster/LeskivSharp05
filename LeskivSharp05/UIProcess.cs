using System;
using System.Collections.Generic;
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
        public long MEM => _process.PrivateMemorySize64 / 1024;
        public int ThreadsCnt => _process.Threads.Count;

        private HashSet<UiModule> _modules;

        public HashSet<UiModule> Modules
        {
            get
            {
                if (_modules == null)
                    RefreshModules();
                return _modules;
            }
        }

        private HashSet<UiThread> _threads;

        public HashSet<UiThread> Threads
        {
            get
            {
                if (_threads == null)
                    RefreshThreads();
                return _threads;
            }
        }

        internal UiProcess([NotNull] Process process)
        {
            this._process = process;
        }

        internal void RefreshModules()
        {
            if (_modules == null)
                _modules = new HashSet<UiModule>();
            foreach (ProcessModule m in _process.Modules)
            {
                _modules.Add(new UiModule(m));
            }
        }

        internal void RefreshThreads()
        {
            if (_threads == null)
                _threads = new HashSet<UiThread>();
            foreach (ProcessThread t in _process.Threads)
            {
                _threads.Add(new UiThread(t));
            }
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

    public class UiModule
    {
        private readonly ProcessModule _module;

        public string Name => _module.ModuleName;
        public string Path => _module.FileName;

        internal UiModule([NotNull] ProcessModule module)
        {
            this._module = module;
        }

        public override bool Equals(object obj)
        {
            return obj is UiModule another && this._module.Equals(another._module);
        }

        public override int GetHashCode()
        {
            return _module.GetHashCode();
        }
    }

    public class UiThread
    {
        private readonly ProcessThread _thread;

        public int Id => _thread.Id;
        public ThreadState State => _thread.ThreadState;
        public DateTime LaunchDateTime => _thread.StartTime;

        internal UiThread([NotNull] ProcessThread thread)
        {
            this._thread = thread;
        }

        public override bool Equals(object obj)
        {
            return obj is UiThread another && this._thread.Equals(another._thread);
        }

        public override int GetHashCode()
        {
            return _thread.GetHashCode();
        }
    }
}