using System;

namespace DataModelLayer
{
    public class Event
    {
        public int ID { get; set; }
        public int PlanID { get; set; }
        public EventType Type { get; set; }
        public DateTime EventDate { get; set; }
        public string Notes { get; set; }
    }
}