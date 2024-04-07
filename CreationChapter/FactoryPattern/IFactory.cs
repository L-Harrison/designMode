using System;

namespace FactoryPattern;

public interface IFactory
{
    Enemy Create();
}

public class Enemy
{
}