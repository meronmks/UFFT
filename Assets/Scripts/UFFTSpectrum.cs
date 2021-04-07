using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UFFTSpectrum : MonoBehaviour
{
    [Header("FFTに関するもの")]
    [SerializeField] private AudioSource _audio = default;
    public float g_Gain = 1.0f;
    public int numSamples = 4096;
    public int sampleChannel = 0;
    public int barAmount = 1024;
    public float frequencyLimitLow = 0;
    public float frequencyLimitHigh = 22050;
    [Tooltip("窓関数、まあ適当に")]
    public FFTWindow windowUsed = FFTWindow.Rectangular;
    private float[] spectrum;

    [Header("グラフの描画")]
    [SerializeField] private Transform taget = default;
    [SerializeField] private GameObject barPrefab = default;
    private GameObject[] bars;
    [SerializeField] private float barsXScale = 0.008f;
    [SerializeField] private float barsYScaleMax = 200;
    private float frequencyScaleFactor = default;
    private float value;
    private float trueSampleIndex;
    [SerializeField] private Gradient _gradient = default;

    [Header("軸線")] 
    [SerializeField] private TMP_Text minXText = default;
    [SerializeField] private TMP_Text X2Text = default;
    [SerializeField] private TMP_Text X3Text = default;
    [SerializeField] private TMP_Text X4Text = default;
    [SerializeField] private TMP_Text X5Text = default;
    [SerializeField] private TMP_Text maxXText = default;

    [Header("表示更新に関するもの")] 
    [SerializeField] private Toggle RunToggle = default;
    public bool isRun = true;
    public bool isOneShot = false;

    public void SetFrequencyLimitLow(string num)
    {
        if (num.Length == 0) return;
        frequencyLimitLow = float.Parse(num);
        UpdateFrequencyLine();
    }
    
    public void SetFrequencyLimitHigh(string num)
    {
        if (num.Length == 0) return;
        frequencyLimitHigh = float.Parse(num);
        UpdateFrequencyLine();
    }

    public void Setg_Gain(string num)
    {
        if (num.Length == 0) return;
        g_Gain = float.Parse(num);
    }

    public void SetisRun(bool flg)
    {
        isRun = flg;
    }
    
    public void SetisOneShot()
    {
        isOneShot = true;
        RunToggle.isOn = false;
    }

    public void SetFFTWindow(int select)
    {
        switch (select)
        {
            case 0:
                windowUsed = FFTWindow.Rectangular;
                break;
            case 1:
                windowUsed = FFTWindow.Triangle;
                break;
            case 2:
                windowUsed = FFTWindow.Hamming;
                break;
            case 3:
                windowUsed = FFTWindow.Hanning;
                break;
            case 4:
                windowUsed = FFTWindow.Blackman;
                break;
            case 5:
                windowUsed = FFTWindow.BlackmanHarris;
                break;
            default:
                Debug.LogError("Unknown Window");
                break;
        }
    }

    private void UpdateFrequencyLine()
    {
        var freqNum = (frequencyLimitHigh - frequencyLimitLow) / 5f;
        minXText.text = $"{frequencyLimitLow}";
        X2Text.text = $"{Math.Round(freqNum) + frequencyLimitLow}";
        X3Text.text = $"{Math.Round(freqNum*2) + frequencyLimitLow}";
        X4Text.text = $"{Math.Round(freqNum*3) + frequencyLimitLow}";
        X5Text.text = $"{Math.Round(freqNum*4) + frequencyLimitLow}";
        maxXText.text = $"{frequencyLimitHigh}Hz";
    }
    // Start is called before the first frame update
    void Start()
    {
        spectrum = new float[numSamples];
        bars = new GameObject[barAmount];
        for (var i = 0; i < barAmount; i++)
        {
            var obj = Instantiate(barPrefab, taget, false);
            //マテリアルを複製して色変えした方が圧倒的にfpsが上がったので（GPUで殴ってるだけ？）
            var sh = obj.transform.GetChild(0).GetComponent<MeshRenderer>().material.shader;
            var mat = new Material(sh);
            obj.transform.GetChild(0).GetComponent<MeshRenderer>().material = mat;
            obj.transform.SetParent(taget);
            obj.transform.localScale = new Vector3(barsXScale, 0f, 1f);
            bars[i] = obj;
        }
        frequencyScaleFactor = 1.0f/(AudioSettings.outputSampleRate /2)  * numSamples;
        UpdateFrequencyLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun || isOneShot)
        {
            isOneShot = false;
            _audio.GetSpectrumData(spectrum, sampleChannel, windowUsed);
            for (var i = 0; i < bars.Length; i++)
            {
                var bar = bars[i];
                //バーに対応する周波数の成分の選り分け
                trueSampleIndex = Mathf.Lerp(frequencyLimitLow, frequencyLimitHigh, ((float) i) / barAmount) *
                                  frequencyScaleFactor;
                int sampleIndexFloor = Mathf.FloorToInt(trueSampleIndex);
                sampleIndexFloor = Mathf.Clamp(sampleIndexFloor, 0, spectrum.Length - 2);
                value = Mathf.SmoothStep(spectrum[sampleIndexFloor], spectrum[sampleIndexFloor + 1],
                    trueSampleIndex - sampleIndexFloor);
                //dB表記に変換（たぶん合ってる）
                value = Mathf.Log10(value) * 20f / g_Gain;
                
                var invLerp = Mathf.InverseLerp(-90f, 0f, value);
                var mat = bar.transform.GetChild(0).GetComponent<Renderer>().material;
                mat.color = _gradient.Evaluate(invLerp);
                bar.transform.localScale = new Vector3(barsXScale, invLerp * barsYScaleMax, 1f);
            }
        }
    }
}
