using System;
using System.Collections.Generic;

namespace VstsSyncMigrator.Engine.Configuration
{
    public class WorkItemFieldsToHistoryConfig
    {
        public string HeaderMessage { get; set; }
        public Dictionary<string, WorkItemFieldMappings> WorkItemFieldMappings { get; set; }
    }
}