 ## 14 迭代器模式
 
 迭代器模式(Iterator Pattern)： 提供一种方法顺序访问一个聚合对象中的各个元素。

 > 对任何类型的集合，要防止内部机制不被暴露或破坏，以及确保用户对每个元素有足够的访问权限，迭代器模式起到了至关重要的地作用迭代器巧妙地利用了内部类的形式与集合类分离，但是依然对齐内部的元素保有访问权限，如此便促成了集合的完美封装。在此基础上给用户提供了一套标准的迭代器接口，使各种繁杂的遍历方式得以统一。

 ***举例：***

 ```
 public class DrivingRecorder : IEnumerable
{
    private int index = 0;
    private string[] records = new string[10];
    public void Append(string record)
    {
        List<string> list = new List<string>();
        list.GetEnumerator();
        records[index] = record;
        if (index == 9)
        {
            index = 0;
        }
        else
        {
            index++;
        }
    }
    public IEnumerator GetEnumerator()
    {
        return new CustomEnumrator(records, index);
    }
    /// <summary>
    /// 自定义迭代器
    /// </summary>
    class CustomEnumrator : IEnumerator
    {
        int curIndex;
        int loopcount = 0;
        object[] objects;
        public CustomEnumrator(object[] objects, int index)
        {
            this.objects = objects;
            this.curIndex = index;
        }
        public object Current => objects[curIndex];
        public bool MoveNext()
        {
            if (loopcount > 10)
            {
                return false;
            }
            if (curIndex == 0)
            {
                curIndex = objects.Length - 1;
            }
            else
            {
                curIndex--;
            }
            loopcount++;
            return true;
        }
        public void Reset()
        {
            curIndex = 0;
        }
    }
}
```
***调用：***

```
public class Iterator
{
    public void DO()
    {
        DrivingRecorder recorder = new DrivingRecorder();
        for (int i = 0; i < 20; i++)
        {
            recorder.Append(DateTime.Now.ToString() + "--------------" + i);
        }

        IEnumerator enumerator = recorder.GetEnumerator();

        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current.ToString());
        }
    }
}
```