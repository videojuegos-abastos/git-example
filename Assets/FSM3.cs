using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum State3 {
    Patrol,
    Chase,
}

public class FSM3 : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Rango al que el enemeigo se ha de acercar para matar al jugador")]
    float range;

    [SerializeField]
    [Tooltip("Estado actual")]
    State3 currentState;

    [SerializeField]
    [Tooltip("Puntos por los que patrullar")]
    Transform[] patrolPoints;

    NavMeshAgent agent; // Referencia al componente NavMeshAgent
    GameObject player; // Referencia al Jugador (GameObject)
    Transform nextPoint; // Siguiente punto de patrulla al que vamos a ir

    void Start()
    {
        // Obtenemos las referencias a los componentes que vamos a utilizar y al jugador. (Esto es muuy importante, hay que tenerlo clarísimo)
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Llamamos a la función de update del estado en el que estemos.
        switch (currentState) {

            case State3.Patrol:
                PatrolUpdate();
                break;
            case State3.Chase:
                ChaseUpdate();
                break;
        }

        // Este switch equivale a:
        /*

        if (currentState == State3.Patrol) {
            PatrolUpdate();
        } else if (currentState == State3.Chase) {
            ChaseUpdate();
        }

        // Si nos aclaramos más con los if, podemos comentar el switch y descomentar los ifs.

        */
        



    }

    // Función Update de nuestro estado Patrol (patrulla)
    void PatrolUpdate() {

        /* Comportamiento:

            Si no tenemos nextPoint asignamos uno aleatorio.
            Una vez tenemos nextPoint, comprobamos si hemos llegado o no.
            Si ya hemos llegado, asignamos otro y vamos, si no, seguimos yendo.

        */

        // True si tenemos nextPont, false si no.
        bool hasNextPoint = nextPoint != null;

        // Si no tenemos nextPont asignamos uno aleatorio
        if (!hasNextPoint) {
            SetRandomNextPointAndGo();
        }

        // Ahora que tenemos nextPoint, necesitamos comprobar si hemos llegado o no.
        // Si hemos llegado, elegiremos otro punto aleatorio para ir, si no, dejaremos que el Player siga yendo.

        // Para saber si hemos llegado comprobaremos la distancia entre nosotros y el jugador. Si esta es muuy cercana a cero, significa que hemos llegado.

        /*

            Una cosa a tener en cuenta es que el punto al que vamos está en el suelo mientras que nuestro transform.position está a media altura,
            si obtenemos la distancia entre estos dos puntos, nunca se aproximará a cero porque las coordenadas 'Y' no coinciden.

            Una forma de solucionar esto podría ser colocar los puntos a la misma altura que la posición del jugador, nosotros lo que haremos
            es ignorar el componente 'Y' utilizando vectores de 2 componentes y utilizando solo la 'X' y la 'Z'.
        */

        // Creamos los Vector2 con las componentes 'X' y 'Z'
        Vector2 myPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 pointPosition = new Vector2(nextPoint.position.x, nextPoint.position.z);
            
        // Comprobamos si la distancia se aproxima a cero. (float.Epsilon es un valor muuy cercano a cero), en concreto: 1.401298E-45, es decir 0. (43 ceros) 1.401298
        bool arrived = Vector2.Distance(myPosition, pointPosition) < float.Epsilon;

        // Si ya hemos llegado al nextPoint asignamos otro aleatorio
        if ( arrived ) {
            SetRandomNextPointAndGo();
        }


        // Condición de salida del estado
        if (Input.GetKeyDown(KeyCode.Space)) {
            nextPoint = null; // Antes de salir nos aseguramos de que nextPoint == null para que cuando volvamos a entrar al estado se asigne otro punto aleatorio a nextPonint
            currentState = State3.Chase;
    	}
    }

    // Escoje un punto aleatorio de nuesto Array / Lista de puntos y lo asigna a nextPonint. Además le dice al componente NavMeshAgent que vaya.
    void SetRandomNextPointAndGo() {
        int r = Random.Range(0, patrolPoints.Length);
        nextPoint = patrolPoints[r];
        agent.SetDestination(nextPoint.position);
    }

    // Función Update de nuestro estado Chase (persecución)
    void ChaseUpdate() {
        
        // Le decimos al componente NavMeshAgent que vaya a la posición del jugador.
        // Lo hacemos cada frame con lo que si el jugador se mueve, seguiremos yendo hacia él 
        agent.SetDestination(player.transform.position);

        // Obtenemos la distancia entre nosotros y el jugador
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Si la distancia es menor que un rango que hemos definido en el editor, destruimos al jugador y cambiamos de estado
        if (distance < range)
        {
            Destroy(player);
            currentState = State3.Patrol;
        }



        // Condición de salida del estado
        if (Input.GetKeyDown(KeyCode.Space)) {
            currentState = State3.Patrol;
    	}

    }
}
