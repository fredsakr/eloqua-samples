using ContactImportHelper.Models;

namespace ContactImportSample.RequestObjects
{
    public class SyncAction
    {
        public SyncActionType? action { get; set; }
        public string destinationUri { get; set; }
    }
}
