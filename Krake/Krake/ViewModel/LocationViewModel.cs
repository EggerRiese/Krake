
using Krake.Object;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Krake.ViewModel
{
    public class LocationViewModel : BaseViewModel
    {
        private Command LoadDataCommand { get; set; }

        public LocationViewModel()
        {
            Location = new ObservableCollection<FirstLetter>();
            LoadDataCommand = new Command(async () => await LoadLocationCommand());
            LoadDataCommand.Execute(null);
        }

        public void Reload()
        {
            Location.Clear();
            LoadDataCommand = new Command(async () => await LoadLocationCommand());
            LoadDataCommand.Execute(null);
        }

        public bool GetReload()
        {
            return ForceReload;
        }
    }
}
