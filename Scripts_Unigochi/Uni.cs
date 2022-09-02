using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // - Nav Agent A.l ���� �ʿ�


// Target(��ǥ) ������ ���� �̵��ϵ��� ����� �ʹ�.
// - ��ǥ ����
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
    public static Uni Instance = null;           // �̱���
    public UniState uniState = UniState.Idle;    // - ������ ���� ����
    private Animator animator;                   // enemy�� �ִϸ��̼��� ����
    public Transform target;                     // - ��ǥ ����
    public NavMeshAgent uni;                     // - uni Component
    float foodSearchRange = 3f;                  // ���� ã�� �� �ִ� �ݰ�
    public float decreaseJumpGauge = 1f;         // - �����Ҷ� �����ϴ� ������
    public float stopDistance = 0.1f;            // - �����Ѽ� ġ�� �Ÿ�

    // �̱��� �Ҵ�
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
        // ������Ʈ�� ������ �ʱ�ȭ�����ֱ� ����!
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        // ���콺 ������ ��ư�� Ŭ���ϸ�,
        if (Input.GetMouseButtonDown(0))
        {
            // GameView���� ������ ��ġ�� Uni�� �̵���Ű���� �Ѵ�.
            // 3. GameView���� ���콺 ��ġ�� �������� ��ǥ���� ����
            // - Camera���� ���콺 ��ġ�� Ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // - �浹�� ���� �ִ� ���
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 2. ��ǥ ������ ���Ѵ�.
                target.position = hitInfo.point;
                // ��ǥ����(=target)���� ������ ������ Uni�� �̵��ϰ� �ϰ� �ʹ�.
                uni.SetDestination(target.position);
                // ������·� ����
                ChangeState(UniState.Move);

            }

        }

        // UniState�� ����ִ� ���� ����, �б�(Idle,Move,Attack,Damage.Die)�� ����
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

            // ���� Ʈ���ſ� ���ϰ� �ε����ٸ� E�� �����ٸ� �Ա�
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnClickEatFood();
            }
        }
    }

    // State�� �������ִ� �Լ�
    public void ChangeState(UniState state)
    {
        // ������ state�� ���� Idle ���¶��,
        if (state == UniState.Idle)
        {
            // Idle�� animation ����
            animator.SetTrigger("Idle");
        }

        // ������ state�� ���� Move ���¶��,
        if (state == UniState.Move)
        {
            // Move�� animation ����
            animator.SetTrigger("Move");
        }
        // ������ state�� ���� Eat���¶��
        else if (state == UniState.Eat)
        {
            // Eat Animation ����
            animator.SetTrigger("Eat");
        }
        // ������ state�� ���� Sleep ���¶��
        else if (state == UniState.Sleep)
        {
            // Sleep Animation ����
            animator.SetTrigger("Sleep");
        }

        // state ����
        uniState = state;

    }

    void Idle()
    {

    }

    public void Move()
    {
        // - �Ÿ� : �� - ������(target.position)
        float distance = Vector3.Distance(target.transform.position, transform.position);
        // ���࿡ ���� �������� �����ϸ�..
        // - �Ÿ��� ���࿡ 0.4 ���� �۾����� �����Ѽ�ġ��.
        if (distance <= stopDistance)
        {
            //  =>>> �� ���¸� �ٽ� Idle ���·� ����
            ChangeState(UniState.Idle);
        }
    }
    public void Jump()
    {
        // ���� ���� �����̽��� ��ư�� �����ٸ�
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            // �����ϸ�.. Tired ������ ����
            FindObjectOfType<TiredStatus>().DecreaseTimeWhenJumnp(decreaseJumpGauge);

            // �����ϸ� �ִϸ����� ����
            GetComponentInChildren<Animator>().SetTrigger("Jump");
        }
    }

    public void Eat()
    {
        // �������� �����ϰų�, ������ ���� ��,
        // �� ���� �ݰ� radius �ȿ� Food �� �����ϰ�,
        // Ray �� ��̴ϴ�.(�� �ڽ��� ��ġ����, ���� �ٶ󺸴� Z�� ��������)
        Ray ray = new Ray(transform.position, transform.forward);
        // ���ĸ� �浹�ϴ� �༮
        int layerMask = LayerMask.NameToLayer("Food");
        // RayCastHit �浹ü'��'�� ������(foodSearchRange ũ��)�� Ray(SphereCastAll)�� ��Ƽ�
        RaycastHit[] hits = Physics.SphereCastAll(ray, foodSearchRange, 0, 1 << layerMask);

        // ���� ����� �Ÿ��� �ִ� Food�� �ε����� �� ����
        int selectedIndex = -1;

        // ���࿡ �ε��� �༮�� �ִٸ�...
        if (hits != null && hits.Length > 0)
        {
            // ���� ù��° �༮�� index�� �ֽ��ϴ�.
            selectedIndex = 0;
            // ���࿡ �ε��� �༮�� �߿�
            for (int i = 1; i < hits.Length; i++)
            {
                // ���� ����� �༮�� �����ô�.
                if (hits[selectedIndex].distance > hits[i].distance)
                {
                    selectedIndex = i;
                }
            }

            // ���� ����� �Ÿ��� �ִ� SelectedIndex ��ȣ�� �浹ü Food�� �Խ��ϴ�.
            Debug.Log(hits[selectedIndex].collider.name);
            hits[selectedIndex].transform.GetComponent<AddHealthEffect>().AddHealth();
            // �԰� ���� Idle ���·� ��ȯ
            uniState = UniState.Idle;
        }
    }

    public void Sleep()
    {
        // ���� �ڸ�.. �����߰�
        FindObjectOfType<SleepStatus>().SleepBed(10);

        // �ִϸ��̼� ����
        //GetComponentInChildren<Animation>().Play("UniSleep");
        animator.SetTrigger("Sleep");
    }


    // "�Ա�"��ư Ŭ������ �� ����Ǵ� �Լ�
    public void OnClickEatFood()
    {
        // ���� ���¸� Eat ����
        uniState = UniState.Eat;
    }

}
