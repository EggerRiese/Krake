using Krake.Object;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Krake.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        public Event Ev { get; set; }
        public Location Loc { get; set; }
        public Command LoadDataCommand { get; set; }

        public DetailViewModel(Event e = null, Location l = null)
        {
            Ev = e;
            Loc = l;

            if(Loc != null)
            {
                eventsFromLocation = new ObservableCollection<DateGroup>();
                LoadDataCommand = new Command(async () => await LoadEventPerLocationCommand(eventsFromLocation, Loc.Name));
                LoadDataCommand.Execute(null);
            }
        }
    }
}
