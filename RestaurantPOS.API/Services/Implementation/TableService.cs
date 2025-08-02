using AutoMapper;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

public class TableService : ITableService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContextService _userContext;
    private readonly IMapper _mapper;

    public TableService(IUnitOfWork unitOfWork, IUserContextService userContext, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TableDto>> GetAllAsync()
    {
        var tables = await _unitOfWork.Tables.GetAllAsync();
        return _mapper.Map<IEnumerable<TableDto>>(tables);
    }

    public async Task<TableDto> GetByIdAsync(int id)
    {
        var table = await _unitOfWork.Tables.GetByIdAsync(id);
        return _mapper.Map<TableDto>(table);
    }

    public async Task<TableDto> GetByGlobalIdAsync(Guid globalId)
    {
        var table = await _unitOfWork.Tables.GetByGlobalIdAsync(globalId);
        return _mapper.Map<TableDto>(table);
    }

    public async Task<TableDto> CreateOrUpdateAsync(CreateTableDto dto)
    {
        if (!dto.GlobalId.HasValue)
        {
            var table = _mapper.Map<Table>(dto);
            table.CreatedBy = _userContext.GetUserId ?? 0;
            table.CreatedOn = DateTime.UtcNow;

            await _unitOfWork.Tables.AddAsync(table);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TableDto>(table);
        }
        else
        {
            var table = await _unitOfWork.Tables.GetByGlobalIdAsync(dto.GlobalId.Value);
            if (table == null) return null;

            table.TableNumber = dto.TableNumber;
            table.IsOccupied = dto.IsOccupied;
            table.ModifiedBy = _userContext.GetUserId ?? 0;
            table.ModifiedOn = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TableDto>(table);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var table = await _unitOfWork.Tables.GetByIdAsync(id);
        if (table != null)
        {
            _unitOfWork.Tables.Delete(table);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

