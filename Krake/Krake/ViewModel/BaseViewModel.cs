using Krake.Network;
using Krake.Object;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Krake.ViewModel
{
    public abstract class BaseViewModel
    {
        protected Connection Connection { get; } = new Connection();
        private bool IsBusy { get; set; }
        public static bool ForceReload = false;

        public ObservableCollection<DateGroup> Party { get; set; }
        public ObservableCollection<DateGroup> Bar { get; set; }
        public ObservableCollection<FirstLetter> Location { get; set; }
        public ObservableCollection<DateGroup> eventsFromLocation { get; set; }

        public int eventsFromLocationCount = 0;

        protected async Task LoadEventCommand(ObservableCollection<DateGroup> collection, string type)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                AssignEvents(await Connection.GetEvent(type, true), collection);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async Task LoadLocationCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                AssignLocations(await Connection.GetLocation(true));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async Task LoadEventPerLocationCommand(ObservableCollection<DateGroup> collection, string location)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                var _list = await Connection.GetEventsFromLocation(location, true);
                eventsFromLocationCount = _list.Count;
                AssignEvents(_list, collection);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void AssignEvents(List<Event> events, ObservableCollection<DateGroup> collection)
        {
            int i = 0;
            foreach (var item in events)
            {
                if (collection.Count == 0)
                {
                    if (item.Sponsored.Equals("True"))
                    {
                        collection.Add(new DateGroup() { DateG = "Gesponsert" });
                        collection[i].Add(item);
                        i++;
                    }
                    else
                    {
                        collection.Add(new DateGroup() { DateG = item.Date });
                        collection[i].Add(item);
                        i++;
                    }
                }
                else if (collection[i - 1].DateG.Equals(item.Date) || item.Sponsored.Equals("Gesponsert"))
                {
                    collection[i - 1].Add(item);
                }
                else
                {
                    collection.Add(new DateGroup() { DateG = item.Date });
                    collection[i].Add(item);
                    i++;
                }
            }
        }
        private void AssignLocations(List<Location> locations)
        {
            int iLocation = 0;
            foreach (var item in locations)
            {
                if (Location.Count == 0)
                {
                    Location.Add(new FirstLetter() { Letter = item.Name[0].ToString().ToUpper() });
                    Location[iLocation].Add(item);
                    iLocation++;
                }
                else if (Location[iLocation - 1].Letter.Equals(item.Name[0].ToString().ToUpper()))
                {
                    Location[iLocation - 1].Add(item);
                }
                else
                {
                    Location.Add(new FirstLetter() { Letter = item.Name[0].ToString().ToUpper() });
                    Location[iLocation].Add(item);
                    iLocation++;
                }
            }
        }

        public Location getLocation(string Name)
        {
            foreach (var letter in Location)
            {
                foreach (var item in letter)
                {
                    if(item.Name == Name)
                    {
                        return item;
                    } 
                }
            }
            return null;
        }
    }
}
