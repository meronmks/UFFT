using System.Collections;
using System.Collections.Generic;
using AudioStream;
using Microsoft.Extensions.Logging;
using TMPro;
using UnityEngine;

public class SetMicDev2AudioSource : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown = default;
    private AudioSource _aud;
    private readonly int SampleNum = 4096;
    private int _defaultDeviceIndex = 0;
    
    public AudioStreamInput audioStreamInput;
    private List<FMODSystemsManager.INPUT_DEVICE> availableInputs = new List<FMODSystemsManager.INPUT_DEVICE>();
    private int selectedInput;
    private int previousSelectedInput;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (!this.audioStreamInput.ready)
        {
            yield return null;
        }
        
        // iniMicDeviceCap(null);
        iniDeviceList();
    }

    public void DeviceReStart()
    {
        // iniMicDeviceCap(null);
        // iniDeviceList();
    }

    public void ChangeDeviceCaps()
    {
        this.selectedInput = _dropdown.value;
        if (this.selectedInput != this.previousSelectedInput)
        {
            if (Application.isPlaying)
            {
                this.audioStreamInput.Stop();
                this.audioStreamInput.recordDeviceId = this.availableInputs[this.selectedInput].id;
                this.audioStreamInput.Record();
            }

            this.previousSelectedInput = this.selectedInput;
        }
        // iniMicDeviceCap(_dropdown.options[_dropdown.value].text);
    }
    
    public void OnError(string goName, string msg)
    {
        Debug.LogError(msg);
    }

    public void OnError_InputNotification(string goName, string msg)
    {
        Debug.LogError(msg);
    }
    
    public void OnRecordDevicesChanged(string goName)
    {
        // update device list
        if (this.audioStreamInput.ready)
            this.availableInputs = FMODSystemsManager.AvailableInputs(this.audioStreamInput.logLevel, this.audioStreamInput.gameObject.name, this.audioStreamInput.OnError, true);
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
            //minFreqおよびmaxFreqが0（任意に設定可能かを調べる）
            if (minFreq == 0 && maxFreq == 0)
            {
                //適当に設定
                minFreq = 44100;
            }
            int ms = minFreq / SampleNum; // サンプリング時間を適切に取る
            _aud.clip = Microphone.Start(deviceName, true, ms, minFreq);
            while (!(Microphone.GetPosition(deviceName) > 0)) { } // きちんと値をとるために待つ
            Microphone.GetPosition(null);
            _aud.Play(); //マイクをオーディオソースとして実行(Play)開始
        }
    }
    
    //入力デバイスの一覧の初期化
    private void iniDeviceList()
    {
        _dropdown.ClearOptions();
        this.availableInputs = FMODSystemsManager.AvailableInputs(this.audioStreamInput.logLevel, this.audioStreamInput.gameObject.name, this.audioStreamInput.OnError, true);
        int i = 0;
        foreach(var device in availableInputs)
        {
            // fmodのおかげで0番目が既定の入力デバイス？
            // if (Microphone.IsRecording(device))
            // {
            //     _defaultDeviceIndex = i;
            // }
            _dropdown.options.Add(new TMP_Dropdown.OptionData { text = this.availableInputs[i].name });
            i++;
        }
        _dropdown.RefreshShownValue();
        _dropdown.value = _defaultDeviceIndex;
        this.selectedInput = 0;
        this.audioStreamInput.Record();
    }
    

    private void OnApplicationPause(bool pauseStatus)
    {
#if UNITY_IOS || UNITY_ANDROID
        if (pauseStatus)
        {
            if (Microphone.IsRecording(_dropdown.options[_dropdown.value].text))
            {
                Microphone.End(_dropdown.options[_dropdown.value].text);
            }
        }
        else
        {
            iniMicDeviceCap(_dropdown.options[_dropdown.value].text);
        }
#endif
    }

}
