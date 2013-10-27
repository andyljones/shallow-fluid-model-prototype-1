﻿using UnityEngine;

public interface ISurfaceGenerator<in TGridElement, out TSurfaceElement>
    where TGridElement: IUsableGridElement
    where TSurfaceElement: IGenerableSurfaceElement, new()
{
    TSurfaceElement[] SurfaceElements(TGridElement[] gridElements);
}