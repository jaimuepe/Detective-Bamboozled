using DoodleStudio95;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {

    // Point to a UI element that covers the screen, with an animator
    public DoodleAnimator m_Animator;
    // Point to the transition animation you want to show
    public DoodleAnimationFile m_Transition;

    public AudioClip screenChangeClip;

    public string nextLevel;
    bool _inTransition = false;

    void Start()
    {
        m_Animator.Hide();
    }

    IEnumerator Transition()
    {
        _inTransition = true;

        AudioManager.instance.PlayClip(screenChangeClip);
        
        // Set the animation
        m_Animator.ChangeAnimation(m_Transition);
        m_Animator.Show();

        // Play the transition and wait
        yield return m_Animator.PlayAndPauseAt();

        // We can do anything while the player isn't looking.
        // Here we'll load the same scene, but we could also move the player somewhere else in the level
        yield return SceneManager.LoadSceneAsync(nextLevel);

        // Play the transition backwards (from the last frame to the first)
        yield return m_Animator.PlayAndPauseAt(-1, 0);


        // Hide the animator
        m_Animator.Hide();
        // m_Animator.gameObject.SetActive(false);

        _inTransition = false;
    }

    public void StartTransition()
    {
        if (_inTransition)
            return;

        // m_Animator.gameObject.SetActive(true);
        m_Animator.Show();

        m_Animator.StopAllCoroutines();
        m_Animator.StartCoroutine(Transition());
    }
}
