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

        GameObject[,] m_Blocks;

        GlassSize glass = new GlassSize(10, 26);

        void FillBlocksMatrix()
        {
            m_Blocks = new GameObject[glass.m_Row, glass.m_Column];
            for (int r = 0; r < glass.m_Row; r++)
            {
                for (int c = 0; c < glass.m_Column; c++)
                {
                    m_Blocks[r, c] = GameObject.Instantiate(m_BlockPrefab);
                    m_Blocks[r, c].transform.position = new Vector3(c, glass.m_Row - 1 - r, 0);
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
                    bool flag = (m_Matrix[r, c] > 0) ? true : false;
                    m_Blocks[r, c].gameObject.SetActive(flag);
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
                    m_Blocks[r, c].gameObject.GetComponent<SpriteRenderer>().color = Color.red;

                }
            }


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


        void startF()
        {
            m_Matrix[0, 2] = 1;
            m_Matrix[0, 3] = 1;
            m_Matrix[0, 4] = 1;
            m_Matrix[1, 3] = 1;
            // m_Matrix[21, 1] = 1;
            // m_Matrix[22, 1] = 1;
            // m_Matrix[23, 1] = 1;
            // m_Matrix[24, 1] = 1;
            // m_Matrix[24, 4] = 1;
            // m_Matrix[24, 5] = 1;
            // m_Matrix[24, 6] = 1;
            // m_Matrix[24, 7] = 1;

        }

        void Start()
        {
            InitMatrix();
            FillBlocksMatrix();

            startF();
        }

        void Update()
        {

            if (Input.GetKeyDown("down"))
            {
                MoveDown();
            }

            Draw();
        }
    }
}