using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern;

/// <summary>
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
