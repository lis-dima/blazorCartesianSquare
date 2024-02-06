using CartesianSquare.Shared.Records;
using CartesianSquare.Shared.Response;
using CartesianSquare.Shared.Squares;

namespace CartesianSquare.Services.SquareService
{
    public interface ISquareService
    {
        public event Action<Square> OnNewSquareCreated;
        public event Action<Record> OnSetRecordOnEdit;
        public event Action OnCencelRecordOnEdit;
        public event Action<Record> OnNewRecordCreated;
        public event Action<int> OnRecordDeleted;
        public event Action<Record> OnRecordUpdated;
        public string[] SquareTypes { get; }

        public Square ActiveSquare { get; set; }
        public Task<ServerResponse<Square>> CreateSquareAsync(CreateSquareDto data);
        public Task<List<Square>> GetAllSquaresAsync(string userID);
        public Task<Square> GetSquareAsync(int squareId);
        public void SetRecordOnEdit(Record record);
        public void CencelRecordOnEdit();
        public bool IsRecordOnEdit();
        public Task<Record> CreateRecordAsync(Record data);
        public Task<int> RemoveRecord(int recordId);
        public Task<Record> UpdateRecordAsync(Record data);

    }
}
