namespace Generator
{
    public interface IGeneratorCommand
    {
        string Description { get; }
        void Execute(string[] args);

    }
}