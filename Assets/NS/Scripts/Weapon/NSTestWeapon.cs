using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NSEnemyManager;

public class NSTestWeapon : NSWeapon
{
	public const float coolDown = 2.5f;

	private GameObject testBulletPrefab;
	private Player player;

	private float m_currentCoolDown = 0f;

	public override void init(Player player)
	{
		this.player = player;
		testBulletPrefab = Resources.Load<GameObject>("NS/Prefabs/Bullet/NSTestBulletPrefab");
		if (this.testBulletPrefab == null)
		{
			Debug.LogError("Failed to load test bullet prefab");
		}
	}

	public override void updateWeapon(float dt)
	{
		base.updateWeapon(dt);

		this.m_currentCoolDown += dt;
		if (this.m_currentCoolDown >= coolDown)
		{
			for(int i = 0; i < 6; i++)
			{

				GameObject bullet = Object.Instantiate(this.testBulletPrefab, this.player.transform.position, Quaternion.identity, null);
				bullet.name = "NSTestBullet";
				BulletController bulletController = bullet.GetComponent<BulletController>();
				bulletController.init();
				float angle = 2f * Mathf.PI / 6f * (float)i;
				float speed = 8f;
				bulletController.m_velocity = new Vector3(Mathf.Cos(angle) * speed, 0, Mathf.Sin(angle) * speed);
				bulletController.m_lifeTime = 10f;
				bulletController.m_damage = 30;
				bulletController.m_triggerCount = 1;
			}
			this.m_currentCoolDown = 0;
		}

	}
}
