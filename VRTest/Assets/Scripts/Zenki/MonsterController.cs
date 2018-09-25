using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] GameObject[] kModels = new GameObject[4];
    [SerializeField] float kModelScale = 50f;
    [SerializeField] float kJumpSpeed = 10f;
    [SerializeField] float wait_count_ = -1f;
    private Rigidbody rigidbody_ = null;
    private Vector3 direction_;

	// Use this for initialization
	void Start ()
    {
        int no = Random.Range(0, 4);
        var model = GameObject.Instantiate(kModels[no], transform);
        model.transform.localScale = new Vector3(kModelScale, kModelScale, kModelScale);
        rigidbody_ = GetComponent<Rigidbody>();
        direction_ = transform.forward;
        //MonsterIconController.Instance.Spawned(transform);
    }

    private void OnDestroy()
    {
        //MonsterIconController.Instance.Dead(transform);
    }

    // Update is called once per frame
    void Update ()
    {
        wait_count_ -= Time.deltaTime;
        if((int)wait_count_ == 0)
        {
            var player_position = Camera.main.transform.position;
            direction_ = Vector3.Scale(player_position - transform.position, new Vector3(1, 0, 1)).normalized;
            transform.rotation *= Quaternion.FromToRotation(transform.forward, direction_);
            Quaternion rotate = Quaternion.AngleAxis(-45f, transform.right);
            direction_ = rotate * direction_;
            rigidbody_.AddForce(direction_ * kJumpSpeed);
            wait_count_ = -1f;
        }

        if(wait_count_ < 0 && Physics.Raycast(transform.position + Vector3.up, Vector3.down, 2f))
        {
            wait_count_ = Random.Range(2.5f, 3.5f);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
            GameController.Instance.GameOver();
        }
    }
}
