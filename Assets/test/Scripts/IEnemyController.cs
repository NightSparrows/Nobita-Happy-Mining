using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy does not self destroy managed by the enemy manager
public interface IEnemyController
{
	// initialize the controller with some useful object
	void initController(TileMapManager tileMapManager, Player player);

	int dealDamage();

	bool deathAnimationOver();			// is the death animation done

}
