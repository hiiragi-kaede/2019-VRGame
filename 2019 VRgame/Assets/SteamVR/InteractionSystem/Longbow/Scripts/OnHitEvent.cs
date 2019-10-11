using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public interface OnHitEvent : IEventSystemHandler
{
    void Onhit(GameObject hitObject, Transform arrow_pos);
}