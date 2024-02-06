using CartesianSquare.Data;
using CartesianSquare.Shared.Records;
using CartesianSquare.Shared.Response;
using CartesianSquare.Shared.Squares;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CartesianSquare.Services.SquareService
{
    public class SquareService : ISquareService
    {
        public event Action<Square> OnNewSquareCreated;
        public event Action<Record> OnSetRecordOnEdit;
        public event Action OnCencelRecordOnEdit;
        public event Action<Record> OnNewRecordCreated;
        public event Action<int> OnRecordDeleted;
        public event Action<Record> OnRecordUpdated;

        public string[] SquareTypes
        {
            get =>
                [
                    "What will happen if this happens?(positive)",
                    "What will happen if this doesn’t happen?(positive)",
                    "What won’t happen if this happens?(negative)",
                    "What won’t happen if this doesn’t happen?(negative)"
                ];
        }

        public Record ActiveEditRecord { get; set; }
        public Square ActiveSquare { get; set; }

        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SquareService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context=context;
            this.httpContextAccessor=httpContextAccessor;
        }

        public async Task<ServerResponse<Square>> CreateSquareAsync(CreateSquareDto data)
        {
            var sr = new ServerResponse<Square>();
            var newSquare = new Square(data);
            context.Add(newSquare);
            await context.SaveChangesAsync();
            sr.Data = newSquare;
            OnNewSquareCreated?.Invoke(newSquare);
            return sr;
        }

        public async Task<List<Square>> GetAllSquaresAsync(string userID)
        {
            return await context.Squares.Where(s => s.UserId == userID).ToListAsync();
        }

        public async Task<Square> GetSquareAsync(int squareId)
        {
            var userId = GetCurrentUserId();
            var res = await context.Squares
                .Where(s => s.Id == squareId && s.UserId == userId)
                .Include(s => s.Records)
                .FirstOrDefaultAsync();
            return res;
        }

        string GetCurrentUserId()
        {
            var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }

        public void SetRecordOnEdit(Record record)
        {
            ActiveEditRecord = record;
            OnSetRecordOnEdit?.Invoke(ActiveEditRecord);
        }
        public void CencelRecordOnEdit()
        {
            ActiveEditRecord = null;
            OnCencelRecordOnEdit?.Invoke();
        }

        public bool IsRecordOnEdit()
        {
            return ActiveEditRecord != null;
        }

        public async Task<Record> CreateRecordAsync(Record data)
        {
            context.Add(data);
            await context.SaveChangesAsync();
            OnNewRecordCreated?.Invoke(data);
            return data;
        }

        public async Task<int> RemoveRecord(int recordId)
        {
            var rec = await context.Records.FindAsync(recordId);
            if (rec != null)
            {
                context.Records.Remove(rec);
                await context.SaveChangesAsync();
                OnRecordDeleted?.Invoke(recordId);
                return recordId;
            }
            return 0;
        }

        public async Task<Record> UpdateRecordAsync(Record data)
        {
            var rec = await context.Records.FindAsync(data.Id);
            if (rec != null)
            {
                rec.Title = data.Title;
                rec.Value = data.Value;
                rec.ParentBlock = data.ParentBlock;
                await context.SaveChangesAsync();
                OnRecordUpdated?.Invoke(rec);
                CencelRecordOnEdit();
                return rec;
            }
            return null;
        }
    }
}
