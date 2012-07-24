namespace ContactSegmentSample.Models.Conditions
{
    public class NumericValueCondition : ValueCondition
    {
        public int? id { get; set; }
        public string @operator { get; set; }
        public string type { get; set; }
        public int? value { get; set; }
    }
}
