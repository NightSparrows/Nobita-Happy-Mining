using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningPickaxe : MonoBehaviour
{
    public ParticleSystem m_miningPS;
    private Transform m_transform;
    private GameObject m_pickaxeGameModel;
    private float m_currentAnimationTime = 0;           // 0 to 1.0
    private float m_currentAnimationSpeed = 10f;       // 0.5 animation in 1 second

    // start the mining animation
    public void startMining()
	{
		this.m_pickaxeGameModel.SetActive(true);
	}

    public void stopMining()
    {
		this.m_pickaxeGameModel.SetActive(false);
		this.m_currentAnimationTime = 0;
	}

    // Start is called before the first frame update
    void Start()
    {
        Transform pickaxeTransform = this.transform.Find("PickaxeModel");
        if (pickaxeTransform == null) {
            Debug.LogError("Failed to find pickaxe model");
        }
        this.m_pickaxeGameModel = pickaxeTransform.gameObject;
		this.m_pickaxeGameModel.SetActive(false);
        this.m_miningPS.Stop();

	}

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = this.m_pickaxeGameModel.transform.localRotation;
        rotation.y = Mathf.Sin(Mathf.PI * this.m_currentAnimationTime);

        this.m_pickaxeGameModel.transform.localRotation = rotation;

    }
	private void FixedUpdate()
	{
		this.m_currentAnimationTime += Time.deltaTime * this.m_currentAnimationSpeed;

		while (this.m_currentAnimationTime > 1.0f)
		{
            this.m_miningPS.Emit((int)(6f * this.m_currentAnimationSpeed));
			this.m_currentAnimationTime -= 1.0f;
		}
	}
}
