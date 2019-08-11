﻿using Prism.Commands;
using Prism.Navigation;
using Rtm.Models;
using Rtm.Repositories;
using Rtm.Services;
using Rtm.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rtm.ViewModels
{
    public class ListPageVM : ViewModelBase
    {
        private RtmService _rtmService;
        private IBusStopRepository _busStopRepository;

        private List<BusStop> _busStops;
        private string _searchText;

        public List<BusStop> BusStops
        {
            get => _busStops;
            set => SetProperty(ref _busStops, value);
        }

        public ReadOnlyCollection<BusStop> BusStopsAll { get; set; }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }


        public ListPageVM(INavigationService navigationService) : base(navigationService)
        {
            _rtmService = new RtmService();
            _busStopRepository = new BusStopRepository();

            DownloadBusStopsIfEmpty();
        }


        public ICommand RefreshCommand => new DelegateCommand(async () =>
        {
            IsBusy = true;
            var result = await _rtmService.GetAllBusStops();
            BusStopsAll = result.AsReadOnly();
            BusStops = result;
            IsBusy = false;
        });

        public ICommand SearchCommand => new DelegateCommand(async () =>
        {
            BusStops = BusStopsAll.Where(b => b.Name.ToLower().Contains(SearchText.ToLower())).ToList();
        });

        public ICommand DownloadBusStopsCommand => new DelegateCommand(async () =>
        {
            IsBusy = true;
            var result = await _rtmService.GetAllBusStops();
            BusStopsAll = result.AsReadOnly();
            BusStops = result;
            
            IsBusy = false;
        });

        public ICommand ItemTappedCommand => new DelegateCommand<BusStop>(async busStop =>
        {
            var parameters = new NavigationParameters();
            parameters.Add("busStopIp", busStop.Id);
            await NavigationService.NavigateAsync(nameof(BusStopPage), parameters);
        });

        private async Task DownloadBusStopsIfEmpty()
        {
            var repositoryStops = _busStopRepository.GetAll();
            if (!repositoryStops.Any())
            {
                var result = await _rtmService.GetAllBusStops();
                _busStopRepository.AddRange(result);
                BusStopsAll = result.AsReadOnly();
                BusStops = result;
            }
            else
            {
                BusStopsAll = repositoryStops.AsReadOnly();
                BusStops = repositoryStops;
            }
        }

    }
}
