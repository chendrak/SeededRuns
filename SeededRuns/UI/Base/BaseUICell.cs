using UnityEngine;
using UniverseLib.UI.Widgets.ScrollView;

namespace SeededRuns.UI.Base;

public abstract class BaseUICell<T> : ICell
{
    public T Data { get; protected set; }

    // ICell
    public GameObject UIRoot { get; set; }
    public RectTransform Rect { get; set; }
    public abstract float DefaultHeight { get; }

    public bool Enabled => UIRoot.activeSelf;
    public void Enable() => UIRoot.SetActive(true);
    public void Disable() => UIRoot.SetActive(false);
    
    public abstract GameObject CreateContent(GameObject parent);

    public virtual void Bind(T obj)
    {
        Data = obj;
    }
}