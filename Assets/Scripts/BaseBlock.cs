
using System;
using UnityEngine;

namespace AVDTetris
{
    abstract class BaseBlock : IBlock
    {
        public const float BLOCK_SIZE = 1f;
        public float AnchorX { get; private set; }
        public float AnchorY { get; private set; }
        public int BlockWidth { get; private set; }
        public int BlockHeight { get; private set; }
        public Transform Parent { get; private set; }

        private GameObject[,] m_Block;
        private readonly GameObject prefab;
        public E_BlockType BlockType { get; private set; }



        public BaseBlock(E_BlockType blockType, GameObject prefab)
        {
            BlockType = blockType;
            this.prefab = prefab;
        }

        protected abstract int[,] GetProtoMatrix();

        public void GetBlockPivot(out float x, out float y)
        {
            x = AnchorX;
            y = AnchorY;
        }

        public void GetBlockSize(out int width, out int height)
        {
            width = BlockWidth;
            height = BlockHeight;
        }

        public void SetBlockPivot()
        {
            int l = m_Block.GetLength(0);
            for (int i = 0; i < l; i++)
            {
                int k = 0;
                for (int j = 0; j < l; j++)
                {
                    if (m_Block[j, i] != null) ++k;
                }
                if (k != 0)
                {
                    AnchorX = i;
                    break;
                }
            }


            for (int i = 0; i < m_Block.GetLength(0); i++)
            {
                int k = 0;
                for (int j = 0; j < l; j++)
                {
                    if (m_Block[i, j] != null) ++k;
                }
                if (k != 0)
                {
                    AnchorY = i;
                    break;
                }
            }

        }

        public void SetBlockSize()
        {
            int l = m_Block.GetLength(0);
            BlockWidth = m_Block.GetLength(0);

            for (int i = 0; i < l; i++)
            {
                int k = 0;
                for (int j = 0; j < l; j++)
                {
                    if (m_Block[j, i] != null) ++k;
                }
                if (k == 0)
                {
                    --BlockWidth;
                }
            }

            BlockHeight = m_Block.GetLength(0);
            for (int i = 0; i < m_Block.GetLength(0); i++)
            {
                int k = 0;
                for (int j = 0; j < l; j++)
                {
                    if (m_Block[i, j] != null) ++k;
                }
                if (k == 0)
                {
                    --BlockHeight;
                }
            }

        }

        private void InstansElements(Transform parent)
        {
            int[,] matrix = GetProtoMatrix();
            int l = matrix.GetLength(0);
            m_Block = new GameObject[l, l];

            for (int i = 0; i < l; ++i)
            {
                for (int j = 0; j < l; ++j)
                {
                    if (matrix[i, j] == 1)
                    {
                        m_Block[i, j] = GameObject.Instantiate(prefab, new Vector3(j * BLOCK_SIZE, i * BLOCK_SIZE, 0), Quaternion.identity, parent);
                    }
                }
            }

            SetBlockPivot();
            SetBlockSize();
            Debug.Log($"AnchorX: {AnchorX}, AnchorY: {AnchorY}, BlockWidth: {BlockWidth}, BlockHeight: {BlockHeight}");

        }

        public void RotateBlock()
        {
            int l = m_Block.GetLength(0);
            GameObject[,] temp = new GameObject[l, l];

            for (int i = 0; i < l; ++i)
            {
                for (int j = 0; j < l; ++j)
                {
                    temp[i, j] = m_Block[l - j - 1, i];
                    if(m_Block[i, j] != null) m_Block[i, j].transform.localPosition = new Vector3(l - j - 1, i, 0);
                }
            }

            m_Block = temp;

            SetBlockPivot();
            SetBlockSize();
            Debug.Log($"AnchorX: {AnchorX}, AnchorY: {AnchorY}, BlockWidth: {BlockWidth}, BlockHeight: {BlockHeight}");



        }

        public GameObject[,] GetBlock()
        {
            if (m_Block == null)
            {
                if (Parent == null) Debug.Log("Property Parent is Null. Set the Parent property.");
                InstansElements(Parent);
            }
            return m_Block;
        }

        public void SetBlockParent(Transform parent) => Parent = parent;


    }
}
