using UnityEngine;
using UnityEngine.EventSystems;

public class Reload : MonoBehaviour, IPointerDownHandler
{
    public Weapons weapons_class;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weapons_class = player.GetComponent<Weapons>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (weapons_class.anim.GetCurrentAnimatorStateInfo(0).IsName("pers_idle_pistol"))
        {
            weapons_class.Reload_Pistol();
        }

        if (weapons_class.anim.GetCurrentAnimatorStateInfo(0).IsName("pers_idle_shotgun"))
        {
            weapons_class.Reload_Shotgun();
        }
    }


}
