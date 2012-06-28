namespace ContactExportSample.Models
{
    public enum FilterRuleType
    {                
        member = 0,
        pendingMember = 1,
        activeMember = 2,
        subscribedMember = 3,
        unsubscribedMember = 4,
        valueEqualsComparisonValue,
        valueDoesNotEqualComparisonValue,
        valueGreaterThanComparisonValue,
        valueGreaterThanOrEqualToComparisonValue,
        valueLessThanComparisonValue,
        valueLessThanOrEqualToComparisonValue
    }
}
