using System;
using System.Reflection;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Volatility.Utilities;

public class FieldDescriptor : IPropertyDescriptor
{
    // Named with a leading underscore: in C# 14 a bare 'field' inside a property accessor binds to the
    // synthesized backing field, which broke 'field.Name' / 'field.FieldType' below.
    private readonly FieldInfo _field;

    public FieldDescriptor(FieldInfo field)
    {
        _field = field;
        // Set defaults
        Required = false;
        TypeOverride = null;
        ConverterType = null;
    }

    public string Name
    {
        get => _field.Name;
        set { }
    }

    public Type Type => _field.FieldType;

    public int Order { get; set; }

    public ScalarStyle ScalarStyle { get; set; }

    public bool CanWrite => true;

    public bool AllowNulls => true;

    public Type? TypeOverride { get; set; }

    public bool Required { get; set; }

    public Type? ConverterType { get; set; }

    public T? GetCustomAttribute<T>() where T : Attribute
    {
        return _field.GetCustomAttribute<T>();
    }

    public void Write(object target, object? value)
    {
        _field.SetValue(target, value);
    }

    public IObjectDescriptor Read(object target)
    {
        var value = _field.GetValue(target);
        return new ObjectDescriptor(value, _field.FieldType, typeof(object));
    }
}
