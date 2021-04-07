using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CamScreenShot : MonoBehaviour
{
    [SerializeField] private Camera _camera = default;
    public void CaptureScreenShot()
    {
        Capture().Forget();
    }
    
    private async UniTaskVoid Capture()
    {
        if(!Directory.Exists("img"))
        {
            Directory.CreateDirectory("img");
        }
        DateTime now = DateTime.Now;
        string filePath = $@"img/{now.ToString("yyyy-MM-dd_HH-mm-ss")}.png";
        var rt = new RenderTexture(_camera.pixelWidth, _camera.pixelHeight, 24);
        var prev = _camera.targetTexture;
        _camera.targetTexture = rt;
        _camera.Render();
        _camera.targetTexture = prev;
        RenderTexture.active = rt;
        
        var screenShot = new Texture2D(
            _camera.pixelWidth,
            _camera.pixelHeight,
            TextureFormat.RGB24,
            false);
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply();
            
        var bytes = screenShot.EncodeToPNG();
        Destroy(screenShot);
        using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            await fs.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
