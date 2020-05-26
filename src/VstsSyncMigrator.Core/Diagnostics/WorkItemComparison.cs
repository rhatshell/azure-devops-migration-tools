using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VstsSyncMigrator.Core.Diagnostics
{
    public class WorkItemComparison
    {
        public WorkItemComparison()
        {
            this.InSourceButNotTarget = new List<FieldComparison>();
            this.InTargetButNotSource = new List<FieldComparison>();
            this.InSourceAndTarget = new List<FieldComparison>();
        }
        public string SourceType { get; set; }
        public string TargetType { get; set; }
        public List<FieldComparison> InSourceButNotTarget { get; set; }
        public List<FieldComparison> InTargetButNotSource { get; set; }
        public List<FieldComparison> InSourceAndTarget { get; set; }
    }
}
