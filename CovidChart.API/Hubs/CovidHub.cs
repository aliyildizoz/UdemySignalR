﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CovidChart.API.Hubs
{
    public class CovidHub:Hub
    {
        public async Task GetCovidList()
        {
            await Clients.All.SendAsync("ReceiveCovidList", "serviceden covid19 verilerini al");
        }
    }
}