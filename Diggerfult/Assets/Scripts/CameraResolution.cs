using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    void Awake()
    {
        ScreenSetting();
    }

    private void ScreenSetting()
    {
        // 하단의 코드들은 추후 Setting에서 설정 조정해야할듯
        Screen.SetResolution(1080, 1920, true);

        Camera camera = GetComponent<Camera>();
        Rect r = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / (9f / 16f);
        float scalewidth = 1f / scaleheight;

        if (scaleheight < 1f)
        {
            r.height = scaleheight;
            r.y = (1f - scaleheight) / 2f;
        }
        else
        {
            r.width = scalewidth;
            r.x = (1f - scalewidth) / 2f;
        }

        camera.rect = r;
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
}
