using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SFB;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AudioLoader : MonoBehaviour
{
    [SerializeField] private AudioSource _ExtAudioSource;
    [SerializeField] private AudioSource _MicAudioMaster;
    [SerializeField] private UFFTSpectrum _ufftSpectrum;
    [SerializeField] private TMP_Text _fileNameLabel;
    
    public void FileSelect()
    {
        var extensions = new[]
        {
            new ExtensionFilter( "Sound Files", "mp3", "wav", "ogg" ),
            new ExtensionFilter( "All Files", "*" ),
        };
        var paths = StandaloneFileBrowser.OpenFilePanel( "Open File", "", extensions, false );

        FileLoading(paths);
    }

    public void FileStart()
    {
        _ufftSpectrum._audio = _ExtAudioSource;
        _ExtAudioSource.Play();
    }

    public void FileStop()
    {
        _ExtAudioSource.Stop();
        _ufftSpectrum._audio = _MicAudioMaster;
    }

    private async UniTaskVoid FileLoading(string[] paths)
    {
        if (paths.Length > 0)
        {
            using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip("file:///" + paths[0], AudioType.UNKNOWN))
            {
                await webRequest.Send();

                if (webRequest.isDone)
                {
                    AudioClip loadClip = DownloadHandlerAudioClip.GetContent(webRequest);

                    _ExtAudioSource.clip = loadClip;
                    _ExtAudioSource.Stop();
                    _fileNameLabel.text = paths[0];
                }
            }
        }
    }
}
