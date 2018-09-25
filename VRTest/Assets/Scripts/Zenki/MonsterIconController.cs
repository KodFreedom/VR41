using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIconController : MonoBehaviour
{
    private static MonsterIconController instance_ = null;
    public static MonsterIconController Instance { get { return instance_; } }
    private struct Monster
    {
        public int hash;
        public Transform monster;
        public RectTransform icon;
    }

    [SerializeField] GameObject kIconPrefab;
    private List<Monster> monsters_ = new List<Monster>();

    public void Spawned(Transform monster)
    {
        Monster new_monster;
        new_monster.hash = monster.GetHashCode();
        new_monster.monster = monster;
        new_monster.icon = GameObject.Instantiate(kIconPrefab, transform).GetComponent<RectTransform>();
        CalculateIconPosition(new_monster);
        monsters_.Add(new_monster);
    }

    public void Dead(Transform monster)
    {
        foreach(var item in monsters_)
        {
            if(item.hash == monster.GetHashCode())
            {
                Destroy(item.icon.gameObject);
                monsters_.Remove(item);
                Debug.Log("Removed");
                return;
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        // インスタンスが生成されてるかどうかをチェックする
        if (null == instance_)
        {
            // ないなら自分を渡す
            instance_ = this;
        }
        else
        {
            // すでにあるなら自分を破棄する
            DestroyImmediate(gameObject);
            return;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        foreach (var item in monsters_)
        {
            CalculateIconPosition(item);
        }
    }

    private void CalculateIconPosition(Monster monster)
    {
        var direction = monster.monster.position;
        direction.y = 0f;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        direction = rotation * direction;
        
        var new_point = new Vector2(64f + 64f * direction.x, -36f + 36f * direction.z);
        
        monster.icon.anchoredPosition = new_point;
    }
}
