using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement pmov;

    public AudioSource jugador;

    public AudioClip correr;
    public AudioClip saltar;

    private bool corriendoSonando = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pmov = GetComponent<PlayerMovement>();

        // No es necesario configurar los estados en Start si ya están configurados en el Animator.
        // animator.SetBool("IsGrounded", true);
        // animator.SetBool("IsRunning", false);
        // animator.SetBool("IsJumping", false);
        // animator.SetBool("IsFalling", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Actualizar el estado "IsGrounded" basado en la función IsGrounded()
        animator.SetBool("IsGrounded", pmov.IsGrounded());

        // Reproducir sonido mientras se mueve
        if (pmov.moveInput != 0 && pmov.IsGrounded())
        {
            Debug.Log("Movimiento detectado, reproduciendo sonido de correr");
            if (!corriendoSonando)
            {
                corriendoSonando = true;
                jugador.clip = correr;
                jugador.loop = true;
                jugador.Play();
            }

            animator.SetBool("IsRunning", true);
        }
        else
        {
            corriendoSonando = false;
            animator.SetBool("IsRunning", false);

            if (pmov.IsGrounded())
            {
                jugador.Stop();
            }
        }

        // Reproducir sonido cuando salta
        animator.SetBool("IsJumping", pmov.jumping);
        if (Input.GetButtonDown("Jump") && pmov.jumping)
        {
            jugador.clip = saltar;
            jugador.loop = false;
            jugador.Play();
        }
        if (Input.GetButtonUp("Jump"))
        {
            jugador.Stop();
        }
    }
}