using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using UnityEngine;
using ZLogger;
#if UNITY_STANDALONE_WIN
using System.IO;
using Valve.VR;
#endif

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
#if UNITY_STANDALONE_WIN
                _OverlaySystem.SetActive(true);
                return;
#endif
            }else if (args[1].Equals("/iniAutoRun"))
            {
#if UNITY_STANDALONE_WIN
                var openVRError = EVRInitError.None;
                CVRSystem openvr = null;
                //OpenVRの初期化
                openvr = OpenVR.Init(ref openVRError, EVRApplicationType.VRApplication_Utility);
                if (openvr == null)
                {
                    return;
                }
                OpenVR.Applications.RemoveApplicationManifest($"{Directory.GetCurrentDirectory()}\\manifest.vrmanifest");
                var vrAppErr = OpenVR.Applications.AddApplicationManifest($"{Directory.GetCurrentDirectory()}\\manifest.vrmanifest",false);
                if (vrAppErr == EVRApplicationError.None)
                {
                    vrAppErr = OpenVR.Applications.SetApplicationAutoLaunch("meronmks.UFFT", true);
                    if (vrAppErr != EVRApplicationError.None)
                    {
                    }
                    OpenVR.Shutdown();
                    Application.Quit();
                    return;
                }
                else
                {
                }
                OpenVR.Shutdown();
                return;
#endif
            }else if (args[1].Equals("/removeAutoRun"))
            {
#if UNITY_STANDALONE_WIN
                var openVRError = EVRInitError.None;
                CVRSystem openvr = null;
                //OpenVRの初期化
                openvr = OpenVR.Init(ref openVRError, EVRApplicationType.VRApplication_Utility);
                if (openvr == null)
                {
                    return;
                }
                OpenVR.Applications.RemoveApplicationManifest($"{Directory.GetCurrentDirectory()}\\manifest.vrmanifest");
                OpenVR.Shutdown();
                return;
#endif
            }
        }
    }
}
