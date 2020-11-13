
using System;
using UnityEngine;

namespace AVDTetris
{
    abstract class BaseBlock : IBlock
    {
        public readonly float BLOCK_SIZE = 1f;
        public float AnchorX { get; private set; }
        public float AnchorY { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public E_BlockType BlockType { get; private set; }


        private readonly GameObject m_Prefab;

        private GameObject[,] m_BlockMatrix;

        public GetPoint_Delegat del_SetRealPosition_X;
        public GetPoint_Delegat del_SetRealPosition_Y;


        public BaseBlock(E_BlockType blockType, GameObject prefab)
        {
            BlockType = blockType;
            m_Prefab = prefab;
            InstantiateElements();
        }

        protected abstract int[,] GetProtoMatrix();

        public void GetPivot(out float x, out float y)
        {
            x = AnchorX;
            y = AnchorY;

        }

        public void GetSize(out int width, out int height)
        {
            width = Width;
            height = Height;
        }

        public void SetPivot()
        {
            int l = m_BlockMatrix.GetLength(0);
            for (int i = 0; i < l; ++i)
            {
                int k = 0;
                for (int j = 0; j < l; ++j)
                {
                    if (m_BlockMatrix[j, i] != null) ++k;
                }
                if (k != 0)
                {
                    AnchorX = i;
                    break;
                }
            }

            for (int i = 0; i < l; ++i)
            {
                int k = 0;
                for (int j = 0; j < l; ++j)
                {
                    if (m_BlockMatrix[i, j] != null) ++k;
                }
                if (k != 0)
                {
                    AnchorY = i;
                    break;
                }
            }
            //Debug.Log($"ancX {AnchorX}, ancY {AnchorY}");

        }

        public void SetSize()
        {
            int l = m_BlockMatrix.GetLength(0);
            Width = m_BlockMatrix.GetLength(0);

            for (int i = 0; i < l; i++)
            {
                int k = 0;
                for (int j = 0; j < l; j++)
                {
                    if (m_BlockMatrix[j, i] != null) ++k;
                }
                if (k == 0)
                {
                    --Width;
                }
            }

            Height = m_BlockMatrix.GetLength(0);
            for (int i = 0; i < m_BlockMatrix.GetLength(0); i++)
            {
                int k = 0;
                for (int j = 0; j < l; j++)
                {
                    if (m_BlockMatrix[i, j] != null) ++k;
                }
                if (k == 0)
                {
                    --Height;
                }
            }


        }

        public void SetPosition(int x, int y)
        {
            PositionX = x;
            PositionY = y;

            RefreshUI();
        }

        public void ShowNextBlock(Transform showPoint)
        {
            int l = m_BlockMatrix.GetLength(0);
            for (int i = 0; i < l; ++i)
            {
                for (int j = 0; j < l; ++j)
                {
                    if (m_BlockMatrix[i, j])
                    {
                        m_BlockMatrix[i, j].transform.position = new Vector3(showPoint.position.x + j, (showPoint.position.y - i), 0);
                        m_BlockMatrix[i, j].GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
        }

        private void RefreshUI()
        {
            if (del_SetRealPosition_X == null) { Debug.Log("ERROR : GetPoint_Delegat del_SetRealPosition_X>>> Is't defined !!!"); return; }
            if (del_SetRealPosition_Y == null) { Debug.Log("ERROR : GetPoint_Delegat del_SetRealPosition_Y>>> Is't defined !!!"); return; }

            int l = m_BlockMatrix.GetLength(0);
            for (int i = 0; i < l; ++i)
            {
                for (int j = 0; j < l; ++j)
                {
                    if (m_BlockMatrix[i, j])
                    {
                        float x = del_SetRealPosition_X.Invoke(PositionX + j);
                        float y = del_SetRealPosition_Y.Invoke(PositionY + i);

                        m_BlockMatrix[i, j].transform.position = new Vector3(x, y, 0);

                        m_BlockMatrix[i, j].GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }

        }

        private void InstantiateElements()
        {
            if (m_BlockMatrix == null)
            {
                int[,] matrix = GetProtoMatrix();
                int l = matrix.GetLength(0);

                m_BlockMatrix = new GameObject[l, l];

                for (int i = 0; i < l; ++i)
                {
                    for (int j = 0; j < l; ++j)
                    {
                        if (matrix[i, j] == 1)
                        { m_BlockMatrix[i, j] = GameObject.Instantiate(m_Prefab); }
                    }
                }

                SetPivot();
                SetSize();
            }
        }

        public GameObject[,] GetBlock() => m_BlockMatrix;

        public void RotateBlock()
        {
            m_BlockMatrix = GetRotatedBlock();

            SetPivot();
            SetSize();

            RefreshUI();

        }

        public GameObject[,] GetRotatedBlock()
        {
            int l = m_BlockMatrix.GetLength(0);
            GameObject[,] rotatedBlock = new GameObject[l, l];

            for (int i = 0; i < l; ++i)
            {
                for (int j = 0; j < l; ++j)
                {
                    rotatedBlock[i, j] = m_BlockMatrix[l - j - 1, i];
                }
            }

            return rotatedBlock;
        }

        #region Moving Block
        public void MoveRight()
        {
            SetPosition(++PositionX, PositionY);
            //Debug.Log($"PositionX: {PositionX}");
        }

        public void MoveLeft()
        {
            SetPosition(--PositionX, PositionY);
            //Debug.Log($"PositionX: {PositionX}");

        }

        public void MoveDown()
        {
            SetPosition(PositionX, ++PositionY);
            //Debug.Log($"PositionY: {PositionY}");

        }

        #endregion


    }
}
