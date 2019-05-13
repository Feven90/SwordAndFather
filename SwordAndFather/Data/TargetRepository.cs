using System;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using SwordAndFather.Models;

namespace SwordAndFather.Data
{
    public class TargetRepository
    {
        readonly string _connectionString;

        public TargetRepository(IOptions<DbConfiguration> dbConfig)
        {
            _connectionString = dbConfig.Value.ConnectionString;
        }
        public Target AddTarget(string name, string location, FitnessLevel fitnessLevel, int userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var insertQuery = @"
                    INSERT INTO [dbo].[Targets]
                               ([Location]
                               ,[Name]
                               ,[FitnessLevel]
                               ,[UserId])
                    output inserted.*
                         VALUES
                               (@location
                                ,@name
                               ,@fitnessLevel
                               ,@userId)";

                var parameters = new
                { Name = name,
                    Location = location,
                    FitnessLevel = fitnessLevel,
                    UserId = userId
                };    // or we can use object litteral =  new { name, 

                var newTarget = db.QueryFirstOrDefault<Target>(insertQuery, parameters);
                if (newTarget != null)
                {
                    return newTarget;
                }

                throw new Exception("Could not create target");
            }
        }
    }
}
