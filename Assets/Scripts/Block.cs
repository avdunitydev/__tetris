using UnityEngine;
using System.Collections;

namespace Tetris
{
    public enum BlockType_enum
    {
        q, line, s, z, j, l, t
    }

    public class Block
    {
        private BlockType_enum m_Type;
        public int[,] m_BlockMatrix { get; private set; }

        public Block()
        {
            m_Type = (BlockType_enum)Random.Range(0, 6);
            InitBlock(m_Type);
        }

        int[,] InitBlock(BlockType_enum type)
        {
            switch (type)
            {
                case (BlockType_enum)0:
                    m_BlockMatrix = GenBlock_Q();
                    return RotateBlock(m_BlockMatrix);
                case (BlockType_enum)1:
                    m_BlockMatrix = GenBlock_Line();
                    return RotateBlock(m_BlockMatrix);
                case (BlockType_enum)2:
                    m_BlockMatrix = GenBlock_S();
                    return RotateBlock(m_BlockMatrix);
                case (BlockType_enum)3:
                    m_BlockMatrix = GenBlock_Z();
                    return RotateBlock(m_BlockMatrix);
                case (BlockType_enum)4:
                    m_BlockMatrix = GenBlock_J();
                    return RotateBlock(m_BlockMatrix);
                case (BlockType_enum)5:
                    m_BlockMatrix = GenBlock_L();
                    return RotateBlock(m_BlockMatrix);
                case (BlockType_enum)6:
                    m_BlockMatrix = GenBlock_T();
                    return RotateBlock(m_BlockMatrix);
                default:
                    m_BlockMatrix = GenBlock_Q();
                    return RotateBlock(m_BlockMatrix);
            }
        }

        private int [,] GenBlock_T()
        {
            return new int[3, 3]{
                { 1, 1, 1},
                { 0, 1, 0},
                { 0, 0, 0}};
        }

        private int[,] GenBlock_L()
        {
            return new int[3, 3]{
                { 0, 1, 0},
                { 0, 1, 0},
                { 0, 1, 1}};
        }

        private int[,] GenBlock_J()
        {
            return new int[3, 3]{
                { 0, 1, 0},
                { 0, 1, 0},
                { 1, 1, 0}};
        }

        private int[,] GenBlock_Z()
        {
            return new int[3, 3]{
                { 1, 1, 0},
                { 0, 1, 1},
                { 0, 0, 0}};
        }

        private int[,] GenBlock_S()
        {
            return new int[3, 3]{
                { 0, 1, 1},
                { 1, 1, 0},
                { 0, 0, 0}};
        }

        private int[,] GenBlock_Line()
        {
            return new int[4, 4]{
                { 1, 1, 1, 1 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }};
        }

        private int[,] GenBlock_Q()
        {
            return new int[2, 2]{
                { 1, 1},
                { 1, 1}};
        }

        private int[,] RotateBlock(int[,] block)
        {
            int l = block.GetLength(0);
            int[,] t = new int[l, l];
            int randomTurn = Random.Range(1, 4);

            for (int r = 0; r < randomTurn; r++)
            {
                for (int i = 0; i < l; i++)
                {
                    for (int j = 0; j < l; j++)
                    {
                        t[i, j] = block[l - j - 1, i];
                    }
                }
                block = t;
            }
            return t;
        }



    }
}