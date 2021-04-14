﻿using System.Windows;
using ex1.Model;
using ex1.ViewModel;

namespace ex1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var model = new FlightGearModel(new AsyncTcpFGClient(), new PluginFlightAnomalyDetector());

            GeneralVM = new GeneralViewModel(model);
            GraphsVM = new GraphsViewModel(model);
            PlayBackVM = new PlayBackViewModel(model);
            RudderVM = new RudderViewModel(model);
            StatisticsVM = new StatisticsViewModel(model);
        }

        public GeneralViewModel GeneralVM { get; set; }
        public GraphsViewModel GraphsVM { get; set; }
        public PlayBackViewModel PlayBackVM { get; set; }
        public RudderViewModel RudderVM { get; set; }
        public StatisticsViewModel StatisticsVM { get; set; }
    }
}
