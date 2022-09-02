using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���࿡ UI���� "�Ա�" ��ư�� ������ ���� ���� ���� �߿���, �������� �ϳ��� ������ ���ϸ� �� ��ġ�� �̵���Ų��.
// - ���� ���� ����
// - ���� ���� ��ġ
public class FoodManager : MonoBehaviour
{
    public GameObject[] foodPrefabs;         // - ���� ���� ����
    public Transform foodSpawnPosition;      // - ���� ���� ��ġ
    public int minHealAmount = 50;           // - �ּҰ�
    public int maxHealAmount = 500;          // - �ִ밪
    public float createTime = 60;            // - �����ð�(���� �������ִ� �ð�)
    float currentTime;                       // - ����ð�                                  
    public float minRandomTime;              // - ���� �ּҽð�
    public float maxRandomTime;              // - ���� �ִ�ð�

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // 4. �ð��� �帥��.
        currentTime += Time.deltaTime;
        // 3. �����ð��� �Ǹ�
        if (currentTime > createTime)
        {
            // ���� �������
            CreatFood();

            // 0. �ð� Reset 
            currentTime = 0;

            // �����ð� Reset
            //ResetCreateTime();

        }
    }

    void CreatFood()
    {
        // ó�� ������ �� foodSpawnPosision�� �ڽ� ����ŭ ���ĵ� ����
        for (int i = 0; i < foodSpawnPosition.childCount; i++)
        {
            // �������� ���� ������ index
            int randomFoodIndex = GetRandomFoodIndex();
            // ���� ���� ����
            GameObject food = Instantiate(foodPrefabs[randomFoodIndex]);
            // ������ ������ foodSpawnPosition�� �ڽĵ� ��ġ�� ��ġ��Ű��.
            food.GetComponent<Transform>().position = foodSpawnPosition.GetChild(i).position;
            // ������ ������ ��, ȸ������ �����ְ� �ʹ�.
            // food.GetComponent<AddHealthEffect>().healthAmount = Random.Range(minHealAmount, maxHealAmount);
        }
    }


    // Ȯ���� ���� �����ϰ� ���� �������ϱ�
    // - Carrot 1% Ȯ���� ����
    int GetRandomFoodIndex()
    {
        // 1~100���� ���� �߿��� 1���� �̴´�.
        int ranNumber = Random.Range(1, 100);
        // ���� ���� ���ڰ� 1�̶��
        if (ranNumber == 1)
        {
            // Carrot�� �ε����� ��ȯ
            return 0;
        }
        else
        {
            // ���� ���� ���ڰ� 1�� �ƴ϶��( 2~100)
            // ���� ���ĵ� �ε����� �����ϰ� ��ȯ
            return Random.Range(1, foodPrefabs.Length);
        }
    }

    void ResetCreateTime()
    {
        // min~max �� ���� �ð��� ����
        float foodTime = Random.Range(minRandomTime, maxRandomTime);
        // createTime�� �����ð� �Ҵ�
        createTime = foodTime;
    }
}
