using UnityEngine;
using System.Collections;
using System;

namespace Tetris
{
    struct GlassSize
    {
        public int m_Column { get; private set; }
        public int m_Row { get; private set; }

        public GlassSize(int colstColumns, int colstRows)
        {
            this.m_Column = colstColumns;
            this.m_Row = colstRows;
        }
    }

    public class GameController : MonoBehaviour
    {
        public GameObject m_BlockPrefab;

        int[,] m_Matrix;

        GameObject[,] m_GameBlocks;

        GlassSize glass = new GlassSize(10, 26);

        float m_Timer = 6f;
        private float m_speed = 20f;

        void FillMatrixWithGameblocks()
        {
            m_GameBlocks = new GameObject[glass.m_Row, glass.m_Column];
            for (int r = 0; r < glass.m_Row; r++)
            {
                for (int c = 0; c < glass.m_Column; c++)
                {
                    m_GameBlocks[r, c] = GameObject.Instantiate(m_BlockPrefab);
                    m_GameBlocks[r, c].transform.position = new Vector3(c, glass.m_Row - 1 - r, 0);
                }
            }
        }

        private void InitMatrix()
        {
            m_Matrix = new int[glass.m_Row, glass.m_Column];
            for (int r = 0; r < glass.m_Row; r++)
            {
                for (int c = 0; c < glass.m_Column; c++)
                {
                    m_Matrix[r, c] = 0;
                }
            }
        }

        void Draw()
        {
            for (int r = 0; r < glass.m_Row; r++)
            {
                for (int c = 0; c < glass.m_Column; c++)
                {
                    if (m_Matrix[r, c] == 1) m_GameBlocks[r, c].gameObject.GetComponent<SpriteRenderer>().color = Color.white;

                    bool flag = (m_Matrix[r, c] > 0) ? true : false;
                    m_GameBlocks[r, c].gameObject.SetActive(flag);
                }
            }
        }

        void Replace()
        {
            for (int r = 0; r < glass.m_Row; r++)
            {
                for (int c = 0; c < glass.m_Column; c++)
                {
                    m_Matrix[r, c] = (m_Matrix[r, c] > 0) ? 2 : 0;
                    m_GameBlocks[r, c].gameObject.GetComponent<SpriteRenderer>().color = Color.red;

                }
            }

            CreateBlock();
        }


        void MoveDown()
        {
            int end_row = m_Matrix.GetLength(0) - 1;
            int[,] tmp = new int[glass.m_Row, glass.m_Column];

            for (int r = end_row; r >= 0; r--)
            {
                for (int c = 0; c < glass.m_Column; c++)
                {
                    if (r > 0)
                    {
                        if (m_Matrix[r, c] == 2 && m_Matrix[r - 1, c] == 1)
                        {
                            Replace();
                            return;
                        }
                    }

                    if (r == end_row && m_Matrix[r, c] == 1)
                    {
                        Replace();
                        return;
                    }

                    if (r < end_row)
                    {
                        if (m_Matrix[r, c] == 1)
                        {
                            tmp[r + 1, c] = 1;
                        }
                    }

                    if (m_Matrix[r, c] == 2)
                    {
                        tmp[r, c] = m_Matrix[r, c];
                    }

                }
            }
            m_Matrix = tmp;
        }

        private void MoveLeft()
        {
            throw new NotImplementedException();
        }

        private void MoveRight()
        {
            throw new NotImplementedException();
        }

        void CreateBlock()
        {
            int offsetPosition = 4;
            Block block = new Block();
            int l = block.m_BlockMatrix.GetLength(0);

            if(l < 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        m_Matrix[i, j + offsetPosition] = 0;
                    }
                }
            }

            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    m_Matrix[i, j + offsetPosition] = block.m_BlockMatrix[i, j];
                }
            }

        }


        void Start()
        {
            InitMatrix();
            FillMatrixWithGameblocks();

            CreateBlock();
        }

        void Update()
        {
            if (m_Timer > 0)
            {
                m_Timer -= Time.deltaTime * m_speed;
            }
            else
            {
                //MoveDown();
                m_Timer = 10f;
            }

            if (Input.GetKeyDown(KeyCode.B)) CreateBlock();

            if (Input.GetKeyDown(KeyCode.DownArrow)) for (int i = 0; i < 1; i++) MoveDown();

            if (Input.GetKeyDown(KeyCode.RightArrow)) MoveRight();

            if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveLeft();

            Draw();
        }


    }
}