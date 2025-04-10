using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AI_BasedTemperaturePredictionApplication.Models;
using AI_BasedTemperaturePredictionApplication.Services;

namespace AI_BasedTemperaturePredictionApplication.Controllers
{
    public class IndexController : Controller
    {
        private readonly WeatherService weatherService = new WeatherService();
        private readonly TemperatureRepository temperatureRepo = new TemperatureRepository();

        public async Task<ActionResult> Index()
        {
            List<string> cities = new List<string> { "Mumbai", "New York", "London", "Tokyo" };
            List<CityTemperature> temperatures = new List<CityTemperature>();

            foreach (var city in cities)
            {
                float temp = await weatherService.GetTemperatureAsync(city);
                var tempData = new CityTemperature { CityName = city, Temperature = temp, RecordedDate = DateTime.Now };

                temperatureRepo.SaveTemperature(tempData);
                temperatures.Add(tempData);
            }

            return View(temperatures);
        }

        public ActionResult PredictTemperature(int days)
        {
            string pythonScript = @"C:\Users\BS5939\source\repos\AI-BasedTemperaturePredictionApplication\AI-BasedTemperaturePredictionApplication\temperature_prediction.py";
            string pythonExe = @"C:\path_to_python\python.exe";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = pythonExe,
                Arguments = $"\"{pythonScript}\" {days}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = psi };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return Json(new { predictedTemperature = output }, JsonRequestBehavior.AllowGet);
        }
    }
}
