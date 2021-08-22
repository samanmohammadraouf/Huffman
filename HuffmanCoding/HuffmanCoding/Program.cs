using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HuffmanCoding
{
    public class BinaryTree
    {
        public BinaryTree LeftTree;
        public BinaryTree RightTree;
        public string characters;
        public int CountOfRepitions;
        public string path = null;

    }
    class Program
    {
        static string GetTextFromFile(string address)
        {
            StreamReader TxtFile = new StreamReader(address + ".txt");
            string txtString = "";
            while(!TxtFile.EndOfStream)
            {
                txtString = txtString + TxtFile.ReadLine();
            }
            return txtString;
        }


        class CharacterCodes
        {
            public char character;
            public string code;
        }
        static List<CharacterCodes> Codes = new List<CharacterCodes>();
        static string GetTextFromCMPFile(string name)
        {
            StreamReader TXTfile = new StreamReader(name + ".cmp");
            string BinaryCode = "";
            while(!TXTfile.EndOfStream)
            {
                string line = TXTfile.ReadLine();
                char[] Line = line.ToCharArray();
                CharacterCodes newCode = new CharacterCodes();
                newCode.character = Line[0];
                string code = "";
                for (int i = 1; i < Line.Length; i++)
                {
                    code = code + Line[i].ToString();
                }
                newCode.code = code;
                Codes.Add(newCode);
                if(TXTfile.EndOfStream)
                {
                    BinaryCode = line;
                }
            }
            Codes.RemoveAt(Codes.Count - 1);
            return BinaryCode;
        }

        static string Decode(string BinCode)
        {
            string Decode = "";
            char[] BinCodeArr = BinCode.ToCharArray();
            int i = 0;
            while (i<BinCodeArr.Length)
            {
                string CurrentCode = BinCodeArr[i].ToString();
                for(int j=0;j<Codes.Count;j++)
                {
                    if(Codes[j].code==CurrentCode)
                    {
                        Decode = Decode + Codes[j].character;
                        i++;
                        break;
                    }
                    else if(j==Codes.Count-1)
                    {
                        i++;
                        CurrentCode = CurrentCode + BinCodeArr[i].ToString();
                        j = -1;
                    }
                }
            }
            return Decode;
        }
        static void Main(string[] args)

        {
            Console.WriteLine("press 1 or 2:");
            Console.WriteLine("press 1 if you want to compress a text file.");
            Console.WriteLine("press 2 If you want to decode a file");
            string choice = Console.ReadLine();
            while(choice!="1"&&choice!="2")
            {
                Console.WriteLine("wrong input!");
                Console.WriteLine("press 1 or 2:");
                Console.WriteLine("press 1 if you want to compress a text file.");
                Console.WriteLine("press 2 If you want to decode a file");
                choice = Console.ReadLine();
            }
            if(choice=="1")
            {
                Console.WriteLine("Enter the name of the txt file: ");
                string address = Console.ReadLine();
                string TxtStr = GetTextFromFile(address);
                List<ContentOfLeaves> InitialElements = InitaialNodes(TxtStr);
                ImplementationLeavesOfTree(InitialElements);
                List<int> finalPath = new List<int>();
                while (currentBinTreeNodesList.Count > 1)
                {
                    NextNode();
                }
                BinTreeNodesList = Sorting(BinTreeNodesList);
                PreOrder(BinTreeNodesList[0], finalPath, 0);
                PrintCharPath(BinTreeNodesList);
                MakingCode(BinTreeNodesList, TxtStr);
            }
            else
            {
                Console.WriteLine("Enter the name of the txt file: ");
                string name = Console.ReadLine();
                string BinCode = GetTextFromCMPFile(name);
                string decodeStr = Decode(BinCode);
                Console.WriteLine(decodeStr);
                StreamWriter DecodeTXTFile = new StreamWriter("DecodeTxtFile.txt");
                DecodeTXTFile.WriteLine(decodeStr);
                DecodeTXTFile.Close();
                Console.WriteLine("the Decompressed txt file created and saved.");
                Console.ReadKey();
            }
            
        }
        public class ContentOfLeaves
        {
            public int count;
            public char character;
        }
        static List<ContentOfLeaves> InitaialNodes(string TxtStr)
        {
            //count repitions of each character
            //than making a new node of BinaryTree 
            //these nodes are leaves
            char[] charactersOfText;
            charactersOfText = TxtStr.ToCharArray();
            List<ContentOfLeaves> elements = new List<ContentOfLeaves>();
            for(int i=0;i<charactersOfText.Length;i++)
            {
                if(i==0)
                {
                    ContentOfLeaves newLeafContent = new ContentOfLeaves();
                    newLeafContent.character = charactersOfText[i];
                    newLeafContent.count = 1;
                    elements.Add(newLeafContent);
                    continue;
                }
                for(int j=0;j<elements.Count;j++)
                {
                    if(elements[j].character==charactersOfText[i])
                    {
                        elements[j].count = elements[j].count + 1;
                        break;
                    }
                    if(j==elements.Count-1)
                    {
                        ContentOfLeaves NewLeafContent = new ContentOfLeaves();
                        NewLeafContent.count = 1;
                        NewLeafContent.character = charactersOfText[i];
                        elements.Add(NewLeafContent);
                        break;
                    }
                }
            }
            return elements;
        }

        static List<BinaryTree> BinTreeNodesList = new List<BinaryTree>();
        static List<BinaryTree> currentBinTreeNodesList = new List<BinaryTree>();
        //this function make a bintree and add the leaves to the tree.
        static void ImplementationLeavesOfTree(List<ContentOfLeaves> ContentOfLeaves)
        {
            for(int i=0;i<ContentOfLeaves.Count;i++)
            {
                BinaryTree NewBinTreeNode = new BinaryTree();
                NewBinTreeNode.LeftTree = null;
                NewBinTreeNode.RightTree = null;
                NewBinTreeNode.characters = ContentOfLeaves[i].character.ToString();
                NewBinTreeNode.CountOfRepitions = ContentOfLeaves[i].count;

                BinTreeNodesList.Add(NewBinTreeNode);
                currentBinTreeNodesList.Add(NewBinTreeNode);
            }
        }
        //this function find the next node from List OF nodes.
        static void NextNode()
        {
            //kochek tarin va yeki gablesh ro bara sakht node jadid niaz darim .
            
            int min = Int32.MaxValue;
            int minIndex = 0;
            int SecondMin = min;
            int SecondMinIndex = minIndex;
            BinaryTree Node1;
            BinaryTree Node2;

            for (int i=0;i< currentBinTreeNodesList.Count;i++)
            {
                if(currentBinTreeNodesList[i].CountOfRepitions<min)
                {
                    min = currentBinTreeNodesList[i].CountOfRepitions;
                    minIndex = i;
                }
            }
            Node1 = currentBinTreeNodesList[minIndex];
            currentBinTreeNodesList.RemoveAt(minIndex);

            for(int i=0;i< currentBinTreeNodesList.Count;i++)
            {
                if(currentBinTreeNodesList[i].CountOfRepitions<SecondMin)
                {
                    SecondMin = currentBinTreeNodesList[i].CountOfRepitions;
                    SecondMinIndex = i;
                }
            }
            Node2 = currentBinTreeNodesList[SecondMinIndex];
            currentBinTreeNodesList.RemoveAt(SecondMinIndex);

            //now making the new node 
            BinaryTree newNode = new BinaryTree();
            newNode.characters = Node1.characters + Node2.characters;
            newNode.CountOfRepitions = Node1.CountOfRepitions + Node2.CountOfRepitions;
            newNode.LeftTree = Node1;
            newNode.RightTree = Node2;

            //adding the new node to currentBinTreeNodesList and BinTreeNodesList
            currentBinTreeNodesList.Add(newNode);
            
            for(int i=0;i<BinTreeNodesList.Count;i++)
            {
                if(BinTreeNodesList[i].characters==Node1.characters&&BinTreeNodesList[i].CountOfRepitions==Node1.CountOfRepitions)
                {
                    newNode.LeftTree = BinTreeNodesList[i];
                }
                if(BinTreeNodesList[i].characters == Node2.characters && BinTreeNodesList[i].CountOfRepitions == Node2.CountOfRepitions)
                {
                    newNode.RightTree = BinTreeNodesList[i];
                }
            }

            BinTreeNodesList.Add(newNode);
        }

        static List<BinaryTree> Sorting(List<BinaryTree> BinTreeNodesList)
        {
            BinaryTree temp = new BinaryTree();
            for (int i = 0; i < BinTreeNodesList.Count; i++)
            {
                for (int j = 0; j < BinTreeNodesList.Count; j++)
                {
                    if (BinTreeNodesList[j].CountOfRepitions < BinTreeNodesList[i].CountOfRepitions)
                    {
                        temp = BinTreeNodesList[j];
                        BinTreeNodesList[j] = BinTreeNodesList[i];
                        BinTreeNodesList[i] = temp;
                    }
                }
            }

            return BinTreeNodesList;
        }

        static void PreOrder(BinaryTree LastNode, List<int> pathList, int index)
        {
            if (LastNode.RightTree == null && LastNode.LeftTree == null)
            {
                string finalPath = "";
                for (int i = 0; i < pathList.Count; i++)
                {
                    finalPath = finalPath + pathList[i].ToString();
                }
                LastNode.path = finalPath;
            }

            if (LastNode.LeftTree != null)
            {
                pathList.Add(0);
                PreOrder(LastNode.LeftTree, pathList, index + 1);
                pathList.RemoveAt(index);
            }

            if (LastNode.RightTree != null)
            {
                pathList.Add(1);
                PreOrder(LastNode.RightTree, pathList, index + 1);
                pathList.RemoveAt(index);
            }

        }

        static void MakingCode(List<BinaryTree> BinTreeNodesList, string TxtStr)
        {
            char[] characterArray;
            characterArray = TxtStr.ToCharArray();
            StreamWriter answerFile = new StreamWriter("answer.cmp", true);
            for (int i = 0; i < characterArray.Length; i++)
            {
                for (int j = 0; j < BinTreeNodesList.Count; j++)
                {
                    if (BinTreeNodesList[j].characters == characterArray[i].ToString())
                    {
                        answerFile.Write(BinTreeNodesList[j].path);
                    }
                }
            }
            answerFile.Close();
        }

        static void PrintCharPath(List<BinaryTree> BinTreeNodesList)
        {
            StreamWriter answerFile = new StreamWriter("answer.cmp", true);
            for (int i = 0; i < BinTreeNodesList.Count; i++)
            {
                if (BinTreeNodesList[i].RightTree == null && BinTreeNodesList[i].LeftTree == null)
                {
                    answerFile.WriteLine(BinTreeNodesList[i].characters + BinTreeNodesList[i].path);
                }
            }
            answerFile.Close();
        }
    }
}
