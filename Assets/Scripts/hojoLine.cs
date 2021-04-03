using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hojoLine : MonoBehaviour
{
    [SerializeField] private LineRenderer[] _lineRenderers = default;

    public void ToggleHojoLine(bool flg)
    {
        foreach (var l in _lineRenderers)
        {
            l.enabled = flg;
        }
    }
}
