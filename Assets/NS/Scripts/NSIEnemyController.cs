using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy interface
// Enemy does not self destroy managed by the enemy manager
public interface NSIEnemyController
{
	// initialize the controller with some useful object
	void initController(NSTileMapManager tileMapManager, Player player);

	bool isDeathAnimationOver();          // is the death animation done

	int expDrop();

}
