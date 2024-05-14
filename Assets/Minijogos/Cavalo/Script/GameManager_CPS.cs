using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_CPS : MonoBehaviour
{
    [Header("Contador Timer")]
    public float Contador_inicial = 3f;
    public Text Contador_inicial_Texto;
    public Text Mensagem_Player_2;
    public float Contador_Timer_2 = 5f;
    [Header("Contador durante o Jogo")]
    public bool Tempo_Acabou;
    public Text Time_text;
    public float Time_Count = 60f;
    [Header("checador de clique")]
    public Text Canva_CPS_Player_1;
    public Text Canva_CPS_Player_2;
    public Text Canva_Contagem_Player_1;
    public Text Canva_Contagem_Player_2;
    public Text Canva_Player_Tanana_Ganhou;
    public int Contador;
    public float CPS_Player_1;
    public float CPS_Player_2;
    public bool Vez_Player_1 = false;
    public bool Vez_Player_2 = false;
    public bool Jogo_Comecou = false;
    public bool Jogo_Acabou = false;
    private int Vezes_que_o_tempo_acabou;
    [Header("Decidir quem ganhou quem perdeu")]
    public int Contagem_Player_1;
    public int Contagem_Player_2;
    public bool Peca_Player_1_ganhou;
    public bool Peca_Player_2_ganhou;
    public int numero_randomico;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    void Update()
    {
        if (Jogo_Acabou == false)
        {
            ComecarJogo();
        }
        else if (Jogo_Acabou = true)
        {
            Checar_Qual_Peca_Ganhou();
        }
    }

    void ComecarJogo()
    {
        if(Contador_inicial <= 0)
        {
            if (Jogo_Comecou == true)
            {
                time_Count();
            }
        }
    }

    public void Clique()
    {
        if (Jogo_Comecou == true)
        {
            Vez_Player_1 = true;
            if (Vez_Player_1 && Tempo_Acabou == false)
            {
                Contador++;
            }
            else if (Vez_Player_2 && Tempo_Acabou == false)
            {
                Contador++;
            }
        }
    }

    void time_Count()
    {
        if(Tempo_Acabou == false)
        {
            RestricaoDeTempo();
            if(Time_Count <= 0 && Vez_Player_1 == true)
            {
                Debug.Log("Acabou a vez do jogador 1");
                CPS_Player_1 = Contador / 60;
                Contagem_Player_1 = Contador;
                Canva_CPS_Player_1.text = CPS_Player_1.ToString(CPS_Player_1 + "Cliques por segundo");
                Canva_Contagem_Player_1.text = Contagem_Player_1.ToString();
                Contador = 0;
                Time_Count = 60;
                Vez_Player_1 = false;
                Jogo_Comecou = false;
                StartCoroutine(CountDownDois());
            }else if(Vez_Player_2 == true && Time_Count <=0)
            {
                Debug.Log("Acabou a vez do jogador 2");
                CPS_Player_2 = Contador / 60;
                Contagem_Player_2 = Contador;
                Canva_CPS_Player_2.text = CPS_Player_1.ToString(CPS_Player_2 + " Cliques por segundo");
                Canva_Contagem_Player_2.text = Contagem_Player_2.ToString();
                Contador = 0;
                Jogo_Comecou = false;
                Jogo_Acabou = true;
                Vez_Player_2 = false;
            }
        }
    }

    void RestricaoDeTempo()
    {
        if (Jogo_Comecou == true)
        {
                Time_Count -= Time.deltaTime;
                Time_text.text = Time_Count.ToString("F0");
        }
    }

    IEnumerator CountdownToStart()
    {
        Contador_inicial = 5f;
        while (Contador_inicial > 0)
        {
            Contador_inicial_Texto.text = Contador_inicial.ToString("F0");

            yield return new WaitForSeconds(1f);

            Contador_inicial--;
        }
        if (Vezes_que_o_tempo_acabou == 0)
        {
            Vez_Player_1 = true;
            Jogo_Comecou = true;
        }else if(Vezes_que_o_tempo_acabou == 1)
        {
            Vez_Player_2 = true;
        }
        Contador_inicial_Texto.gameObject.SetActive(false);
    }
    IEnumerator CountDownDois()
    {
        Contador_Timer_2 = 10f;
        Contador_inicial_Texto.gameObject.SetActive(true);
        Mensagem_Player_2.gameObject.SetActive(true);
        while(Contador_Timer_2 >= 0)
        {

            Contador_inicial_Texto.text = Contador_Timer_2.ToString("F0");
            yield return new WaitForSeconds(1f);
            Contador_Timer_2--;
        }
        Contador_inicial_Texto.gameObject.SetActive(false);
        Mensagem_Player_2.gameObject.SetActive(false);
        Jogo_Comecou = true;
        Vez_Player_2 = true;
    }
    void Checar_Qual_Peca_Ganhou()
    {
        if (Jogo_Acabou == true)
        {
            if (CPS_Player_1 > CPS_Player_2)
            {
                if (Contagem_Player_1 > Contagem_Player_2)
                {
                    Peca_Player_1_ganhou = true;
                }
            }
            else if (CPS_Player_1 < CPS_Player_2)
            {
                if (Contagem_Player_1 < Contagem_Player_2)
                {
                    Peca_Player_2_ganhou = true;
                }
            }
            else if (CPS_Player_1 == CPS_Player_2)
            {
                if (Contagem_Player_1 > Contagem_Player_2)
                {
                    Peca_Player_1_ganhou = true;
                }
                else if (Contagem_Player_1 < Contagem_Player_2)
                {
                    Peca_Player_2_ganhou = true;
                }
                else
                {
                    GerarNumeroAleatorio();
                    if(numero_randomico == 1)
                    {
                        Peca_Player_1_ganhou = true;
                    }
                    else
                    {
                        Peca_Player_2_ganhou = true;
                    }
                }
            }
        }

        if (Peca_Player_1_ganhou == true)
        {
            Canva_Player_Tanana_Ganhou.text = "dadada";
        }else if(Peca_Player_2_ganhou == true)
        {
            Canva_Player_Tanana_Ganhou.text = "papapa";
        }
    }
    void GerarNumeroAleatorio()
    {
        while (numero_randomico == 0)
        {
            numero_randomico = Random.Range(1, 3);
        }
    }
}
