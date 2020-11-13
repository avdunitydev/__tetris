using System;
using UnityEngine;

namespace AVDTetris
{
    static class BlocksFactory
    {
        static GameObject m_Prefab;

        public static BaseBlock CreateBlock(E_BlockType blockType, GameObject elementPrefab)
        {
            m_Prefab = elementPrefab;

            return SelectBlock(blockType);

        }

        public static BaseBlock RandomBlock(GameObject elementPrefab)
        {
            m_Prefab = elementPrefab;

            int n = new System.Random().Next(Enum.GetValues(typeof(E_BlockType)).Length);

            return SelectBlock((E_BlockType)n);

        }

        private static BaseBlock SelectBlock(E_BlockType blockType)
        {
            switch (blockType)
            {
                case (E_BlockType)0:
                    return new Block_I(m_Prefab);
                case (E_BlockType)1:
                    return new Block_J(m_Prefab);
                case (E_BlockType)2:
                    return new Block_L(m_Prefab);
                case (E_BlockType)3:
                    return new Block_O(m_Prefab);
                case (E_BlockType)4:
                    return new Block_S(m_Prefab);
                case (E_BlockType)5:
                    return new Block_T(m_Prefab);
                case (E_BlockType)6:
                    return new Block_Z(m_Prefab);
                default:
                    return new Block_I(m_Prefab);
            }
        }


    }
}