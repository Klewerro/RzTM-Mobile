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
        public BusStopPage()
        {
            InitializeComponent();
        }

        private void RenameToolbarItem_Clicked(object sender, EventArgs e)
        {
            customNameGrid.IsVisible = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            customNameGrid.IsVisible = false;
        }
    }
}