using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationshipMeterController : MonoBehaviour
{
    public int relationshipStrength;
    public Slider slider;

    //public static RelationshipMeterController challenge01strength;

    void Start()
    {
        relationshipStrength = 0;
    }

    public void updateRelationshipStrength(int strengthOffset)
    {
        relationshipStrength += strengthOffset;
        slider.value = relationshipStrength;
    }
}
