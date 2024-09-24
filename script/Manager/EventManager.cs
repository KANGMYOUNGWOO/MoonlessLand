using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Table
{
   
    #region newTable

    public class TableClass
    {
        private int[] sub = new int[4];
        private int[] mid = new int[4];
        private int[] pro = new int[4];

        private int[,] data = new int[,] { {0,0,0,0 } , {0,0,0,0 },{0,0,0,0} };
        private bool[,] exception = new bool[,] { {false,false,false,false },{ false, false, false, false }, { false, false, false, false }};
        public TableClass(int sub1, int sub2 , int sub3 , int sub4 ,int mid1 ,int mid2 ,int mid3 , int mid4, int pro1, int pro2, int pro3, int pro4)
        {
            sub[0] = sub1;
            sub[1] = sub2;
            sub[2] = sub3;
            sub[3] = sub4;

            mid[0] = mid1;
            mid[1] = mid2;
            mid[2] = mid3;
            mid[3] = mid4;

            pro[0] = pro1;
            pro[1] = pro2;
            pro[2] = pro3;
            pro[3] = pro4;
            

        }

        public int subGet(int index)
        {
            return sub[index];
        }

        public int midGet(int index)
        {
            return mid[index];
        }

        public int proGet(int index)
        {
            return pro[index];
        }

        public int dataGet(int index1,int index2)
        {
            return data[index1, index2];
        }

        public void dataSet(int index1, int index2, int insert)
        {
            data[index1, index2] = insert;
        }

        public void exceptionSet(int index1, int index2, bool exc)
        {
            exception[index1, index2] = exc;
        }

        public bool exceptionGet(int index1, int index2)
        {
            return exception[index1, index2];
        }


    }

    #endregion


}

public class EventBase
{
   

    public int Event_index { get; set; }
    public int progress_index { get; set; }
    public int Current_EventCode { get; set; }

    public bool bisOption;

    public bool bisTest { get; set; }
    public Table.TableClass CurrentTable { get; set; }
    public LogicManager logicManager { get; set; }
    public CharacterManager characterManager { get; set; }
    public UIManager uiManager { get; set; }
    public BookData bookData { get; set; }

    /*
     * ���̺� ��Ģ
     * 1���ڸ��� : �̺�Ʈ�� ����
     * �� �̻��� �ڸ��� : ����� �̺�Ʈ�� ��Ÿ��
     *  ex) 6 => 2(1+1) X 3(2+1)  1,2 �� �����
     *      24 => 2(1+1) X 3(2+1) X 4(3+1) 1,2,3 �� �����
     *      30 =>2(1+1) X 3(2+1) X 5(4+1)  1,2,4 �� �����
     *      
     *      ��...61 => 2��° �� 1,2 ���� ����� �̺�Ʈ ���� 1 
     */

    public string[] SpecifyAnswer1 = new string[2]; 
    public string[] SpecifyAnswer2 = new string[2];
    public int RequireLV1;
    public int RequireLV2;

    #region 1X3 table

    public Table.TableClass _1table1 = new Table.TableClass(241, 0, 0, 0, 34, 63, 81, 0, 2, 3, 5, 0);
    public Table.TableClass _1table2 = new Table.TableClass(61 , 0, 0, 0, 23, 31,  0, 0, 4, 5, 0, 0);
    public Table.TableClass _1table3 = new Table.TableClass(241, 0, 0, 0, 21,122, 123, 0, 5, 2, 4, 0);
    public Table.TableClass _1table4 = new Table.TableClass(241, 0, 0, 0, 21, 35,  43, 0, 1, 5, 3, 0);
    public Table.TableClass _1table5 = new Table.TableClass(241, 0, 0, 0, 42, 33, 21, 0, 2, 3, 2, 0);
    public Table.TableClass _1table6 = new Table.TableClass(22, 0, 0, 0, 244, 0, 0, 0, 1, 2, 3, 0);
    public Table.TableClass _1table7 = new Table.TableClass(61, 0, 0, 0, 64, 41, 0, 0, 2, 3, 1, 0);
    public Table.TableClass _1table8 = new Table.TableClass(61, 0, 0, 0, 64, 202,0 , 0, 5, 5, 1, 3);
    public Table.TableClass _1table9 = new Table.TableClass(241, 0, 0, 0, 84, 35, 81, 0, 3, 1, 3, 0);
    public Table.TableClass _1table10 = new Table.TableClass(62, 0, 0, 0, 85, 61, 0, 0, 1, 5, 3, 0);
    public Table.TableClass _1table11 = new Table.TableClass(241, 0, 0, 0, 65, 42, 63, 0, 4, 1, 3, 0);
    public Table.TableClass _1table12 = new Table.TableClass(61, 0, 0, 0, 65, 21, 0, 0, 3, 4, 0, 0);
    public Table.TableClass _1table13 = new Table.TableClass(61, 0, 0, 0, 65, 21, 0, 0, 3, 4, 0, 0);
    public Table.TableClass _1table14 = new Table.TableClass(61, 0, 0, 0, 65, 21, 0, 0, 3, 4, 0, 0);
   


    #endregion


    #region 1X3X2 table


    #endregion

    #region 2X3 table
    public Table.TableClass _table1 = new Table.TableClass(61, 84, 0, 0, 4, 2, 4, 0, 0, 0, 0, 0);
    public Table.TableClass _table2 = new Table.TableClass(61, 42, 0, 0, 2, 2, 1, 0, 0, 0, 0, 0);
    public Table.TableClass _table3 = new Table.TableClass(61, 122, 0, 0, 4, 3, 1, 0, 0, 0, 0, 0);
    public Table.TableClass _table4 = new Table.TableClass(21, 122, 0, 0, 3, 1, 2, 0, 0, 0, 0, 0);
    public Table.TableClass _table5 = new Table.TableClass(23, 41, 0, 0, 4, 4, 3, 0, 0, 0, 0, 0);

    #endregion

    #region 2X3X2 table
    public Table.TableClass _2table1 = new Table.TableClass(61, 84, 0, 0, 64, 22, 34, 0, 1, 2, 0, 0);
    public Table.TableClass _2table2 = new Table.TableClass(61, 84, 0, 0, 24, 62, 34, 0, 3, 2, 0, 0);
    public Table.TableClass _2table3 = new Table.TableClass(61, 84, 0, 0, 34, 62, 24, 0, 2, 4, 0, 0);
    public Table.TableClass _2table4 = new Table.TableClass(61, 84, 0, 0, 64, 22, 34, 0, 3, 2, 0, 0);
    public Table.TableClass _2table5 = new Table.TableClass(61, 84, 0, 0, 64, 32, 24, 0, 4, 2, 0, 0);

    #endregion
    /*
    ���� �������� ���� �̺�Ʈ ó�� �̺�Ʈ�� ���� ī��� ������ �ʴ´ٸ�
    bisOption = true�� �Ͻʽÿ�
    �� �� ������ �̺�Ʈ������ bisOption = false�� �ؾ��մϴ�.

    ���� Current_EventCode ��
    �÷��̾��� ���ÿ� ���� ó���� ���ִ� �������� �����Ͻʽÿ�
    
    ex) 3    ->  Current_EventCode = 30
        1101 ->  Current_EventCode = 511010
        2101 ->  Current_EventCode = 521010
     
    ���� �� �������� �÷��̾��� ���ÿ� ���� ����� ó�����ָ� �˴ϴ�.
    �̶� �÷��̾ �� ī���� ��ȣ�� option 
                   �׽�Ʈ�� ����� testResult �Դϴ�.

    ����� �׽�Ʈ�� ����ϰ� �ʹٸ� logicManager.ForceChangeEventCode�� 
    �ڵ带 �����Ͻʽÿ�

    ex) 0�� ī�带 �´ٸ� �׳� �н�, 1�� ī�带 �´ٸ� 10 ���ظ� �ְ� ������
    
    if(option == 0)
    {
      logicManager.ForceChangeEventCode(11011);
      logicManager.ButtonActive(0, "������.", "������.", false);
    }
    else if(option == 1)
    {
      logicManager.ForceChangeEventCode(11012);
      characterManager.PlayerGetDamage(10);
      logicManager.ButtonActive(0, "������.", "������.", false);
    }

    ex) �׽�Ʈ ���̵��� 1�̰�, �����ϸ� 10 ���ظ� �ְ� ������

    if(testResult >=1)
    {
      logicManager.ForceChangeEventCode(11011);
      logicManager.ButtonActive(0, "������.", "������.", false);
    }
    else
    {
      logicManager.ForceChangeEventCode(11012);
      characterManager.PlayerGetDamage(10);
      logicManager.ButtonActive(0, "������.", "������.", false);
    }

    buttonActive(); �Լ�
       





    ������ �ɶ� (addDisease) ���� �����ؾ��ϴ� ����� �����ϴ�.
    �׳� �����ʽÿ�.

     �׷��� ������ �ɶ���  
     ������ ������ �κп���
     ���̺� ������ ������
     essential.exceptionSet(2, 0, true);
     �� �ɾ��־���մϴ�.
     �̶� essential�� ���̺��� �ǹ��ϸ�
     (2,0,true)�� 3��°�� 1��° ĭ�� �������� ������ �� ���� �ǹ��մϴ�. 

     logicManager.SetBattle(string name);
     �Լ��� ������ �� �� �ֽ��ϴ�.
     name�� Ʋ���� �翬�� �����ʤ��ϴ�.
         
         
    */

    public virtual void TableSet()
    {

    }


    public virtual void Select_Event(int code)
    {
        

    }

    public virtual void Excute_Event(int code = 0, int testResult = 0, bool isStart = false, int option = 0)
    {

    }


    public bool GetBisOption()
    {
        return bisOption;
    }
  



    public  void ShuffleDeck(int[] array , int start, int end)
    {
        System.Random rng = new System.Random();
        int n = end;
        while (n > start)
        {
            n--;
            int k = rng.Next(start, end + 1);
            int value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        
    }

    public void ShuffleDeck(List<int> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        int end = n;
        while (n > 0)
        {
            n--;
            int k = rng.Next(0, end);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }



    public void GetTableData(ref int[] connection)
    {

        
        for(int i=0; i<connection.Length;i++)
        {
            if (i < 4)
            {
                connection[i] = CurrentTable.subGet(i);               
               
            }
            else if (i < 8)
            {
                connection[i] = CurrentTable.midGet(i-4);
                
            }
            else
            {
                connection[i] = CurrentTable.proGet(i-8);
                
            }
        }
       
    }
    public void GetEventCode(ref int[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (i < 4)
            {
                data[i] = CurrentTable.dataGet(0, i);

            }
            else if (i < 8)
            {
                data[i] = CurrentTable.dataGet(1, i-4);

            }
            else
            {
                data[i] = CurrentTable.dataGet(2, i - 8);

            }
        }
    }

    public void GetException(ref bool[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (i < 4)
            {
                data[i] = CurrentTable.exceptionGet(0, i);

            }
            else if (i < 8)
            {
                data[i] = CurrentTable.exceptionGet(1, i - 4);

            }
            else
            {
                data[i] = CurrentTable.exceptionGet(2, i - 8);

            }
        }
    }

    public void SetItemQuiz(string fs1, string fs2, string ss1, string ss2, int lv1 , int lv2)
    {
        SpecifyAnswer1[0] = fs1;
        SpecifyAnswer1[1] = fs2;
        SpecifyAnswer2[0] = ss1;
        SpecifyAnswer2[1] = ss2;
        RequireLV1 = lv1;
        RequireLV2 = lv2;
    }

    public int compareItemQuiz(string as1, string as2, int lv)
    {
        int answer = 0;

        if(SpecifyAnswer1[0] == "All" || SpecifyAnswer1[0] == as1)
        {
            if(SpecifyAnswer1[1] == "All" || SpecifyAnswer1[1] == as2)
            {
                if (lv >= RequireLV1) answer = 1;
            }
        }

        if (SpecifyAnswer2[0] == "All" || SpecifyAnswer2[0] == as1)
        {
            if (SpecifyAnswer2[1] == "All" || SpecifyAnswer2[1] == as2)
            {
                if (lv >= RequireLV2) answer = 2;
            }
        }

        return answer;

    }


}


#region Ʃ�丮�� 
public class Tutorial:EventBase
{

    #region essential_table

    private Table.TableClass essential1 = new Table.TableClass(21, 0, 0, 0, 21, 0, 0, 0, 1, 0, 0, 0);
    private Table.TableClass essential2 = new Table.TableClass(21, 0, 0, 0, 21, 0, 0, 0, 1, 0, 0, 0);
   
    #endregion
    
    private List<Table.TableClass> tableList = new List<Table.TableClass>();

    private int[] indexArray = new int[2] {0, 1};

    public Tutorial()
    {        
        essential1.dataSet(0, 0, 1);
        essential1.dataSet(1, 0, 2);
        essential1.dataSet(2, 0, 3);

        essential2.dataSet(0, 0, 4);
        essential2.dataSet(1, 0, 5);
        essential2.dataSet(2, 0, 6);
      
        tableList.Add(essential1);
        tableList.Add(essential2);
      
        progress_index = 0;        
        CurrentTable = tableList[progress_index];
    }

    public override void Select_Event(int code)
    {
        //base.Select_Event(code);
        progress_index += 1;
        CurrentTable = tableList[indexArray[progress_index]];
    }

    #region Ʃ�丮��
    public override void Excute_Event(int code =0 , int testResult = 0, bool isStart = false, int option =0)
    {
        if (code > 0) Current_EventCode = code;
       
        switch (Current_EventCode)
        {           
            case 1:                
                logicManager.ButtonActive(0, "����", "������ ���� ���ư���", false);
               

            

                break;

            case 10:
                bisOption = false;
                if (testResult == 1)
                {
                    Current_EventCode = 11;
                    logicManager.ForceChangeEventCode(11);
                    logicManager.ButtonActive(0, "���� ����", "���� ����", false, option: 0);
                }
                else  if (testResult == 2)
                {
                    Current_EventCode = 12;
                    logicManager.ForceChangeEventCode(12);
                    logicManager.ButtonActive(0, "���� ����!!!", "���� ����!!!", false, option: 0);
                }
                else
                {
                    Current_EventCode = 13;
                    logicManager.ForceChangeEventCode(13);
                    logicManager.ButtonActive(0, "Ż��", "Ż��", false, option: 0);
                }
                break;

            case 2:
                bisOption = true;
                logicManager.ButtonActive(0, "����", "å�� ��ó����", false);
                Current_EventCode = 20;
                break;

            case 20:
                bisOption = true;
                Current_EventCode = 21;
                uiManager.Prologues("DictionaryStat");
                logicManager.ForceChangeEventCode(21);                
                logicManager.ButtonActive(0, "�ǹ��� ����", "�̰� ��ü ���� �Ҹ���?", false,option:2);

                break;

            case 21:
                bisOption = false;
                Current_EventCode = 22;
                logicManager.ForceChangeEventCode(22);
                logicManager.ButtonActive(0, "�ڸ� ���ϱ�", "�̰����� Ȳ���� �޾Ƴ���.", false,option:3);

                break;


            case 3:
                bisOption = true;
                characterManager.AddDisease(2301);
                logicManager.ButtonActive(0,1,"strength","������ ���� ����.","�� ���� ����\n���� ������ ���ϴ�.",option:0);
                Current_EventCode = 30;
                break;

            case 30:
                bisOption = false;
                if (testResult >= 1)
                {
                    Current_EventCode = 31;
                    logicManager.ForceChangeEventCode(31);
                    logicManager.ButtonActive(0, "����", "����!", false, option: 0);
                }
                else
                {

                    Current_EventCode = 32;
                    logicManager.ForceChangeEventCode(31);
                    logicManager.ButtonActive(0, "����", "����!", false, option: 0);
                }

                break;

            case 4:
                bisOption = true;
                logicManager.ButtonActive(0, "Ž��", "���ڸ� �ݴ´�.", false, option: 1);
                Current_EventCode = 40;
                break;

            case 40:
                characterManager.AddItem(2311, 1);
                Current_EventCode = 41;
                logicManager.ForceChangeEventCode(41);
                uiManager.Prologues("ItemTutorial0");

                break;
            case 41:
                Current_EventCode = 42;
                logicManager.ForceChangeEventCode(42);
                logicManager.ButtonActive(0, 5, "stealth", "��տ� ���´�.", "���� ���̰�\n��տ� �����ϴ�.", option: 0);

                break;
            case 42:
                bisOption = false;
                Current_EventCode = 43;
                logicManager.ForceChangeEventCode(43);                
                logicManager.ButtonActive(0, "����", "���ѷ� ����������.", false, option: 1);
                break;

            case 5:
                bisOption = true;
                Current_EventCode = 50;
                logicManager.ButtonActive(0, "�ֹ��� ����", "�ε� �ֹ濡 �װ��� �ֱ⸦...", false, option: 1);
                break;

            case 50:
                Current_EventCode = 51;
                logicManager.ForceChangeEventCode(51);

                //logicManager.ButtonActive(0, "s");
                logicManager.ButtonActive(0, "Ž��", "ü�� ������ ����.", false, option: 0);
                
                break;


            case 51:
                Current_EventCode = 52;
                logicManager.ForceChangeEventCode(52);
                logicManager.ButtonActive(0, "...", ".......", false, option: 0);
                //logicManager.ButtonActive();
                break;
            case 52:
                bisOption = false;
                Current_EventCode = 53;
                logicManager.ForceChangeEventCode(53);
                logicManager.ButtonActive(0, "��Ȳ", "������ ������", false, option: 0);
                //logicManager.ButtonActive();
                break;

          
            case 6:
                logicManager.ButtonActive(0, "Ǯ���� ����", "���� ���� �ȴ´�.", false, option: 1,bisEnd:true);
                
                //logicManager.ButtonActive();
                break;





        }


    }
    #endregion

}
#endregion


public class YenaGarden : EventBase
{
    #region var
    private List<Table.TableClass> tableList = new List<Table.TableClass>();
   

    private int[] indexArray = new int[15] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14 };
    private List<int> level1 = new List<int>();
    private List<int> level2 = new List<int>();
    private List<int> level3 = new List<int>();
    private List<int> level4 = new List<int>();
    private List<int> level5 = new List<int>();
    

    private Table.TableClass essential6 = new Table.TableClass(63, 23, 32, 0, 21, 22, 0, 0, 1, 0, 0, 0);
  



    #endregion
    #region ������
    public YenaGarden()
    {
        ShuffleDeck(indexArray, 0, 11);

      
        tableList.Add(_1table1);
        tableList.Add(_1table2);
        tableList.Add(_1table3);
        tableList.Add(_1table4);
        tableList.Add(_1table5);
        tableList.Add(_1table6);
        tableList.Add(_1table7);
        tableList.Add(_1table8);
        tableList.Add(_1table9);
        tableList.Add(_1table10);
        tableList.Add(_1table11);
        tableList.Add(_1table12);
        tableList.Add(_1table13);
        tableList.Add(essential6);
        
      
      
        /*
        tableList.Add(_table4);
        tableList.Add(_table5);
        tableList.Add(_2table1);
        tableList.Add(_2table2);
        tableList.Add(_2table3);
    */

        

       
        TableSet();

      

        tableList[indexArray[0]].dataSet(0, 0, 1);
        tableList[indexArray[3]].dataSet(0, 0, 2);
        tableList[indexArray[6]].dataSet(0, 0, 3);
        tableList[indexArray[9]].dataSet(0, 0, 4);
        tableList[indexArray[12]].dataSet(0, 0, 5);
        essential6.dataSet(2, 0, 6);

                  
        progress_index = 0;

        CurrentTable = tableList[indexArray[progress_index]];
        //Debug.Log(string.Format("index : {0}\n table : {1}\ncode : {2}\nprogress_index : {3}", indexArray[0], tableList[indexArray[0]], tableList[indexArray[0]].dataGet(0, 0), progress_index));
    }
    #endregion
    private void AddCard(int level, int code , int num)
    {
        switch(level)
        {
            case 1:
                for (int i = 0; i < num; i++)
                {
                    level1.Add(code);
                }
                break;

            case 2:
                for (int i = 0; i < num; i++)
                {
                    level2.Add(code);
                }

                break;

            case 3:
                for (int i = 0; i < num; i++)
                {
                    level3.Add(code);
                }
                break;

            case 4:
                for (int i = 0; i < num; i++)
                {
                    level4.Add(code);
                }
                break;

            case 5:
                for (int i = 0; i < num; i++)
                {
                    level5.Add(code);
                }
                break;
        }

        
    }

    public override void TableSet()
    {
        #region
        /*
        for (int i = 0; i < this.tableList.Count; i++)
        {

            System.Random rng1 = new System.Random(DateTime.Now.Millisecond);
            System.Random rng2 = new System.Random(DateTime.Now.Millisecond+1);
            System.Random rng3 = new System.Random(DateTime.Now.Millisecond+2);

            for (int j = 0; j < 4; j++)
            {
               

                int risk = 0;
                int tendency = 0;
                int detail = 0;
                int result = 0;

                risk = this.tableList[i].subGet(j) % 10;

                rng1 = new System.Random(DateTime.Now.Millisecond+3);
                tendency = rng1.Next(1,4);

                switch (tendency)
                {
                    case 1:
                        detail = UnityEngine.Random.Range(1, good + 1);
                        break;
                    case 2:
                        detail = UnityEngine.Random.Range(1, bad + 1);
                        break;
                    case 3:
                        detail = UnityEngine.Random.Range(1, mid + 1);
                        break;
                              
                }
                result = tendency * 1000 + risk * 100 + detail;

                this.tableList[i].dataSet(0, j, result);

                risk = tableList[i].midGet(j)% 10;
                rng2 = new System.Random(DateTime.Now.Millisecond+4);
                tendency = rng2.Next(1, 4);

                switch (tendency)
                {
                    case 1:
                        detail = UnityEngine.Random.Range(1, good + 1);
                        break;
                    case 2:
                        detail = UnityEngine.Random.Range(1, bad + 1);
                        break;
                    case 3:
                        detail = UnityEngine.Random.Range(1, mid + 1);
                        break;
                   
                }

                result = tendency * 1000 + risk * 100 + detail;

                this.tableList[i].dataSet(1, j, result);

                risk = tableList[i].proGet(j) % 10;

                rng3 = new System.Random(DateTime.Now.Millisecond+5);
                tendency = rng3.Next(1, 4);
                switch (tendency)
                {
                    case 1:
                        detail = UnityEngine.Random.Range(1, good + 1);
                        break;
                    case 2:
                        detail = UnityEngine.Random.Range(1, bad + 1);
                        break;
                    case 3:
                        detail = UnityEngine.Random.Range(1, mid + 1);
                        break;
                           
                }
                result = tendency * 1000 + risk * 100 + detail;
                this.tableList[i].dataSet(2, j, result);
            }
            
        }
     */
        #endregion

        AddCard(1,1101,3); 
        AddCard(1,1102,2);
        AddCard(1,1103,3);
        AddCard(1,1104,2);
        AddCard(1,1105,1);
        AddCard(1,2101,1);
        AddCard(1,2102,2);
        AddCard(1,2103,2);
        AddCard(1, 3101,2);

        AddCard(2,1201,3);
        AddCard(2,1202,2);
        AddCard(2,1203,2);
        AddCard(2,1204,2);
        AddCard(2,1205,1);
        AddCard(2,2201,2);
        AddCard(2,2202,2);
        AddCard(2,2203,2);      

        AddCard(3,1301,2);
        AddCard(3,1302,2);
        AddCard(3,1303,1);
        AddCard(3,1304,1);
        AddCard(3,2301,2);
        AddCard(3,2302,2);
        AddCard(3,2303,1);
      

       
        AddCard(4, 3102, 1);
        AddCard(4, 3103, 1);
        AddCard(4, 3104, 1);
        AddCard(4, 3105, 1);

        AddCard(4, 3201, 1);
        AddCard(4, 3202, 1);
        AddCard(4, 3203, 1);
        AddCard(4, 3204, 1);
        AddCard(4, 3205, 1);

        AddCard(4, 3301, 1);
        AddCard(4, 3302, 1);
        AddCard(4, 3303, 1);
        AddCard(4, 3304, 1);
        AddCard(4, 3305, 1);


        AddCard(5, 1103, 2);
        AddCard(5, 1302, 2);
        AddCard(5, 1301, 2);
        AddCard(5, 1202, 2);
        AddCard(5, 1104, 2);
        AddCard(5, 2302, 2);
        AddCard(5, 2103, 2);
        AddCard(5, 2202, 2);



        ShuffleDeck(level1);
        ShuffleDeck(level2);       
        ShuffleDeck(level3);
        ShuffleDeck(level4);
        ShuffleDeck(level5);

        int risk = 0;
        int result = 0;
        int index1 = 0;
        int index2 = 0;
        int index3 = 0;
        int index4 = 0;
        int index5 = 0;
        for (int i = 0; i < tableList.Count; i++)
        {
            if (index1 == level1.Count) index1 = 0;
            if (index2 == level2.Count) index2 = 0;
            if (index3 == level3.Count) index3 = 0;
            if (index4 == level4.Count) index4 = 0;
            if (index5 == level5.Count) index5 = 0;

            for (int j = 0; j < 4; j++)
            {              
                risk = this.tableList[i].subGet(j) % 10;

                switch(risk)
                {
                    case 0:
                        break;

                    case 1:                        
                        result = level1[index1];
                        index1 += 1;
                        break;
                    case 2:
                        result = level2[index2];
                        index2 += 1;
                        break;
                    case 3:
                        result = level3[index3];
                        index3 += 1;
                      
                        break;
                    case 4:
                        result = level4[index4];
                        index4 += 1;
                        break;
                    case 5:
                        result = level4[index5];
                        index5 += 1;
                        break;


                }
                if (index1 == level1.Count) index1 = 0;
                if (index2 == level2.Count) index2 = 0;
                if (index3 == level3.Count) index3 = 0;
                if (index4 == level4.Count) index4 = 0;
                if (index5 == level5.Count) index5 = 0;
                this.tableList[i].dataSet(0, j, result);
               

                risk = this.tableList[i].midGet(j) % 10;
                switch (risk)
                {
                    case 0:
                        break;

                    case 1:
                        result = level1[index1];
                        index1 += 1;
                        break;
                    case 2:
                        result = level2[index2];
                        index2 += 1;
                        break;
                    case 3:
                        result = level3[index3];
                        index3 += 1;
                       // Debug.Log(string.Format("index : {0}\nlevel3 : {1}", index3, result));
                        break;
                    case 4:
                        result = level4[index4];
                        index4 += 1;
                        break;
                    case 5:
                        result = level5[index4];
                        index5 += 1;
                        break;
                }
                if (index1 == level1.Count) index1 = 0;
                if (index2 == level2.Count) index2 = 0;
                if (index3 == level3.Count) index3 = 0;
                if (index4 == level4.Count) index4 = 0;
                if (index5 == level5.Count) index5 = 0;
                this.tableList[i].dataSet(1, j, result);

                risk = this.tableList[i].proGet(j) % 10;
              
                switch (risk)
                {
                    case 0:
                        break;

                    case 1:
                        result = level1[index1];
                        index1 += 1;
                        break;
                    case 2:
                        result = level2[index2];
                        index2 += 1;
                        break;
                    case 3:
                        result = level3[index3];
                        index3 += 1;
                        //Debug.Log(string.Format("index : {0}\nlevel3 : {1}", index3, result));
                        break;
                    case 4:
                        result = level4[index4];
                        index4 += 1;
                        break;
                    case 5:
                        result = level5[index4];
                        index5 += 1;
                        break;
                }

                this.tableList[i].dataSet(2, j, result);
               
            }
        }


        }
    public override void Select_Event(int code)
    {
        //base.Select_Event(code);

        progress_index += 1;
        CurrentTable = tableList[indexArray[progress_index]];
    }
    public override void Excute_Event(int code = 0, int testResult = 0, bool isStart = false, int option = 0)
    {
       
        if (code > 0) Current_EventCode = code;
       
        //
        float temp = Time.time * 100f;

        switch (Current_EventCode)
        {
            #region ���� ���丮

            case 1:
                bisOption = true;
                Current_EventCode = 5010;
             
                logicManager.ButtonActive(0,"������","���� ���� �ð��� ����.",false);
                logicManager.ButtonActive(1,"Ž��","������ ���캻��",false);
                break;

            case 5010:
                bisOption = false;
                if(option == 0)
                {
                   
                    logicManager.ForceChangeEventCode(101);
                    logicManager.ButtonActive(0, "������", "�ڸ��� ������.", false, option: 0);
                }
                else
                {                   
                    logicManager.ForceChangeEventCode(102);
                    characterManager.AddItem(1121, 1);
                    uiManager.itemAddMessage.AddMessage("��δ�");
                    logicManager.ButtonActive(0, "������", "�ڸ��� ������.", false, option: 0);
                }
                break;

            case 2:
              
                logicManager.ButtonActive(0, "����", "�� ���� ������", false);
                break;

            case 3:
                
                bisOption = true;
                Current_EventCode = 31;
                logicManager.ButtonActive(0, "����", "���� ����", false);
                break;

            case 31:
                bisOption = false;
                logicManager.ForceChangeEventCode(31);
                Current_EventCode = 31;
                logicManager.ButtonActive(0, "����", "���� ����", false);
                break;

            case 4:
                bisOption = true;
                Current_EventCode = 41;
            
                logicManager.ButtonActive(0, "���?", "�̷� ���� �����?", false);
                break;

            case 41:
                logicManager.ForceChangeEventCode(41);
                Current_EventCode = 42;
                logicManager.ButtonActive(0, "����", "", false, option: 0);
                break;

            case 42:
                logicManager.ForceChangeEventCode(42);
                bisOption = false;
                characterManager.AddItem(1131, 1);
                characterManager.AddItem(1211, 1);
                uiManager.itemAddMessage.AddMessage("�˷��ƽ�#�췹����");
                logicManager.ButtonActive(0, "������", "�� ���� ����", false, option: 0);
                break;

            case 5:
                bisOption = true;
                Current_EventCode = 51;
                logicManager.ButtonActive(0, "���´�", "Ǯ���̿� ���´�.", false);
                break;

            case 51:
                logicManager.ForceChangeEventCode(51);
                Current_EventCode = 52;
                logicManager.ButtonActive(0, "�ǹ��� ����", "���ڸ� ���캻��", false, option: 0);
                break;

            case 52:
                logicManager.ForceChangeEventCode(52);
                Current_EventCode = 53;
                logicManager.ButtonActive(0, "�ٸ�...?", "�߸� �ٸ��� �ֿ췯 ����.", false,option:0);
                break;

            case 53:
                logicManager.ForceChangeEventCode(53);
                Current_EventCode = 54;
                logicManager.ButtonActive(0, "�ٸ��� �ݴ´�", "�� ���� ����������", false, option: 0);
                break;

            case 54:
                logicManager.ForceChangeEventCode(54);
                bisOption = false;                            
                logicManager.ButtonActive(0, "������", "������ ����.", false, option: 0);
                break;

            case 6:
                bisOption = true;
                Current_EventCode = 61;
                logicManager.ForceChangeEventCode(6);
                logicManager.ButtonActive(0, "Ǯ���� ��", "���� ����", false);
                break;

            case 61:
                bisOption = false;
                logicManager.ForceChangeEventCode(61);
             
                logicManager.ButtonActive(0, "???", "???", false, option: 1, bisEnd: true);
                break;

            #endregion
            #region ������ ����
            #region ������ ���ŵ���
            case 1101:
                bookData.SetDictData("������ ����",0);
                bisOption = true;
                Current_EventCode = 511010;
                logicManager.ForceChangeEventCode(1101);
                logicManager.ButtonActive(0,"ä��","���Ÿ� ����",false);
                logicManager.ButtonActive(1,"����","�ڸ��� ������.",false);
                break;

            case 511010:

                if (option == 0)
                {
                    if (bookData.vegetalDict["������ ����"].bisBonus1 == 0)
                    {
                        bisOption = false;

                       
                        logicManager.ForceChangeEventCode(11012);

                        characterManager.playerUseMana(10);                       
                        characterManager.AddItem(1201, 1);
                        logicManager.ButtonActive(0, "��...", "�ڰ� ������ ����ϳ�", false, option: 0);
                        uiManager.itemAddMessage.AddMessage("������ ����");
                        break;
                    }
                    else
                    {
                        bisOption = false;
                        
                        logicManager.ForceChangeEventCode(11013);
                        uiManager.itemAddMessage.AddMessage("������ ����");
                        characterManager.AddItem(1201, 1);
                        logicManager.ButtonActive(0, "������ ����", "���Ÿ� ì�ܰ���", false, option: 0);
                    }
                }
                else if(option == 1)
                {
                    bisOption = false;
                  
                    logicManager.ForceChangeEventCode(11011);
                    logicManager.ButtonActive(0, "������", "������ ����", false, option: 0);
                }

                break;
            #endregion
               #region ������ ���� ����1
            case 3102:
                /*
                if(bookData.vegetalDict["������ ����"].bisFirst == 0)
                {
                    bookData.SetDictData("������ ����", 0);
                    bisOption = true;
                    Current_EventCode = 511010;
                    logicManager.ForceChangeEventCode(1101);
                    logicManager.ButtonActive(0, "ä��", "���Ÿ� ����", false);
                    logicManager.ButtonActive(1, "����", "�ڸ��� ������.", false);
                    break;
                }
                else
                {
                    if (bookData.vegetalDict["������ ����"].bisBonus1 == 0)
                    {
                        bookData.SetDictData("������ ����", 1);
                        uiManager.itemAddMessage.AddMessage("+������ ���� ����");
                        logicManager.ButtonActive(0, "������", "�� ���� ����", false);
                        uiManager.itemAddMessage.AddMessage("������ ����");
                        characterManager.AddItem(1201, 1);
                        logicManager.ButtonActive(0, "������ ����", "���Ÿ� ì�ܰ���", false);
                    }
                    else
                    {                        
                        bisOption = true;
                        Current_EventCode = 511010;
                        logicManager.ForceChangeEventCode(1101);
                        logicManager.ButtonActive(0, "ä��", "���Ÿ� ����", false);
                        logicManager.ButtonActive(1, "����", "�ڸ��� ������.", false);
                        break;
                    }
                }
                */
                bookData.SetDictData("������ ����", 0);
                bookData.SetDictData("������ ����", 1);
                uiManager.itemAddMessage.AddMessage("+������ ���� ����");
              
                uiManager.itemAddMessage.AddMessage("������ ����");
                characterManager.AddItem(1201, 1);
                logicManager.ButtonActive(0, "������ ����", "���Ÿ� ì�ܰ���", false);
                break;

                #endregion
                #region ������ ���� ����2
                
            case 3304:
                /*
                   if (bookData.vegetalDict["������ ����"].bisFirst == 0)
                   {
                       bookData.SetDictData("������ ����", 0);
                       bisOption = true;
                       Current_EventCode = 511010;
                       logicManager.ForceChangeEventCode(1101);
                       logicManager.ButtonActive(0, "ä��", "���Ÿ� ����", false);
                       logicManager.ButtonActive(1, "����", "�ڸ��� ������.", false);
                       break;
                   }
                   else
                   {
                       if (bookData.vegetalDict["������ ����"].bisBonus2 == 0)
                       {
                           bookData.SetDictData("������ ����", 2);
                           uiManager.itemAddMessage.AddMessage("������ ���� ����");
                           logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                       }
                       else
                       {
                           bisOption = true;
                           Current_EventCode = 511010;
                           logicManager.ForceChangeEventCode(1101);
                           logicManager.ButtonActive(0, "ä��", "���Ÿ� ����", false);
                           logicManager.ButtonActive(1, "����", "�ڸ��� ������.", false);
                           break;
                       }
                   }
                   */
                bookData.SetDictData("������ ����", 0);
                bookData.SetDictData("������ ����", 2);
                uiManager.itemAddMessage.AddMessage("������ ���� ����");
                logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                break;
            #endregion
            #endregion
            #region ����
               #region ���� ����
            case 1102:
                bisOption = true;
                bookData.SetDictData("����", 0);
                Current_EventCode = 511020;
                logicManager.ButtonActive(0,"ä��","�� ��ŭ ����.",false);              
                logicManager.ButtonActive(1,"�ܺ�����",1002,"�ܺ�����","�� ���� ���´�.");
                
                break;

            case 511020:
                bisOption = false;
                if (option == 0)
                {
                    Current_EventCode = 11021;
                    logicManager.ForceChangeEventCode(11021);
                    uiManager.itemAddMessage.AddMessage("����");
                    characterManager.AddItem(1401,1);
                    logicManager.ButtonActive(0, "������.", "������.", false);
                }
                else
                {
                    logicManager.ForceChangeEventCode(11022);
                    characterManager.AddItem(2401, 1);
                    uiManager.itemAddMessage.AddMessage("���� ����");
                    logicManager.ButtonActive(0, "������.", "������.", false);
                }
                break;
            #endregion
               #region ���� ����
            case 1205:

                bookData.SetDictData("����", 0);
                bookData.SetDictData("����", 1);
                characterManager.AddItem(2401, 1);
                uiManager.itemAddMessage.AddMessage("���� ����");
                logicManager.ButtonActive(0, "������.", "������.", false);


                break;
            #endregion            
            #endregion
            #region  ����õ��
            case 1103:
                if (bookData.vegetalDict["����õ��"].bisFirst == 0) bookData.SetDictData("����õ��",0);
                bisOption = true;
                Current_EventCode = 511030;
                logicManager.ButtonActive(0, "ä��", "Ǯ�� ����", false);
                break;

            case 511030:
                bisOption = false;
                if (bookData.vegetalDict["����õ��"].bisBonus1 == 0)
                {
                   logicManager.ForceChangeEventCode(11031);
                    uiManager.itemAddMessage.AddMessage("����õ��");
                    characterManager.AddItem(1501, 1);
                    logicManager.ButtonActive(0, "", "���� ������.", false);
                }
                else
                {
                    logicManager.ForceChangeEventCode(11032);
                    uiManager.itemAddMessage.AddMessage("����õ��#����õ��");
                    characterManager.AddItem(1501, 2);
                    logicManager.ButtonActive(0, "", "���� ������.", false);
                }
                 break;

            case 3103:
                /*
                if (bookData.vegetalDict["����õ��"].bisFirst == 0)
                {
                    bookData.SetDictData("����õ��", 0);
                    bisOption = true;
                    Current_EventCode = 511030;
                    logicManager.ForceChangeEventCode(1103);
                    logicManager.ButtonActive(0, "ä��", "Ǯ�� ����", false);
                    break;
                }
                else
                {
                    if (bookData.vegetalDict["����õ��"].bisBonus1 == 0)
                    {                        
                        bookData.SetDictData("����õ��", 1);                       
                        logicManager.ButtonActive(0, "", "������.", false);
                        uiManager.itemAddMessage.AddMessage("����õ��#����õ��");
                        characterManager.AddItem(1501, 2);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 511030;
                        logicManager.ForceChangeEventCode(1103);
                        logicManager.ButtonActive(0, "ä��", "Ǯ�� ����", false);
                        break;
                    }
                }
                */
                bookData.SetDictData("����õ��", 0);
                bookData.SetDictData("����õ��", 1);
                logicManager.ButtonActive(0, "", "������.", false);
                uiManager.itemAddMessage.AddMessage("����õ��#����õ��");
                characterManager.AddItem(1501, 2);
                break;


            #endregion
            #region �������� Ǯ
               #region �������� Ǯ ä��
            case 1104:
                bisOption = true;
                bookData.SetDictData("��������Ǯ", 0);
                Current_EventCode = 511040;
                logicManager.ButtonActive(0, "ä��", "Ǯ�� ĵ��", false);
                if (bookData.vegetalDict["��������Ǯ"].bisBonus1 == 1)
                {
                    if (bookData.vegetalDict["��������Ǯ"].bisBonus2 == 1) characterManager.EventBonus(bookData.vegetalDict["��������Ǯ"].bonus1, "agility", 1);
                    logicManager.ButtonActive(1, 1, "agility", "ä��", "�ٱ⸦ ������ ��´�.", option: 0);
                }

                    break;


            case 511040:
                bisOption = false;
                if(option == 0)
                {
                    Current_EventCode = 11041;
                    logicManager.ForceChangeEventCode(11041);
                    logicManager.ButtonActive(0, "������.", "������.", false);
                }
                else
                {
                    if(testResult >= 1)
                    {
                        Current_EventCode = 11042;
                        logicManager.ForceChangeEventCode(11042);
                        uiManager.itemAddMessage.AddMessage("��������Ǯ");
                        characterManager.AddItem(1101, 1);
                        logicManager.ButtonActive(0, "������.", "������.", false);
                    }
                    else
                    {
                        Current_EventCode = 11043;
                        logicManager.ForceChangeEventCode(11043);
                        logicManager.ButtonActive(0, "������.", "������.", false);
                    }
                }
               

                break;
            #endregion
               #region ��������Ǯ ����1
            case 3104:
                /*
                if(bookData.vegetalDict["��������Ǯ"].bisFirst == 0)
                {

                    bisOption = true;
                    bookData.SetDictData("��������Ǯ", 0);
                    logicManager.ForceChangeEventCode(1104);
                    Current_EventCode = 511040;
                    logicManager.ButtonActive(0, "ä��", "Ǯ�� ĵ��", false);
                }
                else
                {
                    if(bookData.vegetalDict["��������Ǯ"].bisBonus1 == 0)
                    {                       
                        uiManager.itemAddMessage.AddMessage("��������Ǯ ����");
                        bookData.SetDictData("��������Ǯ",1);
                        logicManager.ButtonActive(0, "������.", "������.", false);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 511040;
                        logicManager.ForceChangeEventCode(1104);
                        logicManager.ButtonActive(0, "ä��", "Ǯ�� ĵ��", false);
                        if (bookData.vegetalDict["��������Ǯ"].bisBonus1 == 1)
                        {
                            logicManager.ButtonActive(1, 1, "agility", "ä��", "�ٱ⸦ ������ ��´�.", option: 0);
                        }
                    }
                }
                */
                uiManager.itemAddMessage.AddMessage("��������Ǯ ����");
                bookData.SetDictData("��������Ǯ", 1);
                logicManager.ButtonActive(0, "������.", "������.", false);
                break;

            #endregion
               #region ��������Ǯ ����2
            case 3105:
                /*
                if (bookData.vegetalDict["��������Ǯ"].bisFirst == 0)
                {
                    bisOption = true;
                    Current_EventCode = 511040;
                    bookData.SetDictData("��������Ǯ", 0);
                    logicManager.ForceChangeEventCode(1104);
                    logicManager.ButtonActive(0, "ä��", "Ǯ�� ĵ��", false);
                }
                else
                {
                    if (bookData.vegetalDict["��������Ǯ"].bisBonus2 == 0)
                    {                     
                        uiManager.itemAddMessage.AddMessage("��������Ǯ ����");
                        bookData.SetDictData("��������Ǯ", 2);
                        logicManager.ButtonActive(0, "������.", "������.", false);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 511040;
                        logicManager.ForceChangeEventCode(1104);
                        logicManager.ButtonActive(0, "ä��", "Ǯ�� ĵ��", false);
                        if (bookData.vegetalDict["��������Ǯ"].bisBonus1 == 1)
                        {
                            logicManager.ButtonActive(1, 1, "agility", "ä��", "�ٱ⸦ ������ ��´�.", option: 0);
                        }
                    }
                }
                */
                uiManager.itemAddMessage.AddMessage("��������Ǯ ����");
                bookData.SetDictData("��������Ǯ", 2);
                logicManager.ButtonActive(0, "������.", "������.", false);
                break;
            #endregion
            #endregion
            #region �����̳���
            case 1105:
                bookData.SetDictData("������ ����", 0);
                logicManager.ButtonActive(0, "", "���� ������.", false);
                uiManager.itemAddMessage.AddMessage("���񳯰�");
                characterManager.AddItem(1301, 1);
                break;

            case 1201:
                bisOption = true;
                bookData.SetDictData("������ ����", 0);
                Current_EventCode = 512010;
                logicManager.ButtonActive(0, "����", "���� ���󰣴�.", false);
                break;

            case 512010:
                int a = UnityEngine.Random.Range(0, 3);
                if(a ==0)
                {
                    Current_EventCode = 512011;
                    logicManager.ForceChangeEventCode(12011);
                    logicManager.ButtonActive(0,"���Ÿ� ������","�� ���� ����\n���Ÿ� ������.",false,option:0);
                    if (bookData.vegetalDict["���ָӴ���"].bisBonus2 == 1) logicManager.ButtonActive(1, "ħ��", 1003, "ħ��", "ħ���� ���ٴ��", option: 0);                      
                }
                else if(a==1)
                {
                    temp = Time.time * 100f;
                    UnityEngine.Random.InitState((int)temp);
                    a = UnityEngine.Random.Range(0, 2);
                    if(a==0)
                    {
                        bisOption = false;
                        Current_EventCode = 512011;
                        logicManager.ForceChangeEventCode(12014);
                        logicManager.ButtonActive(0,1,"strength","�� ����","���� �־�\n���� ������",option:0);
                    }
                    else
                    {
                        bisOption = false;
                        logicManager.ForceChangeEventCode(12017);
                        logicManager.ButtonActive(0,  "ì���", "���� ì�ܰ���", false,option: 0);
                        uiManager.itemAddMessage.AddMessage("���� ��");
                        characterManager.AddItem(1004, 1);
                    }
                }
                else
                {
                    bisOption = false;
                    logicManager.ForceChangeEventCode(12018);
                    logicManager.ButtonActive(0, "�̷�... " ,"�� ���� ����", false, option: 0);
                }


                break;
            case 512011:
                if(option == 0)
                {
                    bisOption = false;
                    logicManager.ButtonActive(0, "������", "�� ���� ����.", false, option: 0);
                        
                        logicManager.ForceChangeEventCode(12012);
                       
                    

                }
                else
                {
                    bisOption = false;
                    logicManager.ButtonActive(0, "������", "��帨�� ì�� ����", false, option: 0);
                    
                    logicManager.ForceChangeEventCode(12013);
                    uiManager.itemAddMessage.AddMessage("��帨");
                    characterManager.AddItem(1401, 1);
                }
                break;

        
            case 512012:
               
                if(testResult >= 1)
                {
                    bisOption = false;
                    logicManager.ForceChangeEventCode(12015);
                    logicManager.ButtonActive(0, "����", "����ǰ�� ì���", false, option: 0);
                    uiManager.itemAddMessage.AddMessage("����");
                    characterManager.AddItem(1202, 1);
                }
                else
                {
                    bisOption = false;
                    logicManager.ForceChangeEventCode(12016);
                    logicManager.ButtonActive(0, "����", "�� ���� ����", false,option:0);
                }
                        
                break;
            #endregion
            #region ������
            case 1202:
                bisOption = true;
                bookData.SetDictData("������", 0);
                Current_EventCode = 512020;
                logicManager.ButtonActive(0, "������.", "������ ������ ���̳�.", false);
                logicManager.ButtonActive(1, "����", "�� �� ������\n������ ���� ������.", false);

                break;

            case 512020:

                if (option == 1)
                {
                    Current_EventCode = 612020;
                    logicManager.ForceChangeEventCode(12021);
                    characterManager.playerGetDamage(-5);
                    characterManager.playerUseMana(-5);
                    logicManager.ButtonActive(0, "������.", "�� �Դ°� ������.", false);
                    logicManager.ButtonActive(1, "����", "�����Ѱ� ������.", false);
                }
                else
                {
                    bisOption = false;
                    Current_EventCode = 12022;
                    logicManager.ForceChangeEventCode(12022);
                    logicManager.ButtonActive(0, "������.", "������ ������ �ʿ�� ����.", false);
                }

                break;

            case 612020:
                if (option == 1)
                {
                    Current_EventCode = 712020;
                    logicManager.ForceChangeEventCode(12023);
                    characterManager.playerGetDamage(-5);
                    characterManager.playerUseMana(-5);
                    logicManager.ButtonActive(0, "������.", "���� ���� ���� �ʾ�.", false);
                    logicManager.ButtonActive(1, "����", "���Ը� ��...", false);
                }
                else
                {
                    Current_EventCode = 12022;
                    bisOption = false;
                    logicManager.ForceChangeEventCode(12022);
                    logicManager.ButtonActive(0, "������.", "������ ������ �ʿ�� ����.", false);
                }
                break;

            case 712020:
                UnityEngine.Random.InitState(DateTime.Now.Millisecond);
                a = UnityEngine.Random.Range(0, 2);
                bisOption = false;
                if (a == 0)
                {
                    Current_EventCode = 712021;
                    if (option == 1)
                    {
                       
                        logicManager.ForceChangeEventCode(12024);
                        characterManager.playerGetDamage(-5);
                        characterManager.playerUseMana(-5);
                        logicManager.ButtonActive(0, "������.", "���� �����ϳ�.", false);

                    }
                    else
                    {
                        Current_EventCode = 12022;
                        logicManager.ForceChangeEventCode(12022);
                        logicManager.ButtonActive(0, "������.", "������ ������ �ʿ�� ����.", false);
                    }
                }
                else if(a == 1)
                {
                    Current_EventCode = 712022;
                    if (option == 1)
                    {
                        Current_EventCode = 12025;
                        logicManager.ForceChangeEventCode(12025);
                        characterManager.AddDisease(1102);
                        logicManager.ButtonActive(0, "��ȸ", "������.", false);

                    }
                    else
                    {
                        Current_EventCode = 12022;
                        logicManager.ForceChangeEventCode(12022);
                        logicManager.ButtonActive(0, "������.", "������ ������ �ʿ�� ����.", false);
                    }
                }

               
              
                break;


            #endregion
            #region  �����ٴù���
            case 1203:
                bisOption = true;
                Current_EventCode = 512030;
                logicManager.ButtonActive(0, "����", "�ǵ����� �ʴ´�.", false);
                logicManager.ButtonActive(1, "����", "������ �����ϰ�\n������ ����.", false);
                if(bookData.vegetalDict["���߹ٴڹ���"].bisBonus1 == 1)
                {            
                    logicManager.ButtonActive(2, "����", 1111, "�ܺ�����", "�� ������\n���� �ִ��� Ȯ��");
                }
                break;

            case 512030:
                bisOption = false;
                if (option == 0)
                {
                    Current_EventCode = 12031;
                    logicManager.ForceChangeEventCode(12031);
                    logicManager.ButtonActive(0,"������","�� ���� ����",false);

                }

                else if(option == 1)
                {
                    Current_EventCode = 12032;
                    logicManager.ForceChangeEventCode(12032);
                    uiManager.itemAddMessage.AddMessage("�����ٴù���");
                    logicManager.ButtonActive(0, "������", "�� ���� ����", false);
                }
                else
                {
                    Current_EventCode = 12033;
                    logicManager.ForceChangeEventCode(12033);
                    uiManager.itemAddMessage.AddMessage("�����ٴù���");
                    logicManager.ButtonActive(0, "������", "�� ���� ����", false);
                }

                break;



            #endregion
            #region �ܺ�����
               #region  �õ� �ܺ�����
            case 1204:
                bisOption = true;
                Current_EventCode = 512040;
                logicManager.ButtonActive(0, "ó�ٺ���", "�ű��� ���̳�", false);
                if(bookData.vegetalDict["�ܺ�����"].bisBonus2 == 1) logicManager.ButtonActive(1, "����õ��", 1111, "����õ��", "�� ���� �ɿ� �Ѹ���.");

                break;
          
            case 512040:
                bisOption = false;
                if(option == 0)
                {
                    logicManager.ForceChangeEventCode(12041);
                    logicManager.ButtonActive(0, "������", "������ ����.", false);
                }
                else
                {
                    logicManager.ForceChangeEventCode(12042);
                    logicManager.ButtonActive(0, "������", "������ ����.", false);
                    uiManager.itemAddMessage.AddMessage("�ܺ�����");
                }

                break;
            #endregion
               #region �ܺ�����
            case 1301:
                bisOption = true;
                bookData.SetDictData("�ܺ�����", 0);
                Current_EventCode = 513010;
                logicManager.ButtonActive(0, "����", "�� ���� �ʴ´�.", false);
                logicManager.ButtonActive(1, 1, "agility", "ä��", "���� ����", option: 0);
                break;

            case 513010:
                bisOption = false;
                if(option == 0)
                {
                    logicManager.ForceChangeEventCode(13015);
                    logicManager.ButtonActive(0, "������", "������ ����.", false);
                }
                else
                {
                    if(testResult >= 1)
                    {                        
                        if(bookData.vegetalDict["�ܺ�����"].bisBonus1 == 0)
                        {
                            logicManager.ButtonActive(0, "������", "������ ����.", false);
                            logicManager.ForceChangeEventCode(13011);
                            uiManager.itemAddMessage.AddMessage("�ܺ�����");
                            characterManager.AddItem(1002,1);
                        }
                        else
                        {
                            logicManager.ButtonActive(0, "������", "������ ����.", false);
                            logicManager.ForceChangeEventCode(13013);
                            uiManager.itemAddMessage.AddMessage("�ܺ�����");
                            characterManager.AddItem(1002, 1,decay:15);
                        }

                    }
                    else
                    {                      
                        if (bookData.vegetalDict["�ܺ�����"].bisBonus1 == 0)
                        {
                            logicManager.ButtonActive(0, "������", "������ ����.", false);
                            characterManager.playerGetDamage(15);
                            logicManager.ForceChangeEventCode(13012);
                            uiManager.itemAddMessage.AddMessage("�ܺ�����");
                            characterManager.AddItem(1002, 1);
                        }
                        else
                        {
                            logicManager.ButtonActive(0, "������", "������ ����.", false);
                            characterManager.playerGetDamage(15);
                            logicManager.ForceChangeEventCode(13014);
                            uiManager.itemAddMessage.AddMessage("�ܺ�����");
                            characterManager.AddItem(1002, 1,decay:15);
                        }
                    }
                }

                break;
            #endregion
               #region �ܺ����� ����1
            case 3303:
                bookData.SetDictData("�ܺ�����", 0);
                bookData.SetDictData("�ܺ�����", 1);
                uiManager.itemAddMessage.AddMessage("�ܺ����� ����");
                logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                break;

            #endregion
               #region �ܺ����� ����2
            case 3305:

                bookData.SetDictData("�ܺ�����", 0);
                bookData.SetDictData("�ܺ�����", 2);
                uiManager.itemAddMessage.AddMessage("�ܺ����� ����");
                logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                    
                break;
            #endregion



            #endregion
            #region �ٸ��ϴÿ���
            #region ���� �ݱ�
            case 1302:
                bisOption = true;
                bookData.SetDictData("�ٸ��ϴÿ���", 0);

                if (bookData.vegetalDict["�ٸ��ϴÿ���"].bisBonus1 == 0)
                {
                    Current_EventCode = 513020;
                    logicManager.ButtonActive(0, "����", "���Ÿ� �����Ѵ�", false);
                }
                else
                {
                    Current_EventCode = 513021;
                    logicManager.ButtonActive(0, "����", "������ Ȱ���Ѵ�.", false);
                }                
                break;

            case 513020:
                UnityEngine.Random.InitState(DateTime.Now.Millisecond);
                a = UnityEngine.Random.Range(0, 2);
                Debug.Log(a);
                if(a == 0)
                {
                    Current_EventCode = 513022;
                    logicManager.ForceChangeEventCode(13021);
                    logicManager.ButtonActive(0, "����", "�� �ڼ��� ���캻��", false, option: 0);
                }
                else if(a == 1)
                {
                    Current_EventCode = 513023;
                    logicManager.ForceChangeEventCode(13022);
                    logicManager.ButtonActive(0, "����", "�� �ڼ��� ���캻��", false, option: 0);
                }
                break;

            case 513022:
                UnityEngine.Random.InitState(DateTime.Now.Millisecond+2);
                a = UnityEngine.Random.Range(0, 2);
                Debug.Log(a);
                if (a == 0)
                {
                    Current_EventCode = 613020;
                    logicManager.ForceChangeEventCode(13023);
                    logicManager.ButtonActive(0, "���� �ʴ´�", "�̰� �����غ���.", false, option: 0);
                    logicManager.ButtonActive(1, "�Դ´�", "�� ���� �����ƺ��̳�.", false, option: 0);
                }
                else if (a == 1)
                {
                    Current_EventCode = 613021;
                    logicManager.ForceChangeEventCode(13024);
                    logicManager.ButtonActive(0, "���� �ʴ´�", "�̰� �����غ���.", false, option: 0);
                    logicManager.ButtonActive(1, "�Դ´�", "�� ���� �����ƺ��̳�.", false, option: 0);
                }
                break;
            case 513023:
                UnityEngine.Random.InitState(DateTime.Now.Millisecond+3);
                a = UnityEngine.Random.Range(0, 2);
                Debug.Log(a);
                if (a == 0)
                {
                    Current_EventCode = 613022;
                    logicManager.ForceChangeEventCode(13025);
                    logicManager.ButtonActive(0, "���� �ʴ´�", "�̰� �����غ���.", false, option: 0);
                    logicManager.ButtonActive(1, "�Դ´�", "�� ���� �����ƺ��̳�.", false, option: 0);
                }
                else if (a == 1)
                {
                    Current_EventCode = 613023;
                    logicManager.ForceChangeEventCode(13026);
                    logicManager.ButtonActive(0, "���� �ʴ´�", "�̰� �����غ���.", false, option: 0);
                    logicManager.ButtonActive(1, "�Դ´�", "�� ���� �����ƺ��̳�.", false, option: 0);
                }
                break;

            case 613020:
                bisOption = false;
                if(option == 0)
                {
                    logicManager.ForceChangeEventCode(15021);
                    logicManager.ButtonActive(0, "������", "������ ����.", false, option: 0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(15022);
                    logicManager.ButtonActive(0, "������", "������ ����.", false, option: 0);
                }
                break;
            case 613021:
                bisOption = false;
                if (option == 0)
                {
                    logicManager.ForceChangeEventCode(15021);
                    logicManager.ButtonActive(0, "������", "������ ����.", false, option: 0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(15023);
                    characterManager.playerGetDamage(-5);
                    characterManager.playerUseMana(-5);
                    logicManager.ButtonActive(0, "������", "������ ����.", false, option: 0);
                }
                break;
            case 613022:
                bisOption = false;
                if (option == 0)
                {
                    logicManager.ForceChangeEventCode(15021);
                    logicManager.ButtonActive(0, "������", "������ ����.", false, option: 0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(15024);
                    characterManager.playerUseMana(5);
                    // uiManager.itemAddMessage.AddMessage("�ٸ��ϴÿ���");
                    logicManager.ButtonActive(0, "������", "������ ����.", false, option: 0);
                }
                break;
            case 613023:
                bisOption = false;
                if (option == 0)
                {
                    logicManager.ForceChangeEventCode(15021);
                    logicManager.ButtonActive(0, "������", "������ ����.", false, option: 0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(15025);
                    characterManager.playerGetDamage(-10);
                    logicManager.ButtonActive(0, "������", "������ ����.", false, option: 0);
                }
                break;


            case 513021:
                bisOption = false;
                logicManager.ForceChangeEventCode(13027);
                uiManager.itemAddMessage.AddMessage("�ٸ��ϴÿ���");
                characterManager.AddItem(1601, 1);
                logicManager.ButtonActive(0, "������", "������ ����.", false);
                break;
            #endregion
               #region �ٸ��ϴÿ��� ����1
            case 3302:
                /*
                if(bookData.vegetalDict["�ٸ��ϴÿ���"].bisFirst == 0)
                {
                    bisOption = true;
                    bookData.SetDictData("�ٸ��ϴÿ���",0);
                    logicManager.ForceChangeEventCode(1302);                              
                    Current_EventCode = 513020;
                    logicManager.ButtonActive(0, "����", "���Ÿ� �����Ѵ�", false);                    
                }
                else
                {
                    if (bookData.vegetalDict["�ٸ��ϴÿ���"].bisBonus1 == 1)
                    {
                        bookData.SetDictData("�ٸ��ϴÿ���",1);
                        uiManager.itemAddMessage.AddMessage("�ٸ��ϴÿ��� ����#�ٸ� �ϴ� ����");                       
                        logicManager.ForceChangeEventCode(12012);


                        logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                    }
                    else
                    {
                        bisOption = true;
                      
                            Current_EventCode = 513021;
                            logicManager.ButtonActive(0, "����", "������ Ȱ���Ѵ�.", false);
                        
                    }
                }
                */
                bookData.SetDictData("�ٸ��ϴÿ���", 0);
                bookData.SetDictData("�ٸ��ϴÿ���", 1);
                uiManager.itemAddMessage.AddMessage("�ٸ��ϴÿ��� ����#�ٸ� �ϴ� ����");
            


                logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                break;

            #endregion
            #endregion
            #region ���ָӴ���
            case 2101:
                bookData.SetDictData("���ָӴ���", 0);
                characterManager.playerGetDamage(8);
                logicManager.ButtonActive(0,"������","������ ����.",false);
                break;
            case 3101:
                
                if (bookData.vegetalDict["���ָӴ���"].bisFirst == 0)
                {
                    bookData.SetDictData("���ָӴ���",0);
                    logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                }
                else
                {
                    bisOption = true;
                    Current_EventCode = 531010;
                  
                    logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                    if (bookData.vegetalDict["���ָӴ���"].bisBonus1 == 1)
                        logicManager.ButtonActive(1, "����", 1002, "�ܺ�����", "�� �����Ѵ�.");

                }

                break;
            case 3204:
                /*
                if (bookData.vegetalDict["���ָӴ���"].bisFirst == 0)
                {
                    logicManager.ForceChangeEventCode(3101);
                    bookData.SetDictData("���ָӴ���", 0);
                    logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                }
                else
                {
                    if (bookData.vegetalDict["���ָӴ���"].bisBonus1 == 0)
                    {
                        bookData.SetDictData("���ָӴ���", 1);
                        uiManager.itemAddMessage.AddMessage("+���ָӴ� �� ����");
                        logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 531010;
                        logicManager.ForceChangeEventCode(3101);
                        logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                        logicManager.ButtonActive(1, "����", 1002, "�ܺ�����", "�� �����Ѵ�.");
                    }
                }*/
                bookData.SetDictData("���ָӴ���", 1);
                uiManager.itemAddMessage.AddMessage("+���ָӴ� �� ����");
                logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                break;

            case 3205:
                /*
                if (bookData.vegetalDict["���ָӴ���"].bisFirst == 0)
                {
                    logicManager.ForceChangeEventCode(3101);
                    bookData.SetDictData("���ָӴ���", 0);
                    logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                }
                else
                {
                    if (bookData.vegetalDict["���ָӴ���"].bisBonus2 == 0)
                    {
                        bookData.SetDictData("���ָӴ���", 2);
                        uiManager.itemAddMessage.AddMessage("���ָӴ� �� ����");
                        logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 531010;
                        logicManager.ForceChangeEventCode(3101);
                        logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                        logicManager.ButtonActive(1, "����", 1002, "�ܺ�����", "�� �����Ѵ�.");
                    }
                  
                }
                */
                bookData.SetDictData("���ָӴ���", 2);
                uiManager.itemAddMessage.AddMessage("���ָӴ� �� ����");
                logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                break;

            case 531010:
                bisOption = false;
                if(option == 0)
                {
                    logicManager.ForceChangeEventCode(31012);
                    logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                }

                else if(option == 1)
                {
                    logicManager.ForceChangeEventCode(31013);
                    uiManager.itemAddMessage.AddMessage("ħ��");
                    characterManager.AddItem(1003, 1);
                    logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                }
                break;




            #endregion
            #region ������
               #region ������ ����
            case 2102:
                bookData.SetDictData("������", 0);
                characterManager.playerGetDamage(10);
                logicManager.ButtonActive(0, "������", "������ ����.", false);
                break;
            #endregion
               #region ������ ����
            case 2103:
                bisOption = true;
                Current_EventCode = 521030;
                bookData.SetDictData("������", 0);
                logicManager.ButtonActive(1, 2, "strength", "�����", "���Ÿ� �����.", option: 0);
                if (bookData.vegetalDict["������"].bisBonus1 == 1) characterManager.EventBonus(bookData.vegetalDict["������"].bonus1,"strength",1);

                break;

            case 521030:
                bisOption = false;
                if (testResult >=2)
                {
                    logicManager.ForceChangeEventCode(21031);
                    logicManager.ButtonActive(0, "������", "������ ����.", false);
                }
                else
                {
                    logicManager.ForceChangeEventCode(21032);
                    characterManager.AddDisease(1501);
                    logicManager.ButtonActive(0, "�Ҿ�", "�̰� �� ����", false);
                }
                break;
            #endregion
               #region ������ ����1
            case 3203:
                /*
                if(bookData.vegetalDict["������"].bisFirst == 0)
                {
                    bookData.SetDictData("������", 0);
                    logicManager.ForceChangeEventCode(2103);
                    bisOption = true;
                    Current_EventCode = 521030;
                    logicManager.ButtonActive(1, 2, "strength", "�����", "���Ÿ� �����.", option: 0);
                }
                else
                {
                    if (bookData.vegetalDict["������"].bisBonus1 == 0)
                    {
                        uiManager.itemAddMessage.AddMessage("������ ����");
                        bookData.SetDictData("������", 1);
                        logicManager.ButtonActive(0, "������", "������ ����.", false);                         
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 521030;
                        logicManager.ButtonActive(1, 2, "strength", "�����", "���Ÿ� �����.", option: 0);
                        characterManager.EventBonus(bookData.vegetalDict["������"].bonus1, "strength", 1);
                    }

                }
                */
                uiManager.itemAddMessage.AddMessage("������ ����");
                bookData.SetDictData("������", 0);
                bookData.SetDictData("������", 1);               
                logicManager.ButtonActive(0, "������", "������ ����.", false);
                break;
            #endregion
            #endregion
            #region ���� �Ѹ� 
               #region �濡�� ����
            case 2201:
                characterManager.playerGetDamage(15);
                bookData.SetDictData("���ûѸ�", 0);
                logicManager.ButtonActive(0, "��ó", "��ó�� ���ΰ� ������", false);
                break;
            #endregion
               #region ����� ����
            case 2302:
                bisOption = true;
                Current_EventCode = 523020;
                bookData.SetDictData("���ûѸ�", 0);
                logicManager.ButtonActive(0, "����ģ��", "�������� ����ģ��.", false);
                break;
            case 523020:
                if(characterManager.checkDisease(1501))
                {
                   
                    Debug.Log("��");
                    Current_EventCode = 623020;
                    logicManager.ForceChangeEventCode(23021);
                    logicManager.ButtonActive(0, "��ȣ", "�Ӹ��� ���Ѵ�.", false);
                    logicManager.ButtonActive(1, "�ܺ�����", 1002, "�ܺ�����", "�� ������.");
                    if(bookData.vegetalDict["���� �Ѹ�"].bisBonus1 == 1) logicManager.ButtonActive(2, "������ ����", 1201, "������ ����", "�� ������.");
                }
                else
                {                    
                    bisOption = false;
                    logicManager.ForceChangeEventCode(23022);
                    logicManager.ButtonActive(0, "������", "�� ���� ����", false, option: 0);
                }
                break;
            case 623020:
                bisOption = false;
                if (option == 0)
                {
                
                    logicManager.ForceChangeEventCode(23023);
                    characterManager.playerGetDamage(30);
                    logicManager.ButtonActive(0, "��ó", "��ó�� ���Ѵ�.", false, option: 0);
                }
               else if(option == 1)
                {
                   
                    logicManager.ForceChangeEventCode(23024);
                    uiManager.itemAddMessage.AddMessage("���ûѸ�");
                    characterManager.AddItem(2101, 1);
                    logicManager.ButtonActive(0, "������", "�� ���� ����", false, option: 0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(23025);
                    characterManager.RemoveItem(1201,1);
                    uiManager.itemAddMessage.AddMessage("������ ����",false);
                    logicManager.ButtonActive(0, "������", "�� ���� ����", false);
                }
                break;
            #endregion
               #region ���ûѸ�
            case 3301:
                /*
                if (bookData.vegetalDict["���ûѸ�"].bisFirst == 0)
                {
                    bookData.SetDictData("���ûѸ�",0);
                    logicManager.ForceChangeEventCode(2302);
                    bisOption = true;
                    Current_EventCode = 523020;
                    logicManager.ButtonActive(0, "����ģ��", "�������� ����ģ��.", false);
                }
                else
                {
                    if (bookData.vegetalDict["���ûѸ�"].bisBonus1 == 0)
                    {
                        bookData.SetDictData("���ûѸ�",1);
                        uiManager.itemAddMessage.AddMessage("���ûѸ�");
                        characterManager.AddItem(2101, 1);
                        logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                    }
                    else
                    {
                        logicManager.ForceChangeEventCode(2302);
                        bisOption = true;
                        Current_EventCode = 523020;
                        logicManager.ButtonActive(0, "����ģ��", "�������� ����ģ��.", false);
                    }
                }
                */
                bookData.SetDictData("���ûѸ�", 1);
                bookData.SetDictData("���ûѸ�", 0);
                uiManager.itemAddMessage.AddMessage("���ûѸ� ����");
                logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                break;                                              
            #endregion


            #endregion
            #region Ǯ�������
            #region �������� ����
            case 2202:
                bisOption = true;
                Current_EventCode = 522020;
                bookData.SetDictData("Ǯ���� ����", 0);
                logicManager.ButtonActive(0, 1, "examine", "�����", "���Ÿ� �����.");
                if (bookData.vegetalDict["Ǯ���� ����"].bisBonus1 == 1)
                characterManager.EventBonus(bookData.vegetalDict["Ǯ���� ����"].bonus1,"examine",1);
                if (bookData.vegetalDict["������ ����"].bisBonus2 == 1)
                logicManager.ButtonActive(1, "����", 1201, "������ ����", "�������� �Ѹ���.");
                break;

            case 522020:
                bisOption = false; 
                if (option == 0)
                {
                    if (testResult >= 1)
                    {
                        logicManager.ForceChangeEventCode(22021);
                        logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                    }
                    else
                    {
                        logicManager.ForceChangeEventCode(22022);
                        characterManager.AddDisease(1201);
                        logicManager.ButtonActive(0, "����", "���� ���̰� ��������", false);
                    }
                }
                else
                {
                    uiManager.itemAddMessage.AddMessage("������ ����",false);
                    characterManager.RemoveItem(1201,1);
                    logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                    logicManager.ForceChangeEventCode(22023);
                }

                break;
            #endregion
               #region Ǯ������� ����1
            case 3201:
                logicManager.ForceChangeEventCode(32011);
                bookData.SetDictData("Ǯ���� ����", 1);
                uiManager.itemAddMessage.AddMessage("Ǯ������� ����");
                logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                /*
                if (bookData.vegetalDict["Ǯ���� ����"].bisFirst == 0)
                {
                    bisOption = true;
                    Current_EventCode = 522020;
                    logicManager.ButtonActive(0, 1, "examine", "�����", "���Ÿ� �����.", option: 0);
                }
                else
                {
                    if (bookData.vegetalDict["Ǯ���� ����"].bisBonus1 == 0)
                    {
                        logicManager.ForceChangeEventCode(32011);
                        bookData.SetDictData("Ǯ���� ����",1);
                        uiManager.itemAddMessage.AddMessage("Ǯ������� ����");
                        logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 522020;                      
                        logicManager.ButtonActive(0, 1, "examine", "�����", "���Ÿ� �����.", option: 0);
                        characterManager.EventBonus(bookData.vegetalDict["Ǯ���� ����"].bonus1, "examine", 1);
                        if (bookData.vegetalDict["������ ����"].bisBonus2 == 1)
                            logicManager.ButtonActive(1, "����", 1201, "������ ����", "�������� �Ѹ���.", option: 0);
                    }
                }
                */
                break;
            #endregion
            #endregion
            #region ���߹ٴڹ���
               #region ���� ä��
            case 2203:
                bisOption = true;
                Current_EventCode = 522030;
                bookData.SetDictData("���߹ٴڹ���", 0);
                logicManager.ButtonActive(0, "����", "�ǵ����� �ʴ´�.", false);
                logicManager.ButtonActive(1, "����", "������ �����ϰ�\n������ ����.", false);
                if (bookData.vegetalDict["�����ٴù���"].bisBonus1 == 1)
                {
                    logicManager.ButtonActive(2, "����", 1002, "�ܺ�����", "�� ������\n���� �ִ��� Ȯ��");
                }
                break;

            case 522030:               
                if (option == 0)
                {
                    bisOption = false;
                    Current_EventCode = 12031;
                    logicManager.ForceChangeEventCode(12031);
                    logicManager.ButtonActive(0, "������", "�� ���� ����", false);

                }

                else if (option == 1)
                {
                    bisOption = false;
                    Current_EventCode = 22031;
                    logicManager.ForceChangeEventCode(22031);
                    characterManager.AddDisease(1101);
                    logicManager.ButtonActive(0, "������", "�� ���� ����", false);
                }
                else
                {
                    Current_EventCode = 622030;
                    logicManager.ForceChangeEventCode(12032);
                    if (bookData.vegetalDict["�������� Ǯ"].bisBonus2 == 0)
                    {
                      
                        logicManager.ForceChangeEventCode(22032);
                        logicManager.ButtonActive(0, "������", "�� ���� ����", false);
                    }

                    if (bookData.vegetalDict["�������� Ǯ"].bisBonus2 == 1)
                    {
                        Current_EventCode = 622030;
                        logicManager.ForceChangeEventCode(22033);
                        logicManager.ButtonActive(0, "������", "�� ���� ����", false);
                        logicManager.ButtonActive(1, "�������� Ǯ", 1101, "�������� Ǯ", "�� ����\n�ܾ �����Ѵ�.");
                    }
                }
                break;
            case 622030:
                bisOption = false;
                if (option == 0)
                {
                    logicManager.ForceChangeEventCode(22034);
                    logicManager.ButtonActive(0, "������", "�� ���� ����", false);
                
                }
                else
                {
                    logicManager.ForceChangeEventCode(22035);
                    uiManager.itemAddMessage.AddMessage("���߹ٴڹ���");
                    characterManager.AddItem(2501, 1);
                    logicManager.ButtonActive(0, "������", "�� ���� ����", false);
                }
                break;

            case 2301:
                logicManager.ButtonActive(0, "�ӽ�", "�ӽ÷� ����", false);
                characterManager.AddDisease(1101);
                break;
            #endregion
               #region ���߹ٴڹ��� ���� 1
            case 3202:
                bisOption = true;
                Current_EventCode = 532020;
                logicManager.ButtonActive(0, "�۽�...", "����� ����.", false);
                logicManager.ButtonActive(1, "����", 1002, "�ܺ�����", "�� ������ ����");
                /*
                if(bookData.vegetalDict["���߹ٴڹ���"].bisFirst == 0)
                {
                    bisOption = true;
                    logicManager.ForceChangeEventCode(2203);
                    Current_EventCode = 522030;
                    bookData.SetDictData("���߹ٴڹ���",0);
                    logicManager.ButtonActive(0, "����", "�ǵ����� �ʴ´�.", false);
                    logicManager.ButtonActive(1, "����", "������ �����ϰ�\n������ ����.", false);
                }
                else
                {
                    if(bookData.vegetalDict["���߹ٴڹ���"].bisBonus1 == 0)
                    {
                        bisOption = true;
                        Current_EventCode = 532020;
                        logicManager.ButtonActive(0, "�۽�...", "����� ����.", false);
                        logicManager.ButtonActive(1, "����", 1002, "�ܺ�����", "�� ������ ����");                      
                    }
                    else
                    {
                        logicManager.ForceChangeEventCode(2203);
                       
                        Current_EventCode = 522030;
                        logicManager.ButtonActive(0, "����", "�ǵ����� �ʴ´�.", false);
                        logicManager.ButtonActive(1, "����", "������ �����ϰ�\n������ ����.", false);
                        if (bookData.vegetalDict["�����ٴù���"].bisBonus1 == 1)
                        {
                            logicManager.ButtonActive(2, "����", 1002, "�ܺ�����", "�� ������\n���� �ִ��� Ȯ��");
                        }
                    }
                }
                */
                break;

            case 532020:
                bisOption = false;
                if(option == 0)
                {
                    logicManager.ForceChangeEventCode(32021);
                    logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                }

                else
                {
                    logicManager.ForceChangeEventCode(32022);
                    bookData.SetDictData("���߹ٴڹ���", 1);
                    uiManager.itemAddMessage.AddMessage("���߹ٴڹ��� ����");
                    logicManager.ButtonActive(0, "������", "�� ���� ����.", false);
                }
                break;


            #endregion
            #endregion

            case 1304:
                uiManager.itemAddMessage.AddMessage("��帨");
                characterManager.AddItem(1302,1);
                logicManager.ButtonActive(0, "������.", "�� ���� ����.", false);
                break;

            case 1303:

                bisOption = true;
                logicManager.ForceChangeEventCode(1302);
                bookData.SetDictData("�ٸ��ϴÿ���", 0);

                if (bookData.vegetalDict["�ٸ��ϴÿ���"].bisBonus1 == 0)
                {
                    Current_EventCode = 513020;
                    logicManager.ButtonActive(0, "����", "���Ÿ� �����Ѵ�", false);
                }
                else
                {
                    Current_EventCode = 513021;
                    logicManager.ButtonActive(0, "����", "������ Ȱ���Ѵ�.", false);
                }
              
                break;


            case 1305:
                bisOption = true;
                logicManager.ForceChangeEventCode(1202);
                bookData.SetDictData("������", 0);
                Current_EventCode = 512020;
                logicManager.ButtonActive(0, "������.", "������ ������ ���̳�.", false);
                logicManager.ButtonActive(1, "����", "�� �� ������\n������ ���� ������.", false);

                break;

            case 2303:
                bisOption = true;
                Current_EventCode = 523030;
                logicManager.ButtonActive(0, 2, "examine", "����", "���� �̻��ѵ�?", option: 1);
                break;
            case 523030:
                bisOption = false;
                if (testResult >= 2)
                {
                    logicManager.ForceChangeEventCode(23032);
                    logicManager.ButtonActive(0, "��", "���� ������ �� �߳�", false,option:0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(23031);
                    characterManager.playerGetDamage(20);
                    logicManager.ButtonActive(0, "����", "��ó�� ���Ѵ�.", false, option: 0);
                }
                break;                  
        }

    }

}

public class Test : EventBase
{
    #region essential_table


   private Table.TableClass essential1 = new Table.TableClass(21,0,0,0,21,0,0,0,1,0,0,0);
   private Table.TableClass essential2 = new Table.TableClass(21,0,0,0,21,0,0,0,1,0,0,0);
   private Table.TableClass essential3 = new Table.TableClass(21,0,0,0,21,0,0,0,1,0,0,0);

    


    #endregion
    private List<Table.TableClass> tableList = new List<Table.TableClass>();

    
    
    private int[] indexArray = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    //�ʼ� eventtable 0,3,9
    

    private int good = 3;
    private int mid = 3;
    private int bad = 3;

    private int nuet = 3;
   
    

    public Test()
    {
       
        tableList.Add(_table1);
        tableList.Add(_table2);
        tableList.Add(_table3);
        tableList.Add(_table4);
        tableList.Add(_table5);
        tableList.Add(_2table1);
        tableList.Add(_2table2);
        tableList.Add(_2table3);
        tableList.Add(_2table4);
        tableList.Add(_2table5);
       

        

        TableSet();

       // ShuffleDeck(indexArray, 1, 4);
       // ShuffleDeck(indexArray, 4, 9);

        

        essential1.dataSet(0, 0, 1);
        essential1.dataSet(1, 0, 2);
        essential1.dataSet(2, 0, 3);

        essential2.dataSet(0, 0, 4);
        essential2.dataSet(1, 0, 5);
        essential2.dataSet(2, 0, 6);

        essential3.dataSet(0, 0, 7);
        essential3.dataSet(1, 0, 8);
        essential3.dataSet(2, 0, 9);
        essential1.exceptionSet(2, 0, true);


        tableList[0] = essential1;
        tableList[3] = essential2;
        tableList[9] = essential3;


        //Debug.Log("essential1 : " + essential1.dataGet(0,1));

        progress_index = 0;

        CurrentTable = tableList[progress_index];

        
        // ShuffleDeck(Array3x4);


    }

    public override void TableSet()
    {
        for (int i = 0; i < tableList.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                System.Random rng = new System.Random();

                int risk = 0;
                int tendency = 0;
                int detail = 0;
                int result = 0;

                risk = tableList[i].subGet(j) % 10;

                tendency = risk < 4 ? rng.Next(1, 3) : 3;

                switch (tendency)
                {
                    case 1:
                        detail = rng.Next(1, good + 1);
                        break;
                    case 2:
                        detail = rng.Next(1, mid + 1);
                        break;
                    case 3:
                        detail = rng.Next(1, bad + 1);
                        break;
                    case 4:
                        detail = rng.Next(1, nuet + 1);
                        break;
                }
                result = tendency * 1000 + risk * 100 + detail;

                tableList[i].dataSet(0, j, result);

                risk = tableList[i].midGet(j);
                tendency = risk < 4 ? rng.Next(1, 3) : 3;

                switch (tendency)
                {
                    case 1:
                        detail = rng.Next(1, good + 1);
                        break;
                    case 2:
                        detail = rng.Next(1, mid + 1);
                        break;
                    case 3:
                        detail = rng.Next(1, bad + 1);
                        break;
                    case 4:
                        detail = rng.Next(1, nuet + 1);
                        break;
                }

                result = tendency * 1000 + risk * 100 + detail;

                tableList[i].dataSet(1, j, result);

                risk = tableList[i].proGet(j);
                tendency = risk < 4 ? rng.Next(1, 3) : 3;

                switch (tendency)
                {
                    case 1:
                        detail = rng.Next(1, good + 1);
                        break;
                    case 2:
                        detail = rng.Next(1, mid + 1);
                        break;
                    case 3:
                        detail = rng.Next(1, bad + 1);
                        break;
                    case 4:
                        detail = rng.Next(1, nuet + 1);
                        break;
                }
                result = tendency * 1000 + risk * 100 + detail;
                tableList[i].dataSet(2, j, result);
            }

        }
    }
    public override void Select_Event(int code)
    {
        //base.Select_Event(code);
        progress_index += 1;
        CurrentTable = tableList[indexArray[progress_index]];
    }


    public override void Excute_Event( int code = 0,int testResult =0 , bool isStart = false,int option =0)
    {
        if(code > 0) Current_EventCode = code;
        //Debug
        
        
       
        switch(Current_EventCode)
        {


            case 1:

                bisOption = true;
                logicManager.ButtonActive(0,"1��","1�� ���� �� ���ư��ϱ�???",true);
                logicManager.ButtonActive(1, 2, "strength", "��", "�� �׽�Ʈ");

                
                Current_EventCode = 10;
                


                characterManager.AddItem(1002, 1);
                characterManager.AddItem(1711, 1);
                characterManager.AddItem(1712, 1);

                break;

            case 10:
                bisOption = false;
                if (option ==0)
                {
                    Current_EventCode = 11;
                    logicManager.ForceChangeEventCode(11);
                    logicManager.ButtonActive(0, "1��", "1�� ���� �� ���ư��ϱ�???", true,option :0);
                }
                else if(option == 1)
                {
                    if(testResult >= 1)
                    {
                        Current_EventCode = 12;
                        logicManager.ForceChangeEventCode(12);
                        logicManager.ButtonActive(0, "����", "����!", true, option: 0);
                    }
                    else
                    {
                        Current_EventCode = 13;
                        logicManager.ForceChangeEventCode(13);
                        logicManager.ButtonActive(0, "����", "����!", true, option: 0);
                    }

                }


                break;


            case 2:
                characterManager.AddDisease(1101);
                logicManager.ButtonActive(0,"2��", "2�� ���� �� ���ư��ϱ�???", true);
                logicManager.ButtonActive(1,"��", "�׷�", true);
               
                


                break;
            case 3:

                logicManager.SetBattle("����ƺ�");
                
                break;

            case 30:
                bisOption = false;
                if (option == 0) {
                     Current_EventCode = 31;
                    logicManager.ForceChangeEventCode(31);
                    logicManager.ButtonActive(0, "1�� �ٰ�","��!", true,option : 0);
                    characterManager.AddItem(1121, 1);
                }
                else if (option == 1) {
                     Current_EventCode = 32;
                    logicManager.ForceChangeEventCode(32);
                    logicManager.ButtonActive(0, "2�� �ٰ�","��!!", true, option: 0);
                    characterManager.AddItem(1121, 2);
                }
                else if (option == 2) {
                     Current_EventCode = 33;
                    logicManager.ForceChangeEventCode(33);
                    characterManager.AddDisease(1101);
                    logicManager.ButtonActive(0, "3�� �ٰ�","��!!!", true, option: 0);
                    characterManager.AddItem(1121, 3);                   
                }
                break;
        }

    }

}



public class Observation : EventBase
{
    #region essential_table

    private Table.TableClass essential1 = new Table.TableClass(21, 0, 0, 0, 21, 0, 0, 0, 1, 0, 0, 0);
    private Table.TableClass essential2 = new Table.TableClass(21, 0, 0, 0, 21, 0, 0, 0, 1, 0, 0, 0);
    private Table.TableClass essential3 = new Table.TableClass(21, 0, 0, 0, 21, 0, 0, 0, 1, 0, 0, 0);

    #endregion
    private List<Table.TableClass> tableList = new List<Table.TableClass>();


    private int[] indexArray = new int[2] {0, 1};
    //�ʼ� eventtable 0,3,9

    public Observation()
    {     
        essential1.dataSet(0, 0, 1);
        essential1.dataSet(1, 0, 2);
        essential1.dataSet(2, 0, 3);

        essential2.dataSet(0, 0, 4);
        essential2.dataSet(1, 0, 5);
        essential2.dataSet(2, 0, 6);

        tableList.Add(essential1);
        tableList.Add(essential2);

        //essential2.exceptionSet(0, 0, true);
        //essential1.exceptionSet(0, 0, true);
    
        progress_index = 0;

        CurrentTable = tableList[progress_index];


       


    }

    public override void TableSet()
    {
      
    }
    public override void Select_Event(int code)
    {
        //base.Select_Event(code);
        progress_index += 1;
        CurrentTable = tableList[indexArray[progress_index]];
    }


    public override void Excute_Event(int code = 0, int testResult = 0, bool isStart = false, int option = 0)
    {
        if (code > 0) Current_EventCode = code;
        
        switch (Current_EventCode)
        {
            case 1:
                //logicManager.SetBattle("�ٸ�����");
                logicManager.ButtonActive(0,"Ž��","�ȿ� ������ �ִ���\nȮ���Ѵ�.",false);
                //characterManager.AddItem(1401, 3);
                break;

            case 2:
                logicManager.ButtonActive(0, "Ž��", "�� �ѷ�����", false , option : 5);
                characterManager.AddItem(1111,1);
                characterManager.AddItem(1121,1);
                characterManager.AddItem(1131,1);

                characterManager.AddItem(1101,1);
                characterManager.AddItem(1301,1);
                characterManager.AddItem(2101,1);

                uiManager.itemAddMessage.AddMessage("�ٸ��ٽ�#��δ�#�츣��");
                break;

            case 3:
                bisOption = true;
                logicManager.ButtonActive(0, "â�� ����", "����... ����?", false);
                Current_EventCode = 30;
                      
                break;

            case 30:
                logicManager.ButtonActive(0, "Ȯ��", "���� â�� Ȯ���Ѵ�.", false , option: 0);
                Current_EventCode = 31;
                logicManager.ForceChangeEventCode(31);
                break;

            case 31:
                logicManager.ButtonActive(0, "����", "�ʻ������� ���� ���´�.", false , option: 0);
                Current_EventCode = 32;
                logicManager.ForceChangeEventCode(32);
                break;

            case 32:
                logicManager.ButtonActive(0, "����", "�ֻ縦 ���� �ȴ´�", false , option: 0);
                Current_EventCode = 33;
                logicManager.ForceChangeEventCode(33);

                break;

            case 33:
                Current_EventCode = 34;
                logicManager.ForceChangeEventCode(34);
                break;

            case 34:
                bisOption = false;
                Current_EventCode = 35;
                logicManager.ForceChangeEventCode(35);
                logicManager.ButtonActive(0, "����", "���� �������� �ʴ´�.", false, option: 1);
                break;

            case 4:
                logicManager.SetBattle("�ٸ�����");
                break;



        }
    }

}





public class EventManager : MonoBehaviour,IManager
{
    public GameManager gameManager { get { return GameManager.gameManager; } }
    //public EventData eventData;

    public BookData bookData;

    // ���� � ������, � �̺�Ʈ�ȿ� ���Դ°�?
    public EventBase CurrentEvent { get; set; }

    // �̺�Ʈ�� �� ����� ó�ڰ� ����
    
    private List<EventBase> EventList = new List<EventBase>();

    private LogicManager logicManager;
    private CharacterManager characterManager;
    private UIManager uiManager;
    private ResourceManager resourceManager;


    private void Awake()
    {
        logicManager     =  GameManager.GetManagerClass<LogicManager>();
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        uiManager        = GameManager.GetManagerClass<UIManager>();
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        //gameManager.bookData += GetBookData;
       
    }

    private void Start()
    {        
       // Test test = new Test();
        Tutorial tutorial = new Tutorial();
        YenaGarden yenaGarden = new YenaGarden();
        Observation observation = new Observation();

       // EventList.Add(test);
        EventList.Add(tutorial);
        EventList.Add(yenaGarden);
        EventList.Add(observation);

        LoadArea("Observation");
        resourceManager.GetBookInfo(out bookData);      
    }

    public void GetBookData()
    {
        resourceManager.GetBookInfo(out bookData);
    }

    public void GetBookData(BookData bookdata)
    {
        this.bookData = bookdata;
    }

    public void LoadArea(string area)
    {
        foreach (EventBase E in EventList)
        {
            if (E.GetType().ToString() == area)
                CurrentEvent = E;            
        }
        
        CurrentEvent.logicManager = this.logicManager;
        CurrentEvent.characterManager = this.characterManager;
        CurrentEvent.uiManager = this.uiManager;
        CurrentEvent.bookData = bookData;
        
    }
   

    
}
