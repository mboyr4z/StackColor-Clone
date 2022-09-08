using System;

public interface IColorChangable 
{
    public void ChangeColor(Action<ColorCategory> action);
}
