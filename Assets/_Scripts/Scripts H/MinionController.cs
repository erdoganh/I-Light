using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour {

    private bool isDeath = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle" && !isDeath)
        {
            isDeath = true;

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().followingObjects.Remove(this.gameObject);
            AudioClip minionDeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().minionDead;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayAudioC1ip(minionDeath);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayParticleAtPoint(transform.position);

            this.GetComponent<Animator>().SetTrigger("Death");

            Collider[] colliders = GetComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
                Destroy(colliders[i]);

            Destroy(this.GetComponent<SpringJoint>());
            Destroy(this.gameObject, 1f);
        }
    }
}
