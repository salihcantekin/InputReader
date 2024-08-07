using System;

namespace InputReader;

public record ConsoleKeyInfoInputValue(ConsoleKeyInfo Value) : InputValue<ConsoleKeyInfo>(Value)
{
    public bool Is(ConsoleKey key) => Value.Key == key;
    public bool Is(ConsoleKey key, ConsoleModifiers modifiers) => Value.Key == key && Value.Modifiers == modifiers;
    public bool IsKeyChar(char keyChar) => Value.KeyChar == keyChar;
}