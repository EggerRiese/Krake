
using Krake.Object;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Krake.ViewModel
{
    public class BarViewModel : BaseViewModel
    {
        private Command LoadDataCommand { get; set; }
        public BarViewModel()
        {
            Bar = new ObservableCollection<DateGroup>();
            LoadDataCommand = new Command(async () => await LoadEventCommand(Bar, "Bar"));
            LoadDataCommand.Execute(null);
        }

        public void Reload()
        {
            Bar.Clear();
            LoadDataCommand = new Command(async () => await LoadEventCommand(Bar, "Bar"));
            LoadDataCommand.Execute(null);
        }

        public bool GetReload()
        {
            return ForceReload;
        }
    }
}
