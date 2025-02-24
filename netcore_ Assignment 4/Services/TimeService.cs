using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace netcore__Assignment_4.Services
{
    public class TimeService : ITimeService
    {
        private static readonly Lazy<TimeService> _instance = new Lazy<TimeService>(() => new TimeService());

        public TimeService() { } 
            public static TimeService Instance => _instance.Value;

        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    

    }
}
