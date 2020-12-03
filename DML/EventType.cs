namespace DataModelLayer
{
    public class EventType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAStage { get; set; }
        public int StageID { get; set; }
        public string CurrentStageName { get; set; }
        public bool AllowMultiple { get; set; } = true;

        public virtual EventDurationCategory EventDurationCategory { get; set; }
    }
}