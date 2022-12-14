using Krake.Network;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Krake.ViewModel
{
    public class SelectionViewModel : BaseViewModel
    {
        public Command LoadDataCommand { get; set; }
        public ObservableCollection<string> CityList;
        private bool IsBusy { get; set; }

        public SelectionViewModel()
        {
            CityList = new ObservableCollection<string>();
            LoadDataCommand = new Command(async () => await LoadCitysCommand());
            LoadDataCommand.Execute(null);
        }

        private async Task LoadCitysCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                assignCitys(await Connection.GetAllCitys(true));                
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

        private void assignCitys(List<string> list)
        {
            foreach (var item in list)
            {
                CityList.Add(item);
            }
        }

        public void SetReload()
        {
            ForceReload = true;
        }
    }
}
