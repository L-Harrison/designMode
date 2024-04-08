using System.Drawing;

namespace InterpreterPattern;

//源代码==》编译器==》解释器==》机器码
//源代码根据语言的句法结构被编译器编译成对应的解释器语义树对象文件，然后调用解释器的根表达方法，将语义树解释成计算机可以执行的机器码。

//终极表达式：不可以被分割的语句表达式
//非终极表达式：由终极表达式组成的语句表达式
public class Interpreter
{
    public void DO()
    {
        #region 脚本(源代码)
        /*
         * BEGIN               //脚本开始
         * MOVE 500,600;       //鼠标指针移动到目标位置(500,600)
         *  BEGIN LOOP 5       //开始循环5次
         *    LEFT_CLICK;      //循环体内单击左键
         *    DELAY 1;         //每次延迟1秒
         *  END;               //循环体结束
         * RIGHT_DOWN;         //按下右键
         * DELAY 7200;         //延迟2小时
         * END;                //脚本结束
         * **/
        #endregion

        #region 分析并生成语义表达式树（将脚本翻译成语义表达式树,并生成对象存储到文件中,这个过程由编译器完成）

        #region 语义树分析
        //          ======》鼠标移动表达式
        //                   （Move）
        //                                                                ====》左键按下表达式
        //                                     ======》鼠标单击表达式            （LeftKeyDown）
        //表达式序列 ======》循环表达式                （LeftKeyClick）     ====》左键松开表达式
        //(Sequence)      (RepetExpression)                                     （LeftKeyUp）
        //                                     ======》系统延迟表达式            
        //                                            (Delay)
        //          ======》右键按下表达式
        //                (RightKeyDown)

        #endregion

        #region 生成语义表达式树对象
        //构造指令集语义树，实际情况这个构造过程会由编译器完成(Evaluator or Parser)
        Sequence sequence = new Sequence(new List<Expression>() {
                new Move(new Point(500,600)),
                new RepetExpression(new Sequence(new List<Expression>(){
                    new LeftKeyClick(),
                    new Delay(1)
                }),5),
                new RightKeyDown(),
                new Delay(7200)
            });
        #endregion

        #endregion

        #region 调用解释器的解释接口
        sequence.Interpret();
        #endregion

    }
}

#region 解释器
public interface Expression
{
    /// <summary>
    /// 解释
    /// </summary>
    void Interpret();
}

#region 终极表达式对象
public class Move : Expression
{
    Point MousePos;
    public Move(Point mousePos)
    {
        MousePos = mousePos;
    }

    public void Interpret()
    {
        Console.WriteLine("发送机器指令：移动鼠标到(" + MousePos.X + "," + MousePos.Y + ")");
    }
}

public class LeftKeyDown : Expression
{
    public void Interpret()
    {
        Console.WriteLine("发送机器指令:按下鼠标左键");
    }
}

public class LeftKeyUp : Expression
{
    public void Interpret()
    {
        Console.WriteLine("发送机器指令:松开鼠标左键");
    }
}
public class RightKeyDown : Expression
{
    public void Interpret()
    {
        Console.WriteLine("发送机器指令:按下鼠标右键");
    }
}

public class RightKeyUp : Expression
{
    public void Interpret()
    {
        Console.WriteLine("发送机器指令:松开鼠标右键");
    }
}

public class Delay : Expression
{
    private int seconds;
    public Delay(int seconds)
    {
        this.seconds = seconds;
    }
    public void Interpret()
    {
        Console.WriteLine("发送机器指令:延迟" + this.seconds.ToString() + "秒");
    }
}
#endregion

#region 非终极表达式对象
public class LeftKeyClick : Expression
{
    Expression leftKeyDown;
    Expression leftKeyUp;
    public LeftKeyClick()
    {
        this.leftKeyUp = new LeftKeyUp();
        this.leftKeyDown = new LeftKeyDown();
    }
    public void Interpret()
    {
        this.leftKeyDown.Interpret();
        this.leftKeyUp.Interpret();
    }
}
public class RepetExpression : Expression
{
    private Expression loopBodyExpression;
    private int loopCount;
    public RepetExpression(Expression loopBodyExpression, int loopCount)
    {
        this.loopBodyExpression = loopBodyExpression;
        this.loopCount = loopCount;
    }

    public void Interpret()
    {
        while (loopCount > 0)
        {
            loopBodyExpression.Interpret();
            loopCount--;
        }
    }
}

public class Sequence : Expression
{
    private List<Expression> expressions;
    public Sequence(List<Expression> expressions)
    {
        this.expressions = expressions;
    }

    public void Interpret()
    {
        foreach (var item in expressions)
        {
            item.Interpret();
        }
    }
}
#endregion
#endregion