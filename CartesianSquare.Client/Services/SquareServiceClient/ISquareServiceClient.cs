using CartesianSquare.Shared.Response;
using CartesianSquare.Shared.Squares;

namespace CartesianSquare.Client.Services.SquareServiceClient
{
    public interface ISquareServiceClient
    {
        public Task<ServerResponse<Square>> CreateSquare(CreateSquareDto data);
    }
}
