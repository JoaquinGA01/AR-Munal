// ObservableList.cs
using System;
using System.Collections.Generic;

public class ObservableList<T> where T : class
{
    private List<T> _list = new List<T>();
    public event Action<List<T>> OnListChanged;

    public List<T> List
    {
        get => _list;
        set
        {
            _list = value;
            OnListChanged?.Invoke(_list);
        }
    }

    public void Add(T item)
    {
        _list.Add(item);
        OnListChanged?.Invoke(_list);
    }

    public void Remove(T item)
    {
        _list.Remove(item);
        OnListChanged?.Invoke(_list);
    }

    public void Clear()
    {
        _list.Clear();
        OnListChanged?.Invoke(_list);
    }
}
