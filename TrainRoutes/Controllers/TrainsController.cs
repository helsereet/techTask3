using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainRoutes.Models;
using TrainRoutes.Models.Interfaces;

namespace TrainRoutes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly IPathRepository _pathRepository;

        public TrainsController(IPathRepository pathRepository)
        {
            _pathRepository = pathRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MatchedTrain>> GetTrains(string startStation, string endStation, int arrivalWeekDay)
        {
            if (string.IsNullOrEmpty(startStation) || string.IsNullOrEmpty(endStation) ||
                (arrivalWeekDay < 1 || arrivalWeekDay > 7))
            {
                return BadRequest();
            }

            var trainFinder = new MatchedTrainsFinder(_pathRepository)
            {
                StartStationInput = startStation,
                EndStationInput = endStation,
                ArrivalWeekDayInput = arrivalWeekDay
            };

            return trainFinder.FindMatchedTrains();
        }
    }
}