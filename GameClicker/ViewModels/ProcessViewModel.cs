using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics;

namespace GameClicker.ViewModels
{
    public class ProcessViewModel : ObservableObject
    {
        public Process Process { get; set; }

        public ProcessViewModel(Process process)
        {
            Process = process;
        }

        public override string ToString()
        {
            return Process.ProcessName;
        }

        public static bool IsMatch(ProcessViewModel item, string filterText)
        {
            if (string.IsNullOrEmpty(filterText))
            {
                return true;
            }

            var name = item.Process.ProcessName;
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            if (filterText.Length == 1)
            {
                return name.StartsWith(filterText, StringComparison.InvariantCultureIgnoreCase);
            }

            return name.IndexOf(filterText, 0, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }
    }
}
