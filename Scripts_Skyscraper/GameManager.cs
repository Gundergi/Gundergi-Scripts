using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���� �ý��� ������
// - �ð� ����
public class GameManager : MonoBehaviour
{

    private float playTime = 0;              // ���� �ð� ������ ���� ����
    public float gameOverTime;               // ���� ���� ������� �ð�
    public bool isPlaying = false;           // ���� ���� ������ ����
    public bool isRestart = false;

    // *�̱��� ����
    public static GameManager Instance = null;

    private void Awake()
    {
        // ���� �ν��Ͻ��� ���� ���� ���� ���� ���¶��
        if (Instance == null)
        {
            // �� �ڽ��� ��� ������ ����ִ´�.
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverTime = LiftUp.instance.autoLiftTime * 11f;

        Debug.Log(SceneManager.GetActiveScene().name == "Intro Scene");
        // 1. ���� ���� ���� #Intro���̶�� ���� Start UI ����
        if (SceneManager.GetActiveScene().name == "Intro Scene")
        {
            UIManager.Instance.OpenUIPanel(UIManager.UICanvasMode.GAME_START);
        }
        // 2. �ݴ�� ���� ���� #Intro ���� �ƴ϶�� ����
        else
        { 
        
        }      
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ������ ��쿡�� Ÿ�̸� ����!
        if (isPlaying)
        {
            GameTimer();
        }
    }

    // �ð� �ʰ��ϴ��� üũ.. 
    void GameTimer()
    {
        playTime += Time.deltaTime;
        // �ð� �ʰ��ϸ� ���� ����! => ���� Ŭ���� 
        if (playTime > gameOverTime)
        {
            GameOver();

            playTime = 0;
        }
    }

    // ���� ���� �� ����
    public void GameStart()
    {
        Debug.Log("���� ����...");


        // ���Ӽ��� ����
        isPlaying = true;
        Time.timeScale = 1;
    }

    // ���� �����Ǹ� ����
    public void GameOver()
    {
        Debug.Log("���� ����...");

        isPlaying = false;
    }
    // ���� �����
    public void GameRestart()
    {
        Debug.Log("���� �����...");

        // ���� �ʱ�ȭ
        isPlaying = true;
        playTime = 1;

        if (!isRestart)
        {
            isRestart = true;
            Debug.Log("���� ������");
            //2.ŸŶ �� = "�̵��� �� ���� �̸�(Domino Scene)"
            string sceneName = "Intro Scene";

            //1.���� �̵��ϰ� �ʹ�.
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    // ���� �Ͻ�����
    public void GamePause()
    {
        Debug.Log("���� �Ͻ�����...");

        // ���Ӽ��� ����
        isPlaying = false;
        Time.timeScale = 0;

    }

    // ���� ����
    public void GameExit()
    {
        Debug.Log("���� ����...");

        //Unity Editor ����
        UnityEditor.EditorApplication.isPlaying = false;

        // ���ø����̼� ��
        // Application.Quit();
    }


}
