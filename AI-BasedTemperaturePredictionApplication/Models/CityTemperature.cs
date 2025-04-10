using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AI_BasedTemperaturePredictionApplication.Models
{
    public class CityTemperature
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public float Temperature { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}