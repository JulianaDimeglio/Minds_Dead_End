using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptorBasico : MonoBehaviour
{
    public Transform interruptor;  
    public float anguloEncendido = -30f;
    public float anguloApagado = 0f;
    public float velocidadRotacion = 5f;

    private bool encendido = false;
    private Quaternion rotacionObjetivo;

    void Start()
    {
        rotacionObjetivo = Quaternion.Euler(anguloApagado, 0, 0);
        interruptor.localRotation = rotacionObjetivo;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            encendido = !encendido;
            rotacionObjetivo = Quaternion.Euler(encendido ? anguloEncendido : anguloApagado, 0, 0);
        }

        // Rotaciòn del interruptor para apagar luz
        interruptor.localRotation = Quaternion.Slerp(interruptor.localRotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);
    }
}
