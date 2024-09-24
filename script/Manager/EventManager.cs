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
     * 테이블 규칙
     * 1의자리수 : 이벤트의 세기
     * 그 이상의 자리수 : 연결된 이벤트를 나타냄
     *  ex) 6 => 2(1+1) X 3(2+1)  1,2 와 연결됨
     *      24 => 2(1+1) X 3(2+1) X 4(3+1) 1,2,3 과 연결됨
     *      30 =>2(1+1) X 3(2+1) X 5(4+1)  1,2,4 와 연결됨
     *      
     *      즉...61 => 2번째 줄 1,2 번과 연결된 이벤트 강도 1 
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
    만약 선택지나 연계 이벤트 처럼 이벤트가 단일 카드로 끝나지 않는다면
    bisOption = true로 하십시오
    이 때 마지막 이벤트에서는 bisOption = false로 해야합니다.

    이후 Current_EventCode 를
    플레이어의 선택에 따른 처리를 해주는 구문으로 변경하십시오
    
    ex) 3    ->  Current_EventCode = 30
        1101 ->  Current_EventCode = 511010
        2101 ->  Current_EventCode = 521010
     
    이후 그 구문에서 플레이어의 선택에 따른 결과를 처리해주면 됩니다.
    이때 플레이어가 낸 카드의 번호는 option 
                   테스트의 결과는 testResult 입니다.

    변경된 테스트를 출력하고 싶다면 logicManager.ForceChangeEventCode로 
    코드를 변경하십시오

    ex) 0번 카드를 냈다면 그냥 패스, 1번 카드를 냈다면 10 피해를 주고 싶을때
    
    if(option == 0)
    {
      logicManager.ForceChangeEventCode(11011);
      logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
    }
    else if(option == 1)
    {
      logicManager.ForceChangeEventCode(11012);
      characterManager.PlayerGetDamage(10);
      logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
    }

    ex) 테스트 난이도가 1이고, 실패하면 10 피해를 주고 싶을때

    if(testResult >=1)
    {
      logicManager.ForceChangeEventCode(11011);
      logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
    }
    else
    {
      logicManager.ForceChangeEventCode(11012);
      characterManager.PlayerGetDamage(10);
      logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
    }

    buttonActive(); 함수
       





    질병을 걸때 (addDisease) 따로 실행해야하는 명령은 없습니다.
    그냥 박으십시오.

     그러나 전투를 걸때는  
     지역의 생성자 부분에서
     테이블 셔플이 끝난후
     essential.exceptionSet(2, 0, true);
     를 걸어주어야합니다.
     이때 essential은 테이블을 의미하며
     (2,0,true)는 3번째줄 1번째 칸의 선택지를 전투로 할 것을 의미합니다. 

     logicManager.SetBattle(string name);
     함수로 전투를 걸 수 있습니다.
     name이 틀리면 당연히 좃도ㅚㅂ니다.
         
         
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


#region 튜토리얼 
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

    #region 튜토리얼
    public override void Excute_Event(int code =0 , int testResult = 0, bool isStart = false, int option =0)
    {
        if (code > 0) Current_EventCode = code;
       
        switch (Current_EventCode)
        {           
            case 1:                
                logicManager.ButtonActive(0, "전진", "공장을 향해 나아간다", false);
               

            

                break;

            case 10:
                bisOption = false;
                if (testResult == 1)
                {
                    Current_EventCode = 11;
                    logicManager.ForceChangeEventCode(11);
                    logicManager.ButtonActive(0, "오우 스냅", "오우 스냅", false, option: 0);
                }
                else  if (testResult == 2)
                {
                    Current_EventCode = 12;
                    logicManager.ForceChangeEventCode(12);
                    logicManager.ButtonActive(0, "오우 스냅!!!", "오우 스냅!!!", false, option: 0);
                }
                else
                {
                    Current_EventCode = 13;
                    logicManager.ForceChangeEventCode(13);
                    logicManager.ButtonActive(0, "탈출", "탈출", false, option: 0);
                }
                break;

            case 2:
                bisOption = true;
                logicManager.ButtonActive(0, "관찰", "책을 펼처본다", false);
                Current_EventCode = 20;
                break;

            case 20:
                bisOption = true;
                Current_EventCode = 21;
                uiManager.Prologues("DictionaryStat");
                logicManager.ForceChangeEventCode(21);                
                logicManager.ButtonActive(0, "의문의 괴성", "이게 대체 무슨 소리지?", false,option:2);

                break;

            case 21:
                bisOption = false;
                Current_EventCode = 22;
                logicManager.ForceChangeEventCode(22);
                logicManager.ButtonActive(0, "자리 피하기", "이곳에서 황급히 달아난다.", false,option:3);

                break;


            case 3:
                bisOption = true;
                characterManager.AddDisease(2301);
                logicManager.ButtonActive(0,1,"strength","억지로 문을 연다.","온 힘을 다해\n문을 억지로 엽니다.",option:0);
                Current_EventCode = 30;
                break;

            case 30:
                bisOption = false;
                if (testResult >= 1)
                {
                    Current_EventCode = 31;
                    logicManager.ForceChangeEventCode(31);
                    logicManager.ButtonActive(0, "성공", "성공!", false, option: 0);
                }
                else
                {

                    Current_EventCode = 32;
                    logicManager.ForceChangeEventCode(31);
                    logicManager.ButtonActive(0, "실패", "실패!", false, option: 0);
                }

                break;

            case 4:
                bisOption = true;
                logicManager.ButtonActive(0, "탐색", "상자를 줍는다.", false, option: 1);
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
                logicManager.ButtonActive(0, 5, "stealth", "장롱에 숨는다.", "숨을 죽이고\n장롱에 숨습니다.", option: 0);

                break;
            case 42:
                bisOption = false;
                Current_EventCode = 43;
                logicManager.ForceChangeEventCode(43);                
                logicManager.ButtonActive(0, "도주", "서둘러 빠져나간다.", false, option: 1);
                break;

            case 5:
                bisOption = true;
                Current_EventCode = 50;
                logicManager.ButtonActive(0, "주방을 향해", "부디 주방에 그것이 있기를...", false, option: 1);
                break;

            case 50:
                Current_EventCode = 51;
                logicManager.ForceChangeEventCode(51);

                //logicManager.ButtonActive(0, "s");
                logicManager.ButtonActive(0, "탐색", "체를 뒤집어 쓴다.", false, option: 0);
                
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
                logicManager.ButtonActive(0, "방황", "기약없이 앞으로", false, option: 0);
                //logicManager.ButtonActive();
                break;

          
            case 6:
                logicManager.ButtonActive(0, "풀숲을 향해", "길을 따라 걷는다.", false, option: 1,bisEnd:true);
                
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
    #region 생성자
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
            #region 메인 스토리

            case 1:
                bisOption = true;
                Current_EventCode = 5010;
             
                logicManager.ButtonActive(0,"떠난다","감상에 빠질 시간은 없다.",false);
                logicManager.ButtonActive(1,"탐색","주위를 살펴본다",false);
                break;

            case 5010:
                bisOption = false;
                if(option == 0)
                {
                   
                    logicManager.ForceChangeEventCode(101);
                    logicManager.ButtonActive(0, "떠난다", "자리를 떠난다.", false, option: 0);
                }
                else
                {                   
                    logicManager.ForceChangeEventCode(102);
                    characterManager.AddItem(1121, 1);
                    uiManager.itemAddMessage.AddMessage("모로닐");
                    logicManager.ButtonActive(0, "떠난다", "자리를 떠난다.", false, option: 0);
                }
                break;

            case 2:
              
                logicManager.ButtonActive(0, "전진", "더 깊은 곳으로", false);
                break;

            case 3:
                
                bisOption = true;
                Current_EventCode = 31;
                logicManager.ButtonActive(0, "전진", "길을 따라", false);
                break;

            case 31:
                bisOption = false;
                logicManager.ForceChangeEventCode(31);
                Current_EventCode = 31;
                logicManager.ButtonActive(0, "전진", "길을 따라", false);
                break;

            case 4:
                bisOption = true;
                Current_EventCode = 41;
            
                logicManager.ButtonActive(0, "사람?", "이런 곳에 사람이?", false);
                break;

            case 41:
                logicManager.ForceChangeEventCode(41);
                Current_EventCode = 42;
                logicManager.ButtonActive(0, "다음", "", false, option: 0);
                break;

            case 42:
                logicManager.ForceChangeEventCode(42);
                bisOption = false;
                characterManager.AddItem(1131, 1);
                characterManager.AddItem(1211, 1);
                uiManager.itemAddMessage.AddMessage("알레아신#헤레나민");
                logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false, option: 0);
                break;

            case 5:
                bisOption = true;
                Current_EventCode = 51;
                logicManager.ButtonActive(0, "숨는다", "풀사이에 숨는다.", false);
                break;

            case 51:
                logicManager.ForceChangeEventCode(51);
                Current_EventCode = 52;
                logicManager.ButtonActive(0, "의문의 남자", "남자를 살펴본다", false, option: 0);
                break;

            case 52:
                logicManager.ForceChangeEventCode(52);
                Current_EventCode = 53;
                logicManager.ButtonActive(0, "다리...?", "잘린 다리를 주우러 간다.", false,option:0);
                break;

            case 53:
                logicManager.ForceChangeEventCode(53);
                Current_EventCode = 54;
                logicManager.ButtonActive(0, "다리를 줍는다", "이 또한 지나가리라", false, option: 0);
                break;

            case 54:
                logicManager.ForceChangeEventCode(54);
                bisOption = false;                            
                logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false, option: 0);
                break;

            case 6:
                bisOption = true;
                Current_EventCode = 61;
                logicManager.ForceChangeEventCode(6);
                logicManager.ButtonActive(0, "풀숲의 끝", "앞을 향해", false);
                break;

            case 61:
                bisOption = false;
                logicManager.ForceChangeEventCode(61);
             
                logicManager.ButtonActive(0, "???", "???", false, option: 1, bisEnd: true);
                break;

            #endregion
            #region 도포보 열매
            #region 도포보 열매따기
            case 1101:
                bookData.SetDictData("도포보 열매",0);
                bisOption = true;
                Current_EventCode = 511010;
                logicManager.ForceChangeEventCode(1101);
                logicManager.ButtonActive(0,"채집","열매를 딴다",false);
                logicManager.ButtonActive(1,"포기","자리를 떠난다.",false);
                break;

            case 511010:

                if (option == 0)
                {
                    if (bookData.vegetalDict["도포보 열매"].bisBonus1 == 0)
                    {
                        bisOption = false;

                       
                        logicManager.ForceChangeEventCode(11012);

                        characterManager.playerUseMana(10);                       
                        characterManager.AddItem(1201, 1);
                        logicManager.ButtonActive(0, "으...", "코가 아직도 얼얼하네", false, option: 0);
                        uiManager.itemAddMessage.AddMessage("도포보 열매");
                        break;
                    }
                    else
                    {
                        bisOption = false;
                        
                        logicManager.ForceChangeEventCode(11013);
                        uiManager.itemAddMessage.AddMessage("도포보 열매");
                        characterManager.AddItem(1201, 1);
                        logicManager.ButtonActive(0, "도포보 열매", "열매를 챙겨간다", false, option: 0);
                    }
                }
                else if(option == 1)
                {
                    bisOption = false;
                  
                    logicManager.ForceChangeEventCode(11011);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다", false, option: 0);
                }

                break;
            #endregion
               #region 도포보 열매 지식1
            case 3102:
                /*
                if(bookData.vegetalDict["도포보 열매"].bisFirst == 0)
                {
                    bookData.SetDictData("도포보 열매", 0);
                    bisOption = true;
                    Current_EventCode = 511010;
                    logicManager.ForceChangeEventCode(1101);
                    logicManager.ButtonActive(0, "채집", "열매를 딴다", false);
                    logicManager.ButtonActive(1, "포기", "자리를 떠난다.", false);
                    break;
                }
                else
                {
                    if (bookData.vegetalDict["도포보 열매"].bisBonus1 == 0)
                    {
                        bookData.SetDictData("도포보 열매", 1);
                        uiManager.itemAddMessage.AddMessage("+도포보 열매 지식");
                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false);
                        uiManager.itemAddMessage.AddMessage("도포보 열매");
                        characterManager.AddItem(1201, 1);
                        logicManager.ButtonActive(0, "도포보 열매", "열매를 챙겨간다", false);
                    }
                    else
                    {                        
                        bisOption = true;
                        Current_EventCode = 511010;
                        logicManager.ForceChangeEventCode(1101);
                        logicManager.ButtonActive(0, "채집", "열매를 딴다", false);
                        logicManager.ButtonActive(1, "포기", "자리를 떠난다.", false);
                        break;
                    }
                }
                */
                bookData.SetDictData("도포보 열매", 0);
                bookData.SetDictData("도포보 열매", 1);
                uiManager.itemAddMessage.AddMessage("+도포보 열매 지식");
              
                uiManager.itemAddMessage.AddMessage("도포보 열매");
                characterManager.AddItem(1201, 1);
                logicManager.ButtonActive(0, "도포보 열매", "열매를 챙겨간다", false);
                break;

                #endregion
                #region 도포보 열매 지식2
                
            case 3304:
                /*
                   if (bookData.vegetalDict["도포보 열매"].bisFirst == 0)
                   {
                       bookData.SetDictData("도포보 열매", 0);
                       bisOption = true;
                       Current_EventCode = 511010;
                       logicManager.ForceChangeEventCode(1101);
                       logicManager.ButtonActive(0, "채집", "열매를 딴다", false);
                       logicManager.ButtonActive(1, "포기", "자리를 떠난다.", false);
                       break;
                   }
                   else
                   {
                       if (bookData.vegetalDict["도포보 열매"].bisBonus2 == 0)
                       {
                           bookData.SetDictData("도포보 열매", 2);
                           uiManager.itemAddMessage.AddMessage("도포보 열매 지식");
                           logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                       }
                       else
                       {
                           bisOption = true;
                           Current_EventCode = 511010;
                           logicManager.ForceChangeEventCode(1101);
                           logicManager.ButtonActive(0, "채집", "열매를 딴다", false);
                           logicManager.ButtonActive(1, "포기", "자리를 떠난다.", false);
                           break;
                       }
                   }
                   */
                bookData.SetDictData("도포보 열매", 0);
                bookData.SetDictData("도포보 열매", 2);
                uiManager.itemAddMessage.AddMessage("도포보 열매 지식");
                logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                break;
            #endregion
            #endregion
            #region 콩억새
               #region 콩억새 따기
            case 1102:
                bisOption = true;
                bookData.SetDictData("콩억새", 0);
                Current_EventCode = 511020;
                logicManager.ButtonActive(0,"채집","한 움큼 딴다.",false);              
                logicManager.ButtonActive(1,"잔불쟁이",1002,"잔불쟁이","로 콩을 굽는다.");
                
                break;

            case 511020:
                bisOption = false;
                if (option == 0)
                {
                    Current_EventCode = 11021;
                    logicManager.ForceChangeEventCode(11021);
                    uiManager.itemAddMessage.AddMessage("콩억새");
                    characterManager.AddItem(1401,1);
                    logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
                }
                else
                {
                    logicManager.ForceChangeEventCode(11022);
                    characterManager.AddItem(2401, 1);
                    uiManager.itemAddMessage.AddMessage("구운 콩억새");
                    logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
                }
                break;
            #endregion
               #region 구운 콩억새
            case 1205:

                bookData.SetDictData("콩억새", 0);
                bookData.SetDictData("콩억새", 1);
                characterManager.AddItem(2401, 1);
                uiManager.itemAddMessage.AddMessage("구운 콩억새");
                logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);


                break;
            #endregion            
            #endregion
            #region  파주천초
            case 1103:
                if (bookData.vegetalDict["파주천초"].bisFirst == 0) bookData.SetDictData("파주천초",0);
                bisOption = true;
                Current_EventCode = 511030;
                logicManager.ButtonActive(0, "채집", "풀을 딴다", false);
                break;

            case 511030:
                bisOption = false;
                if (bookData.vegetalDict["파주천초"].bisBonus1 == 0)
                {
                   logicManager.ForceChangeEventCode(11031);
                    uiManager.itemAddMessage.AddMessage("파주천초");
                    characterManager.AddItem(1501, 1);
                    logicManager.ButtonActive(0, "", "길을 떠난다.", false);
                }
                else
                {
                    logicManager.ForceChangeEventCode(11032);
                    uiManager.itemAddMessage.AddMessage("파주천초#파주천초");
                    characterManager.AddItem(1501, 2);
                    logicManager.ButtonActive(0, "", "길을 떠난다.", false);
                }
                 break;

            case 3103:
                /*
                if (bookData.vegetalDict["파주천초"].bisFirst == 0)
                {
                    bookData.SetDictData("파주천초", 0);
                    bisOption = true;
                    Current_EventCode = 511030;
                    logicManager.ForceChangeEventCode(1103);
                    logicManager.ButtonActive(0, "채집", "풀을 딴다", false);
                    break;
                }
                else
                {
                    if (bookData.vegetalDict["파주천초"].bisBonus1 == 0)
                    {                        
                        bookData.SetDictData("파주천초", 1);                       
                        logicManager.ButtonActive(0, "", "떠난다.", false);
                        uiManager.itemAddMessage.AddMessage("파주천초#파주천초");
                        characterManager.AddItem(1501, 2);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 511030;
                        logicManager.ForceChangeEventCode(1103);
                        logicManager.ButtonActive(0, "채집", "풀을 딴다", false);
                        break;
                    }
                }
                */
                bookData.SetDictData("파주천초", 0);
                bookData.SetDictData("파주천초", 1);
                logicManager.ButtonActive(0, "", "떠난다.", false);
                uiManager.itemAddMessage.AddMessage("파주천초#파주천초");
                characterManager.AddItem(1501, 2);
                break;


            #endregion
            #region 수근쟁이 풀
               #region 수근쟁이 풀 채집
            case 1104:
                bisOption = true;
                bookData.SetDictData("수근쟁이풀", 0);
                Current_EventCode = 511040;
                logicManager.ButtonActive(0, "채집", "풀을 캔다", false);
                if (bookData.vegetalDict["수근쟁이풀"].bisBonus1 == 1)
                {
                    if (bookData.vegetalDict["수근쟁이풀"].bisBonus2 == 1) characterManager.EventBonus(bookData.vegetalDict["수근쟁이풀"].bonus1, "agility", 1);
                    logicManager.ButtonActive(1, 1, "agility", "채집", "줄기를 조심히 잡는다.", option: 0);
                }

                    break;


            case 511040:
                bisOption = false;
                if(option == 0)
                {
                    Current_EventCode = 11041;
                    logicManager.ForceChangeEventCode(11041);
                    logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
                }
                else
                {
                    if(testResult >= 1)
                    {
                        Current_EventCode = 11042;
                        logicManager.ForceChangeEventCode(11042);
                        uiManager.itemAddMessage.AddMessage("수근쟁이풀");
                        characterManager.AddItem(1101, 1);
                        logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
                    }
                    else
                    {
                        Current_EventCode = 11043;
                        logicManager.ForceChangeEventCode(11043);
                        logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
                    }
                }
               

                break;
            #endregion
               #region 수근쟁이풀 지식1
            case 3104:
                /*
                if(bookData.vegetalDict["수근쟁이풀"].bisFirst == 0)
                {

                    bisOption = true;
                    bookData.SetDictData("수근쟁이풀", 0);
                    logicManager.ForceChangeEventCode(1104);
                    Current_EventCode = 511040;
                    logicManager.ButtonActive(0, "채집", "풀을 캔다", false);
                }
                else
                {
                    if(bookData.vegetalDict["수근쟁이풀"].bisBonus1 == 0)
                    {                       
                        uiManager.itemAddMessage.AddMessage("수근쟁이풀 지식");
                        bookData.SetDictData("수근쟁이풀",1);
                        logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 511040;
                        logicManager.ForceChangeEventCode(1104);
                        logicManager.ButtonActive(0, "채집", "풀을 캔다", false);
                        if (bookData.vegetalDict["수근쟁이풀"].bisBonus1 == 1)
                        {
                            logicManager.ButtonActive(1, 1, "agility", "채집", "줄기를 조심히 잡는다.", option: 0);
                        }
                    }
                }
                */
                uiManager.itemAddMessage.AddMessage("수근쟁이풀 지식");
                bookData.SetDictData("수근쟁이풀", 1);
                logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
                break;

            #endregion
               #region 수근쟁이풀 지식2
            case 3105:
                /*
                if (bookData.vegetalDict["수근쟁이풀"].bisFirst == 0)
                {
                    bisOption = true;
                    Current_EventCode = 511040;
                    bookData.SetDictData("수근쟁이풀", 0);
                    logicManager.ForceChangeEventCode(1104);
                    logicManager.ButtonActive(0, "채집", "풀을 캔다", false);
                }
                else
                {
                    if (bookData.vegetalDict["수근쟁이풀"].bisBonus2 == 0)
                    {                     
                        uiManager.itemAddMessage.AddMessage("수근쟁이풀 지식");
                        bookData.SetDictData("수근쟁이풀", 2);
                        logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 511040;
                        logicManager.ForceChangeEventCode(1104);
                        logicManager.ButtonActive(0, "채집", "풀을 캔다", false);
                        if (bookData.vegetalDict["수근쟁이풀"].bisBonus1 == 1)
                        {
                            logicManager.ButtonActive(1, 1, "agility", "채집", "줄기를 조심히 잡는다.", option: 0);
                        }
                    }
                }
                */
                uiManager.itemAddMessage.AddMessage("수근쟁이풀 지식");
                bookData.SetDictData("수근쟁이풀", 2);
                logicManager.ButtonActive(0, "떠난다.", "떠난다.", false);
                break;
            #endregion
            #endregion
            #region 갈수이나비
            case 1105:
                bookData.SetDictData("갈수이 나비", 0);
                logicManager.ButtonActive(0, "", "길을 떠난다.", false);
                uiManager.itemAddMessage.AddMessage("나비날개");
                characterManager.AddItem(1301, 1);
                break;

            case 1201:
                bisOption = true;
                bookData.SetDictData("갈수이 나비", 0);
                Current_EventCode = 512010;
                logicManager.ButtonActive(0, "동행", "나비를 따라간다.", false);
                break;

            case 512010:
                int a = UnityEngine.Random.Range(0, 3);
                if(a ==0)
                {
                    Current_EventCode = 512011;
                    logicManager.ForceChangeEventCode(12011);
                    logicManager.ButtonActive(0,"열매를 으깬다","온 힘을 다해\n열매를 으깬다.",false,option:0);
                    if (bookData.vegetalDict["늪주머니쥐"].bisBonus2 == 1) logicManager.ButtonActive(1, "침샘", 1003, "침샘", "침샘을 갔다댄다", option: 0);                      
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
                        logicManager.ButtonActive(0,1,"strength","박 깨기","힘을 주어\n박을 으깬다",option:0);
                    }
                    else
                    {
                        bisOption = false;
                        logicManager.ForceChangeEventCode(12017);
                        logicManager.ButtonActive(0,  "챙기기", "씨라도 챙겨간다", false,option: 0);
                        uiManager.itemAddMessage.AddMessage("유박 씨");
                        characterManager.AddItem(1004, 1);
                    }
                }
                else
                {
                    bisOption = false;
                    logicManager.ForceChangeEventCode(12018);
                    logicManager.ButtonActive(0, "이런... " ,"갈 길을 간다", false, option: 0);
                }


                break;
            case 512011:
                if(option == 0)
                {
                    bisOption = false;
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false, option: 0);
                        
                        logicManager.ForceChangeEventCode(12012);
                       
                    

                }
                else
                {
                    bisOption = false;
                    logicManager.ButtonActive(0, "떠난다", "쇠드릅을 챙겨 간다", false, option: 0);
                    
                    logicManager.ForceChangeEventCode(12013);
                    uiManager.itemAddMessage.AddMessage("쇠드릅");
                    characterManager.AddItem(1401, 1);
                }
                break;

        
            case 512012:
               
                if(testResult >= 1)
                {
                    bisOption = false;
                    logicManager.ForceChangeEventCode(12015);
                    logicManager.ButtonActive(0, "성공", "전리품을 챙긴다", false, option: 0);
                    uiManager.itemAddMessage.AddMessage("유박");
                    characterManager.AddItem(1202, 1);
                }
                else
                {
                    bisOption = false;
                    logicManager.ForceChangeEventCode(12016);
                    logicManager.ButtonActive(0, "실패", "갈 길을 간다", false,option:0);
                }
                        
                break;
            #endregion
            #region 버마꽃
            case 1202:
                bisOption = true;
                bookData.SetDictData("버마꽃", 0);
                Current_EventCode = 512020;
                logicManager.ButtonActive(0, "떠난다.", "딱봐도 위험해 보이네.", false);
                logicManager.ButtonActive(1, "섭취", "한 입 정도는\n맛봐도 되지 않을까.", false);

                break;

            case 512020:

                if (option == 1)
                {
                    Current_EventCode = 612020;
                    logicManager.ForceChangeEventCode(12021);
                    characterManager.playerGetDamage(-5);
                    characterManager.playerUseMana(-5);
                    logicManager.ButtonActive(0, "떠난다.", "더 먹는건 위험해.", false);
                    logicManager.ButtonActive(1, "섭취", "안전한것 같은걸.", false);
                }
                else
                {
                    bisOption = false;
                    Current_EventCode = 12022;
                    logicManager.ForceChangeEventCode(12022);
                    logicManager.ButtonActive(0, "떠난다.", "위험을 감수할 필요는 없지.", false);
                }

                break;

            case 612020:
                if (option == 1)
                {
                    Current_EventCode = 712020;
                    logicManager.ForceChangeEventCode(12023);
                    characterManager.playerGetDamage(-5);
                    characterManager.playerUseMana(-5);
                    logicManager.ButtonActive(0, "떠난다.", "뭔가 감이 좋지 않아.", false);
                    logicManager.ButtonActive(1, "섭취", "한입만 더...", false);
                }
                else
                {
                    Current_EventCode = 12022;
                    bisOption = false;
                    logicManager.ForceChangeEventCode(12022);
                    logicManager.ButtonActive(0, "떠난다.", "위험을 감수할 필요는 없지.", false);
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
                        logicManager.ButtonActive(0, "떠난다.", "몸이 개운하네.", false);

                    }
                    else
                    {
                        Current_EventCode = 12022;
                        logicManager.ForceChangeEventCode(12022);
                        logicManager.ButtonActive(0, "떠난다.", "위험을 감수할 필요는 없지.", false);
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
                        logicManager.ButtonActive(0, "후회", "떠난다.", false);

                    }
                    else
                    {
                        Current_EventCode = 12022;
                        logicManager.ForceChangeEventCode(12022);
                        logicManager.ButtonActive(0, "떠난다.", "위험을 감수할 필요는 없지.", false);
                    }
                }

               
              
                break;


            #endregion
            #region  범혓바늘버섯
            case 1203:
                bisOption = true;
                Current_EventCode = 512030;
                logicManager.ButtonActive(0, "포기", "건들이지 않는다.", false);
                logicManager.ButtonActive(1, "따기", "위험을 감수하고\n버섯을 딴다.", false);
                if(bookData.vegetalDict["굉발바닥버섯"].bisBonus1 == 1)
                {            
                    logicManager.ButtonActive(2, "가열", 1111, "잔불쟁이", "로 가열해\n독이 있는지 확인");
                }
                break;

            case 512030:
                bisOption = false;
                if (option == 0)
                {
                    Current_EventCode = 12031;
                    logicManager.ForceChangeEventCode(12031);
                    logicManager.ButtonActive(0,"떠난다","갈 길을 간다",false);

                }

                else if(option == 1)
                {
                    Current_EventCode = 12032;
                    logicManager.ForceChangeEventCode(12032);
                    uiManager.itemAddMessage.AddMessage("범혓바늘버섯");
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false);
                }
                else
                {
                    Current_EventCode = 12033;
                    logicManager.ForceChangeEventCode(12033);
                    uiManager.itemAddMessage.AddMessage("범혓바늘버섯");
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false);
                }

                break;



            #endregion
            #region 잔불쟁이
               #region  시든 잔불쟁이
            case 1204:
                bisOption = true;
                Current_EventCode = 512040;
                logicManager.ButtonActive(0, "처다보기", "신기한 꽃이네", false);
                if(bookData.vegetalDict["잔불쟁이"].bisBonus2 == 1) logicManager.ButtonActive(1, "파주천초", 1111, "파주천초", "의 즙을 꽃에 뿌린다.");

                break;
          
            case 512040:
                bisOption = false;
                if(option == 0)
                {
                    logicManager.ForceChangeEventCode(12041);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                }
                else
                {
                    logicManager.ForceChangeEventCode(12042);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                    uiManager.itemAddMessage.AddMessage("잔불쟁이");
                }

                break;
            #endregion
               #region 잔불쟁이
            case 1301:
                bisOption = true;
                bookData.SetDictData("잔불쟁이", 0);
                Current_EventCode = 513010;
                logicManager.ButtonActive(0, "무시", "손 대지 않는다.", false);
                logicManager.ButtonActive(1, 1, "agility", "채집", "꽃을 딴다", option: 0);
                break;

            case 513010:
                bisOption = false;
                if(option == 0)
                {
                    logicManager.ForceChangeEventCode(13015);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                }
                else
                {
                    if(testResult >= 1)
                    {                        
                        if(bookData.vegetalDict["잔불쟁이"].bisBonus1 == 0)
                        {
                            logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                            logicManager.ForceChangeEventCode(13011);
                            uiManager.itemAddMessage.AddMessage("잔불쟁이");
                            characterManager.AddItem(1002,1);
                        }
                        else
                        {
                            logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                            logicManager.ForceChangeEventCode(13013);
                            uiManager.itemAddMessage.AddMessage("잔불쟁이");
                            characterManager.AddItem(1002, 1,decay:15);
                        }

                    }
                    else
                    {                      
                        if (bookData.vegetalDict["잔불쟁이"].bisBonus1 == 0)
                        {
                            logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                            characterManager.playerGetDamage(15);
                            logicManager.ForceChangeEventCode(13012);
                            uiManager.itemAddMessage.AddMessage("잔불쟁이");
                            characterManager.AddItem(1002, 1);
                        }
                        else
                        {
                            logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                            characterManager.playerGetDamage(15);
                            logicManager.ForceChangeEventCode(13014);
                            uiManager.itemAddMessage.AddMessage("잔불쟁이");
                            characterManager.AddItem(1002, 1,decay:15);
                        }
                    }
                }

                break;
            #endregion
               #region 잔불쟁이 지식1
            case 3303:
                bookData.SetDictData("잔불쟁이", 0);
                bookData.SetDictData("잔불쟁이", 1);
                uiManager.itemAddMessage.AddMessage("잔불쟁이 지식");
                logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                break;

            #endregion
               #region 잔불쟁이 지식2
            case 3305:

                bookData.SetDictData("잔불쟁이", 0);
                bookData.SetDictData("잔불쟁이", 2);
                uiManager.itemAddMessage.AddMessage("잔불쟁이 지식");
                logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                    
                break;
            #endregion



            #endregion
            #region 바른하늘열매
            #region 열매 줍기
            case 1302:
                bisOption = true;
                bookData.SetDictData("바른하늘열매", 0);

                if (bookData.vegetalDict["바른하늘열매"].bisBonus1 == 0)
                {
                    Current_EventCode = 513020;
                    logicManager.ButtonActive(0, "관찰", "열매를 관찰한다", false);
                }
                else
                {
                    Current_EventCode = 513021;
                    logicManager.ButtonActive(0, "지식", "지식을 활용한다.", false);
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
                    logicManager.ButtonActive(0, "관찰", "더 자세히 살펴본다", false, option: 0);
                }
                else if(a == 1)
                {
                    Current_EventCode = 513023;
                    logicManager.ForceChangeEventCode(13022);
                    logicManager.ButtonActive(0, "관찰", "더 자세히 살펴본다", false, option: 0);
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
                    logicManager.ButtonActive(0, "먹지 않는다", "이건 위험해보여.", false, option: 0);
                    logicManager.ButtonActive(1, "먹는다", "딱 봐도 괜찮아보이네.", false, option: 0);
                }
                else if (a == 1)
                {
                    Current_EventCode = 613021;
                    logicManager.ForceChangeEventCode(13024);
                    logicManager.ButtonActive(0, "먹지 않는다", "이건 위험해보여.", false, option: 0);
                    logicManager.ButtonActive(1, "먹는다", "딱 봐도 괜찮아보이네.", false, option: 0);
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
                    logicManager.ButtonActive(0, "먹지 않는다", "이건 위험해보여.", false, option: 0);
                    logicManager.ButtonActive(1, "먹는다", "딱 봐도 괜찮아보이네.", false, option: 0);
                }
                else if (a == 1)
                {
                    Current_EventCode = 613023;
                    logicManager.ForceChangeEventCode(13026);
                    logicManager.ButtonActive(0, "먹지 않는다", "이건 위험해보여.", false, option: 0);
                    logicManager.ButtonActive(1, "먹는다", "딱 봐도 괜찮아보이네.", false, option: 0);
                }
                break;

            case 613020:
                bisOption = false;
                if(option == 0)
                {
                    logicManager.ForceChangeEventCode(15021);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false, option: 0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(15022);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false, option: 0);
                }
                break;
            case 613021:
                bisOption = false;
                if (option == 0)
                {
                    logicManager.ForceChangeEventCode(15021);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false, option: 0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(15023);
                    characterManager.playerGetDamage(-5);
                    characterManager.playerUseMana(-5);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false, option: 0);
                }
                break;
            case 613022:
                bisOption = false;
                if (option == 0)
                {
                    logicManager.ForceChangeEventCode(15021);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false, option: 0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(15024);
                    characterManager.playerUseMana(5);
                    // uiManager.itemAddMessage.AddMessage("바른하늘열매");
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false, option: 0);
                }
                break;
            case 613023:
                bisOption = false;
                if (option == 0)
                {
                    logicManager.ForceChangeEventCode(15021);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false, option: 0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(15025);
                    characterManager.playerGetDamage(-10);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false, option: 0);
                }
                break;


            case 513021:
                bisOption = false;
                logicManager.ForceChangeEventCode(13027);
                uiManager.itemAddMessage.AddMessage("바른하늘열매");
                characterManager.AddItem(1601, 1);
                logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                break;
            #endregion
               #region 바른하늘열매 지식1
            case 3302:
                /*
                if(bookData.vegetalDict["바른하늘열매"].bisFirst == 0)
                {
                    bisOption = true;
                    bookData.SetDictData("바른하늘열매",0);
                    logicManager.ForceChangeEventCode(1302);                              
                    Current_EventCode = 513020;
                    logicManager.ButtonActive(0, "관찰", "열매를 관찰한다", false);                    
                }
                else
                {
                    if (bookData.vegetalDict["바른하늘열매"].bisBonus1 == 1)
                    {
                        bookData.SetDictData("바른하늘열매",1);
                        uiManager.itemAddMessage.AddMessage("바른하늘열매 지식#바른 하늘 열매");                       
                        logicManager.ForceChangeEventCode(12012);


                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                    }
                    else
                    {
                        bisOption = true;
                      
                            Current_EventCode = 513021;
                            logicManager.ButtonActive(0, "지식", "지식을 활용한다.", false);
                        
                    }
                }
                */
                bookData.SetDictData("바른하늘열매", 0);
                bookData.SetDictData("바른하늘열매", 1);
                uiManager.itemAddMessage.AddMessage("바른하늘열매 지식#바른 하늘 열매");
            


                logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                break;

            #endregion
            #endregion
            #region 늪주머니쥐
            case 2101:
                bookData.SetDictData("늪주머니쥐", 0);
                characterManager.playerGetDamage(8);
                logicManager.ButtonActive(0,"떠난다","갈길을 간다.",false);
                break;
            case 3101:
                
                if (bookData.vegetalDict["늪주머니쥐"].bisFirst == 0)
                {
                    bookData.SetDictData("늪주머니쥐",0);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                }
                else
                {
                    bisOption = true;
                    Current_EventCode = 531010;
                  
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                    if (bookData.vegetalDict["늪주머니쥐"].bisBonus1 == 1)
                        logicManager.ButtonActive(1, "유인", 1002, "잔불쟁이", "로 유인한다.");

                }

                break;
            case 3204:
                /*
                if (bookData.vegetalDict["늪주머니쥐"].bisFirst == 0)
                {
                    logicManager.ForceChangeEventCode(3101);
                    bookData.SetDictData("늪주머니쥐", 0);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                }
                else
                {
                    if (bookData.vegetalDict["늪주머니쥐"].bisBonus1 == 0)
                    {
                        bookData.SetDictData("늪주머니쥐", 1);
                        uiManager.itemAddMessage.AddMessage("+늪주머니 쥐 지식");
                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 531010;
                        logicManager.ForceChangeEventCode(3101);
                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                        logicManager.ButtonActive(1, "유인", 1002, "잔불쟁이", "로 유인한다.");
                    }
                }*/
                bookData.SetDictData("늪주머니쥐", 1);
                uiManager.itemAddMessage.AddMessage("+늪주머니 쥐 지식");
                logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                break;

            case 3205:
                /*
                if (bookData.vegetalDict["늪주머니쥐"].bisFirst == 0)
                {
                    logicManager.ForceChangeEventCode(3101);
                    bookData.SetDictData("늪주머니쥐", 0);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                }
                else
                {
                    if (bookData.vegetalDict["늪주머니쥐"].bisBonus2 == 0)
                    {
                        bookData.SetDictData("늪주머니쥐", 2);
                        uiManager.itemAddMessage.AddMessage("늪주머니 쥐 지식");
                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 531010;
                        logicManager.ForceChangeEventCode(3101);
                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                        logicManager.ButtonActive(1, "유인", 1002, "잔불쟁이", "로 유인한다.");
                    }
                  
                }
                */
                bookData.SetDictData("늪주머니쥐", 2);
                uiManager.itemAddMessage.AddMessage("늪주머니 쥐 지식");
                logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                break;

            case 531010:
                bisOption = false;
                if(option == 0)
                {
                    logicManager.ForceChangeEventCode(31012);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                }

                else if(option == 1)
                {
                    logicManager.ForceChangeEventCode(31013);
                    uiManager.itemAddMessage.AddMessage("침샘");
                    characterManager.AddItem(1003, 1);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                }
                break;




            #endregion
            #region 별고리찌
               #region 별고리찌 폭파
            case 2102:
                bookData.SetDictData("별고리찌", 0);
                characterManager.playerGetDamage(10);
                logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                break;
            #endregion
               #region 별고리찌 부착
            case 2103:
                bisOption = true;
                Current_EventCode = 521030;
                bookData.SetDictData("별고리찌", 0);
                logicManager.ButtonActive(1, 2, "strength", "떼어내기", "열매를 떼어낸다.", option: 0);
                if (bookData.vegetalDict["별고리찌"].bisBonus1 == 1) characterManager.EventBonus(bookData.vegetalDict["별고리찌"].bonus1,"strength",1);

                break;

            case 521030:
                bisOption = false;
                if (testResult >=2)
                {
                    logicManager.ForceChangeEventCode(21031);
                    logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                }
                else
                {
                    logicManager.ForceChangeEventCode(21032);
                    characterManager.AddDisease(1501);
                    logicManager.ButtonActive(0, "불안", "이건 또 뭐람", false);
                }
                break;
            #endregion
               #region 별고리찌 지식1
            case 3203:
                /*
                if(bookData.vegetalDict["별고리찌"].bisFirst == 0)
                {
                    bookData.SetDictData("별고리찌", 0);
                    logicManager.ForceChangeEventCode(2103);
                    bisOption = true;
                    Current_EventCode = 521030;
                    logicManager.ButtonActive(1, 2, "strength", "떼어내기", "열매를 떼어낸다.", option: 0);
                }
                else
                {
                    if (bookData.vegetalDict["별고리찌"].bisBonus1 == 0)
                    {
                        uiManager.itemAddMessage.AddMessage("별고리찌 지식");
                        bookData.SetDictData("별고리찌", 1);
                        logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);                         
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 521030;
                        logicManager.ButtonActive(1, 2, "strength", "떼어내기", "열매를 떼어낸다.", option: 0);
                        characterManager.EventBonus(bookData.vegetalDict["별고리찌"].bonus1, "strength", 1);
                    }

                }
                */
                uiManager.itemAddMessage.AddMessage("별고리찌 지식");
                bookData.SetDictData("별고리찌", 0);
                bookData.SetDictData("별고리찌", 1);               
                logicManager.ButtonActive(0, "떠난다", "갈길을 간다.", false);
                break;
            #endregion
            #endregion
            #region 뱀비늘 뿌리 
               #region 길에서 베임
            case 2201:
                characterManager.playerGetDamage(15);
                bookData.SetDictData("뱀비늘뿌리", 0);
                logicManager.ButtonActive(0, "상처", "상처를 감싸고 떠난다", false);
                break;
            #endregion
               #region 제대로 베임
            case 2302:
                bisOption = true;
                Current_EventCode = 523020;
                bookData.SetDictData("뱀비늘뿌리", 0);
                logicManager.ButtonActive(0, "도망친다", "전력으로 도망친다.", false);
                break;
            case 523020:
                if(characterManager.checkDisease(1501))
                {
                   
                    Debug.Log("ㅗ");
                    Current_EventCode = 623020;
                    logicManager.ForceChangeEventCode(23021);
                    logicManager.ButtonActive(0, "보호", "머리를 감싼다.", false);
                    logicManager.ButtonActive(1, "잔불쟁이", 1002, "잔불쟁이", "로 지진다.");
                    if(bookData.vegetalDict["뱀비늘 뿌리"].bisBonus1 == 1) logicManager.ButtonActive(2, "도포보 열매", 1201, "도포보 열매", "를 던진다.");
                }
                else
                {                    
                    bisOption = false;
                    logicManager.ForceChangeEventCode(23022);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false, option: 0);
                }
                break;
            case 623020:
                bisOption = false;
                if (option == 0)
                {
                
                    logicManager.ForceChangeEventCode(23023);
                    characterManager.playerGetDamage(30);
                    logicManager.ButtonActive(0, "상처", "상처를 감싼다.", false, option: 0);
                }
               else if(option == 1)
                {
                   
                    logicManager.ForceChangeEventCode(23024);
                    uiManager.itemAddMessage.AddMessage("뱀비늘뿌리");
                    characterManager.AddItem(2101, 1);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false, option: 0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(23025);
                    characterManager.RemoveItem(1201,1);
                    uiManager.itemAddMessage.AddMessage("도포보 열매",false);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false);
                }
                break;
            #endregion
               #region 뱀비늘뿌리
            case 3301:
                /*
                if (bookData.vegetalDict["뱀비늘뿌리"].bisFirst == 0)
                {
                    bookData.SetDictData("뱀비늘뿌리",0);
                    logicManager.ForceChangeEventCode(2302);
                    bisOption = true;
                    Current_EventCode = 523020;
                    logicManager.ButtonActive(0, "도망친다", "전력으로 도망친다.", false);
                }
                else
                {
                    if (bookData.vegetalDict["뱀비늘뿌리"].bisBonus1 == 0)
                    {
                        bookData.SetDictData("뱀비늘뿌리",1);
                        uiManager.itemAddMessage.AddMessage("뱀비늘뿌리");
                        characterManager.AddItem(2101, 1);
                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                    }
                    else
                    {
                        logicManager.ForceChangeEventCode(2302);
                        bisOption = true;
                        Current_EventCode = 523020;
                        logicManager.ButtonActive(0, "도망친다", "전력으로 도망친다.", false);
                    }
                }
                */
                bookData.SetDictData("뱀비늘뿌리", 1);
                bookData.SetDictData("뱀비늘뿌리", 0);
                uiManager.itemAddMessage.AddMessage("뱀비늘뿌리 지식");
                logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                break;                                              
            #endregion


            #endregion
            #region 풀발찌벌레
            #region 벌레한테 물림
            case 2202:
                bisOption = true;
                Current_EventCode = 522020;
                bookData.SetDictData("풀발찌 벌레", 0);
                logicManager.ButtonActive(0, 1, "examine", "떼어내기", "열매를 떼어낸다.");
                if (bookData.vegetalDict["풀발찌 벌레"].bisBonus1 == 1)
                characterManager.EventBonus(bookData.vegetalDict["풀발찌 벌레"].bonus1,"examine",1);
                if (bookData.vegetalDict["도포보 열매"].bisBonus2 == 1)
                logicManager.ButtonActive(1, "살충", 1201, "도포보 열매", "열매즙을 뿌린다.");
                break;

            case 522020:
                bisOption = false; 
                if (option == 0)
                {
                    if (testResult >= 1)
                    {
                        logicManager.ForceChangeEventCode(22021);
                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                    }
                    else
                    {
                        logicManager.ForceChangeEventCode(22022);
                        characterManager.AddDisease(1201);
                        logicManager.ButtonActive(0, "고통", "몸이 무겁게 느껴진다", false);
                    }
                }
                else
                {
                    uiManager.itemAddMessage.AddMessage("도포보 열매",false);
                    characterManager.RemoveItem(1201,1);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                    logicManager.ForceChangeEventCode(22023);
                }

                break;
            #endregion
               #region 풀발찌벌레 지식1
            case 3201:
                logicManager.ForceChangeEventCode(32011);
                bookData.SetDictData("풀발찌 벌레", 1);
                uiManager.itemAddMessage.AddMessage("풀발찌벌레 지식");
                logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                /*
                if (bookData.vegetalDict["풀발찌 벌레"].bisFirst == 0)
                {
                    bisOption = true;
                    Current_EventCode = 522020;
                    logicManager.ButtonActive(0, 1, "examine", "떼어내기", "열매를 떼어낸다.", option: 0);
                }
                else
                {
                    if (bookData.vegetalDict["풀발찌 벌레"].bisBonus1 == 0)
                    {
                        logicManager.ForceChangeEventCode(32011);
                        bookData.SetDictData("풀발찌 벌레",1);
                        uiManager.itemAddMessage.AddMessage("풀발찌벌레 지식");
                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                    }
                    else
                    {
                        bisOption = true;
                        Current_EventCode = 522020;                      
                        logicManager.ButtonActive(0, 1, "examine", "떼어내기", "열매를 떼어낸다.", option: 0);
                        characterManager.EventBonus(bookData.vegetalDict["풀발찌 벌레"].bonus1, "examine", 1);
                        if (bookData.vegetalDict["도포보 열매"].bisBonus2 == 1)
                            logicManager.ButtonActive(1, "살충", 1201, "도포보 열매", "열매즙을 뿌린다.", option: 0);
                    }
                }
                */
                break;
            #endregion
            #endregion
            #region 굉발바닥버섯
               #region 버섯 채집
            case 2203:
                bisOption = true;
                Current_EventCode = 522030;
                bookData.SetDictData("굉발바닥버섯", 0);
                logicManager.ButtonActive(0, "포기", "건들이지 않는다.", false);
                logicManager.ButtonActive(1, "따기", "위험을 감수하고\n버섯을 딴다.", false);
                if (bookData.vegetalDict["범혓바늘버섯"].bisBonus1 == 1)
                {
                    logicManager.ButtonActive(2, "가열", 1002, "잔불쟁이", "로 가열해\n독이 있는지 확인");
                }
                break;

            case 522030:               
                if (option == 0)
                {
                    bisOption = false;
                    Current_EventCode = 12031;
                    logicManager.ForceChangeEventCode(12031);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false);

                }

                else if (option == 1)
                {
                    bisOption = false;
                    Current_EventCode = 22031;
                    logicManager.ForceChangeEventCode(22031);
                    characterManager.AddDisease(1101);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false);
                }
                else
                {
                    Current_EventCode = 622030;
                    logicManager.ForceChangeEventCode(12032);
                    if (bookData.vegetalDict["수근쟁이 풀"].bisBonus2 == 0)
                    {
                      
                        logicManager.ForceChangeEventCode(22032);
                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false);
                    }

                    if (bookData.vegetalDict["수근쟁이 풀"].bisBonus2 == 1)
                    {
                        Current_EventCode = 622030;
                        logicManager.ForceChangeEventCode(22033);
                        logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false);
                        logicManager.ButtonActive(1, "수근쟁이 풀", 1101, "수근쟁이 풀", "로 독을\n긁어내 제거한다.");
                    }
                }
                break;
            case 622030:
                bisOption = false;
                if (option == 0)
                {
                    logicManager.ForceChangeEventCode(22034);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false);
                
                }
                else
                {
                    logicManager.ForceChangeEventCode(22035);
                    uiManager.itemAddMessage.AddMessage("굉발바닥버섯");
                    characterManager.AddItem(2501, 1);
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다", false);
                }
                break;

            case 2301:
                logicManager.ButtonActive(0, "임시", "임시로 만듬", false);
                characterManager.AddDisease(1101);
                break;
            #endregion
               #region 굉발바닥버섯 지식 1
            case 3202:
                bisOption = true;
                Current_EventCode = 532020;
                logicManager.ButtonActive(0, "글쎄...", "방법이 없다.", false);
                logicManager.ButtonActive(1, "가열", 1002, "잔불쟁이", "로 가열해 본다");
                /*
                if(bookData.vegetalDict["굉발바닥버섯"].bisFirst == 0)
                {
                    bisOption = true;
                    logicManager.ForceChangeEventCode(2203);
                    Current_EventCode = 522030;
                    bookData.SetDictData("굉발바닥버섯",0);
                    logicManager.ButtonActive(0, "포기", "건들이지 않는다.", false);
                    logicManager.ButtonActive(1, "따기", "위험을 감수하고\n버섯을 딴다.", false);
                }
                else
                {
                    if(bookData.vegetalDict["굉발바닥버섯"].bisBonus1 == 0)
                    {
                        bisOption = true;
                        Current_EventCode = 532020;
                        logicManager.ButtonActive(0, "글쎄...", "방법이 없다.", false);
                        logicManager.ButtonActive(1, "가열", 1002, "잔불쟁이", "로 가열해 본다");                      
                    }
                    else
                    {
                        logicManager.ForceChangeEventCode(2203);
                       
                        Current_EventCode = 522030;
                        logicManager.ButtonActive(0, "포기", "건들이지 않는다.", false);
                        logicManager.ButtonActive(1, "따기", "위험을 감수하고\n버섯을 딴다.", false);
                        if (bookData.vegetalDict["범혓바늘버섯"].bisBonus1 == 1)
                        {
                            logicManager.ButtonActive(2, "가열", 1002, "잔불쟁이", "로 가열해\n독이 있는지 확인");
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
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                }

                else
                {
                    logicManager.ForceChangeEventCode(32022);
                    bookData.SetDictData("굉발바닥버섯", 1);
                    uiManager.itemAddMessage.AddMessage("굉발바닥버섯 지식");
                    logicManager.ButtonActive(0, "떠난다", "갈 길을 간다.", false);
                }
                break;


            #endregion
            #endregion

            case 1304:
                uiManager.itemAddMessage.AddMessage("쇠드릅");
                characterManager.AddItem(1302,1);
                logicManager.ButtonActive(0, "떠난다.", "갈 길을 간다.", false);
                break;

            case 1303:

                bisOption = true;
                logicManager.ForceChangeEventCode(1302);
                bookData.SetDictData("바른하늘열매", 0);

                if (bookData.vegetalDict["바른하늘열매"].bisBonus1 == 0)
                {
                    Current_EventCode = 513020;
                    logicManager.ButtonActive(0, "관찰", "열매를 관찰한다", false);
                }
                else
                {
                    Current_EventCode = 513021;
                    logicManager.ButtonActive(0, "지식", "지식을 활용한다.", false);
                }
              
                break;


            case 1305:
                bisOption = true;
                logicManager.ForceChangeEventCode(1202);
                bookData.SetDictData("버마꽃", 0);
                Current_EventCode = 512020;
                logicManager.ButtonActive(0, "떠난다.", "딱봐도 위험해 보이네.", false);
                logicManager.ButtonActive(1, "섭취", "한 입 정도는\n맛봐도 되지 않을까.", false);

                break;

            case 2303:
                bisOption = true;
                Current_EventCode = 523030;
                logicManager.ButtonActive(0, 2, "examine", "관찰", "뭔가 이상한데?", option: 1);
                break;
            case 523030:
                bisOption = false;
                if (testResult >= 2)
                {
                    logicManager.ForceChangeEventCode(23032);
                    logicManager.ButtonActive(0, "휴", "정말 위험할 뻔 했네", false,option:0);
                }
                else
                {
                    logicManager.ForceChangeEventCode(23031);
                    characterManager.playerGetDamage(20);
                    logicManager.ButtonActive(0, "함정", "상처를 감싼다.", false, option: 0);
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
    //필수 eventtable 0,3,9
    

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
                logicManager.ButtonActive(0,"1번","1번 빠따 잘 돌아갑니까???",true);
                logicManager.ButtonActive(1, 2, "strength", "힘", "힘 테스트");

                
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
                    logicManager.ButtonActive(0, "1번", "1번 빠따 잘 돌아갑니까???", true,option :0);
                }
                else if(option == 1)
                {
                    if(testResult >= 1)
                    {
                        Current_EventCode = 12;
                        logicManager.ForceChangeEventCode(12);
                        logicManager.ButtonActive(0, "성공", "성공!", true, option: 0);
                    }
                    else
                    {
                        Current_EventCode = 13;
                        logicManager.ForceChangeEventCode(13);
                        logicManager.ButtonActive(0, "실패", "실패!", true, option: 0);
                    }

                }


                break;


            case 2:
                characterManager.AddDisease(1101);
                logicManager.ButtonActive(0,"2번", "2번 빠따 잘 돌아갑니까???", true);
                logicManager.ButtonActive(1,"ㅇ", "그래", true);
               
                


                break;
            case 3:

                logicManager.SetBattle("허수아비");
                
                break;

            case 30:
                bisOption = false;
                if (option == 0) {
                     Current_EventCode = 31;
                    logicManager.ForceChangeEventCode(31);
                    logicManager.ButtonActive(0, "1개 줄게","와!", true,option : 0);
                    characterManager.AddItem(1121, 1);
                }
                else if (option == 1) {
                     Current_EventCode = 32;
                    logicManager.ForceChangeEventCode(32);
                    logicManager.ButtonActive(0, "2개 줄게","와!!", true, option: 0);
                    characterManager.AddItem(1121, 2);
                }
                else if (option == 2) {
                     Current_EventCode = 33;
                    logicManager.ForceChangeEventCode(33);
                    characterManager.AddDisease(1101);
                    logicManager.ButtonActive(0, "3개 줄게","와!!!", true, option: 0);
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
    //필수 eventtable 0,3,9

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
                //logicManager.SetBattle("바리잡이");
                logicManager.ButtonActive(0,"탐색","안에 무엇이 있는지\n확인한다.",false);
                //characterManager.AddItem(1401, 3);
                break;

            case 2:
                logicManager.ButtonActive(0, "탐색", "더 둘러본다", false , option : 5);
                characterManager.AddItem(1111,1);
                characterManager.AddItem(1121,1);
                characterManager.AddItem(1131,1);

                characterManager.AddItem(1101,1);
                characterManager.AddItem(1301,1);
                characterManager.AddItem(2101,1);

                uiManager.itemAddMessage.AddMessage("바르바신#모로닐#헤르민");
                break;

            case 3:
                bisOption = true;
                logicManager.ButtonActive(0, "창문 밖의", "저게... 뭐지?", false);
                Current_EventCode = 30;
                      
                break;

            case 30:
                logicManager.ButtonActive(0, "확인", "유리 창을 확인한다.", false , option: 0);
                Current_EventCode = 31;
                logicManager.ForceChangeEventCode(31);
                break;

            case 31:
                logicManager.ButtonActive(0, "위기", "필사적으로 문을 막는다.", false , option: 0);
                Current_EventCode = 32;
                logicManager.ForceChangeEventCode(32);
                break;

            case 32:
                logicManager.ButtonActive(0, "본능", "주사를 몸에 꽂는다", false , option: 0);
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
                logicManager.ButtonActive(0, "대항", "더는 물러서지 않는다.", false, option: 1);
                break;

            case 4:
                logicManager.SetBattle("바리잡이");
                break;



        }
    }

}





public class EventManager : MonoBehaviour,IManager
{
    public GameManager gameManager { get { return GameManager.gameManager; } }
    //public EventData eventData;

    public BookData bookData;

    // 현재 어떤 구역에, 어떤 이벤트안에 들어왔는가?
    public EventBase CurrentEvent { get; set; }

    // 이벤트를 다 여기다 처박고 보관
    
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
