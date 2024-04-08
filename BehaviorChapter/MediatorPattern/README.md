## 19 中介模式

<h2>好好理解</h2>

中介模式(MediatorPattern)：中介是在事物之间传播信息的中间媒介。中介模式为对象架构出一个互动平台，通过减少对象间的依赖程度以达到解耦的目的。

> ***优点：将需要进行互动的多方模块组成的系统，通过一个中介平台来完成互动工作，使各个模块之间的关系更加松散、独立，最终增强系统的可复用性与可扩展性，同事提升了系统运行的效率。***

```
#region 中介模式在聊天室上的应用

#region 共事者实现
public class User
{
    public string Name { get; set; }
    protected ChatRoom ChatRoom { set; get; }

    public User(string name)
    {
        this.Name = name;
    }

    public void Login(ChatRoom chatRoom)
    {
        this.ChatRoom = chatRoom;
        this.ChatRoom.RegisterUser(this);
    }

    public void Logout(ChatRoom chatRoom)
    {
        this.ChatRoom = chatRoom;
        this.ChatRoom.DeregisterUser(this);
    }
    /// <summary>
    /// 向某人发送信息
    /// </summary>
    /// <param name="to"></param>
    /// <param name="msg"></param>
    public void SendMsg(User to, string msg)
    {
        if (to == null)
        {
            Console.WriteLine(this.Name + "发送消息给所有人:" + msg);
        }
        else
        {
            Console.WriteLine(this.Name + "发送消息给" + to.Name + ":" + msg);
        }

        this.ChatRoom.SendMsg(this, to, msg);
    }
    /// <summary>
    /// 收听某人的信息
    /// </summary>
    /// <param name="from"></param>
    /// <param name="Message"></param>
    public void ListenMsg(User from, string msg)
    {
        Console.WriteLine("                         " + this.Name + "收到来自" + from.Name + "的消息:" + msg);
    }

}
public class AdminUser : User
{
    public AdminUser(string name) : base(name) { }

    public void KickOff(User user)
    {
        Console.WriteLine("管理员" + this.Name + "把" + user.Name + "踢出聊天室");
        this.ChatRoom.DeregisterUser(user);
    }
    public void PushIn(User user)
    {
        Console.WriteLine("管理员" + this.Name + "把" + user.Name + "拉进聊天室");
        user.Login(this.ChatRoom);
    }
}
#endregion
#region 中介实现
public class ChatRoom
{
    public string RoomName { set; get; }
    private List<User> _users = new List<User>();
    public ChatRoom(string roomName)
    {
        RoomName = roomName;
    }
    public void RegisterUser(User user)
    {
        if (!this._users.Contains(user))
        {
            this._users.Add(user);
            Console.WriteLine(user.Name + "登录" + this.RoomName + "聊天室");
        }
    }
    public void DeregisterUser(User user)
    {
        if (this._users.Contains(user))
        {
            this._users.Remove(user);
            Console.WriteLine(user.Name + "退出" + this.RoomName + "聊天室");
        }
    }

    /// <summary>
    /// 传递信息
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="msg"></param>
    public void SendMsg(User from, User to, string msg)
    {
        if (to != null)
        {
            if (_users.Contains(from) && _users.Contains(to))
            {
                to.ListenMsg(from, msg);
            }
        }
        else
        {
            if (_users.Contains(from))
            {
                foreach (var item in _users)
                {
                    if (!item.Equals(from))
                    {
                        item.ListenMsg(from, msg);
                    }
                }
            }
        }
    }

}
#endregion

#endregion
```

调用：

```
 public void DO()
    {
        ChatRoom chatRoom = new ChatRoom("狼性团队");
        AdminUser user1 = new AdminUser("张三");
        User user2 = new User("李四");
        User user3 = new User("王五");
        user1.Login(chatRoom);
        user2.Login(chatRoom);
        user3.Login(chatRoom);

        user1.SendMsg(null, "明天晚上加班赶项目");
        user1.SendMsg(null, "我负责项目管理");
        user1.SendMsg(user2, "你负责需求分析");
        user1.SendMsg(user3, "你负责代码编写");
        user2.SendMsg(user1, "好的,收到");
        user3.SendMsg(user2, "拒绝加班");
        user1.KickOff(user3);

        User user4 = new User("卷王");
        user1.PushIn(user4);
        user1.SendMsg(user4, "你负责代码编写");
        user4.SendMsg(user1, "好的,收到,一定完成任务");
    }
```