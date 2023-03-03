using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public float interactDistance = 2f;
    private bool canInteract = false;
    public int numberOfObjects = 15;

    public Animator animator;
    public GameObject stars;

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("OpenChest", true);
            StartCoroutine(SpawnStars());
        }
    }

    IEnumerator SpawnStars()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject star = Instantiate(stars, transform.position, transform.rotation);
            Rigidbody2D rb = star.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0f, 10f); // Set constant velocity upwards
            yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds before spawning the next star
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
