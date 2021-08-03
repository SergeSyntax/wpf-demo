using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {

        public string Query { get; set; }

        public CurrentConditions CurrentConditions { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public ObservableCollection<City> Cities { get; set; }

        private City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                GetCUrrentConditions();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherVM()
        {
            SearchCommand = new RelayCommand(_ => MakeQuery());
            Cities = new ObservableCollection<City>();
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City()
                {
                    LocalizedName = "New York"
                };
                CurrentConditions = new CurrentConditions()
                {
                    WeatherText = "Partly Cloudy",
                    Temperature = new Temperature()
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }
        }

        private async void GetCUrrentConditions()
        {
            if (selectedCity.Key == null) return;
            Query = string.Empty;
            Cities.Clear();
            CurrentConditions = await AccuWeatherService.GetCurrentConditions(selectedCity.Key);
        }


        public async void MakeQuery()
        {
            var cities = await AccuWeatherService.GetCities(Query);
            Cities.Clear();
            cities.ForEach(Cities.Add);
        }


    }
}
