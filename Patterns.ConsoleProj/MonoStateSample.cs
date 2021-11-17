namespace Patterns.ConsoleProj
{
    public class MonoState
    {
        private static int _monoStateInt;
        
        //todo (osipov): add lock here ?
        public int MonoStateInt
        {
            get => _monoStateInt;
            set => _monoStateInt = value;
        }
    }
}