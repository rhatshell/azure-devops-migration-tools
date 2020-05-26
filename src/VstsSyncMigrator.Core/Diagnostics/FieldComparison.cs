using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VstsSyncMigrator.Core.Diagnostics
{
    public sealed class FieldComparison
    {
        public FieldComparison(string referenceName, string displayName, object sourceValue, object targetValue)
        {
            this.ReferenceName = referenceName;
            this.DisplayName = displayName;
            this.SourceValue = sourceValue;
            this.TargetValue = targetValue;
            this.IsMatch = this.SourceValue == this.TargetValue;
        }

        public string ReferenceName { get; private set; }
        public string DisplayName { get; private set; }
        public object SourceValue { get; private set; }
        public object TargetValue { get; private set; }
        public bool IsMatch { get; private set; }
    }
}
