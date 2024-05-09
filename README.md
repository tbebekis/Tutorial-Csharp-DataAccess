C# Data access
===========
 
- [C# Data access](#c-data-access)
  - [ADO.NET](#adonet)
    - [In-memory data manipulation](#in-memory-data-manipulation)
      - [DataTable, DataColumnCollection and DataColumn class: defining table schema](#datatable-datacolumncollection-and-datacolumn-class-defining-table-schema)
      - [DataRowCollection and DataRow class: adding data to a table](#datarowcollection-and-datarow-class-adding-data-to-a-table)
      - [Iterating a DataTable and reading data](#iterating-a-datatable-and-reading-data)
      - [Iterating a table through a DataTableReader](#iterating-a-table-through-a-datatablereader)
      - [DBNull values](#dbnull-values)
      - [DataTable events](#datatable-events)
      - [The state of a row: the DataRowState enum type](#the-state-of-a-row-the-datarowstate-enum-type)
      - [The DataTable.AcceptChanges() and RejectChanges() methods](#the-datatableacceptchanges-and-rejectchanges-methods)
      - [The DataTable.GetChanges() method](#the-datatablegetchanges-method)
      - [DataColumn constraints: the ConstraintCollection class](#datacolumn-constraints-the-constraintcollection-class)
      - [Unique constraints: the UniqueConstraint class](#unique-constraints-the-uniqueconstraint-class)
      - [DataTable.PrimaryKey and primary key constraints](#datatableprimarykey-and-primary-key-constraints)
      - [DataRow editing methods and DataRow versions](#datarow-editing-methods-and-datarow-versions)
      - [Locating rows in a DataTable](#locating-rows-in-a-datatable)
      - [Computing values on multiple rows](#computing-values-on-multiple-rows)
      - [Saving and loading data tables](#saving-and-loading-data-tables)
      - [DataTable.Copy() and Clone() methods](#datatablecopy-and-clone-methods)
      - [DataView class: DataTable.DefaultView property](#dataview-class-datatabledefaultview-property)
      - [The DataSet class](#the-dataset-class)
      - [Foreign key constraints: the ForeignKeyConstraint class](#foreign-key-constraints-the-foreignkeyconstraint-class)
      - [Foreign key constraing rules](#foreign-key-constraing-rules)
      - [DataRelation class: a master-detail relationship](#datarelation-class-a-master-detail-relationship)
    - [Accessing datasources](#accessing-datasources)
      - [Sample database](#sample-database)
      - [Data Providers](#data-providers)
      - [Data types](#data-types)
      - [Provider neutral data access and Provider Factories](#provider-neutral-data-access-and-provider-factories)
      - [Connection strings](#connection-strings)
      - [OleDb connection string](#oledb-connection-string)
      - [Other connection string examples](#other-connection-string-examples)
      - [Connection string Builders](#connection-string-builders)
      - [Connection strings and configuration files](#connection-strings-and-configuration-files)
      - [The DbConnection class](#the-dbconnection-class)
      - [Connection pooling](#connection-pooling)
      - [The DbCommand class](#the-dbcommand-class)
      - [Retrieving data: DbCommand.ExecuteScalar()](#retrieving-data-dbcommandexecutescalar)
      - [Retrieving data: the DbDataReader class](#retrieving-data-the-dbdatareader-class)
      - [Retrieving data: the DbDataAdapter class](#retrieving-data-the-dbdataadapter-class)
      - [Table and column mapping: DataTableMapping and DataColumnMapping class](#table-and-column-mapping-datatablemapping-and-datacolumnmapping-class)
      - [The DbDataAdapter.FillSchema() method](#the-dbdataadapterfillschema-method)
      - [Executing DbCommand(s) which modify data](#executing-dbcommands-which-modify-data)
      - [Transactions and the DbTransaction class](#transactions-and-the-dbtransaction-class)
      - [Posting changes back to the datasource with the DbDataAdapter.Update() method](#posting-changes-back-to-the-datasource-with-the-dbdataadapterupdate-method)
      - [Parameterized commands: DbParameterCollection and DbParameter class](#parameterized-commands-dbparametercollection-and-dbparameter-class)
      - [A helper library](#a-helper-library)
      - [Executing DDL (Data Definition Language) statements](#executing-ddl-data-definition-language-statements)
      - [Using stored procedures](#using-stored-procedures)
      - [Reading and writing BLOB data](#reading-and-writing-blob-data)
      - [Reading database schema information](#reading-database-schema-information)
      - [Creating connections with the Server Explorer window of the MS Visual Studio](#creating-connections-with-the-server-explorer-window-of-the-ms-visual-studio)
      - [Typed datasets](#typed-datasets)
      - [The IISAM of the Jet.OLEDB.4.0 OLEDB provider](#the-iisam-of-the-jetoledb40-oledb-provider)
    - [Windows Forms data binding](#windows-forms-data-binding)
      - [Simple binding](#simple-binding)
      - [Change notification requirements for simple data binding](#change-notification-requirements-for-simple-data-binding)
      - [Complex (or list-based) binding](#complex-or-list-based-binding)
      - [Change notification requirements for complex data binding](#change-notification-requirements-for-complex-data-binding)
      - [Windows Forms binding mechanism: binding contexts and binding managers](#windows-forms-binding-mechanism-binding-contexts-and-binding-managers)
      - [The BindingSource component](#the-bindingsource-component)
      - [BindingSource: binding a BindingSource to a type](#bindingsource-binding-a-bindingsource-to-a-type)
      - [BindingSource: using the AddingNew event and the AddNew() method](#bindingsource-using-the-addingnew-event-and-the-addnew-method)
      - [BindingSource: a master-detail example](#bindingsource-a-master-detail-example)
      - [The BindingNavigator control](#the-bindingnavigator-control)
      - [Lookup ComboBox and ListBox](#lookup-combobox-and-listbox)
      - [The DataGridView control (.Net 2.0 and later, not available to Compact Framework)](#the-datagridview-control-net-20-and-later-not-available-to-compact-framework)
      - [The DataGrid control (available to Compact Framework too)](#the-datagrid-control-available-to-compact-framework-too)
      - [](#)


ADO.NET
-------

ADO.NET is a set of classes that provide data accessing and data manipulation services. Data can reside in memory or in a datasource such as a database, an XML file, a text file or even a spreadsheet.

Three key areas are involved in .Net database application programming:

*   manipulating in-memory data, using DataSet and DataTable objects
*   accessing and modifying data in datasources, using DbConnection, DbCommand etc.
*   binding data to user interface controls such as the DataGridView and the TextBox.

### In-memory data manipulation

ADO.NET promotes the disconnected model. ADO.NET provides classes, such as the DataTable and the DataSet class that work with in-memory-data. DataTable and DataSet act as data containers for data that might come from a datasource such as a SQL server or data that just exist in memory.

#### DataTable, DataColumnCollection and DataColumn class: defining table schema

DataTable class represents a table of data. That is something having columns and rows. The DataTable class is always disconnected from the datasource. It has no reference to a connection object or any knowledge regarding the datasource. It is just an in-memory data container of tabular data.

    public class DataTable : MarshalByValueComponent, IListSource, ISupportInitializeNotification, ISupportInitialize, ISerializable, IXmlSerializable
     {
         public DataTable();
         public DataTable(string tableName);
         public DataTable(string tableName, string tableNamespace);
 
         public bool CaseSensitive { get; set; }
         public DataRelationCollection ChildRelations { get; }
         public DataColumnCollection Columns { get; }
         public ConstraintCollection Constraints { get; }
         public DataSet DataSet { get; }
         public DataView DefaultView { get; }
         public string DisplayExpression { get; set; }
         public PropertyCollection ExtendedProperties { get; }
         public bool HasErrors { get; }
         public bool IsInitialized { get; }
         public CultureInfo Locale { get; set; }
         public int MinimumCapacity { get; set; }
         public string Namespace { get; set; }
         public DataRelationCollection ParentRelations { get; }
         public string Prefix { get; set; }
         public DataColumn\[\] PrimaryKey { get; set; }
         public SerializationFormat RemotingFormat { get; set; }
         public DataRowCollection Rows { get; }
         public override ISite Site { get; set; }
         public string TableName { get; set; }
 
         public event DataColumnChangeEventHandler ColumnChanged;
         public event DataColumnChangeEventHandler ColumnChanging;
         public event EventHandler Initialized;
         public event DataRowChangeEventHandler RowChanged;
         public event DataRowChangeEventHandler RowChanging;
         public event DataRowChangeEventHandler RowDeleted;
         public event DataRowChangeEventHandler RowDeleting;
         public event DataTableClearEventHandler TableCleared;
         public event DataTableClearEventHandler TableClearing;
         public event DataTableNewRowEventHandler TableNewRow;
 
         public void AcceptChanges();
         public virtual void BeginInit();
         public void BeginLoadData();
         public void Clear();
         public virtual DataTable Clone();
         public object Compute(string expression, string filter);
         public DataTable Copy();
         public DataTableReader CreateDataReader();
         public virtual void EndInit();
         public void EndLoadData();
         public DataTable GetChanges();
         public DataTable GetChanges(DataRowState rowStates);
         public static XmlSchemaComplexType GetDataTableSchema(XmlSchemaSet schemaSet);
         public DataRow\[\] GetErrors();
         public virtual void GetObjectData(SerializationInfo info, StreamingContext context);
         public void ImportRow(DataRow row);
         public void Load(IDataReader reader);
         public void Load(IDataReader reader, LoadOption loadOption);
         public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler);
         public DataRow LoadDataRow(object\[\] values, bool fAcceptChanges);
         public DataRow LoadDataRow(object\[\] values, LoadOption loadOption);
         public void Merge(DataTable table);
         public void Merge(DataTable table, bool preserveChanges);
         public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction);
         public DataRow NewRow();
         public XmlReadMode ReadXml(Stream stream);
         public XmlReadMode ReadXml(string fileName);
         public XmlReadMode ReadXml(TextReader reader);
         public XmlReadMode ReadXml(XmlReader reader);
         public void ReadXmlSchema(Stream stream);
         public void ReadXmlSchema(string fileName);
         public void ReadXmlSchema(TextReader reader);
         public void ReadXmlSchema(XmlReader reader);
         public void RejectChanges();
         public virtual void Reset();
         public DataRow\[\] Select();
         public DataRow\[\] Select(string filterExpression);
         public DataRow\[\] Select(string filterExpression, string sort);
         public DataRow\[\] Select(string filterExpression, string sort, DataViewRowState recordStates);
         public override string ToString();
         public void WriteXml(Stream stream);
         public void WriteXml(string fileName);
         public void WriteXml(TextWriter writer);
         public void WriteXml(XmlWriter writer);
         public void WriteXml(Stream stream, bool writeHierarchy);
         public void WriteXml(Stream stream, XmlWriteMode mode);
         public void WriteXml(string fileName, bool writeHierarchy);
         public void WriteXml(string fileName, XmlWriteMode mode);
         public void WriteXml(TextWriter writer, bool writeHierarchy);
         public void WriteXml(TextWriter writer, XmlWriteMode mode);
         public void WriteXml(XmlWriter writer, bool writeHierarchy);
         public void WriteXml(XmlWriter writer, XmlWriteMode mode);
         public void WriteXml(Stream stream, XmlWriteMode mode, bool writeHierarchy);
         public void WriteXml(string fileName, XmlWriteMode mode, bool writeHierarchy);
         public void WriteXml(TextWriter writer, XmlWriteMode mode, bool writeHierarchy);
         public void WriteXml(XmlWriter writer, XmlWriteMode mode, bool writeHierarchy);
         public void WriteXmlSchema(Stream stream);
         public void WriteXmlSchema(string fileName);
         public void WriteXmlSchema(TextWriter writer);
         public void WriteXmlSchema(XmlWriter writer);
         public void WriteXmlSchema(Stream stream, bool writeHierarchy);
         public void WriteXmlSchema(string fileName, bool writeHierarchy);
         public void WriteXmlSchema(TextWriter writer, bool writeHierarchy);
         public void WriteXmlSchema(XmlWriter writer, bool writeHierarchy);
     }    
     
 

DataTable provides the property Columns of type DataColumnCollection.

    public sealed class DataColumnCollection : InternalDataCollectionBase
     {
         public DataColumn this\[int index\] { get; }
         public DataColumn this\[string name\] { get; }
 
         public event CollectionChangeEventHandler CollectionChanged;
 
         public DataColumn Add();
         public void Add(DataColumn column);
         public DataColumn Add(string columnName);
         public DataColumn Add(string columnName, Type type);
         public DataColumn Add(string columnName, Type type, string expression);
         public void AddRange(DataColumn\[\] columns);
         public bool CanRemove(DataColumn column);
         public void Clear();
         public bool Contains(string name);
         public void CopyTo(DataColumn\[\] array, int index);
         public int IndexOf(DataColumn column);
         public int IndexOf(string columnName);
         public void Remove(DataColumn column);
         public void Remove(string name);
         public void RemoveAt(int index);
     }
 
 
 

Columns define the schema of a DataTable and is a collection of DataColumn objects.

    public class DataColumn : MarshalByValueComponent
     {
         public DataColumn();
         public DataColumn(string columnName);
         public DataColumn(string columnName, Type dataType);
         public DataColumn(string columnName, Type dataType, string expr);
         public DataColumn(string columnName, Type dataType, string expr, MappingType type);
 
         public bool AllowDBNull { get; set; }
         public bool AutoIncrement { get; set; }
         public long AutoIncrementSeed { get; set; }
         public long AutoIncrementStep { get; set; }
         public string Caption { get; set; }
         public virtual MappingType ColumnMapping { get; set; }
         public string ColumnName { get; set; }
         public Type DataType { get; set; }
         public DataSetDateTime DateTimeMode { get; set; }
         public object DefaultValue { get; set; }
         public string Expression { get; set; }
         public PropertyCollection ExtendedProperties { get; }
         public int MaxLength { get; set; }
         public string Namespace { get; set; }
         public int Ordinal { get; }
         public string Prefix { get; set; }
         public bool ReadOnly { get; set; }
         public DataTable Table { get; }
         public bool Unique { get; set; }
 
         public void SetOrdinal(int ordinal);
         public override string ToString();
     }
 
     
     
 

Here is how to create a DataTable object and define its schema.

    DataTable table = new DataTable("Person");
     DataColumn column;
 
     column = new DataColumn();                              // using the DataColumn constructor
     column.ColumnName = "ID";
     column.DataType = typeof(int);
     column.AutoIncrement = true;
     column.AutoIncrementSeed = -1;
     column.AutoIncrementStep = -1;
     table.Columns.Add(column);                              // a column  needs to be added to the columns            
 
     column = new DataColumn("FirstName", typeof(string));   // using a different constructor
     column.MaxLength = 12;
     column.AllowDBNull = true;
     column.DefaultValue = "<first name>";
     table.Columns.Add(column);
 
     column = table.Columns.Add("LastName", typeof(string)); // the Columns.Add() provides handy overloads
     column.AllowDBNull = false;
     column.Unique = true;
 
     column = table.Columns.Add("Married", typeof(bool));
 
     column = table.Columns.Add("Name", typeof(string), "LastName + ', ' + FirstName"); 
     
 

#### DataRowCollection and DataRow class: adding data to a table

DataTable has the Rows property of type DataRowCollection.

    public sealed class DataRowCollection : InternalDataCollectionBase
     {
         public override int Count { get; }
 
         public DataRow this\[int index\] { get; }
 
         public void Add(DataRow row);
         public DataRow Add(params object\[\] values);
         public void Clear();
         public bool Contains(object key);
         public bool Contains(object\[\] keys);
         public override void CopyTo(Array ar, int index);
         public void CopyTo(DataRow\[\] array, int index);
         public DataRow Find(object key);
         public DataRow Find(object\[\] keys);
         public override IEnumerator GetEnumerator();
         public int IndexOf(DataRow row);
         public void InsertAt(DataRow row, int pos);
         public void Remove(DataRow row);
         public void RemoveAt(int index);
     }
 
 
 

Rows is a collection of DataRow objects.

    public class DataRow
     {
         public bool HasErrors { get; }
         public object\[\] ItemArray { get; set; }
         public string RowError { get; set; }
         public DataRowState RowState { get; }
         public DataTable Table { get; }
 
         public object this\[DataColumn column\] { get; set; }
         public object this\[int columnIndex\] { get; set; }
         public object this\[string columnName\] { get; set; }
         public object this\[DataColumn column, DataRowVersion version\] { get; }
         public object this\[int columnIndex, DataRowVersion version\] { get; }
         public object this\[string columnName, DataRowVersion version\] { get; }
 
         public void AcceptChanges();
         public void BeginEdit();
         public void CancelEdit();
         public void ClearErrors();
         public void Delete();
         public void EndEdit();
         public DataRow\[\] GetChildRows(DataRelation relation);
         public DataRow\[\] GetChildRows(string relationName);
         public DataRow\[\] GetChildRows(DataRelation relation, DataRowVersion version);
         public DataRow\[\] GetChildRows(string relationName, DataRowVersion version);
         public string GetColumnError(DataColumn column);
         public string GetColumnError(int columnIndex);
         public string GetColumnError(string columnName);
         public DataColumn\[\] GetColumnsInError();
         public DataRow GetParentRow(DataRelation relation);
         public DataRow GetParentRow(string relationName);
         public DataRow GetParentRow(DataRelation relation, DataRowVersion version);
         public DataRow GetParentRow(string relationName, DataRowVersion version);
         public DataRow\[\] GetParentRows(DataRelation relation);
         public DataRow\[\] GetParentRows(string relationName);
         public DataRow\[\] GetParentRows(DataRelation relation, DataRowVersion version);
         public DataRow\[\] GetParentRows(string relationName, DataRowVersion version);
         public bool HasVersion(DataRowVersion version);
         public bool IsNull(DataColumn column);
         public bool IsNull(int columnIndex);
         public bool IsNull(string columnName);
         public bool IsNull(DataColumn column, DataRowVersion version);
         public void RejectChanges();
         public void SetAdded();
         public void SetColumnError(DataColumn column, string error);
         public void SetColumnError(int columnIndex, string error);
         public void SetColumnError(string columnName, string error);
         public void SetModified();
         public void SetParentRow(DataRow parentRow);
         public void SetParentRow(DataRow parentRow, DataRelation relation);
     }
 
  
 

DataRow is where data reside. Here is how to add rows in a DataTable.

    DataTable table = new DataTable();
 
     table.Columns.Add("ID", typeof(int));
     table.Columns.Add("Name", typeof(string));
 
     DataRow row;
     for (int i = 1; i < 6; i++)
     {
         row = table.NewRow();                   // DataTable.NewRow() creates a new row. The row is not added to the rows.
         
         row\[0\] = i;                             // accessing a column by its index (DataColumn.Ordinal property)                 
         row\["Name"\] = "Name\_" + i.ToString();   // accdessing a column by its ColumnName
 
         table.Rows.Add(row);                    // DataTable.Rows.Add() adds a row to the Rows.
     }
     
     
 

The next example is a variation of adding data to a table, using an overloaded version of the DataTable.Rows.Add() method which creates and adds a DataRow in a single call.

    DataTable table = new DataTable();
 
     table.Columns.Add("ID", typeof(int)).AutoIncrement = true;
     table.Columns.Add("Name", typeof(string));
 
     for (int i = 1; i < 6; i++)
     {
         table.Rows.Add( new object\[\] { null, "Name\_" + i.ToString() } );
     } 
 
 

The schema of a table may include auto-computed columns, such as an auto-increment column or an expression column. The DataTable.Rows.Add() overload, the one which creates and adds a DataRow at the same time, is a positional call. With such positional calls the null value should be used in place of auto-computed columns for the call to succeed.

#### Iterating a DataTable and reading data

The DataTable.Rows property of type DataRowCollection provides the means of iterating the rows of a table.

    for (int i = 0; i < table.Rows.Count; i++)
     {
         ...
     }
     
 

Another way is to use an enumerator through a foreach statement.

    foreach (DataRow row in table.Rows)
     {
         ...
     }
     
 

Data can be read in many ways. One way is to typecast the value returned by the DataRow indexer property.

    int ID;
 
     for (int i = 0; i < table.Rows.Count; i++)
     {
         ID = (int)table.Rows\[i\]\["ID"\];
         ...
     }
     
 

The above can be written as

    int ID;
     DataRow row;
     
     for (int i = 0; i < table.Rows.Count; i++)
     {
         row = table.Rows\[i\];
         
         ID = (int)row\["ID"\];
         ...
     }
     
     
 

The DataRow class provides the Field<T>() generic method which can be used in accessing data values as

    foreach (DataRow row in table.Rows)
     {
         ID = row.Field<int>("ID");
         ...
     }     
     
 

#### Iterating a table through a DataTableReader

A DataTableReader object can be used to iterate a table.

    public sealed class DataTableReader : DbDataReader
     {    
         public DataTableReader(DataTable dataTable);
         public DataTableReader(DataTable\[\] dataTables);
 
         public override int Depth { get; }
         public override int FieldCount { get; }
         public override bool HasRows { get; }
         public override bool IsClosed { get; }
         public override int RecordsAffected { get; }
 
         public override object this\[int ordinal\] { get; }
         public override object this\[string name\] { get; }
 
         public override void Close();
         public override bool GetBoolean(int ordinal);
         public override byte GetByte(int ordinal);
         public override long GetBytes(int ordinal, long dataIndex, byte\[\] buffer, int bufferIndex, int length);
         public override char GetChar(int ordinal);
         public override long GetChars(int ordinal, long dataIndex, char\[\] buffer, int bufferIndex, int length);
         public override string GetDataTypeName(int ordinal);
         public override DateTime GetDateTime(int ordinal);
         public override decimal GetDecimal(int ordinal);
         public override double GetDouble(int ordinal);
         public override IEnumerator GetEnumerator();
         public override Type GetFieldType(int ordinal);
         public override float GetFloat(int ordinal);
         public override Guid GetGuid(int ordinal);
         public override short GetInt16(int ordinal);
         public override int GetInt32(int ordinal);
         public override long GetInt64(int ordinal);
         public override string GetName(int ordinal);
         public override int GetOrdinal(string name);
         public override Type GetProviderSpecificFieldType(int ordinal);
         public override object GetProviderSpecificValue(int ordinal);
         public override int GetProviderSpecificValues(object\[\] values);
         public override DataTable GetSchemaTable();
         public override string GetString(int ordinal);
         public override object GetValue(int ordinal);
         public override int GetValues(object\[\] values);
         public override bool IsDBNull(int ordinal);
         public override bool NextResult();
         public override bool Read();        
     }
 
 

The DataTableReader class provides read-only and forward-only access to a table rows. Iteration can be done either using the Read() method

    using (DataTableReader reader = new DataTableReader(table))
     {
         while (reader.Read())
         {
             ID = (int)reader\["ID"\];
             ...
         }
     }
     
 

or using an enumerator (foreach). The DbDataRecord class is the type the DataTableReader enumerator returns.

    using (DataTableReader reader = new DataTableReader(table))
     {
         foreach (DbDataRecord record in reader)
         {
             ID = (int)record\["ID"\];
             ...
         }
     }
     
 

DataTableReader provides safer access than a plain for loop since it automatically maintains its position in the rows collection even when new rows are added or deleted during iteration.

#### DBNull values

Caution is required when reading data values because of the possibility of a null data value. The DataRow provides the IsNull() method and the DataTableReader the IsDBNull() method for checking null values. Furthermore ADO.NET provides the DBNull class.

    public sealed class DBNull : ISerializable, IConvertible
     {
         public static readonly DBNull Value;
 
         public void GetObjectData(SerializationInfo info, StreamingContext context);
         public TypeCode GetTypeCode();
         public override string ToString();
         public string ToString(IFormatProvider provider);
     }
 
 

Here is an example

    table.Rows\[0\]\["Name"\] = DBNull.Value;
 
     if (table.Rows\[0\]\["Name"\] == DBNull.Value) // or if (DBNull.Value.Equals(table.Rows\[0\]\["Name"\]))
     {
         MessageBox.Show("NULL");
     }
     
 

#### DataTable events

DataTable class provides a number of events in order to notify a client code for events such as the insertion, modification or deletion of a row, or the modification of the value of a certain column.

Here is how to link to those events.

    table.ColumnChanging += new DataColumnChangeEventHandler(Table\_ColumnChanging);
     table.ColumnChanged += new DataColumnChangeEventHandler(Table\_ColumnChanged);
 
     table.RowChanging += new DataRowChangeEventHandler(Table\_RowChanging);
     table.RowChanged += new DataRowChangeEventHandler(Table\_RowChanged);
 
     table.RowDeleting += new DataRowChangeEventHandler(Table\_RowDeleting);
     table.RowDeleted += new DataRowChangeEventHandler(Table\_RowDeleted);
 
     table.TableNewRow += new DataTableNewRowEventHandler(Table\_NewRow);    
     
     
 

And here is a possible event handler for the ColumnChanged event

    void Table\_ColumnChanged(object sender, DataColumnChangeEventArgs args)
     {
         lboLog.Items.Add(string.Format("ColumnChanged ({0}): Value = {1}, ProposedValue = {2} ",
                 args.Column.ColumnName, args.Row\[args.Column\], args.ProposedValue));
     }
 
 
 

The RowChanging, RowChanged, RowDeleting, and RowDeleted events use the DataRowChangeEventArgs class as a parameter.

    public class DataRowChangeEventArgs : EventArgs
     {
         public DataRowChangeEventArgs(DataRow row, DataRowAction action);
 
         public DataRowAction Action { get; }
         public DataRow Row { get; }
     }   
     
 
     \[Flags\]
     public enum DataRowAction
     {
         Nothing = 0,
         Delete = 1,
         Change = 2,
         Rollback = 4,
         Commit = 8,
         Add = 16,
         ChangeOriginal = 32,
         ChangeCurrentAndOriginal = 64,
     } 
 
 
 

The TableNewRow event uses the DataTableNewRowEventArgs class.

    public sealed class DataTableNewRowEventArgs : EventArgs
     {
         public DataTableNewRowEventArgs(DataRow dataRow);
 
         public DataRow Row { get; }
     }  
 
 

The ColumnChanging and ColumnChanged events use the DataColumnChangeEventArgs.

    public class DataColumnChangeEventArgs : EventArgs
     {
         public DataColumnChangeEventArgs(DataRow row, DataColumn column, object value);
 
         public DataColumn Column { get; }
         public object ProposedValue { get; set; }
         public DataRow Row { get; }
     }
   
 

#### The state of a row: the DataRowState enum type

The DataRow class provides the RowState property of type DataRowState.

    \[Flags\]
     public enum DataRowState
     {
         Detached = 1,
         Unchanged = 2,
         Added = 4,
         Deleted = 8,
         Modified = 16,
     }
 
 

A data row can be in only one of those states. A data row changes state when it is added deleted or modified. The DataRow.RowState property reports that state.

A row is in Detached state when it is first created and not yet added to the Rows collection.

A Detached row goes to Added state when it is first added to the table. Also the DataRow.SetAdded() method sets the calling row to Added state.

An Unchanged or Added row goes to Modified state when any of its columns gets a new value. Also the DataRow.SetModified() method sets the calling row to Modified state.

An Unchanged, Added or Modified row goes to Deleted state when the DataRow.Delete() method is called. A Deleted row is no more visible. Rows marked as Deleted, are kept in a special cache.

#### The DataTable.AcceptChanges() and RejectChanges() methods

The DataRow, DataTable and DataSet class provide the AcceptChanges() method. The AcceptChanges() commits any pending changes. When AcceptChanges() is called Added and Modified rows go to Unchanged state and Deleted rows removed from their special cache.

CAUTION: The DataRowCollection.Remove() and RemoveAt() methods remove the row from the table but they do not mark the row as Deleted. That is the row does not go to Deleted state. It just disappears.

The DataRow, DataTable and DataSet class provide the RejectChanges() method which rolls back all changes that have been made to the calling level (row, table or dataset) since the last time AcceptChanges() was called.

#### The DataTable.GetChanges() method

The DataTable.GetChanges() method

    public DataTable GetChanges(DataRowState rowStates);
 

returns a new DataTable with just the rows of the calling table with the requested state or states.

    DataTable temp = CreateTempTable();
 
     /\* display the table \*/
     temp.AcceptChanges();
     TableBox.Display(temp);
 
     /\* display added rows \*/
     temp.Rows.Add(111);
 
     DataTable Added = temp.GetChanges(DataRowState.Added);
     if (Added != null)
     {
         Added.TableName = "Added";
         TableBox.Display(Added);
     }
 
     temp.AcceptChanges();
 
 
     /\* display modified rows \*/
     temp.Rows\[0\]\["ID"\] = 123;
     temp.Rows\[2\]\["ID"\] = 456;
 
 
     DataTable Modified = temp.GetChanges(DataRowState.Modified);
     if (Modified != null)
     {
         Modified.TableName = "Modified";
         TableBox.Display(Modified);
     }
 
     temp.AcceptChanges();
 
 
     /\* display deleted rows \*/
     temp.Rows\[2\].Delete();      // DataRow.Delete() marks the row for deletion
     temp.Rows\[4\].Delete();
     temp.Rows.RemoveAt(3);      // WARNING: DataTable.Rows.Remove() and RemoveAt() delete the row and call AcceptChanges()
 
 
     DataTable Deleted = temp.GetChanges(DataRowState.Deleted);
     if (Deleted != null)
     {
         Deleted.TableName = "Deleted";
         Deleted.RejectChanges();    // if RejectChanges() is omitted the Deleted table does not display any row
         TableBox.Display(Deleted);
     }
 
 

#### DataColumn constraints: the ConstraintCollection class

The DataTable provides the Constraints property of type ConstraintCollection.

    public sealed class ConstraintCollection : InternalDataCollectionBase
     {
         protected override ArrayList List { get; }
 
         public Constraint this\[int index\] { get; }
         public Constraint this\[string name\] { get; }
 
         public event CollectionChangeEventHandler CollectionChanged;
 
         public void Add(Constraint constraint);
         public Constraint Add(string name, DataColumn column, bool primaryKey);
         public Constraint Add(string name, DataColumn primaryKeyColumn, DataColumn foreignKeyColumn);
         public Constraint Add(string name, DataColumn\[\] columns, bool primaryKey);
         public Constraint Add(string name, DataColumn\[\] primaryKeyColumns, DataColumn\[\] foreignKeyColumns);
         public void AddRange(Constraint\[\] constraints);
         public bool CanRemove(Constraint constraint);
         public void Clear();
         public bool Contains(string name);
         public void CopyTo(Constraint\[\] array, int index);
         public int IndexOf(Constraint constraint);
         public int IndexOf(string constraintName);
         public void Remove(Constraint constraint);
         public void Remove(string name);
         public void RemoveAt(int index);
     }
 
 

The abstract Constraint class is the element item of the ConstraintCollection class.

    public abstract class Constraint
     {
         protected Constraint();
 
         protected virtual DataSet \_DataSet { get; }
         public virtual string ConstraintName { get; set; }
         public PropertyCollection ExtendedProperties { get; }
         public abstract DataTable Table { get; }
 
         protected void CheckStateForProperty();
         protected internal void SetDataSet(DataSet dataSet);
         public override string ToString();
     }
 
 

A DataColumn may have constraints such as a unique constraint or a foreign key constraint.

#### Unique constraints: the UniqueConstraint class

A unique constraint is placed on a column or a group of columns and it requires that the combination of column values exist just once in that table. The UniqueConstraint class represents a unique constraint.

    public class UniqueConstraint : Constraint
     {
         public UniqueConstraint(DataColumn column);
         public UniqueConstraint(DataColumn\[\] columns);
         public UniqueConstraint(DataColumn column, bool isPrimaryKey);
         public UniqueConstraint(DataColumn\[\] columns, bool isPrimaryKey);
         public UniqueConstraint(string name, DataColumn column);
         public UniqueConstraint(string name, DataColumn\[\] columns);
         public UniqueConstraint(string name, DataColumn column, bool isPrimaryKey);
         public UniqueConstraint(string name, DataColumn\[\] columns, bool isPrimaryKey);
         public UniqueConstraint(string name, string\[\] columnNames, bool isPrimaryKey);
 
         public virtual DataColumn\[\] Columns { get; }
         public bool IsPrimaryKey { get; }
         public override DataTable Table { get; }
 
         public override bool Equals(object key2);
         public override int GetHashCode();
     }
 
 

A unique constraint is added to a DataTable either using the DataColumn.Unique boolean property

    column.Unique = true;
     
 

or using the DataTable.Constraints.Add() method.

    /\* adding a unique constraint \*/
     UniqueConstraint constraint = new UniqueConstraint("UC\_Code\_Material", column);
     table.Constraints.Add(constraint);
     
 

The overloaded versions of the DataTable.Constraints.Add() method

    public Constraint Add(string name, DataColumn column, bool primaryKey);
     public Constraint Add(string name, DataColumn\[\] columns, bool primaryKey);
     
 

are also used to define unique constraints on one or more DataColumns and optionally specify that column or columns as the primary key of the table.

    table.Constraints.Add("PK\_Customers", table.Columns\["ID"\], true);
        
 

#### DataTable.PrimaryKey and primary key constraints

A primary key, just like a unique constraint, is defined on a single column or a group of columns, and uniquely identifies a row among all other rows in the table.

A DataTable can have a primary key defined by using either the DataTable.PrimaryKey property

    table.PrimaryKey = new DataColumn\[\] { table.Columns\["Code"\], table.Columns\["Name"\] };
 
 

or by using a UniqueConstraint object.

    table.Constraints.Add("PK\_CUSTOMER", new DataColumn\[\] { table.Columns\["Code"\], table.Columns\["Name"\] }, true);
     
 

#### DataRow editing methods and DataRow versions

The DataRow.BeginEdit() method temporarily suspends any validation and puts the row into edit mode. Data bound controls call DataRow.BeginEdit() when a user starts editing a DataColumn. The DataRow.EndEdit() ends the edit operation and enforces any validation rule such as a unique constraint. The DataRow.CancelEdit() cancels the edit operation and rollbacks any changes.

The AcceptChanges() method implicitly calls EndEdit() for any row in edit mode.

Regarding that edit mode, a DataRow can hold multiple versions, copies of the same row, as defined by the DataRowVersion enum type.

    public enum DataRowVersion
     {
         Original = 256,
         Current = 512,
         Proposed = 1024,
         Default = 1536,
     }
     
 

When a row is first loaded from a datasource or when the AcceptChanges() method is called then only a single version of the row exists: the Current version.

The BeginEdit() call starts an edit operation and, based on the Current version, creates a new version of the row: the Proposed version. Any change to the data is performed on that Proposed version of the row.

A successful EndEdit() call turns the Current version into the Original version and the Proposed version into the Current version. The Proposed version exists as long as the edit operation is active. After the EndEdit() call only the Current and the Original versions exist.

Not all versions exist all the time. The DataRow provides the method

    public bool HasVersion(DataRowVersion version);
     
 

which indicates whether a version exists at a given time.

The indexer of the DataRow can optionally return a version of the row.

    public object this\[string columnName, DataRowVersion version\] { get; }
 
 

Here is an example

    if (table.Rows.Count > 0)
     {                
         DataRow row = table.Rows\[0\];
 
         /\* commit any penging row changes  \*/
         row.AcceptChanges();    
 
         row.BeginEdit();
         row\["Code"\] = "99";
 
         string S = "";
 
         /\* display the original version of the column \*/
         S = row\["Code", DataRowVersion.Original\].ToString();
         MessageBox.Show("Original: " + S);
 
         /\* display the proposed version of the column \*/
         S = row\["Code", DataRowVersion.Proposed\].ToString();
         MessageBox.Show("Proposed: " + S);
 
         /\* cancel row changes \*/
         row.CancelEdit();                
     } 
 
 
 

#### Locating rows in a DataTable

The DataTable.Rows.Find() method of the DataRowCollection class

    public DataRow Find(object\[\] keys);
     
 

can locate a row based on a primary key. The Find() returns a null object in case of failure.

    DataRow row = table.Rows.Find(new object\[\] { 5, "Name\_5" }); // Find() searches based on the PrimaryKey
     int i = table.Rows.IndexOf(row);
 
 

The DataTable.Select() method

    public DataRow\[\] Select(string filterExpression);
         
 

returns an array of DataRow objects based on a filter expression.

    string expression = "Date >= '{0}' and Date <= '{1}' ";
     expression = string.Format(expression, DateTime.Today.AddDays(5).ToShortDateString(), DateTime.Today.AddDays(7).ToShortDateString());
 
     DataRow\[\] rows = table.Select(expression);      
       
 

#### Computing values on multiple rows

The DataTable.Compute() method

    public object Compute(string expression, string filter);
     
 

computes a value based on an expression on rows that pass the filter criteria.

    object Result = table.Compute("Sum(ID)", "ID > 5");
 
 

The expression parameter requires an aggregate function such as SUM or MIN. It can not contain multiple columns though. Thus given the following DataTable

    DataTable table = new DataTable("Lines");
 
     table.Columns.Add("Qty", typeof(double));
     table.Columns.Add("Price", typeof(double));
     table.Columns.Add("LineValue", typeof(double), "Qty \* Price");
 
     table.Rows.Add(new object\[\] { 2, 5 } );
     table.Rows.Add(new object\[\] { 3, 4 } );
     table.Rows.Add(new object\[\] { 2, 2 } );
     
 

the next line results in an error

    object Result = table.Compute("SUM(Qty \* Price)", "");    
 
 

while the following line executes fine.

    object Result = table.Compute("SUM(LineValue)", ""); 
        
 

#### Saving and loading data tables

DataTable provides a great number of methods for saving/loading the table to a medium. Here is an example which saves a table to a xml disk file.

    string FileName = Path.GetFullPath(@"..\\..\\Table.XML");
     table.WriteXml(FileName, XmlWriteMode.WriteSchema);
 
     DataTable temp = new DataTable();
     temp.ReadXml(FileName);
 
 

#### DataTable.Copy() and Clone() methods

The DataTable.Copy() creates an exact copy of schema and data of the table. The DataTable.Clone() copies just the schema of the table.

    DataTable temp = table.Copy();
 
     temp = table.Clone();
 
 

#### DataView class: DataTable.DefaultView property

The DataView class represents a view of a table data.

    public class DataView : MarshalByValueComponent, IBindingListView, IBindingList, IList, ICollection, IEnumerable, ITypedList, ISupportInitializeNotification, ISupportInitialize
     {
         public DataView();
         public DataView(DataTable table);
         public DataView(DataTable table, string RowFilter, string Sort, DataViewRowState RowState);
 
         public bool AllowDelete { get; set; }
         public bool AllowEdit { get; set; }
         public bool AllowNew { get; set; }
         public bool ApplyDefaultSort { get; set; }
         public int Count { get; }
         public DataViewManager DataViewManager { get; }
         public bool IsInitialized { get; }
         public virtual string RowFilter { get; set; }
         public DataViewRowState RowStateFilter { get; set; }
         public string Sort { get; set; }
         public DataTable Table { get; set; }
 
         public DataRowView this\[int recordIndex\] { get; }
 
         public event EventHandler Initialized;
         public event ListChangedEventHandler ListChanged;
 
         public virtual DataRowView AddNew();
         public void BeginInit();
         public void CopyTo(Array array, int index);
         public void Delete(int index);
         public void EndInit();
         public virtual bool Equals(DataView view);
         public int Find(object key);
         public int Find(object\[\] key);
         public DataRowView\[\] FindRows(object key);
         public DataRowView\[\] FindRows(object\[\] key);
         public IEnumerator GetEnumerator();
         public DataTable ToTable();
         public DataTable ToTable(string tableName);
         public DataTable ToTable(bool distinct, params string\[\] columnNames);
         public DataTable ToTable(string tableName, bool distinct, params string\[\] columnNames);
     }
 
 
 

A DataView can sort and filter the data it represents. Also a DataView can serve as a DataSource to a control. The DataTable provides a DefaultView property of type DataView.

    public partial class MainForm : Form
     {
         public MainForm()
         {
             InitializeComponent();
             PrepareDataTable();
         }
 
         DataTable table = new DataTable("City"); 
  
         void PrepareDataTable()
         {
             table.Columns.Add("ID", typeof(int));
             table.Columns.Add("Name", typeof(string));
 
             table.Rows.Add(new object\[\] {1, "Paris" });
             table.Rows.Add(new object\[\] {2, "London" });
             table.Rows.Add(new object\[\] {3, "Athens" });
             table.Rows.Add(new object\[\] {4, "Berlin" });
             table.Rows.Add(new object\[\] {5, "Rome" });
 
             Grid.DataSource = table.DefaultView;            
         }
 
         private void btnSort\_Click(object sender, EventArgs e)
         {
             table.DefaultView.Sort = edtSort.Text;
         }
 
         private void btnRowFilter\_Click(object sender, EventArgs e)
         {
             table.DefaultView.RowFilter = edtRowFilter.Text;
         } 
     }
     
 

It is possible to create additional DataView objects with any required configuration and use that DataView as a DataSource of a control.

#### The DataSet class

The DataSet class represents a set of DataTable objects. The DataSet class is a disconnected object. Just like the DataTable it has no reference to a connection object.

    public class DataSet : MarshalByValueComponent, IListSource, IXmlSerializable, ISupportInitializeNotification, ISupportInitialize, ISerializable
     {
         public DataSet();
         public DataSet(string dataSetName);
 
         public bool CaseSensitive { get; set; }
         public string DataSetName { get; set; }
         public DataViewManager DefaultViewManager { get; }
         public bool EnforceConstraints { get; set; }
         public PropertyCollection ExtendedProperties { get; }
         public bool HasErrors { get; }
         public bool IsInitialized { get; }
         public CultureInfo Locale { get; set; }
         public string Namespace { get; set; }
         public string Prefix { get; set; }
         public DataRelationCollection Relations { get; }
         public SerializationFormat RemotingFormat { get; set; }
         public virtual SchemaSerializationMode SchemaSerializationMode { get; set; }
         public override ISite Site { get; set; }
         public DataTableCollection Tables { get; }
 
         public event EventHandler Initialized;
         public event MergeFailedEventHandler MergeFailed;
 
         public void AcceptChanges();
         public void BeginInit();
         public void Clear();
         public virtual DataSet Clone();
         public DataSet Copy();
         public DataTableReader CreateDataReader();
         public DataTableReader CreateDataReader(params DataTable\[\] dataTables);
         public void EndInit();
         public DataSet GetChanges();
         public DataSet GetChanges(DataRowState rowStates);
         public static XmlSchemaComplexType GetDataSetSchema(XmlSchemaSet schemaSet);
         public virtual void GetObjectData(SerializationInfo info, StreamingContext context);
         public string GetXml();
         public string GetXmlSchema();
         public bool HasChanges();
         public bool HasChanges(DataRowState rowStates);
         public void InferXmlSchema(Stream stream, string\[\] nsArray);
         public void InferXmlSchema(string fileName, string\[\] nsArray);
         public void InferXmlSchema(TextReader reader, string\[\] nsArray);
         public void InferXmlSchema(XmlReader reader, string\[\] nsArray);
         public void Load(IDataReader reader, LoadOption loadOption, params DataTable\[\] tables);
         public void Load(IDataReader reader, LoadOption loadOption, params string\[\] tables);
         public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler, params DataTable\[\] tables);
         public void Merge(DataRow\[\] rows);
         public void Merge(DataSet dataSet);
         public void Merge(DataTable table);
         public void Merge(DataSet dataSet, bool preserveChanges);
         public void Merge(DataRow\[\] rows, bool preserveChanges, MissingSchemaAction missingSchemaAction);
         public void Merge(DataSet dataSet, bool preserveChanges, MissingSchemaAction missingSchemaAction);
         public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction);
         public XmlReadMode ReadXml(Stream stream);
         public XmlReadMode ReadXml(string fileName);
         public XmlReadMode ReadXml(TextReader reader);
         public XmlReadMode ReadXml(XmlReader reader);
         public XmlReadMode ReadXml(Stream stream, XmlReadMode mode);
         public XmlReadMode ReadXml(string fileName, XmlReadMode mode);
         public XmlReadMode ReadXml(TextReader reader, XmlReadMode mode);
         public XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode);
         public void ReadXmlSchema(Stream stream);
         public void ReadXmlSchema(string fileName);
         public void ReadXmlSchema(TextReader reader);
         public void ReadXmlSchema(XmlReader reader);
         public virtual void RejectChanges();
         public virtual void Reset();
         public void WriteXml(Stream stream);
         public void WriteXml(string fileName);
         public void WriteXml(TextWriter writer);
         public void WriteXml(XmlWriter writer);
         public void WriteXml(Stream stream, XmlWriteMode mode);
         public void WriteXml(string fileName, XmlWriteMode mode);
         public void WriteXml(TextWriter writer, XmlWriteMode mode);
         public void WriteXml(XmlWriter writer, XmlWriteMode mode);
         public void WriteXmlSchema(Stream stream);
         public void WriteXmlSchema(string fileName);
         public void WriteXmlSchema(TextWriter writer);
         public void WriteXmlSchema(XmlWriter writer);
     }        
     
 

Here is how to create a DataSet.

    DataSet ds = new DataSet("CustomerOrders");
 
 

And here is how to create DataTable objects and add them to a DataSet.

    DataTable tblOrders = new DataTable("Orders");
     DataTable tblLines = new DataTable("Lines");   
     
     ...
 
     ds.Tables.Add(tblOrders);
     ds.Tables.Add(tblLines);  
        
 

#### Foreign key constraints: the ForeignKeyConstraint class

A foreign key declares that one or more columns of a table, reference one or more columns of another table. For example, the Customer.CityID column references the City.ID column. The Customer.CityID is the foreign key.

Foreign keys are declared as constraints. Foreign key constraints enforce the so-called "referential integrity" between two tables. A foreign key constraint ensures that child table rows reference an existing row in a parent table and they will not become orphan because of a change to the parent table. A foreign key constraint may also define actions to performed when parent rows are deleted or updated.

The ForeignKeyConstraint class represents a foreign key constraint.

    public class ForeignKeyConstraint : Constraint
     {
         public ForeignKeyConstraint(DataColumn parentColumn, DataColumn childColumn);
         public ForeignKeyConstraint(DataColumn\[\] parentColumns, DataColumn\[\] childColumns);
         public ForeignKeyConstraint(string constraintName, DataColumn parentColumn, DataColumn childColumn);
         public ForeignKeyConstraint(string constraintName, DataColumn\[\] parentColumns, DataColumn\[\] childColumns);
         public ForeignKeyConstraint(string constraintName, string parentTableName, string\[\] parentColumnNames, string\[\] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule);
         public ForeignKeyConstraint(string constraintName, string parentTableName, string parentTableNamespace, string\[\] parentColumnNames, string\[\] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule);
 
         public virtual AcceptRejectRule AcceptRejectRule { get; set; }
         public virtual DataColumn\[\] Columns { get; }
         public virtual Rule DeleteRule { get; set; }
         public virtual DataColumn\[\] RelatedColumns { get; }
         public virtual DataTable RelatedTable { get; }
         public override DataTable Table { get; }
         public virtual Rule UpdateRule { get; set; }
 
         public override bool Equals(object key);
         public override int GetHashCode();
     }
     
 

A foreign key constraint can be added to a table by using either the ForeignKeyConstraint class

    ForeignKeyConstraint fkcOrders = new ForeignKeyConstraint("FK\_ORDERS", tblOrders.Columns\["ID"\], tblLines.Columns\["OrderID"\]);
     tblLines.Constraints.Add(fkcOrders);
 
 

or one of the following overloaded versions of the DataTable.Constraints.Add() method

    public Constraint Add(string name, DataColumn primaryKeyColumn, DataColumn foreignKeyColumn);
     public Constraint Add(string name, DataColumn\[\] primaryKeyColumns, DataColumn\[\] foreignKeyColumns);
 
 

Here is an example

    tblLines.Constraints.Add("FK\_ORDERS", tblOrders.Columns\["ID"\], tblLines.Columns\["OrderID"\]);
     
 

#### Foreign key constraing rules

The properties DeleteRule, UpdateRule and AcceptRejectRule of the ForeignKeyConstraint class control what happens to child rows when a parent row is deleted, updated or the AcceptChanges or RejectChanges method is invoked.

The DeleteRule and UpdateRule are of the Rule enum type.

    public enum Rule
     {
         None = 0,
         Cascade = 1,        // Delete or update related rows. This is the default
         SetNull = 2,        // Set values in related rows to DBNull.
         SetDefault = 3,     // Set values in related rows to the value contained in the DefaultValue property.
     }
     
 

The AcceptRejectRule is of the AcceptRejectRule enum type.

    public enum AcceptRejectRule
     {
         None = 0,
         Cascade = 1,        // Changes are cascaded across the relationship
     }
 
 

Here is an example.

    ForeignKeyConstraint fkcOrders = new ForeignKeyConstraint("FK\_ORDERS", tblOrders.Columns\["ID"\], tblLines.Columns\["OrderID"\]);
     fkcOrders.DeleteRule = Rule.SetNull;
     fkcOrders.UpdateRule = Rule.Cascade;
     fkcOrders.AcceptRejectRule = AcceptRejectRule.Cascade;
     tblLines.Constraints.Add(fkcOrders);
     
 

#### DataRelation class: a master-detail relationship

A DataRelation object represents a master-detail relationship between two DataTable objects.

    public class DataRelation
     {
         public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn);
         public DataRelation(string relationName, DataColumn\[\] parentColumns, DataColumn\[\] childColumns);
         public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn, bool createConstraints);
         public DataRelation(string relationName, DataColumn\[\] parentColumns, DataColumn\[\] childColumns, bool createConstraints);
         public DataRelation(string relationName, string parentTableName, string childTableName, string\[\] parentColumnNames, string\[\] childColumnNames, bool nested);
         public DataRelation(string relationName, string parentTableName, string parentTableNamespace, string childTableName, string childTableNamespace, string\[\] parentColumnNames, string\[\] childColumnNames, bool nested);
 
         public virtual DataColumn\[\] ChildColumns { get; }
         public virtual ForeignKeyConstraint ChildKeyConstraint { get; }
         public virtual DataTable ChildTable { get; }
         public virtual DataSet DataSet { get; }
         public PropertyCollection ExtendedProperties { get; }
         public virtual bool Nested { get; set; }
         public virtual DataColumn\[\] ParentColumns { get; }
         public virtual UniqueConstraint ParentKeyConstraint { get; }
         public virtual DataTable ParentTable { get; }
         public virtual string RelationName { get; set; }
 
         public override string ToString();
     }
 
 

DataRelation objects are kept in DataRelationCollection collection objects.

    public abstract class DataRelationCollection : InternalDataCollectionBase
     {
         public abstract DataRelation this\[int index\] { get; }
         public abstract DataRelation this\[string name\] { get; }
 
         public event CollectionChangeEventHandler CollectionChanged;
 
         public void Add(DataRelation relation);
         public virtual DataRelation Add(DataColumn parentColumn, DataColumn childColumn);
         public virtual DataRelation Add(DataColumn\[\] parentColumns, DataColumn\[\] childColumns);
         public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn);
         public virtual DataRelation Add(string name, DataColumn\[\] parentColumns, DataColumn\[\] childColumns);
         public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn, bool createConstraints);
         public virtual DataRelation Add(string name, DataColumn\[\] parentColumns, DataColumn\[\] childColumns, bool createConstraints);
         public virtual void AddRange(DataRelation\[\] relations);
         public virtual bool CanRemove(DataRelation relation);
         public virtual void Clear();
         public virtual bool Contains(string name);
         public void CopyTo(DataRelation\[\] array, int index);
         public virtual int IndexOf(DataRelation relation);
         public virtual int IndexOf(string relationName);
         public void Remove(DataRelation relation);
         public void Remove(string name);
         public void RemoveAt(int index);
     }
 
 
 

The DataSet class provides the Relations property

    public DataRelationCollection Relations { get; }
 
 

and the DataTable provides the ChildRelations and ParentRelations properties.

    public DataRelationCollection ChildRelations { get; }
     public DataRelationCollection ParentRelations { get; }
  
 

DataRelation objects are only valid between DataTable objects which belong to the same DataSet. A DataRelation is created between matching columns in the master and detail tables. The value of the DataType property for both columns must be identical.

    DataRelation relation = new DataRelation("OrderLines", tblOrders.Columns\["ID"\], tblLines.Columns\["OrderID"\]);
     ds.Relations.Add(relation);    
 
 

or

    tblOrders.ChildRelations.Add("OrderLines", tblOrders.Columns\["ID"\], tblLines.Columns\["OrderID"\]);
     
 

And here is an example of how to setup two DataGridView controls in a master-detail relationship.

    /\* pass the ds DataSet as the DataSource to both DataGridView objects, instead of DataTables \*/
     gridMaster.DataSource = ds;
     gridDetail.DataSource = ds;
 
     /\* set the DataMember properly, in order to have 
        automatic generation of the OrderID value for the detail table \*/
     gridMaster.DataMember = "Orders";
     gridDetail.DataMember = "Orders.OrderLines";  // OrderLines refers to the DataRelation here
     
     
 

The DataRow class provides the GetChildRows(), GetParentRow() and GetParentRows() which return the rows in a relation.

    public class DataRow
     {
      ...
         public DataRow\[\] GetChildRows(DataRelation relation);
         public DataRow\[\] GetChildRows(string relationName);   
      ... 
         public DataRow GetParentRow(DataRelation relation);
         public DataRow GetParentRow(string relationName);
      ... 
         public DataRow\[\] GetParentRows(DataRelation relation);
         public DataRow\[\] GetParentRows(string relationName);    
      ...     
     }      
     
 

### Accessing datasources

ADO.NET provides classes that can connect to a datasource, fetch data, alter data and save the modifications back to the datasource.

#### Sample database

The examples in this section use a MS Access database with the following schema and data.

    create table COUNTRY
     (  ID           integer identity not null primary key,
        CODE         varchar(4),
        NAME         varchar(32)
     );
     
     create table CITY
     (
        ID           integer identity not null primary key,
        COUNTRY\_ID   integer constraint FK\_CITY\_0 references COUNTRY(ID),
        NAME         varchar(32)
     );    
     
     create table TRADER
     (
        ID           integer identity not null primary key,
        NAME         varchar(32),
        COUNTRY\_ID   integer constraint FK\_TRADER\_0 references COUNTRY(ID) 
     );    
     
     create table MATERIAL
     (
         ID          integer identity not null primary key,
         NAME        varchar(32),
         PRICE       float,
         VAT         float
     );    
        
     create table TRADE
     (
         ID          integer identity not null primary key,
         TRADER\_ID   integer constraint FK\_TRADE\_0 references TRADER(ID),
         TRADE\_TYPE  integer,
         TRADE\_DATE  date,
         TOTAL\_VALUE float
     );
     
     create table TRADE\_LINES
     (
         ID          integer identity not null primary key,
         TRADE\_ID    integer constraint FK\_TRADE\_LINES\_0 references TRADE(ID),
         MATERIAL\_ID integer constraint FK\_TRADE\_LINES\_1 references MATERIAL(ID),
         QTY         float,
         PRICE       float,
         VAT         float,
         LINE\_VALUE  float
     );
     
     create table PICTURES
     (
         ID integer identity not null primary key,
         IMG image
     );
     
     
     insert into COUNTRY (CODE, NAME) values ('ES', 'Spain');
     insert into COUNTRY (CODE, NAME) values ('CZ', 'Czech Republic');
     insert into COUNTRY (CODE, NAME) values ('GR', 'Greece');
     insert into COUNTRY (CODE, NAME) values ('DK', 'Denmark');
     insert into COUNTRY (CODE, NAME) values ('UK', 'United Kingdom');
     
     insert into CITY (COUNTRY\_ID, NAME) values (1, 'Madrid');
     insert into CITY (COUNTRY\_ID, NAME) values (1, 'Barcelona');
     insert into CITY (COUNTRY\_ID, NAME) values (1, 'Valencia');
     insert into CITY (COUNTRY\_ID, NAME) values (2, 'Prague');
     insert into CITY (COUNTRY\_ID, NAME) values (2, 'Brno');
     insert into CITY (COUNTRY\_ID, NAME) values (3, 'Athens');
     insert into CITY (COUNTRY\_ID, NAME) values (3, 'Thessaloniki');
     insert into CITY (COUNTRY\_ID, NAME) values (4, 'Copenhagen');
     insert into CITY (COUNTRY\_ID, NAME) values (4, 'Aarhus');
     insert into CITY (COUNTRY\_ID, NAME) values (5, 'London');
     insert into CITY (COUNTRY\_ID, NAME) values (5, 'Edinburgh');    
     
     insert into TRADER (NAME, COUNTRY\_ID) values ('Cheap Software Ltd'    , '3'); 
     insert into TRADER (NAME, COUNTRY\_ID) values ('Fast Internet Co'      , '4'); 
     insert into TRADER (NAME, COUNTRY\_ID) values ('Neat Hardware'         , '1'); 
     insert into TRADER (NAME, COUNTRY\_ID) values ('Cool Tech'             , '4'); 
     insert into TRADER (NAME, COUNTRY\_ID) values ('Steady Logic'          , '2'); 
     insert into TRADER (NAME, COUNTRY\_ID) values ('Nasty Data'            , '3');     
     
     insert into MATERIAL (NAME, PRICE, VAT) values ('Hard disk'     ,  50 , 0.19); 
     insert into MATERIAL (NAME, PRICE, VAT) values ('Mouse'         ,   5 , 0.19); 
     insert into MATERIAL (NAME, PRICE, VAT) values ('Keyboard'      ,  12 , 0.19); 
     insert into MATERIAL (NAME, PRICE, VAT) values ('Soundcard'     ,  17 , 0.19); 
     insert into MATERIAL (NAME, PRICE, VAT) values ('Scanner'       ,  65 , 0.19); 
     insert into MATERIAL (NAME, PRICE, VAT) values ('Monitor'       , 120 , 0.19); 
     
     insert into TRADE (TRADER\_ID, TRADE\_TYPE, TRADE\_DATE, TOTAL\_VALUE) values (4, 1, '2004-07-12', 0);    
     insert into TRADE (TRADER\_ID, TRADE\_TYPE, TRADE\_DATE, TOTAL\_VALUE) values (6, 1, '2004-07-13', 0);  
     insert into TRADE (TRADER\_ID, TRADE\_TYPE, TRADE\_DATE, TOTAL\_VALUE) values (3, 1, '2004-07-13', 0);  
     
     insert into TRADE\_LINES (TRADE\_ID, MATERIAL\_ID, QTY, PRICE, VAT, LINE\_VALUE) values (1, 2, 1,  5, 0.19, (1 \* 5 ) + (1 \*  5 \* 0.19));
     insert into TRADE\_LINES (TRADE\_ID, MATERIAL\_ID, QTY, PRICE, VAT, LINE\_VALUE) values (1, 3, 1, 12, 0.19, (1 \* 12) + (1 \* 12 \* 0.19));
     
     insert into TRADE\_LINES (TRADE\_ID, MATERIAL\_ID, QTY, PRICE, VAT, LINE\_VALUE) values (2, 4, 2, 17, 0.19, (2 \* 17) + (2 \* 17 \* 0.19));
     insert into TRADE\_LINES (TRADE\_ID, MATERIAL\_ID, QTY, PRICE, VAT, LINE\_VALUE) values (2, 5, 1, 65, 0.19, (1 \* 65) + (1 \* 65 \* 0.19));
     insert into TRADE\_LINES (TRADE\_ID, MATERIAL\_ID, QTY, PRICE, VAT, LINE\_VALUE) values (2, 2, 2,  5, 0.19, (2 \* 5 ) + (2 \*  5 \* 0.19));
     
     insert into TRADE\_LINES (TRADE\_ID, MATERIAL\_ID, QTY, PRICE, VAT, LINE\_VALUE) values (3, 6, 1, 120, 0.19, (1 \* 120) + (1 \* 120 \* 0.19));
     
     update TRADE set TOTAL\_VALUE =  20.23 where ID = 1;
     update TRADE set TOTAL\_VALUE = 129.71 where ID = 2;
     update TRADE set TOTAL\_VALUE = 142.80 where ID = 3;
     
 
 
 

#### Data Providers

Different datasources require different groups of ADO.NET classes. Such a group of classes is known collectively as a Data Provider. An ADO.NET Data Provider is a group of classes used in accessing a specific type of a datasource.

ADO.NET comes with a set of predefined Data Providers. Here is the Data Provider list along with their namespaces.

*   OleDb (System.Data.OleDb)
*   ODBC (System.Data.Odbc)
*   MS SQL (System.Data.SqlClient)
*   SQLCE (System.Data.SqlServerCe)
*   Oracle (System.Data.OracleClient)

The System.Data namespace defines a group of interfaces which define the functionality a Data Provider should provide.

*   IDbConnection
*   IDbCommand
*   IDbTransaction
*   IDataReader
*   IDataRecord
*   IDataAdapter
*   IDbDataAdapter
*   IDataParameterCollection
*   IDataParameter
*   IDbDataParameter

The System.Data.Common namespace provides the base classes a Data Provider should derive from. Here is the most significant of them and the interfaces they implement.

*   DbConnection (IDbConnection)
*   DbCommand (IDbCommand)
*   DbTransaction (IDbTransaction)
*   DbDataReader (IDataReader, IDataRecord)
*   DbDataAdapter (IDbDataAdapter, IDataAdapter)
*   DbParameterCollection (IDataParameterCollection)
*   DbParameter (IDbDataParameter, IDataParameter)
*   DbCommandBuilder
*   DbConnectionStringBuilder

NOTE: Before the .Net 2.0, Data Providers were just implementations of the interfaces defined in the System.Data. That is there was not a base hierarchy common to all Data Providers. Each Data Provider had its own totally independent hierarchy.

A specific Data Provider provides descendants of those base classes suitable for the type of datasource it represents. Here is the list for the MS SQL Data Provider

*   SqlConnection
*   SqlCommand
*   SqlTransaction
*   SqlDataReader
*   SqlDataAdapter
*   SqlParameterCollection
*   SqlParameter
*   SqlCommandBuilder
*   SqlConnectionStringBuilder

Except of the above Data Providers that ship with the .Net, there are a lot of Data Providers both freeware and commercial.

see also:

*   [http://sourceforge.net/projects/sqlite-dotnet2](   http://sourceforge.net/projects/sqlite-dotnet2)
*   [http://uda.openlinksw.com/dotnet/mt/dotnet-mysql-mt/](http://uda.openlinksw.com/dotnet/mt/dotnet-mysql-mt/)
*   [http://www.devart.com/dotconnect/postgresql/](http://www.devart.com/dotconnect/postgresql/)
*   [http://www.ibphoenix.com/main.nfs?page=ibp\_download\_dotnet](http://www.ibphoenix.com/main.nfs?page=ibp_download_dotnet)

#### Data types

The System.Data namespace defines the DbType enumeration which specifies the data type of a column or a DbParameter object.

    public enum DbType
     {
         AnsiString = 0,
         Binary = 1,
         Byte = 2,
         Boolean = 3,
         Currency = 4,
         Date = 5,
         DateTime = 6,
         Decimal = 7,
         Double = 8,
         Guid = 9,
         Int16 = 10,
         Int32 = 11,
         Int64 = 12,
         Object = 13,
         SByte = 14,
         Single = 15,
         String = 16,
         Time = 17,
         UInt16 = 18,
         UInt32 = 19,
         UInt64 = 20,
         VarNumeric = 21,
         AnsiStringFixedLength = 22,
         StringFixedLength = 23,
         Xml = 25,
         DateTime2 = 26,
         DateTimeOffset = 27 
     }
     
 

Although this DbType is defined, a Data Provider can define and use its own data types specific to the datasource it represents. Here is the System.Data.SqlClient.SqlDbType enumeration.

    public enum SqlDbType
     {
         BigInt = 0,
         Binary = 1,
         Bit = 2,
         Char = 3,
         DateTime = 4,
         Decimal = 5,
         Float = 6,
         Image = 7,
         Int = 8,
         Money = 9,
         NChar = 10,
         NText = 11,
         NVarChar = 12,
         Real = 13,
         UniqueIdentifier = 14,
         SmallDateTime = 15,
         SmallInt = 16,
         SmallMoney = 17,
         Text = 18,
         Timestamp = 19,
         TinyInt = 20,
         VarBinary = 21,
         VarChar = 22,
         Variant = 23,
         Xml = 25,
         Udt = 29,
         Structured = 30,
         Date = 31,
         Time = 32,
         DateTime2 = 33,
         DateTimeOffset = 34
     }
     
     
 

#### Provider neutral data access and Provider Factories

A database application should be written in a provider neutral manner. Except of very well justified situations. Writing provider specific code ties the application to a specific provider.

Starting from .Net 2.0 the System.Data.Common namespace provides the DbProviderFactories and the DbProviderFactory class. These two classes help in writing provider neutral code.

    public static class DbProviderFactories
     {
         public static DbProviderFactory GetFactory(DataRow providerRow);
         public static DbProviderFactory GetFactory(string providerInvariantName);
         public static DataTable GetFactoryClasses();
     }
 
     public abstract class DbProviderFactory
     {
         protected DbProviderFactory();
 
         public virtual bool CanCreateDataSourceEnumerator { get; }
 
         public virtual DbCommand CreateCommand();
         public virtual DbCommandBuilder CreateCommandBuilder();
         public virtual DbConnection CreateConnection();
         public virtual DbConnectionStringBuilder CreateConnectionStringBuilder();
         public virtual DbDataAdapter CreateDataAdapter();
         public virtual DbDataSourceEnumerator CreateDataSourceEnumerator();
         public virtual DbParameter CreateParameter();
         public virtual CodeAccessPermission CreatePermission(PermissionState state);
     }
     
 

The machine.config file found at

    C:\\<windows>\\Microsoft.NET\\Framework\\<version>\\config\\machine.config 
 

is a .Net config file containing machine-wide settings. A data provider which exposes factory functionality, registers itself to this file. Here is a fragment taken from that file.

    <system.data>
       <DbProviderFactories>
         <add name="SqlClient Data Provider"
          invariant="System.Data.SqlClient" 
          description=".Net Framework Data Provider for SqlServer" 
          type="System.Data.SqlClient.SqlClientFactory, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
         />
         
         ...
         
       </DbProviderFactories>
     </system.data>
     
 

The DbProviderFactories.GetFactoryClasses() method returns information regarding registered providers. That information is returned as a DataTable object.

    Grid.DataSource = DbProviderFactories.GetFactoryClasses();
     
 

An application can use the DbProviderFactories static class in order to get a proper DbProviderFactory object, based on the invariant name, for any of the registered data providers.

    DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
     
 

Then that factory can be used to create connections, commands and adapters.

    DbConnection connection = factory.CreateConnection();      
        
 

#### Connection strings

A connection requires a proper connection string in regard to the provider. A connection string describes the datasource and other connection settings and it actually is a series of key=value semicolon delimited pairs. The order and the case sensitivity of those key=value pairs is irrelevant.

    key1=value1; key2=value2; ... keyn=valuen;
 
 

The exact format and the meaning of the keys and values is provider specific. The ConnectionString property of a DbConnection derived class documents the specific syntax for each provider.

#### OleDb connection string

The most common keywords used by an OleDb connection string are

*   Provider
*   Data Source
*   Initial Catalog
*   Persist Security Info
*   Integrated Security
*   User ID
*   Password or Pwd

It is easy to create a connection string for the System.Data.OleDb provider. Just create a new empty text file somewhere and rename its extension to .udl, which stands for Universal Data Link. For example, c:\\Temp\\ConnectionString.udl.

Double clicking on the .udl file displays the Data Link Properties system dialog box which is then used to construct the connection string. The constructed connection string is automatically saved to the .udl file, which is just a plain .ini file. It can be opened with the Notepad or any other text editor.

The first page of the Data Link Properties dialog box displays all the installed OLEDB providers in the system. Next pages provide settings that vary, according to the OLEDB provider.

NOTE: OLEDB is a midware system. It is the successor of the ODBC technology. An OLEDB provider is a driver used in accessing a specific type of a datasource. An ODBC driver is a group of functions. An OLEDB provider is the implementation of a group of interfaces. The ADO.NET OleDb Data Provider uses an OLEDB provider to access data. Do not confuse OLEDB providers with ADO.NET Data Providers.

Here are some sample connection strings for the System.Data.OleDb provider.

    Provider=SQLOLEDB.1; Data Source=MyComputerName; Initial Catalog=myMsSqlDatabase; Integrated Security=SSPI; Persist Security Info=False;
     Provider=MSDAORA; Data Source=InstanceOfOracleServerToConnect; User Id=admin; Password=<password>;    
     Provider=Microsoft.Jet.OLEDB.4.0; Data Source="C:\\My App\\MyDB.MDB"; User Id=admin; Password=;
     Provider=Microsoft.Jet.OLEDB.4.0; Data Source="C:\\My App\\Data.XLS"; Extended Properties=Excel 8.0;    
     Provider=Microsoft.Jet.OLEDB.4.0; Data Source="C:\\My App\\"; Extended Properties="text; HDR=Yes; FMT=Delimited(,)";
 
 

NOTE: Do not use the "Microsoft OLEDB Provider for ODBC Drivers", (MSDASQL), with the System.Data.OleDb provider because the System.Data.OleDb provider does not support the OLEDB Provider for ODBC (MSDASQL). If the datasource is an ODBC datasource then use the System.Data.Odbc provider.

#### Other connection string examples

Here are some connection string examples for various datasources.

System.Data.SqlClient (MS SQL server).

    Data Source=localhost; Initial Catalog=MyDB; Integrated Security=SSPI;
     Data Source=.\\SQLEXPRESS; Initial Catalog=MyDB; AttachDbFilename="C:\\My App\\MyDB.mdf"; Integrated Security=SSPI;  
 
 

System.Data.OracleClient (Oracle server).

    Data Source=InstanceOfOracleServerToConnect; User ID=admin; Password=<password>;
     
 

FirebirdSql.Data.FirebirdClient (Firebird SQL server)

    User=SYSDBA; Password=masterkey; DataSource=localhost; Database="C:\\My App\\Data.FDB";
     
 

System.Data.Odbc (ODBC connections)

    Driver={SQL Server}; Server=(local); Database=MyDB; Trusted\_Connection=Yes; 
     DSN=OdbcDsnName; 
 
 

NOTE: The ODBC applet in the Control Panel can be used to create DSN names.

see also:

*   [http://www.connectionstrings.com/](   http://www.connectionstrings.com/)
*   [http://www.carlprothman.net/Default.aspx?tabid=81](http://www.carlprothman.net/Default.aspx?tabid=81)
*   [http://www.firebirdsql.org/](http://www.firebirdsql.org/)

#### Connection string Builders

To help with building connection strings each provider provides a specific connection string builder. Connection string builders inherit from the base DbConnectionStringBuilder.

    public class DbConnectionStringBuilder : IDictionary, ICollection, IEnumerable, ICustomTypeDescriptor
     {
         public DbConnectionStringBuilder();
         public DbConnectionStringBuilder(bool useOdbcRules);
 
         public bool BrowsableConnectionString { get; set; }
         public string ConnectionString { get; set; }
         public virtual int Count { get; }
         public virtual bool IsFixedSize { get; }
         public bool IsReadOnly { get; }
         public virtual ICollection Keys { get; }
         public virtual ICollection Values { get; }
 
         public virtual object this\[string keyword\] { get; set; }
 
         public void Add(string keyword, object value);
         public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value);
         public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value, bool useOdbcRules);
         public virtual void Clear();
         public virtual bool ContainsKey(string keyword);
         public virtual bool EquivalentTo(DbConnectionStringBuilder connectionStringBuilder);
         public virtual bool Remove(string keyword);
         public virtual bool ShouldSerialize(string keyword);
         public override string ToString();
         public virtual bool TryGetValue(string keyword, out object value);
     }
     
 

Here is the list of the provided connection string builders

*   SqlConnectionStringBuilder
*   OleDbConnectionStringBuilder
*   OdbcConnectionStringBuilder
*   OracleConnectionStringBuilder

As always it is easier to work with a neutral connection string builder.

    DbConnectionStringBuilder csBuilder = factory.CreateConnectionStringBuilder(); 
  
     csBuilder\["Data Source"\] = "localhost";
     csBuilder\["Initial Catalog"\] = "MyDB";
     csBuilder\["Integrated Security"\] = true;
     
     string cs = csBuilder.ConnectionString;
 
 

#### Connection strings and configuration files

Application configuration files, such as app.config and web.config, may contain connection string settings.

    <?xml version="1.0" encoding="utf-8" ?>
     <configuration>
       <connectionStrings>
         <add
            name="MyDB"
            connectionString="Data Source=localhost; Initial Catalog=MyDB; Integrated Security=SSPI;"
            providerName="System.Data.SqlClient"
        />
        
        <add
             ...
        />
        
       </connectionStrings>
     </configuration>
     
 

The System.Configuration namespace (in System.Configuration.dll assembly) contains classes capable of handling that information.

    ConnectionStringSettings csSettings = ConfigurationManager.ConnectionStrings\["MyDB"\];
     string cs = csSettings.ConnectionString;
     
 

#### The DbConnection class

The DbConnection class represents a connection to a database.

    public abstract class DbConnection : Component, IDbConnection, IDisposable
     {
         public abstract string ConnectionString { get; set; }
         public virtual int ConnectionTimeout { get; }
         public abstract string Database { get; }
         public abstract string DataSource { get; }
         public abstract string ServerVersion { get; }
         public abstract ConnectionState State { get; }
 
         public virtual event StateChangeEventHandler StateChange;
 
         public DbTransaction BeginTransaction();
         public DbTransaction BeginTransaction(IsolationLevel isolationLevel);
         public abstract void ChangeDatabase(string databaseName);
         public abstract void Close();
         public DbCommand CreateCommand();
         public virtual void EnlistTransaction(System.Transactions.Transaction transaction);
         public virtual DataTable GetSchema();
         public virtual DataTable GetSchema(string collectionName);
         public virtual DataTable GetSchema(string collectionName, string\[\] restrictionValues);
         public abstract void Open();
     }
 
 
 

Each data provider provides its own DbConnection descendant connection class, i.e. SqlConnection, OleDbConnection etc. Any interaction with a database requires an opened DbConnection object. A connection opens by calling the DbConnection.Open() method. A connection should be closed as soon as possible since it refers to a disk file through an operation system file handle. That is a connection owns unmanaged resources.

DbConnection class provides the Close() method for closing the connection. The DbConnection class implements the IDisposable interface and its Dispose() is functionally equivalent to Close(). Furthermore calling Close() more than one time generates no exceptions.

WARNING: The Close() method rolls back any pending transactions.

Here is a snippet that uses the using keyword to close and dispose the connection.

    string FileName = Path.GetFullPath(@"..\\..\\..\\Lessons.MDB");
     string cs = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}""; User Id=admin; Password=;", FileName);
     
     DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
     using (DbConnection con = factory.CreateConnection())
     {
         con.ConnectionString = cs;
         con.Open();
 
         ...           
     }
 
 

The above could be written using a try-finally block, as

    DbConnection con = factory.CreateConnection();
     try
     {
         con.ConnectionString = cs;
         con.Open();
 
         ...   
     }
     finally
     {
        con.Close(); // or con.Dispose();
     }
 
 

#### Connection pooling

Regarding connection life-time there are two models: the connected and the disconnected model.

In connected model a connection remains open until the application terminates. In disconnected model an application connects to a database for just a short period of time, enough to retrieve or modify data and then closes the connection.

ADO.NET promotes the disconnected model. The problem with disconnected model is that repeatedly opening and closing connections to a datasource can be time consuming.

Connection pooling is an optimization technique used by data providers in order to minimize the cost of repeatedly opening and closing connections. What basically happens when the Close() method of a DbConnection object is called, is that although the object is disposed, the actual connection handle is kept to a connection pool. Later on when application code needs to create a new DbConnection object with the same connection string, the provider uses that existing connection handle for that certain connection string thus minimizing the time cost.

Connection pooling happens automatically. Some providers use special settings in connection strings in order to further refine the connection pooling and some concrete DbConnection descendants provide special members for that same reason.

#### The DbCommand class

The DbCommand class represents a Data Definition (DDL) such as CREATE, ALTER, DROP etc. or a Data Manipulation (DML) SQL statement, such as SELECT, INSERT, CREATE TABLE etc.

    public abstract class DbCommand : Component, IDbCommand, IDisposable
     {
         public abstract string CommandText { get; set; }
         public abstract int CommandTimeout { get; set; }
         public abstract CommandType CommandType { get; set; }
         public DbConnection Connection { get; set; }
         public abstract bool DesignTimeVisible { get; set; }
         public DbParameterCollection Parameters { get; }
         public DbTransaction Transaction { get; set; }
         public abstract UpdateRowSource UpdatedRowSource { get; set; }
 
         public abstract void Cancel();
         public DbParameter CreateParameter();
         public abstract int ExecuteNonQuery();
         public DbDataReader ExecuteReader();
         public DbDataReader ExecuteReader(CommandBehavior behavior);
         public abstract object ExecuteScalar();
         public abstract void Prepare();
     }
 
 

The Connection property associates the DbCommand to a DbConnection instance. The string property CommandText contains the command text that is going to be executed. The CommandType property specifies how the command text should be interpreted.

    public enum CommandType
     {
         Text = 1,
         StoredProcedure = 4,
         TableDirect = 512        
     }
     
 

The Parameters property, of type DbParameterCollection, is used by parameterized commands. The Transaction property, of type DbTransaction, is used to specify the transaction within which the DbCommand object executes.

    DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
     using (DbConnection con = factory.CreateConnection())
     {
         con.ConnectionString = cs;
         con.Open();
 
         DbCommand cmd = con.CreateCommand();
         cmd.CommandText = "select \* from COUNTRY";
 
         ...              
     }
     
     
 

see also:

*   [http://en.wikipedia.org/wiki/Data\_Definition\_Language](   http://en.wikipedia.org/wiki/Data_Definition_Language)
*   [http://en.wikipedia.org/wiki/Data\_Manipulation\_Language](http://en.wikipedia.org/wiki/Data_Manipulation_Language)

#### Retrieving data: DbCommand.ExecuteScalar()

The DbCommand.ExecuteScalar() executes the CommandText and returns the first column of the first row in the result set of the returned query.

    cmd.CommandText = "select count(ID) from COUNTRY";
     int count = (int)cmd.ExecuteScalar(); 
     
 

#### Retrieving data: the DbDataReader class

The DbCommand.ExecuteReader() overloaded method executes the CommandText and returns a DbDataReader object. The DbDataReader class represents a forward-only group of rows from a datasource.

    public abstract class DbDataReader : MarshalByRefObject, IDataReader, IDisposable, IDataRecord, IEnumerable
     {
         public abstract int Depth { get; }
         public abstract int FieldCount { get; }
         public abstract bool HasRows { get; }
         public abstract bool IsClosed { get; }
         public abstract int RecordsAffected { get; }
         public virtual int VisibleFieldCount { get; }
 
         public abstract object this\[int ordinal\] { get; }
         public abstract object this\[string name\] { get; }
 
         public abstract void Close();
         public void Dispose();
         public abstract bool GetBoolean(int ordinal);
         public abstract byte GetByte(int ordinal);
         public abstract long GetBytes(int ordinal, long dataOffset, byte\[\] buffer, int bufferOffset, int length);
         public abstract char GetChar(int ordinal);
         public abstract long GetChars(int ordinal, long dataOffset, char\[\] buffer, int bufferOffset, int length);
         public DbDataReader GetData(int ordinal);
         public abstract string GetDataTypeName(int ordinal);
         public abstract DateTime GetDateTime(int ordinal);
         public abstract decimal GetDecimal(int ordinal);
         public abstract double GetDouble(int ordinal);
         public abstract IEnumerator GetEnumerator();
         public abstract Type GetFieldType(int ordinal);
         public abstract float GetFloat(int ordinal);
         public abstract Guid GetGuid(int ordinal);
         public abstract short GetInt16(int ordinal);
         public abstract int GetInt32(int ordinal);
         public abstract long GetInt64(int ordinal);
         public abstract string GetName(int ordinal);
         public abstract int GetOrdinal(string name);
         public virtual Type GetProviderSpecificFieldType(int ordinal);
         public virtual object GetProviderSpecificValue(int ordinal);
         public virtual int GetProviderSpecificValues(object\[\] values);
         public abstract DataTable GetSchemaTable();
         public abstract string GetString(int ordinal);
         public abstract object GetValue(int ordinal);
         public abstract int GetValues(object\[\] values);
         public abstract bool IsDBNull(int ordinal);
         public abstract bool NextResult();
         public abstract bool Read();
     }
 
 
 

Here is some example code which uses a DbDataReader.

    public class Country
     {
         public Country(int ID, string Code, string Name)
         {
             this.ID = ID;
             this.Code = Code;
             this.Name = Name;
         }
 
         public int ID { get; set; }
         public string Code { get; set; }
         public string Name { get; set; }
     }
     
     ...    
     
     cmd.CommandText = "select \* from COUNTRY";
 
     using (DbDataReader reader = cmd.ExecuteReader())
     {
         List<Country> list = new List<Country>();
 
         while (reader.Read())
         {
             list.Add(new Country((int)reader\["ID"\], (string)reader\["Code"\], (string)reader\["Name"\]));
         }
 
         Grid.DataSource = list;
     }      
     
 

#### Retrieving data: the DbDataAdapter class

The DbDataAdapter class is used in order to place a result set into a DataTable or DataSet object. The DbDataAdapter inherits from DbDataAdapter.

    public class DataAdapter : Component, IDataAdapter
     {
         public bool AcceptChangesDuringFill { get; set; }
         public bool AcceptChangesDuringUpdate { get; set; }
         public bool ContinueUpdateOnError { get; set; }
         public LoadOption FillLoadOption { get; set; }
         public MissingMappingAction MissingMappingAction { get; set; }
         public MissingSchemaAction MissingSchemaAction { get; set; }
         public virtual bool ReturnProviderSpecificTypes { get; set; }
         public DataTableMappingCollection TableMappings { get; }
 
         public event FillErrorEventHandler FillError;
 
         public virtual int Fill(DataSet dataSet);
         public virtual DataTable\[\] FillSchema(DataSet dataSet, SchemaType schemaType);
         public virtual IDataParameter\[\] GetFillParameters();
         public void ResetFillLoadOption();
         public virtual bool ShouldSerializeAcceptChangesDuringFill();
         public virtual bool ShouldSerializeFillLoadOption();
         public virtual int Update(DataSet dataSet);
     }
     
     public abstract class DbDataAdapter : DataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
     {
         public const string DefaultSourceTableName = "Table";
 
         public DbCommand DeleteCommand { get; set; }
         public DbCommand InsertCommand { get; set; }
         public DbCommand SelectCommand { get; set; }
         public virtual int UpdateBatchSize { get; set; }
         public DbCommand UpdateCommand { get; set; }
 
         public override int Fill(DataSet dataSet);
         public int Fill(DataTable dataTable);
         public int Fill(DataSet dataSet, string srcTable);
         public int Fill(int startRecord, int maxRecords, params DataTable\[\] dataTables);
         public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable);
         public override DataTable\[\] FillSchema(DataSet dataSet, SchemaType schemaType);
         public DataTable FillSchema(DataTable dataTable, SchemaType schemaType);
         public DataTable\[\] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable);
         public override IDataParameter\[\] GetFillParameters();
         public int Update(DataRow\[\] dataRows);
         public override int Update(DataSet dataSet);
         public int Update(DataTable dataTable);
         public int Update(DataSet dataSet, string srcTable);
     }
 
 

The DbDataAdapter.Fill() overloaded method executes the DbCommand passed to its SelectCommand property and fills either a DataTable or a DataSet by executing the query against the database.

    cmd.CommandText = "select \* from COUNTRY";
 
     using (DbDataAdapter adapter = factory.CreateDataAdapter())
     {
         adapter.SelectCommand = cmd;
 
         DataTable table = new DataTable();
         adapter.Fill(table);
 
         Grid.DataSource = table;  
     }
     
 

A DataSet object is eventually a collection of DataTable objects. An oveloaded version of the DbDataAdapter.Fill() can be used in order to select multiple tables and place them into a DataSet object. The DataSet class provides the Tables property of type DataTableCollection. When the Fill() is used with a DataSet it creates a new DataTable object for each selected table. That new table is added to the Tables of the DataSet.

    SelectTables(new string\[\] { "COUNTRY", "CITY", "TRADER", "MATERIAL", "TRADE", "TRADE\_LINES" });
     
     ...    
 
     void SelectTables(string\[\] TableNames)
     {
         using (DbConnection con = factory.CreateConnection())
         {
             con.ConnectionString = cs;
             con.Open();
 
             DbCommand cmd = con.CreateCommand();                
 
             using (DbDataAdapter adapter = factory.CreateDataAdapter())
             {
                 adapter.SelectCommand = cmd;
 
                 /\*  The DbDataAdapter.Fill() uses the DataSet.Tables property.
                     It creates a new DataTable object for each selected table.
                     That new table is added to the Tables of the DataSet.\*/
                 foreach (string s in TableNames)
                 {
                     cmd.CommandText = string.Format("select \* from {0}", s);
                     adapter.Fill(ds, s);
                 }
             }
 
             /\* add table names to the combo box \*/
             for (int i = 0; i < ds.Tables.Count; i++)
                 cboTableNames.Items.Add(ds.Tables\[i\].TableName);
 
             cboTableNames.SelectedIndex = 0;
 
             Grid.DataSource = ds.Tables\[0\];
         }
     } 
     
     
 

#### Table and column mapping: DataTableMapping and DataColumnMapping class

The DataAdapter has the property TableMappings of type DataTableMappingCollection.

    public sealed class DataTableMappingCollection : MarshalByRefObject, ITableMappingCollection, IList, ICollection, IEnumerable
     {
         public DataTableMappingCollection();
 
         public int Count { get; }
 
         public DataTableMapping this\[int index\] { get; set; }
         public DataTableMapping this\[string sourceTable\] { get; set; }
 
         public int Add(object value);
         public DataTableMapping Add(string sourceTable, string dataSetTable);
         public void AddRange(Array values);
         public void AddRange(DataTableMapping\[\] values);
         public void Clear();
         public bool Contains(object value);
         public bool Contains(string value);
         public void CopyTo(Array array, int index);
         public void CopyTo(DataTableMapping\[\] array, int index);
         public DataTableMapping GetByDataSetTable(string dataSetTable);
         public IEnumerator GetEnumerator();
         public static DataTableMapping GetTableMappingBySchemaAction(DataTableMappingCollection tableMappings, string sourceTable, string dataSetTable, MissingMappingAction mappingAction);
         public int IndexOf(object value);
         public int IndexOf(string sourceTable);
         public int IndexOfDataSetTable(string dataSetTable);
         public void Insert(int index, DataTableMapping value);
         public void Insert(int index, object value);
         public void Remove(DataTableMapping value);
         public void Remove(object value);
         public void RemoveAt(int index);
         public void RemoveAt(string sourceTable);
     }
     
 

The DataTableMappingCollection is a collection of DataTableMapping objects.

    public sealed class DataTableMapping : MarshalByRefObject, ITableMapping, ICloneable
     {
         public DataTableMapping();
         public DataTableMapping(string sourceTable, string dataSetTable);
         public DataTableMapping(string sourceTable, string dataSetTable, DataColumnMapping\[\] columnMappings);
 
         public DataColumnMappingCollection ColumnMappings { get; }
         public string DataSetTable { get; set; }
         public string SourceTable { get; set; }
 
         public DataColumnMapping GetColumnMappingBySchemaAction(string sourceColumn, MissingMappingAction mappingAction);
         public DataColumn GetDataColumn(string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction);
         public DataTable GetDataTableBySchemaAction(DataSet dataSet, MissingSchemaAction schemaAction);
         public override string ToString();
     }    
     
 

A DataTableMapping object represents the mapping between a DataTable object and a table in the underlying datasource. The DataSetTable string property is the value of the TableName property of a DataTable where the SourceTable string property is the name of the table in the underlying datasource. The DataTableMapping.ColumnMappings property is of type DataColumnMappingCollection.

    public sealed class DataColumnMappingCollection : MarshalByRefObject, IColumnMappingCollection, IList, ICollection, IEnumerable
     {
         public DataColumnMappingCollection();
 
         public int Count { get; }
 
         public DataColumnMapping this\[int index\] { get; set; }
         public DataColumnMapping this\[string sourceColumn\] { get; set; }
 
         public int Add(object value);
         public DataColumnMapping Add(string sourceColumn, string dataSetColumn);
         public void AddRange(Array values);
         public void AddRange(DataColumnMapping\[\] values);
         public void Clear();
         public bool Contains(object value);
         public bool Contains(string value);
         public void CopyTo(Array array, int index);
         public void CopyTo(DataColumnMapping\[\] array, int index);
         public DataColumnMapping GetByDataSetColumn(string value);
         public static DataColumnMapping GetColumnMappingBySchemaAction(DataColumnMappingCollection columnMappings, string sourceColumn, MissingMappingAction mappingAction);
         public static DataColumn GetDataColumn(DataColumnMappingCollection columnMappings, string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction);
         public IEnumerator GetEnumerator();
         public int IndexOf(object value);
         public int IndexOf(string sourceColumn);
         public int IndexOfDataSetColumn(string dataSetColumn);
         public void Insert(int index, DataColumnMapping value);
         public void Insert(int index, object value);
         public void Remove(DataColumnMapping value);
         public void Remove(object value);
         public void RemoveAt(int index);
         public void RemoveAt(string sourceColumn);
     }
 
 

A DataColumnMappingCollection object is a collection of DataColumnMapping objects.

    public sealed class DataColumnMapping : MarshalByRefObject, IColumnMapping, ICloneable
     {
         public DataColumnMapping();
         public DataColumnMapping(string sourceColumn, string dataSetColumn);
 
         public string DataSetColumn { get; set; }
         public string SourceColumn { get; set; }
 
         public DataColumn GetDataColumnBySchemaAction(DataTable dataTable, Type dataType, MissingSchemaAction schemaAction);
         public static DataColumn GetDataColumnBySchemaAction(string sourceColumn, string dataSetColumn, DataTable dataTable, Type dataType, MissingSchemaAction schemaAction);
         public override string ToString();
     }
     
 

A DataColumnMapping object defines a mapping between a DataColumn object in a DataTable and a column in a table in the underlying datasource. The DataSetColumn string property is the value of the ColumnName property of a DataColumn where the SourceColumn string property is the name of the column in a table in the underlying datasource.

In short, the classes DataTableMappingCollection, DataTableMapping, DataColumnMappingCollection and DataColumnMapping is a mechanism for mapping DataTable and DataColumn objects to tables and columns in an underlying datasource, by name.

    using (DbDataAdapter adapter = factory.CreateDataAdapter())
     {
         DataTableMapping tableMapping = new DataTableMapping();
         adapter.TableMappings.Add(tableMapping);
 
         tableMapping.DataSetTable = "Customers";
         tableMapping.SourceTable = "CUST";
 
         DataColumnMapping columnMapping = new DataColumnMapping();
         tableMapping.ColumnMappings.Add(columnMapping);
         columnMapping.DataSetColumn = "ID";
         columnMapping.SourceColumn = "CUST\_ID";
         
         ...
     }
     
 

The DbDataAdapter.Fill() and Update() methods use internally mapping information if present.

#### The DbDataAdapter.FillSchema() method

The DbDataAdapter.FillSchema() method retrieves schema information based on the SelectCommand of the adapter.

    public override DataTable\[\] FillSchema(DataSet dataSet, SchemaType schemaType);
     public DataTable FillSchema(DataTable dataTable, SchemaType schemaType);
     public DataTable\[\] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable);
     
 

The SchemaType enum type is defined as

    public enum SchemaType
     {
         Source = 1,  // Ignore any table mappings on the DataAdapter
         Mapped = 2,  // Apply any existing table mappings to the incoming schema
     }    
 
 

Also the FillSchema() configures the following DataColumn properties

*   AllowDBNull
*   AutoIncrement (AutoIncrementStep and AutoIncrementSeed must by set manually)
*   MaxLength
*   ReadOnly
*   Unique

and the following DataTable properties

*   PrimaryKey
*   Constraints

FillSchema() preserves any schema already defined in the DataTable objects.

    void SelectFillSchema(string SQL, DataTable table)
     {
         using (DbConnection con = factory.CreateConnection())
         {
             con.ConnectionString = cs;
             con.Open();
 
             DbCommand cmd = con.CreateCommand();
             cmd.CommandText = SQL;
 
             using (DbDataAdapter adapter = factory.CreateDataAdapter())
             {
                 adapter.SelectCommand = cmd;
                 adapter.FillSchema(table, SchemaType.Source);
                 adapter.Fill(table);
                 Grid.DataSource = table;
             }
         }
     }
     
     ...
 
     string SQL = @"select \* from CITY";
 
     DataTable table = new DataTable("City");
     table.Columns.Add("Flag", typeof(System.Boolean));
 
     SelectFillSchema(SQL, table);
  
  
 

#### Executing DbCommand(s) which modify data

The DbCommand.ExecuteNonQuery() executes INSERT, UPDATE and DELETE statements. Actually the ExecuteNonQuery() executes any SQL statement which is not a SELECT statement. It could be used the execute DDL statements such as the CREATE TABLE and CREATE INDEX statements.

    void Exec2(string SQL)
     {
         using (DbConnection con = factory.CreateConnection())
         {
             con.ConnectionString = cs;
             con.Open();
 
             DbCommand cmd = con.CreateCommand();
             cmd.CommandText = SQL;
             cmd.ExecuteNonQuery(); 
         }
     } 
     
 

#### Transactions and the DbTransaction class

One or more database commands can be executed inside a scope and treated as a single unit of work which can be either succed or fail as a whole. That scope is a transaction.

The ADO.NET provides the DbTransaction class which represents a transaction.

    public abstract class DbTransaction : MarshalByRefObject, IDbTransaction, IDisposable
     {
         public DbConnection Connection { get; }
         public abstract IsolationLevel IsolationLevel { get; }
 
         public abstract void Commit();
         public void Dispose();
         public abstract void Rollback();
     }
     
 

Here is an example using the DbTransaction class.

    void Exec(string\[\] SQL)
     {
         using (DbConnection con = factory.CreateConnection())
         {
             con.ConnectionString = cs;
             con.Open();
 
             using (DbTransaction trans = con.BeginTransaction())
             {
                 try
                 {
                     DbCommand cmd = con.CreateCommand();
                     cmd.Transaction = trans;
 
                     foreach (string s in SQL)
                     {
                         cmd.CommandText = s;
                         cmd.ExecuteNonQuery();
                     }
 
                     trans.Commit();
                 }
                 catch (Exception)
                 {
                     trans.Rollback();
                     throw;
                 }
             }          
         }
     }    
 
 

Multiple commands can be executed inside a transaction by associating the DbTransaction to one or more DbCommand objects.

A DbTransaction object is created by calling DbConnection.BeginTransaction(). The BeginTransaction() initiates a transaction in the datasource and must be matched by either a DbTransaction.Commit() or a DbTransaction.Rollback() call.

The DbTransaction.Commit() is called to persist any changes to the datasource, made by the DbCommand objects. The DbTransaction.Rollback() is called to undone any changes made. The DbTransaction.Rollback() must be called in case of an exception.

CAUTION: A transaction should be as short as possible. No message boxes inside transactions. Long running transactions may cause problems because a transaction may place locks on data while executing.

The DbConnection provides another BeginTransaction() overload, the

    public DbTransaction BeginTransaction(IsolationLevel isolationLevel);
     
 

which accepts an IsolationLevel argument. The IsolationLevel is declared as

    public enum IsolationLevel
     {
         Unspecified = -1,
         Chaos = 16,
         ReadUncommitted = 256,
         ReadCommitted = 4096,
         RepeatableRead = 65536,
         Serializable = 1048576,
         Snapshot = 16777216,
     }
     
 

The isolation level of a transaction controls how and when changes made by the transaction are visible and accessible to other concurrent transactions.

see also:

*   [http://en.wikipedia.org/wiki/Database\_transaction](   http://en.wikipedia.org/wiki/Database_transaction)
*   [http://en.wikipedia.org/wiki/Isolation\_(computer\_science)](http://en.wikipedia.org/wiki/Isolation_(computer_science))

#### Posting changes back to the datasource with the DbDataAdapter.Update() method

The DbDataAdapter class provides the Update() method for posting changes back to the underlying datasource.

    public int Update(DataRow\[\] dataRows);
     public override int Update(DataSet dataSet);
     public int Update(DataTable dataTable);
     public int Update(DataSet dataSet, string srcTable);
     
 

The DbDataAdapter object must be the same object used to execute the SelectCommand though. The Update() method analyzes any changes made to the data and executes an INSERT, UPDATE or DELETE SQL statement, for any added, modified or deleted DataRow, using the InsertCommand, UpdateCommand and DeleteCommand of the adapter.

The InsertCommand, UpdateCommand and DeleteCommand must be already defined or an exception is thrown.

ADO.NET provides the DbCommandBuilder class which automatically generates the required InsertCommand, UpdateCommand and DeleteCommand commands by the DbDataAdapter, if the SelectCommand is a simple command which returns data from a single table.

    public abstract class DbCommandBuilder : Component
     {
         public virtual CatalogLocation CatalogLocation { get; set; }
         public virtual string CatalogSeparator { get; set; }
         public virtual ConflictOption ConflictOption { get; set; }
         public DbDataAdapter DataAdapter { get; set; }
         public virtual string QuotePrefix { get; set; }
         public virtual string QuoteSuffix { get; set; }
         public virtual string SchemaSeparator { get; set; }
         public bool SetAllValues { get; set; }
 
         public DbCommand GetDeleteCommand();
         public DbCommand GetDeleteCommand(bool useColumnsForParameterNames);
         public DbCommand GetInsertCommand();
         public DbCommand GetInsertCommand(bool useColumnsForParameterNames);
         public DbCommand GetUpdateCommand();
         public DbCommand GetUpdateCommand(bool useColumnsForParameterNames);
         public virtual string QuoteIdentifier(string unquotedIdentifier);
         public virtual void RefreshSchema();
         public virtual string UnquoteIdentifier(string quotedIdentifier);
     }
     
 

DbCommandBuilder has no logic to handle complex situations such as table joins. In that cases the required DbCommand objects must be constructed manually. Furthermore the SelectCommand upon which the construction is based, it must contain a primary key or at least a unique column, which is then used by the generated UpdateCommand and DeleteCommand when locating records in the datasource.

Another issue arises regarding the refreshing of autoincrement primary keys back to the client. The Update() method promises that it can return back any updated information. The DbCommand.UpdatedRowSource property of type UpdateRowSource

    public enum UpdateRowSource
     {
         None = 0,
         OutputParameters = 1,
         FirstReturnedRecord = 2,
         Both = 3,
     }
 
 

provides the setting for contolling what happens in that situation, but what actually happens has to do with the ADO.NET Data Provider. No refreshing takes place with the OleDb and the MS Access, for example (.Net 2.0).

Here is an example which uses the DbDataAdapter.Update() method.

    public partial class MainForm : Form
     {
         public MainForm()
         {
             InitializeComponent();
         }
 
         const string MsAccessFileName = @"..\\..\\..\\Lessons.MDB";
         readonly string cs = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}"" ", Path.GetFullPath(MsAccessFileName));
         DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
 
         
         DataSet ds;
         DataTable table;
         DbDataAdapter adapter;      
 
         private void MainForm\_Load(object sender, EventArgs e)
         {
             ds = new DataSet("temp");
             table = ds.Tables.Add("COUNTRY");            
 
             adapter = factory.CreateDataAdapter();
 
             using (DbConnection con = factory.CreateConnection())
             {
                 con.ConnectionString = cs;
                 con.Open();
 
                 DbCommand cmd = con.CreateCommand();
                 cmd.CommandText = "select \* from COUNTRY";
  
                 adapter.SelectCommand = cmd;
 
                 /\* use FillSchema() to get any constraints from the table \*/
                 adapter.FillSchema(table, SchemaType.Source);
                 adapter.Fill(table);
 
                 Grid.DataSource = table;        
             }
         }
 
         private void MainForm\_FormClosed(object sender, FormClosedEventArgs e)
         {
             adapter.Dispose();
         }
 
         private void button1\_Click(object sender, EventArgs e)
         {
             /\* create a DbCommandBuilder and link it to the adapter \*/
             DbCommandBuilder commandBuilder = factory.CreateCommandBuilder();
             commandBuilder.DataAdapter = adapter;
 
             using (DbConnection con = factory.CreateConnection())
             {
                 con.ConnectionString = cs;
                 con.Open();
 
                 /\* The SelectCommand is already defined in the MainForm\_Load \*/
                 adapter.SelectCommand.Connection = con;
 
                 /\* link the auto-generated commands to the adapter \*/
                 adapter.InsertCommand = commandBuilder.GetInsertCommand();
                 adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                 adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
 
                 /\*  instruct adapter to refresh the autoincrement primary key back to the client 
                     NOTE: this will not work for the OleDb and MS Access. It should work with
                     other datasources such as MS SQL, Oracle etc, though.   \*/
                 adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;  
                 adapter.InsertCommand.CommandText += Environment.NewLine + "SELECT @@IDENTITY as ID";
 
                 adapter.Update(table);
             }        
 
         }
     }
 
 

#### Parameterized commands: DbParameterCollection and DbParameter class

So far the SQL statement and the data values were in a single string. This is not always a desirable practice, although it is easy and convenient one. Also in some cases may cause security problems because of the possibility of sql injection.

ADO.NET is capable of executing parameterized commands. In a parameterized command the SQL statement text contains placeholders, called parameters, which then are replaced by the actual data values.

Unfortunately the syntax used in creating a parameterized SQL statement varies, depending on the ADO.NET Data Provider. Consider the same SQL SELECT statement written for the various providers.

    select \* from CUSTOMER where ID > @ID       // SqlClient
     select \* from CUSTOMER where ID > ?         // OleDb and Odbc
     select \* from CUSTOMER where ID > :ID       // OracleClient   
     
 

Here is a list of the intricasies

*   SqlClient: it uses named parameters in the format @ParameterName
*   OleDb and Odbc: they use un-named positional parameters. The question mark (?) is the placeholder
*   OracleClient: it uses named parameters in the format :ParameterName

The DbCommand provides the Parameters property of type DbParameterCollection.

    public abstract class DbParameterCollection : MarshalByRefObject, IDataParameterCollection, IList, ICollection, IEnumerable
     {
         public abstract int Count { get; }
         public abstract bool IsFixedSize { get; }
         public abstract bool IsReadOnly { get; }
         public abstract bool IsSynchronized { get; }
         public abstract object SyncRoot { get; }
 
         public DbParameter this\[int index\] { get; set; }
         public DbParameter this\[string parameterName\] { get; set; }
 
         public abstract int Add(object value);
         public abstract void AddRange(Array values);
         public abstract void Clear();
         public abstract bool Contains(object value);
         public abstract bool Contains(string value);
         public abstract void CopyTo(Array array, int index);
         public abstract IEnumerator GetEnumerator();
         public abstract int IndexOf(object value);
         public abstract int IndexOf(string parameterName);
         public abstract void Insert(int index, object value);
         public abstract void Remove(object value);
         public abstract void RemoveAt(int index);
         public abstract void RemoveAt(string parameterName);
     }
 
     
 

A DbParameterCollection is a collection of DbParameter objects.

    public abstract class DbParameter : MarshalByRefObject, IDbDataParameter, IDataParameter
     {
         public abstract DbType DbType { get; set; }
         public abstract ParameterDirection Direction { get; set; }
         public abstract bool IsNullable { get; set; }
         public abstract string ParameterName { get; set; }
         public abstract int Size { get; set; }
         public abstract string SourceColumn { get; set; }
         public abstract bool SourceColumnNullMapping { get; set; }
         public abstract DataRowVersion SourceVersion { get; set; }
         public abstract object Value { get; set; }
 
         public abstract void ResetDbType();
     }
     
 

A DbParameter object represents a parameter in an SQL statement. It is created using the DbCommand.CreateParameter() method. The most crucial properties are the ParameterName, the Value and the DbType. The ADO.NET can infer the DbType from the Value of the DbParameter though.

Paremeter placeholders are replaced by actual data values when the DbCommand is executed. Named parameters is the easiest case. Positional parameters get their value based on their ordinal position both in the SQL statement and in the DbParameterCollection they belong to.

    DbParameter Param = Command.CreateParameter();
     Param.ParameterName = "ID";
     Param.Value = 123;
     Command.Parameters.Add(Param);  
     
 
 

Given the next SQL SELECT statement

    string SelectSql =      "select                                                        " + Environment.NewLine +
                             "   TRADE.ID                 as ID                             " + Environment.NewLine +
                             "  ,TRADER.NAME              as CUSTOMER                       " + Environment.NewLine +
                             "  ,TRADE.TRADE\_DATE         as TRADE\_DATE                     " + Environment.NewLine +
                             "  ,TRADE.TOTAL\_VALUE        as TOTAL                          " + Environment.NewLine +
                             "from                                                          " + Environment.NewLine +
                             "  TRADE                                                       " + Environment.NewLine +
                             "    left join TRADER on TRADER.ID = TRADE.TRADER\_ID           " + Environment.NewLine
                             ;
                             
 

here is an example which creates and executes a parameterized query for the OleDb data provider.

    private void button1\_Click(object sender, EventArgs e)
     {           
         using (DbConnection Con = factory.CreateConnection())
         {
             Con.ConnectionString = cs;
             Con.Open();
 
             DbCommand Cmd = Con.CreateCommand();
 
 
             // 1. Where clause preparation.
             // ===============================================================
 
             /\* TradeDate \*/
             string Where = "where TRADE.TRADE\_DATE >= ? " + Environment.NewLine;          
 
             DbParameter Param = factory.CreateParameter();
             Param.ParameterName = "TradeDate";
             Param.Value = edtDate.Value;
             Cmd.Parameters.Add(Param);
 
             /\* Customer \*/
             if (!string.IsNullOrEmpty(edtCustomer.Text))
             {
                 Where += " and TRADER.NAME like ? " + Environment.NewLine;
 
                 string Customer = edtCustomer.Text.Trim();
                 if (!Customer.EndsWith("%"))
                     Customer += "%";
 
                 Param = factory.CreateParameter();
                 Param.ParameterName = "Customer";
                 Param.Value = Customer;
                 Cmd.Parameters.Add(Param);
             }
 
             /\* Total \*/
             if (!string.IsNullOrEmpty(edtTotal.Text))
             {
                 double Total = 0;
                 if (double.TryParse(edtTotal.Text, out Total))
                     Where += " and TRADE.TOTAL\_VALUE >= ? " + Environment.NewLine;
 
                 Param = factory.CreateParameter();
                 Param.ParameterName = "Total";
                 Param.Value = Total;
                 Cmd.Parameters.Add(Param);
             }
 
             Cmd.CommandText = SelectSql + Where;
 
             // 2. Command execution
             // ===============================================================
             DataTable table = new DataTable();
 
             using (DbDataAdapter adapter = provider.CreateAdapter())
             {
                 adapter.SelectCommand = Cmd;
                 adapter.Fill(table);
             }
 
             Grid.DataSource = table;
         }
     }
                             
     
 

see also:

*   [http://en.wikipedia.org/wiki/SQL\_injection](   http://en.wikipedia.org/wiki/SQL_injection)

#### A helper library

The provided LessonsLib.dll assembly contains the DataProviders static class, the DataProvider class and a number of DataProvider descendants such as the OleDbProvider and the MsSqlProvider class. Those classes are part of the C# Tripous Library (CTL) and help in writing provider neutral code especially when it comes to parameterized commands.

The DataProviders static class plays a role similar to the DbProviderFactories class. It is just a registry for DataProvider descendants. Here is its static constructor.

        static DataProviders()
         {
 

#if !NET\_CF

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
         
 

The user works by getting access to a concrete DataProvider using the DataProviders.Find() method

    static public DataProvider Find(string Alias)
     
 

A DataProvider Alias is a string uniquely identifying a DataProvider. The DataProviders class contains public fields for all supported DataProvider classes.

    /\* Provider aliases \*/
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
     
 

Tripous can handle connection strings that may contain such an alias. For example

    Alias=OleDb; Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\MyApp\\Data.MDB;
     
 

The DataProviders.ExtractAlias() method

    static public void ExtractAlias(string Input, ref string ProviderAlias, ref string ConnectionString)
     
 

is used in extracting the Alias and deleting it from the connection string which is then can be used as usual.

The DataProvider class contains methods similar to the ones found in the DbProviderFactory.

    public abstract DbConnection CreateConnection(string ConnectionString);
     public abstract DbDataAdapter CreateAdapter();
     public virtual DbCommandBuilder CreateCommandBuilder();
     public virtual DbConnectionStringBuilder CreateConnectionStringBuilder();
     public virtual DbParameter CreateParameter();
     
 

The DataProviders and DataProvider class provide parameter handling logic. The DataProviders.GlobalPrefix property gets or sets the parameter prefix to be used globally by the client application. Tripous allows SQL scripts to be written in a uniform format. The programmer just selects a suitable GlobalPrefix and then writes all SQL scripts using that prefix.

The DataProvider.ParamPrefix read-only property indicates the prefix used by the native ADO.NET provider. The DataProvider.PrefixMode property of type PrefixMode

    public enum PrefixMode
     {
         Prefixed,     
         Positional,
     } 
     
 

indicates the mode of the parameter name and prefix the native ADO.NET data provider uses. To help in analyzing SQL statements, creating DbParameter objects automatically, and assigning data values to those parameters, the DataProvider provides the following two methods.

    public DbCommand CreateCommand(DbConnection Connection, string SQL, params object\[\] Params);
     public void SetupCommand(DbCommand Command, string SQL, params object\[\] Params);
     
 

The CreateCommand() has the same logic as the SetupCommand() plus that it creates and returns a DbCommand object. The SetupCommand()

    /// 1. Analyzes the passed SQL which may contain parameter names prefixed by the DataProviders.GlobalPrefix, i.e.  
     ///       select \* from CUSTOMER where ID > :ID            
     /// 2. Converts the passed SQL to be what the native ADO.NET Data Provider expects to be and assigns the Command.CommandText     
     ///    For the MS SQL the above statement would be converted to
     ///      select \* from CUSTOMER where ID > @ID
     ///    and for the OleDb would be converted to
     ///      select \* from CUSTOMER where ID > ?
     /// 3. Creates DbParameter objects and adds them to the Command.Parameters collection. 
     ///    The ParameterName of those objects has no prefix, ie. ID.
     ///    If required by the provider though, a later call to PrefixToNative() method would arrange the values
     ///    of those ParameterName properties to be what the native provider expects to be.
     /// 4. If the passed Params is not null and contains parameter values, it assigns the Parameters of the Command
     ///    object by calling the DataProviders.AssignParams() method.    
     
 

The DataProvider.Select() and Exec() methods execute SELECT and INSERT, UPDATE and DELETE statements.

    public DataTable Select(string ConnectionString, string SQL, params object\[\] Params);
     public DataTable Select(string ConnectionString, string SQL);
     public DataTable Select(DbCommand Command);
     public void Exec(string ConnectionString, string SQL, params object\[\] Params);
     public void Exec(string ConnectionString, string SQL);
     
 

Here is an example which utilizes those classes.

    private void button2\_Click(object sender, EventArgs e)
     {
         // 1. Where clause preparation.
         // ===============================================================
 
         /\* prepare a Params Dictionary to be passed to DbCommand.Parameters \*/
         Dictionary<string, object> Params = new Dictionary<string, object>();
 
 
         /\* TradeDate 
            Tripous DataProviders use the GlobalPrefix which defaults to : for all ADO.NET Data Providers. \*/
         string Where = "where TRADE.TRADE\_DATE >= :TradeDate " + Environment.NewLine ;
         Params.Add("TradeDate",  edtDate.Value.ToString("yyyy-MM-dd"));
 
 
         /\* Customer \*/
         if (!string.IsNullOrEmpty(edtCustomer.Text))
         {
             Where += " and TRADER.NAME like :Customer " + Environment.NewLine ;
 
             string Customer = edtCustomer.Text.Trim();
             if (!Customer.EndsWith("%"))
                 Customer += "%";
 
             Params.Add("Customer", Customer);
         }
 
 
         /\* Total \*/
         if (!string.IsNullOrEmpty(edtTotal.Text))
         {
             double Total = 0;
             if (double.TryParse(edtTotal.Text, out Total))
             {
                 Where += " and TRADE.TOTAL\_VALUE >= :Total " + Environment.NewLine;
                 Params.Add("Total", Total);                   
             }
         }
 
 
         /\* displays the SELECT sql statement \*/
         //MessageBox.Show(SelectSql + Where);
 
 
         // 2. Command execution
         // ===============================================================
         DataTable table = new DataTable(); 
 
         using (DbConnection Con = provider.CreateConnection(cs))
         {
             Con.ConnectionString = cs;
             Con.Open();
 
             /\* creates the DbCommand, parses SQL, creates DbParameters, and assigns values \*/
             DbCommand Cmd = provider.CreateCommand(Con, SelectSql + Where, Params);
 
             using (DbDataAdapter adapter = provider.CreateAdapter())
             {
                 adapter.SelectCommand = Cmd;
                 adapter.Fill(table);
             }
 
         }
 
         Grid.DataSource = table;
 
         /\* the whole Command execution could be written in a single line as \*/
         //Grid.DataSource = provider.Select(cs, SelectSql + Where, Params);
     }    
               
 

#### Executing DDL (Data Definition Language) statements

DDL statements include the CREATE, ALTER and DROP sql statements and it is used in creating altering and deleting database objects such as tables, views, stored procedures and indexes.

DDL statements are executed by using the DbCommand.ExecuteNonQuery() method.

    /\*  Executes not selectable SQL statements (CREATE, DROP, ALTER, INSERT, UPDATE, DELETE). 
         The statement is written by the user in the Editor text box. \*/
     private void btnExec\_Click(object sender, EventArgs e)
     {
         try
         {
             using (DbConnection Con = factory.CreateConnection())
             {
                 Con.ConnectionString = cs;
                 Con.Open();
 
                 DbCommand Cmd = Con.CreateCommand();
                 Cmd.Connection = Con;
 
                 Cmd.CommandText = Editor.Text;
                 Cmd.ExecuteNonQuery();
             }
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message);
         }
     }
 
 
 

see also:

*   [http://en.wikipedia.org/wiki/Data\_Definition\_Language](   http://en.wikipedia.org/wiki/Data_Definition_Language)

#### Using stored procedures

Stored procedures are procedures written in SQL. They can take parameters and return results. Stored procedures are stored in the database.

A selectable stored procedure is a stored procedure which returns a result set, that is something that has columns and rows.

A non-selectable stored procedure is a procedure that either returns nothing or it returns a single value. Non selectable stored procedures can also return one or more of the so-called output parameters, which are similar to by reference parameters of a method in a high level language such as C#.

Unfortunately each database vendor uses its own SQL dialect for writing stored procedures. So stored procedures are not portable from database server to database server.

Here is a selectable MS SQL stored procedure

    create procedure SelectTestTable
       @ID integer
     as
       set nocount on
 
       select \* from TEST\_TABLE
       where ID >= @ID
       
 

And here is a non-selectable MS SQL stored procedure with a single output parameter.

    create procedure AddNumbers 
       @NumberA integer,
       @NumberB integer,
       @Result integer output
     as
       set nocount on
       
       set @Result = @NumberA + @NumberB;    
       
 

And here is how to execute the above stored procedures in a tool such as the MS SQL server Query Analyzer or the MS SQL server Management Studio.

    declare @Result integer
     exec AddNumbers 3, 5, @Result output
     select @Result;
 
     exec SelectTestTable 1;
     
 

NOTE: MS Access supports stored procedures. A web search reveals many articles on that matter.

Selectable stored procedures are executed by using the DbDataAdapter.Fill(). Non-selectable stored procedures are executed by using the DbCommand.ExecuteScalar(). Any output parameter is then accessed through the DbCommands.Parameter property.

The DbParameter.Direction property of type ParameterDirection

    public enum ParameterDirection
     {
         Input = 1,
         Output = 2,
         InputOutput = 3,
         ReturnValue = 6,
     }
 
 

is where the direction of the parameter is defined.

Here is an example which demonstrates how to call the above two MS SQL stored procedures.

    /\* Demonstrates how to use selectable and non-selectable stored procedures \*/
     private void btnExecStoreProc\_Click(object sender, EventArgs e)
     {
         try
         {
             using (DbConnection Con = factory.CreateConnection())
             {
                 Con.ConnectionString = cs;
                 Con.Open();
 
                 DbCommand Cmd = Con.CreateCommand();
 
                 /\* by default CommandType is Text, that is any DDL or DML statement \*/
                 Cmd.CommandType = CommandType.StoredProcedure;
                 Cmd.Connection = Con;
 
                 DbParameter Param;
 
 
                 /\* AddNumbers is a NON-selectable stored proc that adds two numbers passed as parameters. 
                    The sum is returned as an output parameter. 
                    DbParameter.Direction property controls the direction (in/out/in-out) of a parameter \*/
                 Cmd.Parameters.Clear();
                 Cmd.CommandText = "AddNumbers";
 
                 Param = factory.CreateParameter();
                 Param.ParameterName = "@NumberA";
                 Param.Direction = ParameterDirection.Input;
                 Param.Value = 2;
                 Cmd.Parameters.Add(Param);
 
                 Param = factory.CreateParameter();
                 Param.ParameterName = "@NumberB";
                 Param.Direction = ParameterDirection.Input;
                 Param.Value = 3;
                 Cmd.Parameters.Add(Param);
 
                 Param = factory.CreateParameter();
                 Param.ParameterName = "@Result";
                 Param.Direction = ParameterDirection.Output;
                 Param.Value = 0;
                 Cmd.Parameters.Add(Param);
 
                 /\* non-selectable stored procs are executed by using the DbCommand.ExecuteScalar() \*/
                 Cmd.ExecuteScalar();
 
                 /\* read and display the output parameter after stored proc execution \*/
                 string ProcResult = Cmd.Parameters\["@Result"\].Value.ToString();
                 MessageBox.Show(ProcResult);
 
 
                 /\* SelectTestTable is a selectable stored proc that accepts a single input parameter \*/
                 Cmd.Parameters.Clear();
                 Cmd.CommandText = "SelectTestTable";                    
 
                 Param = factory.CreateParameter();
                 Param.ParameterName = "@ID";
                 Param.Direction = ParameterDirection.Input;
                 Param.Value = 1;
                 Cmd.Parameters.Add(Param);
 
                 DbDataAdapter adapter = factory.CreateDataAdapter();
                 adapter.SelectCommand = Cmd;
 
                 /\* selectable stored procs are executed by using the DbDataAdapter.Fill() \*/
                 DataTable Table = new DataTable();
                 adapter.Fill(Table);
 
                 Grid.DataSource = Table;
                 Pager.SelectedTab = tabGrid;
 
             }
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message);
         }
 
     }
     
 

see also:

*   [http://www.devcity.net/Articles/18/msaccess\_sp.aspx](   http://www.devcity.net/Articles/18/msaccess_sp.aspx)

#### Reading and writing BLOB data

Columns of type BLOB (Binary Large Object) are used in storing variable length binary data such as images or long texts.

Unfortunately each database vendor uses its own data type to represent blob data. MS SQL and MS Access use the "IMAGE" data type for binary blobs and the "TEXT" data type for text blobs. Oracle has the "BLOB" and the "CLOB" respectively. Firebird/Interbase has the "BLOB SUB\_TYPE 0" and the "BLOB SUB\_TYPE TEXT".

The DbType.Binary is the flag used with blob types and DbParameter objects. It allows a length of up to 8000 bytes, according to documentation. Specific data provider types such as the SqlDbType.Image of the SqlClient allows for greater length. In most cases a DbParameter object infers the right DbType data type, that is DbType.Binary, when it is passed an array of bytes and it also adapts the right DbParameter.Size.

    byte\[\] Bytes = File.ReadAllBytes(FileName);
     
     ...
     
     DbParameter Param = Cmd.CreateParameter();
     Param.ParameterName = "IMG";                    
     Param.Value = Bytes; 
     
 

Here is an example application which handles blob columns and images by using DbParameter objects.

    public partial class MainForm : Form
     {
         public MainForm()
         {
             InitializeComponent();
 
             SelectTable();
         }
 
         const string MsAccessFileName = @"..\\..\\..\\Lessons.MDB";
         readonly string cs = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}"" ", Path.GetFullPath(MsAccessFileName));
         DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
 
 
         /\* arranges the user interface \*/
         void EnableCommands()
         {
             DataTable Table = Grid.DataSource as DataTable;
 
             btnUpdate.Enabled = (Table != null) && (Grid.RowCount > 0);
             btnDelete.Enabled = btnUpdate.Enabled;
         }
 
 
         /\* selects the PICTURES table and binds Grid and picBox to data \*/
         void SelectTable()
         {
             picBox.DataBindings.Clear();
             Grid.DataSource = null;
 
             DataTable Table = new DataTable("PICTURES");
 
             using (DbConnection Con = factory.CreateConnection())
             {
                 Con.ConnectionString = cs;
                 Con.Open();
 
                 DbCommand Cmd = Con.CreateCommand();
                 Cmd.CommandText = "select \* from PICTURES";
 
                 using (DbDataAdapter adapter = factory.CreateDataAdapter())
                 {
                     adapter.SelectCommand = Cmd;
                     adapter.FillSchema(Table, SchemaType.Source);
                     adapter.Fill(Table);
 
                     Grid.DataSource = Table;
 
                     /\* add a proper DataBinding to picBox \*/
                     picBox.DataBindings.Add("Image", Table, "IMG", true, DataSourceUpdateMode.OnPropertyChanged);
                 }
             }
 
             EnableCommands();
         }
 
 
         /\* inserts a picture from disk to the database  \*/
         private void btnInsert\_Click(object sender, EventArgs e)
         {    
             OpenFileDialog Dlg = new OpenFileDialog();
   
             Dlg.Filter = "bitmaps (\*.bmp)|\*.bmp|Jpegs (\*.jpg)|\*.jpg";       
 
             if (Dlg.ShowDialog() == DialogResult.OK)
             {
                 /\* File.ReadAllBytes() reads the file and returns its data as an array of bytes \*/
                 byte\[\] Bytes = File.ReadAllBytes(Dlg.FileName);
 
                 using (DbConnection Con = factory.CreateConnection())
                 {
                     Con.ConnectionString = cs;
                     Con.Open();
 
                     DbCommand Cmd = Con.CreateCommand();
                     Cmd.CommandText = "insert into PICTURES (IMG) values (?)";
 
                     DbParameter Param = Cmd.CreateParameter();
                     Param.ParameterName = "IMG";                    
 
                     /\*  after next assignement the DbParameter infers is DbType as DbType.Binary  
                         which permits a length of up to 8000 bytes. Specific data provider types
                         such as the SqlDbType.Image of the SqlClient allows for greater length. \*/
                     Param.Value = Bytes; 
                     Cmd.Parameters.Add(Param);
 
                     //MessageBox.Show(Param.DbType.ToString());
 
                     Cmd.ExecuteNonQuery();                    
                 }
 
                 SelectTable();
             }           
 
         }
 
 
 
         /\* updates a row with a picture to the database \*/
         private void btnUpdate\_Click(object sender, EventArgs e)
         {
             if (!Grid.HasCurrentDataRow())
                 return;
 
             OpenFileDialog Dlg = new OpenFileDialog();
 
             Dlg.Filter = "bitmaps (\*.bmp)|\*.bmp|Jpegs (\*.jpg)|\*.jpg";
 
             if (Dlg.ShowDialog() == DialogResult.OK)
             {
 
                 /\* File.ReadAllBytes() reads the file and returns its data as an array of bytes \*/
                 byte\[\] Bytes = File.ReadAllBytes(Dlg.FileName);
 
                 using (DbConnection Con = factory.CreateConnection())
                 {
                     Con.ConnectionString = cs;
                     Con.Open();
 
                     DbCommand Cmd = Con.CreateCommand();
                     Cmd.CommandText = "update PICTURES set IMG = ? where ID = ?";
 
                     DbParameter Param = Cmd.CreateParameter();
                     Param.ParameterName = "IMG";
                     Param.Value = Bytes;
                     Cmd.Parameters.Add(Param);
 
                     Param = Cmd.CreateParameter();
                     Param.ParameterName = "ID";
                     Param.Value = Grid.AsInteger("ID"); // using a custom extension method
                     Cmd.Parameters.Add(Param);
 
                     Cmd.ExecuteNonQuery();                    
                 }
 
                 SelectTable();
             }
         }
 
         /\* deletes a row \*/
         private void btnDelete\_Click(object sender, EventArgs e)
         {
             if (!Grid.HasCurrentDataRow())
                 return;
 
             using (DbConnection Con = factory.CreateConnection())
             {
                 Con.ConnectionString = cs;
                 Con.Open();
 
                 DbCommand Cmd = Con.CreateCommand();
                 Cmd.CommandText = string.Format("delete from PICTURES where ID = {0}", Grid.AsInteger("ID")); // using a custom extension method
 
                 Cmd.ExecuteNonQuery();                
             }
 
             SelectTable();
         }
  
     }    
     
     
     
 

Another way to handle blob data is to directly assign a DataColumn in a DataRow and then call the DbDataAdapter.Update() method.

    byte\[\] Bytes = File.ReadAllBytes(FileName);
     
     ...
 
     DataRow Row = table.NewRow();
     table.Rows.Add(Row);
     Row\["IMG"\] = Bytes;         
     
     ...
     
     adapter.Update(table);
     
 
 

see also:

*   [http://en.wikipedia.org/wiki/BLOB](   http://en.wikipedia.org/wiki/BLOB)
*   [http://www.cvalde.net/misc/blob\_true\_history.htm](http://www.cvalde.net/misc/blob_true_history.htm)

#### Reading database schema information

The DbConnection class provides the GetSchema() method which returns schema information in the form of a DataTable.

    public virtual DataTable GetSchema();
     public virtual DataTable GetSchema(string collectionName);
     public virtual DataTable GetSchema(string collectionName, string\[\] restrictionValues);
     
 

The returned DataTable has different schema depending on the collectionName parameter.

Here is an example project.

    public partial class MainForm : Form
     {
         public MainForm()
         {
             InitializeComponent();
 
             Initialize();
         }
 
         const string MsAccessFileName = @"..\\..\\..\\Lessons.MDB";
         readonly string cs = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}"" ", Path.GetFullPath(MsAccessFileName));
         DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
 
         void Initialize()
         {
             using (DbConnection Con = factory.CreateConnection())
             {
                 Con.ConnectionString = cs;
                 Con.Open();
 
                 DataTable table = Con.GetSchema();
 
                 foreach (DataRow Row in table.Rows)
                     cboCollections.Items.Add(Row\["CollectionName"\].ToString());
             }
         } 
 
         private void cboCollections\_SelectedValueChanged(object sender, EventArgs e)
         {
             if (cboCollections.SelectedItem != null)
             {
                 string Collection = cboCollections.SelectedItem.ToString();
 
                 using (DbConnection Con = factory.CreateConnection())
                 {
                     Con.ConnectionString = cs;
                     Con.Open();
 
                     Grid.DataSource = Con.GetSchema(Collection);
                 }
             }            
         }
 
 
     }
 

#### Creating connections with the Server Explorer window of the MS Visual Studio

The Server Explorer window is accessible through the menu View | Server Explorer. (In some versions this window is named Database Explorer).

Clicking the Connect to Database button or right clicking in the Data Connections node and then choosing Add Connection menu item, displays the Add Connection dialog box. This dialog box is used in creating database connections for any of the standard ADO.NET Data Providers.

Connections are added as nodes to the Data Connections root node. Right clicking on a connection and choosing the Properties menu item presents the properties of the connection where the user can copy the created connection string. Also a connection node contains a list of tables, views and stored procedures of the connection.

#### Typed datasets

In MS Visual Studio a typed dataset is created using the Add | New Item | Data | Dataset wizard. That wizard ends up creating a DataSet Designer. Dragging and dropping tables from a connection of the Server Explorer window on that designer populates the newly created dataset with DataTable classes. That wizard is a code generator too and creates many files some of them containing class definitions related to the dataset.

A typed dataset is a DataSet descendant which provides strongly typed access to database objects such as tables and columns. Consider the following code

    dsLessons.CITY\[0\].ID
 
 

dsLessons is the name of the typed dataset. dsLessons is automatically created as a private field to the current Form class by that DataSet wizard. CITY is a public property of the dsLessons dataset and refers to the CITY table. The indexer notation returns a CITYRow object. The number refers to the row index. CITYRow class is created automatically by the wizard. That wizard creates a DataRow class for each table in the dataset. The ID is a strongly typed property for the underlying CITY.ID column.

In short a typed dataset is a collection of strongly typed data related classes. The major drawback is that a typed dataset needs to be regenerated each time the schema of a table changes.

#### The IISAM of the Jet.OLEDB.4.0 OLEDB provider

IISAM (Installable Indexed Sequential Access Method) is a driver the Jet.OLEDB.4.0 OLEDB provider uses in order to access non Jet data, like Excel, Text and dBase.

In order to connect to non-Jet data using the Jet.OLEDB.4.0 ADO driver the "Extended Properties" part of the connection string should be assigned to a valid verb. For example

    Extended Properties=dBase IV
 
 

To find what IISAM drivers are installed on a machine check the registry key

    HKEY\_LOCAL\_MACHINE\\Software\\Microsoft\\Jet\\4.0\\ISAM Formats 
    
 

Here are two ADO.NET connection strings for Excel, dBase and Text files respectively.

    Provider=Microsoft.Jet.OLEDB.4.0; Data Source="C:\\Data\\ExcelFile.xls"; Extended Properties=Excel 9.0;   
     Provider=Microsoft.Jet.OLEDB.4.0; Data Source="C:\\Data\\"; Extended Properties=dBase IV;   
     Provider=Microsoft.Jet.OLEDB.4.0; Data Source="C:\\Data\\"; Extended Properties=Text;  
  
 

see also:

*   [http://msdn.microsoft.com/en-us/library/ms709353.aspx](   http://msdn.microsoft.com/en-us/library/ms709353.aspx)
*   [http://msdn.microsoft.com/en-us/library/aa140022.aspx](http://msdn.microsoft.com/en-us/library/aa140022.aspx)
*   [http://msdn.microsoft.com/en-us/library/ms810810.aspx](http://msdn.microsoft.com/en-us/library/ms810810.aspx)

### Windows Forms data binding

Data binding is about linking user interface elements to datasources. Data binding is a mechanism which automates both, displaying data to user interface elements and sending changes back to the datasource, with minimum code. User interfacace elements are bound to datasources and any change in the control content or the data content is propagated to both sides automatically.

In Windows Forms almost anything would serve as a datasource as long as it contains data. Here is a list of possible datasources

*   a simple object, custom or not
*   an array or a collection and in general any IList object
*   an ADO.NET data object such as a DataSet, a DataTable or a DataView
*   the special BindingList<T> generic class (introduced in .Net 2.0)
*   a BindingSource object (introduced in .Net 2.0)

In Windows Forms there are two different types of data binding: simple and complex (or list-based) binding.

Controls that display a single piece of information, such as the TextBox and the Label, utilize simple binding. Controls that display lists of information, such as the ListBox, the ComboBox and the DataGridView, utilize complex binding.

Data binding imposes a requirement. Datasources and bound controls should always have the most recent data. A control should send changes to its datasource and a datasource should notify all controls bound to it about any data change. This is called Change Notification and is handled differently in simple and complex data binding.

#### Simple binding

In simple binding a property of a control is bound to a data member of a datasource. That data member can be a property of a single object or the property of an object element in an array, list or collection and of cource it can be a DataColumn of a DataTable. All controls support simple binding.

Simple binding is done through the Control.DataBindings public property all controls inherit from the Control class.

    public ControlBindingsCollection DataBindings { get; }
     
 

DataBindings is of type ControlBindingsCollection.

    public class ControlBindingsCollection : BindingsCollection
     {
         public ControlBindingsCollection(IBindableComponent control);
 
         public IBindableComponent BindableComponent { get; }
         public Control Control { get; }
         public DataSourceUpdateMode DefaultDataSourceUpdateMode { get; set; }
 
         public Binding this\[string propertyName\] { get; }
 
         public void Add(Binding binding);
         public Binding Add(string propertyName, object dataSource, string dataMember);
         public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled);
         public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode);
         public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue);
         public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue, string formatString);
         public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue, string formatString, IFormatProvider formatInfo);
         protected override void AddCore(Binding dataBinding);
         public void Clear();
         protected override void ClearCore();
         public void Remove(Binding binding);
         public void RemoveAt(int index);
         protected override void RemoveCore(Binding dataBinding);
     }
 
 

DataBindings is a collection of Binding objects.

    public class Binding
     {
         public Binding(string propertyName, object dataSource, string dataMember);
         public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled);
         public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode);
         public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue);
         public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString);
         public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString, IFormatProvider formatInfo);
 
         public IBindableComponent BindableComponent { get; }
         public BindingManagerBase BindingManagerBase { get; }
         public BindingMemberInfo BindingMemberInfo { get; }
         public Control Control { get; }
         public ControlUpdateMode ControlUpdateMode { get; set; }
         public object DataSource { get; }
         public object DataSourceNullValue { get; set; }
         public DataSourceUpdateMode DataSourceUpdateMode { get; set; }
         public IFormatProvider FormatInfo { get; set; }
         public string FormatString { get; set; }
         public bool FormattingEnabled { get; set; }
         public bool IsBinding { get; }
         public object NullValue { get; set; }
         public string PropertyName { get; }
 
         public event BindingCompleteEventHandler BindingComplete;
         public event ConvertEventHandler Format;
         public event ConvertEventHandler Parse;
 
         protected virtual void OnBindingComplete(BindingCompleteEventArgs e);
         protected virtual void OnFormat(ConvertEventArgs cevent);
         protected virtual void OnParse(ConvertEventArgs cevent);
         public void ReadValue();
         public void WriteValue();
     }
 
 

A Binding object represents a link between a property of a control and a data member of a datasource. It is possible to bind more than one property of a control to the same or different datasources. This is why the DataBindings property is a collection. It is not possible to bind the same property of a control twice.

The most common scenario is the binding of the Text property of a TextBox to a DataColumn of a DataTable.

    textCustomer.DataBindings.Add("Text", tableCustomer, "CUSTOMER\_NAME");
     
 

In the above example, textCustomer is a TextBox control, the string "Text" refers to the Text property of the textCustomer, tableCustomer is a DataTable that acts as a datasource and the string "CUSTOMER\_NAME" refers to the CUSTOMER\_NAME DataColumn of the tableCustomer.

#### Change notification requirements for simple data binding

Almost any object can be used as a datasource in simple binding scenarios.

Objects that act as simple binding datasources have to implement one of two patterns, if they are to support property change notification.

The old pattern dictates that for each datasource property that is going to be a Binding target, an event must be provided. That event should be of type EventHandler

    public delegate void EventHandler(object sender, EventArgs e);
 
 

and must be named as <PropertyName>Changed.

The binding mechanism whenever binds to a property, it looks for an event whose name is the name of the property plus the word Changed, ie NameChanged. It such an event exists, it registers an event handler to it. Then, whenever that event is triggered, the binding mechanism refreshes any control bound to the associated property.

Here is a class that implements that property change notification pattern.

    public class Person
     {
         private string name = "";
         private int age = 0;
 
         public Person()
             : this("John Doe", 32)
         {
         }
         public Person(string Name, int Age)
         {
             this.Name = Name;
             this.Age = Age;
         }
 
         public string Name
         {
             get { return name; }
             set
             {
                 if (value != name)
                 {
                     name = value;
 
                     if (NameChanged != null)
                         NameChanged(this, EventArgs.Empty);
                 }
             }
         }
         public int Age
         {
             get { return age; }
             set
             {
                 if (value != age)
                 {
                     age = value;
 
                     if (AgeChanged != null)
                         AgeChanged(this, EventArgs.Empty);
                 }
             }
         }
 
         public event EventHandler NameChanged;
         public event EventHandler AgeChanged;
     }
         
         
 

The new pattern, introduced in .Net 2.0, dictates the implementation of the INotifyPropertyChanged interface.

    public interface INotifyPropertyChanged
     {
         event PropertyChangedEventHandler PropertyChanged;
     }
 
     public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);    
         
     public class PropertyChangedEventArgs : EventArgs
     {
         public PropertyChangedEventArgs(string propertyName);
 
         public virtual string PropertyName { get; }
     }
 
 

The binding mechanism checks to see if the datasource implements the INotifyPropertyChanged interface and if it does then it registers an event handler to the PropertyChanged event, in order to be notified for any property change.

Here is the above Person class implementing this new pattern.

    public class Person : INotifyPropertyChanged
     {
         private string name = "";
         private int age = 0;
 
         protected void OnPropertyChanged(string PropertyName)
         {
             if (PropertyChanged != null)
             {
                 PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
             }
         }
 
         public Person()
             : this("John Doe", 32)
         {
         }
         public Person(string Name, int Age)
         {
             this.Name = Name;
             this.Age = Age;
         }
 
         public string Name
         {
             get { return name; }
             set
             {
                 if (value != name)
                 {
                     name = value;
 
                     OnPropertyChanged("Name");
                 }
             }
         }
         public int Age
         {
             get { return age; }
             set
             {
                 if (value != age)
                 {
                     age = value;
 
                     OnPropertyChanged("Age");
                 }
             }
         }
 
 
         public event PropertyChangedEventHandler PropertyChanged;
  
     }        
         
         
         
 

The above class can act as a datasource to controls such as TextBoxes and notify them for any change to its data, even changes generated by code and not user interface actions.

    Person person = new Person();
     
     ...
 
     edtName.DataBindings.Add("Text", persons, "Name");
     edtAge.DataBindings.Add("Text", persons, "Age");
     
 

#### Complex (or list-based) binding

In complex binding a control is bound to a datasource through its DataSource property. Controls that support complex binding provide the DataSource property and display the full list of items provided by the datasource.

There are two categories of complex binding controls: controls that can display multiple data members of the datasource (multiple columns) and controls that display a single data member. In the first category belong the DataGridView and the good old DataGrid. In the second category belong the ListBox, the CheckedListBox and the ComboBox.

The DataSource property those controls provide is of type object. Thus it is assignable to anything can function as a datasource.

    public object DataSource { get; set; }
 
 

The ListBox, CheckedListBox and ComboBox controls inherit the DisplayMember and ValueMember string properties and the SelectedValue object property from their common ancestor, the ListControl class.

    public string DisplayMember { get; set; }
     public string ValueMember { get; set; }
     public object SelectedValue { get; set; }
 
 

The DisplayMember is used to designate the data member of the datasource the control displays. The ValueMember is used to designate the data member of the datasource which provides the value of the SelectedValue property of the control. When the ValueMember is empty the SelectedValue is provided based on the DisplayMember property setting though. Any change to the SelectedValue triggers the SelectedValueChanged event.

When both the ValueMember and the DisplayMember properties are empty, then setting the ValueMember sets the DisplayMember too. On the contrary setting the DisplayMember leaves the ValueMember empty.

If the DisplayMember is left empty the binding mechanism calls the ToString() method on the element object which usually returns the class name.

The term "data member of a datasource" includes the name of a property of an object in the datasource list or the ColumnName of a DataColumn and the like.

The DataGridView and DataGrid controls have the ability to either auto-generate the columns or use a set of columns defined by the user.

#### Change notification requirements for complex data binding

Types that act as complex data binding datasources they have to implement, at least, the IList interface. Arrays and predefined list classes fulfill that requirement.

Arrays and other simple list classes acting as datasources should contain homogenous elements (elements of the same type). The first element in the array or list determines the type the control is bound to.

Change notification is provided when a datasource implements the IBindingList interface.

    public interface IBindingList : IList, ICollection, IEnumerable
     {
         bool AllowEdit { get; }
         bool AllowNew { get; }
         bool AllowRemove { get; }
         bool IsSorted { get; }
         ListSortDirection SortDirection { get; }
         PropertyDescriptor SortProperty { get; }
         bool SupportsChangeNotification { get; }
         bool SupportsSearching { get; }
         bool SupportsSorting { get; }
 
         event ListChangedEventHandler ListChanged;
 
         void AddIndex(PropertyDescriptor property);
         object AddNew();
         void ApplySort(PropertyDescriptor property, ListSortDirection direction);
         int Find(PropertyDescriptor property, object key);
         void RemoveIndex(PropertyDescriptor property);
         void RemoveSort();
     }
 
 

The SupportsChangeNotification property should return true for change notification to be active.

Except IBindingList there are many other interfaces related to complex data binding. In general implementing complex data binding in a datasouce is a complex technical issue. The BCL provides, for that reason, the BindingList<T> generic class which is a generic collection that supports data binding and change notification. BindingList<T> is the best choise as a generic datasource in most situations. Its main purpose is to be used as a base class, but it acts equally well in many situations.

    public class BindingList<T> : Collection<T>, IBindingList, IList, ICollection, IEnumerable, ICancelAddNew, IRaiseItemChangedEvents
     {
         public BindingList();
         public BindingList(IList<T> list);
 
         public bool AllowEdit { get; set; }
         public bool AllowNew { get; set; }
         public bool AllowRemove { get; set; }
         protected virtual bool IsSortedCore { get; }
         public bool RaiseListChangedEvents { get; set; }
         protected virtual ListSortDirection SortDirectionCore { get; }
         protected virtual PropertyDescriptor SortPropertyCore { get; }
         protected virtual bool SupportsChangeNotificationCore { get; }
         protected virtual bool SupportsSearchingCore { get; }
         protected virtual bool SupportsSortingCore { get; }
 
         public event AddingNewEventHandler AddingNew;
         public event ListChangedEventHandler ListChanged;
 
         public T AddNew();
         protected virtual object AddNewCore();
         protected virtual void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction);
         public virtual void CancelNew(int itemIndex);
         protected override void ClearItems();
         public virtual void EndNew(int itemIndex);
         protected virtual int FindCore(PropertyDescriptor prop, object key);
         protected override void InsertItem(int index, T item);
         protected virtual void OnAddingNew(AddingNewEventArgs e);
         protected virtual void OnListChanged(ListChangedEventArgs e);
         protected override void RemoveItem(int index);
         protected virtual void RemoveSortCore();
         public void ResetBindings();
         public void ResetItem(int position);
         protected override void SetItem(int index, T item);
     }
 
 

Here is a code snippet taken form an example application that utilizes the BindingList<T>, among other choises, and user interface elements that support complex binding.

    /\*  Using datasources of different types. 
         It turns out that the BindingList<T> is the most flexible one. \*/
 
     //Person\[\] persons = { new Person(), new Person("Jane Doe", 30)   };
     //List<Person> persons = new List<Person>(){ new Person(), new Person("Jane Doe", 30)  };
     BindingList<Person> persons = new BindingList<Person>() { new Person(), new Person("Jane Doe", 30) };
 
     private void MainForm\_Load(object sender, EventArgs e)
     {      
         edtName.DataBindings.Add("Text", persons, "Name");
         edtAge.DataBindings.Add("Text", persons, "Age");
 
         Grid.DataSource = persons;
         
         /\* if the DisplayMember is left unassigned then the binding mechanism calls 
            the ToString() method on the datasource element. Else it displays the defined member. 
            Setting the ValueMember sets the DisplayMember too. \*/
         listBox.DataSource = persons; 
         //listBox.ValueMember = "Name";
     }
 
 

#### Windows Forms binding mechanism: binding contexts and binding managers

In Windows Forms every Control derived class inherits the Control.BindingContext property.

    public virtual BindingContext BindingContext { get; set; }
     
 

Here is the BindingContext class declaration.

    public class BindingContext : ICollection, IEnumerable
     {
         public BindingContext();
 
         public bool IsReadOnly { get; }
 
         public BindingManagerBase this\[object dataSource\] { get; }
         public BindingManagerBase this\[object dataSource, string dataMember\] { get; }
 
         public event CollectionChangeEventHandler CollectionChanged;
 
         protected internal void Add(object dataSource, BindingManagerBase listManager);
         protected virtual void AddCore(object dataSource, BindingManagerBase listManager);
         protected internal void Clear();
         protected virtual void ClearCore();
         public bool Contains(object dataSource);
         public bool Contains(object dataSource, string dataMember);
         protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent);
         protected internal void Remove(object dataSource);
         protected virtual void RemoveCore(object dataSource);
         public static void UpdateBinding(BindingContext newBindingContext, Binding binding);
     }
 
 
 

A BindingContext is actually a "collection" of BindingManagerBase objects.

    public abstract class BindingManagerBase
     {
         protected EventHandler onCurrentChangedHandler;
         protected EventHandler onPositionChangedHandler;
 
         public BindingManagerBase();
 
         public BindingsCollection Bindings { get; }
         public abstract int Count { get; }
         public abstract object Current { get; }
         public bool IsBindingSuspended { get; }
         public abstract int Position { get; set; }
 
         public event BindingCompleteEventHandler BindingComplete;
         public event EventHandler CurrentChanged;
         public event EventHandler CurrentItemChanged;
         public event BindingManagerDataErrorEventHandler DataError;
         public event EventHandler PositionChanged;
 
         public abstract void AddNew();
         public abstract void CancelCurrentEdit();
         public abstract void EndCurrentEdit();
         public virtual PropertyDescriptorCollection GetItemProperties();
         protected internal virtual PropertyDescriptorCollection GetItemProperties(ArrayList dataSources, ArrayList listAccessors);
         protected virtual PropertyDescriptorCollection GetItemProperties(Type listType, int offset, ArrayList dataSources, ArrayList listAccessors);
         protected internal abstract string GetListName(ArrayList listAccessors);
         protected internal void OnBindingComplete(BindingCompleteEventArgs args);
         protected internal abstract void OnCurrentChanged(EventArgs e);
         protected internal abstract void OnCurrentItemChanged(EventArgs e);
         protected internal void OnDataError(Exception e);
         protected void PullData();
         protected void PushData();
         public abstract void RemoveAt(int index);
         public abstract void ResumeBinding();
         public abstract void SuspendBinding();
         protected abstract void UpdateIsBinding();
     }
 
 

BindingManagerBase is an abstract class. There are two BindingManagerBase descendant classes: the PropertyManager class

    public class PropertyManager : BindingManagerBase
     {
         public PropertyManager();
 
         public override int Count { get; }
         public override object Current { get; }
         public override int Position { get; set; }
 
         public override void AddNew();
         public override void CancelCurrentEdit();
         public override void EndCurrentEdit();
         protected internal override string GetListName(ArrayList listAccessors);
         protected internal override void OnCurrentChanged(EventArgs ea);
         protected internal override void OnCurrentItemChanged(EventArgs ea);
         public override void RemoveAt(int index);
         public override void ResumeBinding();
         public override void SuspendBinding();
         protected override void UpdateIsBinding();
     }
 
 

and the CurrencyManager class.

    public class CurrencyManager : BindingManagerBase
     {
         protected Type finalType;
         protected int listposition;
 
         public override int Count { get; }
         public override object Current { get; }
         public IList List { get; }
         public override int Position { get; set; }
 
         public event ItemChangedEventHandler ItemChanged;
         public event ListChangedEventHandler ListChanged;
         public event EventHandler MetaDataChanged;
 
         public override void AddNew();
         public override void CancelCurrentEdit();
         protected void CheckEmpty();
         public override void EndCurrentEdit();
         public override PropertyDescriptorCollection GetItemProperties();
         protected internal override string GetListName(ArrayList listAccessors);
         protected internal override void OnCurrentChanged(EventArgs e);
         protected internal override void OnCurrentItemChanged(EventArgs e);
         protected virtual void OnItemChanged(ItemChangedEventArgs e);
         protected internal void OnMetaDataChanged(EventArgs e);
         protected virtual void OnPositionChanged(EventArgs e);
         public void Refresh();
         public override void RemoveAt(int index);
         public override void ResumeBinding();
         public override void SuspendBinding();
         protected override void UpdateIsBinding();
     }
 
 

A BindingContext contains zero or more BindingManagerBase objects (PropertyManager or CurrencyManager), known collectively as binding managers. A binding manager object represents a single datasource and keeps all controls that are bound to its datasource synchronized. A BindingContext may contain just a single binding manager per datasource.

A BindingContext is essentially a binding scope. Although each and every control has a BindingContext property, initially there is just a single BindingContext instance. The one that belongs to the parent Form. Child control BindingContexts are just references to that BindingContext of their parent form. If a control is a child on a container control, say a Panel, then it references the BindingContext of its parent and so on.

When a control is associated to a datasource it asks its BindingContext (which whould be a reference to a parent BindingContext and so on) to provide a binding manager for that particular datasource. If there is not a binding manager for that datasource the BindingContext creates one. If the datasource supports simple binding a PropertyManager is created, else if the datasource supports complex binding a CurrencyManager is created.

It is possible to create a separate BindingContext object for any control. Then that control and child controls of that control use that BindingContext, when they need a binding manager. As said before a BindingContext is essentially a binding scope and a separate BindingContext makes more sense with container controls such as the Panel, the GroupBox and the TabControl.

The most basic service a binding manager provides is data synchronization between bound controls and the datasource. Additionally the CurrencyManager supports navigation services in the datasource list. It provides the Count, Current and Position properties. These very same properties are provided by the PropertyManager too but they have no effect at all. Thus currency here means "the state of being current" and has nothing to do with any financial issue.

A CurrencyManager maintains its own individual Position and Current property values even if there are multiple CurrencyManager objects associated to the very same datasource. Currency goes with the CurrencyManager, not with the datasource.

It is possible to get access to a binding manager for a control and use it for navigation.

    BindingManagerBase Manager = listBox1.BindingContext\[table\];
     Manager.Position++;
 
 

It is also possible to create separate BindingContext objects for certain control sets, possible belonging to the same parent container.

    dataPanel.BindingContext = new BindingContext();
     
 

Now, because of the newly created binging scope, all child controls of the dataPanel will create binding managers to that new BindingContext.

Here is an example which demonstrates the use of the BindingContext and the CurrencyManager classes and some of the consequences of creating a new BindingContext for a certain control.

    public partial class MainForm : Form
     {
         public MainForm()
         {
             InitializeComponent();
             Initialize();
         }
 
         DataTable table = new DataTable("temp");
 
         void Initialize()
         {
             /\* create and fill a table \*/
             table.Columns.Add("ID", typeof(System.Int32));
             table.Columns.Add("Name", typeof(System.String));
 
             for (int i = 1; i < 100; i++)
                 table.Rows.Add(new object\[\] { i, "Name\_" + i.ToString() });
 
 
             /\* bind the two ListBox controls to the very same table \*/
             listBox1.DataSource = table;
             listBox1.DisplayMember = "Name";
 
             listBox2.DataSource = table;
             listBox2.DisplayMember = "Name";
 
 
             /\* if the next line is commented, then both the above ListBox controls 
                will use the same CurrencyManager, the one provided by the Form's BindingContext.
                If the next line is left un-commented then the second ListBox will have a separate
                BindingContext and thus a separate CurrencyManager, although it continues to be bound
                to the same datasource \*/
             listBox2.BindingContext = new BindingContext(); 
         }
 
         private void btnPrevious\_Click(object sender, EventArgs e)
         {
             BindingManagerBase Manager = listBox1.BindingContext\[table\];
             Manager.Position--;
         } 
 
         private void btnNext\_Click(object sender, EventArgs e)
         {
             BindingManagerBase Manager = listBox1.BindingContext\[table\];
             Manager.Position++;
         }        
     }
 
 

#### The BindingSource component

The BindingSource component added to the BCL with the .Net 2.0.

    public class BindingSource : Component, IBindingListView, IBindingList, IList, ICollection, IEnumerable, ITypedList, ICancelAddNew, ISupportInitializeNotification, ISupportInitialize, ICurrencyManagerProvider
     {
         public BindingSource();
         public BindingSource(IContainer container);
         public BindingSource(object dataSource, string dataMember);
 
         public virtual bool AllowEdit { get; }
         public virtual bool AllowNew { get; set; }
         public virtual bool AllowRemove { get; }
         public virtual int Count { get; }
         public virtual CurrencyManager CurrencyManager { get; }
         public object Current { get; }
         public string DataMember { get; set; }
         public object DataSource { get; set; }
         public virtual string Filter { get; set; }
         public bool IsBindingSuspended { get; }
         public virtual bool IsFixedSize { get; }
         public virtual bool IsReadOnly { get; }
         public virtual bool IsSorted { get; }
         public virtual bool IsSynchronized { get; }
         public IList List { get; }
         public int Position { get; set; }
         public bool RaiseListChangedEvents { get; set; }
         public string Sort { get; set; }
         public virtual ListSortDescriptionCollection SortDescriptions { get; }
         public virtual ListSortDirection SortDirection { get; }
         public virtual PropertyDescriptor SortProperty { get; }
         public virtual bool SupportsAdvancedSorting { get; }
         public virtual bool SupportsChangeNotification { get; }
         public virtual bool SupportsFiltering { get; }
         public virtual bool SupportsSearching { get; }
         public virtual bool SupportsSorting { get; }
         public virtual object SyncRoot { get; }
 
         public virtual object this\[int index\] { get; set; }
 
         public event AddingNewEventHandler AddingNew;
         public event BindingCompleteEventHandler BindingComplete;
         public event EventHandler CurrentChanged;
         public event EventHandler CurrentItemChanged;
         public event BindingManagerDataErrorEventHandler DataError;
         public event EventHandler DataMemberChanged;
         public event EventHandler DataSourceChanged;
         public event ListChangedEventHandler ListChanged;
         public event EventHandler PositionChanged;
 
         public virtual int Add(object value);
         public virtual object AddNew();
         public virtual void ApplySort(ListSortDescriptionCollection sorts);
         public virtual void ApplySort(PropertyDescriptor property, ListSortDirection sort);
         public void CancelEdit();
         public virtual void Clear();
         public virtual bool Contains(object value);
         public virtual void CopyTo(Array arr, int index);
         protected override void Dispose(bool disposing);
         public void EndEdit();
         public virtual int Find(PropertyDescriptor prop, object key);
         public int Find(string propertyName, object key);
         public virtual IEnumerator GetEnumerator();
         public virtual PropertyDescriptorCollection GetItemProperties(PropertyDescriptor\[\] listAccessors);
         public virtual string GetListName(PropertyDescriptor\[\] listAccessors);
         public virtual CurrencyManager GetRelatedCurrencyManager(string dataMember);
         public virtual int IndexOf(object value);
         public virtual void Insert(int index, object value);
         public void MoveFirst();
         public void MoveLast();
         public void MoveNext();
         public void MovePrevious();
         protected virtual void OnAddingNew(AddingNewEventArgs e);
         protected virtual void OnBindingComplete(BindingCompleteEventArgs e);
         protected virtual void OnCurrentChanged(EventArgs e);
         protected virtual void OnCurrentItemChanged(EventArgs e);
         protected virtual void OnDataError(BindingManagerDataErrorEventArgs e);
         protected virtual void OnDataMemberChanged(EventArgs e);
         protected virtual void OnDataSourceChanged(EventArgs e);
         protected virtual void OnListChanged(ListChangedEventArgs e);
         protected virtual void OnPositionChanged(EventArgs e);
         public virtual void Remove(object value);
         public virtual void RemoveAt(int index);
         public void RemoveCurrent();
         public virtual void RemoveFilter();
         public virtual void RemoveSort();
         public virtual void ResetAllowNew();
         public void ResetBindings(bool metadataChanged);
         public void ResetCurrentItem();
         public void ResetItem(int itemIndex);
         public void ResumeBinding();
         public void SuspendBinding();
     }
 
 

The BindingSource can act as a stand-alone datasource or as an intermediary between bindable controls and the actual datasource.

Furthermore the BindingSource provides navigation and currency facilities with the Count, Current, Position and CurrencyManager properties and the MoveFirst(), MoveLast(), MoveNext() and MovePrevious() methods.

Searching and locating is supported by the Find(), IndexOf() and Contains() methods. Sorting is supported through the Sort() and ApplySort() methods while filtering is supported through the Filter property. A RemoveSort() and RemoveFilter() method remove any sorting or filtering previously applied.

The Add(), AddNew(), Insert(), Remove(), RemoveAt(), RemoveCurrent() and Clear() methods provided for handling the content of the BindingSource.

Also the BindingSource componento provides "transactional" edit facilities with the CancelEdit() and EndEdit() methods.

#### BindingSource: binding a BindingSource to a type

BindingSource can be bound to a type such as a custom defined class type.

    myBindingSource.DataSource = typeof(MyClass);
 
 

Here is an example.

    public class Person
     {
         public Person()
             : this("John Doe", 32)
         {
         }
         public Person(string Name, int Age)
         {
             this.Name = Name;
             this.Age = Age;
         }
 
         public string Name { get; set; }
         public int Age { get; set; }
     }
     
 
     public partial class MainForm : Form
     {
         public MainForm()
         {
             InitializeComponent();
             Initialize();
         }
 
         BindingSource bs = new BindingSource();
 
         void Initialize()
         {
             /\* bind the bs BindingSource to the Person class \*/
             bs.DataSource = typeof(Person);
 
             Grid.DataSource = bs;
 
             edtName.DataBindings.Add("Text", bs, "Name");
             edtAge.DataBindings.Add("Text", bs, "Age");
         }
     }
 
 
 

#### BindingSource: using the AddingNew event and the AddNew() method

The BindingSource provides the AddingNew event

    public event AddingNewEventHandler AddingNew;
     
 

of type AddingNewEventHandler.

    public delegate void AddingNewEventHandler(object sender, AddingNewEventArgs e);
     
 

Here is the declaration of the AddingNewEventArgs.

    public class AddingNewEventArgs : EventArgs
     {
         public AddingNewEventArgs();
         public AddingNewEventArgs(object newObject);
 
         public object NewObject { get; set; }
     }    
 
 

The AddingNew is triggered when the AddNew() method is called and before the new item is added to the list. The AddNew() may be called explicitly by code or as a result of a user interface action.

Here is an example of using the AddingNew event.

    public partial class MainForm : Form
     {
         public MainForm()
         {
             InitializeComponent();
             Initialize();
         }
 
         BindingSource bs = new BindingSource();
 
         void Initialize()
         {
             /\* AddingNew event is triggered any time a new row is going to be added to the BindingSource \*/
             bs.AddingNew += new AddingNewEventHandler(BindingSource\_AddingNew);
 
             /\* AddNew() triggers the AddingNew event \*/
             bs.AddNew();
 
             Grid.DataSource = bs;
 
             edtName.DataBindings.Add("Text", bs, "Name");
             edtAge.DataBindings.Add("Text", bs, "Age");
         }
 
         void BindingSource\_AddingNew(object sender, AddingNewEventArgs e)
         {
             e.NewObject = new Person();
         }
     }
     
 

#### BindingSource: a master-detail example

The BindingSource can be bound to a more traditional datasource such as a DataSet or a DataTable acting as an intermediary between bindable controls and the datasource.

Here is an example which utilizes a master-detail relationship between two tables.

    public partial class MainForm : Form
     {
         public MainForm()
         {
             InitializeComponent();
             Initialize();
         }
 
         const string MsAccessFileName = @"..\\..\\..\\Lessons.MDB";
         readonly string cs = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}"" ", Path.GetFullPath(MsAccessFileName));
         DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
 
         DataSet ds = new DataSet("Lessons");
         BindingSource bsMaster = new BindingSource();
         BindingSource bsDetail = new BindingSource();
 
 
         /\* selects TableNames tables from the database into the ds DataSet object. \*/
         void SelectTables(string\[\] TableNames)
         {            
             using (DbConnection Con = factory.CreateConnection())
             {
                 Con.ConnectionString = cs;
                 Con.Open();
 
                 DbCommand Cmd = Con.CreateCommand();
 
                 using (DbDataAdapter Adapter = factory.CreateDataAdapter())
                 {
                     Adapter.SelectCommand = Cmd;
  
                     foreach (string TableName in TableNames)
                     {
                         Cmd.CommandText = string.Format("select \* from {0}", TableName);
                         Adapter.FillSchema(ds, SchemaType.Source, TableName);
                         Adapter.Fill(ds, TableName);
                     }
                 }                 
             }
         }
 
         void Initialize()
         {
             /\* select the two tables \*/
             SelectTables(new string\[\] {"COUNTRY", "CITY"});
 
             /\* create a relation between those two tables \*/
             ds.Relations.Add("CountryCities", ds.Tables\["COUNTRY"\].Columns\["ID"\], ds.Tables\["CITY"\].Columns\["COUNTRY\_ID"\]);
 
             /\* the top master BindingSource. It gets as DataSource the ds DataSet 
                and as DataMember the name of the COUNTRY table \*/
             bsMaster.DataSource = ds;
             bsMaster.DataMember = "COUNTRY"; 
 
             /\* the detail BindingSource. It gets as DataSource a master BindingSource 
                and as DataMember the name of a proper Relation object \*/
             bsDetail.DataSource = bsMaster;
             bsDetail.DataMember = "CountryCities";
 
             /\* set DataGridView datasources  \*/
             gridMaster.DataSource = bsMaster;
             gridDetail.DataSource = bsDetail;
         }
 
     }
 
 
 

#### The BindingNavigator control

The BindingNavigator control is data navigator control. It provides the property BindingSource which represents the associated BindingSource. The BindingNavigator provides buttons for moving forword and backword, creating new rows, deleting etc.

#### Lookup ComboBox and ListBox

In database programming a lookup operation resembles a dictionary search: it returns a value based on another value. For example the value of the ID column based on the value of the NAME column. Lookup controls are those controls that support lookup operations.

The DisplayMember and ValueMember properties of the ListBox, CheckedListBox and ComboBox controls serve that lookup logic.

    cboCountry.DataSource = ds.Tables\["COUNTRY"\];
     cboCountry.DisplayMember = "NAME";
     cboCountry.ValueMember = "ID";
     
 

In the above example the cboCountry is bound to the COUNTRY table. It will display the COUNTRY.NAME column and the value of its SelectedValue property will reflect the value of the COUNTRY.ID column.

If the value of the SelectedValue property has to be passed to a column in another DataTable, then a proper Binding is required for the control.

    cboCountry.DataBindings.Add("SelectedValue", ds.Tables\["CITY"\], "COUNTRY\_ID");
 
 

Now, everytime the SelectedValue property changes value, it assignes the CITY.COUNTRY\_ID column.

Here is a complete example displaying the lookup usage of the ComboBox and ListBox controls.

    public partial class MainForm : Form
     {
         public MainForm()
         {
             InitializeComponent();
 
             Initialize();
         }
 
         const string MsAccessFileName = @"..\\..\\..\\Lessons.MDB";
         readonly string cs = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}"" ", Path.GetFullPath(MsAccessFileName));
         DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
 
         DataSet ds = new DataSet("Lessons");
 
         /\* selects TableNames tables from the database into the ds DataSet object. \*/
         void SelectTables(string\[\] TableNames)
         {
             using (DbConnection Con = factory.CreateConnection())
             {
                 Con.ConnectionString = cs;
                 Con.Open();
 
                 DbCommand Cmd = Con.CreateCommand();
 
                 using (DbDataAdapter Adapter = factory.CreateDataAdapter())
                 {
                     Adapter.SelectCommand = Cmd;
 
                     foreach (string TableName in TableNames)
                     {
                         Cmd.CommandText = string.Format("select \* from {0}", TableName);
                         Adapter.FillSchema(ds, SchemaType.Source, TableName);
                         Adapter.Fill(ds, TableName);
                     }
                 }
             }
         }
 
         void Initialize()
         {
             /\* select the two tables \*/
             SelectTables(new string\[\] { "COUNTRY", "CITY" });
             Grid.DataSource = ds.Tables\["CITY"\];
 
 
             /\* define the "source" of the data for the control \*/
             cboCountry.DataSource = ds.Tables\["COUNTRY"\];
             cboCountry.DisplayMember = "NAME";
             cboCountry.ValueMember = "ID";
 
             /\* define where to "put" the ValueMember value \*/
             cboCountry.DataBindings.Add("SelectedValue", ds.Tables\["CITY"\], "COUNTRY\_ID");
 
             /\* Here is a trick: BindingContexts create binging managers based on datasources. 
                A binding manager per datasource. So passing here a "different" datasource
                to the ListBox than the one passed to the ComboBox, creates two distinct 
                binding managers, although both controls display the same table  \*/
             /\* define the "source" of the data for the control \*/
             lboCountry.DataSource = ds;
             lboCountry.DisplayMember = "COUNTRY.NAME";
             lboCountry.ValueMember = "COUNTRY.ID";
 
             /\* define where to "put" the ValueMember value \*/
             lboCountry.DataBindings.Add("SelectedValue", ds.Tables\["CITY"\], "COUNTRY\_ID"); 
 
         } 
 
         private void cboCountry\_SelectedValueChanged(object sender, EventArgs e)
         {
             if (cboCountry.SelectedIndex >= 0)
                 edtComboBoxValue.Text = cboCountry.SelectedValue.ToString();
         }
 
         private void lboCountry\_SelectedValueChanged(object sender, EventArgs e)
         {
             if (lboCountry.SelectedIndex >= 0)
                 edtListBoxValue.Text = lboCountry.SelectedValue.ToString();
         }
     }
 
 
 

#### The DataGridView control (.Net 2.0 and later, not available to Compact Framework)

The DataGridView is a huge control. It displays tabular data and can be used either in boud or in unbound (no-datasource) mode. The functionality that control provides is tremendous. It also provides great extensibility.

DataGridView provides the Columns property of type DataGridViewColumnCollection. The base column class is the DataGridViewColumn which serves as the base class for the next column classes

*   DataGridViewButtonColumn
*   DataGridViewCheckBoxColumn
*   DataGridViewComboBoxColumn
*   DataGridViewImageColumn
*   DataGridViewLinkColumn
*   DataGridViewTextBoxColumn

It is possible to create custom column classes to handle specific needs.

Columns are created automatically when the DataGridView is bound to a datasource. Columns may also created manually.

    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
     col.DataPropertyName = "NAME";
     col.HeaderText = "Customer";
     ...
     Grid.Columns.Add(col);
 
 

DataGridView provides the Rows property of type DataGridViewRowCollection. The DataGridViewRow class is the row class type. The DataGridViewRow provides the Cells property of type DataGridViewCellCollection. That is a row provides access to its cells. The DataGridViewCell class is the cell class type which serves as the base class for the next cell classes

*   DataGridViewButtonCell
*   DataGridViewCheckBoxCell
*   DataGridViewHeaderCell
*   DataGridViewComboBoxCell
*   DataGridViewImageCell
*   DataGridViewLinkCell
*   DataGridViewTextBoxCell

A column type added to the DataGridView.Columns property determines the type of the cell class for the Rows property.

Here is how to create and add a lookup column.

    DataGridViewComboBoxColumn colLookUp = new DataGridViewComboBoxColumn();
     colLookUp.HeaderText = "Country";
     colLookUp.DataSource = ds.Tables\["COUNTRY"\];
     colLookUp.DataPropertyName = "COUNTRY\_ID";
     
     colLookUp.DisplayMember = "NAME";
     colLookUp.ValueMember = "ID";
     colLookUp.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
     Grid.Columns.Insert(2, colLookUp);
 
 
 

#### The DataGrid control (available to Compact Framework too)

The DataGrid control is the initial attempt of the .Net to have a grid control.

####