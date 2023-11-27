using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSWeaponManager : MonoBehaviour
{
    public Player player;
    List<NSWeapon> weapons;
    // Start is called before the first frame update
    void Start()
    {
        this.weapons = new List<NSWeapon>(); 
        ///
        /// for testing
        /// 
        this.addWeapon(new NSTestWeapon());
        /// end
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addWeapon(NSWeapon weapon)
    {
        weapon.init(player);
        this.weapons.Add(weapon);
    }

	private void FixedUpdate()
	{
		foreach (NSWeapon weapon in this.weapons)
        {
            if (weapon.activated)
                weapon.updateWeapon(Time.deltaTime);
        }
	}
}
