﻿using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace AirQualityApi.Models
{
    public class StationWithParams
    {
        public string StationName { get; set; }
        public int StationId { get; set; }
        public List<Param> Param { get; set; } 
    }
}
