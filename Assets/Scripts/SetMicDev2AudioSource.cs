using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetMicDev2AudioSource : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown = default;
    private AudioSource _aud;
    private readonly int SampleNum = 4096;
    private int _defaultDeviceIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        iniMicDeviceCap(null);
        iniDeviceList();
    }

    public void ChangeDeviceCaps()
    {
        iniMicDeviceCap(_dropdown.options[_dropdown.value].text);
    }

    //入力デバイスの初期化
    private void iniMicDeviceCap(string deviceName)
    {
        _aud = GetComponent<AudioSource>();
        if ((_aud != null) && (Microphone.devices.Length > 0)) // オーディオソースとマイクがある
        {
            if (Microphone.IsRecording(deviceName))
            {
                Microphone.End(deviceName);
            }
            int minFreq, maxFreq;
            Microphone.GetDeviceCaps(deviceName, out minFreq, out maxFreq); // 最大最小サンプリング数を得る
            int ms = minFreq / SampleNum; // サンプリング時間を適切に取る
            _aud.clip = Microphone.Start(deviceName, true, ms, minFreq);
            while (!(Microphone.GetPosition(deviceName) > 0)) { } // きちんと値をとるために待つ
            Microphone.GetPosition(null);
            _aud.Play(); //マイクをオーディオソースとして実行(Play)開始
            Debug.Log("マイクの初期化完了");
        }
    }
    
    //入力デバイスの一覧の初期化
    public void iniDeviceList()
    {
        _dropdown.ClearOptions();
        int i = 0;
        foreach(var device in Microphone.devices)
        {
            if (Microphone.IsRecording(device))
            {
                _defaultDeviceIndex = i;
            }
            _dropdown.options.Add(new TMP_Dropdown.OptionData { text = device });
            i++;
        }
        _dropdown.RefreshShownValue();
        _dropdown.value = _defaultDeviceIndex;
        Debug.Log("デバイスの一覧更新完了");
    }
    
#if UNITY_IOS || UNITY_ANDROID
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            if (Microphone.IsRecording(deviceName))
            {
                Microphone.End(deviceName);
            }
        }
        else
        {
            iniMicDeviceCap(_dropdown.options[_dropdown.value].text);
        }
    }
#endif
}
