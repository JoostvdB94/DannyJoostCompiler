namespace DannyJoostCompiler.VirtualMachine
{
    public class Variable
    {
        public TokenEnumeration type { get; set; }
        public object Value { get; set; }

        public Variable Copy()
        {
            return (Variable)MemberwiseClone();
        }
    }
}