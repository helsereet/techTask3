using System.Collections.Generic;
using System.Linq;
using TrainRoutes.Models.Interfaces;

namespace TrainRoutes.Models
{
    public class MatchedTrainsFinder
    {
        private readonly IPathRepository _pathRepository;

        private readonly List<Path> _paths;

        public string StartStationInput { get; set; }

        public string EndStationInput { get; set; }

        public int ArrivalWeekDayInput { get; set; }

        private List<MatchedTrain> MatchedTrains = new List<MatchedTrain>();

        public MatchedTrainsFinder(IPathRepository pathRepository)
        {
            _pathRepository = pathRepository;
            //_trainRepository = trainRepository;

            _paths = _pathRepository.GetPaths();
        }

        public List<MatchedTrain> FindMatchedTrains()
        {
            var dict = BuildDictionary();
            FindMatchedTrains(dict);
            return MatchedTrains;
        }

        private void FindMatchedTrains(Dictionary<string, Dictionary<string, List<Path>>> trainsPaths)
        {
            foreach (var train in trainsPaths)
            {
                foreach (var path in train.Value)
                {
                    if (IsPathMatching(path.Value))
                    {
                        AddTrain(path.Value[0]);
                        break;
                    }
                }
            }
        }

        public void AddTrain(Path path)
        {
            MatchedTrains.Add(new MatchedTrain
            {
                Id = path.Train_Id,
                Name = path.TrainName
            });
        }

        public Dictionary<string, Dictionary<string, List<Path>>> BuildDictionary()
        {
            var dict = new Dictionary<string, Dictionary<string, List<Path>>>();

            // initialize 
            foreach (var path in _paths)
            {
                if (!dict.ContainsKey(path.TrainName))
                    dict[path.TrainName] = new Dictionary<string, List<Path>>();
                if (!dict[path.TrainName].ContainsKey(path.Route_Name))
                    dict[path.TrainName][path.Route_Name] = new List<Path>();
                dict[path.TrainName][path.Route_Name].Add(path);
            }

            return dict;
        }

        public bool IsPathMatching(List<Path> trainPath)
        {
            bool startStationAndDateMatched, endStationMatched;
            startStationAndDateMatched = endStationMatched = false;

            //find start station (previous station is null)
            var currentStation = trainPath.Where(path => path.Previous_Station == null).SingleOrDefault();

            while (currentStation.Next_Station != null)
            {
                if (currentStation.Current_Station == StartStationInput &&
                    currentStation.Arrival_WeekDay == ArrivalWeekDayInput)
                    startStationAndDateMatched = true;
                if (startStationAndDateMatched && currentStation.Current_Station == EndStationInput)
                    endStationMatched = true;

                currentStation = trainPath.Where(path => path.Current_Station == currentStation.Next_Station).SingleOrDefault();
            }

            if (currentStation.Current_Station == EndStationInput)
                endStationMatched = true;

            return startStationAndDateMatched && endStationMatched;
        }
    }
}
