using UnityEngine;
using UnityEngine.EventSystems;

public class Shoot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player;
    public Weapons weapons_class;

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weapons_class = player.GetComponent<Weapons>();
    }
  
    public void OnPointerDown(PointerEventData eventData)
    {
        weapons_class.Shoot_S();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        weapons_class.Shoot_F();
    }
}
