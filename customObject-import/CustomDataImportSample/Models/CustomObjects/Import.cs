using System.Collections.Generic;

namespace CustomDataImportSample.Models.CustomObjects
{
    public class Import
    {
        public Dictionary<string, string> fields { get; set; }
        public string importPriorityUri { get; set; }
        public string identifierFieldName { get; set; }
        public bool isSyncTriggeredOnImport { get; set; }
        public long? kbUsed { get; set; }
        public string name { get; set; }
        public int? secondsToRetainData { get; set; }
        public int? secondsToAutoDelete { get; set; }
        public List<SyncAction> syncActions { get; set; }
        public RuleType? updateRule { get; set; }
        public string uri { get; set; }
    }
}
