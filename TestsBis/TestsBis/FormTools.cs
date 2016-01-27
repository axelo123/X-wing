using MyDbLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public static class FormTools
{
    public interface IInitializeFromRecord
    {
        bool Initialize(MyDB.IRecord Record);
    }

    public static void Fill<T>(this DataGridView DGV, IEnumerable<MyDB.IRecord> Records, params string[] ColumnNames)
        where T : IInitializeFromRecord, new()
    {
        Fill<T>(
            DGV,
            Records.Select<MyDB.IRecord, T>(delegate(MyDB.IRecord Record)
            {
                T NewObject = new T();
                return NewObject.Initialize(Record) ? NewObject : default(T);
            }).Where(Object => !Object.Equals(default(T))),
            ColumnNames);
    }

    public static void Fill<T>(this DataGridView DGV, IEnumerable<T> DataSource, params string[] ColumnNames)
    {
        Fill(DGV, (object)DataSource.ToArray(), ColumnNames);
    }

    public static void Fill(this DataGridView DGV, object DataSource, params string[] ColumnNames)
    {
        DGV.DataSource = DataSource;
        if (ColumnNames != null)
        {
            for (int Index = 0, Count = Math.Min(DGV.Columns.Count, ColumnNames.Length); Index < Count; Index++)
            {
                DGV.Columns[Index].HeaderText = ColumnNames[Index].Replace(' ', '_');
            }
        }
        Form ParentForm = DGV.FindForm();
        if (ParentForm != null)
        {
            if (!ParentForm.Visible)
                ParentForm.Activated += DataGridViewParentForm_Activated;
            else
                DataGridView_ColumnResize(DGV);
        }
    }

    private static void DataGridViewParentForm_Activated(object sender, EventArgs e)
    {
        Form ParentForm = sender as Form;
        ParentForm.Activated -= DataGridViewParentForm_Activated;
        DataGridViewParentForm_Activated(ParentForm.Controls.OfType<Control>());
    }

    private static void DataGridViewParentForm_Activated(IEnumerable<Control> Controls)
    {
        foreach (Control ChildControl in Controls)
        {
            if (ChildControl is DataGridView)
            {
                DataGridView_ColumnResize(ChildControl as DataGridView);
            }
            else
            {
                DataGridViewParentForm_Activated(ChildControl.Controls.OfType<Control>());
            }
        }
    }

    private static void DataGridView_ColumnResize(DataGridView DGV)
    {
        foreach (DataGridViewColumn Colonne in DGV.Columns)
        {
            Colonne.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            int Width = Colonne.Width;
            Colonne.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Colonne.HeaderText = Colonne.HeaderText.Replace('_', ' ');
            Colonne.Width = Width;
        }
    }
}
