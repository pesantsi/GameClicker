using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GameClicker.ViewModels
{
    public class KeyConfigViewModel : ObservableObject
    {
        private Key m_Key;
        private int m_Frequency;
        private int m_InputCount;
        private bool m_IsActive = true;
        private int m_Progress;

        [JsonIgnore]
        public List<Key> Keys
        {
            get;
            private set;
        }

        public Key Key
        {
            get => m_Key;
            set => SetProperty(ref m_Key, value);
        }

        public int Frequency
        {
            get => m_Frequency;
            set => SetProperty(ref m_Frequency, value);
        }

        public int InputCount
        {
            get => m_InputCount;
            set => SetProperty(ref m_InputCount, value);
        }

        public bool IsActive
        {
            get => m_IsActive;
            set => SetProperty(ref m_IsActive, value);
        }

        public int Progress
        {
            get => m_Progress;
            set => SetProperty(ref m_Progress, value);
        }

        public KeyConfigViewModel()
        {
            Keys = new List<Key>(Enum.GetValues(typeof(Key)).Cast<Key>());

        }
    }
}
