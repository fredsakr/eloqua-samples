namespace CustomObjectDataSample.Models
{
    public class Field
    {
        public string dataType { get; set; }
        public bool? hasNotNullConstraint { get; set; }
        public bool? hasReadOnlyContraint { get; set; }
        public bool? hasUniquenessConstraint { get; set; }
        public string internalName { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
    }
}
