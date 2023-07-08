using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward_system : MonoBehaviour
{

    public Text Star_Number;
    public int Total_Stars;
    public GameObject contenedor_Recompensas;

    // Start is called before the first frame update
    void Start()
    {
        ConteoDeEstrellas(Total_Stars);
        cargar_Recompensas();
    }

    void cargar_Recompensas()
    {
        // Obtén una referencia al GameObject del que deseas iterar sus hijos
        GameObject objetoPadre = contenedor_Recompensas;

        // Itera a través de los hijos del objeto padre
        for (int i = 0; i < objetoPadre.transform.childCount; i++)
        {

            Transform hijo = objetoPadre.transform.GetChild(i);

            // Comprueba si el hijo tiene un hijo con un nombre específico
            Transform hijoBuscado = hijo.Find("TARJETA");


            Total_Stars = 0;
            for (int j = 0; j < 7; j++)
            {
                Total_Stars += PlayerPrefs.GetInt("Lv" + j);
            }
            Debug.Log("Estrellas: " + Total_Stars.ToString());


            if (hijoBuscado != null)
            {
                // Hacer algo con el hijo que tiene el hijo buscado
                Debug.Log("INDICE ACTUAL: "  + hijo.name + i.ToString());

                Text textoRecompensa = hijoBuscado.GetComponent<Text>();
                if (i == 0)
                {
                    if (Total_Stars >= 3)
                    {
                        textoRecompensa.text = "Recompensa 1";
                    }
                }

                if (i == 1)
                {
                    if (Total_Stars >= 6)
                    {
                        textoRecompensa.text = "Recompensa 2";
                    }
                }

                if (i == 2)
                {
                    if (Total_Stars >= 9)
                    {
                        textoRecompensa.text = "Recompensa 3";
                    }
                }

                if (i == 3)
                {
                    if (Total_Stars >= 12)
                    {
                        textoRecompensa.text = "Recompensa 4";
                    }
                }
            }
        }
    }
    void Update()
    {
        ConteoDeEstrellas(Total_Stars);
        

    }

    public void ConteoDeEstrellas(int Total_Stars1)
    {
        Total_Stars1 = 0;
        for (int i = 0; i < 7; i++)
        {
            Total_Stars1 += PlayerPrefs.GetInt("Lv" + i);
        }
        Debug.Log("Estrellas: " + Total_Stars1.ToString());
       
    }
}
