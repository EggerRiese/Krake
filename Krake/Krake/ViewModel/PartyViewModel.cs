using Krake.Network;
using Krake.Object;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Krake.ViewModel
{
    public class PartyViewModel : BaseViewModel
    {
        private Command LoadDataCommand { get; set; }
        public PartyViewModel()
        {
            Party = new ObservableCollection<DateGroup>();
            LoadDataCommand = new Command(async () => await LoadEventCommand(Party, "Event"));
            LoadDataCommand.Execute(null);
        }

        public void Reload()
        {
            Party.Clear();
            LoadDataCommand = new Command(async () => await LoadEventCommand(Party, "Event"));
            LoadDataCommand.Execute(null);
        }

        public bool GetReload()
        {
            return ForceReload;
        }
    }
}
