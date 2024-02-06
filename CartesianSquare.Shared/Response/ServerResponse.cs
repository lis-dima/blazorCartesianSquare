
namespace CartesianSquare.Shared.Response
{
    public class ServerResponse<T>
    {
        public bool Ok { get => Errors.Count == 0; }
        public List<Error> Errors { get; set; } = new();
        public T Data { get; set; }
    }
}
