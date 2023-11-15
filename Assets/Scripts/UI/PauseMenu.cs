using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ---------- Pause Menu ----------
 * 
 *      Ref: https://www.youtube.com/watch?v=ROwsdftEGF0
 *           https://www.youtube.com/watch?v=JivuXdrIHK0
 *           
 *      How to Give a Scene Pause Menu?
 *          1. Add Prefabs/UI/PauseMenu.pefab under Canvas
 *          2. Add Scripts/UI/PauseMenu.cs TO Canvas (not to PauseMenu)
 *          3. Grab PauseMenu (the game object) to 'Pause Menu UI' of the script in step 2.
 *          
 *      What DO actually paused?
 *          1. Anything depends on Time.deltaTime
 *          2. `FixedUpdate()`
 *          3. Co-routine, e.g. `WaitForSeconds(1);`
 *          4. Build-in time-based function, e.g. `Destroy(gameObject, 1);`
 *      
 *      What do NOT paused?
 *          1. `Update()`
 *          2. Audios
 *          3. `unscaled` series in `Time` library
 *          4. `Input`
 *        
 *      How to know it is currently be paused?
 *          `PauseMenu.IsPaused` where PauseMenu is in Canvas
 *        
 *      NOTE:
 *          So `if (Input...) { DoSth(); }` is processed in `Update();`
 *          Fix it with `if (Input) { if (PauseMenu.IsPaused) {DoSth(); } }`
 */

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused = false;

    [SerializeField] private GameObject _pauseMenuUI;
    public bool IsPaused
    {
        get
        {
            return _isPaused;
        }

        set
        {
            _pauseMenuUI.SetActive(value);
            Time.timeScale = value ? 0f : 1f;
            
            _isPaused = value;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
        }
    }

    public void Resume()
    {
        IsPaused = false;
    }

    public void Back2Menu()
    {
        Debug.Log("TODO: Back to Menu");
    }

    public void OtherThings()
    {
        Debug.Log("TODO: Other things...");
    }
}
