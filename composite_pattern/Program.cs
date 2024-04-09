using System;
using System.Collections.Generic;

namespace composite_pattern
{
    //將物件(objects)組合(compose)成樹狀結構
    //ex: 檔案系統
    //定義所說的一致性對待。只要遇到階層或是樹狀的結構都可以評估看看是否需要套用Composite pattern
    interface IEmployee
    {
        string Name { get; set; }
        string Dept { get; set; }
        string Designation { get; set; }
        void DisplayDetails();
    }
    class Employee : IEmployee
    {
        public string Name { get; set; }
        public string Dept { get; set; }
        public string Designation { get; set; }
        public void DisplayDetails()
        {
            Console.WriteLine($"\t{Name} 隸屬於 {Dept} 頭銜 : {Designation}");
        }

    }
    class CompositeEmployee : IEmployee
    {
        public string Name { get; set; }
        public string Dept { get; set; }
        public string Designation { get; set; }
        //下屬容器
        private List<IEmployee> subordinatrList = new List<IEmployee>();
        //新增員工
        public void AddEmployee(IEmployee e)
        {
            subordinatrList.Add(e);
        }
        public void RemoveEmployee(IEmployee e)
        {
            subordinatrList.Remove(e);
        }
        public void DisplayDetails()
        {
            Console.WriteLine($"\t{Name} 隸屬於 {Dept} 頭銜 : {Designation}");
            foreach (IEmployee e in subordinatrList)
            {
                e.DisplayDetails();
            }
        }
    }

    /// <summary>
    /// 呼叫AddEmployee方法來新增員工，形成樹狀結構
    /// 呼叫DisplayDetails方法來顯示員工明細(for迴圈走訪樹狀結構)
    /// 呼叫RemoveEmployee方法，移除樹狀結構中的node(CompositeEmployee)或leaf(Employee)
    /// 整個樹狀結構中CompositeEmployee作為node，Employee作為leaf，這兩個類別共同實作了IEmployee介面，達到了一致性的對待
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Composite Pattern Demo.***");
            #region 審計委員會
            Employee e1 = new Employee { Name = "John", Dept = "審計委員會", Designation = "工讀生" };
            Employee e2 = new Employee { Name = "Daniel", Dept = "審計委員會", Designation = "組長" };

            //審計委員會主管
            CompositeEmployee manager1 = new CompositeEmployee { Name = "Peter", Dept = "審計委員會", Designation = "主管" };

            //主管Peter旗下新增2名員工
            manager1.AddEmployee(e1);
            manager1.AddEmployee(e2);
            #endregion

            #region 薪酬委員會
            Employee e3 = new Employee { Name = "Maggie", Dept = "薪酬委員會", Designation = "工讀生" };
            Employee e4 = new Employee { Name = "Amy", Dept = "薪酬委員會", Designation = "工讀生" };
            Employee e5 = new Employee { Name = "Allen", Dept = "薪酬委員會", Designation = "組長" };

            CompositeEmployee manager2 = new CompositeEmployee { Name = "Alex", Dept = "薪酬委員會", Designation = "主管" };

            //主管Alex旗下新增3名員工
            manager1.AddEmployee(e3);
            manager1.AddEmployee(e4);
            manager1.AddEmployee(e5);
            #endregion

            #region 管理層頂端
            CompositeEmployee CEO = new CompositeEmployee { Name = "Tom", Dept = "董事會", Designation = "執行長" };
            //主管Alex旗下新增3名員工
            CEO.AddEmployee(manager1);
            CEO.AddEmployee(manager2);
            #endregion


            Console.WriteLine("\n列出完整組織員工明細:");
            CEO.DisplayDetails();

            //移除CEO旗下的員工Peter
            CEO.RemoveEmployee(manager1);

            Console.WriteLine("\n-----------------------\n");
            Console.WriteLine("\n移除Peter員工後的員工明細");
            CEO.DisplayDetails();

            Console.WriteLine("\n-----------------------------------------\n");
            Console.WriteLine("\n只列出主管Alex旗下員工明細:");
            manager2.DisplayDetails();

            Console.WriteLine("\n-----------------------------------------\n");
            Console.WriteLine("\n只列出員工John的員工明細:");
            e1.DisplayDetails();

            Console.ReadKey();
        }
    }
}
