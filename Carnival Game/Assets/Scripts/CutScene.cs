using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CutScene : MonoBehaviour {

    public Texture2D[] mSprites;
    private Texture2D currentSprite;
    private int counter;
    public float switchTime = 0.5f;

    public Rect r_Sprite;

    void Start()
    {
        counter = 0;
        StartCoroutine("SwitchSprite");
    }

    void OnGUI()
    {
        GUI.DrawTexture(r_Sprite, currentSprite);
    }

    private IEnumerator SwitchSprite()
    {

        if (counter < mSprites.Length)
        {
            currentSprite = mSprites[counter];
            counter++;
        }
        else
        {
            SceneManager.LoadScene("SideScrollerPrototype");
        }

        yield return new WaitForSeconds(switchTime);
        StartCoroutine("SwitchSprite");
    }
}
