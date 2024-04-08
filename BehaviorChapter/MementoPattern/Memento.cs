namespace MementoPattern;

public class Memento
{

}


#region 备忘录模式在文本编辑器上的使用
//历史快照
public class History
{
    private string body;
    public History(string body)
    {
        this.body = body;
    }
    public string GetBody()
    {
        return this.body;
    }
}
//文档元对象类
public class Doc
{
    public string Title { set; get; }
    public string Body { set; get; }
    public Doc(string title)
    {
        this.Title = title;
        this.Body = "";
    }

    public History CreateHistory()
    {
        return new History(this.Body);
    }
    public void RestoreHistory(History history)
    {
        this.Body = history.GetBody();
    }
}
public class Editor
{
    /// <summary>
    /// 文档元对象
    /// </summary>
    public Doc Doc;
    /// <summary>
    /// 历史快照
    /// </summary>
    private List<History> Histories;
    /// <summary>
    /// 历史快照指针
    /// </summary>
    private int historyPosition = -1;
    public Editor(Doc doc)
    {
        this.Doc = doc;
        Histories = new List<History>();
        this.BackUp();
    }
    /// <summary>
    /// 记录历史快照
    /// </summary>
    public void BackUp()
    {
        if (Histories.Count > 20)
        {
            Histories.Remove(Histories[0]);
        }
        Histories.Add(this.Doc.CreateHistory());
        historyPosition++;
    }
    /// <summary>
    /// 恢复到上一历史快照
    /// </summary>
    public void Undo()
    {
        if (historyPosition <= 0)
        {
            return;
        }
        else
        {
            this.Histories.RemoveAt(historyPosition);
            historyPosition--;
            this.Doc.RestoreHistory(this.Histories[this.historyPosition]);
        }
    }
}
#endregion