using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayInitialize : MonoBehaviour
{
    [SerializeField] private GameObject _OverlaySystem = default;
    // Start is called before the first frame update
    void Start()
    {
        var args = System.Environment.GetCommandLineArgs();
        if (args.Length > 1)
        {
            if (args[1].Equals("/vr"))
            {
                _OverlaySystem.SetActive(true);
            }
        }
    }
}
