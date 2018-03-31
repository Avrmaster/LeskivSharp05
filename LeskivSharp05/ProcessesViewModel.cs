
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using LeskivSharp05.Annotations;

namespace LeskivSharp05
{
    class ProcessesViewModel : INotifyPropertyChanged
    {
        public UiProcess SelectedProcess { get; set; }
        public HashSet<UiProcess> ProcessesList { get; set; }

        internal ProcessesViewModel(Action refreshGridAction)
        {
            Process[] processes = Process.GetProcesses();
            ProcessesList = new HashSet<UiProcess>();
            foreach (var process in processes)
            {
                ProcessesList.Add(new UiProcess(process));
            }
            
            
        }


        #region Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
