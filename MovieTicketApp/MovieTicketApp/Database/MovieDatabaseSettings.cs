using System;
using MovieTicketApp.Interface;
namespace MovieTicketApp.Database
{
	
        public class MovieDatabaseSettings : IMovieDatabaseSettings
        {
            public string UserCollectionName { get; set; }
            public string MovieCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }
    
}

