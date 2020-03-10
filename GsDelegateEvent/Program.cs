using GsDelegateEvent.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsDelegateEvent
{
    class Program
    {
        static void Main(string[] args)
        {
			try
			{
				Console.WriteLine("================委托========================");

                #region 委托
                { 
                    MyDelegate myDelegate = new MyDelegate();
                    //myDelegate.Show();
                }
                #endregion

                #region 委托解耦，代码复用
                {
                    Console.WriteLine("==========委托解耦，代码复用==========");
                    Student student = new Student()
                    {
                        Id = 123,
                        Name = "Rabbit",
                        Age = 23,
                        ClassId = 1
                    };
                    //上端还不是得知道是哪个国家的人？
                    student.Study();
                    student.SayHi("大脸猫", PeopleType.Chinese);
                    student.SayHi("icefoxz", PeopleType.Malaysia);
                    student.SayHiChinese("大脸猫");
                    {
                        Action<string> method = student.SayHiChinese;
                        student.SayHiPerfact("大脸猫", method);
                    }
                    {
                        Action<string> method = student.SayHiAmerican;
                        student.SayHiPerfact("PHS", method);
                    }
                    {
                        Action<string> method = student.SayHiMalaysia;
                        student.SayHiPerfact("icefoxz", method);
                    }
                }
                #endregion


                #region Action Func
                {
                    Console.WriteLine("****************************Event*************************");
                    Cat cat = new Cat();
                    cat.Miao();

                    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    cat.CatMiaoAction += new Dog().Wang;
                    cat.CatMiaoAction += new Mouse().Run;
                    cat.CatMiaoAction += new Baby().Cry;
                    cat.CatMiaoAction += new Mother().Wispher;

                    cat.CatMiaoAction.Invoke();
                    cat.CatMiaoAction = null;

                    cat.CatMiaoAction += new Brother().Turn;
                    cat.CatMiaoAction += new Father().Roar;
                    cat.CatMiaoAction += new Neighbor().Awake;
                    cat.CatMiaoAction += new Stealer().Hide;
                    cat.MiaoDelegate();
                    //去除依赖，Cat稳定了；还可以有多个Cat实例


                    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    cat.CatMiaoActionHandler += new Dog().Wang;
                    cat.CatMiaoActionHandler += new Mouse().Run;
                    cat.CatMiaoActionHandler += new Baby().Cry;

                    //cat.CatMiaoActionHandler.Invoke();
                    //cat.CatMiaoActionHandler = null;

                    cat.CatMiaoActionHandler += new Mother().Wispher;
                    cat.CatMiaoActionHandler += new Brother().Turn;
                    cat.CatMiaoActionHandler += new Father().Roar;
                    cat.CatMiaoActionHandler += new Neighbor().Awake;
                    cat.CatMiaoActionHandler += new Stealer().Hide;
                    cat.MiaoEvent();


                    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    cat.AddObserver(new Dog());
                    cat.AddObserver(new Mouse());
                    cat.AddObserver(new Baby());
                    cat.AddObserver(new Mother());
                    cat.AddObserver(new Brother());
                    cat.AddObserver(new Father());
                    cat.AddObserver(new Neighbor());
                    cat.AddObserver(new Stealer());
                    cat.MiaoObserver();
                }
                #endregion
                {
                    //EventStandard.Show();
                }
            }
            catch (Exception Ex)
			{
				Console.WriteLine("错误信息："+Ex.Message);
			}
            Console.WriteLine("结束");
            Console.ReadLine();
        }
    }
}
