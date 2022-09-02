using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // - Nav Agent A.l 도구 필요


// Target(목표) 지점을 향해 이동하도록 만들고 싶다.
// - 목표 지점
// - uni Component

public class Uni : MonoBehaviour
{
    public enum UniState
    {
        Idle,
        Move,
        Eat,
        Sleep
    }
    public static Uni Instance = null;           // 싱글톤
    public UniState uniState = UniState.Idle;    // - 유니의 현재 상태
    private Animator animator;                   // enemy의 애니메이션을 관리
    public Transform target;                     // - 목표 지점
    public NavMeshAgent uni;                     // - uni Component
    float foodSearchRange = 3f;                  // 음식 찾을 수 있는 반경
    public float decreaseJumpGauge = 1f;         // - 점프할때 감속하는 게이지
    public float stopDistance = 0.1f;            // - 도착한셈 치는 거리

    // 싱글톤 할당
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ResetAnimator();
    }

    public void ResetAnimator()
    {
        // 컴포넌트들 가져와 초기화시켜주기 꼭꼭!
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        // 마우스 오른쪽 버튼을 클릭하면,
        if (Input.GetMouseButtonDown(0))
        {
            // GameView에서 선택한 위치로 Uni를 이동시키려고 한다.
            // 3. GameView에서 마우스 위치를 기준으로 목표지점 설정
            // - Camera에서 마우스 위치로 Ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // - 충돌할 곳이 있는 경우
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 2. 목표 지점을 구한다.
                target.position = hitInfo.point;
                // 목표지점(=target)으로 설정된 곳으로 Uni를 이동하게 하고 싶다.
                uni.SetDestination(target.position);
                // 무브상태로 변경
                ChangeState(UniState.Move);

            }

        }

        // UniState에 들어있는 값에 따라, 분기(Idle,Move,Attack,Damage.Die)를 지정
        switch (uniState)
        {
            case UniState.Idle: Idle(); break;
            case UniState.Move: Move(); break;
            case UniState.Eat: Eat(); break;
            case UniState.Sleep: Sleep(); break;
        }

        if (uniState == UniState.Idle || uniState == UniState.Move)
        {
            Jump();

            // 음식 트리거에 유니가 부딪혔다면 E를 누른다면 먹기
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnClickEatFood();
            }
        }
    }

    // State를 변경해주는 함수
    public void ChangeState(UniState state)
    {
        // 변경한 state가 만일 Idle 상태라면,
        if (state == UniState.Idle)
        {
            // Idle의 animation 설정
            animator.SetTrigger("Idle");
        }

        // 변경한 state가 만일 Move 상태라면,
        if (state == UniState.Move)
        {
            // Move의 animation 설정
            animator.SetTrigger("Move");
        }
        // 변경한 state가 만일 Eat상태라면
        else if (state == UniState.Eat)
        {
            // Eat Animation 실행
            animator.SetTrigger("Eat");
        }
        // 변경한 state가 만일 Sleep 상태라면
        else if (state == UniState.Sleep)
        {
            // Sleep Animation 실행
            animator.SetTrigger("Sleep");
        }

        // state 변경
        uniState = state;

    }

    void Idle()
    {

    }

    public void Move()
    {
        // - 거리 : 나 - 목적지(target.position)
        float distance = Vector3.Distance(target.transform.position, transform.position);
        // 만약에 내가 목적지에 도착하면..
        // - 거리가 만약에 0.4 보다 작아지면 도착한셈치자.
        if (distance <= stopDistance)
        {
            //  =>>> 내 상태를 다시 Idle 상태로 변경
            ChangeState(UniState.Idle);
        }
    }
    public void Jump()
    {
        // 만일 내가 스페이스바 버튼을 눌렀다면
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            // 점프하면.. Tired 게이지 감소
            FindObjectOfType<TiredStatus>().DecreaseTimeWhenJumnp(decreaseJumpGauge);

            // 점프하면 애니메이터 실행
            GetComponentInChildren<Animator>().SetTrigger("Jump");
        }
    }

    public void Eat()
    {
        // 목적지에 도착하거나, 가만히 있을 때,
        // 내 주위 반경 radius 안에 Food 가 존재하고,
        // Ray 를 쏠겁니다.(내 자신의 위치에서, 내가 바라보는 Z축 방향으로)
        Ray ray = new Ray(transform.position, transform.forward);
        // 음식만 충돌하는 녀석
        int layerMask = LayerMask.NameToLayer("Food");
        // RayCastHit 충돌체'들'을 구형태(foodSearchRange 크기)의 Ray(SphereCastAll)을 쏘아서
        RaycastHit[] hits = Physics.SphereCastAll(ray, foodSearchRange, 0, 1 << layerMask);

        // 가장 가까운 거리에 있는 Food의 인덱스가 들어갈 변수
        int selectedIndex = -1;

        // 만약에 부딪힌 녀석이 있다면...
        if (hits != null && hits.Length > 0)
        {
            // 가장 첫번째 녀석의 index를 넣습니다.
            selectedIndex = 0;
            // 만약에 부딪힌 녀석들 중에
            for (int i = 1; i < hits.Length; i++)
            {
                // 가장 가까운 녀석을 먹읍시다.
                if (hits[selectedIndex].distance > hits[i].distance)
                {
                    selectedIndex = i;
                }
            }

            // 가장 가까운 거리에 있는 SelectedIndex 번호의 충돌체 Food를 먹습니다.
            Debug.Log(hits[selectedIndex].collider.name);
            hits[selectedIndex].transform.GetComponent<AddHealthEffect>().AddHealth();
            // 먹고 나면 Idle 상태로 변환
            uniState = UniState.Idle;
        }
    }

    public void Sleep()
    {
        // 잠을 자면.. 졸림추가
        FindObjectOfType<SleepStatus>().SleepBed(10);

        // 애니메이션 실행
        //GetComponentInChildren<Animation>().Play("UniSleep");
        animator.SetTrigger("Sleep");
    }


    // "먹기"버튼 클릭했을 때 실행되는 함수
    public void OnClickEatFood()
    {
        // 현재 상태를 Eat 변경
        uniState = UniState.Eat;
    }

}
