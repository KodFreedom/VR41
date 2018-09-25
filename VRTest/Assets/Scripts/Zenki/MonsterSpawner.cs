using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] float kSpawnRange = 1f;
    [SerializeField] GameObject kMonsterPrefab;
    private float count_down_ = 1f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        count_down_ -= Time.deltaTime;
        if(count_down_ <= 0f)
        {
            // モンスター生成
            var forward = Camera.main.transform.forward;
            forward.y = 0f;
            forward.Normalize();
            float angle = Vector3.Angle(Vector3.forward, forward) + Random.Range(-45f, 45f) * Mathf.Deg2Rad;
            Vector3 position = new Vector3(kSpawnRange * Mathf.Sin(angle), 0f, kSpawnRange * Mathf.Cos(angle));
            Debug.Log("angle : " + angle);
            Debug.Log("position : " + position);
            GameObject.Instantiate(kMonsterPrefab, position, Quaternion.identity);
            count_down_ = Random.Range(2f, 4f);
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, kSpawnRange);
    }
}
