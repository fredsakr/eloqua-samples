namespace ContactImportSample.RequestObjects
{
    public enum RuleType
    {
        always = 1,
        ifNewIsNotNull = 2,
        ifExistingIsNull = 6,
        useFieldRule = 17,
    }
}
