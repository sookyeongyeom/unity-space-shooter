using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // 컴포넌트를 캐시 처리할 변수
    private Transform tr;
    // Animation 컴포넌트를 저장할 변수
    private Animation anim;

    // 이동 속력 변수 (public으로 선언되어 인스펙터 뷰에 노출됨)
    public float moveSpeed = 10.0f;
    // 회전 속도 변수
    public float turnSpeed = 80.0f;

    // 초기 생명 값
    private readonly float initHp = 100.0f;
    // 현재 생명 값
    public float currHp = 100.0f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // 컴포넌트를 추출해 변수에 대입
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        // 애니메이션 실행
        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float r = Input.GetAxis("Mouse X");

        // Debug.Log("h=" + h);
        // Debug.Log("v=" + v);

        // Transform 컴포넌트의 위치를 변경
        // transform.position += new Vector3(0, 0, 1);

        // 정규화 벡터를 사용한 코드 (전진방향 * 속력)
        // tr.position += Vector3.forward * 1;

        // Translate 함수를 사용한 이동 로직
        // tr.Translate(Vector3.forward * 1);

        // 프레임마다 10유닛씩 이동
        // tr.Translate(Vector3.forward * 10);

        // 매 초 10유닛씩 이동
        // tr.Translate(Vector3.forward * Time.deltaTime * 10);

        // Translate + Time.deltaTime
        // tr.Translate(Vector3.forward * Time.deltaTime * v * moveSpeed);
        // tr.Translate(Vector3.right * Time.deltaTime * h * moveSpeed);

        // 전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        // Translate(이동 방향 * 속력 * Time.deltaTime)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

        // Vector3.up 축을 기준으로 turnSpeed만큼의 속도로 회전
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        // 주인공 캐릭터의 애니메이션 설정
        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        // 키보드 입력값을 기준으로 동작할 애니메이션 수행
        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f);  // 전진
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);  // 후진
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);  // 우측 이동
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);  // 좌측 이동
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);  // 정지 시 Idle
        }
    }

    // 충돌한 Collider의 IsTrigger 옵션이 체크됐을 때 발생
    private void OnTriggerEnter(Collider coll)
    {
        if (currHp >= 0.0f && coll.CompareTag("PUNCH"))
        {
            if (currHp > 0.0f)
            {
                currHp -= 10.0f;
                Debug.Log($"Player HP = {currHp / initHp}");
            }
            else
            {
                return;
            }

            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player Die !");

        // MONSTER 태그를 가진 모든 게임오브젝트를 찾아옴
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        foreach (GameObject monster in monsters)
        {
            monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }
    }
}
