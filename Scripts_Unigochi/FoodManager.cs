using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 만약에 UI에서 "먹기" 버튼을 누르면 내가 만든 음식 중에서, 랜덤으로 하나를 선택해 유니를 그 위치로 이동시킨다.
// - 만들 음식 종류
// - 음식 만들 위치
public class FoodManager : MonoBehaviour
{
    public GameObject[] foodPrefabs;         // - 만들 음식 종류
    public Transform foodSpawnPosition;      // - 음식 만들 위치
    public int minHealAmount = 50;           // - 최소값
    public int maxHealAmount = 500;          // - 최대값
    public float createTime = 60;            // - 일정시간(음식 생성해주는 시간)
    float currentTime;                       // - 경과시간                                  
    public float minRandomTime;              // - 랜덤 최소시간
    public float maxRandomTime;              // - 랜덤 최대시간

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // 4. 시간이 흐른다.
        currentTime += Time.deltaTime;
        // 3. 일정시간이 되면
        if (currentTime > createTime)
        {
            // 음식 만들어줘
            CreatFood();

            // 0. 시간 Reset 
            currentTime = 0;

            // 생성시간 Reset
            //ResetCreateTime();

        }
    }

    void CreatFood()
    {
        // 처음 시작할 때 foodSpawnPosision의 자식 수만큼 음식들 생성
        for (int i = 0; i < foodSpawnPosition.childCount; i++)
        {
            // 랜덤으로 만들 음식의 index
            int randomFoodIndex = GetRandomFoodIndex();
            // 랜덤 음식 생성
            GameObject food = Instantiate(foodPrefabs[randomFoodIndex]);
            // 생성한 음식을 foodSpawnPosition의 자식들 위치에 위치시키자.
            food.GetComponent<Transform>().position = foodSpawnPosition.GetChild(i).position;
            // 음식을 생성할 때, 회복량을 정해주고 싶다.
            // food.GetComponent<AddHealthEffect>().healthAmount = Random.Range(minHealAmount, maxHealAmount);
        }
    }


    // 확률에 따라서 랜덤하게 음식 나오게하기
    // - Carrot 1% 확률로 나옴
    int GetRandomFoodIndex()
    {
        // 1~100까지 숫자 중에서 1개를 뽑는다.
        int ranNumber = Random.Range(1, 100);
        // 만일 뽑은 숫자가 1이라면
        if (ranNumber == 1)
        {
            // Carrot의 인덱스를 반환
            return 0;
        }
        else
        {
            // 만일 뽑은 숫자가 1이 아니라면( 2~100)
            // 남은 음식들 인덱스를 랜덤하게 반환
            return Random.Range(1, foodPrefabs.Length);
        }
    }

    void ResetCreateTime()
    {
        // min~max 의 랜덤 시간을 생성
        float foodTime = Random.Range(minRandomTime, maxRandomTime);
        // createTime에 랜덤시간 할당
        createTime = foodTime;
    }
}
