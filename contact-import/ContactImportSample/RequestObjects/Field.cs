namespace ContactImportSample.RequestObjects
{
    public class Field
    {
        public string defaultValue { get; set; }
        public string internalName { get; set; }
        public bool? hasReadOnlyConstraint { get; set; }
        public bool? hasNotNullConstraint { get; set; }
        public bool? hasUniquenessConstraint { get; set; }
        public string name { get; set; }
        public string statement { get; set; }
    }
}
