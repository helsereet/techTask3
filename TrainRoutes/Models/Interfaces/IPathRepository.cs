using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainRoutes.Models.Interfaces
{
    public interface IPathRepository
    {
        List<Path> GetPaths();
    }
}
