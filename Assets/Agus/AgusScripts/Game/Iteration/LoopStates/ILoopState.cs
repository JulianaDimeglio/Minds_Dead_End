public interface ILoopState
{
    void Configure(); // se ejecuta al comenzar la iteraci�n

    void CleanIteration(); // se ejecuta al finalizar la iteraci�n
}