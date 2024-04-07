## 07组合模式
 
组合模式（composite pattern)：使用多个节点对象组成的树形整体的模式。(将整体与局部‘树形结合’进行递归组合，让客户端能够以一种的方式对其进行处理）

> 它能使客户端在操作整体或者其下的每个节点对象时做出统一的响应，保证树形结构对象使用方法的一致性，使客户端不必关注对象为整体还是部分，最终达到对象复杂层次结构与客户端解耦的目的

***举例：***

```/// <summary>
/// 组件接口
/// </summary>
public interface IComponent
{
    void DoOperation();
}
/// <summary>
/// 复合组件
/// </summary>
public class Composite : IComponent
{
    List<IComponent> components = new List<IComponent>();
    public void DoOperation()
    {
        throw new NotImplementedException();
    }
    public void AddComponent(IComponent component)
    {
        if (!components.Contains(component))
        {
            components.Add(component);
        }
    }
    public void RemoveComponent(IComponent component)
    {
        if (components.Contains(component))
        {
            components.Remove(component);
        }
    }
    public IComponent GetChild(int index)
    {
        if (index < components.Count)
        {
            return components[index];
        }
        else
        {
            return null;
        }
    }
}
/// <summary>
/// 叶端组件
/// </summary>
public class Leaf : IComponent
{
    public void DoOperation()
    {
        throw new NotImplementedException();
    }
}

```