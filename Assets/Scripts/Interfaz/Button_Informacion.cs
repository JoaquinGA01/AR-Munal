using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Informacion : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject vista;
    public Sprite informacion;
    public Sprite Escaneo;
    private bool accion;
    private Animator animator;
    private Image image;


    void Start()
    {
        animator = vista.GetComponent<Animator>();
        image =vista.GetComponent<Image>();
        accion = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ver_Informacion(){
        if(accion){
            image.sprite = informacion;
            animator.Play("Quieto");
            accion = false;
        }else{
            image.sprite = Escaneo;
            animator.Play("Animacion Escanear");
            accion = true;
        }
    }
}
