using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPrefab : MonoBehaviour
{
    int itemType = 0;
    float moveSpeed = 2f;

    [SerializeField]
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, destroyTime);
        moveSpeed = Random.Range(0.5f, 1f) * moveSpeed;
        itemType = Random.Range(0, sprites.Length);
        GetComponent<Button>().image.sprite = sprites[itemType];
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonActions.gamePlay)
        {
            transform.GetComponent<RectTransform>().Translate(Vector3.down * moveSpeed);
            //Debug.Log("item : " + transform.GetComponent<RectTransform>().position.y);
            if(transform.GetComponent<RectTransform>().position.y <= 200)
            {
                ButtonActions.health -= 10;
                Destroy(gameObject);
            }
        }
    }

    public void onTouch()
    {
        int deltaScore = 0;
        switch (itemType)
        {
            case 0: deltaScore = 10; break; case 1: deltaScore = 20; break;   
        }
        ButtonActions.score += deltaScore;
        Destroy(gameObject);
    }
}
