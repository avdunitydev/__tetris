using System.Threading;
using UnityEngine;

namespace AVDTetris
{
    public class GameController : MonoBehaviour
    {
        //Public Members
        public GameObject m_PrefabElement;

        //Local Members
        GameObject[,] m_Glass;

        private BaseBlock NextBlock
        {
            get;
            set;
        }
        BaseBlock m_currentBlock;
        GameObject m_blockWrapper;
        int m_gameSpeed;
        float m_counter;


        float GlobalTimer() => Time.deltaTime * m_gameSpeed;

        private void InitGame()
        {
            m_Glass = new GameObject[10, 26];
            m_gameSpeed = 2;
            m_counter = 0;
            NextBlock = BlocksFactory.CreateBlock(m_PrefabElement);

        }

        private void InstansBlock()
        {
            m_currentBlock = NextBlock;

            m_blockWrapper = new GameObject { name = "Block" };

            m_currentBlock.SetBlockParent(m_blockWrapper.transform);

            GameObject[,] block = m_currentBlock.GetBlock();
            m_currentBlock.GetBlockPivot(out float x, out float y);
            m_blockWrapper.transform.position = new Vector3(-x, -y + (m_Glass.Length / m_Glass.GetLength(0)) - 2, 0);

            NextBlock = BlocksFactory.CreateBlock(m_PrefabElement);


        }




        void Start()
        {
            InitGame();

            InstansBlock();

        }

        void Update()
        {
            m_counter += GlobalTimer();

            //if (m_counter < 4) MoveBlockDown();
            //else m_counter = 0;

            if (Input.GetKeyDown(KeyCode.R)) m_currentBlock.RotateBlock();

        }

    }
}