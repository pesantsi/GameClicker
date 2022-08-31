using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace GameClicker.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<ProcessViewModel> m_LocalProcess;
        private ProcessViewModel m_SelectedProcess;
        private string m_FilterText;
        private Predicate<object> m_Filter;
        private ObservableCollection<KeyConfigViewModel> m_KeyConfigs;

        public IRelayCommand LoadProcessCommand { get; }
        public IRelayCommand AddKeyCommand { get; }
        public IRelayCommand LoadCommand { get; }
        public IRelayCommand SaveCommand { get; }
        public IRelayCommand StartCommand { get; }
        public IRelayCommand StopCommand { get; }

        public ObservableCollection<ProcessViewModel> LocalProcess
        {
            get => m_LocalProcess;
            set => SetProperty(ref m_LocalProcess, value);
        }

        public ProcessViewModel SelectedProcess
        {
            get => m_SelectedProcess;
            set => SetProperty(ref m_SelectedProcess, value);
        }

        public string FilterText
        {
            get => m_FilterText;
            set
            {
                SetProperty(ref m_FilterText, value);

                Filter = string.IsNullOrEmpty(m_FilterText) ? (Predicate<object>)null : IsMatch;
            }
        }

        public Predicate<object> Filter
        {
            get { return m_Filter; }
            private set
            {
                m_Filter = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyConfigViewModel> KeyConfigs
        {
            get => m_KeyConfigs;
            set => SetProperty(ref m_KeyConfigs, value);
        }

        public MainViewModel()
        {
            LocalProcess = new ObservableCollection<ProcessViewModel>();
            KeyConfigs = new ObservableCollection<KeyConfigViewModel>();

            LoadProcessCommand = new RelayCommand(LoadProcess);
            LoadProcess();

            AddKeyCommand = new RelayCommand(AddKey);

            LoadCommand = new RelayCommand(Load);
            SaveCommand = new RelayCommand(Save);
            StartCommand = new RelayCommand(Start, ()=> SelectedProcess != null);
            StopCommand = new RelayCommand(Stop);
        }

        private void LoadProcess()
        {
            LocalProcess.Clear();

            List<Process> processes = new List<Process>(Process.GetProcesses());

            foreach (Process p in processes.OrderBy(x => x.ProcessName).GroupBy(x => x.ProcessName).Select(x => x.FirstOrDefault()))
            {
                LocalProcess.Add(new ProcessViewModel(p));
            }

            OnPropertyChanged(nameof(LocalProcess));
        }

        private bool IsMatch(object item)
        {
            return ProcessViewModel.IsMatch((ProcessViewModel)item, m_FilterText);
        }

        private void AddKey()
        {

        }

        private void Load()
        {

        }

        private void Save()
        { 
        }

        private void Start()
        {
        }

        private void Stop()
        {
        }
    }
}
