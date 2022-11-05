using System;
using System.Collections.Generic;
using UniverseLib.UI.Widgets.ScrollView;

namespace SeededRuns.UI.Base;

public abstract class UiElementListHandler<TData, TCell> : ICellPoolDataSource<TCell> where TCell : ICell
{
    public ScrollPool<TCell> ScrollPool { get; }
    public int ItemCount => CurrentEntries.Count;
    public List<TData> CurrentEntries { get; } = new();

    protected Func<List<TData>> GetEntries;
    protected Action<TCell, int> SetICell;
    
    protected Action<int> OnCellClicked;

    /// <summary>
    /// Create a wrapper to handle your Button ScrollPool.
    /// </summary>
    /// <param name="scrollPool">The ScrollPool&lt;ButtonCell&gt; you have already created.</param>
    /// <param name="getEntriesMethod">A method which should return your current data values.</param>
    /// <param name="setICellMethod">A method which should set the data at the int index to the cell.</param>
    /// <param name="onCellClickedMethod">A method invoked when a cell is clicked, containing the data index assigned to the cell.</param>
    public UiElementListHandler(ScrollPool<TCell> scrollPool, Func<List<TData>> getEntriesMethod,
        Action<TCell, int> setICellMethod, Action<int> onCellClickedMethod)
    {
        ScrollPool = scrollPool;

        GetEntries = getEntriesMethod;
        SetICell = setICellMethod;
        OnCellClicked = onCellClickedMethod;
        CurrentEntries = GetEntries();
    }

    public abstract void OnCellBorrowed(TCell cell);

    public virtual void SetCell(TCell cell, int index)
    {
        if (index < 0 || index >= CurrentEntries.Count)
        {
            cell.Disable();
        }
        else
        {
            cell.Enable();
            SetICell(cell, index);
        }
    }
}