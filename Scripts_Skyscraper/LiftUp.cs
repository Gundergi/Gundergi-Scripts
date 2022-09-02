using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftUp : MonoBehaviour
{
    // True면 위로 올라가는 모드, False면 아래로 내려가는 모드
    enum LiftMode
    {
        Up, Down
    }
    LiftMode mode = LiftMode.Up;

    public MeshRenderer lopeRenderer;           // 리프트 올라가는 줄 랜더러
    float matOffset = 0f;                       // 리프트 올라가는 Lope 텍스쳐 Offset 초기값

    public float moveSpeed = 1f;                // 올라가는 속도
    public float moveDistance = 2.93f;          // 범위
    public float autoLiftTime = 30;             // 일정시간(리프트 올라가는 시간)
    public float currentTime;                   // 경과시간
    float totalTime;

    public bool isMoveStart;                    // True 상태면 움직이고, False이면 정지
    public Vector3 targetPos;                   // 내가 이동해야할 목표 위치
    int floorCount=90;

    public static LiftUp instance = null;

    AudioSource audioSource;
    public AudioClip liftUp;
    public LiftBroken liftBroken;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlaying == false) { return; } //kyj 0731

        // 시간이 흐른다
        currentTime += Time.deltaTime;
        totalTime += Time.deltaTime;

        //한층당 흐르는 시간을 표시 ->전달
        UISystem.instance.OneFloorTimer((int)currentTime);

        // 일정시간이 되면
        if (currentTime >autoLiftTime)
        {
            // 위로 이동
            StartLiftUp(moveDistance);

            // 소리 시작
            audioSource.clip = liftUp;
            audioSource.Play();

            // 일정시간 리셋
            floorCount++;
            liftBroken.stainListCount++;
            UISystem.instance.CurrentFloor(floorCount);
            currentTime = 0;
        }    



        //// Q누르면 위로 리프트 이동
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    StartLiftUp(moveDistance);   // 3. 타겟 위치를 향해 moveDistance만큼 올라감
        //}
        //// E 누르면 아래로 리프트 이동
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    StartLiftDown(moveDistance); // 3. 타겟 위치를 향해 moveDistance만큼 내려감
        //}


        // ----------- 리프트 이동 영역 --------------
        if (mode == LiftMode.Up)
        {
            MoveLiftUp();       // LiftMode가 true면 위로 올라가는 함수 실행
        }
        else if (mode == LiftMode.Down)
        {
            MoveLiftDown();   // LiftMode가 false면 아래로 내려가는 함수 실행
        }

        //전체시간 지나면 게임클리어 UI 실행하도록 만들기
        //if (totalTime > autoLiftTime * 11)
        if(floorCount==100f)
        {
            Debug.Log("게임클리어");
            UIManager.Instance.OpenGameClear();
            Time.timeScale = 0;
        }
    }

    // 호출되면, Lift를 distance만큼 위로 이동시킨다.
    public void StartLiftUp(float distance)
    {
        mode = LiftMode.Up;         // 1. 리프트 모드 '위'로 변경
        IintTargetPosition();       // 2. 위로 내려갈 때 타겟 위치 설정
        moveDistance = distance;    // 3. 올라갈 거리 설정
        isMoveStart = true;         // 4. 올라가! 시작!
    }
    // 호출되면, Lift를 distance만큼 아래로 이동시킨다.
    public void StartLiftDown(float distance)
    {
        mode = LiftMode.Down;       // 1. 리프트 모드 '아래'로 변경
        IintTargetPosition();       // 2. 아래로 내려갈 때 타겟 위치 설정
        moveDistance = distance;    // 3. 내려갈 거리 설정
        isMoveStart = true;         // 4. 내려가! 내려가!
    }

    void MoveLiftUp()
    {
        if (isMoveStart == true)
        {
            // - 내위치&TargetPos의 거리를 비교해서,
            //  이 값이 0의 수렴될만큼 작은 값이라면
            if (transform.position.y > targetPos.y)
            {
                // moveDistance만큼 이동한 경우에는 멈춤
                isMoveStart = false;
                // 다음 이동할 TargetPos 초기화
                IintTargetPosition();
            }

            // 위 : Vector.up moveDistance 거리만큼 moveSpeed의 속도로 위로 위로 올라갑니다.
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
           //메테리얼 Offset을 5의 속도만큼 이동한다.
            matOffset += moveSpeed * 5 * Time.deltaTime;

            // 로프 Mat 움직이는 애니메이션 .. offset 변동(원본 메테리얼 바꾸려면 sharedMaterial로 작성)
            lopeRenderer.sharedMaterial.SetTextureOffset("_BaseMap", new Vector2(matOffset, 0));
        }
    }

    void MoveLiftDown()
    {
        if (isMoveStart == true)
        {
            // - 내위치&TargetPos의 거리를 비교해서,
            //  이 값이 0의 수렴될만큼 작은 값이라면
            if (transform.position.y < targetPos.y)
            {
                // moveDistance만큼 이동한 경우에는 멈춤
                isMoveStart = false;
                // 다음 이동할 TargetPos 초기화
                IintTargetPosition();
            }

            // 아래 : Vector.down moveDistance 거리만큼 moveSpeed의 속도로 위로 위로 올라갑니다.
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
            matOffset -= moveSpeed * 5 * Time.deltaTime;
            // 로프 Mat 움직이는 애니메이션 .. offset 변동(원본 메테리얼 바꾸려면 sharedMaterial로 작성)
            lopeRenderer.sharedMaterial.SetTextureOffset("_BaseMap", new Vector2(matOffset, 0));
        }
    }

    // 다음 이동할 TargetPos 초기화
    void IintTargetPosition()
    {
        targetPos = transform.position;

        if (mode == LiftMode.Up)
        {
            // 타겟 위치는 내 현재 기준 위치로 '위'쪽
            targetPos.y += moveDistance;
        }
        else if (mode == LiftMode.Down)
        {
            // 타겟 위치는 내 현재 기준 위치로 '아래'쪽
            targetPos.y -= moveDistance;
        }
    }
}
