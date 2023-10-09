using Assessment.DTO.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Helpers
{
    
    public class RepositoryFactory
    {
        private readonly IConfiguration _config;
        public RepositoryFactory(IConfiguration config)
        {
            _config = config;
        }

        // this method gets our connection by reading the db enum and connection type
        public dynamic GetMyConnection(Databases databaseName, ConnectionType connectionType)
        {
            switch (connectionType)
            {
                case ConnectionType.Dapper:
                    return ConnectionStringFactory.GetConnection(_config.GetConnectionString(databaseName.ToString()));
                default:
                    return null;
            }
        }
    }
}
