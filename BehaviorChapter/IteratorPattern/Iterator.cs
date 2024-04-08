using System.Collections;

namespace IteratorPattern;

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