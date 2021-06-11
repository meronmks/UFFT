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
    static readonly ILogger<OverlayInitialize> logger = LogManager.GetLogger<OverlayInitialize>();
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
                logger.ZLogInformation("VROverlayモードで起動します");
                return;
#endif
            }else if (args[1].Equals("/iniAutoRun"))
            {
#if UNITY_STANDALONE_WIN
                logger.ZLogInformation("VRUtilityモードで起動します");
                var openVRError = EVRInitError.None;
                var overlayError = EVROverlayError.None;
                CVRSystem openvr = null;
                //OpenVRの初期化
                openvr = OpenVR.Init(ref openVRError, EVRApplicationType.VRApplication_Utility);
                if (openvr == null)
                {
                    return;
                }
                logger.ZLogDebug("manifestPath:{0}\\manifest.vrmanifest",Directory.GetCurrentDirectory());
                OpenVR.Applications.RemoveApplicationManifest($"{Directory.GetCurrentDirectory()}\\manifest.vrmanifest");
                var vrAppErr = OpenVR.Applications.AddApplicationManifest($"{Directory.GetCurrentDirectory()}\\manifest.vrmanifest",false);
                if (vrAppErr == EVRApplicationError.None)
                {
                    vrAppErr = OpenVR.Applications.SetApplicationAutoLaunch("meronmks.UFFT", true);
                    if (vrAppErr != EVRApplicationError.None)
                    {
                        logger.ZLogError("vrAppErr:{0}",vrAppErr);
                    }
                    OpenVR.Shutdown();
                    Application.Quit();
                    return;
                }
                else
                {
                    logger.ZLogError("vrAppErr:{0}",vrAppErr);
                }
                OpenVR.Shutdown();
                logger.ZLogInformation("自動起動の登録完了");
                return;
#endif
            }else if (args[1].Equals("/removeAutoRun"))
            {
#if UNITY_STANDALONE_WIN
                logger.ZLogInformation("VRUtilityモードで起動します");
                var openVRError = EVRInitError.None;
                var overlayError = EVROverlayError.None;
                CVRSystem openvr = null;
                //OpenVRの初期化
                openvr = OpenVR.Init(ref openVRError, EVRApplicationType.VRApplication_Utility);
                if (openvr == null)
                {
                    return;
                }
                logger.ZLogDebug("manifestPath:{0}\\manifest.vrmanifest",Directory.GetCurrentDirectory());
                OpenVR.Applications.RemoveApplicationManifest($"{Directory.GetCurrentDirectory()}\\manifest.vrmanifest");
                OpenVR.Shutdown();
                logger.ZLogInformation("自動起動の登録解除完了");
                return;
#endif
            }
        }
        logger.ZLogInformation("Desktopモードで起動します");
    }
}
