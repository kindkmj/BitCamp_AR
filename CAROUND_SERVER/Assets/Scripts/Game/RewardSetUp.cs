using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSetUp : MonoBehaviour
{
    //함수 생성뒤 함수에 입력된 이름과 같은 유저를 검색한 다음 해당유저에 코인을 가져온뒤 보상으로 받은 코인을 기존 코인에 더한뒤 저장해줌
    // Start is called before the first frame update
    public void settest(List<string> tests)
    {
        int coin = 20;
        for (int i = 0; i < tests.Count; i++)
        {
            //tests에 들어있는 값을 비교해서 최상단에는 가장 높은 금액을 주기
            coin -= 5;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
