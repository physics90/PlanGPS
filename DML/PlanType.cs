namespace DataModelLayer
{
    public class PlanType
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsLOT { get; set; }
        public string DesiredColor { get; set; }
    }
}