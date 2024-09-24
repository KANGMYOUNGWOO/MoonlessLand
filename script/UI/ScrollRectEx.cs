using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollRectEx : ScrollRect
{

    private bool routeToParent = false;

    private void DoForParents<T>(Action<T> action) where T : IEventSystemHandler
    {
        Transform parent = transform.parent;
        while(parent != null)
        {
            foreach(var component in parent.GetComponents<Component>())
            {
                if (component is T)
                    action((T)(IEventSystemHandler)component);
            }
            parent = parent.parent;
        }
        
    }

    public override void  OnInitializePotentialDrag(PointerEventData eventData)
    {
        DoForParents<IInitializePotentialDragHandler>((parent) => { parent.OnInitializePotentialDrag(eventData); });
        base.OnInitializePotentialDrag(eventData);


    }


    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (routeToParent)
            DoForParents<IDragHandler>((parent) => { parent.OnDrag(eventData); });
        else
        base.OnDrag(eventData);
    }

    public override void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (!horizontal && Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            routeToParent = true;
        else if (!vertical && Mathf.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
            routeToParent = true;
        else routeToParent = false;
        if (routeToParent)
            DoForParents<IBeginDragHandler>((parent) => { parent.OnBeginDrag(eventData); });
        else
            base.OnBeginDrag(eventData);
    }


    public override void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if(routeToParent)
            DoForParents<IEndDragHandler>((parent) => { parent.OnEndDrag(eventData); });
        else
        base.OnEndDrag(eventData);

        routeToParent = false;
    }

}
