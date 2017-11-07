using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateInMemory
{
    interface ISQLBuilder
    {
        string Generate();
    }
    public class Table : ISQLBuilder
    {
        public string TableName { get; set; }

        public ColumnDefSet ColumnDefSet { get; set; }

        public string Generate()
        {
            return $"CREATE TABLE {TableName} ({ColumnDefSet.Generate()})";
        }
    }
    public class ColumnDefSet : HashSet<SQLBuilder>, ISQLBuilder
    {
        public string Generate()
        {
           string result =  this.Select(s => s.Generate())
                .Aggregate((a,b) => { return a + "," + b; });
            return result;
        }
    }

    public class SQLBuilder : ISQLBuilder
    {
        public string ColumnName { get; set; }

        public string TypeName { get; set; }

        public string ColumnConstraint { get; set; }

        public string Generate()
        {
            return $"{ColumnName} {TypeName} {ColumnConstraint}";
        }
    }

    public class ColumnConstraint
    {
        public string Name { get; set; }

    }
}
