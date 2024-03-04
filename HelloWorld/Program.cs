using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[module: UnverifiableCode]

[CompilerGenerated]
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine(new Greeter("Hello", "World").Greeting);
    }
}

internal class Greeter : IEquatable<Greeter>
{
    [CompilerGenerated]
    private string _salutation_k__BackingField;

    [CompilerGenerated]
    private string _name_k__BackingField;

    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
        [CompilerGenerated]
        get
        {
            return typeof(Greeter);
        }
    }

    public string salutation
    {
        [CompilerGenerated]
        get
        {
            return _salutation_k__BackingField;
        }
        [CompilerGenerated]
        private set
        {
            _salutation_k__BackingField = value;
        }
    }

    public string name
    {
        [CompilerGenerated]
        get
        {
            return _name_k__BackingField;
        }
        [CompilerGenerated]
        private set
        {
            _name_k__BackingField = value;
        }
    }

    public string Greeting
    {
        get
        {
            return string.Concat(salutation, ", ", name, "!");
        }
    }

    public Greeter(string salutation, string name)
    {
        _salutation_k__BackingField = salutation;
        _name_k__BackingField = name;
    }

    [CompilerGenerated]
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Greeter");
        stringBuilder.Append(" { ");
        if (PrintMembers(stringBuilder))
        {
            stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
    }

    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("salutation = ");
        builder.Append((object)salutation);
        builder.Append(", name = ");
        builder.Append((object)name);
        builder.Append(", Greeting = ");
        builder.Append((object)Greeting);
        return true;
    }

    [CompilerGenerated]
    public static bool operator !=(Greeter left, Greeter right)
    {
        return !(left == right);
    }

    [CompilerGenerated]
    public static bool operator ==(Greeter left, Greeter right)
    {
        if ((object)left != right)
        {
            if ((object)left != null)
            {
                return left.Equals(right);
            }
            return false;
        }
        return true;
    }

    [CompilerGenerated]
    public override int GetHashCode()
    {
        return (EqualityComparer<Type>.Default.GetHashCode(EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_salutation_k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_name_k__BackingField);
    }

    [CompilerGenerated]
    public override bool Equals(object obj)
    {
        return Equals(obj as Greeter);
    }

    [CompilerGenerated]
    public virtual bool Equals(Greeter other)
    {
        if ((object)this != other)
        {
            if ((object)other != null && EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(_salutation_k__BackingField, other._salutation_k__BackingField))
            {
                return EqualityComparer<string>.Default.Equals(_name_k__BackingField, other._name_k__BackingField);
            }
            return false;
        }
        return true;
    }

    [CompilerGenerated]
    public virtual Greeter Clone()
    {
        return new Greeter(this);
    }

    [CompilerGenerated]
    protected Greeter(Greeter original)
    {
        _salutation_k__BackingField = original._salutation_k__BackingField;
        _name_k__BackingField = original._name_k__BackingField;
    }

    [CompilerGenerated]
    public void Deconstruct(out string salutation, out string name)
    {
        salutation = this.salutation;
        name = this.name;
    }
}
