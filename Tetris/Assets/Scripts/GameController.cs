using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Block")]
    [SerializeField]
    private GameObject     blockPrefab;                     // Block ������
    private Transform[,]   blocks;                          // Block �迭

    [Header("Tetris")]
    [SerializeField]
    private int            cols = 20;                       // ���� Block ��
    [SerializeField]
    private int            rows = 15;                       // ���� Block ��
    [SerializeField]
    private TetrisState    state  = TetrisState.Running;    // tetris ����
    [SerializeField]
    private float          downSpeed = 1.0f;                // Block�� �������� �ӵ�
    private Tetris         board;                           // Tetris ����
    private int            dw;                              // ���� �β�

    [Header("Game")]
    [SerializeField]
    private GameObject     lineEffect;                      // lineEffect ������

    [Header("UI")]
    [SerializeField]
    private GameObject     panel;                           // �Ͻ����� Panel UI

    public int Score { private set; get; } = 0;

    private void Awake()
    {
        CTetris.Init(setOfBlockArrays.array, setOfBlockArrays.maxBlkTypes, setOfBlockArrays.maxBlkDegrees);
        board = new ScoreTetris(cols, rows);
        dw = ScoreTetris.iScreenDw;

        Setup();

        // �� Block ����
        NewBlock();

        // 1�ʸ��� Block ��ĭ�� ��������
        StartCoroutine("BlockDown");
    }

    private void Update()
    {
        // ���ӿ���
        if ( state == TetrisState.Finished ) { GameOver(); }

        // �Ͻ�����
        if ( state == TetrisState.Paused ) return;

        // Block�� �ٴڿ� ������ �� Block ����
        if ( state == TetrisState.NewBlock ) { NewBlock(); }

        // line Clear
        ClearLine();

        // ����ȭ�� ����
        DrawScreen();
        //DrawConsole();

        // Ű���� �Է¿� ���� ó��
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            Pause();
        }
        if ( Input.GetKeyDown(KeyCode.A) )
        {
            ControlBlock("a");
        }
        if ( Input.GetKeyDown(KeyCode.D) )
        {
            ControlBlock("d");
        }
        if ( Input.GetKeyDown(KeyCode.S) )
        {
            ControlBlock("s");
        }
        if ( Input.GetKeyDown(KeyCode.W) )
        {
            ControlBlock("w");
        }
        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            ControlBlock(" ");
        }
    }

    private void Setup()
    {
        blocks = new Transform[cols, rows];

        float left    = -2.6f;
        float right   = 2.6f;
        float top     = 3.9f;
        float bottom  = -2.35f;

        float dy   = (top - bottom);
        float dx   = (right - left);

        for ( int y=0; y<cols; ++y )
        {
            for ( int x=0; x<rows; ++x )
            {
                Vector3 position = new Vector3(left+x*dx/rows, top-y*dy/cols, 0);

                GameObject clone = Instantiate(blockPrefab, position, Quaternion.identity);
                float scaleY = clone.transform.localScale.y / cols;
                float scaleX = clone.transform.localScale.x / rows;
                clone.transform.localScale = new Vector3(scaleX, scaleY, 1);

                blocks[y,x] = clone.transform;
            }
        }
    }

    private IEnumerator BlockDown()
    {
        float speed;

        while ( true )
        {
            if ( state == TetrisState.Finished ) break;

            if ( state != TetrisState.Paused )
            { 
                state = ((ScoreTetris)board).Accept('s');
            }

            speed = downSpeed * Mathf.Pow(1.2f, (Score / 100000));

            yield return new WaitForSeconds(1/speed);
        }
    }

    /// <summary>
    /// ������� ���� �ܼ� ���
    /// </summary>
    private void DrawConsole()
    {
        int dy = board.oScreen.Get_dy();
        int dx = board.oScreen.Get_dx();
        int[,] array = ((ScoreTetris)board).oCScreen.Get_array();

        string screen = "";
        for ( int y=0; y<dy-dw+1; ++y )
        {
            string line = "";
            for ( int x=dw-1; x<dx-dw+1; ++x )
            {
                if (array[y,x] == 0)
                    line += "0";
                else if ( array[y,x]==1 && x>dw-1 && x<dx-dw && y<dy-dw ) // I
                    line += "1";
                else if (array[y,x] == 2) // J
                    line += "2";
                else if (array[y,x] == 3) // L
                    line += "3";
                else if (array[y,x] == 4) // O
                    line += "4";
                else if (array[y,x] == 5) // S
                    line += "5";
                else if (array[y,x] == 6) // T
                    line += "6";
                else if (array[y,x] == 7) // Z
                    line += "7";
                else // if (array[y,x] == 8) // wall
                    line += "X";
            }
            screen += line + "\n";
        }
        print(screen);
    }

    private void DrawScreen()
    {
        int dy = board.oScreen.Get_dy();
        int dx = board.oScreen.Get_dx();
        int[,] array = ((ScoreTetris)board).oCScreen.Get_array();

        for ( int y=0; y<cols; ++y )
        {
            for ( int x=0; x<rows; ++x )
            {
                if ( array[y,x+dw] == 0 )
                {
                    blocks[y,x].GetComponent<SpriteRenderer>().color = Color.black;
                }
                else if ( array[y,x+dw] == 1 )
                {
                    blocks[y,x].GetComponent<SpriteRenderer>().color = Color.blue;
                }
                else if ( array[y,x+dw] == 2 )
                {
                    blocks[y,x].GetComponent<SpriteRenderer>().color = Color.cyan;
                }
                else if ( array[y,x+dw] == 3 )
                {
                    blocks[y,x].GetComponent<SpriteRenderer>().color = Color.green;
                }
                else if ( array[y,x+dw] == 4 )
                {
                    blocks[y,x].GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                else if ( array[y,x+dw] == 5 )
                {
                    blocks[y,x].GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.75f, 0.8f); // Pink
                }
                else if ( array[y,x+dw] == 6 )
                {
                    blocks[y,x].GetComponent<SpriteRenderer>().color = new Color(0.57f, 0.17f, 0.93f); // Purple
                }
                else if ( array[y,x+dw] == 7 )
                {
                    blocks[y,x].GetComponent<SpriteRenderer>().color = Color.red;
                }
                else
                {
                    blocks[y, x].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }

    private void NewBlock()
    {
        // ���Ƿ� �� Block ���� (key�� ����)
        char key = (char)('0' + Random.Range(0, setOfBlockArrays.maxBlkTypes));
        state = ((ScoreTetris)board).Accept(key);
    }

    private void ClearLine()
    {
        int line;
        while ( (line = ((ScoreTetris)board).ClearLine()) != -1 )
        {
            float y = blocks[line, 0].transform.position.y;
            y += (y - blocks[line-1, 0].transform.position.y)/2;
            Vector3 position = new Vector3(0, y, 0);
            Instantiate(lineEffect, position, Quaternion.identity);
        }

        // ���� ����
        Score = ((ScoreTetris)board).Score;
    }

    private void GameOver()
    {
        // ���� ������������ ȹ���� ���� ���� ����
        PlayerPrefs.SetInt("CurrentScore", Score);

        // "GameOver" ������ �̵�
        SceneManager.LoadScene("GameOver");
    }

    public void ControlBlock(string key)
    {
        if (state == TetrisState.Paused) return;

        state = ((ScoreTetris)board).Accept(key[0]);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Pause()
    {
        if (state == TetrisState.Paused)
        {
            state = TetrisState.Running;
            panel.SetActive(false);
        }
        else
        {
            state = TetrisState.Paused;
            panel.SetActive(true);
        }
    }
}
