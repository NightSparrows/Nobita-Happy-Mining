using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    List<GameObject> gameObjects = new List<GameObject>();

    void addObstacle()
    {
		GameObject obstacleObject = Instantiate(new GameObject(), new Vector3(Random.Range(-10, 10), 20, Random.Range(-10, 10)), Quaternion.identity);
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		obstacleObject.name = "Obstacle";
		sphere.transform.parent = obstacleObject.transform;
		sphere.transform.localPosition = Vector3.zero;
		Rigidbody rb = sphere.AddComponent<Rigidbody>();
		sphere.AddComponent<SphereCollider>();
		rb.mass = 1.0f;
        this.gameObjects.Add(obstacleObject);
	}

    // Start is called before the first frame update
    void Start()
    {
        for(uint i = 0; i < 100; i++)
        {
            addObstacle();
		}
    }

	static float time = 0;
	// Update is called once per frame
	void Update()
    {
        time += Time.deltaTime;

        if (time >= 2.0f)
        {
            int remove = Random.Range(0, this.gameObjects.Count);
            Destroy(this.gameObjects[remove]);
            this.gameObjects.RemoveAt(remove);
            this.addObstacle();
            time = 0;
        }
    }
}
