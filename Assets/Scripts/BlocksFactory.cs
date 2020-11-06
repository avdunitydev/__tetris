using System;
using UnityEngine;

namespace AVDTetris
{
    static class BlocksFactory
    {
        private static GameObject m_prefab;

        public static BaseBlock CreateBlock(GameObject prefab)
        {
            m_prefab = prefab;
            return RandomBlock();
        }

        private static BaseBlock RandomBlock()
        {
            int n = new System.Random().Next(Enum.GetValues(typeof(E_BlockType)).Length);

            switch (n)
            {
                case 0:
                    return new Block_I(m_prefab);
                case 1:
                    return new Block_J(m_prefab);
                case 2:
                    return new Block_L(m_prefab);
                case 3:
                    return new Block_O(m_prefab);
                case 4:
                    return new Block_S(m_prefab);
                case 5:
                    return new Block_T(m_prefab);
                case 6:
                    return new Block_Z(m_prefab);
                default:
                    return new Block_O(m_prefab);
            }

        }
    }
}