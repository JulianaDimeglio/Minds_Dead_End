public interface ILoopState
{
    void Configure(); // se ejecuta al comenzar la iteración

    void CleanIteration(); // se ejecuta al finalizar la iteración
}