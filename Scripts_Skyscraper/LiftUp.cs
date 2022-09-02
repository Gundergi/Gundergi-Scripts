using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftUp : MonoBehaviour
{
    // True�� ���� �ö󰡴� ���, False�� �Ʒ��� �������� ���
    enum LiftMode
    {
        Up, Down
    }
    LiftMode mode = LiftMode.Up;

    public MeshRenderer lopeRenderer;           // ����Ʈ �ö󰡴� �� ������
    float matOffset = 0f;                       // ����Ʈ �ö󰡴� Lope �ؽ��� Offset �ʱⰪ

    public float moveSpeed = 1f;                // �ö󰡴� �ӵ�
    public float moveDistance = 2.93f;          // ����
    public float autoLiftTime = 30;             // �����ð�(����Ʈ �ö󰡴� �ð�)
    public float currentTime;                   // ����ð�
    float totalTime;

    public bool isMoveStart;                    // True ���¸� �����̰�, False�̸� ����
    public Vector3 targetPos;                   // ���� �̵��ؾ��� ��ǥ ��ġ
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

        // �ð��� �帥��
        currentTime += Time.deltaTime;
        totalTime += Time.deltaTime;

        //������ �帣�� �ð��� ǥ�� ->����
        UISystem.instance.OneFloorTimer((int)currentTime);

        // �����ð��� �Ǹ�
        if (currentTime >autoLiftTime)
        {
            // ���� �̵�
            StartLiftUp(moveDistance);

            // �Ҹ� ����
            audioSource.clip = liftUp;
            audioSource.Play();

            // �����ð� ����
            floorCount++;
            liftBroken.stainListCount++;
            UISystem.instance.CurrentFloor(floorCount);
            currentTime = 0;
        }    



        //// Q������ ���� ����Ʈ �̵�
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    StartLiftUp(moveDistance);   // 3. Ÿ�� ��ġ�� ���� moveDistance��ŭ �ö�
        //}
        //// E ������ �Ʒ��� ����Ʈ �̵�
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    StartLiftDown(moveDistance); // 3. Ÿ�� ��ġ�� ���� moveDistance��ŭ ������
        //}


        // ----------- ����Ʈ �̵� ���� --------------
        if (mode == LiftMode.Up)
        {
            MoveLiftUp();       // LiftMode�� true�� ���� �ö󰡴� �Լ� ����
        }
        else if (mode == LiftMode.Down)
        {
            MoveLiftDown();   // LiftMode�� false�� �Ʒ��� �������� �Լ� ����
        }

        //��ü�ð� ������ ����Ŭ���� UI �����ϵ��� �����
        //if (totalTime > autoLiftTime * 11)
        if(floorCount==100f)
        {
            Debug.Log("����Ŭ����");
            UIManager.Instance.OpenGameClear();
            Time.timeScale = 0;
        }
    }

    // ȣ��Ǹ�, Lift�� distance��ŭ ���� �̵���Ų��.
    public void StartLiftUp(float distance)
    {
        mode = LiftMode.Up;         // 1. ����Ʈ ��� '��'�� ����
        IintTargetPosition();       // 2. ���� ������ �� Ÿ�� ��ġ ����
        moveDistance = distance;    // 3. �ö� �Ÿ� ����
        isMoveStart = true;         // 4. �ö�! ����!
    }
    // ȣ��Ǹ�, Lift�� distance��ŭ �Ʒ��� �̵���Ų��.
    public void StartLiftDown(float distance)
    {
        mode = LiftMode.Down;       // 1. ����Ʈ ��� '�Ʒ�'�� ����
        IintTargetPosition();       // 2. �Ʒ��� ������ �� Ÿ�� ��ġ ����
        moveDistance = distance;    // 3. ������ �Ÿ� ����
        isMoveStart = true;         // 4. ������! ������!
    }

    void MoveLiftUp()
    {
        if (isMoveStart == true)
        {
            // - ����ġ&TargetPos�� �Ÿ��� ���ؼ�,
            //  �� ���� 0�� ���ŵɸ�ŭ ���� ���̶��
            if (transform.position.y > targetPos.y)
            {
                // moveDistance��ŭ �̵��� ��쿡�� ����
                isMoveStart = false;
                // ���� �̵��� TargetPos �ʱ�ȭ
                IintTargetPosition();
            }

            // �� : Vector.up moveDistance �Ÿ���ŭ moveSpeed�� �ӵ��� ���� ���� �ö󰩴ϴ�.
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
           //���׸��� Offset�� 5�� �ӵ���ŭ �̵��Ѵ�.
            matOffset += moveSpeed * 5 * Time.deltaTime;

            // ���� Mat �����̴� �ִϸ��̼� .. offset ����(���� ���׸��� �ٲٷ��� sharedMaterial�� �ۼ�)
            lopeRenderer.sharedMaterial.SetTextureOffset("_BaseMap", new Vector2(matOffset, 0));
        }
    }

    void MoveLiftDown()
    {
        if (isMoveStart == true)
        {
            // - ����ġ&TargetPos�� �Ÿ��� ���ؼ�,
            //  �� ���� 0�� ���ŵɸ�ŭ ���� ���̶��
            if (transform.position.y < targetPos.y)
            {
                // moveDistance��ŭ �̵��� ��쿡�� ����
                isMoveStart = false;
                // ���� �̵��� TargetPos �ʱ�ȭ
                IintTargetPosition();
            }

            // �Ʒ� : Vector.down moveDistance �Ÿ���ŭ moveSpeed�� �ӵ��� ���� ���� �ö󰩴ϴ�.
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
            matOffset -= moveSpeed * 5 * Time.deltaTime;
            // ���� Mat �����̴� �ִϸ��̼� .. offset ����(���� ���׸��� �ٲٷ��� sharedMaterial�� �ۼ�)
            lopeRenderer.sharedMaterial.SetTextureOffset("_BaseMap", new Vector2(matOffset, 0));
        }
    }

    // ���� �̵��� TargetPos �ʱ�ȭ
    void IintTargetPosition()
    {
        targetPos = transform.position;

        if (mode == LiftMode.Up)
        {
            // Ÿ�� ��ġ�� �� ���� ���� ��ġ�� '��'��
            targetPos.y += moveDistance;
        }
        else if (mode == LiftMode.Down)
        {
            // Ÿ�� ��ġ�� �� ���� ���� ��ġ�� '�Ʒ�'��
            targetPos.y -= moveDistance;
        }
    }
}
