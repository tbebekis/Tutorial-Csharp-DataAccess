 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;


/*  
 
 #if NET_CF
#else
#endif
 
 */

namespace Tripous.Data
{


    static public class DataProviders
    {
        /// <summary>
        /// a private exception class, used by the Error() methods
        /// </summary>
        private class DataProviderException : ApplicationException
        {
            public DataProviderException(string message)
                : base(message)
            {
            }
        }
 
        static private List<DataProvider> list = new List<DataProvider>();
        static private char globalPrefix = ':';

        public const string AliasKey = "Alias";

        /* Provider aliases */
        public const string MsSql = "MsSql";
        public const string SqlCe = "SqlCe";
        public const string OleDb = "OleDb";
        public const string Odbc = "Odbc";
        public const string OracleClient = "OracleClient";
        public const string Oracle = "Oracle";
        public const string Firebird = "Firebird";
        public const string SQLite = "SQLite";
        public const string OleDbFirebird = "OleDb.Firebird";        
        public const string OleDbAccess = "OleDb.Access";
        public const string OleDbExcel = "OleDb.Excel";
        public const string OleDbText = "OleDb.Text";
        public const string OleDbDbf = "OleDb.DBF";


        /// <summary>
        /// static constructor
        /// </summary>
        static DataProviders()
        {
#if !NET_CF
            Add(new OleDbProvider());
            Add(new MsAccessProvider());
            Add(new OleDbDbfProvider());
            Add(new OleDbExcelProvider());
            Add(new OdbcProvider());
            Add(new MsSqlProvider());
            Add(new OracleClientProvider());
            Add(new OracleProvider());
            Add(new FirebirdProvider());
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Error(string Text)
        {
            throw (new DataProviderException(Text));
        }
        /// <summary>
        /// 
        /// </summary>
        public static void Error(string Text, params object[] Args)
        {
            throw (new DataProviderException(string.Format(Text, Args)));
        }

        /// <summary>
        /// Searches for an instance of a Provider by Alias.
        /// </summary>
        /// <param name="Alias">The  Alias of a Provider.</param>
        /// <returns>The Provider if a match is find, else null</returns>
        static public DataProvider Find(string Alias)
        {
            for (int i = 0; i < list.Count; i++)
                if (string.Compare(list[i].Alias, Alias, StringComparison.InvariantCultureIgnoreCase) == 0)
                    return list[i];

            return null;
        }
        /// <summary>
        /// Returns DataProvider with Name. Exception if not exists.
        /// </summary>
        static public DataProvider ByName(string Alias)
        {
            DataProvider Result = Find(Alias);
            if (Result == null)
                Error("DataProvider not found: {0}", Alias);

            return Result;
        }
        /// <summary>
        /// Returns DataProvider at Index. Exception if not exists.
        /// </summary>
        static public DataProvider ByIndex(int Index)
        {
            return list[Index];
        }
        /// <summary>
        /// Determines whether a Provider is in the Providers list. 
        /// </summary>
        static public bool Contains(string Alias)
        {
            return Find(Alias) != null;
        }
        /// <summary>
        /// Adds the Provider to the internal list.
        /// </summary>
        static public void Add(DataProvider Provider)
        {
            if (!Contains(Provider.Alias))
                list.Add(Provider);
        }

        /// <summary>
        /// Extracts the <paramref name="ProviderAlias"/> and the <paramref name="ConnectionString"/> 
        /// from the <paramref name="Input"/> parameter.
        /// </summary>
        /// <remarks>The  <paramref name="Input"/> parameter is a ConnectionString that may contains a 
        /// "Alias=XXX" key-value pair. This static method parses the Input parameter
        /// and returns the contained information. The ProviderAlias entry is deleted from the ConnectionString if found </remarks>
        static public void ExtractAlias(string Input, ref string ProviderAlias, ref string ConnectionString)
        {
            ProviderAlias = "";
            ConnectionString = "";

            string[] Split = Input.Split(';');

            foreach (string item in Split)
            {
                string S = item.Trim();
                string[] KeyValuePair = S.Split('=');

                if ((string.Compare(KeyValuePair[0], AliasKey, StringComparison.InvariantCultureIgnoreCase) == 0))
                    ProviderAlias = KeyValuePair[1].Trim();
                else 
                    ConnectionString = ConnectionString + S + ";";
            }

            ConnectionString = ConnectionString.TrimEnd(';');
        }
        /// <summary>
        /// Passes Params values to Command.Parameters.
        /// Params can be
        /// 1. either a comma separated list of parameters 
        /// 2. or the Params[0] element, that is the first element in Params, may be a DataRow, generic IDictionary, IList or Array
        /// and in that case no other Params elements are used.
        /// </summary>
        static public void AssignParams(DbCommand Command, params object[] Params)
        {
            AssignParams(Command.Parameters, Params);
        }
        /// <summary>
        /// Passes Params values to Parameters.
        /// Params can be
        /// 1. either a comma separated list of parameters 
        /// 2. or the Params[0] element, that is the first element in Params, may be a DataRow, generic IDictionary, IList or Array
        /// and in that case no other Params elements are used.
        /// </summary>
        static public void AssignParams(IDataParameterCollection Parameters, params object[] Params)
        {
            if (Params == null)
                return;
 
            if (Params.Length > 0)
            {
                IDataParameter Parameter;
                for (int i = 0; i < Parameters.Count; i++)
                {
                    Parameter = (IDataParameter)Parameters[i];


                    if (Params[0] is DataRow)
                    {
                        DataRow Row = (DataRow)Params[0];
                        if (Row.Table.Columns.Contains(Parameter.ParameterName))
                            Parameter.Value = Row[Parameter.ParameterName];
                    }
                    else if (Params[0] is IDictionary<string, object>)
                    {
                        IDictionary<string, object> Dictionary = Params[0] as IDictionary<string, object>;
                        if (Dictionary.ContainsKey(Parameter.ParameterName))
                            Parameter.Value = Dictionary[Parameter.ParameterName];
                    }
                    else if (Params[0] is IList)
                    {
                        //IList List = (IList)Params[0];
                        Parameter.Value = ((IList)Params[0])[i];
                    }
                    else if (Params[0] is Array)
                    {
                        //object[] A = (object[])Params[0];
                        Parameter.Value = ((object[])Params[0])[i];
                    }
                    else
                    {
                        Parameter.Value = Params[i];
                    }
                }

            }
        }



        /// <summary>
        /// Gets the number of DataProviders.
        /// </summary>
        static public int Count { get { return list.Count; } }
        /// <summary>
        /// Gets or sets the parameter prefix to be used globally for this application.
        /// Data Providers provide methods for replacing their native ParamPrefix with this prefix.
        /// </summary>
        static public char GlobalPrefix
        {
            get { return globalPrefix; }
            set { globalPrefix = value; }
        }
    }


    /// <summary>
    /// Indicates the mode of the parameter name and prefix the native ADO.NET Data Provider uses
    /// </summary>
    public enum PrefixMode
    {
        /// <summary>
        /// The ADO.NET Data Provider expects a parameter name with a prefix, ie. @CUSTOMER_NAME or :CUSTOMER_NAME
        /// </summary>
        Prefixed,     
        /// <summary>
        /// The ADO.NET Data Provider expects no prefix or name at all. It uses the question mark as a positional placeholder
        /// </summary>
        Positional,
    }



    /// <summary>
    /// 
    /// </summary>
    public abstract class DataProvider
    {
        protected string alias = "";
        protected string description = "";
        protected char nativePrefix = ':';
        protected PrefixMode prefixMode = PrefixMode.Prefixed;
        protected bool requiresNativeParamNames = false;


        /// <summary>
        /// Returns true if C is a name delimiter.
        /// </summary>
        protected bool IsNameDelimiter(char C)
        {
            return (C == ' ') || (C == ',') || (C == ';') || (C == ')') || (C == '\n') || (C == '\r');
        }
        /// <summary>
        /// Returns true if C is a literal delimiter.
        /// </summary>
        protected bool IsLiteral(char C)
        {
            return (C == '\'') || (C == '"') || (C == '`');
        }
        /// <summary>
        /// Initializes a instance by assinging valid values to its properties.
        /// </summary>
        protected abstract void Initialize(); 


        /// <summary>
        /// constructor
        /// </summary>
        public DataProvider()
        {
            Initialize();
        }

        /// <summary>
        /// Creates and returns a DbConnection initialized with ConnectionString.
        /// </summary>
        public abstract DbConnection CreateConnection(string ConnectionString);
        /// <summary>
        /// Creates and returns a DbDataAdapter.
        /// </summary>
        public abstract DbDataAdapter CreateAdapter();
        /// <summary>
        /// Creates and returns a DbCommandBuilder
        /// </summary>
        public virtual DbCommandBuilder CreateCommandBuilder()
        {
            throw new NotSupportedException("CreateCommandBuilder");
        }
        /// <summary>
        /// Creates and returns a DbConnectionStringBuilder
        /// </summary>
        public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            throw new NotSupportedException("CreateConnectionStringBuilder");
        }
        /// <summary>
        /// Creates and returns a DbParameter
        /// </summary>
        public virtual DbParameter CreateParameter()
        {
            throw new NotSupportedException("CreateParameter");
        }


        /// <summary>
        /// Creates a DbCommand and its DbParameters based on SQL.
        /// If the Params is not null, they used to assign DbParameter values.
        /// </summary>
        public DbCommand CreateCommand(DbConnection Connection, string SQL, params object[] Params)
        {
            DbCommand Result = Connection.CreateCommand();
            if (!string.IsNullOrEmpty(SQL))
                SetupCommand(Result, SQL, Params);
            return Result;
        }
        /// <summary>
        /// 1. Analyzes the passed SQL which may contain parameter names prefixed by the DataProviders.GlobalPrefix, i.e.  
        ///       select * from CUSTOMER where ID > :ID            
        /// 2. Converts the passed SQL to be what the native ADO.NET Data Provider expects to be and assigns the Command.CommandText     
        ///    For the MS SQL the above statement would be converted to
        ///      select * from CUSTOMER where ID > @ID
        ///    and for the OleDb would be converted to
        ///      select * from CUSTOMER where ID > ?
        /// 3. Creates DbParameter objects and adds them to the Command.Parameters collection. 
        ///    The ParameterName of those objects has no prefix, ie. ID.
        ///    If required by the provider though, a later call to PrefixToNative() method would arrange the values
        ///    of those ParameterName properties to be what the native provider expects to be.
        /// 4. If the passed Params is not null and contains parameter values, it assigns the Parameters of the Command
        ///    object by calling the DataProviders.AssignParams() method.
        /// </summary>
        public void SetupCommand(DbCommand Command, string SQL, params object[] Params)
        {
            int CurPos = 0;
            int StartPos = 0;
            char CurChar;
            bool Literal = false;
            string Name = "";

            StringBuilder SB = new StringBuilder();
            SQL = SQL + " ";
            int Len = SQL.Length;

            Command.Parameters.Clear();

            /* anlayze the passed SQL and create DbParameter objects */
            while (CurPos <= Len - 1)
            {
                CurChar = SQL[CurPos];
                if ((CurChar == DataProviders.GlobalPrefix) && (!Literal) && (SQL[CurPos + 1] != DataProviders.GlobalPrefix))
                {
                    StartPos = CurPos;
                    while ((CurPos < Len - 1) && (Literal || (!IsNameDelimiter(CurChar))))
                    {
                        CurPos++;
                        CurChar = SQL[CurPos];
                        if (IsLiteral(CurChar))
                        {
                            Literal = Literal ^ true;
                        }
                    }

                    Name = SQL.Substring(StartPos + 1, CurPos - (StartPos + 1));


                    DbParameter Param = Command.CreateParameter(); 
                    Command.Parameters.Add(Param);
                    Param.ParameterName = Name;

                    Name = PrefixToNative(Name);

                    SB.Append(Name);

                }
                else if ((CurChar == DataProviders.GlobalPrefix) && (!Literal) && (SQL[CurPos + 1] == DataProviders.GlobalPrefix))
                {
                    CurPos++;
                    SB.Append(CurChar);
                }
                else if (IsLiteral(CurChar))
                {
                    Literal = Literal ^ true;
                }

                CurPos++;
                SB.Append(CurChar);
            }


            /* assign CommandText */
            Command.CommandText = SB.ToString();


            /* assign DbParameter values */
            DataProviders.AssignParams(Command, Params);
            if (requiresNativeParamNames)
                PrefixToNative(Command);
 
 
        }

        /// <summary>
        /// Removes any prefix from the ParameterName
        /// </summary>
        public virtual string PrefixRemove(string ParameterName)
        {
            if ((ParameterName.StartsWith(NativePrefix.ToString())) || (ParameterName.StartsWith(DataProviders.GlobalPrefix.ToString())))
                return ParameterName.Substring(1, ParameterName.Length - 1);
            return ParameterName;
        }
        /// <summary>
        /// Ensures that the ParemeterName is prefixed by the NativePrefix, in regard to PrefixMode.
        /// If PrefixMode is PrefixMode.Prefixed it returns prefix + param, ie. @CUSTOMER_NAME or :CUSTOMER_NAME
        /// If PrefixMode is Positional it just returns the position mark, ie. ?
        /// </summary>
        public virtual string PrefixToNative(string ParameterName)
        {
            if (this.PrefixMode == PrefixMode.Prefixed)
            {
                return NativePrefix.ToString() + PrefixRemove(ParameterName);
            }
            else  // PrefixMod.Positional
            {
                return NativePrefix.ToString();
            }
        }
        /// <summary>
        /// Ensures that the ParameterName is prefixed by the DataProviders.GlobalPrefix
        /// </summary>
        public virtual string PrefixToGlobal(string ParameterName)
        {
            return DataProviders.GlobalPrefix.ToString() + PrefixRemove(ParameterName);
        }
        /// <summary>
        /// Removes any prefix from the ParameterName of each Parameter in the Command
        /// </summary>
        public void PrefixRemove(DbCommand Command)
        {
            foreach (DbParameter Parameter in Command.Parameters)
                Parameter.ParameterName = PrefixRemove(Parameter.ParameterName);
        }
        /// <summary>
        /// Ensures that the ParemeterName of each Parameter in the Command is prefixed by the NativePrefix, in regard to PrefixMode.
        /// If PrefixMode is PrefixMode.Prefixed it returns prefix + param, ie. @CUSTOMER_NAME or :CUSTOMER_NAME
        /// If PrefixMode is Positional it just returns the position mark, ie. ?
        /// </summary>
        public void PrefixToNative(DbCommand Command)
        {
            foreach (DbParameter Parameter in Command.Parameters)
                Parameter.ParameterName = PrefixToNative(Parameter.ParameterName);
        }
        /// <summary>
        /// Ensures that the ParameterName of each Parameter in the Command is prefixed by the DataProviders.GlobalPrefix
        /// </summary>
        public void PrefixToGlobal(DbCommand Command)
        {
            foreach (DbParameter Parameter in Command.Parameters)
                Parameter.ParameterName = PrefixToGlobal(Parameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool CreateDatabase(string ServerName, string DatabaseName, string UserName, string Password)
        {
            return false;
        }
        

        public DataTable Select(string ConnectionString, string SQL, params object[] Params)
        {
            DataTable Result = new DataTable();

            string sAlias = "";
            DataProviders.ExtractAlias(ConnectionString, ref sAlias, ref ConnectionString); 

            using (DbConnection Con = CreateConnection(ConnectionString))
            {   
                Con.Open();

                DbCommand Cmd = Con.CreateCommand();
                SetupCommand(Cmd, SQL, Params);

                using (DbDataAdapter adapter = CreateAdapter())
                {
                    adapter.SelectCommand = Cmd;
                    adapter.Fill(Result);
                }
            }

            return Result;
        }
        public DataTable Select(string ConnectionString, string SQL)
        {
            return Select(ConnectionString, SQL, null);
        }
        public DataTable Select(DbCommand Command)
        {
            DataTable Result = new DataTable();

            using (DbDataAdapter adapter = CreateAdapter())
            {
                adapter.SelectCommand = Command;                
                adapter.Fill(Result);
            }

            return Result;

        }
        public void Exec(string ConnectionString, string SQL, params object[] Params)
        {
            string sAlias = "";
            DataProviders.ExtractAlias(ConnectionString, ref sAlias, ref ConnectionString);

            using (DbConnection Con = CreateConnection(ConnectionString))
            {
                Con.Open();

                DbCommand Cmd = Con.CreateCommand();
                SetupCommand(Cmd, SQL, Params);
                Cmd.ExecuteNonQuery(); 
            } 
        }
        public void Exec(string ConnectionString, string SQL)
        {
            Exec(ConnectionString, SQL, null);
        }        
        

        
        /// <summary>
        /// Gets the Alias of this Provider, i.e. MSSQL
        /// </summary>
        public string Alias { get { return alias; } }
        /// <summary>
        /// Gets the Description this Provider
        /// </summary>
        public string Description { get { return description; } }
        /// <summary>
        /// Gets the ParamPrefix the native .Net Provider uses.
        /// </summary>
        public char NativePrefix { get { return nativePrefix; } }
        /// <summary>
        /// Indicates the mode of the parameter name and prefix the ADO.NET Data Provider uses
        /// </summary>
        public PrefixMode PrefixMode { get { return prefixMode; } }
        /// <summary>
        /// Returns true if parameters names should be normalized back to what the
        /// provider expects to be, after parameter value assignment
        /// </summary>
        public bool RequiresNativeParamNames { get { return requiresNativeParamNames; } }

    }


    #region Providers with a DbProviderFactory factory

#if !NET_CF

    /// <summary>
    /// Represents a DataProvider for whom there is a DbProviderFactory available
    /// </summary>
    public abstract class ProviderWithFactory : DataProvider
    {
        protected DbProviderFactory factory = null;
        protected string providerNamespace = "";

        /// <summary>
        /// constructor
        /// </summary>
        public ProviderWithFactory()
        {
        }


        /// <summary>
        /// Creates and returns a DbConnection initialized with ConnectionString.
        /// </summary>
        public override DbConnection CreateConnection(string ConnectionString)
        {
            DbConnection Result = Factory.CreateConnection();
            Result.ConnectionString = ConnectionString;
            return Result;
        }
        /// <summary>
        /// Creates and returns a DbDataAdapter.
        /// </summary>
        public override DbDataAdapter CreateAdapter()
        {
            return Factory.CreateDataAdapter();
        }
        /// <summary>
        /// Creates and returns a DbCommandBuilder
        /// </summary>
        public override DbCommandBuilder CreateCommandBuilder()
        {
            return Factory.CreateCommandBuilder();
        }
        /// <summary>
        /// Creates and returns a DbConnectionStringBuilder
        /// </summary>
        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return Factory.CreateConnectionStringBuilder();
        }
        /// <summary>
        /// Creates and returns a DbParameter
        /// </summary>
        public override DbParameter CreateParameter()
        {
            return Factory.CreateParameter();
        }
        
        /// <summary>
        /// Returns the DbProviderFactory for this provider
        /// </summary>
        public DbProviderFactory Factory 
        {             
            get 
            { 
                if ((factory == null) && (!string.IsNullOrEmpty(providerNamespace)))
                    factory = DbProviderFactories.GetFactory(providerNamespace);
                return factory; 
            
            }         
        }
        /// <summary>
        /// Returns the namespace of this provider, ie. System.Data.SqlClient
        /// </summary>
        public string Namespace { get { return providerNamespace; } }
    }




    /// <summary>
    /// 
    /// </summary>
    public class OleDbProvider : ProviderWithFactory
    {

        protected override void Initialize()
        {
            alias = DataProviders.OleDb;
            description = "OLE DB Data Provider";
            nativePrefix = '?';
            prefixMode = PrefixMode.Positional;
            providerNamespace = "System.Data.OleDb";
        }

        public OleDbProvider()
        {
        }

    }




    /// <summary>
    /// 
    /// </summary>
    public class MsAccessProvider : OleDbProvider
    {
        /// <summary>
        /// Initializes a instance by assinging valid values to its properties.
        /// </summary>  
        protected override void Initialize()
        {
            base.Initialize();

            alias = DataProviders.OleDbAccess;
            description = "OLE DB Access Provider";
        }
        
        /// <summary>
        /// constructor
        /// </summary>
        public MsAccessProvider()
        {
        }
    }




    /// <summary>
    /// 
    /// </summary>
    public class OleDbDbfProvider : OleDbProvider
    {
        /// <summary>
        /// Initializes a instance by assinging valid values to its properties.
        /// </summary>  
        protected override void Initialize()
        {
            base.Initialize();

            alias = DataProviders.OleDbDbf;
            description = "OLE DB DBF Provider";
        }

        /// <summary>
        /// constructor
        /// </summary>
        public OleDbDbfProvider()
        {
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public class OleDbExcelProvider : OleDbProvider
    {
        /// <summary>
        /// Initializes a instance by assinging valid values to its properties.
        /// </summary>  
        protected override void Initialize()
        {
            base.Initialize();

            alias = DataProviders.OleDbExcel;
            description = "OLE DB Excel Provider";
 
        }


        /// <summary>
        /// constructor
        /// </summary>
        public OleDbExcelProvider()
        {
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public class OdbcProvider : ProviderWithFactory
    {
        /// <summary>
        /// Initializes a instance by assinging valid values to its properties.
        /// </summary>
        protected override void Initialize()
        {
            alias = DataProviders.Odbc;
            description = "ODBC Data Provider";
            nativePrefix = '?';
            prefixMode = PrefixMode.Positional;
            providerNamespace = "System.Data.Odbc";
        }

        /// <summary>
        /// constructor
        /// </summary>
        public OdbcProvider()
        {
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class MsSqlProvider : ProviderWithFactory
    {
        /// <summary>
        /// Initializes a instance by assinging valid values to its properties.
        /// </summary>
        protected override void Initialize()
        {
            alias = DataProviders.MsSql;
            description = "Microsoft SQL Server 7.0/2000";
            nativePrefix = '@';
            providerNamespace = "System.Data.SqlClient";
        }

        public MsSqlProvider()
        {
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public class OracleClientProvider : ProviderWithFactory
    {
        /// <summary>
        /// Initializes a instance by assinging valid values to its properties.
        /// </summary>
        protected override void Initialize()
        {
            alias = DataProviders.OracleClient;
            description = "Oracle (Microsoft version) Provider";
            nativePrefix = ':';
            providerNamespace = "System.Data.OracleClient";
        }

        /// <summary>
        /// constructor
        /// </summary>
        public OracleClientProvider()
        {
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public class OracleProvider : ProviderWithFactory
    {
        /// <summary>
        /// Initializes a instance by assinging valid values to its properties.
        /// </summary>
        protected override void Initialize()
        {
            alias = DataProviders.Oracle;    
            description = "Oracle 9i Provider";
            nativePrefix = ':';
            providerNamespace = "Oracle.DataAccess.Client";
        }


        /// <summary>
        /// constructor
        /// </summary>
        public OracleProvider()
        {
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public class FirebirdProvider : ProviderWithFactory
    {
        /// <summary>
        /// Initializes a instance by assinging valid values to its properties.
        /// </summary>
        protected override void Initialize()
        {
            alias = DataProviders.Firebird;
            description = "Firebird";
            nativePrefix = '@';
            providerNamespace = "FirebirdSql.Data.FirebirdClient";

            //Con = new FbConnection("User=SYSDBA;Password=masterkey;DataSource=localhost;Database=C:\\BUGTRACK.GDB");
        }


        /// <summary>
        /// constructor
        /// </summary>
        public FirebirdProvider()
        {
        }
    }
#endif

    #endregion Providers with a DbProviderFactory factory


}


