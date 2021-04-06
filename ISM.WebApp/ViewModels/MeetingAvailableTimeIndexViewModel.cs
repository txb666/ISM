using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class MeetingAvailableTimeIndexViewModel
    {
        public List<AvailableTime> availableTimes { get; set; }
        public List<MeetingSchedule> meetingRegisters { get; set; }
        public List<MeetingSchedule> meetingSchedules { get; set; }
        public DateTime? date { get; set; }
        public TimeSpan? start_time { get; set; }
        public TimeSpan? end_time { get; set; }
        public int page { get; set; }
        public int totalPage { get; set; }
        public int pageSize { get; set; }
    }
}
