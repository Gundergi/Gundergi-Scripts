using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;// 유니티 씬을 로드하기 위해서 사용

public class UIManager : MonoBehaviour
{

    // UI Canvas 종류(모드) 
    public enum UICanvasMode
    {
        GAME_START, GAME_OVER, GAME_PAUSE, GAME_SETTINGS, GAME_CLEAR, NONE
    }
    public UICanvasMode canvasMode = UICanvasMode.NONE;
    public GameObject[] panels;

    bool isMoveScene = false;       //씬 이동 실행하도록

    // *싱글톤 공식
    public static UIManager Instance = null;

    private void Awake()
    {
        // * 싱글톤 공식
        // 만일 인스턴스에 아직 넣은 것이 없는 상태라면
        if (Instance == null)
        {
            // 나 자신의 모든 정보를 집어넣는다.
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

            // UI 모드를 On 했을 때!
            if (isUIMode)
            {
                // canvasPointer 활성화해서.. 라인랜더러랑, ui 선택 될 수 있도록..
                uiCanvasPointer.gameObject.SetActive(true);
                // Game 일시정지
                //GameManager.Instance.GamePause();
            }
            // UI 모드를 Off 했을 때!
            else
            {
                // canvasPointer 비활성화해서.. 라인랜더러랑, ui 선택 안되게..
                uiCanvasPointer.gameObject.SetActive(false);
                // Game 다시 시작
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

        // ESC 누를 때 일시정지
        //if (Input.GetKeyDown(KeyCode.Escape))

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch)) //윤주 0731
        {
            for (int i = 0; i < UIManager.Instance.panels.Length; i++)
            {
                // 만일 켜져있는 캔버스창이 있다면
                if (UIManager.Instance.panels[i].activeSelf == true)
                {
                    return;
                }
            }
            UIManager.Instance.OpenGamePause();

        }
    }

    // 선택한 Canvas Panel 열기(활성화)
    public void OpenUIPanel(UICanvasMode panel)
    {
        switch (panel)
        {
            case UICanvasMode.GAME_START:
                // GAME_START 캔버스를 열었을 경우..
                //GameManager.Instance.GameStart();
                break;
            case UICanvasMode.GAME_OVER:
                // GAME_OVER 캔버스를 열었을 경우..
                GameManager.Instance.GameOver();
                break;
            case UICanvasMode.GAME_PAUSE:
                // GAME_PAUSE 캔버스를 열었을 경우..
                GameManager.Instance.GamePause();
                break;
            case UICanvasMode.GAME_SETTINGS:
                // GAME_SETTINGS 캔버스를 열었을 경우..
                GameManager.Instance.GamePause();
                break;
            case UICanvasMode.GAME_CLEAR:
                // GAME_CLEAR 캔버스를 열었을 경우..
                GameManager.Instance.GamePause();
                break;
        }

        Debug.Log($"[{panel.ToString()}] 패널 열어!");
        // 내가 선택한 패널의 캔버스 오브젝트를 활성화
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
                // GAME_PAUSE 캔버스를 닫았을 경우..
                GameManager.Instance.GameStart();
                break;
            case UICanvasMode.GAME_SETTINGS:
                break;
            case UICanvasMode.GAME_CLEAR:
                break;
        }
        // 내가 선택한 패널의 캔버스 오브젝트를 비활성화
        panels[(int)panel].SetActive(false);

        // 현재 캔버스모드 변경
        canvasMode = UICanvasMode.NONE;
    }

    // -- 게임 시작 창 열기/닫기 --
    public void OpenGameStart()
    {
        OpenUIPanel(UICanvasMode.GAME_START);

        if (!isMoveScene)
        {
            isMoveScene = true;
            Debug.Log("다음 씬으로");
            //2.타킷 씬 = "이동할 씬 파일 이름(Domino Scene)"
            string sceneName = "Intro Scene";

            //1.씬을 이동하고 싶다.
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    public void CloseGameStart()
    {
        CloseUIPanel(UICanvasMode.GAME_START);
    }

    // -- 게임 오버 창 열기/닫기 --
    public void OpenGameOver()
    {
        OpenUIPanel(UICanvasMode.GAME_OVER);
    }

    public void CloseGameOver()
    {
        CloseUIPanel(UICanvasMode.GAME_OVER);
    }

    // -- 게임 일시정지 창 열기/닫기 --
    public void OpenGamePause()
    {
        OpenUIPanel(UICanvasMode.GAME_PAUSE);
    }

    public void CloseGamePause()
    {
        CloseUIPanel(UICanvasMode.GAME_PAUSE);
    }

    // -- 게임 설정 창 열기/닫기 --
    public void OpenGameSettings()
    {
        OpenUIPanel(UICanvasMode.GAME_SETTINGS);
    }

    public void CloseGameSettings()
    {
        CloseUIPanel(UICanvasMode.GAME_SETTINGS);
    }

    // -- 게임 완료 창 열기/닫기 --
    public void OpenGameClear()
    {
        OpenUIPanel(UICanvasMode.GAME_CLEAR);
        GetComponentInChildren<AudioSource>().Play();
    }

    public void CloseGameClear()
    {
        CloseUIPanel(UICanvasMode.GAME_CLEAR);
    }

    // GameStart 버튼을 누르면 실행
    public void OnClickGameStart()
    {
        // 만일 캔버스 창이 열려있다면 닫기
        for (int i = 0; i < panels.Length; i++)
        {
            // 만일 켜져있는 캔버스창이 있다면
            if (panels[i].activeSelf == true)
            {
                // -> panels 배열 안에 있는 오브젝트들 모두 비활성화 
                panels[i].SetActive(false);
            }
        }

        // 게임 시작.
        GameManager.Instance.GameStart();
    }

    // Restart 버튼을 누르면 실행
    public void OnClickRestart()
    {
        // 만일 캔버스 창이 열려있다면 닫기
        for (int i = 0; i < panels.Length; i++)
        {
            // 만일 켜져있는 캔버스창이 있다면
            if (panels[i].activeSelf == true)
            {
                // -> panels 배열 안에 있는 오브젝트들 모두 비활성화 
                panels[i].SetActive(false);
            }
        }

        // 게임 재시작.
        GameManager.Instance.GameRestart();
    }

    // Quit 버튼을 누르면 실행
    public void OnClickExit()
    {
        // 만일 캔버스 창이 열려있다면 닫기
        for (int i = 0; i < panels.Length; i++)
        {
            // 만일 켜져있는 캔버스창이 있다면
            if (panels[i].activeSelf == true)
            {
                // -> panels 배열 안에 있는 오브젝트들 모두 비활성화 
                panels[i].SetActive(false);
            }
        }

        // 게임 종료.
        GameManager.Instance.GameExit();
    }

    // 설정 창에서 뒤로 가기 버튼
    public void OnclickBack()
    {
        // 1. 세팅 창 닫기
        CloseUIPanel(UICanvasMode.GAME_SETTINGS);
        // 2. 게임 Pause 창 열기
        OpenUIPanel(UICanvasMode.GAME_PAUSE);
    }
}
