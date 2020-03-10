using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GsDelegateEvent
{

    public delegate void NoReturnNoParaOutClass();
    public delegate void Action<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, in T17>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17);
    public class MyDelegate
    {
        /// <summary>
        /// 1 委托在IL就是一个类
        /// 2 继承自System.MulticastDelegate 特殊类-不能被继承
        /// </summary>
        public delegate void NoReturnNoPara(); //声明委托
        public delegate void NoReturnWithPara(int x, int y);
        public delegate int WithReturnNoPara();
        public delegate int WithReturnPara(int x);
        public delegate MyDelegate WithReturnWithPara(out int x, ref int y);

        public void Show()
        {
  
            Student student = new Student()
            {
                Id = 96,
                Name = "小目标一个亿"
            };
            student.Study();

            #region 委托
            {
                //给委托赋值（参数和返回值要和委托保持一致）
                NoReturnNoPara method = new NoReturnNoPara(this.DoNothing);
                Console.WriteLine("==========委托调用==========");
                method.Invoke();
                Console.WriteLine("==========普通调用==========");
                this.DoNothing();
                Console.WriteLine("==========可以省略.Invoke==========");
                method();

                Console.WriteLine("==========委托的BeginInvoke和EndInvoke方法==========");
                WithReturnPara withReturnParaMethod = new WithReturnPara(newTask);
                //BeginInvoke方法可以使用线程异步地执行委托所指向的方法。然后通过EndInvoke方法获得方法的返回值（EndInvoke方法的返回值就是被调用方法的返回值），或是确定方法已经被成功调用。我们可以通过四种方法从EndInvoke方法来获得返回值。
                IAsyncResult asyncResult = withReturnParaMethod.BeginInvoke(2000, null, null);
                int result = withReturnParaMethod.EndInvoke(asyncResult);
                Console.WriteLine(result);
                //在运行上面的程序后，由于newTask方法通过Sleep延迟了2秒，因此，程序直到2秒后才输出最终结果（一个随机整数）。如果不调用EndInvoke方法，程序会立即退出，这是由于使用BeginInvoke创建的线程都是后台线程，这种线程一但所有的前台线程都退出后（其中主线程就是一个前台线程），不管后台线程是否执行完毕，都会结束线程，并退出程序。
            }
            #endregion

            #region 有返回值、无参的委托
            {
                Console.WriteLine("==========有返回值、无参的委托==========");
                WithReturnNoPara method = new WithReturnNoPara(this.Get);
                int i = method.Invoke();
                int k = this.Get();
                Console.WriteLine("返回值i:" + i);
                Console.WriteLine("返回值k:" + k);
                //以上委托调用和普通调用运行结果一样
            }
            #endregion

            #region 自定义委托
            {
                Console.WriteLine("==========自定义委托==========");
                //Action Func  .NetFramework3.0出现的
                //Action 系统提供  0到16个泛型参数  不带返回值  委托

                Action action0 = this.DoNothing;//是个语法糖，就是编译器帮我们添加上new Action
                Console.WriteLine("执行DoNothing方法");
                action0.Invoke();

                Action<int> action1 = this.ShowInt;
                Console.WriteLine("执行带参数ShowInt方法");
                action1.Invoke(20200310);

                //Func 系统提供  0到16个泛型参数,如果不够用可以自定义
                Action<int, string, DateTime, long, int, string, DateTime, long, int, string, DateTime, long, int, string, DateTime, long, int> action16 = null;

                //Func 系统提供  0到16个泛型参数  带泛型返回值  委托
                Func<int> func0 = this.Get;
                int iResult = func0.Invoke();
                Console.WriteLine("无参有返回值委托，返回值：" + iResult);

                Func<int, string> func1 = this.ToString;
                string str = func1.Invoke(20200310);
                Console.WriteLine("返回值：" + str);
            }
            #endregion

            #region 使用框架自带的委托
            {
                Console.WriteLine("==========使用框架自带的委托==========");
                Action action0 = this.DoNothing;
                NoReturnNoPara method = this.DoNothing;
                //为啥框架要提供这种委托呢？  框架提供这种封装，自然是希望大家都统一使用Action/Func
                this.DoAction(action0);

                //this.DoAction(method); Action和NoReturnNoPara是不同的类，虽然实例化都可以传递相同的方法，但是没有父子关系,所以会报错，就像Student和Teacher两个类，实例化都是传递id/name，但是二者不能替换的

            }
            #endregion

            #region 多播委托
            {
                Console.WriteLine("==========多播委托==========");
                //多播委托：一个委托实例包含多个方法，可以通过+=/-=去增加/移除方法，Invoke时可以按顺序执行全部动作

                //多播委托:任何一个委托都是多播委托类型的子类，可以通过+=去添加方法
                //+=  给委托的实例添加方法，会形成方法链，Invoke时，会按顺序执行系列方法
                Action method = this.DoNothing;
                method += this.DoNothing;
                method += DoNothingStatic;
                method += new Student().Study;
                method += Student.StudyAdvanced;
                //method.BeginInvoke(null, null);//启动线程来完成计算  会报错，多播委托实例不能异步
                //Console.WriteLine("==========循环执行==========");
                //foreach (Action item in method.GetInvocationList())
                //{
                //    item.Invoke();
                //    //循环可以使用BeginInvoke
                //    item.BeginInvoke(null, null);
                //}
                Console.WriteLine("==========一次性调用==========");
                method.Invoke();
                //-=  给委托的实例移除方法，从方法链的尾部开始匹配，遇到第一个完全吻合的，移除，且只移除一个，如果没有匹配，就啥事儿不发生
                method -= this.DoNothing;
                method -= DoNothingStatic;
                method -= new Student().Study;
                Console.WriteLine("==========从委托中减去几个方法==========");
                method.Invoke();
                //如果中间出现未捕获的异常，直接方法链就结束了

            }
            #endregion

            #region 带返回值的多播委托
            {
                Console.WriteLine("==========带返回值的多播委托==========");
                Func<int> func = this.Get;
                func += this.Get2;
                func += this.Get3;
                func += this.Get4;
                int iResult = func.Invoke();
                Console.WriteLine("返回结果：" + iResult);
                //带返回值的多播委托，返回值以最后一个委托为准
            }
            #endregion

        }

        private void DoAction(Action act)
        {
            Console.WriteLine("执行传进来的委托方法");
            act.Invoke();
        }


        private string ToString(int i)
        {
            Console.WriteLine("执行有参，有返回值方法");
            return i.ToString();
        }

        private void ShowInt(int i)
        {
            Console.WriteLine("有参数，无返回值，参数是："+i);
        }

        public int Get()
        {

            return 1;
        }
        public int Get2()
        {
            return 2;
        }
        public int Get3()
        {
            return 3;
        }
        public int Get4()
        {
            return 4;
        }
        private MyDelegate ParaReturn(out int x, ref int y)
        {
            throw new Exception();
        }


        private void DoNothing()
        {
            Console.WriteLine("什么也不做");
        }
        private static void DoNothingStatic()
        {
            Console.WriteLine("什么都不要动");
        }

        private int newTask(int ms)
        {
            Console.WriteLine("任务开始");
            Thread.Sleep(ms);
            Random random = new Random();
            int n = random.Next(10000);
            Console.WriteLine("任务完成");
            return n;

        }

    }

    public class OtherClass
    {
        public void DoNothing()
        {
            Console.WriteLine("什么也不做");
        }
        public static void DoNothingStatic()
        {
            Console.WriteLine("什么都不要动");
        }
    }
}
