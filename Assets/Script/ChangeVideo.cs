﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChangeVideo : MonoBehaviour {

    public VideoClip[] videoClips;

    private VideoPlayer videoPlayer;
    private int videoClipIndex;

    #region FIELDS
    public Image fadeOutUIImage;
    public float fadeSpeed = 0.8f;
    public enum FadeDirection
    {
        In, //Alpha = 1
        Out // Alpha = 0
    }
    #endregion
    #region MONOBHEAVIOR
   /* void OnEnable()
    {
        StartCoroutine(Fade(FadeDirection.Out));
    }*/
    #endregion
    #region FADE
    private IEnumerator Fade(FadeDirection fadeDirection)
    {
        float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;
        if (fadeDirection == FadeDirection.Out)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
            fadeOutUIImage.enabled = false;
        }
        else
        {
            fadeOutUIImage.enabled = true;
            while (alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
        }
    }
    #endregion
    #region HELPERS
    public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection)
    {
        yield return Fade(fadeDirection);
        //SceneManager.LoadScene(sceneToLoad);
    }
    private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
    }
    #endregion

    public IEnumerator FadeInAndOut()
    {
        StartCoroutine(Fade(FadeDirection.In));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Fade(FadeDirection.Out));
    }

void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(FadeInAndOut());
        videoPlayer.targetTexture.Release();
        videoPlayer.clip = videoClips[videoClipIndex];
    }

    public void PlayNewVideo()
    {
        StartCoroutine(FadeInAndOut());
        videoClipIndex++;
        videoPlayer.clip = videoClips[videoClipIndex];
    }

}