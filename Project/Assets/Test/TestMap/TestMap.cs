using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TestMap : MonoBehaviour
{
    public Texture2D CreatePNG(RenderTexture renderT)
    {
        if (renderT == null)
            return null;

        int width = renderT.width;
        int height = renderT.height;
        Texture2D tex2d = new Texture2D(width, height, TextureFormat.ARGB32, false);
        RenderTexture.active = renderT;
        tex2d.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex2d.Apply();

        byte[] b = tex2d.EncodeToPNG();
        Destroy(tex2d);
        File.WriteAllBytes($"{Path.Combine(Application.dataPath, "../")}/{DateTime.Now.ToShortTimeString().Replace(':', '_')}_Map.png", b);
        return tex2d;
    }
}