
using UnityEngine;

namespace AVDTetris
{
    class Block_J : BaseBlock
    {
        readonly int[,] prototype = new int[,] { 
            { 0, 1, 0 }, 
            { 0, 1, 0 }, 
            { 1, 1, 0 }};

        public Block_J(GameObject prefab) : base(E_BlockType.J, prefab) { }

        protected override int[,] GetProtoMatrix() => prototype;

    }
}
