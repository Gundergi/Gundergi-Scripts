using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임 시스템 관리자
// - 시간 관리
public class GameManager : MonoBehaviour
{

    private float playTime = 0;              // 일정 시간 지나면 게임 오버
    public float gameOverTime;               // 게임 오버 데드라인 시간
    public bool isPlaying = false;           // 게임 진행 중인지 여부
    public bool isRestart = false;

    // *싱글톤 공식
    public static GameManager Instance = null;

    private void Awake()
    {
        // 만일 인스턴스에 아직 넣은 것이 없는 상태라면
        if (Instance == null)
        {
            // 나 자신의 모든 정보를 집어넣는다.
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverTime = LiftUp.instance.autoLiftTime * 11f;

        Debug.Log(SceneManager.GetActiveScene().name == "Intro Scene");
        // 1. 만일 현재 씬이 #Intro씬이라면 게임 Start UI 오픈
        if (SceneManager.GetActiveScene().name == "Intro Scene")
        {
            UIManager.Instance.OpenUIPanel(UIManager.UICanvasMode.GAME_START);
        }
        // 2. 반대로 현재 씬이 #Intro 씬이 아니라면 무시
        else
        { 
        
        }      
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 시작한 경우에만 타이머 동작!
        if (isPlaying)
        {
            GameTimer();
        }
    }

    // 시간 초과하는지 체크.. 
    void GameTimer()
    {
        playTime += Time.deltaTime;
        // 시간 초과하면 게임 종료! => 게임 클리어 
        if (playTime > gameOverTime)
        {
            GameOver();

            playTime = 0;
        }
    }

    // 게임 시작 시 실행
    public void GameStart()
    {
        Debug.Log("게임 시작...");


        // 게임세상 시작
        isPlaying = true;
        Time.timeScale = 1;
    }

    // 게임 오버되면 실행
    public void GameOver()
    {
        Debug.Log("게임 오버...");

        isPlaying = false;
    }
    // 게임 재시작
    public void GameRestart()
    {
        Debug.Log("게임 재시작...");

        // 게임 초기화
        isPlaying = true;
        playTime = 1;

        if (!isRestart)
        {
            isRestart = true;
            Debug.Log("다음 씬으로");
            //2.타킷 씬 = "이동할 씬 파일 이름(Domino Scene)"
            string sceneName = "Intro Scene";

            //1.씬을 이동하고 싶다.
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    // 게임 일시정지
    public void GamePause()
    {
        Debug.Log("게임 일시정지...");

        // 게임세상 정지
        isPlaying = false;
        Time.timeScale = 0;

    }

    // 게임 종료
    public void GameExit()
    {
        Debug.Log("게임 종료...");

        //Unity Editor 종료
        UnityEditor.EditorApplication.isPlaying = false;

        // 어플리케이션 끝
        // Application.Quit();
    }


}
