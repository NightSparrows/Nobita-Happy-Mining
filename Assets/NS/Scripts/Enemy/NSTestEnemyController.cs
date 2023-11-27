using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NSTestEnemyController : MonoBehaviour, NSIEnemyController
{
	private NSTileMapManager tileMapManager;
	private Player player;
	private Health health;
	private CharacterController characterController;


	// TODO add enemy status
	// walk to player, attacking, idling, sleeping, ... etc

	// movement para.
	private bool hasTarget = false;
	private float reachRadiis = 1f;
	private Vector3 targetPosition;

	// TODO move to enemy attribute
	private float moveSpeed = 10f;

	public bool isDeathAnimationOver()
	{
		return true;        // TODO 
	}
	public int expDrop()
	{
		return 10;
	}

	public void initController(NSTileMapManager tileMapManager, Player player)
	{
		this.tileMapManager = tileMapManager;
		this.player = player;
	}

	private void Awake()
	{
		this.health = GetComponent<Health>();
		this.characterController = GetComponent<CharacterController>();
	}

	// Start is called before the first frame update
	void Start()
	{

	}
	public Vector2Int FindNearestDiscretePoint(Vector2 continuousPoint, int n)
	{
		// 將連續位置的浮點數坐標轉換為最接近的整數點
		Vector2Int nearestPoint = new Vector2Int((int)Mathf.Round(continuousPoint.x / (float)n) * n, (int)Mathf.Round(continuousPoint.y / (float)n) * n);
		return nearestPoint;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void FixedUpdate()
	{

		/*
		//////////////////////////////////////////////////////////////////////////////////
		/// need to move to walk to player state
		//////////////////////////////////////////////////////////////////////////////////
		if (!this.hasTarget)
		{
			Vector3 toPlayerVector = this.player.transform.position - this.transform.position;
			if (toPlayerVector.magnitude < 2f)
			{
				// just move to player
				this.targetPosition = this.player.transform.position;
				this.hasTarget = true;
			}
			else
			{
				List<Vector2Int> pathToPlayer = this.tileMapManager.findPath(new Vector2(transform.position.x, transform.position.z), new Vector2(this.player.transform.position.x, this.player.transform.position.z), 100);
				if (pathToPlayer != null)
				{
					if (pathToPlayer.Count >= 2)
					{
						Vector2 moveDir = pathToPlayer[1];
						this.targetPosition = new Vector3(moveDir.x * NSTileController.Size, 0, moveDir.y * NSTileController.Size);
					}
					else if (pathToPlayer.Count == 1)
					{
						// just move to player
						this.targetPosition = this.player.transform.position;
					}
					this.hasTarget = true;
				}
				else
				{
					Debug.LogWarning("Path error");
				}
			}
		}
		else
		{
			Vector3 motion = this.targetPosition - this.transform.position;
			motion.y = 0;
			if (motion.magnitude < reachRadiis)
			{
				this.hasTarget = false;
			}
			motion.Normalize();
			motion *= this.moveSpeed;
			motion *= Time.deltaTime;
			this.characterController.Move(motion);
		}*/
		//////////////////////////////////////////////////////////////////////////////////
		/// END: need to move to walk to player state
		//////////////////////////////////////////////////////////////////////////////////
	}

	private void OnTriggerEnter(Collider other)
	{
		// hit by bullet
		if (other.tag == "PlayerBullet")
		{
			BulletController bulletController = other.gameObject.GetComponent<BulletController>();
			this.health.takeDamage(bulletController.getDamage());
			Debug.Log("hit by bullet");

			if (this.health.isDead())
			{
				// TODO exp drop
			}

		}
	}
}
