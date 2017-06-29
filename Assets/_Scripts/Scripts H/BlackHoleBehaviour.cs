using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBehaviour : MonoBehaviour {

    public float maxForceAmunt = 10f;
    private List<GameObject> objectsInTrigger = new List<GameObject>();

	void Update () {
        for (int i=0; i<objectsInTrigger.Count; i++)
        {
            Vector3 forceVector = transform.position - objectsInTrigger[i].transform.position;
            float forceAmount = forceVector.magnitude;
            if (forceAmount < 1f) forceAmount = 1f;
            if(objectsInTrigger[i] != null)
                objectsInTrigger[i].GetComponent<Rigidbody>().AddForce((forceVector.normalized) * (maxForceAmunt / forceAmount), ForceMode.Force);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
            objectsInTrigger.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(objectsInTrigger.Contains(other.gameObject))
            objectsInTrigger.Remove(other.gameObject);
    }

}
