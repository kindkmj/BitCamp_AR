using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class csWebCamController : MonoBehaviour
{
    // Start is called before the first frame update
    //캡쳐 이미지 저장 경로
    //../ 현재 폴더의 상위 폴더
    //./ 현재 폴더
    //capture/ capture 자식폴더
    public static readonly string CAPTURE_PATH = "../../capture/"; //상대경로
    private int Width = Screen.width; //화면의 너비
    private int Height = Screen.height; //화면의 높이
    private const int FPS = 60; //1초에 60장의 영상 프레임
    private int CNT = 0; //이미지 저장시 증가하는 번호

    private WebCamTexture webCamTexture; // Raw Image 에 전달할 Texture
    private Color32[] colors32; //이미지 정보 저장 배열

    private RawImage rawImage; //영상 출력 대상

    //일반 오브젝트는 Transform이 기본이지만 ui오브젝트는 Rect Transform이 기본임.
    private RectTransform rectTransform; //화면 크기 조정을 위한 컴포넌트.

    public bool initialized { get; private set; } //초기화 작업
    public bool isSave { get; set; } = false; //저장할 것인지 아닌지 구분

    public void ToggleSave()
    {
        isSave = !isSave;
    }

    void Start()
    {
        StartCoroutine(Initialized()); //카메라 정보를 Raw Image에 연결해줌 그래야 영상이 출력되므로
    }

    IEnumerator Initialized()
    {
        yield return new WaitForSeconds(1.5f);
        Width = Screen.width;
        Height = Screen.height;

        //Raw Image 크기를 출력화면 크기로 조정해줌
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Width, Height);
        //컴퓨터에 연결된 카메라들의 정보가 배열에 저장된다.
        WebCamDevice[] devices = WebCamTexture.devices;
        //첫번째 카메라만 저장함.
        webCamTexture = new WebCamTexture(devices[0].name, Width, Height, FPS);

        rawImage = GetComponent<RawImage>();
        rawImage.texture = webCamTexture;
        webCamTexture.Play();

        StartCoroutine(WaitWebCamInitialized());
    }

    IEnumerator WaitWebCamInitialized()
    {
        yield return new WaitForSeconds(1f);
        initialized = true;
    }

    void ChangeWidthHeight()
    {
        if (Width != Screen.width || Height != Screen.height)
        {
            Height = Screen.height;
            Width = Screen.width;
            rectTransform.sizeDelta = new Vector2(Width, Height);
        }
    }
    
    void Update()
    {
        if(initialized&&webCamTexture.isPlaying)
        ChangeWidthHeight();

        if (isSave)
        {
            SavePNG(CNT++.ToString());
        }
    }

    public Texture GetTexture()
    {
        return webCamTexture;
    }

    public Texture2D GetWebcamTexture2D()
    {
        colors32 = webCamTexture.GetPixels32();
        Texture2D texture = new Texture2D(webCamTexture.width,webCamTexture.height);
        texture.SetPixels32(colors32);
        texture.Apply();
        return texture;
    }

    public void SavePNG(string name)
    {
        colors32 = webCamTexture.GetPixels32();
        Texture2D normalTexture = GetWebcamTexture2D();
        byte[] bytes = normalTexture.EncodeToPNG();
        string savePath = GetCaptureTexturePath(name);

        using (var fw = new FileStream(savePath, FileMode.Create, FileAccess.Write))
        {
            fw.Write(bytes,0,bytes.Length);
        }
    }

    private string GetCaptureTexturePath(string name)
    {
        string dirPath = Application.dataPath + CAPTURE_PATH;
        DirectoryInfo di = new DirectoryInfo(dirPath);
        //폴더가 없으면 폴더 생성
        if (di.Exists == false)
        {
            di.Create();
        }
        return dirPath + name + ".png";
    }
}

