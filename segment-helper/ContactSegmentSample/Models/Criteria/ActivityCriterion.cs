namespace ContactSegmentSample.Models.Criteria
{
    public class ActivityCriterion : Criterion
    {
        public Conditions.Condition activityRestriction { get; set; }
        public Conditions.Condition timeRestriction { get; set; }
    }
}
