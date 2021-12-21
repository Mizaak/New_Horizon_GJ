using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPiece : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Interact"))
        {

            //communicate to game manager, use a counter, subract each time one of these gets triggered until reach 0 then minigame complete
            Destroy(this.gameObject);


        }

    }

}
