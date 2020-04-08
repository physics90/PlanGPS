﻿namespace DataModelLayer
{
    public class EventType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAStage { get; set; }
        public string StageID { get; set; }
        public string CurrentStageName { get; set; }
    }
}