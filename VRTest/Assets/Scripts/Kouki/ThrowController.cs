using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private struct ThrowInfo
    {
        public Vector3 position;
        public int item_no;
    }

    [SerializeField] Vector2 kThrowCoolDownRange = Vector2.zero;
    [SerializeField] Vector2 kThrowInfoCreatRange = Vector2.zero;
    [SerializeField] Vector2 kArriveTimeRange = Vector2.zero;
    [SerializeField] GameObject[] kItems = new GameObject[1];
    private float throw_cool_down_ = 1f;
    private float throw_info_cool_down_ = 0.25f;
    private Queue<ThrowInfo> throw_infos_ = new Queue<ThrowInfo>();

    // Use this for initialization
    private void Start ()
    {
	    	
	}

    // Update is called once per frame
    private void Update ()
    {
        DecideThrowInfo();
        ThrowAll();
	}

    private void DecideThrowInfo()
    {
        throw_info_cool_down_ -= Time.deltaTime;
        if(throw_info_cool_down_ <= 0f)
        {
            throw_info_cool_down_ = Random.Range(kThrowInfoCreatRange.x, kThrowInfoCreatRange.y);
            ThrowInfo new_info = new ThrowInfo();
            new_info.position = transform.position;
            new_info.position.x += Random.Range(-transform.localScale.x * 0.5f, transform.localScale.x * 0.5f);
            new_info.position.z += Random.Range(-transform.localScale.z * 0.5f, transform.localScale.z * 0.5f);
            new_info.item_no = Random.Range(0, kItems.Length);
            throw_infos_.Enqueue(new_info);
        }
    }

    private void ThrowAll()
    {
        throw_cool_down_ -= Time.deltaTime;
        if (throw_cool_down_ <= 0f)
        {
            throw_cool_down_ = Random.Range(kThrowCoolDownRange.x, kThrowCoolDownRange.y);
            while(throw_infos_.Count > 0)
            {
                Throw(throw_infos_.Dequeue(), Camera.main.transform.position);
            }
        }
    }

    private void Throw(ThrowInfo throw_info, Vector3 target)
    {
        var item = Instantiate<GameObject>(kItems[throw_info.item_no], throw_info.position, Quaternion.identity);
        var rigidbody = item.AddComponent<Rigidbody>();
        rigidbody.drag = 0f;
        var direction_xz = target - throw_info.position;
        float height = direction_xz.y;
        direction_xz.y = 0f;
        float arrive_time = Random.Range(kArriveTimeRange.x, kArriveTimeRange.y);

        // 水平移動
        var velocity = direction_xz / arrive_time;

        // 垂直移動
        float g = Physics.gravity.y;
        velocity.y = Mathf.Sqrt(0.75f * g * g * arrive_time * arrive_time - g * height) + 0.5f * g * arrive_time;
        rigidbody.AddForce(velocity / Time.deltaTime);

        // 回転速度
        rigidbody.inertiaTensorRotation = (Quaternion.Euler(new Vector3(30f, 0f, 60f)));
    }

    private void OnDrawGizmos()
    {
        var size = transform.localScale;
        size.y = 0f;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
