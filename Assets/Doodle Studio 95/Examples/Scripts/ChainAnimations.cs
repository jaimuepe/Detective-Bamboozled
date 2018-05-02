using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleStudio95;

namespace DoodleStudio95Examples {
[RequireComponent(typeof(DoodleAnimator))]
public class ChainAnimations : MonoBehaviour {

	public DoodleAnimationFile[] m_Animations;
	[Header("Settings")]
	public bool m_PlayOnStart = true;
	public bool m_Loop = true;

	void Start () {
		if (m_PlayOnStart)
			Play();
	}

	IEnumerator PlaySequence() {
		DoodleAnimator animator = GetComponent<DoodleAnimator>();
		int i = 0;
		while(i < m_Animations.Length) {
			// Set the new animation
			animator.ChangeAnimation(m_Animations[i]);
			// Play the animation and wait until it's finished
			yield return animator.PlayAndPauseAt();
			// Advanced to the next animation
			i++;
			// Loop if we've reached the end
			if (i >= m_Animations.Length && m_Loop)
				i = 0;
		}
		animator.Stop();
	}

	public void Play() {
		StopAllCoroutines();
		StartCoroutine(PlaySequence());
	}
}
}