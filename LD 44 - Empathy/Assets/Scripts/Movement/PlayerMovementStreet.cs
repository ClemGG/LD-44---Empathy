using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
public class PlayerMovementStreet : MonoBehaviour
{
    public float speed;
    public GameObject flamme;

    private float moveInput;

    private Animator a;
    private PlayableDirector dir;
    private SpriteRenderer sr;
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        dir = GetComponent<PlayableDirector>();
        sr = GetComponent<SpriteRenderer>();
        t = transform;

        flamme.SetActive(false);
    }




    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        a.SetBool("isWalking", !Mathf.Approximately(moveInput, 0f));
        sr.flipX = moveInput < -.1f;
    }



    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        t.Translate(Vector3.right * moveInput * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("PNJ"))
        {

            if(!c.GetComponent<PNJs>().hasInteractionStarted)
                c.GetComponent<PNJs>().TriggerDialogue(0);
        }
        else
        {
            Counter.instance.GetEndingBasedOnPointsLeft();

            StartCoroutine(ReturnToMenu());
            moveInput = 0f;
            a.SetBool("isWalking", false);
            dir.Play();
            enabled = false;
        }
    }


    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds((float)GetComponent<PlayableDirector>().playableAsset.duration);
        SceneFader.instance.LoadSceneDirectly(PlayerPrefs.GetInt("Nobody") == 0 ? 2 : 4);
    }
}
