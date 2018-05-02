using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class InputSystem : MonoBehaviour
{
    Player player;
    Transform mainCamera;
    PathfindingParticles[] _particles;
    bool olfateando = false;

    VignetteAndChromaticAberration vignette;

    float sniffCountDown = 0f;

    public float sniffDelayMin = 2f;
    public float sniffDelayMax = 4f;

    AudioSource sniffAudioSource;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        mainCamera = Camera.main.transform;
        _particles = FindObjectsOfType<PathfindingParticles>();

        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].gameObject.SetActive(false);
        }
        vignette = mainCamera.GetComponent<VignetteAndChromaticAberration>();
        sniffAudioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        HandlePlayerMovement();
        HandlePlayerActions();

        if (Input.GetButtonDown("Pause"))
        {
            UIManager.instance.ShowHidePausePanel();
        }

        if (olfateando)
        {
            sniffCountDown -= Time.deltaTime;
            if (sniffCountDown <= 0f)
            {
                sniffCountDown = Random.Range(sniffDelayMin, sniffDelayMax);
                if (sniffAudioSource)
                {
                    sniffAudioSource.Play();
                }
            }
        }
    }

    private void HandlePlayerActions()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            if (InteractionManager.Instance.PlayingInteraction)
            {
                InteractionManager.Instance.HandleInput();
            }
            else
            {
                player.ReqInteract();
            }
        }

        // olfateando
        if (Input.GetButtonDown("Fire2"))
        {
            if (olfateando == false)
            {
                Debug.Log("Estoy olfateando");
                UIManager.instance.ShowHideSmellsPanel(true);
                foreach (PathfindingParticles particle in _particles)
                {
                    particle.gameObject.SetActive(true);
                }

                if (applyVignetteCoroutine != null)
                {
                    StopCoroutine(applyVignetteCoroutine);
                }
                applyVignetteCoroutine = StartCoroutine(ApplyVignetteEffect());

                olfateando = true;
            }
            else
            {
                Debug.Log("Dejo de olfatear");
                UIManager.instance.ShowHideSmellsPanel(false);
                foreach (PathfindingParticles particle in _particles)
                {
                    particle.gameObject.SetActive(false);
                    foreach (Transform t in particle.transform)
                    {
                        Destroy(t.gameObject);
                    }
                }

                if (applyVignetteCoroutine != null)
                {
                    StopCoroutine(applyVignetteCoroutine);
                }
                applyVignetteCoroutine = StartCoroutine(StopVignetteEffect());

                olfateando = false;
            }
        }
    }

    private void HandlePlayerMovement()
    {
        Vector3 direction = Vector3.zero;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 cameraRight = mainCamera.right;
        Vector3 cameraFront = Vector3.Cross(cameraRight, Vector3.up);

        if (h != 0)
        {
            direction.x = h * cameraRight.x;
        }

        if (v != 0)
        {
            direction.z = v * cameraFront.z;
        }

        player.ReqMove(direction.normalized);
    }

    Coroutine applyVignetteCoroutine;

    IEnumerator ApplyVignetteEffect()
    {
        float startValue = vignette.intensity;

        float targetTime = (1 + Mathf.Abs(startValue)) / 2;

        float elapsedTime = 0f;

        while (elapsedTime < targetTime)
        {
            startValue = Mathf.Lerp(startValue, 0.4f, elapsedTime / targetTime);
            elapsedTime += Time.deltaTime;

            vignette.intensity = startValue;

            yield return null;
        }

        vignette.intensity = 0.4f;
    }

    IEnumerator StopVignetteEffect()
    {
        float startValue = vignette.intensity;

        float targetTime = (1 + Mathf.Abs(startValue)) / 2;

        float elapsedTime = 0f;

        while (elapsedTime < targetTime)
        {
            startValue = Mathf.Lerp(startValue, 0.0f, elapsedTime / targetTime);
            elapsedTime += Time.deltaTime;

            vignette.intensity = startValue;

            yield return null;
        }

        vignette.intensity = 0.0f;
    }
}
