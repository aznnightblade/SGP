using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

    [SerializeField]
    float damageToPlayer = 10.0f;
	float playTime = 2.0f;

	// Update is called once per frame
	void Update () {
        if (playTime <= 0.0f)
            Destroy(transform.parent.gameObject);

		playTime -= Time.deltaTime * GameManager.CTimeScale;
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player")
            col.GetComponent<Player>().DamagePlayer((int)damageToPlayer);
    }
}
