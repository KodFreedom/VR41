using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject kBulletPrefab = null;
    [SerializeField] Transform kBulletSpawner = null;
    [SerializeField] TextMeshPro kText = null;
    private SteamVR_TrackedObject gun_ = null;
    private static readonly int kMaxBullet = 30;
    private int bullet_number_ = kMaxBullet;
    private float previous_position_z_ = 0f;

    // Use this for initialization
    void Start ()
    {
        gun_ = GetComponent<SteamVR_TrackedObject>();
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (kBulletPrefab == null) return;
        Shoot();
        Reload();
        UpdateUi();
	}

    private void Reload()
    {
        float position_z = transform.position.z;
        if (transform.localRotation.eulerAngles.x <= -30f)
        {
            bullet_number_ = kMaxBullet;
        }
        previous_position_z_ = position_z;
    }

    private void Shoot()
    {
        if (gun_ == null || kBulletSpawner == null || bullet_number_ == 0) return;
        var device = SteamVR_Controller.Input((int)gun_.index);
        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            --bullet_number_;
            var bullet = Instantiate(kBulletPrefab);
            bullet.transform.position = kBulletSpawner.position;
            var rigidbody = bullet.GetComponent<Rigidbody>();
            rigidbody.AddForce(kBulletSpawner.right * -1f * 1000f);
        }
    }

    private void UpdateUi()
    {
        if (kText == null) return;
        kText.SetText(bullet_number_.ToString("00") + "/" + kMaxBullet.ToString("00"));
    }
}
