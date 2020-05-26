using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.Services.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VstsSyncMigrator.Core.Diagnostics
{
    class WorkItemComparer
    {
        public WorkItemComparison Compare(WorkItem source, WorkItem target)
        {
            var comparison = new WorkItemComparison();
            comparison.SourceType = source.Type.Name;
            comparison.TargetType = target.Type.Name;
            
            source.Fields.AsDictionary().Keys.ForEach(key =>
            {
                if (target.Fields.Contains(key))
                {
                    comparison.InSourceAndTarget.Add(new FieldComparison(source.Fields[key].ReferenceName, key, source.Fields[key].Value, target.Fields[key].Value));
                } else
                {
                    comparison.InSourceButNotTarget.Add(new FieldComparison(source.Fields[key].ReferenceName, key, source.Fields[key].Value, this.GetDefaultValue(source.Fields[key].FieldDefinition.SystemType)));
                }
            });
            target.Fields.AsDictionary().Keys.ForEach(key =>
            {
                if (!source.Fields.Contains(key))
                {
                    comparison.InTargetButNotSource.Add(new FieldComparison(target.Fields[key].ReferenceName, key, this.GetDefaultValue(target.Fields[key].FieldDefinition.SystemType), target.Fields[key].Value));
                }
            });

            return comparison;
        }

        public string ToJSON(WorkItemComparison comparison)
        {
            return JsonConvert.SerializeObject(comparison);
        }

        public string ToCSV(WorkItemComparison comparison)
        {
            var sb = new StringBuilder();
            comparison.InSourceAndTarget.ForEach(field =>
            {
                sb.AppendLine(ToRow("In Source AND Target", comparison.SourceType,comparison.TargetType, field));
            });
            comparison.InSourceButNotTarget.ForEach(field =>
            {
                sb.AppendLine(ToRow("In Source But NOT Target", comparison.SourceType,comparison.TargetType, field));
            });
            comparison.InTargetButNotSource.ForEach(field =>
            {
                sb.AppendLine(ToRow("In Target But NOT Source", comparison.SourceType,comparison.TargetType, field));
            });

            return sb.ToString();
        }

        private string ToRow(string category, string sourceType, string targetType, FieldComparison comparison)
        {
            var sb = new StringBuilder($"{category},{sourceType},{targetType},");
            Type t = comparison.GetType();
            t.GetProperties().ForEach(prop => sb.Append($"{prop.GetValue(comparison)},"));
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }
    }
}
