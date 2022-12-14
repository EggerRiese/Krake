using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Krake.Object
{
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public bool Bar { get; set; }
    }

    public class FirstLetter : ObservableCollection<Location>
    {
        public string Letter { get; set; }
    }
}
