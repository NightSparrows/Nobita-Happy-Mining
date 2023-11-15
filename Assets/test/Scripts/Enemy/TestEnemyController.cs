using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyController : MonoBehaviour, IEnemyController
{
	private TileMapManager tileMapManager;
	private Player player;
	private Health health;
	private CharacterController characterController;

	// TODO move to enemy attribute
	private float moveSpeed = 10f;
	public int dealDamage()
	{
		return 20;
	}

	public bool deathAnimationOver()
	{
		return true;		// TODO 
	}

	public void initController(TileMapManager tileMapManager, Player player)
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
		Vector2Int gridPosition = FindNearestDiscretePoint(new Vector2(transform.position.x, transform.position.z), TileController.Size);
		gridPosition /= TileController.Size;

		Vector2Int playerGridPosition = FindNearestDiscretePoint(new Vector2(this.player.transform.position.x, this.player.transform.position.z), TileController.Size);
		playerGridPosition /= TileController.Size;

		List<Vector2Int> pathToPlayer = this.tileMapManager.findPath(gridPosition, playerGridPosition, 5000);
		if (pathToPlayer != null && pathToPlayer.Count >= 2)
		{
			if (pathToPlayer.Count >= 2)
			{
				Vector2 moveDir = pathToPlayer[1];
				Vector3 motion = new Vector3((moveDir.x * TileController.Size) - transform.position.x, 0, (moveDir.y * TileController.Size) - transform.position.z);
				motion.Normalize();
				motion *= this.moveSpeed;
				motion *= Time.deltaTime;
				this.characterController.Move(motion);
			}
			else if (pathToPlayer.Count == 1)
			{
				// just move to player
				Vector3 motion = new Vector3(this.player.transform.position.x - transform.position.x, 0, this.player.transform.position.z - transform.position.z);
				motion.Normalize();
				motion *= this.moveSpeed;
				motion *= Time.deltaTime;
				this.characterController.Move(motion);
			}
		}
		else
		{
			Debug.LogWarning("Path error");
			Debug.LogWarning("Player grid position: " + playerGridPosition);
		}
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
