namespace Lab12_JeremyVizcarra.Services;

public class RecursoNoEncontradoException : Exception
{
    public RecursoNoEncontradoException(string message) : base(message)
    {
    }
}
