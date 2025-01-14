using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(50 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject effect = ObjectPool.instance.GetPooledObject();
            if (effect != null)
            {
                effect.transform.position = transform.position;
                effect.transform.rotation = effect.transform.rotation;
                effect.SetActive(true);
            }
            FindObjectOfType<AudioManager>().playsound("PickUpCoins");
            //playermanager.numberofCoins += 1;
            // Debug.Log("Coins:"+playermanager.numberofCoins);
            gameObject.SetActive(false);
        }
    }
}
