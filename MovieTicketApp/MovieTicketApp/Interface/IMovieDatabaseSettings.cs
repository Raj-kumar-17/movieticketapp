using System;
namespace MovieTicketApp.Interface
{
	public interface IMovieDatabaseSettings
	{
        public string UserCollectionName { get; set; }
        public string MovieCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

