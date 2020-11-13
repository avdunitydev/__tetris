using System;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace AVDTetris
{
    public class GameController : MonoBehaviour
    {
        //Public Members
        public GameObject m_PrefabElement;
        public Text m_Score_textUI;
        public Text m_SpeedLevel_textUI;
        public Text m_GameStatus_textUI;
        public Transform m_NextBlockShowPoint;
        

        //Local Members
        private GameObject[,] m_Glass;

        private BaseBlock CurrentBlock { get; set; }
        private BaseBlock m_Next_Block;

        private int m_GameScore;

        private int m_GameSpeed;
        private const int GAME_SPEED = 1;

        private int m_FastMove;
        private const int FAST_MOVE = 50;

        private float m_MoveCounter;
        private const float MOVE_COUNTER = 4;

        [SerializeField]
        private float m_LevelCounter;
        private const float LEVEL_COUNTER = 100;

        private const int GLASS_SIZE_Y = 26;
        private const int GLASS_SIZE_X = 10;


        private BaseBlock NextBlock() => BlocksFactory.RandomBlock(m_PrefabElement);
        private float GlobalTimer() => Time.deltaTime * m_GameSpeed * 2;

        private void InitGame()
        {
            m_Glass = new GameObject[GLASS_SIZE_Y, GLASS_SIZE_X];
            m_GameSpeed = GAME_SPEED;
            m_MoveCounter = MOVE_COUNTER;
            m_LevelCounter = LEVEL_COUNTER;
            m_GameScore = 0;
            m_FastMove = 1;
            m_Score_textUI.text = $"{m_GameScore}";
            m_GameStatus_textUI.enabled = false;
            m_SpeedLevel_textUI.text = $"Speed level: {m_GameSpeed}";

            m_Next_Block = NextBlock();

        }


        private float GetPhysical_X(int x) => x * CurrentBlock.BLOCK_SIZE;

        private float GetPhysical_Y(int y) => (++y - m_Glass.GetLength(0)) * CurrentBlock.BLOCK_SIZE * -1;

        private void InstantiateBlock()
        {
            CurrentBlock = m_Next_Block;

            CurrentBlock.del_SetRealPosition_X = GetPhysical_X;
            CurrentBlock.del_SetRealPosition_Y = GetPhysical_Y;

            int instantiatePoint_X = UnityEngine.Random.Range(0, m_Glass.GetLength(1) - CurrentBlock.Width);
            int instantiatePoint_Y = 0;

            if (!IsCollision(instantiatePoint_X, instantiatePoint_Y, CurrentBlock.GetBlock())) CurrentBlock.SetPosition(instantiatePoint_X, instantiatePoint_Y);
            else GameOver();

            m_Next_Block = NextBlock();
            m_Next_Block.ShowNextBlock(m_NextBlockShowPoint);
        }

        private void GameOver()
        {
            Time.timeScale = 0;

            m_GameStatus_textUI.text = "GAME OVER";
            //m_GameStatus_textUI.color = Color.Lerp(new Color32(32, 42, 52, 255), new Color32(62, 82, 102, 255), 2);
            m_GameStatus_textUI.enabled = true;
        }

        private void RotateBlock()
        {
            if (!IsCollision(CurrentBlock.PositionX, CurrentBlock.PositionY, CurrentBlock.GetRotatedBlock())) CurrentBlock.RotateBlock();
        }

        #region MOVE block
        private void MoveRight()
        {
            if (!IsCollision(CurrentBlock.PositionX + 1, CurrentBlock.PositionY, CurrentBlock.GetBlock())) CurrentBlock.MoveRight();
        }

        void MoveLeft()
        {
            if (!IsCollision(CurrentBlock.PositionX - 1, CurrentBlock.PositionY, CurrentBlock.GetBlock())) CurrentBlock.MoveLeft();

        }

        private void MoveDown()
        {
            if (!IsCollision(CurrentBlock.PositionX, CurrentBlock.PositionY + 1, CurrentBlock.GetBlock())) CurrentBlock.MoveDown();
            else
            {
                m_FastMove = 1;
                CopyBlock();

                InstantiateBlock();
            }
        }

        private void MoveDownFast()
        {
            m_FastMove = FAST_MOVE;
            MoveDown();
        }

        #endregion

        void CopyBlock()
        {
            GameObject[,] tmp_block = CurrentBlock.GetBlock();
            int l = tmp_block.GetLength(0);
            int x = CurrentBlock.PositionX;
            int y = CurrentBlock.PositionY;

            for (int i = 0; i < l; ++i)
            {
                for (int j = 0; j < l; ++j)
                {

                    if (tmp_block[i, j])
                    {
                        m_Glass[y + i, x + j] = tmp_block[i, j];
                        m_Glass[y + i, x + j].GetComponent<SpriteRenderer>().color = new Color32(85, 120, 155, 255);
                    }
                }
            }

            CheckLines();
            printMatrix(m_Glass);
        }

        private void CheckLines()
        {
            int h = m_Glass.GetLength(0);
            int w = m_Glass.GetLength(1);

            for (int i = 0; i < h; ++i)
            {
                int k = 0;
                for (int j = 0; j < w; ++j)
                {
                    if (m_Glass[i, j]) ++k;
                }
                if (k == w)
                {
                    ++m_GameScore;
                    m_Score_textUI.text = $"{m_GameScore}";

                    for (int ii = 0; ii < w; ++ii)
                    {
                        Destroy(m_Glass[i, ii].gameObject);
                    }

                    for (int ii = i; ii >= 0; --ii)
                    {
                        for (int jj = 0; jj < w; ++jj)
                        {
                            if (ii != 0)
                            {
                                m_Glass[ii, jj] = m_Glass[ii - 1, jj];
                                if (m_Glass[ii, jj]) m_Glass[ii, jj].transform.position += Vector3.down;
                            }
                            else m_Glass[ii, jj] = null;

                        }

                    }

                }
            }
        }

        bool IsCollision(int next_X, int next_Y, GameObject[,] nextBlock)
        {
            int l = nextBlock.GetLength(0);
            int gyl = m_Glass.GetLength(0);
            int gxl = m_Glass.GetLength(1);

            for (int i = 0; i < l; ++i)
            {
                for (int j = 0; j < l; ++j)
                {
                    if (nextBlock[i, j])
                    {
                        if (next_X + j < 0 || next_X + j >= gxl || next_Y + i >= gyl) return true;
                        else if (m_Glass[next_Y + i, next_X + j]) return true;
                    }
                }
            }
            return false;
        }


#if DEVELOPMENT_BUILD || UNITY_EDITOR
        void printMatrix(GameObject[,] matrix)
        {
            string str = "";
            int h = matrix.GetLength(0);
            int w = matrix.GetLength(1);

            for (int i = 0; i < h; ++i)
            {
                str += $"{i}_>";
                for (int j = 0; j < w; ++j)
                {
                    if (matrix[i, j]) str += ($"\t[X]");
                    else str += ($"\t ");
                }
                str += "\n";
            }
            Debug.Log(str);
            //Debug.LogForma
        }
#endif

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.UpArrow)) RotateBlock();
            if (Input.GetKeyDown(KeyCode.DownArrow)) MoveDownFast();
            if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow)) MoveRight();
            //if (Input.GetKeyDown(KeyCode.N)) InstantiateBlock();
        }

        void Start()
        {
            InitGame();

            InstantiateBlock();

        }

        void Update()
        {
            float t = GlobalTimer();
            m_MoveCounter -= t * m_FastMove;
            m_LevelCounter -= Time.deltaTime;

            if (m_MoveCounter < 0)
            {
                MoveDown();
                m_MoveCounter = MOVE_COUNTER;
            }
            if (m_LevelCounter < 0)
            {
                ++m_GameSpeed;
                m_SpeedLevel_textUI.text = $"Speed level: {m_GameSpeed}";
                m_LevelCounter = LEVEL_COUNTER;
            }

            CheckInput();

        }

    }
}