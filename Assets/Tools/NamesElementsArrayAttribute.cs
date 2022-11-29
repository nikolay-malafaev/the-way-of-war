using System;
using UnityEngine;
public class NamesElementsArrayAttribute : PropertyAttribute {
    
    public Type TargetEnum;
    public NamesElementsArrayAttribute(Type TargetEnum) {
        this.TargetEnum = TargetEnum;
    }
}