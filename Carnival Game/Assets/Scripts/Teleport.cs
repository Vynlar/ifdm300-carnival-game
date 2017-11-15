using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour {

    public GameObject objToTeleport;
    public GameObject teleportTo;
    public BoxCollider2D switchBoundingBoxTo = null;
    public float fadeTime = 0.5f;

    // Image used to fade between scenes
    private Image fadeImage;

	// Use this for initialization
	void Start () {
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    // Teleports the object to this transform's position
    public void ActivateTeleport()
    {
        if (fadeImage != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        fadeImage.color = new Color(0, 0, 0, 0);

        PlayerController pController = objToTeleport.GetComponent<PlayerController>();

        if (pController)
        {
            pController.enabled = false;
        }


        float timer = 0;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, 1 * (timer / fadeTime));
            if(fadeImage.color.a >= 1)
            {
                fadeImage.color = new Color(0, 0, 0, 1);
            }
            yield return null;
        }

        if(pController)
        {
            pController.enabled = true;
        }

        // Fade back in
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        // We've finished fading, so teleport the player
        objToTeleport.transform.position = teleportTo.transform.position;

        // Then change the bounding box
        if (switchBoundingBoxTo != null)
        {
            CameraFollow cFollow = objToTeleport.GetComponent<CameraFollow>();

            if (cFollow)
            {
                cFollow.SetBoundingBox(switchBoundingBoxTo);
            }
        }

        float timeLeft = fadeTime;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, 1 * (timeLeft / fadeTime));
            if (fadeImage.color.a <= 0)
            {
                fadeImage.color = new Color(0, 0, 0, 0);
            }
            yield return null;
        }
    }

}
