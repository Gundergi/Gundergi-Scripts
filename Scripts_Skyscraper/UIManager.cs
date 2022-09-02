using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;// ����Ƽ ���� �ε��ϱ� ���ؼ� ���

public class UIManager : MonoBehaviour
{

    // UI Canvas ����(���) 
    public enum UICanvasMode
    {
        GAME_START, GAME_OVER, GAME_PAUSE, GAME_SETTINGS, GAME_CLEAR, NONE
    }
    public UICanvasMode canvasMode = UICanvasMode.NONE;
    public GameObject[] panels;

    bool isMoveScene = false;       //�� �̵� �����ϵ���

    // *�̱��� ����
    public static UIManager Instance = null;

    private void Awake()
    {
        // * �̱��� ����
        // ���� �ν��Ͻ��� ���� ���� ���� ���� ���¶��
        if (Instance == null)
        {
            // �� �ڽ��� ��� ������ ����ִ´�.
            Instance = this;
        }
    }
    public CanvasPointer uiCanvasPointer;
    bool isUIMode = false;
    public bool UIMode
    {
        get { return isUIMode; }
        set
        {
            isUIMode = value;

            // UI ��带 On ���� ��!
            if (isUIMode)
            {
                // canvasPointer Ȱ��ȭ�ؼ�.. ���η�������, ui ���� �� �� �ֵ���..
                uiCanvasPointer.gameObject.SetActive(true);
                // Game �Ͻ�����
                //GameManager.Instance.GamePause();
            }
            // UI ��带 Off ���� ��!
            else
            {
                // canvasPointer ��Ȱ��ȭ�ؼ�.. ���η�������, ui ���� �ȵǰ�..
                uiCanvasPointer.gameObject.SetActive(false);
                // Game �ٽ� ����
                //GameManager.Instance.GameStart();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.UIMode = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlaying) { UIManager.Instance.UIMode = false; }
        if (GameManager.Instance.isPlaying == false) { UIManager.Instance.UIMode = true; }

        // ESC ���� �� �Ͻ�����
        //if (Input.GetKeyDown(KeyCode.Escape))

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch)) //���� 0731
        {
            for (int i = 0; i < UIManager.Instance.panels.Length; i++)
            {
                // ���� �����ִ� ĵ����â�� �ִٸ�
                if (UIManager.Instance.panels[i].activeSelf == true)
                {
                    return;
                }
            }
            UIManager.Instance.OpenGamePause();

        }
    }

    // ������ Canvas Panel ����(Ȱ��ȭ)
    public void OpenUIPanel(UICanvasMode panel)
    {
        switch (panel)
        {
            case UICanvasMode.GAME_START:
                // GAME_START ĵ������ ������ ���..
                //GameManager.Instance.GameStart();
                break;
            case UICanvasMode.GAME_OVER:
                // GAME_OVER ĵ������ ������ ���..
                GameManager.Instance.GameOver();
                break;
            case UICanvasMode.GAME_PAUSE:
                // GAME_PAUSE ĵ������ ������ ���..
                GameManager.Instance.GamePause();
                break;
            case UICanvasMode.GAME_SETTINGS:
                // GAME_SETTINGS ĵ������ ������ ���..
                GameManager.Instance.GamePause();
                break;
            case UICanvasMode.GAME_CLEAR:
                // GAME_CLEAR ĵ������ ������ ���..
                GameManager.Instance.GamePause();
                break;
        }

        Debug.Log($"[{panel.ToString()}] �г� ����!");
        // ���� ������ �г��� ĵ���� ������Ʈ�� Ȱ��ȭ
        panels[(int)panel].SetActive(true);
    }

    public void CloseUIPanel(UICanvasMode panel)
    {
        switch (panel)
        {
            case UICanvasMode.GAME_START:
                break;
            case UICanvasMode.GAME_OVER:
                break;
            case UICanvasMode.GAME_PAUSE:
                // GAME_PAUSE ĵ������ �ݾ��� ���..
                GameManager.Instance.GameStart();
                break;
            case UICanvasMode.GAME_SETTINGS:
                break;
            case UICanvasMode.GAME_CLEAR:
                break;
        }
        // ���� ������ �г��� ĵ���� ������Ʈ�� ��Ȱ��ȭ
        panels[(int)panel].SetActive(false);

        // ���� ĵ������� ����
        canvasMode = UICanvasMode.NONE;
    }

    // -- ���� ���� â ����/�ݱ� --
    public void OpenGameStart()
    {
        OpenUIPanel(UICanvasMode.GAME_START);

        if (!isMoveScene)
        {
            isMoveScene = true;
            Debug.Log("���� ������");
            //2.ŸŶ �� = "�̵��� �� ���� �̸�(Domino Scene)"
            string sceneName = "Intro Scene";

            //1.���� �̵��ϰ� �ʹ�.
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    public void CloseGameStart()
    {
        CloseUIPanel(UICanvasMode.GAME_START);
    }

    // -- ���� ���� â ����/�ݱ� --
    public void OpenGameOver()
    {
        OpenUIPanel(UICanvasMode.GAME_OVER);
    }

    public void CloseGameOver()
    {
        CloseUIPanel(UICanvasMode.GAME_OVER);
    }

    // -- ���� �Ͻ����� â ����/�ݱ� --
    public void OpenGamePause()
    {
        OpenUIPanel(UICanvasMode.GAME_PAUSE);
    }

    public void CloseGamePause()
    {
        CloseUIPanel(UICanvasMode.GAME_PAUSE);
    }

    // -- ���� ���� â ����/�ݱ� --
    public void OpenGameSettings()
    {
        OpenUIPanel(UICanvasMode.GAME_SETTINGS);
    }

    public void CloseGameSettings()
    {
        CloseUIPanel(UICanvasMode.GAME_SETTINGS);
    }

    // -- ���� �Ϸ� â ����/�ݱ� --
    public void OpenGameClear()
    {
        OpenUIPanel(UICanvasMode.GAME_CLEAR);
        GetComponentInChildren<AudioSource>().Play();
    }

    public void CloseGameClear()
    {
        CloseUIPanel(UICanvasMode.GAME_CLEAR);
    }

    // GameStart ��ư�� ������ ����
    public void OnClickGameStart()
    {
        // ���� ĵ���� â�� �����ִٸ� �ݱ�
        for (int i = 0; i < panels.Length; i++)
        {
            // ���� �����ִ� ĵ����â�� �ִٸ�
            if (panels[i].activeSelf == true)
            {
                // -> panels �迭 �ȿ� �ִ� ������Ʈ�� ��� ��Ȱ��ȭ 
                panels[i].SetActive(false);
            }
        }

        // ���� ����.
        GameManager.Instance.GameStart();
    }

    // Restart ��ư�� ������ ����
    public void OnClickRestart()
    {
        // ���� ĵ���� â�� �����ִٸ� �ݱ�
        for (int i = 0; i < panels.Length; i++)
        {
            // ���� �����ִ� ĵ����â�� �ִٸ�
            if (panels[i].activeSelf == true)
            {
                // -> panels �迭 �ȿ� �ִ� ������Ʈ�� ��� ��Ȱ��ȭ 
                panels[i].SetActive(false);
            }
        }

        // ���� �����.
        GameManager.Instance.GameRestart();
    }

    // Quit ��ư�� ������ ����
    public void OnClickExit()
    {
        // ���� ĵ���� â�� �����ִٸ� �ݱ�
        for (int i = 0; i < panels.Length; i++)
        {
            // ���� �����ִ� ĵ����â�� �ִٸ�
            if (panels[i].activeSelf == true)
            {
                // -> panels �迭 �ȿ� �ִ� ������Ʈ�� ��� ��Ȱ��ȭ 
                panels[i].SetActive(false);
            }
        }

        // ���� ����.
        GameManager.Instance.GameExit();
    }

    // ���� â���� �ڷ� ���� ��ư
    public void OnclickBack()
    {
        // 1. ���� â �ݱ�
        CloseUIPanel(UICanvasMode.GAME_SETTINGS);
        // 2. ���� Pause â ����
        OpenUIPanel(UICanvasMode.GAME_PAUSE);
    }
}
