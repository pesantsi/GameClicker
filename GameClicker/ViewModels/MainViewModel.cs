using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameClicker.Collections;
using GameClicker.Services;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace GameClicker.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<ProcessViewModel> m_LocalProcess;
        private ProcessViewModel m_SelectedProcess;
        private string m_FilterText;
        private Predicate<object> m_Filter;
        private KeyConfigCollection m_KeyConfigs;
        private bool m_IsRunning;
        private IDialogCoordinator m_DialogCoordinator;

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
            set
            {
                SetProperty(ref m_SelectedProcess, value);
                StartCommand.NotifyCanExecuteChanged();
            }
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

        public KeyConfigCollection KeyConfigs
        {
            get => m_KeyConfigs;
            set => SetProperty(ref m_KeyConfigs, value);
        }

        public bool IsRunning
        {
            get => m_IsRunning;
            set => SetProperty(ref m_IsRunning, value);
        }

        public MainViewModel(IDialogCoordinator instance)
        {
            m_DialogCoordinator = instance;

            LocalProcess = new ObservableCollection<ProcessViewModel>();
            KeyConfigs = new KeyConfigCollection();

            LoadProcessCommand = new RelayCommand(LoadProcess);
            LoadProcess();

            AddKeyCommand = new RelayCommand(AddKey);

            LoadCommand = new RelayCommand(Load);
            SaveCommand = new RelayCommand(Save);
            StartCommand = new RelayCommand(Start, () => SelectedProcess != null);
            StopCommand = new RelayCommand(Stop, () => m_IsRunning);
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
            KeyConfigs.AddNewItem();
        }

        private async void Load()
        {
            IFileManager fileManager = (IFileManager)App.Current.Services.GetService(typeof(IFileManager));
            KeyConfigCollection keyConfigs;

            if (fileManager.Load(out keyConfigs))
            {
                KeyConfigs = keyConfigs;
                await m_DialogCoordinator.ShowMessageAsync(this, "Chargement", "Le chargement a été effectué avec succès.", MessageDialogStyle.Affirmative);
            }
        }

        private async void Save()
        {
            IFileManager fileManager = (IFileManager)App.Current.Services.GetService(typeof(IFileManager));
            if (fileManager.Save(KeyConfigs))
            {
                await m_DialogCoordinator.ShowMessageAsync(this, "Sauvegarde", "La sauvegarde a été effectuée avec succès.", MessageDialogStyle.Affirmative);
            }
        }

        private void Start()
        {
        }

        private void Stop()
        {
        }
    }
}
