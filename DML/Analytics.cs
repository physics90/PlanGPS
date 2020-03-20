namespace DataModelLayer
{
    public class Analytics
    {
        public int ID { get; set; }
        public int PlanID { get; set; }
        public double ImportDays { get; set; }
        public double ImagingDays { get; set; }
        public double ContouringDays { get; set; }
        public double PlanningDays { get; set; }
        public double ReviewDays { get; set; }
    }
}