using System.Collections.Generic;

namespace UserSample.Models
{
    public class User
    {
        public List<string> betaAccess { get; set; } 
        public List<string> capabilities { get; set; }
        public int? defaultContactViewId { get; set; }
        public string emailAddress { get; set; }
        public int? id { get; set; }
        public string loginName { get; set; }
        public string name { get; set; }
        public List<ProductPermission> productPermissions { get; set; } 
        public List<TypePermissions> typePermissions { get; set; } 
    }
}
