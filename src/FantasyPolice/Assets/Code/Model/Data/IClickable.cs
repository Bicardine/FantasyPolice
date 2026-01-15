using System;
using UnityEngine.EventSystems;

namespace Model.Data
{
    public interface IClickable
    {
        event Action<IClickable, PointerEventData> OnClicked;
    }
}