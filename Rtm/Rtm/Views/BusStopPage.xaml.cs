﻿using Rtm.Models;
using Rtm.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rtm.Views
{
    [DesignTimeVisible(false)] 
    public partial class BusStopPage : ContentPage
    {
        BusStopPageVM viewModel;

        public BusStopPage()
        {
            InitializeComponent();
        }

        public BusStopPage(BusStop busStop)
        {
            InitializeComponent();
            viewModel = BindingContext as BusStopPageVM;
            viewModel.BusStop = busStop;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}