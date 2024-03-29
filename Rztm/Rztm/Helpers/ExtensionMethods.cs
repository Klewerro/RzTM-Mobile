﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using Plugin.Geolocator.Abstractions;
using Rztm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Rztm.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Adding downloaded using RTM API departures and description to busStop downloaded from database.
        /// </summary>
        /// <param name="busStop">BusStop from database, which dont have departures and description</param>
        /// <param name="downloadedBusStop">BusStop downloaded from API, which have all busStop data</param>
        public static void AddDownloadedData(this BusStop busStop, BusStop downloadedBusStop)
        {
            busStop.Description = downloadedBusStop.Description;
            busStop.Departures = downloadedBusStop.Departures;
            busStop.CoursingLines = downloadedBusStop.CoursingLines;
        }

        public static void AddRange<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                observableCollection.Add(item);
            }
        }

        public static Position ConvertBusStopToPositon(this BusStop busStop)
            => new Position(busStop.Latitude, busStop.Longitude);

        public static bool IsZeroPosition(this Position position)
            => (position.Latitude == 0 && position.Longitude == 0 && position.Accuracy == 0) ? true : false;

        public static bool IsGeolocationAvailableAndEnabled(this IGeolocator locator)
            => locator.IsGeolocationAvailable && locator.IsGeolocationEnabled;

        public static void AddRouteIdsToDepartures(this List<Departure> departures, List<(int routeId, string number)> stopRouteList)
        {
            foreach (var departure in departures)
            {
                departure.RouteId = stopRouteList
                    .SingleOrDefault(x => x.number.Equals(departure.Number))
                    .routeId;
            }
        }
    }
}
