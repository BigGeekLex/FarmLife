using System;

public interface ISelectable<T>
{
    event Action<T> OnSelected;
}