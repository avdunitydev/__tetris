using UnityEngine;
using System.Collections;

namespace Tetris
{
    public enum enum_TypeElement
    {
        q, line, s, z, j, l, t
    }
    public class Element
    {
        enum_TypeElement elType;
        public int[,] m_Element { get; private set; }

        public Element()
        {
            elType = (enum_TypeElement)Random.Range(0, 6);
            InitElement(elType);
        }

        public void InitElement(enum_TypeElement type)
        {
            switch (type)
            {
                case (enum_TypeElement)0:
                    GenQElement();
                    break;
                case (enum_TypeElement)1:
                    GenLineElement();
                    break;
                case (enum_TypeElement)2:
                    GenSElement();
                    break;
                case (enum_TypeElement)3:
                    GenZElement();
                    break;
                case (enum_TypeElement)4:
                    GenJElement();
                    break;
                case (enum_TypeElement)5:
                    GenLElement();
                    break;
                case (enum_TypeElement)6:
                    GenTElement();
                    break;
                default:
                    GenQElement();
                    break;
            }
        }

        private void GenTElement()
        {
            m_Element = new int[3, 3]{
                { 1, 1, 1},
                { 0, 1, 0},
                { 0, 0, 0}};
        }

        private void GenLElement()
        {
            m_Element = new int[3, 3]{
                { 0, 1, 0},
                { 0, 1, 0},
                { 0, 1, 1}};
        }

        private void GenJElement()
        {
            m_Element = new int[3, 3]{
                { 0, 1, 0},
                { 0, 1, 0},
                { 1, 1, 0}};
        }

        private void GenZElement()
        {
            m_Element = new int[3, 3]{
                { 1, 1, 0},
                { 0, 1, 1},
                { 0, 0, 0}};
        }

        private void GenSElement()
        {
            m_Element = new int[3, 3]{
                { 0, 1, 1},
                { 1, 1, 0},
                { 0, 0, 0}};
        }

        private void GenLineElement()
        {
            m_Element = new int[4, 4]{
                { 1, 1, 1, 1 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }};
        }

        private void GenQElement()
        {
            m_Element = new int[2, 2]{
                { 1, 1},
                { 1, 1}};
        }

        int[,] RotateElement(int[,] element)
        {
            int l = element.GetLength(0);
            int[,] t = new int[l, l];
            int randomTurn = Random.Range(0,3);

            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < l; j++)
                {

                    return t;
                }
            }
            return t;
        }


    }
}