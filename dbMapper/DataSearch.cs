using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web.Script.Serialization;

namespace DBMapper
{

    public enum CompareType
    {
        Like,
        Equals,
        Greater,
        GreaterEquals,
        Less,
        LessEquals,
        In,
        Between,
        Likes
    }

    public class CompareValue
    {
        public static string ValueSeparator = ";";
        [ScriptIgnore]
        public string Name { get; set; }
        public CompareType Compare { get; set; }
        public bool NOT { get; set; }

        [ScriptIgnore]
        public bool HasDelimeter { get; set; }
        public string Value { get; set; }
        [ScriptIgnore]
        public List<string> Values
        {
            get
            {
                var values = new List<string>();
                if (!string.IsNullOrEmpty(Value))
                {
                    values.AddRange(Value.Split(new string[] { ValueSeparator }, StringSplitOptions.RemoveEmptyEntries));
                }
                return values;
            }
        }

        public static string CompareTypeText(CompareType type)
        {
            switch (type)
            {
                case CompareType.Equals: return "=";
                case CompareType.Like: return "LIKE";
                case CompareType.Greater: return ">";
                case CompareType.GreaterEquals: return ">=";
                case CompareType.Less: return "<";
                case CompareType.LessEquals: return "<=";
                case CompareType.In: return "IN";
                case CompareType.Between: return "BETWEEN";
                case CompareType.Likes: return "LIKES";
            }
            return "";
        }

        [ScriptIgnore]
        public string ComparePattern
        {
            get
            {
                var delimeter = HasDelimeter || Compare == CompareType.Like || Compare == CompareType.Likes ? "'" : "";
                var delimeterPrefix = string.IsNullOrEmpty(delimeter) ? "" : "N";
                var prefix = "";
                var suffix = "";
                var mvalues = Values;
                var likePattern = Compare == CompareType.Like &&
                    (string.IsNullOrEmpty(Value) || Value.IndexOfAny(new char[] { '_', '%' }) < 0) ? "%" : "";
                var cvalue = string.Format("{3}{0}{2}{1}{2}{0}", delimeter, Value, likePattern, delimeterPrefix);
                var compareText = CompareTypeText(Compare);
                if (Compare == CompareType.Between)
                {
                    cvalue = string.Format("{2}{0}{1}{0}", delimeter, mvalues[0], delimeterPrefix);
                    if (mvalues.Count > 1 && !string.IsNullOrEmpty(mvalues[1]))
                    {
                        cvalue = string.Format("{2} AND {3}{0}{1}{0}", delimeter, mvalues[1], cvalue, delimeterPrefix);
                    }
                    else if (mvalues.Count == 1 && string.IsNullOrEmpty(mvalues[0]))
                    {
                        compareText = "IS";
                        cvalue = "NULL";
                    }
                    else
                        compareText = CompareTypeText(CompareType.GreaterEquals);
                }
                else if (Compare == CompareType.In)
                {
                    if (mvalues.Count == 1 && string.IsNullOrEmpty(mvalues[0]))
                    {
                        compareText = "IS";
                        cvalue = "NULL"; 
                    }
                    else
                    {
                        cvalue = string.Format("{0}{1}{2}{1}", delimeterPrefix, delimeter, string.Join(delimeter + ", " + delimeterPrefix + delimeter, mvalues));
                        prefix = "(";
                        suffix = ")";
                    }
                }
                var result = "";
                if (Compare == CompareType.Likes)
                {
                    compareText = "LIKE";
                    for (var i = 0; i < mvalues.Count; i++)
                    {
                        if (string.IsNullOrEmpty(mvalues[i]) || mvalues[i].IndexOfAny(new char[] { '_', '%' }) < 0) mvalues[i] = $"%{mvalues[i]}%";

                    }
                    cvalue = string.Format("{0}{1}{2}{1}", delimeterPrefix, delimeter, string.Join(delimeter + " OR {1}[{0}] LIKE " + delimeterPrefix + delimeter, mvalues));
                    if (!NOT)
                    {
                        prefix = "(";
                        suffix = ")";
                    }
                    result = string.Format("{1}{{1}}[{{0}}] {0} {2}{3}", compareText, prefix, cvalue, suffix);
                }
                else
                {
                    result = string.Format("{{1}}[{{0}}] {0} {1}{2}{3}", compareText, prefix, cvalue, suffix);
                }
                return NOT ? string.Format("NOT ({0})", result) : result;
            }
        }
        public override string ToString()
        {
            return string.Format(ComparePattern, Name, "");
        }

        public string BuildWhereToken(string tableAlias, string columnName, string linkOperator = "")
        {
            if (!string.IsNullOrEmpty(tableAlias)) tableAlias += ".";
            return string.IsNullOrEmpty(Value) ? "" : string.Format("{0} {1}", linkOperator, string.Format(ComparePattern, columnName, tableAlias)).Trim();
        }
    }

    public class DataSearchOptions
    {
        public List<string> Databases { get; set; }
        public bool SearchInTables { get; set; }
        public bool SearchInViews { get; set; }
        public CompareValue CompareSchema { get; set; }
        public CompareValue CompareObject { get; set; }
        public CompareValue CompareColName { get; set; }
        public CompareValue CompareColType { get; set; }
        public CompareValue CompareContent { get; set; }

        [ScriptIgnore]
        public string SearchIn
        {
            get
            {
                if (SearchInTables && !SearchInViews) return "= 'U'";
                if (!SearchInTables && SearchInViews) return "= 'V'";
                return "IN ('U', 'V')";
            }
        }

        [ScriptIgnore]
        public string MetadataSelect
        {
            get
            {
                var sqlSchema = @"select 
o.type, s.name as schemaname, o.name as objectname, 
--o.modify_date, o.create_date, o.object_id, 
c.name as columnname, c.column_id, t.name as typename, c.max_length
from sys.objects o 
inner join sys.schemas s on s.schema_id=o.schema_id
inner join sys.columns c on c.object_id = o.object_id
inner join sys.types t on t.user_type_id = c.user_type_id -- t.system_type_id = c.system_type_id
where o.type {0}
    {1}
    {2}
    {3}
    {4}
order by s.name, o.type, o.name, c.column_id";
                return string.Format(sqlSchema
                    , SearchIn
                    , CompareSchema == null ? "" : CompareSchema.BuildWhereToken("s", "name", "AND")
                    , CompareObject == null ? "" : CompareObject.BuildWhereToken("o", "name", "AND")
                    , CompareColName == null ? "" : CompareColName.BuildWhereToken("c", "name", "AND")
                    , CompareColType == null ? "" : CompareColType.BuildWhereToken("t", "name", "AND")
                    );
            }
        }
    }

    public class DataSearchColumn
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public string TypeName { get; set; }
        public int RowsCount { get; set; }
        public int MaxLength { get; set; }

        public bool IsNumber { get { return DataTypes_Numbers.Contains(TypeName); } }

        public string FullTypeName
        {
            get
            {
                return "char, varchar, nchar, nvarchar".IndexOf(TypeName) >= 0 && MaxLength > 0 && MaxLength <= 0x7fff ? string.Format("{0}({1})", TypeName, MaxLength) : TypeName;
            }
        }
        public static readonly List<string> NotLikeableTypes = new List<string> { "geography", "geometry", "xml" };

        public const string DataTypes_String = "char, varchar, text, nchar, nvarchar, ntext, xml, uniqueidentifier";
        public const string DataTypes_Datetime = "date, datetimeoffset, datetime2, smalldatetime, datetime, time";
        public const string DataTypes_Numbers = "bigint, numeric, bit, smallint, decimal, smallmoney, int, tinyint, money, float, real";
        public const string DataTypes_Binary = "cursor, timestamp, hierarchyid, sql_variant, table, geometry, geography, binary, varbinary, image";
        /*
         * select name, is_nullable, max_length, 
         * case when charindex('date', name)>0 then 1 when 'time' = name then 1 when precision <> 0 then 0 else 1 end as has_delimeter 
         * from master.sys.types order by name
         */

        public bool IsLikeable
        {
            get { return !NotLikeableTypes.Contains(TypeName); }
        }
        public bool HasDelimeter
        {
            get { return (DataTypes_String + DataTypes_Datetime).IndexOf(TypeName) >= 0; }
        }
    }
    public class DataSearchObject
    {
        public DataSearch Parent { get; set; }
        public bool IsView { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public List<DataSearchColumn> Columns { get; set; }

        public List<DataSearchColumn> AllColumns { get; set; }
        public IEnumerable<DataSearchColumn> FoundColumns
        {
            get
            {
                return Columns.Where(c => c.RowsCount > 0);
            }
        }
        public bool AnyFound { get; private set; }

        public bool Cancelling { get; set; }

        public object UIObject { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}].[{1}]", Schema, Name);
        }

        public void Search()
        {
            Cancelling = false;
            var columnQueries = new List<string>();
            foreach(var column in Columns)
            {
                var whereClausel = Parent.BuildContentQuery(column);
                if (!string.IsNullOrEmpty(whereClausel))
                    columnQueries.Add(string.Format("select '{0}', count([{0}]) from [{1}].[{2}] where {3}",
                    column.Name, Schema, Name, whereClausel));
            }
            using (var conn = new SqlConnection(ConnectionString))
            {
                //Debug.Print("OPEN-CONN {0}: {1}", this, ConnectionString);
                conn.Open();
                if (Columns.Count > 0) columnQueries.Insert(0, string.Join("\r\nUNION\r\n", columnQueries));
                var firstCmd = true;
                foreach (var cmdtxt in columnQueries)
                {
                    if (!string.IsNullOrEmpty(cmdtxt))
                        using (var cmd = new SqlCommand(cmdtxt, conn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = cmdtxt;
                        //Debug.Print("SQL-EXEC {0}: {1}", this, cmd.CommandText);
                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (!Cancelling && reader.Read())
                            {
                                var cname = reader.GetString(0);
                                var rcount = reader.GetInt32(1);
                                if (rcount > 0) AnyFound = true;
                                Columns.First(c => c.Name == cname).RowsCount = rcount;
                            }
                            if (firstCmd) break;
                        }
                        catch (Exception ex)
                        {
                            Debug.Print("*** SQL-ERROR *** {0} in: {1}", ex.Message, cmdtxt);
                            firstCmd = false;
                        }
                    }
                }
            }
            Cancelling = false;
        }
        public void FillAllColumns(SqlConnection conn)
        {
            if (AllColumns.Count > 0) return;
            var allColsOptions = new DataSearchOptions
            {
                SearchInTables = !IsView,
                SearchInViews = IsView,
                CompareSchema = new CompareValue { Compare = CompareType.Equals, HasDelimeter = true, Value = Schema },
                CompareObject = new CompareValue { Compare = CompareType.Equals, HasDelimeter = true, Value = Name }
            };
            using (var ccmd = new SqlCommand(allColsOptions.MetadataSelect, conn))
            {
                using (var creader = ccmd.ExecuteReader())
                {
                    while (creader.Read())
                    {
                        AllColumns.Add(new DataSearchColumn
                        {
                            Name = (creader.GetString(3) ?? "").Trim(),
                            Index = creader.GetInt32(4),
                            TypeName = (creader.GetString(5) ?? "").Trim(),
                            MaxLength = creader.GetInt16(6)
                        });
                    }
                }
            }
        }
    }

    public class DataSearch
    {
        public string DbName { get; set; }
        public DataSearchOptions Options { get; set; }

        public List<DataSearchObject> Objects { get; set; }

        public string BuildContentQuery(DataSearchColumn column, string tableAlias = "", string linkOperator="")
        {
            //TODO: check delimeter
            Options.CompareContent.HasDelimeter = Options.CompareContent.Compare == CompareType.Like || column.HasDelimeter ||
            !Options.CompareContent.Value.IsNumber() || column.HasDelimeter;

            return Options.CompareContent.Compare == CompareType.Like && !column.IsLikeable
                ? ""
                : Options.CompareContent.BuildWhereToken(tableAlias, column.Name, linkOperator);
        }

        public void BuildObjects(string connstr, Func<DataSearchObject, bool> buildObjCallback)
        {
            Objects = new List<DataSearchObject>();
            using (var conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Options.MetadataSelect, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataSearchObject obj = null;
                        while (reader.Read())
                        {
                            var otype = reader.GetString(0).Trim();
                            var schema = reader.GetString(1);
                            var oname = reader.GetString(2);
                            var cname = reader.GetString(3);
                            var cid = reader.GetInt32(4);
                            var ctype = reader.GetString(5);
                            var objname = string.Format("[{0}].[{1}]", schema, oname);
                            if (obj == null || obj.ToString() != objname)
                            {
                                obj = new DataSearchObject
                                {
                                    Parent = this,
                                    IsView = otype == "V",
                                    Schema = schema,
                                    Name = oname,
                                    ConnectionString = connstr,
                                    Columns = new List<DataSearchColumn>(),
                                    AllColumns = new List<DataSearchColumn>()
                                };
                                if (!buildObjCallback(obj)) return;
                                Objects.Add(obj);
                            }
                            obj.Columns.Add(new DataSearchColumn
                            {
                                Name = cname,
                                Index = cid,
                                TypeName = ctype
                            });
                        }
                    }
                }
            }
        }
    }
}
