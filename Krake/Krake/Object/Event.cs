using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Krake.Object
{
    public class Event
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public string Entryfee { get; set; }
        public string Sponsored { get; set; }
    }

    public class DateGroup : ObservableCollection<Event>
    {
        public string DateG { get; set; }
    }
}
