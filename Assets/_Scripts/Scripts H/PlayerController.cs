using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float horizontalSpeed = 5f;
    public float jumpSpeed = 5f;

    public List<ParticleSystem> explosionParticles;

    private Rigidbody rigidbody;
    private Animator animator;
    public List<GameObject> followingObjects;
    public Sprite[] eyeSprites;

    public AudioClip jumpSound;
    public AudioClip mainPlayerDead;
    public AudioClip minionAwake;
    public AudioClip minionDead;

    private Vector3 startScale;

    private void Start()
    {
        followingObjects = new List<GameObject>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        startScale = transform.lossyScale;

        StartCoroutine(BlinkTimer(transform));
      
        SpriteRenderer renderer = FindComponentInChildWithTag<SpriteRenderer>(transform, "Eye Holder");
        int eyeColorIndex = Random.Range(0, eyeSprites.Length + 1);
        if (eyeColorIndex != eyeSprites.Length)
            renderer.sprite = eyeSprites[eyeColorIndex];
        else
            renderer.enabled = false;


        ParticleSystem explosionParticle = GameObject.FindGameObjectWithTag("ExplosionParticle").GetComponent<ParticleSystem>();
        explosionParticle.gameObject.SetActive(false);
        explosionParticles.Add(explosionParticle);
    }

    private void Update()
    {
        Vector2 velocity = rigidbody.velocity;

        if (Input.GetKeyDown(KeyCode.UpArrow) || MobileInputController.isUpPressed)
        {
            velocity.y = jumpSpeed;
            animator.SetTrigger("Jump");
            PlayAudioC1ip(jumpSound);
            for (int i=0; i< followingObjects.Count; i++)
            {
                Rigidbody rb = followingObjects[i].GetComponent<Rigidbody>();
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || MobileInputController.isRightPressed)
            velocity.x = horizontalSpeed;
        else if (Input.GetKey(KeyCode.LeftArrow) || MobileInputController.isLeftPressed)
            velocity.x = -horizontalSpeed;

        rigidbody.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Main Menu");
        }
    }

    public void PlayAudioC1ip(AudioClip clip)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void AddSpringToObject(GameObject gameObject)
    {
        SpringJoint joint = gameObject.AddComponent<SpringJoint>();
        joint.connectedBody = rigidbody;
        joint.enableCollision = true;
        joint.autoConfigureConnectedAnchor = false;
        joint.damper = 0.01f;
        joint.minDistance = Random.Range(1.25f, 1.5f);
        joint.maxDistance = Random.Range(1.75f, 2f);
        //joint.damper = 0.5f;
        //joint.distance = Random.Range(0.5f, 1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            PlayParticleAtPoint(transform.position);
            PlayAudioC1ip(mainPlayerDead);
            //animator.SetTrigger("Death");
            if (followingObjects.Count > 0)
            {                
                StartCoroutine(ChangeMainPlayer());
            }
            else
            {
                animator.SetTrigger("Death");
                LevelFinishController.loadSameLevel = true;
                Collider[] colliders = GetComponentsInChildren<Collider>();
                for (int i = 0; i < colliders.Length; i++)
                    Destroy(colliders[i]);
            }

            //print("Death");
            //LevelFinishController.loadSameLevel = true;
            //StartCoroutine(ChangeMainPlayer());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Minion")
        {
            GameObject other = collision.gameObject;
            if (!followingObjects.Contains(other) && collision.GetComponent<SpringJoint>() == null)
            {
                SpriteRenderer renderer = FindComponentInChildWithTag<SpriteRenderer>(other.transform, "Eye Holder");
                int eyeColorIndex = Random.Range(0, eyeSprites.Length + 1);
                if (eyeColorIndex != eyeSprites.Length)
                    renderer.sprite = eyeSprites[eyeColorIndex];
                else
                    renderer.enabled = false;

                PlayAudioC1ip(minionAwake);
                other.GetComponent<Animator>().SetTrigger("Awake");
                StartCoroutine(BlinkTimer(other.transform));
                followingObjects.Add(other);
                AddSpringToObject(other);
            }
        }
    }

    IEnumerator ChangeMainPlayer()
    {
        Vector3 targetScale = startScale;

        int nextPlayerIndex = Random.Range(0, followingObjects.Count);
        GameObject nextPlayer = followingObjects[nextPlayerIndex];
        ChangeTransformValues(transform, nextPlayer.transform);

        nextPlayer.GetComponent<Rigidbody>().mass = 100f;
        nextPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        Collider[] colliders = nextPlayer.GetComponentsInChildren<Collider>();
        for (int i = 0; i < colliders.Length; i++)
            Destroy(colliders[i]);

        nextPlayer.GetComponent<Animator>().SetTrigger("Death");
        Destroy(nextPlayer.GetComponent<SpringJoint>());

        followingObjects.Remove(nextPlayer);

        RearrangeSpringDistances();
        Destroy(nextPlayer, 3f);

        while (Vector3.Distance(targetScale, transform.localScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        yield return null; 
    }

    public void PlayParticleAtPoint(Vector3 point)
    {
        ParticleSystem particle = null;
        for (int i=0; i<explosionParticles.Count; i++)
        {
            if (explosionParticles[i].isStopped)
            {
                particle = explosionParticles[i];
                break;
            }
        }

        if (particle == null)
        {
            particle = Instantiate(explosionParticles[0]);
            explosionParticles.Add(particle);
        }

        particle.gameObject.SetActive(true);
        particle.transform.position = point;
        particle.gameObject.SetActive(true);
        particle.Stop();
        particle.Play();
    }

    public void EnableParticle()
    {
        //explosionParticle.gameObject.SetActive(false);
    }

    IEnumerator BlinkTimer(Transform transform)
    {
        Collider[] colliders = transform.GetComponentsInChildren<Collider>();


        while(colliders.Length != 0) //(transform && transform.GetComponent<Collider>())
        {
            float blinkTime = Random.Range(4f, 6f);
            yield return new WaitForSeconds(blinkTime);

            if (colliders.Length != 0) //(transform)
            {
                transform.GetComponent<Animator>().SetTrigger("Blink");
            }

            if (Random.value > 0.5f)
            {
                yield return new WaitForSeconds(0.5f);
                if (colliders.Length != 0) //(transform)
                    transform.GetComponent<Animator>().SetTrigger("Blink");
            }
        }

        yield return null;
    }

    private void ChangeTransformValues( Transform transform1, Transform transform2)
    {
        Vector3 position1 = transform1.position;
        Quaternion rotation1 = transform1.rotation;
        Vector3 scale1 = transform1.localScale;

        transform1.position = transform2.position;
        transform1.rotation = transform2.rotation;
        transform1.localScale = transform2.localScale;

        transform2.position = position1;
        transform2.rotation = rotation1;
        transform2.localScale = scale1;
    }

    private void RearrangeSpringDistances()
    {
        for (int i = 0; i < followingObjects.Count; i++)
        {
            SpringJoint joint = followingObjects[i].GetComponent<SpringJoint>();
            joint.minDistance = Random.Range(1.25f, 1.5f);
            joint.maxDistance = Random.Range(1.75f, 2f);
        }
    }

    public T FindComponentInChildWithTag<T>(Transform parent, string tag) where T : Component
    {
        Transform[] transforms = parent.GetComponentsInChildren<Transform>();
        foreach (Transform _transform in transforms)
        {
            if (_transform.gameObject.tag == tag)
            {
                return _transform.GetComponent<T>();
            }
        }
        return null;
    }

}
