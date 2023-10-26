using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabuleiroManager : MonoBehaviour
{
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> piecePrefabs;
    private List<GameObject> activePieces = new List<GameObject>();


    private void Start()
    {
        //Spawn Peão branco 
        SpawnPiece(5, 0, 0);//1 numero = hierarquia da peça
        SpawnPiece(5, 7, 0);//2 numero = casa que a peça spawna para direita
                            //3 numero= casa  que a peça spawna para frente 

        //Spawn Peão preto 
        SpawnPiece(11, 0, 7);
        SpawnPiece(11, 7, 7);
    }
    private void Update()
    {
        DrawnTabuleiroXadrez();
        UpdateSelection();
    }
    private void SpawnPiece( int index, int x, int y )
    {
        
        GameObject go = Instantiate(piecePrefabs[index],GetTileCenter(x,y),Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        activePieces.Add(go);
    }

    private void SpawnAllPieces()
    {
        activePieces = new List<GameObject>();
        // Peças brancas
        SpawnPiece(0, 0, 0);// Editar para colocar as peças brancas 
        SpawnPiece(0, 0, 0);// Editar para colocar as peças brancas  
        SpawnPiece(0, 0, 0);// Editar para colocar as peças brancas 
        SpawnPiece(0, 0, 0);// Editar para colocar as peças brancas
        SpawnPiece(0, 0, 0);// Editar para colocar as peças brancas 
        SpawnPiece(0, 0, 0);// Editar para colocar as peças brancas

        for(int i = 0; i < 8; i++)
        {
            SpawnPiece(0, i, 0);
        }


        //Peças pretas
        SpawnPiece(6, 0, 0);// Editar para colocar as peças pretas 
        SpawnPiece(7, 0, 0);// Editar para colocar as peças pretas 
        SpawnPiece(8, 0, 0);// Editar para colocar as peças pretas 
        SpawnPiece(9, 0, 0);// Editar para colocar as peças pretas 
        SpawnPiece(10, 0, 0);// Editar para colocar as peças pretas 
        SpawnPiece(11, 0, 0);// Editar para colocar as peças pretas 

        for (int i = 0;i <8;i++)
        {
            SpawnPiece(11, i, 6);
        }

    }
    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 centerTile = Vector3.zero;
        centerTile.x += (TILE_SIZE * x) + TILE_OFFSET;
        centerTile.z += (TILE_SIZE * y) + TILE_OFFSET;
        return centerTile;
    }

    private void UpdateSelection()
    {
        if(!Camera.main) 
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Tabuleiro")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }
        

    private void DrawnTabuleiroXadrez()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;
        #region Desenhar Tabuleiro
        for (int i = 0; i < 8; i++)
        {
            Vector3 startLine =Vector3.forward * i;
            Debug.DrawLine(startLine, startLine +widthLine);
            for(int j= 0; j<= 8;j++)
            {
                startLine = Vector3.right * j;
                Debug.DrawLine(startLine, startLine + heightLine);
            }
        }
        #endregion
        #region Desenhar posição Mouse
        if (selectionX >=0 && selectionY>= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,//canto inferior esquerdo
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));//canto superior direito
            
            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,//canto superior esquerdo 
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));//canto inferior direito
        }
        #endregion
    }
}
