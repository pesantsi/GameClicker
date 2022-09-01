using GameClicker.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GameClicker.Collections
{
    public class KeyConfigCollection : ObservableCollection<KeyConfigViewModel>
    {
        public void AddNewItem()
        {
            Add(new KeyConfigViewModel());
        }
    }
}
