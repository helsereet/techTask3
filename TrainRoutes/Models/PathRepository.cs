using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainRoutes.Models.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace TrainRoutes.Models
{
    public class PathRepository : IPathRepository
    {
        private readonly IConfiguration _config;

        public PathRepository(IConfiguration configuration) => _config = configuration;

        public List<Path> GetPaths()
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var result = connection.Query<Path>("dbo.GetPathResult").ToList();
                return result;
            }
        }
    }
}
