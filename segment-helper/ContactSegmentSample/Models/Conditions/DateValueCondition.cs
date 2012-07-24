namespace ContactSegmentSample.Models.Conditions
{
    public class DateValueCondition : ValueCondition
    {
        public int? id { get; set; }
        public string @operator { get; set; }
        public string type { get; set; }
        public RelativeDate value { get; set; }
    }
}
