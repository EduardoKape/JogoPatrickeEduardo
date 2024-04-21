using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static int pontos = 0;
    [SerializeField]
    private static TMP_Text texto;
    private static int vidas = 3;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("TextoPontos");
        texto = go.GetComponent<TMP_Text>();
    }

    void Update()
    {

    }

    public static void setPontos(int ponto)
    {
        if (ponto == 0)
        {
            pontos = 0;
        }
        else
        { 
            pontos += ponto;
        }
        texto.text = "Pontos: " + pontos.ToString();
    }

    public static void perdeVida()
    {
        vidas--;
        if (vidas <= 0)
        {
            //Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }
    }

    public static void GanhouJogo()
    {
        SceneManager.LoadScene("GameOver");
    }

}
