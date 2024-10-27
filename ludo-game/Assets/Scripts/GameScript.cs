using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Disclaimer: 
Por enquanto, estou trabalhando com essa quantidade gigantesca de objetos sem ser em array, o que pode complicar o projeto no futuro e não ideal para trabalhar.
Primeiro vou deixar de pé, depois quero ver se consigo consertar isso:

Não é fácil, pois temos que descobrir um jeito de assinalar X a 1, 2, 3, 4 de alguma forma.

*/

public class GameScript : MonoBehaviour{

    // Se total é igual a 1 temos 3 peças no tabuleiro da casa de cor. Vai armazenar quanto falta para acabar.
    private int totalvermelhocasa, totalverdecasa, totalazulcasa, totalamarelocasa;

    //Borda da casa inteira, mostrando que é o turno do jogador. Tem que criar os objetos.
    public GameObject frame_vermelho, frame_verde, frame_amarelo, frame_azul;

    //Bordas para mostrar que a peça está disponível para jogo. Temos que criar os objetos ainda. 
    public GameObject borda_peca_vermelho_1, borda_peca_vermelho_2, borda_peca_vermelho_3, borda_peca_vermelho_4;
    public GameObject borda_peca_verde_1, borda_peca_verde_2, borda_peca_verde_3, borda_peca_verde_4;
    public GameObject borda_peca_amarelo_1, borda_peca_amarelo_2, borda_peca_amarelo_3, borda_peca_amarelo_4;
    public GameObject borda_peca_azul_1, borda_peca_azul_2, borda_peca_azul_3, borda_peca_azul_4;  

    //Vetores para assinalar a posição das peças do tabuleiro, vinculadas a algum dos objetos já inseridos internamente

    public Vector3 posicao_peca_vermelho_1, posicao_peca_vermelho_2, posicao_peca_vermelho_3, posicao_peca_vermelho_4;
    public Vector3 posicao_peca_verde_1, posicao_peca_verde_2, posicao_peca_verde_3, posicao_peca_verde_4;
    public Vector3 posicao_peca_amarelo_1, posicao_peca_amarelo_2, posicao_peca_amarelo_3, posicao_peca_amarelo_4;
    public Vector3 posicao_peca_azul_1, posicao_peca_azul_2, posicao_peca_azul_3, posicao_peca_azul_4;

//Responsável pela animação do dado. Não criei a animação dentro do unity ainda, só assinalei as imagens.
    public Transform rodar_dado;

//Assinalar o dado a qualquer uma das posicoes de jogador. Podemos tirar isso, acho que fica muito poluído.

    public Transform dado_vermelho_rolagem, dado_verde_rolagem, dado_azul_rolagem, dado_amarelo_rolagem;

//Botao que faz que o dado só funcione para o jogador clickar quando for devido.
    public Button botaodado;

    //Determina quando ou não podemos selecionar as peças de cada cor para jogar.
    public Button botao_jogador_vermelho_1, botao_jogador_vermelho_2,botao_jogador_vermelho_3, botao_jogador_vermelho_4;
    public Button botao_jogador_verde_1, botao_jogador_verde_2, botao_jogador_verde_3, botao_jogador_verde_4;
    public Button botao_jogador_amarelo_1, botao_jogador_amarelo_2, botao_jogador_amarelo_3 , botao_jogador_amarelo_4;
    public Button botao_jogador_azul_1, botao_jogador_azul_2, botao_jogador_azul_3, botao_jogador_azul_4;

//Vai cobrir cada canto de jogador com alguma coisa que mostre que ele venceu, pode ser um minimapa de ludo, um quadrado dizendo venceu, qualquer coisa.
    public GameObject vitoria_azul, vitoria_verde, vitoria_amarelo, vitoria_vermelho;

   /* 

   ----------DISCLAIMER--------
   ATIVA DEPOIS, só para não ficar com flag de warning no unity
   */
   private string turno_jogador = "amarelo";
    private string jogador_atual = "none";
    private string nome_jogador_atual = "none";

//Responsável pelo movimento das peças. Após todas as etapas de movimento anterior. Da para escolher qual peça jogar, falei merda.

    public GameObject jogador_vermelho_1, jogador_vermelho_2, jogador_vermelho_3, jogador_vermelho_4;
    public GameObject jogador_verde_1, jogador_verde_2, jogador_verde_3, jogador_verde_4;
    public GameObject jogador_amarelo_1, jogador_amarelo_2, jogador_amarelo_3, jogador_amarelo_4;
    public GameObject jogador_azul_1, jogador_azul_2, jogador_azul_3, jogador_azul_4;




//vai armazenar a posição de onde está cada peça

    private int local_peca_vermelho_1, local_peca_vermelho_2, local_peca_vermelho_3, local_peca_vermelho_4;
    private int local_peca_verde_1, local_peca_verde_2, local_peca_verde_3, local_peca_verde_4;
    private int local_peca_amarelo_1, local_peca_amarelo_2, local_peca_amarelo_3, local_peca_amarelo_4;
    private int local_peca_azul_1, local_peca_azul_2, local_peca_azul_3, local_peca_azul_4;


    //Animações do dado abaixo
    private int selecionar_animacao_dado;

    public GameObject animacao_dado_1, animacao_dado_2, animacao_dado_3, animacao_dado_4, animacao_dado_5, animacao_dado_6;

    // Aqui associamos cada uma das posições do tabuleiro para cada um dos jogadores a uma lista que podemos associar a valores do dado.

    public List<GameObject> bloco_movimento_vermelho = new List<GameObject>();
    public List<GameObject> bloco_movimento_verde = new List<GameObject>();
    public List<GameObject> bloco_movimento_azul = new List<GameObject>();
    public List<GameObject> bloco_movimento_amarelo = new List<GameObject>();

    private System.Random randNo;
    //Tela do Deseja sair? sim/nao
    public GameObject tela_sair;

    //O jogo acabou uhu
    public GameObject tela_fim_jogo;


    //Valores aleatórios de 1 a 6 para o dado :);
    private System.Random randno;



//métodos de entrada e saída do jogo
    public void SairJogo () 
    {
    tela_sair.SetActive(true);
    }
//TROCAR O NOME PARA MENUPRINCIPAL OU A CENA QUE VOU USAR
    public void Confirmarsair()
    {
    SceneManager.LoadScene("main menu");
    }   

//Método para ativar a tela de jogo completo após 1.5 segundos (vamos executar dentro de outro void)

    IEnumerator jogocompletorotina ()
    {

    yield return new WaitForSeconds (1.5f);
    tela_fim_jogo.SetActive(true);
    } 


//Sim quero jogar de novo
    public void simjogoconcluido ()
    {   
    SceneManager.LoadScene("Ludo");
    }

//Nao quero jogar de novo
    public void naojogoconcluido ()
    {
    SceneManager.LoadScene("Main menu");
    }
//FUNCIONA EM CONJUNTO COM A FUNCAO DE INICIALIZAR DADO.




/*
██████░██████▄░██████░▄█████░██████░▄████▄░██░░░░░██████░███████░▄████▄░█████▄░░░██████▄░▄████▄░██████▄░▄█████▄
░░██░░░██░░░██░░░██░░░██░░░░░░░██░░░██░░██░██░░░░░░░██░░░░░░░░██░██░░██░██░░██░░░██░░░██░██░░██░██░░░██░██░░░██
░░██░░░██░░░██░░░██░░░██░░░░░░░██░░░██░░██░██░░░░░░░██░░░▄█████▀░██░░██░█████▀░░░██░░░██░██░░██░██░░░██░██░░░██
░░██░░░██░░░██░░░██░░░██░░░░░░░██░░░██████░██░░░░░░░██░░░██░░░░░░██████░██░░██░░░██░░░██░██████░██░░░██░██░░░██
██████░██░░░██░██████░▀█████░██████░██░░██░██████░██████░███████░██░░██░██░░██░░░██████▀░██░░██░██████▀░▀█████▀
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░

*/

   private void inicializardado ()
{   
        botaodado.interactable = true;
        animacao_dado_1.SetActive(false);
        animacao_dado_2.SetActive(false);
        animacao_dado_3.SetActive(false);
        animacao_dado_4.SetActive(false);
        animacao_dado_5.SetActive(false);
        animacao_dado_6.SetActive(false);

        /*


           ███████╗ ██╗ ███╗░░░███╗   ██████╗░ ░█████╗░   ░░░░░██╗ ░█████╗░ ░██████╗░ ░█████╗░
           ██╔════╝ ██║ ████╗░████║   ██╔══██╗ ██╔══██╗   ░░░░░██║ ██╔══██╗ ██╔════╝░ ██╔══██╗
           █████╗░░ ██║ ██╔████╔██║   ██║░░██║ ██║░░██║   ░░░░░██║ ██║░░██║ ██║░░██╗░ ██║░░██║
           ██╔══╝░░ ██║ ██║╚██╔╝██║   ██║░░██║ ██║░░██║   ██╗░░██║ ██║░░██║ ██║░░╚██╗ ██║░░██║
           ██║░░░░░ ██║ ██║░╚═╝░██║   ██████╔╝ ╚█████╔╝   ╚█████╔╝ ╚█████╔╝ ╚██████╔╝ ╚█████╔╝
           ╚═╝░░░░░ ╚═╝ ╚═╝░░░░░╚═╝   ╚═════╝░ ░╚════╝░   ░╚════╝░ ░╚════╝░ ░╚═════╝░ ░╚════╝░




         */
        

            

        switch (MainMenuManager.howManyPlayers)
        {
          case 2:

          if(totalamarelocasa > 3)
          {
            SoundManager.winAudioSource.Play();
            vitoria_amarelo.SetActive(true);
            StartCoroutine("GameCompleteRoutine");
            turno_jogador = "none";            
          }
          if(totalverdecasa > 3)
          {
            SoundManager.winAudioSource.Play();
            vitoria_verde.SetActive(true);
            StartCoroutine("GameCompleteRoutine");
            turno_jogador = "none";            
          }
          break;

          case 3:


          if(totalamarelocasa > 3 && totalverdecasa < 4 && totalvermelhocasa < 4 && turno_jogador == "amarelo")
          {
            if(!vitoria_amarelo.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_amarelo.SetActive(true);
            Debug.Log("jogador amarelo venceu");
          
            turno_jogador = "verde";
                     
          }
          if(totalverdecasa > 3 && totalamarelocasa < 4 && totalvermelhocasa < 4 && turno_jogador == "verde")
          {
            if(!vitoria_verde.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_verde.SetActive(true);
            Debug.Log("jogador verde venceu");
            
            turno_jogador = "vermelho";
                     
          }
        
          if(totalvermelhocasa > 3 && totalamarelocasa < 4 && totalvermelhocasa < 4 && turno_jogador == "vermelho")
          {
            if(!vitoria_vermelho.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_vermelho.SetActive(true);
            Debug.Log("jogador vermelho venceu");
            
            turno_jogador = "amarelo";
                     
          }
          break;


          //============================================================SE DOIS OU MAIS JOGADORES TERMINARAM (MODO DE 3 JOGADORES)===========================================================

          if (totalamarelocasa > 3 && totalverdecasa > 3 && totalvermelhocasa < 4 )
          {

            if (!vitoria_amarelo.activeInHierarchy)
            {
              SoundManager.winAudioSource.Play();
            }
            if(!vitoria_verde.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            } 
            vitoria_amarelo.SetActive(true);
            vitoria_verde.SetActive(true);


            StartCoroutine("GameCompleteRoutine");
            Debug.Log("Jogo concluido");
            turno_jogador = "none";

          }


          if (totalamarelocasa > 3 && totalvermelhocasa > 3 && totalverdecasa < 4 )
          {

            if (!vitoria_amarelo.activeInHierarchy)
            {
              SoundManager.winAudioSource.Play();
            }
            if(!vitoria_vermelho.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            } 
            vitoria_amarelo.SetActive(true);
            vitoria_vermelho.SetActive(true);


            StartCoroutine("GameCompleteRoutine");
            Debug.Log("Jogo concluido");
            turno_jogador = "none";

          }


          
          if (totalverdecasa > 3 && totalvermelhocasa > 3 && totalamarelocasa < 4 )
          {

            if (!vitoria_verde.activeInHierarchy)
            {
              SoundManager.winAudioSource.Play();
            }
            if(!vitoria_vermelho.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            } 
            vitoria_verde.SetActive(true);
            vitoria_vermelho.SetActive(true);


            StartCoroutine("GameCompleteRoutine");
            Debug.Log("Jogo concluido");
            turno_jogador = "none";

          }
          break;
    


    
           case 4:


            if(totalamarelocasa > 3 && totalverdecasa < 4 && totalvermelhocasa < 4 && totalazulcasa < 4 && turno_jogador == "amarelo")
          {
            if(!vitoria_amarelo.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_amarelo.SetActive(true);
            Debug.Log("jogador amarelo venceu");
          
            turno_jogador = "verde";
                     
          }


          if(totalverdecasa > 3 && totalamarelocasa < 4 && totalvermelhocasa < 4 && totalazulcasa < 4 && turno_jogador == "verde")
          {
            if(!vitoria_verde.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_verde.SetActive(true);
            Debug.Log("jogador verde venceu");
            
            turno_jogador = "vermelho";
                     
          }

          if(totalvermelhocasa > 3 && totalamarelocasa < 4 && totalvermelhocasa < 4 && totalazulcasa < 4  && turno_jogador == "vermelho")
          {
            if(!vitoria_vermelho.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_vermelho.SetActive(true);
            Debug.Log("jogador vermelho venceu");
            
            turno_jogador = "azul";
                     
          }
          if(totalazulcasa > 3 && totalamarelocasa < 4 && totalazulcasa < 4 && totalvermelhocasa < 4  && turno_jogador == "azul")
          {
            if(!vitoria_azul.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_azul.SetActive(true);
            Debug.Log("jogador azul venceu");
            
            turno_jogador = "amarelo";
                     
          }




          //============================================================SE DOIS OU MAIS JOGADORES TERMINARAM (MODO DE 4 JOGADORES)===========================================================
        

           if(totalamarelocasa > 3 && totalverdecasa > 3 && totalvermelhocasa < 4 && totalazulcasa < 4 && (turno_jogador == "amarelo" || turno_jogador == "verde"))
          {
            if(!vitoria_amarelo.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }
            if(!vitoria_verde.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_amarelo.SetActive(true);
            vitoria_verde.SetActive(true);
            Debug.Log("jogador amarelo e verde venceram");
        
            if(turno_jogador == "amarelo")
            {
              turno_jogador = "vermelho";
            }
            else{
              if(turno_jogador == "verde")
              {
              turno_jogador = "vermelho";
              }
            }                          
          }


         if(totalamarelocasa > 3 && totalazulcasa > 3 && totalvermelhocasa < 4 && totalverdecasa < 4 && (turno_jogador == "amarelo" || turno_jogador == "azul"))
          {
            if(!vitoria_amarelo.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }
            if(!vitoria_azul.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_amarelo.SetActive(true);
            vitoria_azul.SetActive(true);
            Debug.Log("jogador amarelo e azul venceram");
        
            if(turno_jogador == "amarelo")
            {
              turno_jogador = "verde";
            }
            else{
            if(turno_jogador == "azul")
            {
              turno_jogador = "verde";
            }
            }                          
          }

         if(totalamarelocasa > 3 && totalvermelhocasa > 3 && totalazulcasa < 4 && totalverdecasa < 4 && (turno_jogador == "amarelo" || turno_jogador == "vermelho"))
          {
            if(!vitoria_amarelo.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }
            if(!vitoria_vermelho.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_amarelo.SetActive(true);
            vitoria_vermelho.SetActive(true);
            Debug.Log("jogador amarelo e vermelho venceram");
        
            if(turno_jogador == "amarelo")
            {
              turno_jogador = "verde";
            }
            else{
            if(turno_jogador == "vermelho")
            {
              turno_jogador = "azul";
            }
            }                          
          }

          
         if(totalverdecasa > 3 && totalazulcasa > 3 && totalvermelhocasa < 4 && totalamarelocasa < 4 && turno_jogador == "verde" || turno_jogador == "azul")
          {
            if(!vitoria_verde.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }
            if(!vitoria_azul.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_verde.SetActive(true);
            vitoria_azul.SetActive(true);
            Debug.Log("jogador verde e azul venceram");
        
            if(turno_jogador == "verde")
            {
              turno_jogador = "vermelho";
            }
            else{
            if(turno_jogador == "azul")
            {
              turno_jogador = "amarelo";
            }
            }                          
          }


          
         if(totalverdecasa > 3 && totalvermelhocasa > 3 && totalazulcasa < 4 && totalamarelocasa < 4 && (turno_jogador == "verde" || turno_jogador == "vermelho"))
          {
            if(!vitoria_verde.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }
            if(!vitoria_vermelho.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_verde.SetActive(true);
            vitoria_vermelho.SetActive(true);
            Debug.Log("jogador verde e vermelho venceram");
        
            if(turno_jogador == "verde")
            {
              turno_jogador = "azul";
            }
            else{
            if(turno_jogador == "vermelho")
            {
              turno_jogador = "azul";
            }    
            }                      
          }

             if(totalazulcasa > 3 && totalvermelhocasa > 3 && totalverdecasa < 4 && totalamarelocasa < 4 && (turno_jogador == "azul" || turno_jogador == "vermelho"))
          {
            if(!vitoria_azul.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }
            if(!vitoria_vermelho.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            vitoria_azul.SetActive(true);
            vitoria_vermelho.SetActive(true);
            Debug.Log("jogador azul e vermelho venceram");
        
            if(turno_jogador == "azul")
            {
              turno_jogador = "amarelo";
            }
            else{
            if(turno_jogador == "vermelho")
            {
              turno_jogador = "amarelo";
            }    
            }                      
          }
        

          //==============================================SE 3 OU MAIS JOGADORES ACABAREM (Modo 4 jogadores)==============================================================
            if(totalamarelocasa > 3 && totalverdecasa > 3 && totalvermelhocasa > 3 && totalazulcasa < 4)
          {
            if(!vitoria_amarelo.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }
            if(!vitoria_verde.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            if(!vitoria_vermelho.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            } 
            vitoria_amarelo.SetActive(true);
            vitoria_verde.SetActive(true);
            vitoria_vermelho.SetActive(true);
            StartCoroutine("GameCompleteRoutine");
            Debug.Log("o jogo acabou");
            turno_jogador = "none";
        
                                    
          }
            if(totalamarelocasa > 3 && totalverdecasa > 3 && totalazulcasa > 3 && totalvermelhocasa < 4)
          {
            if(!vitoria_amarelo.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }
            if(!vitoria_verde.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            if(!vitoria_azul.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            } 
            vitoria_amarelo.SetActive(true);
            vitoria_verde.SetActive(true);
            vitoria_azul.SetActive(true);
            StartCoroutine("GameCompleteRoutine");
            Debug.Log("o jogo acabou");
            turno_jogador = "none";
        
                                    
          }

            if(totalamarelocasa > 3 && totalvermelhocasa > 3 && totalazulcasa > 3 && totalverdecasa < 4)
          {
            if(!vitoria_amarelo.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }
            if(!vitoria_vermelho.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            if(!vitoria_azul.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            } 
            vitoria_amarelo.SetActive(true);
            vitoria_vermelho.SetActive(true);
            vitoria_azul.SetActive(true);
            StartCoroutine("GameCompleteRoutine");
            Debug.Log("o jogo acabou");
            turno_jogador = "none";
        
                                    
          }

            if(totalverdecasa > 3 && totalvermelhocasa > 3 && totalazulcasa > 3 && totalamarelocasa < 4)
          {
            if(!vitoria_verde.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }
            if(!vitoria_vermelho.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            }  
            if(!vitoria_azul.activeInHierarchy)
            {
            SoundManager.winAudioSource.Play();
            } 
            vitoria_verde.SetActive(true);
            vitoria_vermelho.SetActive(true);
            vitoria_azul.SetActive(true);
            StartCoroutine("GameCompleteRoutine");
            Debug.Log("o jogo acabou");
            turno_jogador = "none";
        
                                    
          }


          break;
        }

       

      if(nome_jogador_atual.Contains("jogador_vermelho"))
      {
        if(nome_jogador_atual == "jogador_vermelho_1" ) {
          jogador_atual = codjogadorvermelho1.codjogadorvermelho1col;
        }
        if(nome_jogador_atual == "jogador_vermelho_2" ) {
          jogador_atual = codjogadorvermelho2.codjogadorvermelho2col;
        }
        if(nome_jogador_atual == "jogador_vermelho_3" ) {
          jogador_atual = codjogadorvermelho3.codjogadorvermelho3col;
        }
        if(nome_jogador_atual == "jogador_vermelho_4" ) {
          jogador_atual = codjogadorvermelho4.codjogadorvermelho4col;
        }
      }

      if(nome_jogador_atual.Contains("jogador_verde"))
      {
        if(nome_jogador_atual == "jogador_verde_1" ) {
          jogador_atual = codjogadorverde1.codjogadorverde1col;
        }
        if(nome_jogador_atual == "jogador_verde_2" ) {
          jogador_atual = codjogadorverde2.codjogadorverde2col;
        }
        if(nome_jogador_atual == "jogador_verde_3" ) {
          jogador_atual = codjogadorverde3.codjogadorverde3col;
        }
        if(nome_jogador_atual == "jogador_verde_4" ) {
          jogador_atual = codjogadorverde4.codjogadorverde4col;
        }
      }

      if(nome_jogador_atual.Contains("jogador_azul"))
      {
        if(nome_jogador_atual == "jogador_azul_1" ) {
          jogador_atual = codjogadorazul1.codjogadorazul1col;
        }
        if(nome_jogador_atual == "jogador_azul_2" ) {
          jogador_atual = codjogadorazul2.codjogadorazul2col;
        }
        if(nome_jogador_atual == "jogador_azul_3" ) {
          jogador_atual = codjogadorazul3.codjogadorazul3col;
        }
        if(nome_jogador_atual == "jogador_azul_4" ) {
          jogador_atual = codjogadorazul4.codjogadorazul4col;
        }
      }


      if(nome_jogador_atual.Contains("jogador_amarelo"))
      {
        if(nome_jogador_atual == "jogador_amarelo_1" ) {
          jogador_atual = codjogadoramarelo1.codjogadoramarelo1col;
        }
        if(nome_jogador_atual == "jogador_amarelo_2" ) {
          jogador_atual = codjogadoramarelo2.codjogadoramarelo2col;
        }
        if(nome_jogador_atual == "jogador_amarelo_3" ) {
          jogador_atual = codjogadoramarelo3.codjogadoramarelo3col;
        }
        if(nome_jogador_atual == "jogador_amarelo_4" ) {
          jogador_atual = codjogadoramarelo4.codjogadoramarelo4col;
        }
      }


    //=============================================================JOGADOR VS OPONENTE (SE PASSOU POR CIMA) COLISAO ===========================================================================================================
  /*

  ▄█████░▄█████▄░██░░░░░██████░▄██████░▄████▄░▄█████▄░░░█████▄░░░█████░▄█████▄░▄██████░▄████▄░██████▄░▄█████▄░█████▄░▄█████░▄██████
  ██░░░░░██░░░██░██░░░░░░░██░░░██░░░░░░██░░██░██░░░██░░░░░░░██░░░░░░██░██░░░██░██░░░░░░██░░██░██░░░██░██░░░██░██░░██░██░░░░░██░░░░░
  ██░░░░░██░░░██░██░░░░░░░██░░░▀█████▄░██░░██░██░░░██░░░▄████▀░░░░░░██░██░░░██░██░░███░██░░██░██░░░██░██░░░██░█████▀░█████░░▀█████▄
  ██░░░░░██░░░██░██░░░░░░░██░░░░░░░░██░██████░██░░░██░░░██░░░░░░░▄▄░██░██░░░██░██░░░██░██████░██░░░██░██░░░██░██░░██░██░░░░░░░░░░██
  ▀█████░▀█████▀░██████░██████░██████▀░██░░██░▀█████▀░░░██████░░░█████░▀█████▀░▀█████▀░██░░██░██████▀░▀█████▀░██░░██░▀█████░██████▀
  ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░




  */
    if(nome_jogador_atual != "none")
    {
      switch (MainMenuManager.howManyPlayers)
      {
        case 2:
        if(nome_jogador_atual.Contains("jogador_amarelo"))
        {
          if(jogador_atual == codjogadorverde1.codjogadorverde1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_1.transform.position = posicao_peca_verde_1;
            codjogadorverde1.codjogadorverde1col = "none";
            local_peca_verde_1 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorverde2.codjogadorverde2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_2.transform.position = posicao_peca_verde_2;
            codjogadorverde2.codjogadorverde2col = "none";
            local_peca_verde_2 = 0;
            turno_jogador = "amarelo";
            
          }

          if(jogador_atual == codjogadorverde3.codjogadorverde3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_3.transform.position = posicao_peca_verde_3;
            codjogadorverde3.codjogadorverde3col = "none";
            local_peca_verde_3 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorverde4.codjogadorverde4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_4.transform.position = posicao_peca_verde_4;
            codjogadorverde4.codjogadorverde4col = "none";
            local_peca_verde_4 = 0;
            turno_jogador = "amarelo";
            
          }
        }
        if(nome_jogador_atual.Contains("jogador_verde"))
        {
          if(jogador_atual == codjogadoramarelo1.codjogadoramarelo1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_1.transform.position = posicao_peca_amarelo_1;
            codjogadoramarelo1.codjogadoramarelo1col = "none";
            local_peca_amarelo_1 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadoramarelo2.codjogadoramarelo2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_2.transform.position = posicao_peca_amarelo_2;
            codjogadoramarelo2.codjogadoramarelo2col = "none";
            local_peca_amarelo_2 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadoramarelo3.codjogadoramarelo3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_3.transform.position = posicao_peca_amarelo_3;
            codjogadoramarelo3.codjogadoramarelo3col = "none";
            local_peca_amarelo_3 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadoramarelo4.codjogadoramarelo4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_4.transform.position = posicao_peca_amarelo_4;
            codjogadoramarelo4.codjogadoramarelo4col = "none";
            local_peca_amarelo_4 = 0;
            turno_jogador = "verde";
            
          }
        }
        break;
  /*


  ▄█████░▄█████▄░██░░░░░██████░▄██████░▄████▄░▄█████▄░░░█████▄░░░█████░▄█████▄░▄██████░▄████▄░██████▄░▄█████▄░█████▄░▄█████░▄██████
  ██░░░░░██░░░██░██░░░░░░░██░░░██░░░░░░██░░██░██░░░██░░░░░░░██░░░░░░██░██░░░██░██░░░░░░██░░██░██░░░██░██░░░██░██░░██░██░░░░░██░░░░░
  ██░░░░░██░░░██░██░░░░░░░██░░░▀█████▄░██░░██░██░░░██░░░░████░░░░░░░██░██░░░██░██░░███░██░░██░██░░░██░██░░░██░█████▀░█████░░▀█████▄
  ██░░░░░██░░░██░██░░░░░░░██░░░░░░░░██░██████░██░░░██░░░░░░░██░░░▄▄░██░██░░░██░██░░░██░██████░██░░░██░██░░░██░██░░██░██░░░░░░░░░░██
  ▀█████░▀█████▀░██████░██████░██████▀░██░░██░▀█████▀░░░█████▀░░░█████░▀█████▀░▀█████▀░██░░██░██████▀░▀█████▀░██░░██░▀█████░██████▀
  ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░






  */
        case 3:

        if(nome_jogador_atual.Contains("jogador_amarelo"))
        {
          if(jogador_atual == codjogadorverde1.codjogadorverde1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_1.transform.position = posicao_peca_verde_1;
            codjogadorverde1.codjogadorverde1col = "none";
            local_peca_verde_1 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorverde2.codjogadorverde2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_2.transform.position = posicao_peca_verde_2;
            codjogadorverde2.codjogadorverde2col = "none";
            local_peca_verde_2 = 0;
            turno_jogador = "amarelo";
            
          }

          if(jogador_atual == codjogadorverde3.codjogadorverde3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_3.transform.position = posicao_peca_verde_3;
            codjogadorverde3.codjogadorverde3col = "none";
            local_peca_verde_3 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorverde4.codjogadorverde4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_4.transform.position = posicao_peca_verde_4;
            codjogadorverde4.codjogadorverde4col = "none";
            local_peca_verde_4 = 0;
            turno_jogador = "amarelo";
            
          }
           if(jogador_atual == codjogadorvermelho1.codjogadorvermelho1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_1.transform.position = posicao_peca_vermelho_1;
            codjogadorvermelho1.codjogadorvermelho1col = "none";
            local_peca_vermelho_1 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorvermelho2.codjogadorvermelho2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_2.transform.position = posicao_peca_vermelho_2;
            codjogadorvermelho2.codjogadorvermelho2col = "none";
            local_peca_vermelho_2 = 0;
            turno_jogador = "amarelo";
            
          }

          if(jogador_atual == codjogadorvermelho3.codjogadorvermelho3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_3.transform.position = posicao_peca_vermelho_3;
            codjogadorvermelho3.codjogadorvermelho3col = "none";
            local_peca_vermelho_3 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorvermelho4.codjogadorvermelho4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_4.transform.position = posicao_peca_vermelho_4;
            codjogadorvermelho4.codjogadorvermelho4col = "none";
            local_peca_vermelho_4 = 0;
            turno_jogador = "amarelo";
            
          }
        }

      if(nome_jogador_atual.Contains("jogador_verde"))
        {
          if(jogador_atual == codjogadoramarelo1.codjogadoramarelo1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_1.transform.position = posicao_peca_amarelo_1;
            codjogadoramarelo1.codjogadoramarelo1col = "none";
            local_peca_amarelo_1 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadoramarelo2.codjogadoramarelo2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_2.transform.position = posicao_peca_amarelo_2;
            codjogadoramarelo2.codjogadoramarelo2col = "none";
            local_peca_amarelo_2 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadoramarelo3.codjogadoramarelo3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_3.transform.position = posicao_peca_amarelo_3;
            codjogadoramarelo3.codjogadoramarelo3col = "none";
            local_peca_amarelo_3 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadoramarelo4.codjogadoramarelo4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_4.transform.position = posicao_peca_amarelo_4;
            codjogadoramarelo4.codjogadoramarelo4col = "none";
            local_peca_amarelo_4 = 0;
            turno_jogador = "verde";
            
          }

          if(jogador_atual == codjogadorvermelho1.codjogadorvermelho1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_1.transform.position = posicao_peca_vermelho_1;
            codjogadorvermelho1.codjogadorvermelho1col = "none";
            local_peca_vermelho_1 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadorvermelho2.codjogadorvermelho2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_2.transform.position = posicao_peca_vermelho_2;
            codjogadorvermelho2.codjogadorvermelho2col = "none";
            local_peca_vermelho_2 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadorvermelho3.codjogadorvermelho3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_3.transform.position = posicao_peca_vermelho_3;
            codjogadorvermelho3.codjogadorvermelho3col = "none";
            local_peca_vermelho_3 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadorvermelho4.codjogadorvermelho4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_4.transform.position = posicao_peca_vermelho_4;
            codjogadorvermelho4.codjogadorvermelho4col = "none";
            local_peca_vermelho_4 = 0;
            turno_jogador = "verde";
            
          }
        }
        if(nome_jogador_atual.Contains("jogador_vermelho"))
        {
          if(jogador_atual == codjogadoramarelo1.codjogadoramarelo1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_1.transform.position = posicao_peca_amarelo_1;
            codjogadoramarelo1.codjogadoramarelo1col = "none";
            local_peca_amarelo_1 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadoramarelo2.codjogadoramarelo2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_2.transform.position = posicao_peca_amarelo_2;
            codjogadoramarelo2.codjogadoramarelo2col = "none";
            local_peca_amarelo_2 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadoramarelo3.codjogadoramarelo3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_3.transform.position = posicao_peca_amarelo_3;
            codjogadoramarelo3.codjogadoramarelo3col = "none";
            local_peca_amarelo_3 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadoramarelo4.codjogadoramarelo4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_4.transform.position = posicao_peca_amarelo_4;
            codjogadoramarelo4.codjogadoramarelo4col = "none";
            local_peca_amarelo_4 = 0;
            turno_jogador = "vermelho";
            
          }

          if(jogador_atual == codjogadorverde1.codjogadorverde1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_1.transform.position = posicao_peca_verde_1;
            codjogadorverde1.codjogadorverde1col = "none";
            local_peca_verde_1 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadorverde2.codjogadorverde2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_2.transform.position = posicao_peca_verde_2;
            codjogadorverde2.codjogadorverde2col = "none";
            local_peca_verde_2 = 0;
            turno_jogador = "vermelho";
            
          }

          if(jogador_atual == codjogadorverde3.codjogadorverde3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_3.transform.position = posicao_peca_verde_3;
            codjogadorverde3.codjogadorverde3col = "none";
            local_peca_verde_3 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadorverde4.codjogadorverde4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_4.transform.position = posicao_peca_verde_4;
            codjogadorverde4.codjogadorverde4col = "none";
            local_peca_verde_4 = 0;
            turno_jogador = "vermelho";
            
          }

        }
        break;
        /*
        
      ▄█████░▄█████▄░██░░░░░██████░▄██████░▄████▄░▄█████▄░░░██░░░██░░░█████░▄█████▄░▄██████░▄████▄░██████▄░▄█████▄░█████▄░▄█████░▄██████
      ██░░░░░██░░░██░██░░░░░░░██░░░██░░░░░░██░░██░██░░░██░░░██░░░██░░░░░░██░██░░░██░██░░░░░░██░░██░██░░░██░██░░░██░██░░██░██░░░░░██░░░░░
      ██░░░░░██░░░██░██░░░░░░░██░░░▀█████▄░██░░██░██░░░██░░░███████░░░░░░██░██░░░██░██░░███░██░░██░██░░░██░██░░░██░█████▀░█████░░▀█████▄
      ██░░░░░██░░░██░██░░░░░░░██░░░░░░░░██░██████░██░░░██░░░░░░░░██░░░▄▄░██░██░░░██░██░░░██░██████░██░░░██░██░░░██░██░░██░██░░░░░░░░░░██
      ▀█████░▀█████▀░██████░██████░██████▀░██░░██░▀█████▀░░░░░░░░██░░░█████░▀█████▀░▀█████▀░██░░██░██████▀░▀█████▀░██░░██░▀█████░██████▀
      ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░

        */
        case 4:

      if(nome_jogador_atual.Contains("jogador_amarelo"))
        {
          if(jogador_atual == codjogadorverde1.codjogadorverde1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_1.transform.position = posicao_peca_verde_1;
            codjogadorverde1.codjogadorverde1col = "none";
            local_peca_verde_1 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorverde2.codjogadorverde2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_2.transform.position = posicao_peca_verde_2;
            codjogadorverde2.codjogadorverde2col = "none";
            local_peca_verde_2 = 0;
            turno_jogador = "amarelo";
            
          }

          if(jogador_atual == codjogadorverde3.codjogadorverde3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_3.transform.position = posicao_peca_verde_3;
            codjogadorverde3.codjogadorverde3col = "none";
            local_peca_verde_3 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorverde4.codjogadorverde4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_4.transform.position = posicao_peca_verde_4;
            codjogadorverde4.codjogadorverde4col = "none";
            local_peca_verde_4 = 0;
            turno_jogador = "amarelo";
            
          }
           if(jogador_atual == codjogadorvermelho1.codjogadorvermelho1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_1.transform.position = posicao_peca_vermelho_1;
            codjogadorvermelho1.codjogadorvermelho1col = "none";
            local_peca_vermelho_1 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorvermelho2.codjogadorvermelho2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_2.transform.position = posicao_peca_vermelho_2;
            codjogadorvermelho2.codjogadorvermelho2col = "none";
            local_peca_vermelho_2 = 0;
            turno_jogador = "amarelo";
            
          }

          if(jogador_atual == codjogadorvermelho3.codjogadorvermelho3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_3.transform.position = posicao_peca_vermelho_3;
            codjogadorvermelho3.codjogadorvermelho3col = "none";
            local_peca_vermelho_3 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorvermelho4.codjogadorvermelho4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_4.transform.position = posicao_peca_vermelho_4;
            codjogadorvermelho4.codjogadorvermelho4col = "none";
            local_peca_vermelho_4 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorazul1.codjogadorazul1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_1.transform.position = posicao_peca_azul_1;
            codjogadorazul1.codjogadorazul1col = "none";
            local_peca_azul_1 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorazul2.codjogadorazul2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_2.transform.position = posicao_peca_azul_2;
            codjogadorazul2.codjogadorazul2col = "none";
            local_peca_azul_2 = 0;
            turno_jogador = "amarelo";
            
          }

          if(jogador_atual == codjogadorazul3.codjogadorazul3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_3.transform.position = posicao_peca_azul_3;
            codjogadorazul3.codjogadorazul3col = "none";
            local_peca_azul_3 = 0;
            turno_jogador = "amarelo";
            
          }
          if(jogador_atual == codjogadorazul4.codjogadorazul4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_4.transform.position = posicao_peca_azul_4;
            codjogadorazul4.codjogadorazul4col = "none";
            local_peca_azul_4 = 0;
            turno_jogador = "amarelo";
            
          }
        }


       if(nome_jogador_atual.Contains("jogador_verde"))
        {
          if(jogador_atual == codjogadoramarelo1.codjogadoramarelo1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_1.transform.position = posicao_peca_amarelo_1;
            codjogadoramarelo1.codjogadoramarelo1col = "none";
            local_peca_amarelo_1 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadoramarelo2.codjogadoramarelo2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_2.transform.position = posicao_peca_amarelo_2;
            codjogadoramarelo2.codjogadoramarelo2col = "none";
            local_peca_amarelo_2 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadoramarelo3.codjogadoramarelo3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_3.transform.position = posicao_peca_amarelo_3;
            codjogadoramarelo3.codjogadoramarelo3col = "none";
            local_peca_amarelo_3 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadoramarelo4.codjogadoramarelo4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_4.transform.position = posicao_peca_amarelo_4;
            codjogadoramarelo4.codjogadoramarelo4col = "none";
            local_peca_amarelo_4 = 0;
            turno_jogador = "verde";
            
          }

          if(jogador_atual == codjogadorvermelho1.codjogadorvermelho1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_1.transform.position = posicao_peca_vermelho_1;
            codjogadorvermelho1.codjogadorvermelho1col = "none";
            local_peca_vermelho_1 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadorvermelho2.codjogadorvermelho2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_2.transform.position = posicao_peca_vermelho_2;
            codjogadorvermelho2.codjogadorvermelho2col = "none";
            local_peca_vermelho_2 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadorvermelho3.codjogadorvermelho3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_3.transform.position = posicao_peca_vermelho_3;
            codjogadorvermelho3.codjogadorvermelho3col = "none";
            local_peca_vermelho_3 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadorvermelho4.codjogadorvermelho4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_4.transform.position = posicao_peca_vermelho_4;
            codjogadorvermelho4.codjogadorvermelho4col = "none";
            local_peca_vermelho_4 = 0;
            turno_jogador = "verde";
            
          }
            if(jogador_atual == codjogadorazul1.codjogadorazul1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_1.transform.position = posicao_peca_azul_1;
            codjogadorazul1.codjogadorazul1col = "none";
            local_peca_azul_1 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadorazul2.codjogadorazul2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_2.transform.position = posicao_peca_azul_2;
            codjogadorazul2.codjogadorazul2col = "none";
            local_peca_azul_2 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadorazul3.codjogadorazul3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_3.transform.position = posicao_peca_azul_3;
            codjogadorazul3.codjogadorazul3col = "none";
            local_peca_azul_3 = 0;
            turno_jogador = "verde";
            
          }
          if(jogador_atual == codjogadorazul4.codjogadorazul4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_4.transform.position = posicao_peca_azul_4;
            codjogadorazul4.codjogadorazul4col = "none";
            local_peca_azul_4 = 0;
            turno_jogador = "verde";
            
          }
        }

      if(nome_jogador_atual.Contains("jogador_vermelho"))
        {
          if(jogador_atual == codjogadoramarelo1.codjogadoramarelo1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_1.transform.position = posicao_peca_amarelo_1;
            codjogadoramarelo1.codjogadoramarelo1col = "none";
            local_peca_amarelo_1 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadoramarelo2.codjogadoramarelo2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_2.transform.position = posicao_peca_amarelo_2;
            codjogadoramarelo2.codjogadoramarelo2col = "none";
            local_peca_amarelo_2 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadoramarelo3.codjogadoramarelo3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_3.transform.position = posicao_peca_amarelo_3;
            codjogadoramarelo3.codjogadoramarelo3col = "none";
            local_peca_amarelo_3 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadoramarelo4.codjogadoramarelo4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_4.transform.position = posicao_peca_amarelo_4;
            codjogadoramarelo4.codjogadoramarelo4col = "none";
            local_peca_amarelo_4 = 0;
            turno_jogador = "vermelho";
            
          }

          if(jogador_atual == codjogadorverde1.codjogadorverde1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_1.transform.position = posicao_peca_verde_1;
            codjogadorverde1.codjogadorverde1col = "none";
            local_peca_verde_1 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadorverde2.codjogadorverde2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_2.transform.position = posicao_peca_verde_2;
            codjogadorverde2.codjogadorverde2col = "none";
            local_peca_verde_2 = 0;
            turno_jogador = "vermelho";
            
          }

          if(jogador_atual == codjogadorverde3.codjogadorverde3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_3.transform.position = posicao_peca_verde_3;
            codjogadorverde3.codjogadorverde3col = "none";
            local_peca_verde_3 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadorverde4.codjogadorverde4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_4.transform.position = posicao_peca_verde_4;
            codjogadorverde4.codjogadorverde4col = "none";
            local_peca_verde_4 = 0;
            turno_jogador = "vermelho";
            
          }


          if(jogador_atual == codjogadorazul1.codjogadorazul1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_1.transform.position = posicao_peca_azul_1;
            codjogadorazul1.codjogadorazul1col = "none";
            local_peca_azul_1 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadorazul2.codjogadorazul2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_2.transform.position = posicao_peca_azul_2;
            codjogadorazul2.codjogadorazul2col = "none";
            local_peca_azul_2 = 0;
            turno_jogador = "vermelho";
            
          }

          if(jogador_atual == codjogadorazul3.codjogadorazul3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_3.transform.position = posicao_peca_azul_3;
            codjogadorazul3.codjogadorazul3col = "none";
            local_peca_azul_3 = 0;
            turno_jogador = "vermelho";
            
          }
          if(jogador_atual == codjogadorazul4.codjogadorazul4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_azul_4.transform.position = posicao_peca_azul_4;
            codjogadorazul4.codjogadorazul4col = "none";
            local_peca_azul_4 = 0;
            turno_jogador = "vermelho";
            
          }

        }
        if(nome_jogador_atual.Contains("jogador_azul"))
        {
          if(jogador_atual == codjogadoramarelo1.codjogadoramarelo1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_1.transform.position = posicao_peca_amarelo_1;
            codjogadoramarelo1.codjogadoramarelo1col = "none";
            local_peca_amarelo_1 = 0;
            turno_jogador = "azul";
            
          }
          if(jogador_atual == codjogadoramarelo2.codjogadoramarelo2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_2.transform.position = posicao_peca_amarelo_2;
            codjogadoramarelo2.codjogadoramarelo2col = "none";
            local_peca_amarelo_2 = 0;
            turno_jogador = "azul";
            
          }
          if(jogador_atual == codjogadoramarelo3.codjogadoramarelo3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_3.transform.position = posicao_peca_amarelo_3;
            codjogadoramarelo3.codjogadoramarelo3col = "none";
            local_peca_amarelo_3 = 0;
            turno_jogador = "azul";
            
          }
          if(jogador_atual == codjogadoramarelo4.codjogadoramarelo4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_amarelo_4.transform.position = posicao_peca_amarelo_4;
            codjogadoramarelo4.codjogadoramarelo4col = "none";
            local_peca_amarelo_4 = 0;
            turno_jogador = "azul";
            
          }

          if(jogador_atual == codjogadorverde1.codjogadorverde1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_1.transform.position = posicao_peca_verde_1;
            codjogadorverde1.codjogadorverde1col = "none";
            local_peca_verde_1 = 0;
            turno_jogador = "azul";
            
          }
          if(jogador_atual == codjogadorverde2.codjogadorverde2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_2.transform.position = posicao_peca_verde_2;
            codjogadorverde2.codjogadorverde2col = "none";
            local_peca_verde_2 = 0;
            turno_jogador = "azul";
            
          }

          if(jogador_atual == codjogadorverde3.codjogadorverde3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_3.transform.position = posicao_peca_verde_3;
            codjogadorverde3.codjogadorverde3col = "none";
            local_peca_verde_3 = 0;
            turno_jogador = "azul";
            
          }
          if(jogador_atual == codjogadorverde4.codjogadorverde4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_verde_4.transform.position = posicao_peca_verde_4;
            codjogadorverde4.codjogadorverde4col = "none";
            local_peca_verde_4 = 0;
            turno_jogador = "azul";
            
          }


           if(jogador_atual == codjogadorvermelho1.codjogadorvermelho1col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_1.transform.position = posicao_peca_vermelho_1;
            codjogadorvermelho1.codjogadorvermelho1col = "none";
            local_peca_vermelho_1 = 0;
            turno_jogador = "azul";
            
          }
          if(jogador_atual == codjogadorvermelho2.codjogadorvermelho2col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_2.transform.position = posicao_peca_vermelho_2;
            codjogadorvermelho2.codjogadorvermelho2col = "none";
            local_peca_vermelho_2 = 0;
            turno_jogador = "azul";
            
          }
          if(jogador_atual == codjogadorvermelho3.codjogadorvermelho3col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_3.transform.position = posicao_peca_vermelho_3;
            codjogadorvermelho3.codjogadorvermelho3col = "none";
            local_peca_vermelho_3 = 0;
            turno_jogador = "azul";
            
          }
          if(jogador_atual == codjogadorvermelho4.codjogadorvermelho4col && jogador_atual!= "Estrela")
          {
            SoundManager.diceAudioSource.Play();
            jogador_vermelho_4.transform.position = posicao_peca_vermelho_4;
            codjogadorvermelho4.codjogadorvermelho4col = "none";
            local_peca_vermelho_4 = 0;
            turno_jogador = "azul";
            
          }
        }
      break;
      }
    }

        switch(MainMenuManager.howManyPlayers)
        {
            case 2:
                if(turno_jogador == "verde")
                    {
                    rodar_dado.position = dado_verde_rolagem.position;
                    frame_verde.SetActive(true);
                    frame_amarelo.SetActive(false);
                    }
                if(turno_jogador == "amarelo")
                    {
                    rodar_dado.position = dado_amarelo_rolagem.position;
                    frame_amarelo.SetActive(true);
                    frame_verde.SetActive(false);
                    }
                botao_jogador_amarelo_1.interactable = false;
                botao_jogador_amarelo_2.interactable = false;
                botao_jogador_amarelo_3.interactable = false;
                botao_jogador_amarelo_4.interactable = false;

                botao_jogador_verde_1.interactable = false;
                botao_jogador_verde_2.interactable = false;
                botao_jogador_verde_3.interactable = false;
                botao_jogador_verde_4.interactable = false;



                borda_peca_amarelo_1.SetActive(false);
                borda_peca_amarelo_2.SetActive(false);
                borda_peca_amarelo_3.SetActive(false);
                borda_peca_amarelo_4.SetActive(false);

                borda_peca_verde_1.SetActive(false);
                borda_peca_verde_2.SetActive(false);
                borda_peca_verde_3.SetActive(false);
                borda_peca_verde_4.SetActive(false);
                break;
            
             case 3:
                if(turno_jogador == "verde")
                    {
                    rodar_dado.position = dado_verde_rolagem.position;
                    frame_verde.SetActive(true);
                    frame_amarelo.SetActive(false);
                    frame_vermelho.SetActive(false);
                    }
                if(turno_jogador == "amarelo")
                    {
                    rodar_dado.position = dado_amarelo_rolagem.position;
                    frame_amarelo.SetActive(true);
                    frame_verde.SetActive(false);
                    frame_vermelho.SetActive(false);
                    }
                if(turno_jogador == "vermelho")
                    {
                    rodar_dado.position = dado_vermelho_rolagem.position;
                    frame_amarelo.SetActive(false);
                    frame_verde.SetActive(false);
                    frame_vermelho.SetActive(true);
                    }
                botao_jogador_amarelo_1.interactable = false;
                botao_jogador_amarelo_2.interactable = false;
                botao_jogador_amarelo_3.interactable = false;
                botao_jogador_amarelo_4.interactable = false;

                botao_jogador_verde_1.interactable = false;
                botao_jogador_verde_2.interactable = false;
                botao_jogador_verde_3.interactable = false;
                botao_jogador_verde_4.interactable = false;

                botao_jogador_vermelho_1.interactable = false;
                botao_jogador_vermelho_2.interactable = false;
                botao_jogador_vermelho_3.interactable = false;
                botao_jogador_vermelho_4.interactable = false;



                borda_peca_amarelo_1.SetActive(false);
                borda_peca_amarelo_2.SetActive(false);
                borda_peca_amarelo_3.SetActive(false);
                borda_peca_amarelo_4.SetActive(false);

                borda_peca_verde_1.SetActive(false);
                borda_peca_verde_2.SetActive(false);
                borda_peca_verde_3.SetActive(false);
                borda_peca_verde_4.SetActive(false);

                borda_peca_vermelho_1.SetActive(false);
                borda_peca_vermelho_2.SetActive(false);
                borda_peca_vermelho_3.SetActive(false);
                borda_peca_vermelho_4.SetActive(false);
                break;


            case 4:
                if(turno_jogador == "verde")
                    {
                    rodar_dado.position = dado_verde_rolagem.position;
                    frame_verde.SetActive(true);
                    frame_amarelo.SetActive(false);
                    frame_vermelho.SetActive(false);
                    frame_azul.SetActive(false);
                    }
                if(turno_jogador == "amarelo")
                    {
                    rodar_dado.position = dado_amarelo_rolagem.position;
                    frame_amarelo.SetActive(true);
                    frame_verde.SetActive(false);
                    frame_vermelho.SetActive(false);
                    frame_azul.SetActive(false);
                    }
                if(turno_jogador == "vermelho")
                    {
                    rodar_dado.position = dado_vermelho_rolagem.position;
                    frame_amarelo.SetActive(false);
                    frame_verde.SetActive(false);
                    frame_vermelho.SetActive(true);
                    frame_azul.SetActive(false);
                    }

                  if(turno_jogador == "azul")
                    {
                    rodar_dado.position = dado_azul_rolagem.position;
                    frame_amarelo.SetActive(false);
                    frame_verde.SetActive(false);
                    frame_vermelho.SetActive(false);
                    frame_azul.SetActive(true);
                    }
                botao_jogador_amarelo_1.interactable = false;
                botao_jogador_amarelo_2.interactable = false;
                botao_jogador_amarelo_3.interactable = false;
                botao_jogador_amarelo_4.interactable = false;

                botao_jogador_verde_1.interactable = false;
                botao_jogador_verde_2.interactable = false;
                botao_jogador_verde_3.interactable = false;
                botao_jogador_verde_4.interactable = false;

                botao_jogador_vermelho_1.interactable = false;
                botao_jogador_vermelho_2.interactable = false;
                botao_jogador_vermelho_3.interactable = false;
                botao_jogador_vermelho_4.interactable = false;

                botao_jogador_azul_1.interactable = false;
                botao_jogador_azul_2.interactable = false;
                botao_jogador_azul_3.interactable = false;
                botao_jogador_azul_4.interactable = false;



                borda_peca_amarelo_1.SetActive(false);
                borda_peca_amarelo_2.SetActive(false);
                borda_peca_amarelo_3.SetActive(false);
                borda_peca_amarelo_4.SetActive(false);

                borda_peca_verde_1.SetActive(false);
                borda_peca_verde_2.SetActive(false);
                borda_peca_verde_3.SetActive(false);
                borda_peca_verde_4.SetActive(false);

                borda_peca_vermelho_1.SetActive(false);
                borda_peca_vermelho_2.SetActive(false);
                borda_peca_vermelho_3.SetActive(false);
                borda_peca_vermelho_4.SetActive(false);

                borda_peca_azul_1.SetActive(false);
                borda_peca_azul_2.SetActive(false);
                borda_peca_azul_3.SetActive(false);
                borda_peca_azul_4.SetActive(false);        
                break;



        }

    
}

/*


██████░██████░▄██▄▄██▄░░░██████░██████▄░██████░▄█████░██████░▄████▄░██░░░░░██████░███████░▄████▄░█████▄░░░██████▄░▄████▄░██████▄░▄█████▄
██░░░░░░░██░░░██░██░██░░░░░██░░░██░░░██░░░██░░░██░░░░░░░██░░░██░░██░██░░░░░░░██░░░░░░░░██░██░░██░██░░██░░░██░░░██░██░░██░██░░░██░██░░░██
█████░░░░██░░░██░██░██░░░░░██░░░██░░░██░░░██░░░██░░░░░░░██░░░██░░██░██░░░░░░░██░░░▄█████▀░██░░██░█████▀░░░██░░░██░██░░██░██░░░██░██░░░██
██░░░░░░░██░░░██░██░██░░░░░██░░░██░░░██░░░██░░░██░░░░░░░██░░░██████░██░░░░░░░██░░░██░░░░░░██████░██░░██░░░██░░░██░██████░██░░░██░██░░░██
██░░░░░██████░██░██░██░░░██████░██░░░██░██████░▀█████░██████░██░░██░██████░██████░███████░██░░██░██░░██░░░██████▀░██░░██░██████▀░▀█████▀
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░





*/





/*
█████▄░▄█████▄░██░░░░░▄████▄░█████▄░░░██████▄░▄████▄░██████▄░▄█████▄
██░░██░██░░░██░██░░░░░██░░██░██░░██░░░██░░░██░██░░██░██░░░██░██░░░██
█████▀░██░░░██░██░░░░░██░░██░█████▀░░░██░░░██░██░░██░██░░░██░██░░░██
██░░██░██░░░██░██░░░░░██████░██░░██░░░██░░░██░██████░██░░░██░██░░░██
██░░██░▀█████▀░██████░██░░██░██░░██░░░██████▀░██░░██░██████▀░▀█████▀
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
*/


//Só rodar o dado quando disponível 
    public void rolar_dado ()
    {   
    botaodado.interactable = false;

    SoundManager.diceAudioSource.Play();
    botaodado.interactable = false;

    //Já Criei animacao do dado. Assinala um número de 1 a 6 de Randno para o dado.


        selecionar_animacao_dado = randNo.Next(1,7);

        switch(selecionar_animacao_dado)
        {
            case 1:
            animacao_dado_1.SetActive(true);  
            animacao_dado_2.SetActive(false);
            animacao_dado_3.SetActive(false); 
            animacao_dado_4.SetActive(false);
            animacao_dado_5.SetActive(false);
            animacao_dado_6.SetActive(false);
            break;
            case 2:
            animacao_dado_1.SetActive(false);  
            animacao_dado_2.SetActive(true);
            animacao_dado_3.SetActive(false); 
            animacao_dado_4.SetActive(false);
            animacao_dado_5.SetActive(false);
            animacao_dado_6.SetActive(false);
            break;
            case 3:
            animacao_dado_1.SetActive(false);  
            animacao_dado_2.SetActive(false);
            animacao_dado_3.SetActive(true); 
            animacao_dado_4.SetActive(false);
            animacao_dado_5.SetActive(false);
            animacao_dado_6.SetActive(false);
            break;
            case 4:
            animacao_dado_1.SetActive(false);  
            animacao_dado_2.SetActive(false);
            animacao_dado_3.SetActive(false); 
            animacao_dado_4.SetActive(true);
            animacao_dado_5.SetActive(false);
            animacao_dado_6.SetActive(false);
            break;
            case 5:
            animacao_dado_1.SetActive(false);  
            animacao_dado_2.SetActive(false);
            animacao_dado_3.SetActive(false); 
            animacao_dado_4.SetActive(false);
            animacao_dado_5.SetActive(true);
            animacao_dado_6.SetActive(false);
            break;
            case 6:
            animacao_dado_1.SetActive(false);  
            animacao_dado_2.SetActive(false);
            animacao_dado_3.SetActive(false); 
            animacao_dado_4.SetActive(false);
            animacao_dado_5.SetActive(false);
            animacao_dado_6.SetActive(true);
            break;

            StartCoroutine("jogadoresnaoinicializados");


        }
    }

/*



█████░▄█████▄░▄██████░▄████▄░██████▄░▄█████▄░█████▄░▄█████░▄██████░░░██████▄░▄████▄░▄█████▄░░░██████░██████▄░██████░▄█████░██████░▄████▄░██░░░░░██████░███████░▄████▄░██████▄░▄█████▄░▄██████
░░░██░██░░░██░██░░░░░░██░░██░██░░░██░██░░░██░██░░██░██░░░░░██░░░░░░░░██░░░██░██░░██░██░░░██░░░░░██░░░██░░░██░░░██░░░██░░░░░░░██░░░██░░██░██░░░░░░░██░░░░░░░░██░██░░██░██░░░██░██░░░██░██░░░░░
░░░██░██░░░██░██░░███░██░░██░██░░░██░██░░░██░█████▀░█████░░▀█████▄░░░██░░░██░██░░██░██░░░██░░░░░██░░░██░░░██░░░██░░░██░░░░░░░██░░░██░░██░██░░░░░░░██░░░▄█████▀░██░░██░██░░░██░██░░░██░▀█████▄
▄▄░██░██░░░██░██░░░██░██████░██░░░██░██░░░██░██░░██░██░░░░░░░░░░██░░░██░░░██░██████░██░░░██░░░░░██░░░██░░░██░░░██░░░██░░░░░░░██░░░██████░██░░░░░░░██░░░██░░░░░░██████░██░░░██░██░░░██░░░░░░██
█████░▀█████▀░▀█████▀░██░░██░██████▀░▀█████▀░██░░██░▀█████░██████▀░░░██░░░██░██░░██░▀█████▀░░░██████░██░░░██░██████░▀█████░██████░██░░██░██████░██████░███████░██░░██░██████▀░▀█████▀░██████▀
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░





*/
//Entra no escopo da animação do dado, é para esperar a inicialização dos jogadores;
    IEnumerator jogadoresnaoinicializados ()
    {
        yield return new WaitForSeconds(1f);

        switch (turno_jogador)
        {
            case "amarelo":

            //determina quando podemos mexer na peca amarelo 1, sendo no caso ter um valor de movimento maior do que voce tirou no dado, que a peça não esteja num local que não conhecemos (menor que 0) e que ele não tenha completado sua jornada chegando na 
            //casa final.

            if ((bloco_movimento_amarelo.Count - local_peca_amarelo_1)>= selecionar_animacao_dado && local_peca_amarelo_1 > 0 && (bloco_movimento_amarelo.Count > local_peca_amarelo_1))
            {
                borda_peca_amarelo_1.SetActive(true);
                botao_jogador_amarelo_1.interactable = true;
            }

            else{
                borda_peca_amarelo_1.SetActive(false);
                botao_jogador_amarelo_1.interactable = false;

            }

             if ((bloco_movimento_amarelo.Count - local_peca_amarelo_2)>= selecionar_animacao_dado && local_peca_amarelo_2 > 0 && (bloco_movimento_amarelo.Count > local_peca_amarelo_2))
            {
                borda_peca_amarelo_2.SetActive(true);
                botao_jogador_amarelo_2.interactable = true;
            }

            else{
              borda_peca_amarelo_2.SetActive(false);
              botao_jogador_amarelo_2.interactable = false;

            }

             if ((bloco_movimento_amarelo.Count - local_peca_amarelo_3)>= selecionar_animacao_dado && local_peca_amarelo_3 > 0 && (bloco_movimento_amarelo.Count > local_peca_amarelo_3))
            {
                borda_peca_amarelo_3.SetActive(true);
                botao_jogador_amarelo_3.interactable = true;
            }

            else{
                borda_peca_amarelo_3.SetActive(false);
                botao_jogador_amarelo_3.interactable = false;
            }

             if ((bloco_movimento_amarelo.Count - local_peca_amarelo_4)>= selecionar_animacao_dado && local_peca_amarelo_4 > 0 && (bloco_movimento_amarelo.Count > local_peca_amarelo_4))
            {
                borda_peca_amarelo_4.SetActive(true);
                botao_jogador_amarelo_4.interactable = true;
            }
            else
             {
                borda_peca_amarelo_4.SetActive(false);
                botao_jogador_amarelo_4.interactable = false;
            }

            // tirando 6 e movendo a peça da casa 0.
            
            if (selecionar_animacao_dado == 6 && local_peca_amarelo_1 == 0)
            {
                borda_peca_amarelo_1.SetActive(true);
                botao_jogador_amarelo_1.interactable = true;

            }

              if (selecionar_animacao_dado == 6 && local_peca_amarelo_2 == 0)
            {
                borda_peca_amarelo_2.SetActive(true);
                botao_jogador_amarelo_2.interactable = true;

            }

              if (selecionar_animacao_dado == 6 && local_peca_amarelo_3 == 0)
            {
                borda_peca_amarelo_3.SetActive(true);
                botao_jogador_amarelo_3.interactable = true;

            }

              if (selecionar_animacao_dado == 6 && local_peca_amarelo_4 == 0)
            {
                borda_peca_amarelo_4.SetActive(true);
                botao_jogador_amarelo_4.interactable = true;

            }

            if (!borda_peca_amarelo_1.activeInHierarchy && !borda_peca_amarelo_2.activeInHierarchy && !borda_peca_amarelo_3.activeInHierarchy && borda_peca_amarelo_4.activeInHierarchy)
            {
                botao_jogador_amarelo_1.interactable = false;
                botao_jogador_amarelo_2.interactable = false;
                botao_jogador_amarelo_3.interactable = false;
                botao_jogador_amarelo_4.interactable = false;
                
        
                switch (MainMenuManager.howManyPlayers)
                {
                case 2:
                turno_jogador = "verde";
                inicializardado();
                break;
                 
                case 3:
                 turno_jogador = "verde";
                inicializardado();
                break;

                case 4:
                turno_jogador = "verde";
                inicializardado();
                break;

                }
                
     
            }
            break;
            case "verde":

             if ((bloco_movimento_verde.Count - local_peca_verde_1)>= selecionar_animacao_dado && local_peca_verde_1 > 0 && (bloco_movimento_verde.Count > local_peca_verde_1))
            {
                borda_peca_verde_1.SetActive(true);
                botao_jogador_verde_1.interactable = true;
            }
            else

            {
                borda_peca_verde_1.SetActive(false);
                botao_jogador_verde_1.interactable = false;
            }

             if ((bloco_movimento_verde.Count - local_peca_verde_2)>= selecionar_animacao_dado && local_peca_verde_2 > 0 && (bloco_movimento_verde.Count > local_peca_verde_2))
            {
                borda_peca_verde_2.SetActive(true);
                botao_jogador_verde_2.interactable = true;
            }

            {
                borda_peca_verde_2.SetActive(false);
                botao_jogador_verde_2.interactable = false;
            }

             if ((bloco_movimento_verde.Count - local_peca_verde_3)>= selecionar_animacao_dado && local_peca_verde_3 > 0 && (bloco_movimento_verde.Count > local_peca_verde_3))
            {
                borda_peca_verde_3.SetActive(true);
                botao_jogador_verde_3.interactable = true;
            }
            else
            {
                borda_peca_verde_3.SetActive(false);
                botao_jogador_verde_3.interactable = false;
            }

             if ((bloco_movimento_verde.Count - local_peca_verde_4)>= selecionar_animacao_dado && local_peca_verde_4 > 0 && (bloco_movimento_verde.Count > local_peca_verde_4))
            {
                borda_peca_verde_4.SetActive(true);
                botao_jogador_verde_4.interactable = true;
            }
            else
            {
                borda_peca_verde_4.SetActive(false);
                botao_jogador_verde_4.interactable = false;
            }









           if (selecionar_animacao_dado == 6 && local_peca_verde_1 == 0)
            {
                borda_peca_verde_1.SetActive(true);
                botao_jogador_verde_1.interactable = true;

            }

              if (selecionar_animacao_dado == 6 && local_peca_verde_2 == 0)
            {
                borda_peca_verde_2.SetActive(true);
                botao_jogador_verde_2.interactable = true;

            }

              if (selecionar_animacao_dado == 6 && local_peca_verde_3 == 0)
            {
                borda_peca_verde_3.SetActive(true);
                botao_jogador_verde_3.interactable = true;

            }

              if (selecionar_animacao_dado == 6 && local_peca_verde_4 == 0)
            {
                borda_peca_verde_4.SetActive(true);
                botao_jogador_verde_4.interactable = true;

            }


            if (!borda_peca_verde_1.activeInHierarchy && !borda_peca_verde_2.activeInHierarchy && !borda_peca_verde_3.activeInHierarchy && borda_peca_verde_4.activeInHierarchy)
            {
                //botao_jogador_verde_1.interactable = false;
                //botao_jogador_verde_2.interactable = false;
                //botao_jogador_verde_3.interactable = false;
                //botao_jogador_verde_4.interactable = false;
    
             switch (MainMenuManager.howManyPlayers)
                {
                case 2:
                turno_jogador = "amarelo";
                inicializardado();
                break;

                case 3:
                turno_jogador = "vermelho";
                inicializardado();
                break;
                
                case 4:
                turno_jogador = "vermelho";
                inicializardado();
                break;

                }
            }
            break;

            case "vermelho":




            if ((bloco_movimento_vermelho.Count - local_peca_vermelho_1)>= selecionar_animacao_dado && local_peca_vermelho_1 > 0 && (bloco_movimento_vermelho.Count > local_peca_vermelho_1))
            {
                borda_peca_vermelho_1.SetActive(true);
                botao_jogador_vermelho_1.interactable = true;
            }

            else

            {
                borda_peca_vermelho_1.SetActive(false);
                botao_jogador_vermelho_1.interactable = false;
            }

             if ((bloco_movimento_vermelho.Count - local_peca_vermelho_2)>= selecionar_animacao_dado && local_peca_vermelho_2 > 0 && (bloco_movimento_vermelho.Count > local_peca_vermelho_2))
            {
                borda_peca_vermelho_2.SetActive(true);
                botao_jogador_vermelho_2.interactable = true;
            }
            
            else
             {
                borda_peca_vermelho_2.SetActive(false);
                botao_jogador_vermelho_2.interactable = false;
            }

             if ((bloco_movimento_vermelho.Count - local_peca_vermelho_3)>= selecionar_animacao_dado && local_peca_vermelho_3 > 0 && (bloco_movimento_vermelho.Count > local_peca_vermelho_3))
            {
                borda_peca_vermelho_3.SetActive(true);
                botao_jogador_vermelho_3.interactable = true;
            }

            else

            {
                borda_peca_vermelho_3.SetActive(false);
                botao_jogador_vermelho_3.interactable = false;
            }

             if ((bloco_movimento_vermelho.Count - local_peca_vermelho_4)>= selecionar_animacao_dado && local_peca_vermelho_4 > 0 && (bloco_movimento_vermelho.Count > local_peca_vermelho_4))
            {
                borda_peca_vermelho_4.SetActive(true);
                botao_jogador_vermelho_4.interactable = true;
            }
            else
            {
                borda_peca_vermelho_4.SetActive(false);
                botao_jogador_vermelho_4.interactable = false;
            }



              if (selecionar_animacao_dado == 6 && local_peca_vermelho_1 == 0)
            {
                borda_peca_vermelho_1.SetActive(true);
                botao_jogador_vermelho_1.interactable = true;

            }

          else
            {
                borda_peca_vermelho_1.SetActive(false);
                botao_jogador_vermelho_1.interactable = false;

            }

              if (selecionar_animacao_dado == 6 && local_peca_vermelho_2 == 0)
            {
                borda_peca_vermelho_2.SetActive(true);
                botao_jogador_vermelho_2.interactable = true;

            }
          else
            {
                borda_peca_vermelho_2.SetActive(false);
                botao_jogador_vermelho_2.interactable = false;

            }

              if (selecionar_animacao_dado == 6 && local_peca_vermelho_3 == 0)
            {
                borda_peca_vermelho_3.SetActive(true);
                botao_jogador_vermelho_3.interactable = true;

            }

             {
                borda_peca_vermelho_3.SetActive(false);
                botao_jogador_vermelho_3.interactable = false;

            }

              if (selecionar_animacao_dado == 6 && local_peca_vermelho_4 == 0)
            {
                borda_peca_vermelho_4.SetActive(true);
                botao_jogador_vermelho_4.interactable = true;
            }


            else
            {
                borda_peca_vermelho_4.SetActive(false);
                botao_jogador_vermelho_4.interactable = false;
            }
             if (!borda_peca_vermelho_1.activeInHierarchy && !borda_peca_vermelho_2.activeInHierarchy && !borda_peca_vermelho_3.activeInHierarchy && borda_peca_vermelho_4.activeInHierarchy)
            {
                //botao_jogador_vermelho_1.interactable = false;
                //botao_jogador_vermelho_2.interactable = false;
                //botao_jogador_vermelho_3.interactable = false;
                //botao_jogador_vermelho_4.interactable = false;
                
             switch (MainMenuManager.howManyPlayers)
                {
                case 3:
                turno_jogador = "amarelo";
                inicializardado();
                break;
                
                case 4:
                turno_jogador = "azul";
                inicializardado();
                break;

                }
            }
            break;

             case "azul":

            if ((bloco_movimento_azul.Count - local_peca_azul_1)>= selecionar_animacao_dado && local_peca_azul_1 > 0 && (bloco_movimento_azul.Count > local_peca_azul_1))
            {
                borda_peca_azul_1.SetActive(true);
                botao_jogador_azul_1.interactable = true;
            }

            else

            {
                borda_peca_azul_1.SetActive(false);
                botao_jogador_azul_1.interactable = false;
            }

             if ((bloco_movimento_azul.Count - local_peca_azul_2)>= selecionar_animacao_dado && local_peca_azul_2 > 0 && (bloco_movimento_azul.Count > local_peca_azul_2))
            {
                borda_peca_azul_2.SetActive(true);
                botao_jogador_azul_2.interactable = true;
            }

            else


             {
                borda_peca_azul_2.SetActive(false);
                botao_jogador_azul_2.interactable = false;
            }

             if ((bloco_movimento_azul.Count - local_peca_azul_3)>= selecionar_animacao_dado && local_peca_azul_3 > 0 && (bloco_movimento_azul.Count > local_peca_azul_3))
            {
                borda_peca_azul_3.SetActive(true);
                botao_jogador_azul_3.interactable = true;
            }

            else

            {
                borda_peca_azul_3.SetActive(false);
                botao_jogador_azul_3.interactable = false;
            }

             if ((bloco_movimento_azul.Count - local_peca_azul_4)>= selecionar_animacao_dado && local_peca_azul_4 > 0 && (bloco_movimento_azul.Count > local_peca_azul_4))
            {
                borda_peca_azul_4.SetActive(true);
                botao_jogador_azul_4.interactable = true;
            }

            else

            {
                borda_peca_azul_4.SetActive(false);
                botao_jogador_azul_4.interactable = false;
            }
            






            
            
            if (selecionar_animacao_dado == 6 && local_peca_azul_1 == 0)
            {
                borda_peca_azul_1.SetActive(true);
                botao_jogador_azul_1.interactable = true;

            }

              if (selecionar_animacao_dado == 6 && local_peca_azul_2 == 0)
            {
                borda_peca_azul_2.SetActive(true);
                botao_jogador_azul_2.interactable = true;

            }

              if (selecionar_animacao_dado == 6 && local_peca_azul_3 == 0)
            {
                borda_peca_azul_3.SetActive(true);
                botao_jogador_azul_3.interactable = true;

            }

              if (selecionar_animacao_dado == 6 && local_peca_azul_4 == 0)
            {
                borda_peca_azul_4.SetActive(true);
                botao_jogador_azul_4.interactable = true;


            }
            if (!borda_peca_azul_1.activeInHierarchy && !borda_peca_azul_2.activeInHierarchy && !borda_peca_azul_3.activeInHierarchy && borda_peca_azul_4.activeInHierarchy)
            {
                //botao_jogador_azul_1.interactable = false;
                //botao_jogador_azul_2.interactable = false;
                //botao_jogador_azul_3.interactable = false;
                //botao_jogador_azul_4.interactable = false;
                
             switch (MainMenuManager.howManyPlayers)
                {
                case 4:
                turno_jogador = "amarelo";
                inicializardado();
                break;

                }
            }
            break;
        }

    }




void Start(){
    //Posicao Inicial do Jogo no primeiro Frame_.
        //Sincronizar o jogo com a tela
        QualitySettings.vSyncCount = 1;
        //Estamos travando a 30 Frame_s por segundo
        Application.targetFrameRate = 30;

        //Valor do dado
        randNo = new System.Random();


        //Dependendo do valor de randNo, ira ativar a animação do dado para exibir ao jogador quanto ele tirou
        animacao_dado_1.SetActive(false);
        animacao_dado_2.SetActive(false);
        animacao_dado_3.SetActive(false);
        animacao_dado_4.SetActive(false);
        animacao_dado_5.SetActive(false);
        animacao_dado_6.SetActive(false);
        
        //Posicao inicial das pecas

        posicao_peca_vermelho_1 = jogador_vermelho_1.transform.position;
        posicao_peca_vermelho_2 = jogador_vermelho_2.transform.position;
        posicao_peca_vermelho_3 = jogador_vermelho_3.transform.position;
        posicao_peca_vermelho_4= jogador_vermelho_4.transform.position;

        posicao_peca_verde_1= jogador_verde_1.transform.position;
        posicao_peca_verde_2= jogador_verde_2.transform.position;
        posicao_peca_verde_3= jogador_verde_3.transform.position;
        posicao_peca_verde_4= jogador_verde_4.transform.position;

        posicao_peca_amarelo_1 = jogador_amarelo_1.transform.position;
        posicao_peca_amarelo_2 = jogador_amarelo_2.transform.position;
        posicao_peca_amarelo_3 = jogador_amarelo_3.transform.position;
        posicao_peca_amarelo_4 = jogador_amarelo_4.transform.position;

        posicao_peca_azul_1= jogador_azul_1.transform.position;
        posicao_peca_azul_2= jogador_azul_2.transform.position;
        posicao_peca_azul_3= jogador_azul_3.transform.position;
        posicao_peca_azul_4= jogador_azul_4.transform.position;


        //Desativar as bordas das peças. Caso a gente queira tirar isso depois é de boa. É só para colocar alguma animação:

        borda_peca_vermelho_1.SetActive(false);
        borda_peca_vermelho_2.SetActive(false);
        borda_peca_vermelho_3.SetActive(false);
        borda_peca_vermelho_4.SetActive(false);

        borda_peca_verde_1.SetActive(false);
        borda_peca_verde_2.SetActive(false);
        borda_peca_verde_3.SetActive(false);
        borda_peca_verde_4.SetActive(false);

        borda_peca_amarelo_1.SetActive(false);
        borda_peca_amarelo_2.SetActive(false);
        borda_peca_amarelo_3.SetActive(false);
        borda_peca_amarelo_4.SetActive(false);

        borda_peca_azul_1.SetActive(false);
        borda_peca_azul_2.SetActive(false);
        borda_peca_azul_3.SetActive(false);
        borda_peca_azul_4.SetActive(false);


       //Ainda não fiz o menu do jogo, mas tem um Case que dá para usar para quantos jogadores escolhemos
        //Se quiser criar uma forma de escolher quais cores escolher para jogar, temos que implementar outra camada nesta lógica


        /* CONSIDERANDO 2 JOGADORES COMO SÓ AMARELO E VERMELHO
           3 JOGADORES SEM O VERDE   */

        switch (MainMenuManager.howManyPlayers)
        {
            case 2:
                turno_jogador = "amarelo";
                
                frame_amarelo.SetActive(true);
                frame_vermelho.SetActive(false);
                frame_azul.SetActive(false);
                frame_verde.SetActive(false);

                jogador_vermelho_1.SetActive(false);
                jogador_vermelho_2.SetActive(false);
                jogador_vermelho_3.SetActive(false);
                jogador_vermelho_4.SetActive(false);
            
                jogador_azul_1.SetActive(false);
                jogador_azul_2.SetActive(false);
                jogador_azul_3.SetActive(false);
                jogador_azul_4.SetActive(false);

                rodar_dado.position = dado_amarelo_rolagem.position;


                break;
            case 3:
                turno_jogador = "amarelo";
                
                frame_amarelo.SetActive(true);
                frame_vermelho.SetActive(false);
                frame_azul.SetActive(false);
                frame_verde.SetActive(false);

                jogador_verde_1.SetActive(false);
                jogador_verde_2.SetActive(false);
                jogador_verde_3.SetActive(false);
                jogador_verde_4.SetActive(false);

                rodar_dado.position = dado_amarelo_rolagem.position;

                break;
            case 4:
                turno_jogador = "amarelo";
                
                frame_amarelo.SetActive(true);
                frame_vermelho.SetActive(false);
                frame_azul.SetActive(false);
                frame_verde.SetActive(false);

                rodar_dado.position = dado_amarelo_rolagem.position;

                break;


        }

     



}




/*


█   █  ███  █   █ █████ █   █ █████ █   █ █████  ███  
██ ██ █   █ █   █   █   ██ ██ █     ██  █   █   █   █ 
█ █ █ █   █  █ █    █   █ █ █ ████  █ █ █   █   █   █ 
█   █ █   █  █ █    █   █   █ █     █  ██   █   █   █ 
█   █  ███    █   █████ █   █ █████ █   █   █    ███  


████  █████  ████  ███  
█   █ █     █     █   █ 
████  ████  █     █████ 
█     █     █     █   █ 
█     █████  ████ █   █ 
█     █████  ████ █   █   



█   █ █████ ████  █   █ █████ █     █   █  ███  
█   █ █     █   █ ██ ██ █     █     █   █ █   █ 
 █ █  ████  ████  █ █ █ ████  █     █████ █████ 
 █ █  █     █   █ █   █ █     █     █   █ █   █ 
  █   █████ █   █ █   █ █████ █████ █   █ █   █ 


*/

public void jogador_vermelho_1_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_vermelho_1.SetActive(false);
    borda_peca_vermelho_2.SetActive(false);
    borda_peca_vermelho_3.SetActive(false);
    borda_peca_vermelho_4.SetActive(false);

    botao_jogador_vermelho_1.interactable = false;
    botao_jogador_vermelho_2.interactable = false;
    botao_jogador_vermelho_3.interactable = false;
    botao_jogador_vermelho_4.interactable = false;

    if(turno_jogador == "vermelho" && (bloco_movimento_vermelho.Count - local_peca_vermelho_1) > selecionar_animacao_dado)
    {
        
        //Mover a peca
        if (local_peca_vermelho_1 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_vermelho_1 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_vermelho_1[i] = bloco_movimento_vermelho[local_peca_vermelho_1 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_vermelho_1+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "vermelho";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 3:
                turno_jogador = "amarelo";
                break;

                case 4:
                turno_jogador = "azul";
                break;
            }
          }

          if(caminho_jogador_vermelho_1.Length > 1)
          {
            iTween.MoveTo(jogador_vermelho_1, iTween.Hash("path", caminho_jogador_vermelho_1, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_vermelho_1, iTween.Hash("position", caminho_jogador_vermelho_1[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_vermelho_1";
        }
        else
        {

      
        //Mover a peca do inicio
        if (selecionar_animacao_dado == 6 && local_peca_vermelho_1 == 0  )

        {
            Vector3[] caminho_jogador_vermelho_1 = new Vector3[1];
            caminho_jogador_vermelho_1[0] = bloco_movimento_vermelho[local_peca_vermelho_1].transform.position;
            local_peca_vermelho_1 +=1;
            turno_jogador = "vermelho";
            nome_jogador_atual = "jogador_vermelho_1";
            iTween.MoveTo(jogador_vermelho_1, iTween.Hash("position", caminho_jogador_vermelho_1[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
        
    }
    else{

      if(turno_jogador == "vermelho" && (bloco_movimento_vermelho.Count - local_peca_vermelho_1)  == selecionar_animacao_dado)
      
      {
        Vector3[] caminho_jogador_vermelho_1 = new Vector3[selecionar_animacao_dado];
        for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_vermelho_1[i] = bloco_movimento_vermelho[local_peca_vermelho_1 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_vermelho_1+= selecionar_animacao_dado;
           if(caminho_jogador_vermelho_1.Length > 1)
          {
            iTween.MoveTo(jogador_vermelho_1, iTween.Hash("path", caminho_jogador_vermelho_1, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_vermelho_1, iTween.Hash("position", caminho_jogador_vermelho_1[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
           turno_jogador = "vermelho";
           totalvermelhocasa +=1;
           Debug.Log("Boa, chegou em casa meu veio!");
           botao_jogador_vermelho_1.enabled=false;
      }

      else{
        Debug.Log("You need" + (bloco_movimento_vermelho.Count - local_peca_vermelho_1).ToString() + "Steps to enter into the house");
        if((local_peca_vermelho_2 + local_peca_vermelho_3 + local_peca_vermelho_4) == 0 && selecionar_animacao_dado !=6)
        {

            switch (MainMenuManager.howManyPlayers)
            {
                case 3:
                turno_jogador = "amarelo";
                break;

                case 4:
                turno_jogador = "azul";
                break;
            }

        }
        
      inicializardado();



      }
      
      
      
   }

}



public void jogador_vermelho_2_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_vermelho_1.SetActive(false);
    borda_peca_vermelho_2.SetActive(false);
    borda_peca_vermelho_3.SetActive(false);
    borda_peca_vermelho_4.SetActive(false);

    botao_jogador_vermelho_1.interactable = false;
    botao_jogador_vermelho_2.interactable = false;
    botao_jogador_vermelho_3.interactable = false;
    botao_jogador_vermelho_4.interactable = false;

    if(turno_jogador == "vermelho" && (bloco_movimento_vermelho.Count - local_peca_vermelho_2) > selecionar_animacao_dado)
    {
        if (local_peca_vermelho_2 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_vermelho_2 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_vermelho_2[i] = bloco_movimento_vermelho[local_peca_vermelho_2 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_vermelho_2+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "vermelho";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 3:
                turno_jogador = "amarelo";
                break;

                case 4:
                turno_jogador = "azul";
                break;
            }
          }

          if(caminho_jogador_vermelho_2.Length > 1)
          {
            iTween.MoveTo(jogador_vermelho_2, iTween.Hash("path", caminho_jogador_vermelho_2, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_vermelho_2, iTween.Hash("position", caminho_jogador_vermelho_2[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_vermelho_2";
        }
        else{


        if (selecionar_animacao_dado == 6 && local_peca_vermelho_2 == 0 )
        {
            Vector3[] caminho_jogador_vermelho_2 = new Vector3[1];
            caminho_jogador_vermelho_2[0] = bloco_movimento_vermelho[local_peca_vermelho_2].transform.position;
            local_peca_vermelho_2 +=1;
            turno_jogador = "vermelho";
            nome_jogador_atual = "jogador_vermelho_2";
            iTween.MoveTo(jogador_vermelho_2, iTween.Hash("position", caminho_jogador_vermelho_2[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }

        }
    }

    else{

       if(turno_jogador == "vermelho" && (bloco_movimento_vermelho.Count - local_peca_vermelho_2)  == selecionar_animacao_dado)
      
      {
        Vector3[] caminho_jogador_vermelho_2 = new Vector3[selecionar_animacao_dado];
        for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_vermelho_2[i] = bloco_movimento_vermelho[local_peca_vermelho_2 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_vermelho_2+= selecionar_animacao_dado;
           if(caminho_jogador_vermelho_2.Length > 1)
          {
            iTween.MoveTo(jogador_vermelho_2, iTween.Hash("path", caminho_jogador_vermelho_2, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_vermelho_2, iTween.Hash("position", caminho_jogador_vermelho_2[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
           turno_jogador = "vermelho";
           totalvermelhocasa +=1;
           Debug.Log("Boa, chegou em casa meu veio!");
           botao_jogador_vermelho_2.enabled=false;
      }

      else{
        Debug.Log("You need" + (bloco_movimento_vermelho.Count - local_peca_vermelho_2).ToString() + "Steps to enter into the house");
        if((local_peca_vermelho_1 + local_peca_vermelho_3 + local_peca_vermelho_4) == 0 && selecionar_animacao_dado !=6)
        {

            switch (MainMenuManager.howManyPlayers)
            {
                case 3:
                turno_jogador = "amarelo";
                break;

                case 4:
                turno_jogador = "azul";
                break;
            }

        }
        
      inicializardado();



      }

    }
        

}


public void jogador_vermelho_3_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_vermelho_1.SetActive(false);
    borda_peca_vermelho_2.SetActive(false);
    borda_peca_vermelho_3.SetActive(false);
    borda_peca_vermelho_4.SetActive(false);

    botao_jogador_vermelho_1.interactable = false;
    botao_jogador_vermelho_2.interactable = false;
    botao_jogador_vermelho_3.interactable = false;
    botao_jogador_vermelho_4.interactable = false;

    if(turno_jogador == "vermelho" && (bloco_movimento_vermelho.Count - local_peca_vermelho_3) > selecionar_animacao_dado)
    {

        if (local_peca_vermelho_3 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_vermelho_3 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_vermelho_3[i] = bloco_movimento_vermelho[local_peca_vermelho_3 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_vermelho_3+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "vermelho";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 3:
                turno_jogador = "amarelo";
                break;

                case 4:
                turno_jogador = "azul";
                break;
            }
          }

          if(caminho_jogador_vermelho_3.Length > 1)
          {
            iTween.MoveTo(jogador_vermelho_3, iTween.Hash("path", caminho_jogador_vermelho_3, "speed", 135, "time", 3.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_vermelho_3, iTween.Hash("position", caminho_jogador_vermelho_3[0], "speed", 135, "time", 3.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_vermelho_3";
        }
        else{


        if (selecionar_animacao_dado == 6 && local_peca_vermelho_3 == 0  )
        {
            Vector3[] caminho_jogador_vermelho_3 = new Vector3[1];
            caminho_jogador_vermelho_3[0] = bloco_movimento_vermelho[local_peca_vermelho_3].transform.position;
            local_peca_vermelho_3 +=1;
            turno_jogador = "vermelho";
            nome_jogador_atual = "jogador_vermelho_3";
            iTween.MoveTo(jogador_vermelho_3, iTween.Hash("position", caminho_jogador_vermelho_3[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
    }
    else{

       if(turno_jogador == "vermelho" && (bloco_movimento_vermelho.Count - local_peca_vermelho_3)  == selecionar_animacao_dado)
      
      {
        Vector3[] caminho_jogador_vermelho_3 = new Vector3[selecionar_animacao_dado];
        for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_vermelho_3[i] = bloco_movimento_vermelho[local_peca_vermelho_3 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_vermelho_3+= selecionar_animacao_dado;
           if(caminho_jogador_vermelho_3.Length > 1)
          {
            iTween.MoveTo(jogador_vermelho_3, iTween.Hash("path", caminho_jogador_vermelho_3, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_vermelho_3, iTween.Hash("position", caminho_jogador_vermelho_3[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
           turno_jogador = "vermelho";
           totalvermelhocasa +=1;
           Debug.Log("Boa, chegou em casa meu veio!");
           botao_jogador_vermelho_3.enabled=false;
      }

      else{
        Debug.Log("You need" + (bloco_movimento_vermelho.Count - local_peca_vermelho_3).ToString() + "Steps to enter into the house");
        if((local_peca_vermelho_1 + local_peca_vermelho_2 + local_peca_vermelho_4) == 0 && selecionar_animacao_dado !=6)
        {

            switch (MainMenuManager.howManyPlayers)
            {
                case 3:
                turno_jogador = "amarelo";
                break;

                case 4:
                turno_jogador = "azul";
                break;
            }

        }
        
      inicializardado();



      }


    }


}



public void jogador_vermelho_4_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_vermelho_1.SetActive(false);
    borda_peca_vermelho_2.SetActive(false);
    borda_peca_vermelho_3.SetActive(false);
    borda_peca_vermelho_4.SetActive(false);

    botao_jogador_vermelho_1.interactable = false;
    botao_jogador_vermelho_2.interactable = false;
    botao_jogador_vermelho_3.interactable = false;
    botao_jogador_vermelho_4.interactable = false;

    if(turno_jogador == "vermelho" && (bloco_movimento_vermelho.Count - local_peca_vermelho_4) > selecionar_animacao_dado)
    {
        if (local_peca_vermelho_4 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector4[] caminho_jogador_vermelho_4 = new Vector4[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_vermelho_4[i] = bloco_movimento_vermelho[local_peca_vermelho_4 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_vermelho_4+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "vermelho";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 3:
                turno_jogador = "amarelo";
                break;

                case 4:
                turno_jogador = "azul";
                break;
            }
          }

          if(caminho_jogador_vermelho_4.Length > 1)
          {
            iTween.MoveTo(jogador_vermelho_4, iTween.Hash("path", caminho_jogador_vermelho_4, "speed", 145, "time", 4.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_vermelho_4, iTween.Hash("position", caminho_jogador_vermelho_4[0], "speed", 145, "time", 4.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_vermelho_4";          
        }
        else{
        
        if (selecionar_animacao_dado == 6 && local_peca_vermelho_4 == 0  )
        {
            Vector3[] caminho_jogador_vermelho_4 = new Vector3[1];
            caminho_jogador_vermelho_4[0] = bloco_movimento_vermelho[local_peca_vermelho_4].transform.position;
            local_peca_vermelho_4 +=1;
            turno_jogador = "vermelho";
            nome_jogador_atual = "jogador_vermelho_4";
            iTween.MoveTo(jogador_vermelho_4, iTween.Hash("position", caminho_jogador_vermelho_4[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
        }
    }
    else{

       if(turno_jogador == "vermelho" && (bloco_movimento_vermelho.Count - local_peca_vermelho_4)  == selecionar_animacao_dado)
      
      {
        Vector3[] caminho_jogador_vermelho_4 = new Vector3[selecionar_animacao_dado];
        for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_vermelho_4[i] = bloco_movimento_vermelho[local_peca_vermelho_4 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_vermelho_4+= selecionar_animacao_dado;
           if(caminho_jogador_vermelho_4.Length > 1)
          {
            iTween.MoveTo(jogador_vermelho_4, iTween.Hash("path", caminho_jogador_vermelho_4, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_vermelho_4, iTween.Hash("position", caminho_jogador_vermelho_4[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
           turno_jogador = "vermelho";
           totalvermelhocasa +=1;
           Debug.Log("Boa, chegou em casa meu veio!");
           botao_jogador_vermelho_4.enabled=false;
      }

      else{
        Debug.Log("You need" + (bloco_movimento_vermelho.Count - local_peca_vermelho_4).ToString() + "Steps to enter into the house");
        if((local_peca_vermelho_1 + local_peca_vermelho_2 + local_peca_vermelho_3) == 0 && selecionar_animacao_dado !=6)
        {

            switch (MainMenuManager.howManyPlayers)
            {
                case 3:
                turno_jogador = "amarelo";
                break;

                case 4:
                turno_jogador = "azul";
                break;
            }

        }
        
      inicializardado();



      }

    }
        

}
/*

█   █  ███  █   █ █████ █   █ █████ █   █ █████  ███  
██ ██ █   █ █   █   █   ██ ██ █     ██  █   █   █   █ 
█ █ █ █   █  █ █    █   █ █ █ ████  █ █ █   █   █   █ 
█   █ █   █  █ █    █   █   █ █     █  ██   █   █   █ 
█   █  ███    █   █████ █   █ █████ █   █   █    ███  


███  █████  ████  ███       
█   █ █     █     █   █ 
████  ████  █     █████ 
█     █     █     █   █ 
█     █████  ████ █   █ 

█   █ █████ ████  ████  █████ 
█   █ █     █   █ █   █ █     
 █ █  ████  ████  █   █ ████  
 █ █  █     █   █ █   █ █     
  █   █████ █   █ ████  █████ 

*/
public void jogador_verde_1_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_verde_1.SetActive(false);
    borda_peca_verde_2.SetActive(false);
    borda_peca_verde_3.SetActive(false);
    borda_peca_verde_4.SetActive(false);

    botao_jogador_verde_1.interactable = false;
    botao_jogador_verde_2.interactable = false;
    botao_jogador_verde_3.interactable = false;
    botao_jogador_verde_4.interactable = false;

    if(turno_jogador == "verde" && (bloco_movimento_verde.Count - local_peca_verde_1) > selecionar_animacao_dado)
    {
        if (local_peca_verde_1 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_verde_1 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_verde_1[i] = bloco_movimento_verde[local_peca_verde_1 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_verde_1+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "verde";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 2:
                turno_jogador = "amarelo";
                break;

                case 3:
                turno_jogador = "vermelho";
                break;

                case 4:
                turno_jogador = "vermelho";
                break;
            }
          }

          if(caminho_jogador_verde_1.Length > 1)
          {
            iTween.MoveTo(jogador_verde_1, iTween.Hash("path", caminho_jogador_verde_1, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_verde_1, iTween.Hash("position", caminho_jogador_verde_1[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_verde_1";
        }
        else{


        if (selecionar_animacao_dado == 6 && local_peca_verde_1 == 0  )
        {
            Vector3[] caminho_jogador_verde_1 = new Vector3[1];
            caminho_jogador_verde_1[0] = bloco_movimento_verde[local_peca_verde_1].transform.position;
            local_peca_verde_1 +=1;
            turno_jogador = "verde";
            nome_jogador_atual = "jogador_verde_1";
            iTween.MoveTo(jogador_verde_1, iTween.Hash("position", caminho_jogador_verde_1[0], "speed", 115, "time", 1.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
    }
  }

  else{

  if(turno_jogador == "verde" && (bloco_movimento_verde.Count - local_peca_verde_1)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_verde_1 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_verde_1[i] = bloco_movimento_verde[local_peca_verde_1 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_verde_1+= selecionar_animacao_dado;
       if(caminho_jogador_verde_1.Length > 1)
      {
        iTween.MoveTo(jogador_verde_1, iTween.Hash("path", caminho_jogador_verde_1, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_verde_1, iTween.Hash("position", caminho_jogador_verde_1[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "verde";
       totalverdecasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_verde_1.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_verde.Count - local_peca_verde_1).ToString() + "Steps to enter into the house");
    if((local_peca_verde_2 + local_peca_verde_3 + local_peca_verde_4) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
           case 2:
                turno_jogador = "amarelo";
                break;

                case 3:
                turno_jogador = "vermelho";
                break;

                case 4:
                turno_jogador = "vermelho";
                break;

    }
    }
  inicializardado();



  
  
  
}

}



        

}


///Movimentos das pecas verdes

public void jogador_verde_2_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_verde_1.SetActive(false);
    borda_peca_verde_2.SetActive(false);
    borda_peca_verde_3.SetActive(false);
    borda_peca_verde_4.SetActive(false);

    botao_jogador_verde_1.interactable = false;
    botao_jogador_verde_2.interactable = false;
    botao_jogador_verde_3.interactable = false;
    botao_jogador_verde_4.interactable = false;

    if(turno_jogador == "verde" && (bloco_movimento_verde.Count - local_peca_verde_2) > selecionar_animacao_dado)
    {

         if (local_peca_verde_2 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_verde_2 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_verde_2[i] = bloco_movimento_verde[local_peca_verde_2 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_verde_2+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "verde";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 2:
                turno_jogador = "amarelo";
                break;

                case 3:
                turno_jogador = "vermelho";
                break;

                case 4:
                turno_jogador = "vermelho";
                break;
            }
          }

          if(caminho_jogador_verde_2.Length > 1)
          {
            iTween.MoveTo(jogador_verde_2, iTween.Hash("path", caminho_jogador_verde_2, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_verde_2, iTween.Hash("position", caminho_jogador_verde_2[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_verde_2";
        }
        else{

        






        if (selecionar_animacao_dado == 6 && local_peca_verde_2 == 0  )
        {
            Vector3[] caminho_jogador_verde_2 = new Vector3[1];
            caminho_jogador_verde_2[0] = bloco_movimento_verde[local_peca_verde_2].transform.position;
            local_peca_verde_2 +=1;
            turno_jogador = "verde";
            nome_jogador_atual = "jogador_verde_2";
            iTween.MoveTo(jogador_verde_2, iTween.Hash("position", caminho_jogador_verde_2[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
    }
  
  else{

  if(turno_jogador == "verde" && (bloco_movimento_verde.Count - local_peca_verde_2)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_verde_2 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_verde_2[i] = bloco_movimento_verde[local_peca_verde_2 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_verde_2+= selecionar_animacao_dado;
       if(caminho_jogador_verde_2.Length > 1)
      {
        iTween.MoveTo(jogador_verde_2, iTween.Hash("path", caminho_jogador_verde_2, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_verde_2, iTween.Hash("position", caminho_jogador_verde_2[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "verde";
       totalverdecasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_verde_2.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_verde.Count - local_peca_verde_2).ToString() + "Steps to enter into the house");
    if((local_peca_verde_1 + local_peca_verde_3 + local_peca_verde_4) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
           case 2:
                turno_jogador = "amarelo";
                break;

                case 3:
                turno_jogador = "vermelho";
                break;

                case 4:
                turno_jogador = "vermelho";
                break;

    }
    }
    
  inicializardado();



  
  
  
  
}

}
    

}

public void jogador_verde_3_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_verde_1.SetActive(false);
    borda_peca_verde_2.SetActive(false);
    borda_peca_verde_3.SetActive(false);
    borda_peca_verde_4.SetActive(false);

    botao_jogador_verde_1.interactable = false;
    botao_jogador_verde_2.interactable = false;
    botao_jogador_verde_3.interactable = false;
    botao_jogador_verde_4.interactable = false;

    if(turno_jogador == "verde" && (bloco_movimento_verde.Count - local_peca_verde_3) > selecionar_animacao_dado)
    {



        if (local_peca_verde_3 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_verde_3 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_verde_3[i] = bloco_movimento_verde[local_peca_verde_3 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_verde_3+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "verde";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 2:
                turno_jogador = "amarelo";
                break;

                case 3:
                turno_jogador = "vermelho";
                break;

                case 4:
                turno_jogador = "vermelho";
                break;
            }
          }

          if(caminho_jogador_verde_3.Length > 1)
          {
            iTween.MoveTo(jogador_verde_3, iTween.Hash("path", caminho_jogador_verde_3, "speed", 135, "time", 3.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_verde_3, iTween.Hash("position", caminho_jogador_verde_3[0], "speed", 135, "time", 3.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_verde_3";
        }
        else{

        



        if (selecionar_animacao_dado == 6 && local_peca_verde_3 == 0  )
        {
            Vector3[] caminho_jogador_verde_3 = new Vector3[1];
            caminho_jogador_verde_3[0] = bloco_movimento_verde[local_peca_verde_3].transform.position;
            local_peca_verde_3 +=1;
            turno_jogador = "verde";
            nome_jogador_atual = "jogador_verde_3";
            iTween.MoveTo(jogador_verde_3, iTween.Hash("position", caminho_jogador_verde_3[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
    }
  else{

  if(turno_jogador == "verde" && (bloco_movimento_verde.Count - local_peca_verde_3)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_verde_3 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_verde_3[i] = bloco_movimento_verde[local_peca_verde_3 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_verde_3+= selecionar_animacao_dado;
       if(caminho_jogador_verde_3.Length > 1)
      {
        iTween.MoveTo(jogador_verde_3, iTween.Hash("path", caminho_jogador_verde_3, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_verde_3, iTween.Hash("position", caminho_jogador_verde_3[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "verde";
       totalverdecasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_verde_3.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_verde.Count - local_peca_verde_3).ToString() + "Steps to enter into the house");
    if((local_peca_verde_1 + local_peca_verde_2 + local_peca_verde_4) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
           case 2:
                turno_jogador = "amarelo";
                break;

                case 3:
                turno_jogador = "vermelho";
                break;

                case 4:
                turno_jogador = "vermelho";
                break;

    }
    }
    
  inicializardado();



  
  
  
  
}

}

        

}

public void jogador_verde_4_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_verde_1.SetActive(false);
    borda_peca_verde_2.SetActive(false);
    borda_peca_verde_3.SetActive(false);
    borda_peca_verde_4.SetActive(false);

    botao_jogador_verde_1.interactable = false;
    botao_jogador_verde_2.interactable = false;
    botao_jogador_verde_3.interactable = false;
    botao_jogador_verde_4.interactable = false;

    if(turno_jogador == "verde" && (bloco_movimento_verde.Count - local_peca_verde_4) > selecionar_animacao_dado)
    {
        if (local_peca_verde_4 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector4[] caminho_jogador_verde_4 = new Vector4[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_verde_4[i] = bloco_movimento_verde[local_peca_verde_4 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_verde_4+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "verde";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                 case 2:
                turno_jogador = "amarelo";
                break;

                case 3:
                turno_jogador = "vermelho";
                break;

                case 4:
                turno_jogador = "vermelho";
                break;
            }
          }

          if(caminho_jogador_verde_4.Length > 1)
          {
            iTween.MoveTo(jogador_verde_4, iTween.Hash("path", caminho_jogador_verde_4, "speed", 145, "time", 4.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_verde_4, iTween.Hash("position", caminho_jogador_verde_4[0], "speed", 145, "time", 4.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_verde_4";
        }
        else{

        
        


        if (selecionar_animacao_dado == 6 && local_peca_verde_4 == 0  )
        {
            Vector3[] caminho_jogador_verde_4 = new Vector3[1];
            caminho_jogador_verde_4[0] = bloco_movimento_verde[local_peca_verde_4].transform.position;
            local_peca_verde_4 +=1;
            turno_jogador = "verde";
            nome_jogador_atual = "jogador_verde_4";
            iTween.MoveTo(jogador_verde_4, iTween.Hash("position", caminho_jogador_verde_4[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
    }

  else{

  if(turno_jogador == "verde" && (bloco_movimento_verde.Count - local_peca_verde_4)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_verde_4 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_verde_4[i] = bloco_movimento_verde[local_peca_verde_4 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_verde_4+= selecionar_animacao_dado;
       if(caminho_jogador_verde_4.Length > 1)
      {
        iTween.MoveTo(jogador_verde_4, iTween.Hash("path", caminho_jogador_verde_4, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_verde_4, iTween.Hash("position", caminho_jogador_verde_4[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "verde";
       totalverdecasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_verde_4.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_verde.Count - local_peca_verde_4).ToString() + "Steps to enter into the house");
    if((local_peca_verde_1 + local_peca_verde_2 + local_peca_verde_3) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
           case 2:
                turno_jogador = "amarelo";
                break;

                case 3:
                turno_jogador = "vermelho";
                break;

                case 4:
                turno_jogador = "vermelho";
                break;

    }
    }
  inicializardado();



  
  
  
  
}

}


}
/*

█   █  ███  █   █ █████ █   █ █████ █   █ █████  ███  
██ ██ █   █ █   █   █   ██ ██ █     ██  █   █   █   █ 
█ █ █ █   █  █ █    █   █ █ █ ████  █ █ █   █   █   █ 
█   █ █   █  █ █    █   █   █ █     █  ██   █   █   █ 
█   █  ███    █   █████ █   █ █████ █   █   █    ███  


███  █████  ████  ███       
█   █ █     █     █   █ 
████  ████  █     █████ 
█     █     █     █   █ 
█     █████  ████ █   █ 

 ███  █████ █   █ █     
█   █    █  █   █ █     
█████   █   █   █ █     
█   █  █    █   █ █     
█   █ █████ █████ █████ 
*/

public void jogador_azul_1_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_azul_1.SetActive(false);
    borda_peca_azul_2.SetActive(false);
    borda_peca_azul_3.SetActive(false);
    borda_peca_azul_4.SetActive(false);

    botao_jogador_azul_1.interactable = false;
    botao_jogador_azul_2.interactable = false;
    botao_jogador_azul_3.interactable = false;
    botao_jogador_azul_4.interactable = false;

    if(turno_jogador == "azul" && (bloco_movimento_azul.Count - local_peca_azul_1) > selecionar_animacao_dado)
    {


        if (local_peca_azul_1 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_azul_1 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_azul_1[i] = bloco_movimento_azul[local_peca_azul_1 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_azul_1+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "azul";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
               
                case 4:
                turno_jogador = "amarelo";
                break;
            }
          }

          if(caminho_jogador_azul_1.Length > 1)
          {
            iTween.MoveTo(jogador_azul_1, iTween.Hash("path", caminho_jogador_azul_1, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_azul_1, iTween.Hash("position", caminho_jogador_azul_1[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_azul_1";
        }
        else{


        if (selecionar_animacao_dado == 6 && local_peca_azul_1 == 0  )
        {
            Vector3[] caminho_jogador_azul_1 = new Vector3[1];
            caminho_jogador_azul_1[0] = bloco_movimento_azul[local_peca_azul_1].transform.position;
            local_peca_azul_1 +=1;
            turno_jogador = "azul";
            nome_jogador_atual = "jogador_azul_1";
            iTween.MoveTo(jogador_azul_1, iTween.Hash("position", caminho_jogador_azul_1[0], "speed", 115, "time", 1.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
        }
    }

    else{

  if(turno_jogador == "azul" && (bloco_movimento_azul.Count - local_peca_azul_1)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_azul_1 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_azul_1[i] = bloco_movimento_azul[local_peca_azul_1 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_azul_1+= selecionar_animacao_dado;
       if(caminho_jogador_azul_1.Length > 1)
      {
        iTween.MoveTo(jogador_azul_1, iTween.Hash("path", caminho_jogador_azul_1, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_azul_1, iTween.Hash("position", caminho_jogador_azul_1[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "azul";
       totalazulcasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_azul_1.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_azul.Count - local_peca_azul_1).ToString() + "Steps to enter into the house");
    if((local_peca_azul_2 + local_peca_azul_3 + local_peca_azul_4) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
                case 4:
                turno_jogador = "amarelo";
                break;

    }
    }
    
  inicializardado();



  
  
  
  
}

}
        

}



public void jogador_azul_2_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_azul_1.SetActive(false);
    borda_peca_azul_2.SetActive(false);
    borda_peca_azul_3.SetActive(false);
    borda_peca_azul_4.SetActive(false);

    botao_jogador_azul_1.interactable = false;
    botao_jogador_azul_2.interactable = false;
    botao_jogador_azul_3.interactable = false;
    botao_jogador_azul_4.interactable = false;

    if(turno_jogador == "azul" && (bloco_movimento_azul.Count - local_peca_azul_2) > selecionar_animacao_dado)
    {

        if (local_peca_azul_2 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_azul_2 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_azul_2[i] = bloco_movimento_azul[local_peca_azul_2 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_azul_2+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "azul";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                 case 4:
                turno_jogador = "amarelo";
                break;
            }
          }

          if(caminho_jogador_azul_2.Length > 1)
          {
            iTween.MoveTo(jogador_azul_2, iTween.Hash("path", caminho_jogador_azul_2, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_azul_2, iTween.Hash("position", caminho_jogador_azul_2[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_azul_2";
        }
        else{


        if (selecionar_animacao_dado == 6 && local_peca_azul_2 == 0  )
        {
            Vector3[] caminho_jogador_azul_2 = new Vector3[1];
            caminho_jogador_azul_2[0] = bloco_movimento_azul[local_peca_azul_2].transform.position;
            local_peca_azul_2 +=1;
            turno_jogador = "azul";
            nome_jogador_atual = "jogador_azul_2";
            iTween.MoveTo(jogador_azul_2, iTween.Hash("position", caminho_jogador_azul_2[0], "speed", 115, "time", 1.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
    }
    else{

  if(turno_jogador == "azul" && (bloco_movimento_azul.Count - local_peca_azul_2)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_azul_2 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_azul_2[i] = bloco_movimento_azul[local_peca_azul_2 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_azul_2+= selecionar_animacao_dado;
       if(caminho_jogador_azul_2.Length > 1)
      {
        iTween.MoveTo(jogador_azul_2, iTween.Hash("path", caminho_jogador_azul_2, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_azul_2, iTween.Hash("position", caminho_jogador_azul_2[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "azul";
       totalazulcasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_azul_2.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_azul.Count - local_peca_azul_2).ToString() + "Steps to enter into the house");
    if((local_peca_azul_1 + local_peca_azul_3 + local_peca_azul_4) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
                case 4:
                turno_jogador = "amarelo";
                break;

    }
    }
  inicializardado();



  
  
  
  
}

}
        

}

public void jogador_azul_3_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_azul_1.SetActive(false);
    borda_peca_azul_2.SetActive(false);
    borda_peca_azul_3.SetActive(false);
    borda_peca_azul_4.SetActive(false);

    botao_jogador_azul_1.interactable = false;
    botao_jogador_azul_2.interactable = false;
    botao_jogador_azul_3.interactable = false;
    botao_jogador_azul_4.interactable = false;

    if(turno_jogador == "azul" && (bloco_movimento_azul.Count - local_peca_azul_3) > selecionar_animacao_dado)
    {


        
        if (local_peca_azul_3 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_azul_3 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_azul_3[i] = bloco_movimento_azul[local_peca_azul_3 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_azul_3+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "azul";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 4:
                turno_jogador = "amarelo";
                break;
            }
          }

          if(caminho_jogador_azul_3.Length > 1)
          {
            iTween.MoveTo(jogador_azul_3, iTween.Hash("path", caminho_jogador_azul_3, "speed", 135, "time", 3.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_azul_3, iTween.Hash("position", caminho_jogador_azul_3[0], "speed", 135, "time", 3.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_azul_3";
        }
        else{

        





        if (selecionar_animacao_dado == 6 && local_peca_azul_3 == 0  )
        {
            Vector3[] caminho_jogador_azul_3 = new Vector3[1];
            caminho_jogador_azul_3[0] = bloco_movimento_azul[local_peca_azul_3].transform.position;
            local_peca_azul_3 +=1;
            turno_jogador = "azul";
            nome_jogador_atual = "jogador_azul_3";
            iTween.MoveTo(jogador_azul_3, iTween.Hash("position", caminho_jogador_azul_3[0], "speed", 115, "time", 1.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
    }

  else{

  if(turno_jogador == "azul" && (bloco_movimento_azul.Count - local_peca_azul_3)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_azul_3 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_azul_3[i] = bloco_movimento_azul[local_peca_azul_3 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_azul_3+= selecionar_animacao_dado;
       if(caminho_jogador_azul_3.Length > 1)
      {
        iTween.MoveTo(jogador_azul_3, iTween.Hash("path", caminho_jogador_azul_3, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_azul_3, iTween.Hash("position", caminho_jogador_azul_3[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "azul";
       totalazulcasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_azul_3.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_azul.Count - local_peca_azul_3).ToString() + "Steps to enter into the house");
    if((local_peca_azul_1 + local_peca_azul_2 + local_peca_azul_4) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
                case 4:
                turno_jogador = "amarelo";
                break;

    }
    }
  inicializardado();



  
  
  
  
}

}
        

}

public void jogador_azul_4_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_azul_1.SetActive(false);
    borda_peca_azul_2.SetActive(false);
    borda_peca_azul_3.SetActive(false);
    borda_peca_azul_4.SetActive(false);

    botao_jogador_azul_1.interactable = false;
    botao_jogador_azul_2.interactable = false;
    botao_jogador_azul_3.interactable = false;
    botao_jogador_azul_4.interactable = false;

    if(turno_jogador == "azul" && (bloco_movimento_azul.Count - local_peca_azul_4) > selecionar_animacao_dado)
    {

        if (local_peca_azul_4 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector4[] caminho_jogador_azul_4 = new Vector4[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_azul_4[i] = bloco_movimento_azul[local_peca_azul_4 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_azul_4+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "azul";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 4:
                turno_jogador = "amarelo";
                break;
            }
          }

          if(caminho_jogador_azul_4.Length > 1)
          {
            iTween.MoveTo(jogador_azul_4, iTween.Hash("path", caminho_jogador_azul_4, "speed", 145, "time", 4.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_azul_4, iTween.Hash("position", caminho_jogador_azul_4[0], "speed", 145, "time", 4.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_azul_4";
        }
        else{
        


        if (selecionar_animacao_dado == 6 && local_peca_azul_4 == 0  )
        {
            Vector3[] caminho_jogador_azul_4 = new Vector3[1];
            caminho_jogador_azul_4[0] = bloco_movimento_azul[local_peca_azul_4].transform.position;
            local_peca_azul_4 +=1;
            turno_jogador = "azul";
            nome_jogador_atual = "jogador_azul_4";
            iTween.MoveTo(jogador_azul_4, iTween.Hash("position", caminho_jogador_azul_4[0], "speed", 115, "time", 1.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
    }

  else{

  if(turno_jogador == "azul" && (bloco_movimento_azul.Count - local_peca_azul_4)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_azul_4 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_azul_4[i] = bloco_movimento_azul[local_peca_azul_4 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_azul_4+= selecionar_animacao_dado;
       if(caminho_jogador_azul_4.Length > 1)
      {
        iTween.MoveTo(jogador_azul_4, iTween.Hash("path", caminho_jogador_azul_4, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_azul_4, iTween.Hash("position", caminho_jogador_azul_4[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "azul";
       totalazulcasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_azul_4.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_azul.Count - local_peca_azul_4).ToString() + "Steps to enter into the house");
    if((local_peca_azul_1 + local_peca_azul_2 + local_peca_azul_3) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
                case 4:
                turno_jogador = "amarelo";
                break;

    }
    }
    
  inicializardado();



  
  
  
  
}

}

}        

/*

█   █  ███  █   █ █████ █   █ █████ █   █ █████  ███  
██ ██ █   █ █   █   █   ██ ██ █     ██  █   █   █   █ 
█ █ █ █   █  █ █    █   █ █ █ ████  █ █ █   █   █   █ 
█   █ █   █  █ █    █   █   █ █     █  ██   █   █   █ 
█   █  ███    █   █████ █   █ █████ █   █   █    ███  


███  █████  ████  ███       
█   █ █     █     █   █ 
████  ████  █     █████ 
█     █     █     █   █ 
█     █████  ████ █   █ 

 ███  █   █  ███  ████  █████ █      ███  
█   █ ██ ██ █   █ █   █ █     █     █   █ 
█████ █ █ █ █████ ████  ████  █     █   █ 
█   █ █   █ █   █ █   █ █     █     █   █ 
█   █ █   █ █   █ █   █ █████ █████  ███  

*/



public void jogador_amarelo_1_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_amarelo_1.SetActive(false);
    borda_peca_amarelo_2.SetActive(false);
    borda_peca_amarelo_3.SetActive(false);
    borda_peca_amarelo_4.SetActive(false);

    botao_jogador_amarelo_1.interactable = false;
    botao_jogador_amarelo_2.interactable = false;
    botao_jogador_amarelo_3.interactable = false;
    botao_jogador_amarelo_4.interactable = false;

    if(turno_jogador == "amarelo" && (bloco_movimento_amarelo.Count - local_peca_amarelo_1) > selecionar_animacao_dado)
    {

         if (local_peca_amarelo_1 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_amarelo_1 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_amarelo_1[i] = bloco_movimento_amarelo[local_peca_amarelo_1 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_amarelo_1+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "amarelo";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 2:
                turno_jogador = "verde";
                break;

                case 3:
                turno_jogador = "verde";
                break;

                case 4:
                turno_jogador = "verde";
                break;
            }
          }

          if(caminho_jogador_amarelo_1.Length > 1)
          {
            iTween.MoveTo(jogador_amarelo_1, iTween.Hash("path", caminho_jogador_amarelo_1, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_amarelo_1, iTween.Hash("position", caminho_jogador_amarelo_1[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_amarelo_1";
        }
        else{



        if (selecionar_animacao_dado == 6 && local_peca_amarelo_1 == 0  )
        {
            Vector3[] caminho_jogador_amarelo_1 = new Vector3[1];
            caminho_jogador_amarelo_1[0] = bloco_movimento_amarelo[local_peca_amarelo_1].transform.position;
            local_peca_amarelo_1 +=1;
            turno_jogador = "amarelo";
            nome_jogador_atual = "jogador_amarelo_1";
            iTween.MoveTo(jogador_amarelo_1, iTween.Hash("position", caminho_jogador_amarelo_1[0], "speed", 115, "time", 1.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
        }
    }
    else{

  if(turno_jogador == "amarelo" && (bloco_movimento_amarelo.Count - local_peca_amarelo_1)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_amarelo_1 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_amarelo_1[i] = bloco_movimento_amarelo[local_peca_amarelo_1 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_amarelo_1+= selecionar_animacao_dado;
       if(caminho_jogador_amarelo_1.Length > 1)
      {
        iTween.MoveTo(jogador_amarelo_1, iTween.Hash("path", caminho_jogador_amarelo_1, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_amarelo_1, iTween.Hash("position", caminho_jogador_amarelo_1[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "amarelo";
       totalamarelocasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_amarelo_1.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_amarelo.Count - local_peca_amarelo_1).ToString() + "Steps to enter into the house");
    if((local_peca_amarelo_2 + local_peca_amarelo_3 + local_peca_amarelo_4) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
                case 2:
                turno_jogador = "verde";
                break;

                case 3:
                turno_jogador = "verde";
                break;

                case 4:
                turno_jogador = "verde";
                break;

    }
    }
    
  inicializardado();



 
  
  
  
}
    
}        

}

public void jogador_amarelo_2_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_amarelo_1.SetActive(false);
    borda_peca_amarelo_2.SetActive(false);
    borda_peca_amarelo_3.SetActive(false);
    borda_peca_amarelo_4.SetActive(false);

    botao_jogador_amarelo_1.interactable = false;
    botao_jogador_amarelo_2.interactable = false;
    botao_jogador_amarelo_3.interactable = false;
    botao_jogador_amarelo_4.interactable = false;

    if(turno_jogador == "amarelo" && (bloco_movimento_amarelo.Count - local_peca_amarelo_2) > selecionar_animacao_dado)
    {

         if (local_peca_amarelo_2 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_amarelo_2 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_amarelo_2[i] = bloco_movimento_amarelo[local_peca_amarelo_2 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_amarelo_2+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "amarelo";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
                case 2:
                turno_jogador = "verde";
                break;

                case 3:
                turno_jogador = "verde";
                break;

                case 4:
                turno_jogador = "verde";
                break;
            }
          }

          if(caminho_jogador_amarelo_2.Length > 1)
          {
            iTween.MoveTo(jogador_amarelo_2, iTween.Hash("path", caminho_jogador_amarelo_2, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_amarelo_2, iTween.Hash("position", caminho_jogador_amarelo_2[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_amarelo_2";
        }
        else{

        
        

        if (selecionar_animacao_dado == 6 && local_peca_amarelo_2 == 0  )
        {
            Vector3[] caminho_jogador_amarelo_2 = new Vector3[1];
            caminho_jogador_amarelo_2[0] = bloco_movimento_amarelo[local_peca_amarelo_2].transform.position;
            local_peca_amarelo_2 +=1;
            turno_jogador = "amarelo";
            nome_jogador_atual = "jogador_amarelo_2";
            iTween.MoveTo(jogador_amarelo_2, iTween.Hash("position", caminho_jogador_amarelo_2[0], "speed", 115, "time", 1.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
    }


  else{

  if(turno_jogador == "amarelo" && (bloco_movimento_amarelo.Count - local_peca_amarelo_2)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_amarelo_2 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_amarelo_2[i] = bloco_movimento_amarelo[local_peca_amarelo_2 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_amarelo_2+= selecionar_animacao_dado;
       if(caminho_jogador_amarelo_2.Length > 1)
      {
        iTween.MoveTo(jogador_amarelo_2, iTween.Hash("path", caminho_jogador_amarelo_2, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_amarelo_2, iTween.Hash("position", caminho_jogador_amarelo_2[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "amarelo";
       totalamarelocasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_amarelo_2.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_amarelo.Count - local_peca_amarelo_2).ToString() + "Steps to enter into the house");
    if((local_peca_amarelo_1 + local_peca_amarelo_3 + local_peca_amarelo_4) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
                case 2:
                turno_jogador = "verde";
                break;

                case 3:
                turno_jogador = "verde";
                break;

                case 4:
                turno_jogador = "verde";
                break;

    }
    }
  inicializardado();



  
  
  
  
}

}
        

}

public void jogador_amarelo_3_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_amarelo_1.SetActive(false);
    borda_peca_amarelo_2.SetActive(false);
    borda_peca_amarelo_3.SetActive(false);
    borda_peca_amarelo_4.SetActive(false);

    botao_jogador_amarelo_1.interactable = false;
    botao_jogador_amarelo_2.interactable = false;
    botao_jogador_amarelo_3.interactable = false;
    botao_jogador_amarelo_4.interactable = false;

    if(turno_jogador == "amarelo" && (bloco_movimento_amarelo.Count - local_peca_amarelo_3) > selecionar_animacao_dado)
    {


         if (local_peca_amarelo_3 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector3[] caminho_jogador_amarelo_3 = new Vector3[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_amarelo_3[i] = bloco_movimento_amarelo[local_peca_amarelo_3 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_amarelo_3+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "amarelo";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
               case 2:
                turno_jogador = "verde";
                break;

                case 3:
                turno_jogador = "verde";
                break;

                case 4:
                turno_jogador = "verde";
                break;
            }
          }

          if(caminho_jogador_amarelo_3.Length > 1)
          {
            iTween.MoveTo(jogador_amarelo_3, iTween.Hash("path", caminho_jogador_amarelo_3, "speed", 135, "time", 3.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_amarelo_3, iTween.Hash("position", caminho_jogador_amarelo_3[0], "speed", 135, "time", 3.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_amarelo_3";
        }
        else{






        if (selecionar_animacao_dado == 6 && local_peca_amarelo_3 == 0  )
        {
            Vector3[] caminho_jogador_amarelo_3 = new Vector3[1];
            caminho_jogador_amarelo_3[0] = bloco_movimento_amarelo[local_peca_amarelo_3].transform.position;
            local_peca_amarelo_3 +=1;
            turno_jogador = "amarelo";
            nome_jogador_atual = "jogador_amarelo_3";
            iTween.MoveTo(jogador_amarelo_3, iTween.Hash("position", caminho_jogador_amarelo_3[0], "speed", 115, "time", 1.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
    }


    else{

  if(turno_jogador == "amarelo" && (bloco_movimento_amarelo.Count - local_peca_amarelo_3)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_amarelo_3 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_amarelo_3[i] = bloco_movimento_amarelo[local_peca_amarelo_3 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_amarelo_3+= selecionar_animacao_dado;
       if(caminho_jogador_amarelo_3.Length > 1)
      {
        iTween.MoveTo(jogador_amarelo_3, iTween.Hash("path", caminho_jogador_amarelo_3, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_amarelo_3, iTween.Hash("position", caminho_jogador_amarelo_3[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "amarelo";
       totalamarelocasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_amarelo_3.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_amarelo.Count - local_peca_amarelo_3).ToString() + "Steps to enter into the house");
    if((local_peca_amarelo_1 + local_peca_amarelo_2 + local_peca_amarelo_4) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
                case 2:
                turno_jogador = "verde";
                break;

                case 3:
                turno_jogador = "verde";
                break;

                case 4:
                turno_jogador = "verde";
                break;

    }
    }
    
  inicializardado();



 
  
  
  
}

}
        

}

public void jogador_amarelo_4_movimento(){

    SoundManager.playerAudioSource.Play();

    borda_peca_amarelo_1.SetActive(false);
    borda_peca_amarelo_2.SetActive(false);
    borda_peca_amarelo_3.SetActive(false);
    borda_peca_amarelo_4.SetActive(false);

    botao_jogador_amarelo_1.interactable = false;
    botao_jogador_amarelo_2.interactable = false;
    botao_jogador_amarelo_3.interactable = false;
    botao_jogador_amarelo_4.interactable = false;

    if(turno_jogador == "amarelo" && (bloco_movimento_amarelo.Count - local_peca_amarelo_4) > selecionar_animacao_dado)
    {


        if (local_peca_amarelo_4 > 0 )
        {
            //armazena no vetor o valor que tirei no dado.
           Vector4[] caminho_jogador_amarelo_4 = new Vector4[selecionar_animacao_dado];

            //para cada valor do dado, mostra o movimento da peca
           for(int i = 0; i < selecionar_animacao_dado; i++)
           {
            caminho_jogador_amarelo_4[i] = bloco_movimento_amarelo[local_peca_amarelo_4 + i].transform.position;
           }

           //armazena o valor do dado para os próximos movimentos (onde está a peça)
           local_peca_amarelo_4+= selecionar_animacao_dado;

          //se tirar valor de 6 no dado e a peça já estiver em jogo
          if (selecionar_animacao_dado == 6)
          {
            turno_jogador = "amarelo";
          }

          else
          {
            switch (MainMenuManager.howManyPlayers)
            {
              case 2:
                turno_jogador = "verde";
                break;

                case 3:
                turno_jogador = "verde";
                break;

                case 4:
                turno_jogador = "verde";
                break;
            }
          }

          if(caminho_jogador_amarelo_4.Length > 1)
          {
            iTween.MoveTo(jogador_amarelo_4, iTween.Hash("path", caminho_jogador_amarelo_4, "speed", 145, "time", 4.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          else
          {
            iTween.MoveTo(jogador_amarelo_4, iTween.Hash("position", caminho_jogador_amarelo_4[0], "speed", 145, "time", 4.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
          }
          nome_jogador_atual = "jogador_amarelo_4";
        }
        else{



        if (selecionar_animacao_dado == 6 && local_peca_amarelo_4 == 0  )
        {
            Vector3[] caminho_jogador_amarelo_4 = new Vector3[1];
            caminho_jogador_amarelo_4[0] = bloco_movimento_amarelo[local_peca_amarelo_4].transform.position;
            local_peca_amarelo_4 +=1;
            turno_jogador = "amarelo";
            nome_jogador_atual = "jogador_amarelo_4";
            iTween.MoveTo(jogador_amarelo_4, iTween.Hash("position", caminho_jogador_amarelo_4[0], "speed", 115, "time", 1.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
        }
      }
    }


    else{

  if(turno_jogador == "amarelo" && (bloco_movimento_amarelo.Count - local_peca_amarelo_4)  == selecionar_animacao_dado)
  
  {
    Vector3[] caminho_jogador_amarelo_4 = new Vector3[selecionar_animacao_dado];
    for(int i = 0; i < selecionar_animacao_dado; i++)
       {
        caminho_jogador_amarelo_4[i] = bloco_movimento_amarelo[local_peca_amarelo_4 + i].transform.position;
       }

       //armazena o valor do dado para os próximos movimentos (onde está a peça)
       local_peca_amarelo_4+= selecionar_animacao_dado;
       if(caminho_jogador_amarelo_4.Length > 1)
      {
        iTween.MoveTo(jogador_amarelo_4, iTween.Hash("path", caminho_jogador_amarelo_4, "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
      else
      {
        iTween.MoveTo(jogador_amarelo_4, iTween.Hash("position", caminho_jogador_amarelo_4[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "oncomplete", "inicializardado", "oncompletetarget", this.gameObject));
      }
       turno_jogador = "amarelo";
       totalamarelocasa +=1;
       Debug.Log("Boa, chegou em casa meu veio!");
       botao_jogador_amarelo_4.enabled=false;
  }

  else{
    Debug.Log("You need" + (bloco_movimento_amarelo.Count - local_peca_amarelo_4).ToString() + "Steps to enter into the house");
    if((local_peca_amarelo_1 + local_peca_amarelo_2 + local_peca_amarelo_3) == 0 && selecionar_animacao_dado !=6)
    {

        switch (MainMenuManager.howManyPlayers)
        {
                case 2:
                turno_jogador = "verde";
                break;

                case 3:
                turno_jogador = "verde";
                break;

                case 4:
                turno_jogador = "verde";
                break;

    }
    }
  inicializardado();



  
  
  
  
}

}
        

}




//Posicao Alterada a cada Frame_. 30 fps, 30 Frame_s por segundo. Nós podemos alterar esse comportamento.
void Update(){        

        
}

}