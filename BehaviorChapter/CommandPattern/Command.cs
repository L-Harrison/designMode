namespace CommandPattern;

public class Command
{
    public void DO()
    {
        //接收方
        Bulb bulb = new Bulb();
        TV tV = new TV();
        //发送方
        Switcher switcher = new Switcher();
        KeyBoard keyBoard = new KeyBoard();

        //按钮控制电视开关
        switcher.SetCommand(new TVOnCommand(tV));
        switcher.ButtonPush();
        switcher.ButtonPop();
        //键盘控制电视
        keyBoard.SetBindingsBetweenKeyCodeAndCommand(KeyBoard.KeyCode.On, new HashSet<ICommand>() { new TVOnCommand(tV) });
        keyBoard.SetBindingsBetweenKeyCodeAndCommand(KeyBoard.KeyCode.Off, new HashSet<ICommand>() { new TVOffCommand(tV) });
        keyBoard.SetBindingsBetweenKeyCodeAndCommand(KeyBoard.KeyCode.ChannelUp, new HashSet<ICommand>() { new TVChannelUpCommand(tV) });
        keyBoard.SetBindingsBetweenKeyCodeAndCommand(KeyBoard.KeyCode.ChannelDown, new HashSet<ICommand>() { new TVChannelDownCommand(tV) });
        keyBoard.SetBindingsBetweenKeyCodeAndCommand(KeyBoard.KeyCode.VolumeUp, new HashSet<ICommand>() { new TVVolumeUpCommand(tV) });
        keyBoard.SetBindingsBetweenKeyCodeAndCommand(KeyBoard.KeyCode.VolumeDown, new HashSet<ICommand>() { new TVVolumeDownCommand(tV) });

        keyBoard.OnKeyPressed(KeyBoard.KeyCode.On);
        keyBoard.OnKeyPressed(KeyBoard.KeyCode.Off);
        keyBoard.OnKeyPressed(KeyBoard.KeyCode.ChannelUp);
        keyBoard.OnKeyPressed(KeyBoard.KeyCode.ChannelDown);
        keyBoard.OnKeyPressed(KeyBoard.KeyCode.VolumeDown);
        keyBoard.OnKeyPressed(KeyBoard.KeyCode.VolumeUp);

        //键盘On按钮按下时，不仅电视打开，而且灯泡开始闪烁
        keyBoard.SetBindingsBetweenKeyCodeAndCommand(KeyBoard.KeyCode.On, new HashSet<ICommand>() { new FlashOnCommand(bulb) });
        //键盘Off按下时，不仅电视关掉，而且灯盘闪烁停止
        keyBoard.SetBindingsBetweenKeyCodeAndCommand(KeyBoard.KeyCode.Off, new HashSet<ICommand>() { new FlashOffCommand(bulb) });
        keyBoard.OnKeyPressed(KeyBoard.KeyCode.On);
        Thread.Sleep(8000);
        keyBoard.OnKeyPressed(KeyBoard.KeyCode.Off);

        //发送方和指令可以进行任意组合,读者可进行任意组合... ...

    }
}

#region 接收方
public class Bulb
{
    public void On()
    {
        Console.WriteLine("灯泡打开了");
    }
    public void Off()
    {
        Console.WriteLine("灯泡关闭了");
    }

    private bool flashing = false;
    public void FlashOn()
    {
        flashing = true;
        Task.Run(() =>
        {
            while (flashing)
            {
                this.On();
                Thread.Sleep(500);
                this.Off();
                Thread.Sleep(500);
            }

        });
    }

    public void FlashOff()
    {
        this.flashing = false;
    }
}
public class TV
{
    public void On()
    {
        Console.WriteLine("电视打开了");
    }

    public void Off()
    {
        Console.WriteLine("电视关闭了");
    }

    public void ChannelUp()
    {
        Console.WriteLine("电视频道+");
    }
    public void ChannelDown()
    {
        Console.WriteLine("电视频道-");
    }

    public void VolumeUp()
    {
        Console.WriteLine("电视声音+");
    }
    public void VolumeDown()
    {
        Console.WriteLine("电视声音-");
    }
}
#endregion

#region 发送方
public class Switcher
{
    ICommand Command;
    public void SetCommand(ICommand command)
    {
        this.Command = command;
    }
    public void ButtonPush()
    {
        this.Command?.Execute();
    }
    public void ButtonPop()
    {
        this.Command?.UnExecute();
    }
}

public class KeyBoard
{
    ICommand Command;
    public enum KeyCode
    {
        On, Off, ChannelUp, ChannelDown, VolumeUp, VolumeDown
    }

    private Dictionary<KeyCode, HashSet<ICommand>> CommandsBindingToKey = new Dictionary<KeyCode, HashSet<ICommand>>();

    public void SetBindingsBetweenKeyCodeAndCommand(KeyCode keyCode, HashSet<ICommand> commands)
    {
        if (this.CommandsBindingToKey.ContainsKey(keyCode))
        {
            foreach (var item in commands)
            {
                this.CommandsBindingToKey[keyCode].Add(item);
            }
        }
        else
        {
            this.CommandsBindingToKey.Add(keyCode, commands);
        }
    }

    public void OnKeyPressed(KeyCode keyCode)
    {
        Console.WriteLine(keyCode.ToString() + "被按下");
        if (this.CommandsBindingToKey.ContainsKey(keyCode))
        {
            HashSet<ICommand> commands = this.CommandsBindingToKey[keyCode];

            foreach (var item in commands)
            {
                item.Execute();
            }
        }
        else
        {
            return;
        }
    }
}

#endregion

#region 命令
public interface ICommand
{
    /// <summary>
    /// 执行
    /// </summary>
    void Execute();

    void UnExecute();
}
public class BulbOnCommand : ICommand
{
    Bulb bulb;
    public BulbOnCommand(Bulb bulb)
    {
        this.bulb = bulb;
    }
    public void Execute()
    {
        this.bulb.On();
    }

    public void UnExecute()
    {
        this.bulb.Off();
    }
}
public class BulbOffCommand : ICommand
{
    Bulb bulb;
    public BulbOffCommand(Bulb bulb)
    {
        this.bulb = bulb;
    }
    public void Execute()
    {
        this.bulb.Off();
    }

    public void UnExecute()
    {
        this.bulb.On();
    }
}
public class FlashOnCommand : ICommand
{
    Bulb bulb;

    public FlashOnCommand(Bulb bulb)
    {
        this.bulb = bulb;
    }
    public void Execute()
    {
        this.bulb.FlashOn();
    }

    public void UnExecute()
    {
        this.bulb.FlashOff();
    }
}
public class FlashOffCommand : ICommand
{
    Bulb bulb;

    public FlashOffCommand(Bulb bulb)
    {
        this.bulb = bulb;
    }
    public void Execute()
    {
        this.bulb.FlashOff();
    }

    public void UnExecute()
    {
        this.bulb.FlashOn();
    }
}
public class TVOnCommand : ICommand
{
    TV tv;
    public TVOnCommand(TV tv)
    {
        this.tv = tv;
    }

    public void Execute()
    {
        this.tv.On();
    }

    public void UnExecute()
    {
        this.tv.Off();
    }
}
public class TVOffCommand : ICommand
{
    TV tv;
    public TVOffCommand(TV tv)
    {
        this.tv = tv;
    }

    public void Execute()
    {
        this.tv.Off();
    }

    public void UnExecute()
    {
        this.tv.On();
    }
}
public class TVChannelUpCommand : ICommand
{
    TV tv;
    public TVChannelUpCommand(TV tv)
    {
        this.tv = tv;
    }

    public void Execute()
    {
        this.tv.ChannelUp();
    }

    public void UnExecute()
    {
        this.tv.ChannelDown();
    }
}
public class TVChannelDownCommand : ICommand
{
    TV tv;
    public TVChannelDownCommand(TV tv)
    {
        this.tv = tv;
    }

    public void Execute()
    {
        this.tv.ChannelDown();
    }

    public void UnExecute()
    {
        this.tv.ChannelUp();
    }
}
public class TVVolumeUpCommand : ICommand
{
    TV tv;
    public TVVolumeUpCommand(TV tv)
    {
        this.tv = tv;
    }

    public void Execute()
    {
        this.tv.VolumeUp();
    }

    public void UnExecute()
    {
        this.tv.VolumeUp();
    }
}
public class TVVolumeDownCommand : ICommand
{
    TV tv;
    public TVVolumeDownCommand(TV tv)
    {
        this.tv = tv;
    }

    public void Execute()
    {
        this.tv.VolumeDown();
    }

    public void UnExecute()
    {
        this.tv.VolumeUp();
    }
}
#endregion