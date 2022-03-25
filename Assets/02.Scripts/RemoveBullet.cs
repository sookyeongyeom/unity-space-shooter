using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    // 충돌이 시작할 때 발생하는 이벤트
    private void OnCollisionEnter(Collision coll)
    {
        // 충돌한 게임오브젝트의 태그값 비교 (가비지 컬렉션의 대상이 됨)
        // if (coll.collider.tag == "BULLET")
        // {
        //     // 충돌한 게임오브젝트 삭제
        //     Destroy(coll.gameObject);
        // }

        // 가비지 컬렉션이 발생하지 않는 방식
        if (coll.collider.CompareTag("BULLET"))
        {
            // 충돌한 게임오브젝트 삭제
            Destroy(coll.gameObject);
        }
    }
}
