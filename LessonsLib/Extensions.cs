 

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tripous.Extensions
{

#if !NET_CF
    /// <summary>
    /// A helper class for a DataGridView bound to a DataTable
    /// </summary>
    static public class DataGridViewExtensions
    {
        static public bool IsDataTableBound(this DataGridView Grid)
        {
            return (Grid.DataSource != null) && (Grid.DataSource is DataTable);
        }

        static public bool HasCurrentDataRow(this DataGridView Grid)
        {
            return Grid.CurrentDataRow() != null;
        }

        static public DataRow CurrentDataRow(this DataGridView Grid)
        {
            if ((Grid.CurrentRow != null) && (Grid.CurrentRow.DataBoundItem is DataRowView))
                return (Grid.CurrentRow.DataBoundItem as DataRowView).Row;

            return null;
        }


        static public object AsObject(this DataGridView Grid, string ColumnName, object Default)
        {
            DataRow Row = CurrentDataRow(Grid);
            if (Row != null)
                return DataRowExtensions.AsObject(Row, ColumnName, Default);

            return Default;
        }

        static public int AsInteger(this DataGridView Grid, string ColumnName, int Default)
        {
            DataRow Row = CurrentDataRow(Grid);
            if (Row != null)
                return DataRowExtensions.AsInteger(Row, ColumnName, Default);

            return Default;
        }

        static public int AsInteger(this DataGridView Grid, string ColumnName)
        {
            return AsInteger(Grid, ColumnName, 0);
        }

        static public string AsString(this DataGridView Grid, string ColumnName, string Default)
        {
            DataRow Row = CurrentDataRow(Grid);
            if (Row != null)
                return DataRowExtensions.AsString(Row, ColumnName, Default);

            return Default;
        }

        static public string AsString(this DataGridView Grid, string ColumnName)
        {
            return AsString(Grid, ColumnName, "");
        }

        static public double AsFloat(this DataGridView Grid, string ColumnName, double Default)
        {
            DataRow Row = CurrentDataRow(Grid);
            if (Row != null)
                return DataRowExtensions.AsFloat(Row, ColumnName, Default);

            return Default;
        }

        static public double AsFloat(this DataGridView Grid, string ColumnName)
        {
            return AsFloat(Grid, ColumnName, 0);
        }

        static public bool AsBoolean(this DataGridView Grid, string ColumnName, bool Default)
        {
            DataRow Row = CurrentDataRow(Grid);
            if (Row != null)
                return DataRowExtensions.AsBoolean(Row, ColumnName, Default);

            return Default;
        }

        static public bool AsBoolean(this DataGridView Grid, string ColumnName)
        {
            return AsBoolean(Grid, ColumnName, false);
        }

        static public DateTime AsDateTime(this DataGridView Grid, string ColumnName, DateTime Default)
        {
            DataRow Row = CurrentDataRow(Grid);
            if (Row != null)
                return DataRowExtensions.AsDateTime(Row, ColumnName, Default);

            return Default;
        }

        static public DateTime AsDateTime(this DataGridView Grid, string ColumnName)
        {
            return AsDateTime(Grid, ColumnName, DateTime.Now);
        }
    }
#endif


    /// <summary>
    /// A helper class for returning typed values from a DataRow in a safe manner
    /// </summary>
    static public class DataRowExtensions
    {

        static public object AsObject(this DataRow row, string ColumnName, object Default)
        {
            if (Default == null)
                throw new ArgumentNullException("Default", "Default parameter can not be null");

            if (row.Table.Columns.Contains(ColumnName))
            {
                DataColumn column = row.Table.Columns[ColumnName];
                if (!row.IsNull(column))
                {
                    if (column.DataType.Equals(Default.GetType()))
                        return row[ColumnName];
                }
            }

            return Default;
        }

        static public int AsInteger(this DataRow row, string ColumnName, int Default)
        {
            if (row.Table.Columns.Contains(ColumnName))
            {
                DataColumn column = row.Table.Columns[ColumnName];
                if (!row.IsNull(column))
                {
                    if (column.DataType.Equals(typeof(int)))
                        return (int)row[ColumnName];
                    else
                        return Convert.ToInt32(row[ColumnName]);
                }
            }

            return Default;
        }

        static public int AsInteger(this DataRow row, string ColumnName)
        {
            return AsInteger(row, ColumnName, 0);
        }

        static public string AsString(this DataRow row, string ColumnName, string Default)
        {
            if (row.Table.Columns.Contains(ColumnName))
            {
                DataColumn column = row.Table.Columns[ColumnName];
                if (!row.IsNull(column))
                {
                    if (column.DataType.Equals(typeof(string)))
                        return (string)row[ColumnName];
                    else
                        return row[ColumnName].ToString();
                }
            }

            return Default;
        }

        static public string AsString(this DataRow row, string ColumnName)
        {
            return AsString(row, ColumnName, "");
        }

        static public double AsFloat(this DataRow row, string ColumnName, double Default)
        {
            if (row.Table.Columns.Contains(ColumnName))
            {
                DataColumn column = row.Table.Columns[ColumnName];
                if (!row.IsNull(column))
                {
                    if (column.DataType.Equals(typeof(double)))
                        return (double)row[ColumnName];
                    else
                        return Convert.ToDouble(row[ColumnName]);
                }
            }

            return Default;
        }

        static public double AsFloat(this DataRow row, string ColumnName)
        {
            return AsFloat(row, ColumnName, 0);
        }

        static public bool AsBoolean(this DataRow row, string ColumnName, bool Default)
        {
            if (row.Table.Columns.Contains(ColumnName))
            {
                DataColumn column = row.Table.Columns[ColumnName];
                if (!row.IsNull(column))
                {
                    if (column.DataType.Equals(typeof(bool)))
                        return (bool)row[ColumnName];
                    else
                        return Convert.ToBoolean(row[ColumnName]);
                }
            }

            return Default;
        }

        static public bool AsBoolean(this DataRow row, string ColumnName)
        {
            return AsBoolean(row, ColumnName, false);
        }

        static public DateTime AsDateTime(this DataRow row, string ColumnName, DateTime Default)
        {
            if (row.Table.Columns.Contains(ColumnName))
            {
                DataColumn column = row.Table.Columns[ColumnName];
                if (!row.IsNull(column))
                {
                    if (column.DataType.Equals(typeof(DateTime)))
                        return (DateTime)row[ColumnName];
                    else
                        return Convert.ToDateTime(row[ColumnName]);
                }
            }

            return Default;
        }

        static public DateTime AsDateTime(this DataRow row, string ColumnName)
        {
            return AsDateTime(row, ColumnName, DateTime.Now);
        }
    }


    /// <summary>
    /// A helper class for returning typed values from an IDictionary in a safe manner.
    /// It assumes that the Key is always a string
    /// </summary>
    static public class DictionaryExtensions
    {
        static public object AsObject(this IDictionary Dictionary, string Key, object Default)
        {
            if (Default == null)
                throw new ArgumentNullException("Default", "Default parameter can not be null");

            if ((Dictionary.Contains(Key)) && (Dictionary[Key] != null))
            {
                if (Dictionary[Key].GetType().Equals(Default.GetType()))
                    return Dictionary[Key];
            }

            return Default;
        }

        static public int AsInteger(this IDictionary Dictionary, string Key, int Default)
        {
            if ((Dictionary.Contains(Key)) && (Dictionary[Key] != null))
            {
                if (Dictionary[Key].GetType().Equals(typeof(int)))
                    return (int)Dictionary[Key];
                else
                    return Convert.ToInt32(Dictionary[Key]);
            }

            return Default;
        }

        static public int AsInteger(this IDictionary Dictionary, string Key)
        {
            return AsInteger(Dictionary, Key, 0);
        }

        static public string AsString(this IDictionary Dictionary, string Key, string Default)
        {

            if ((Dictionary.Contains(Key)) && (Dictionary[Key] != null))
            {
                if (Dictionary[Key].GetType().Equals(typeof(string)))
                    return (string)Dictionary[Key];
                else
                    return Dictionary[Key].ToString();
            }

            return Default;
        }

        static public string AsString(this IDictionary Dictionary, string Key)
        {
            return AsString(Dictionary, Key, "");
        }

        static public double AsFloat(this IDictionary Dictionary, string Key, double Default)
        {
            if ((Dictionary.Contains(Key)) && (Dictionary[Key] != null))
            {
                if (Dictionary[Key].GetType().Equals(typeof(double)))
                    return (double)Dictionary[Key];
                else
                    return Convert.ToInt32(Dictionary[Key]);
            }

            return Default;
        }

        static public double AsFloat(this IDictionary Dictionary, string Key)
        {
            return AsFloat(Dictionary, Key, 0);
        }

        static public bool AsBoolean(this IDictionary Dictionary, string Key, bool Default)
        {

            if ((Dictionary.Contains(Key)) && (Dictionary[Key] != null))
            {
                if (Dictionary[Key].GetType().Equals(typeof(bool)))
                    return (bool)Dictionary[Key];
                else
                    return Convert.ToBoolean(Dictionary[Key]);
            }

            return Default;
        }

        static public bool AsBoolean(this IDictionary Dictionary, string Key)
        {
            return AsBoolean(Dictionary, Key, false);
        }

        static public DateTime AsDateTime(this IDictionary Dictionary, string Key, DateTime Default)
        {
            if ((Dictionary.Contains(Key)) && (Dictionary[Key] != null))
            {
                if (Dictionary[Key].GetType().Equals(typeof(DateTime)))
                    return (DateTime)Dictionary[Key];
                else
                    return Convert.ToDateTime(Dictionary[Key]);
            }

            return Default;
        }

        static public DateTime AsDateTime(this IDictionary Dictionary, string Key)
        {
            return AsDateTime(Dictionary, Key, DateTime.Now);
        }
    }
}
