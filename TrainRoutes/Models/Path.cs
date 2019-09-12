using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainRoutes.Models
{
    public class Path
    {
        public int Train_Id { get; set; }

        public string TrainName { get; set; }

        public int Departure_WeekDay { get; set; }

        public int Arrival_WeekDay { get; set; }

        public string Previous_Station { get; set; }

        public string Current_Station { get; set; }

        public string Next_Station { get; set; }

        public string Route_Name { get; set; }

        public string Route_Id { get; set; }
    }
}
