using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] float kMinSpawnRange = 1f;
    [SerializeField] float kMaxSpawnRange = 10f;
    [SerializeField] GameObject[] kTreePrefabs;

	// Use this for initialization
	void Start ()
    {
        float add_angle = 30f;
        int min_round_per_angle = (int)((kMaxSpawnRange - kMinSpawnRange) / 5f);
        int max_round_per_angle = (int)((kMaxSpawnRange - kMinSpawnRange) / 2f) + 1;
        for(float angle = 0f; angle < 360f; angle += add_angle)
        {
            add_angle = Random.Range(20f, 35f);
            int round = Random.Range(min_round_per_angle, max_round_per_angle);
            for(int count_round = 0; count_round < round; ++count_round)
            {
                int tree_number = 1 + count_round;
                for (int count_tree = 0; count_tree < tree_number; ++count_tree)
                {
                    float angle_range = 5f * count_tree;
                    float fixed_radian = (angle + Random.Range(-angle_range, angle_range)) * Mathf.Deg2Rad;
                    var tree = Instantiate(kTreePrefabs[Random.Range(0, kTreePrefabs.Length)], transform);
                    float range = kMinSpawnRange + (kMaxSpawnRange - kMinSpawnRange) * ((float)count_tree / tree_number);
                    var tree_position = new Vector3(Mathf.Cos(fixed_radian), 0f, Mathf.Sin(fixed_radian)) * range;
                    tree.transform.localPosition = tree_position;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private void OnDrawGizmosSelected()
    {
        bool is_range_error = kMinSpawnRange >= kMaxSpawnRange;
        Gizmos.color = is_range_error ? Color.red : new Color(0.25f, 1f, 0.25f);
        Gizmos.DrawWireSphere(transform.position, kMinSpawnRange);
        Gizmos.color = is_range_error ? Color.red : new Color(0f, 1f, 0f);
        Gizmos.DrawWireSphere(transform.position, kMaxSpawnRange);
    }
}
